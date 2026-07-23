using ShoulderCam;
using TaleWorlds.MountAndBlade;

public class ShoulderCamMissionLogic : MissionLogic
{
	private float _cameraXOffset = 0.35f;

	private float _cameraYOffset = 0f;

	private float _cameraZOffset = -0.5f;

	private float _cameraFieldOfView = 65f;

	private bool _isAimingRangedWeapon = false;

	private ShoulderCamRangedMode _rangedMode;

	public void UpdateCameraOffsets(float xOffset, float yOffset, float zOffset, float fov, ShoulderCamRangedMode rangedMode)
	{
		_cameraXOffset = xOffset;
		_cameraYOffset = yOffset;
		_cameraZOffset = zOffset;
		_cameraFieldOfView = fov;
		_rangedMode = rangedMode;
		HandleCameraRevert();
	}

	public void HandleCameraRevert()
	{
		switch (_rangedMode)
		{
		case ShoulderCamRangedMode.RevertWhenAiming:
			if (!_isAimingRangedWeapon)
			{
				ResetCameraPosition();
			}
			break;
		case ShoulderCamRangedMode.RevertWhenEquipped:
			if (!_isAimingRangedWeapon)
			{
				ResetCameraPosition();
			}
			break;
		case ShoulderCamRangedMode.NoRevert:
			break;
		}
	}

	private void ResetCameraPosition()
	{
		_cameraXOffset = 0.35f;
		_cameraYOffset = 0f;
		_cameraZOffset = -0.5f;
		_cameraFieldOfView = 65f;
	}

	public void OnPlayerAiming(bool isAiming)
	{
		_isAimingRangedWeapon = isAiming;
	}
}
