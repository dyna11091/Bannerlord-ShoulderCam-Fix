# Shoulder Cam

Shoulder Cam changes Mount & Blade II: Bannerlord's third-person camera into an over-the-shoulder camera, with configurable offsets, field of view, ranged-weapon behavior, mounted behavior, shoulder switching, camera shake, MCM support, and XML-based localization.

This revised build is version `1.0.0` and supports Bannerlord `v1.3.15–v1.4.x`.

## Credits

- Xorberax: original author of the base Shoulder Cam logic.
- RitchieRitteer: Nexus uploader/maintainer of the updated Shoulder Camera Mod, including the MCM-menu version and expanded camera behavior.
- Firefly Julius: `v1.0.0` revision, Bannerlord `v1.3.15–v1.4.x` compatibility pass, dependency cleanup, MCM restoration, localization support, ranged-mode fixes, and configuration coverage fixes.

Revision page: https://www.nexusmods.com/mountandblade2bannerlord/mods/11295

Upstream updated mod: https://www.nexusmods.com/mountandblade2bannerlord/mods/7515

## What This Revision Changes

- Rebuilt the module for Bannerlord `v1.3.15–v1.4.x`.
- Added explicit dependencies on `Bannerlord.Harmony` and `Bannerlord.MBOptionScreen`.
- Removed the old bundled `0Harmony.dll`; the mod now uses the Harmony module dependency.
- Restored the MCM settings menu in a safer form.
- Added MCM entries for every `config.json` field so saving from MCM does not overwrite hidden settings.
- Added XML localization for the MCM menu, initially in English and Simplified Chinese.
- Fixed `revertWhenAiming` / `revertWhenAimingReturnDelay`.
- Changed ranged mode `1` so it reverts only when a ranged weapon is equipped and the player is aiming.
- Completed temporary shoulder-switch timestamp handling.

## Requirements

Install and enable these modules before Shoulder Cam:

- Bannerlord.Harmony `v2.4.2.225` or compatible
- Bannerlord.MBOptionScreen / MCM `v5.11.3` or compatible

The module is singleplayer only.

Do not distribute a separate `0Harmony.dll` inside this mod. Harmony should come from `Bannerlord.Harmony`.

## Compatibility

This revision targets Mount & Blade II: Bannerlord `v1.3.15–v1.4.x`.

The main runtime patch targets `TaleWorlds.MountAndBlade.View.Screens.MissionScreen.UpdateCamera`. Compatibility can be affected if TaleWorlds changes the camera internals in a future version.

Because this is a camera-only module and does not add campaign data, troops, items, settlements, or save-game records, it should be safe to add or remove between game launches in an existing campaign. Do not enable, disable, replace, or delete the DLL while the game process is running.

## Installation

Place the module folder here:

```text
Mount & Blade II Bannerlord\Modules\ShoulderCam
```

Enable these modules in the launcher, in dependency order:

```text
Bannerlord.Harmony
Bannerlord.MBOptionScreen
Shoulder Cam
```

## Files

Important files:

```text
ShoulderCam\SubModule.xml
ShoulderCam\bin\Win64_Shipping_Client\ShoulderCam.dll
ShoulderCam\bin\Win64_Shipping_Client\config.json
ShoulderCam\ModuleData\Languages\EN\shouldercam_strings.xml
ShoulderCam\ModuleData\Languages\CNs\shouldercam_strings.xml
```

If `config.json` does not exist, the mod creates it with default values.

## How It Works

On module load, Shoulder Cam loads `config.json`, applies Harmony patches, and registers the MCM settings page.

During missions, it patches the game camera update flow and modifies third-person camera offset, FOV, bearing, elevation, shoulder side, and camera shake. The mod does not affect first-person camera mode, spectator mode, or the character-view/free-look key state.

When live config updates are enabled, `config.json` is reloaded while the game is running. MCM changes are saved back to the same file.

## MCM and Config

You can configure the mod through the in-game MCM menu or by editing:

```text
ShoulderCam\bin\Win64_Shipping_Client\config.json
```

MCM sliders use numeric modes:

- Ranged Mode: `0`, `1`, `2`
- Mounted Mode: `0`, `1`
- Shoulder Switch Mode: `0`, `1`, `2`

In `config.json`, enum names are also supported:

- `noRevert`
- `revertWhenAiming`
- `revertWhenEquipped`
- `revertWhenMounted`
- `noSwitching`
- `matchAttackAndBlockDirection`
- `temporarilyMatchAttackAndBlockDirection`

## Settings

### On Foot Camera

`onFootPositionXOffset`

Horizontal shoulder offset while on foot. Positive values move the camera toward the right shoulder; negative values move it toward the left shoulder.

`onFootPositionYOffset`

Forward/back camera distance while on foot.

`onFootPositionZOffset`

Camera height offset while on foot.

### Mounted Camera

`mountedPositionXOffset`

Horizontal shoulder offset while mounted.

`mountedPositionYOffset`

Forward/back camera distance while mounted.

`mountedPositionZOffset`

Camera height offset while mounted.

### Rotation and FOV

`bearingOffset`

Yaw offset in radians. Use this to rotate the camera left or right.

`elevationOffset`

Pitch offset in radians. Use this to rotate the camera up or down.

`thirdPersonFieldOfView`

Third-person camera field of view in degrees.

`torsoTrackedCameraSwayAmount`

How strongly the camera follows torso movement. Higher values make the camera feel more tied to the character's body rotation.

### Mode Behavior

`shoulderCamRangedMode`

Controls when the camera returns to the vanilla camera for ranged weapons.

- `0` / `noRevert`: never revert for ranged weapons. This can make ranged aiming inaccurate with large offsets.
- `1` / `revertWhenAiming`: revert only when a ranged weapon is equipped and the player is aiming. After aiming stops, the camera remains reverted for `revertWhenAimingReturnDelay` seconds.
- `2` / `revertWhenEquipped`: revert whenever the active weapon is ranged.

`revertWhenAimingReturnDelay`

Seconds to keep the vanilla camera after ranged aiming stops. This only applies to ranged mode `1`.

`shoulderCamMountedMode`

Controls whether the camera returns to vanilla while mounted.

- `0` / `noRevert`: keep the shoulder camera while mounted.
- `1` / `revertWhenMounted`: use the vanilla camera while mounted.

`shoulderSwitchMode`

Controls automatic shoulder switching.

- `0` / `noSwitching`: keep the default shoulder.
- `1` / `matchAttackAndBlockDirection`: switch shoulders based on attack/block direction.
- `2` / `temporarilyMatchAttackAndBlockDirection`: switch shoulders based on attack/block direction, then return to the default shoulder after `temporaryShoulderSwitchDuration`.

`temporaryShoulderSwitchDuration`

Seconds before temporary shoulder switching returns to the default shoulder.

### Camera Shake

`minimumPlayerHitCamShake`

Base camera shake when the player is hit.

`playerHitCamShakeMultiplier`

Extra player-hit shake scaled by received damage.

`playerHitCamShakeDuration`

Duration of player-hit camera shake in seconds.

`minimumEnemyHitCamShakeAmount`

Base camera shake when the player hits an enemy.

`enemyHitCamShakeMultiplier`

Extra enemy-hit shake scaled by dealt damage.

`enemyHitCamShakeDuration`

Duration of enemy-hit camera shake in seconds.

`maxCamShakeAmount`

Maximum camera shake angle in radians. This clamps all shake effects.

### Advanced

`enableLiveConfigUpdates`

When `true`, the mod reloads `config.json` during gameplay. This makes manual JSON edits apply without restarting the game. MCM changes also save back into `config.json`.

## Localization

MCM text is localized through XML files:

```text
ModuleData\Languages\EN\shouldercam_strings.xml
ModuleData\Languages\CNs\shouldercam_strings.xml
```

To add another language, copy one language folder, edit `language_data.xml`, and translate the `text` values in `shouldercam_strings.xml`. Keep the `id` values unchanged.

## Hot Swap Notes

- Safe between launches: yes, this is a camera/runtime mod and does not write custom campaign save data.
- Safe during a running game process: no. Do not replace or remove the DLL while Bannerlord is open.
- Safe to edit config while playing: yes, if `enableLiveConfigUpdates` is `true`.
- Requires restart after changing dependencies or enabling/disabling the module: yes.
