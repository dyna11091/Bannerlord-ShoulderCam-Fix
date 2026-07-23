using System.IO;
using System.Reflection;
using HarmonyLib;
using MCM.Abstractions;
using MCM.Abstractions.Base.Global;
using TaleWorlds.MountAndBlade;

namespace ShoulderCam;

public class SubModule : MBSubModuleBase
{
	private static readonly string ConfigFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "config.json");

	public static Config Config { get; private set; }

	public static ShoulderCamSettings ShoulderCamSettings { get; private set; }

	protected override void OnSubModuleLoad()
	{
		base.OnSubModuleLoad();
		LoadConfig();
		try
		{
			Harmony harmony = new Harmony("xorberax.shouldercam");
			harmony.PatchAll();
		}
		catch (System.Exception ex)
		{
			File.WriteAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ShoulderCam.error.log"), ex.ToString());
		}
		try
		{
			RegisterMCM();
		}
		catch (System.Exception ex)
		{
			File.WriteAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ShoulderCam.mcm.error.log"), ex.ToString());
		}
	}

	protected override void OnApplicationTick(float dt)
	{
		base.OnApplicationTick(dt);
		if (Config.AreLiveConfigUpdatesEnabled)
		{
			LoadConfig();
		}
	}

	public override void OnMissionBehaviorInitialize(Mission mission)
	{
		base.OnMissionBehaviorInitialize(mission);
		mission.MissionBehaviors.Add((MissionBehavior)(object)new ShoulderCamMissionLogic());
	}

	private static void LoadConfig()
	{
		try
		{
			Config = Config.Load(ConfigFilePath);
			if (!File.Exists(ConfigFilePath))
			{
				Config.Save(ConfigFilePath);
			}
		}
		catch
		{
			Config ??= new Config();
		}
	}

	private void RegisterMCM()
	{
		if (BaseSettingsProvider.Instance != null)
		{
			ShoulderCamSettings = GlobalSettings<ShoulderCamSettings>.Instance;
		}
	}

}
