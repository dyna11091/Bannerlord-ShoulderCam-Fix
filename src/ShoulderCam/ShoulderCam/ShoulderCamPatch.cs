using System;
using HarmonyLib;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.TwoDimension;

namespace ShoulderCam;

[HarmonyPatch(typeof(MissionScreen))]
[HarmonyPatch("UpdateCamera")]
internal static class ShoulderCamPatch
{
	private static ShoulderPosition _focusedShoulderPosition = ShoulderPosition.Right;

	private static float _alternateShoulderSwitchTimestamp;

	private static float _revertRangedModeEndTimestamp;

	private static float _camShakeAmount;

	private static float _camShakeEndTimestamp;

	public static void ShakeCamera(float amount, float duration)
	{
		_camShakeAmount = Mathf.Clamp(amount, 0f, SubModule.Config.MaxCamShakeAmount);
		_camShakeEndTimestamp = Mission.Current.CurrentTime + duration;
	}

	public static void Prefix(MissionScreen __instance, ref float ____cameraSpecialTargetFOV, ref float ____cameraSpecialTargetDistanceToAdd, ref float ____cameraSpecialTargetAddedBearing, ref float ____cameraSpecialTargetAddedElevation, ref Vec3 ____cameraSpecialTargetPositionToAdd)
	{
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Invalid comparison between Unknown and I4
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		if (!ShouldApplyCameraTransformation(__instance))
		{
			if (!__instance.InputManager.IsGameKeyDown(25) && (int)__instance.Mission.Mode != 1)
			{
				____cameraSpecialTargetFOV = 65f;
				____cameraSpecialTargetDistanceToAdd = 0f;
				____cameraSpecialTargetAddedBearing = 0f;
				____cameraSpecialTargetAddedElevation = 0f;
				____cameraSpecialTargetPositionToAdd = Vec3.Zero;
			}
		}
		else
		{
			Agent mainAgent = __instance.Mission.MainAgent;
			UpdateFocusedShoulderPosition(__instance, mainAgent);
			sbyte headLookDirectionBoneIndex = mainAgent.Monster.HeadLookDirectionBoneIndex;
			MatrixFrame boneEntitialFrame = mainAgent.AgentVisuals.GetSkeleton().GetBoneEntitialFrame(headLookDirectionBoneIndex);
			Vec3 firstPersonCameraOffset = mainAgent.Monster.FirstPersonCameraOffsetWrtHead;
			boneEntitialFrame.origin = boneEntitialFrame.TransformToParent(in firstPersonCameraOffset);
			____cameraSpecialTargetFOV = SubModule.Config.ThirdPersonFieldOfView;
			____cameraSpecialTargetDistanceToAdd = ((mainAgent.MountAgent == null) ? SubModule.Config.OnFootPositionYOffset : SubModule.Config.MountedPositionYOffset);
			____cameraSpecialTargetAddedBearing = SubModule.Config.BearingOffset + boneEntitialFrame.rotation.f.z * SubModule.Config.TorsoTrackedCameraSwayAmount;
			____cameraSpecialTargetAddedElevation = SubModule.Config.ElevationOffset + boneEntitialFrame.rotation.f.x * SubModule.Config.TorsoTrackedCameraSwayAmount;
			Vec3 camShakeVector = GetCamShakeVector();
			____cameraSpecialTargetAddedBearing += camShakeVector.z;
			____cameraSpecialTargetAddedElevation += camShakeVector.x;
		}
	}

	public static void Postfix(MissionScreen __instance, ref Vec3 ____cameraSpecialTargetPositionToAdd)
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		if (!ShouldApplyCameraTransformation(__instance))
		{
			____cameraSpecialTargetPositionToAdd = Vec3.Zero;
			return;
		}
		Agent mainAgent = __instance.Mission.MainAgent;
		UpdateFocusedShoulderPosition(__instance, mainAgent);
		sbyte headLookDirectionBoneIndex = mainAgent.Monster.HeadLookDirectionBoneIndex;
		MatrixFrame boneEntitialFrame = mainAgent.AgentVisuals.GetSkeleton().GetBoneEntitialFrame(headLookDirectionBoneIndex);
		Vec3 firstPersonCameraOffset = mainAgent.Monster.FirstPersonCameraOffsetWrtHead;
		boneEntitialFrame.origin = boneEntitialFrame.TransformToParent(in firstPersonCameraOffset);
		boneEntitialFrame.origin.x = boneEntitialFrame.origin.x + ((mainAgent.MountAgent == null) ? SubModule.Config.OnFootPositionXOffset : SubModule.Config.MountedPositionXOffset) * _focusedShoulderPosition.GetOffsetValue();
		MatrixFrame frame = mainAgent.AgentVisuals.GetFrame();
		MatrixFrame val = frame.TransformToParent(in boneEntitialFrame);
		____cameraSpecialTargetPositionToAdd = new Vec3(val.origin.x - mainAgent.Position.x, val.origin.y - mainAgent.Position.y, (mainAgent.MountAgent == null) ? SubModule.Config.OnFootPositionZOffset : SubModule.Config.MountedPositionZOffset, -1f);
	}

	private static void UpdateFocusedShoulderPosition(MissionScreen missionScreen, Agent mainAgent)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Invalid comparison between Unknown and I4
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Invalid comparison between Unknown and I4
		if (SubModule.Config.ShoulderSwitchMode != ShoulderSwitchMode.MatchAttackAndBlockDirection && SubModule.Config.ShoulderSwitchMode != ShoulderSwitchMode.TemporarilyMatchAttackAndBlockDirection)
		{
			return;
		}
		Agent.UsageDirection currentActionDirection = mainAgent.GetCurrentActionDirection(1);
		bool isAttackingOrBlocking = missionScreen.InputManager.IsGameKeyDown(9) || missionScreen.InputManager.IsGameKeyDown(10);
		if (isAttackingOrBlocking)
		{
			_alternateShoulderSwitchTimestamp = missionScreen.Mission.CurrentTime;
			if (currentActionDirection == Agent.UsageDirection.AttackLeft || currentActionDirection == Agent.UsageDirection.DefendLeft)
			{
				_focusedShoulderPosition = ShoulderPosition.Left;
			}
			else if (currentActionDirection == Agent.UsageDirection.AttackRight || currentActionDirection == Agent.UsageDirection.DefendRight)
			{
				_focusedShoulderPosition = ShoulderPosition.Right;
			}
		}
		else if (ShouldReturnFocusToOriginalShoulder(missionScreen))
		{
			_focusedShoulderPosition = ShoulderPosition.Right;
		}
	}

	private static bool ShouldReturnFocusToOriginalShoulder(MissionScreen missionScreen)
	{
		float num = _alternateShoulderSwitchTimestamp + SubModule.Config.TemporaryShoulderSwitchDuration;
		return SubModule.Config.ShoulderSwitchMode == ShoulderSwitchMode.TemporarilyMatchAttackAndBlockDirection && missionScreen.Mission.CurrentTime > num;
	}

	private static bool ShouldApplyCameraTransformation(MissionScreen missionScreen)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Invalid comparison between Unknown and I4
		Agent mainAgent = missionScreen.Mission.MainAgent;
		MissionMode mode = missionScreen.Mission.Mode;
		bool cameraIsFirstPerson = missionScreen.Mission.CameraIsFirstPerson;
		return mainAgent != null && (int)mode != 1 && !missionScreen.InputManager.IsGameKeyDown(25) && !cameraIsFirstPerson && !mainAgent.ShouldRevertCameraForRangedMode(missionScreen) && !mainAgent.ShouldRevertCameraForMountMode();
	}

	private static bool ShouldRevertCameraForRangedMode(this Agent agent, MissionScreen missionScreen)
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Invalid comparison between Unknown and I4
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		if (SubModule.Config.ShoulderCamRangedMode == ShoulderCamRangedMode.NoRevert)
		{
			return false;
		}
		if (SubModule.Config.ShoulderCamRangedMode == ShoulderCamRangedMode.RevertWhenAiming)
		{
			bool isRangedWeaponEquipped = agent.IsPrimaryWieldedWeaponRanged();
			if (!isRangedWeaponEquipped)
			{
				_revertRangedModeEndTimestamp = 0f;
				return false;
			}
			if (missionScreen.InputManager.IsGameKeyDown(9))
			{
				_revertRangedModeEndTimestamp = missionScreen.Mission.CurrentTime + SubModule.Config.RevertWhenAimingReturnDelay;
				return true;
			}
			return missionScreen.Mission.CurrentTime < _revertRangedModeEndTimestamp;
		}
		if (SubModule.Config.ShoulderCamRangedMode == ShoulderCamRangedMode.RevertWhenEquipped)
		{
			return agent.IsPrimaryWieldedWeaponRanged();
		}
		return false;
	}

	private static bool IsPrimaryWieldedWeaponRanged(this Agent agent)
	{
		EquipmentIndex wieldedItemIndex = agent.GetPrimaryWieldedItemIndex();
		if (wieldedItemIndex == EquipmentIndex.None)
		{
			return false;
		}
		MissionWeapon val = agent.Equipment[wieldedItemIndex];
		WeaponComponentData weaponComponentDataForUsage = val.GetWeaponComponentDataForUsage(val.CurrentUsageIndex);
		return weaponComponentDataForUsage != null && weaponComponentDataForUsage.IsRangedWeapon;
	}

	private static bool ShouldRevertCameraForMountMode(this Agent agent)
	{
		return SubModule.Config.ShoulderCamMountedMode == ShoulderCamMountedMode.RevertWhenMounted && agent.MountAgent != null;
	}

	public static Vec3 GetCamShakeVector()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		float num = Math.Max(_camShakeEndTimestamp - Mission.Current.CurrentTime, 0f);
		return new Vec3(MBRandom.RandomFloatNormal * _camShakeAmount * num, MBRandom.RandomFloatNormal * _camShakeAmount * num, MBRandom.RandomFloatNormal * _camShakeAmount * num, -1f);
	}
}
