using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ShoulderCam;

public class Config
{
	[JsonProperty("onFootPositionXOffset")]
	public float OnFootPositionXOffset { get; set; } = 0.35f;

	[JsonProperty("onFootPositionYOffset")]
	public float OnFootPositionYOffset { get; set; }

	[JsonProperty("onFootPositionZOffset")]
	public float OnFootPositionZOffset { get; set; } = -0.2962953f;

	[JsonProperty("mountedPositionXOffset")]
	public float MountedPositionXOffset { get; set; } = 0.354939938f;

	[JsonProperty("mountedPositionYOffset")]
	public float MountedPositionYOffset { get; set; }

	[JsonProperty("mountedPositionZOffset")]
	public float MountedPositionZOffset { get; set; } = -0.333332419f;

	[JsonProperty("bearingOffset")]
	public float BearingOffset { get; set; }

	[JsonProperty("elevationOffset")]
	public float ElevationOffset { get; set; }

	[JsonProperty("thirdPersonFieldOfView")]
	public float ThirdPersonFieldOfView { get; set; } = 70f;

	[JsonProperty("shoulderCamRangedMode")]
	[JsonConverter(typeof(StringEnumConverter))]
	public ShoulderCamRangedMode ShoulderCamRangedMode { get; set; } = ShoulderCamRangedMode.RevertWhenAiming;

	[JsonProperty("revertWhenAimingReturnDelay")]
	public float RevertWhenAimingReturnDelay { get; set; } = 0.5246893f;

	[JsonProperty("shoulderCamMountedMode")]
	[JsonConverter(typeof(StringEnumConverter))]
	public ShoulderCamMountedMode ShoulderCamMountedMode { get; set; }

	[JsonProperty("shoulderSwitchMode")]
	[JsonConverter(typeof(StringEnumConverter))]
	public ShoulderSwitchMode ShoulderSwitchMode { get; set; }

	[JsonProperty("temporaryShoulderSwitchDuration")]
	public float TemporaryShoulderSwitchDuration { get; set; }

	[JsonProperty("minimumPlayerHitCamShake")]
	public float MinimumPlayerHitCamShake { get; set; } = 0.3981484f;

	[JsonProperty("playerHitCamShakeMultiplier")]
	public float PlayerHitCamShakeMultiplier { get; set; } = 1f;

	[JsonProperty("playerHitCamShakeDuration")]
	public float PlayerHitCamShakeDuration { get; set; } = 0.4976855f;

	[JsonProperty("minimumEnemyHitCamShakeAmount")]
	public float MinimumEnemyHitCamShakeAmount { get; set; } = 0.395833552f;

	[JsonProperty("enemyHitCamShakeMultiplier")]
	public float EnemyHitCamShakeMultiplier { get; set; } = 0.4976855f;

	[JsonProperty("enemyHitCamShakeDuration")]
	public float EnemyHitCamShakeDuration { get; set; } = 0.8024672f;

	[JsonProperty("enableLiveConfigUpdates")]
	public bool AreLiveConfigUpdatesEnabled { get; set; } = true;

	[JsonProperty("maxCamShakeAmount")]
	public float MaxCamShakeAmount { get; set; } = 1f;

	[JsonProperty("torsoTrackedCameraSwayAmount")]
	public float TorsoTrackedCameraSwayAmount { get; set; } = 0.1f;

	public static Config Load(string configFilePath)
	{
		if (string.IsNullOrEmpty(configFilePath) || !File.Exists(configFilePath))
		{
			return new Config();
		}
		try
		{
			string text = File.ReadAllText(configFilePath);
			return JsonConvert.DeserializeObject<Config>(text) ?? new Config();
		}
		catch (Exception innerException)
		{
			throw new InvalidOperationException("Failed to load configuration.", innerException);
		}
	}

	public void Save(string configFilePath)
	{
		File.WriteAllText(configFilePath, JsonConvert.SerializeObject(this, Formatting.Indented));
	}
}
