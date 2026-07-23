using System.IO;
using System.Reflection;
using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base;
using MCM.Abstractions.Base.Global;
using ShoulderCam;
using TaleWorlds.Localization;

public class ShoulderCamSettings : AttributeGlobalSettings<ShoulderCamSettings>
{
	private static readonly string ConfigFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "config.json");

	private bool _isInitializing;

	public override string Id => "ShoulderCamMod";

	public override string DisplayName => new TextObject("{=shouldercam_display_name}Shoulder Camera Mod").ToString();

	public override string FolderName => "ShoulderCamModSettings";

	public override string FormatType => "json";

	public ShoulderCamSettings()
	{
		_isInitializing = true;
		ApplyConfig(Config.Load(ConfigFilePath));
		_isInitializing = false;
	}

	[SettingPropertyFloatingInteger("{=shouldercam_on_foot_x}On Foot X Offset", -2f, 2f, "#0.00", Order = 1, RequireRestart = false, HintText = "{=shouldercam_on_foot_x_hint}Camera offset on the character left/right axis while on foot.")]
	[SettingPropertyGroup("{=shouldercam_group_on_foot}On Foot Camera")]
	public float OnFootPositionXOffset { get; set; } = 0.35f;

	[SettingPropertyFloatingInteger("{=shouldercam_on_foot_y}On Foot Y Offset", -2f, 2f, "#0.00", Order = 2, RequireRestart = false, HintText = "{=shouldercam_on_foot_y_hint}Camera forward/back distance while on foot.")]
	[SettingPropertyGroup("{=shouldercam_group_on_foot}On Foot Camera")]
	public float OnFootPositionYOffset { get; set; }

	[SettingPropertyFloatingInteger("{=shouldercam_on_foot_z}On Foot Z Offset", -2f, 2f, "#0.00", Order = 3, RequireRestart = false, HintText = "{=shouldercam_on_foot_z_hint}Camera height offset while on foot.")]
	[SettingPropertyGroup("{=shouldercam_group_on_foot}On Foot Camera")]
	public float OnFootPositionZOffset { get; set; } = -0.5f;

	[SettingPropertyFloatingInteger("{=shouldercam_mounted_x}Mounted X Offset", -2f, 2f, "#0.00", Order = 1, RequireRestart = false, HintText = "{=shouldercam_mounted_x_hint}Camera offset on the character left/right axis while mounted.")]
	[SettingPropertyGroup("{=shouldercam_group_mounted}Mounted Camera")]
	public float MountedPositionXOffset { get; set; }

	[SettingPropertyFloatingInteger("{=shouldercam_mounted_y}Mounted Y Offset", -2f, 2f, "#0.00", Order = 2, RequireRestart = false, HintText = "{=shouldercam_mounted_y_hint}Camera forward/back distance while mounted.")]
	[SettingPropertyGroup("{=shouldercam_group_mounted}Mounted Camera")]
	public float MountedPositionYOffset { get; set; }

	[SettingPropertyFloatingInteger("{=shouldercam_mounted_z}Mounted Z Offset", -2f, 2f, "#0.00", Order = 3, RequireRestart = false, HintText = "{=shouldercam_mounted_z_hint}Camera height offset while mounted.")]
	[SettingPropertyGroup("{=shouldercam_group_mounted}Mounted Camera")]
	public float MountedPositionZOffset { get; set; }

	[SettingPropertyFloatingInteger("{=shouldercam_bearing}Bearing Offset", -2f, 2f, "#0.00", Order = 1, RequireRestart = false, HintText = "{=shouldercam_bearing_hint}Yaw offset in radians.")]
	[SettingPropertyGroup("{=shouldercam_group_rotation}Rotation and FOV")]
	public float BearingOffset { get; set; }

	[SettingPropertyFloatingInteger("{=shouldercam_elevation}Elevation Offset", -2f, 2f, "#0.00", Order = 2, RequireRestart = false, HintText = "{=shouldercam_elevation_hint}Pitch offset in radians.")]
	[SettingPropertyGroup("{=shouldercam_group_rotation}Rotation and FOV")]
	public float ElevationOffset { get; set; }

	[SettingPropertyFloatingInteger("{=shouldercam_third_person_fov}Third Person FOV", 30f, 120f, "#0.00", Order = 3, RequireRestart = false, HintText = "{=shouldercam_third_person_fov_hint}Third-person camera field of view.")]
	[SettingPropertyGroup("{=shouldercam_group_rotation}Rotation and FOV")]
	public float ThirdPersonFieldOfView { get; set; } = 65f;

	[SettingPropertyFloatingInteger("{=shouldercam_torso_sway}Torso Sway Amount", 0f, 1f, "#0.00", Order = 4, RequireRestart = false, HintText = "{=shouldercam_torso_sway_hint}Camera sway amount from torso tracking.")]
	[SettingPropertyGroup("{=shouldercam_group_rotation}Rotation and FOV")]
	public float TorsoTrackedCameraSwayAmount { get; set; }

	[SettingPropertyInteger("{=shouldercam_ranged_mode}Ranged Mode", 0, 2, "0", Order = 1, RequireRestart = false, HintText = "{=shouldercam_ranged_mode_hint}0: no revert, 1: revert only while a ranged weapon is equipped and being aimed, 2: revert while a ranged weapon is equipped.")]
	[SettingPropertyGroup("{=shouldercam_group_modes}Mode Behavior")]
	public int ShoulderCamRangedMode { get; set; }

	[SettingPropertyFloatingInteger("{=shouldercam_aim_return_delay}Aim Return Delay", 0f, 5f, "#0.00", Order = 2, RequireRestart = false, HintText = "{=shouldercam_aim_return_delay_hint}Seconds to keep the vanilla camera after ranged aiming stops.")]
	[SettingPropertyGroup("{=shouldercam_group_modes}Mode Behavior")]
	public float RevertWhenAimingReturnDelay { get; set; }

	[SettingPropertyInteger("{=shouldercam_mounted_mode}Mounted Mode", 0, 1, "0", Order = 3, RequireRestart = false, HintText = "{=shouldercam_mounted_mode_hint}0: no revert, 1: revert while mounted.")]
	[SettingPropertyGroup("{=shouldercam_group_modes}Mode Behavior")]
	public int ShoulderCamMountedMode { get; set; }

	[SettingPropertyInteger("{=shouldercam_switch_mode}Shoulder Switch Mode", 0, 2, "0", Order = 4, RequireRestart = false, HintText = "{=shouldercam_switch_mode_hint}0: no switching, 1: match attack/block direction, 2: temporarily match attack/block direction.")]
	[SettingPropertyGroup("{=shouldercam_group_modes}Mode Behavior")]
	public int ShoulderSwitchMode { get; set; }

	[SettingPropertyFloatingInteger("{=shouldercam_switch_duration}Temporary Switch Duration", 0f, 5f, "#0.00", Order = 5, RequireRestart = false, HintText = "{=shouldercam_switch_duration_hint}Seconds before the temporary shoulder switch returns to the default shoulder.")]
	[SettingPropertyGroup("{=shouldercam_group_modes}Mode Behavior")]
	public float TemporaryShoulderSwitchDuration { get; set; }

	[SettingPropertyFloatingInteger("{=shouldercam_min_player_shake}Minimum Player Hit Shake", 0f, 1f, "#0.00", Order = 1, RequireRestart = false, HintText = "{=shouldercam_min_player_shake_hint}Base shake amount when the player is hit.")]
	[SettingPropertyGroup("{=shouldercam_group_shake}Camera Shake")]
	public float MinimumPlayerHitCamShake { get; set; }

	[SettingPropertyFloatingInteger("{=shouldercam_player_shake_mult}Player Hit Shake Multiplier", 0f, 5f, "#0.00", Order = 2, RequireRestart = false, HintText = "{=shouldercam_player_shake_mult_hint}Damage-scaled shake multiplier when the player is hit.")]
	[SettingPropertyGroup("{=shouldercam_group_shake}Camera Shake")]
	public float PlayerHitCamShakeMultiplier { get; set; }

	[SettingPropertyFloatingInteger("{=shouldercam_player_shake_duration}Player Hit Shake Duration", 0f, 5f, "#0.00", Order = 3, RequireRestart = false, HintText = "{=shouldercam_player_shake_duration_hint}Shake duration when the player is hit.")]
	[SettingPropertyGroup("{=shouldercam_group_shake}Camera Shake")]
	public float PlayerHitCamShakeDuration { get; set; }

	[SettingPropertyFloatingInteger("{=shouldercam_min_enemy_shake}Minimum Enemy Hit Shake", 0f, 1f, "#0.00", Order = 4, RequireRestart = false, HintText = "{=shouldercam_min_enemy_shake_hint}Base shake amount when the player hits an enemy.")]
	[SettingPropertyGroup("{=shouldercam_group_shake}Camera Shake")]
	public float MinimumEnemyHitCamShakeAmount { get; set; }

	[SettingPropertyFloatingInteger("{=shouldercam_enemy_shake_mult}Enemy Hit Shake Multiplier", 0f, 5f, "#0.00", Order = 5, RequireRestart = false, HintText = "{=shouldercam_enemy_shake_mult_hint}Damage-scaled shake multiplier when the player hits an enemy.")]
	[SettingPropertyGroup("{=shouldercam_group_shake}Camera Shake")]
	public float EnemyHitCamShakeMultiplier { get; set; }

	[SettingPropertyFloatingInteger("{=shouldercam_enemy_shake_duration}Enemy Hit Shake Duration", 0f, 5f, "#0.00", Order = 6, RequireRestart = false, HintText = "{=shouldercam_enemy_shake_duration_hint}Shake duration when the player hits an enemy.")]
	[SettingPropertyGroup("{=shouldercam_group_shake}Camera Shake")]
	public float EnemyHitCamShakeDuration { get; set; }

	[SettingPropertyFloatingInteger("{=shouldercam_max_shake}Max Camera Shake", 0f, 1f, "#0.00", Order = 7, RequireRestart = false, HintText = "{=shouldercam_max_shake_hint}Maximum shake angle in radians.")]
	[SettingPropertyGroup("{=shouldercam_group_shake}Camera Shake")]
	public float MaxCamShakeAmount { get; set; }

	[SettingPropertyBool("{=shouldercam_live_updates}Enable Live Config Updates", Order = 1, RequireRestart = false, HintText = "{=shouldercam_live_updates_hint}Reload config.json while the game is running.")]
	[SettingPropertyGroup("{=shouldercam_group_advanced}Advanced")]
	public bool EnableLiveConfigUpdates { get; set; } = true;

	public override void OnPropertyChanged(string propertyName)
	{
		base.OnPropertyChanged(propertyName);
		if (!_isInitializing)
		{
			ToConfig().Save(ConfigFilePath);
		}
	}

	private void ApplyConfig(Config config)
	{
		OnFootPositionXOffset = config.OnFootPositionXOffset;
		OnFootPositionYOffset = config.OnFootPositionYOffset;
		OnFootPositionZOffset = config.OnFootPositionZOffset;
		MountedPositionXOffset = config.MountedPositionXOffset;
		MountedPositionYOffset = config.MountedPositionYOffset;
		MountedPositionZOffset = config.MountedPositionZOffset;
		BearingOffset = config.BearingOffset;
		ElevationOffset = config.ElevationOffset;
		ThirdPersonFieldOfView = config.ThirdPersonFieldOfView;
		ShoulderCamRangedMode = (int)config.ShoulderCamRangedMode;
		RevertWhenAimingReturnDelay = config.RevertWhenAimingReturnDelay;
		ShoulderCamMountedMode = (int)config.ShoulderCamMountedMode;
		ShoulderSwitchMode = (int)config.ShoulderSwitchMode;
		TemporaryShoulderSwitchDuration = config.TemporaryShoulderSwitchDuration;
		MinimumPlayerHitCamShake = config.MinimumPlayerHitCamShake;
		PlayerHitCamShakeMultiplier = config.PlayerHitCamShakeMultiplier;
		PlayerHitCamShakeDuration = config.PlayerHitCamShakeDuration;
		MinimumEnemyHitCamShakeAmount = config.MinimumEnemyHitCamShakeAmount;
		EnemyHitCamShakeMultiplier = config.EnemyHitCamShakeMultiplier;
		EnemyHitCamShakeDuration = config.EnemyHitCamShakeDuration;
		EnableLiveConfigUpdates = config.AreLiveConfigUpdatesEnabled;
		MaxCamShakeAmount = config.MaxCamShakeAmount;
		TorsoTrackedCameraSwayAmount = config.TorsoTrackedCameraSwayAmount;
	}

	private Config ToConfig()
	{
		return new Config
		{
			OnFootPositionXOffset = OnFootPositionXOffset,
			OnFootPositionYOffset = OnFootPositionYOffset,
			OnFootPositionZOffset = OnFootPositionZOffset,
			MountedPositionXOffset = MountedPositionXOffset,
			MountedPositionYOffset = MountedPositionYOffset,
			MountedPositionZOffset = MountedPositionZOffset,
			BearingOffset = BearingOffset,
			ElevationOffset = ElevationOffset,
			ThirdPersonFieldOfView = ThirdPersonFieldOfView,
			ShoulderCamRangedMode = (ShoulderCamRangedMode)ShoulderCamRangedMode,
			RevertWhenAimingReturnDelay = RevertWhenAimingReturnDelay,
			ShoulderCamMountedMode = (ShoulderCamMountedMode)ShoulderCamMountedMode,
			ShoulderSwitchMode = (ShoulderSwitchMode)ShoulderSwitchMode,
			TemporaryShoulderSwitchDuration = TemporaryShoulderSwitchDuration,
			MinimumPlayerHitCamShake = MinimumPlayerHitCamShake,
			PlayerHitCamShakeMultiplier = PlayerHitCamShakeMultiplier,
			PlayerHitCamShakeDuration = PlayerHitCamShakeDuration,
			MinimumEnemyHitCamShakeAmount = MinimumEnemyHitCamShakeAmount,
			EnemyHitCamShakeMultiplier = EnemyHitCamShakeMultiplier,
			EnemyHitCamShakeDuration = EnemyHitCamShakeDuration,
			AreLiveConfigUpdatesEnabled = EnableLiveConfigUpdates,
			MaxCamShakeAmount = MaxCamShakeAmount,
			TorsoTrackedCameraSwayAmount = TorsoTrackedCameraSwayAmount
		};
	}
}
