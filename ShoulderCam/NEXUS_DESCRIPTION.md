Shoulder Cam changes the third-person camera into an over-the-shoulder camera, with MCM settings, XML localization, and Bannerlord v1.3.15-v1.4.x compatibility. 越肩视角模组将第三人称相机改为越肩视角，支持 MCM 设置、XML 多语言，并适配 Bannerlord v1.3.15-v1.4.x。

## English

### Summary

Shoulder Cam is an over-the-shoulder third-person camera mod for Mount & Blade II: Bannerlord.

This revision updates the original Shoulder Cam / Shoulder Camera Mod for Bannerlord v1.3.15-v1.4.x, restores the MCM settings menu, adds XML-based localization, cleans up dependencies, and fixes several camera behavior issues.

The mod is designed for players who want a tighter, more action-focused third-person view while keeping ranged aiming, mounted play, and first-person mode usable.

### Features

* Over-the-shoulder third-person camera.
* Separate camera offsets for on-foot and mounted gameplay.
* Configurable horizontal offset, forward/back distance, height, FOV, bearing, and elevation.
* Ranged weapon behavior modes:
  * Never revert.
  * Revert only when a ranged weapon is equipped and being aimed.
  * Revert whenever a ranged weapon is equipped.
* Mounted behavior mode:
  * Keep the shoulder camera while mounted.
  * Revert to the vanilla camera while mounted.
* Optional shoulder switching based on attack/block direction.
* Optional temporary shoulder switching with a configurable return delay.
* Player-hit and enemy-hit camera shake settings.
* MCM settings menu.
* XML localization support.
* English and Simplified Chinese included.
* Full config coverage in MCM, so saving from the menu does not overwrite hidden settings.

### Credits

* Xorberax: original Shoulder Cam base logic.
* RitchieRitteer: Nexus uploader/maintainer of the updated Shoulder Camera Mod, including the MCM-menu version and expanded camera behavior.
* Firefly Julius: v1.0.0 revision, Bannerlord v1.3.15-v1.4.x compatibility pass, dependency cleanup, MCM restoration, XML localization, ranged-mode fixes, default config update, and configuration coverage fixes.

### Requirements

* Mount & Blade II: Bannerlord v1.3.15-v1.4.x
* Bannerlord.Harmony
* Bannerlord.MBOptionScreen / MCM
* Singleplayer

Recommended tested dependency versions:

* Bannerlord.Harmony v2.4.2.225
* Bannerlord.MBOptionScreen / MCM v5.11.3

Do not install or distribute an extra 0Harmony.dll inside this mod. Harmony should come from Bannerlord.Harmony.

### Installation

1. Install Bannerlord.Harmony.
2. Install Bannerlord.MBOptionScreen / MCM.
3. Place the ShoulderCam folder in:

```text
Mount & Blade II Bannerlord\Modules\ShoulderCam
```

4. Enable the modules in the launcher.

Suggested load order:

```text
Bannerlord.Harmony
Bannerlord.MBOptionScreen
Shoulder Cam
```

### How to Use

Open the in-game Mod Options / MCM menu and select Shoulder Camera Mod.

You can adjust the camera in real time through MCM. Settings are saved to:

```text
ShoulderCam\bin\Win64_Shipping_Client\config.json
```

If live config updates are enabled, manual edits to config.json can also be reloaded while the game is running.

### Main Settings

On Foot Camera:

* X Offset: left/right shoulder position while on foot.
* Y Offset: forward/back camera distance while on foot.
* Z Offset: camera height while on foot.

Mounted Camera:

* X Offset: left/right shoulder position while mounted.
* Y Offset: forward/back camera distance while mounted.
* Z Offset: camera height while mounted.

Rotation and FOV:

* Bearing Offset: yaw offset in radians.
* Elevation Offset: pitch offset in radians.
* Third Person FOV: third-person field of view.
* Torso Sway Amount: how strongly the camera follows torso movement.

Mode Behavior:

* Ranged Mode 0: never revert for ranged weapons.
* Ranged Mode 1: revert only while a ranged weapon is equipped and the player is aiming.
* Ranged Mode 2: revert whenever a ranged weapon is equipped.
* Aim Return Delay: how long the vanilla camera remains active after ranged aiming stops.
* Mounted Mode 0: keep shoulder camera while mounted.
* Mounted Mode 1: revert to vanilla camera while mounted.
* Shoulder Switch Mode 0: no shoulder switching.
* Shoulder Switch Mode 1: match attack/block direction.
* Shoulder Switch Mode 2: temporarily match attack/block direction.
* Temporary Switch Duration: delay before temporary shoulder switching returns to the default shoulder.

Camera Shake:

* Player hit shake amount, multiplier, and duration.
* Enemy hit shake amount, multiplier, and duration.
* Maximum shake amount.

Advanced:

* Enable Live Config Updates: reload config.json while the game is running.

### Language Support

This mod includes XML localization files:

```text
ModuleData\Languages\EN\shouldercam_strings.xml
ModuleData\Languages\CNs\shouldercam_strings.xml
```

Users may freely add translations by copying one language folder, editing language_data.xml, and translating the text values in shouldercam_strings.xml.

Keep the string IDs unchanged.

### Compatibility

This revision targets Bannerlord v1.3.15-v1.4.x.

The mod patches MissionScreen.UpdateCamera, so it may conflict with mods that heavily rewrite:

* Third-person camera behavior.
* MissionScreen camera logic.
* First-person/third-person camera switching.
* Combat camera effects.
* Large combat or camera overhaul mods.

It does not add campaign data, troops, items, settlements, heroes, or save-game records.

### Uninstallation

For the safest uninstall:

1. Save your game.
2. Exit Bannerlord completely.
3. Disable or remove Shoulder Cam.

This mod is camera-only and does not store custom campaign data in saves, so it should generally be safe to remove between game launches.

Do not replace, delete, or disable the DLL while the game is running.

## 中文

### 简介

越肩视角 Shoulder Cam 是一个用于《骑马与砍杀 2：霸主》的第三人称越肩相机 Mod。

此修订版将原始 Shoulder Cam / Shoulder Camera Mod 更新到 Bannerlord v1.3.15-v1.4.x，恢复 MCM 设置菜单，加入 XML 多语言支持，清理依赖，并修正多个相机行为问题。

本 Mod 适合想要更贴近角色、更偏动作游戏视角的玩家，同时尽量保持远程瞄准、骑乘和第一人称模式可用。

### 功能

* 第三人称越肩相机。
* 步行与骑乘分别设置相机偏移。
* 可调整横向偏移、前后距离、高度、视野、水平角和俯仰角。
* 远程武器行为模式：
  * 不恢复原版相机。
  * 仅在装备远程武器并瞄准时恢复原版相机。
  * 装备远程武器时始终恢复原版相机。
* 骑乘行为模式：
  * 骑乘时保留越肩相机。
  * 骑乘时恢复原版相机。
* 可根据攻击/格挡方向自动换肩。
* 支持临时换肩，并可设置返回默认肩位的延迟。
* 可设置玩家受击与命中敌人时的相机震动。
* 支持 MCM 设置菜单。
* 支持 XML 多语言。
* 内置英文与简体中文。
* MCM 覆盖所有 config.json 字段，避免保存时覆盖隐藏配置。

### 作者与鸣谢

* Xorberax：原始 Shoulder Cam 核心逻辑作者。
* RitchieRitteer：Nexus 更新版发布者/维护者，制作了带 MCM 菜单与扩展相机逻辑的版本。
* Firefly Julius：v1.0.0 修订版作者，完成 Bannerlord v1.3.15-v1.4.x 兼容性处理、依赖清理、MCM 恢复、XML 多语言、远程模式修正、默认配置更新和配置覆盖修正。

### 前置需求

* Mount & Blade II: Bannerlord v1.3.15-v1.4.x
* Bannerlord.Harmony
* Bannerlord.MBOptionScreen / MCM
* 单人模式

推荐测试版本：

* Bannerlord.Harmony v2.4.2.225
* Bannerlord.MBOptionScreen / MCM v5.11.3

不要在本 Mod 内额外安装或分发 0Harmony.dll。Harmony 应由 Bannerlord.Harmony 提供。

### 安装

1. 安装 Bannerlord.Harmony。
2. 安装 Bannerlord.MBOptionScreen / MCM。
3. 将 ShoulderCam 文件夹放入：

```text
Mount & Blade II Bannerlord\Modules\ShoulderCam
```

4. 在启动器中启用模块。

建议排序：

```text
Bannerlord.Harmony
Bannerlord.MBOptionScreen
Shoulder Cam
```

### 如何使用

进入游戏内 Mod Options / MCM 菜单，选择 Shoulder Camera Mod。

你可以通过 MCM 实时调整相机。设置会保存到：

```text
ShoulderCam\bin\Win64_Shipping_Client\config.json
```

如果启用了实时配置更新，也可以在游戏运行时手动编辑 config.json，并让 Mod 重新读取配置。

### 主要设置

步行相机：

* X 偏移：步行时相机左右肩位。
* Y 偏移：步行时相机前后距离。
* Z 偏移：步行时相机高度。

骑乘相机：

* X 偏移：骑乘时相机左右肩位。
* Y 偏移：骑乘时相机前后距离。
* Z 偏移：骑乘时相机高度。

旋转与视野：

* 水平角偏移：水平旋转偏移，单位为弧度。
* 俯仰角偏移：俯仰旋转偏移，单位为弧度。
* 第三人称视野：第三人称相机 FOV。
* 躯干跟随摆动：相机跟随角色躯干运动的强度。

模式行为：

* 远程模式 0：远程武器不恢复原版相机。
* 远程模式 1：只有装备远程武器并正在瞄准时，恢复原版相机。
* 远程模式 2：装备远程武器时始终恢复原版相机。
* 瞄准恢复延迟：停止远程瞄准后，原版相机继续保持的时间。
* 骑乘模式 0：骑乘时保留越肩相机。
* 骑乘模式 1：骑乘时恢复原版相机。
* 换肩模式 0：不换肩。
* 换肩模式 1：匹配攻击/格挡方向。
* 换肩模式 2：临时匹配攻击/格挡方向。
* 临时换肩持续时间：临时换肩后回到默认肩位前的延迟。

相机震动：

* 玩家受击震动强度、倍率和持续时间。
* 命中敌人震动强度、倍率和持续时间。
* 最大震动强度。

高级：

* 启用实时配置更新：游戏运行时重新读取 config.json。

### 语言支持

本 Mod 包含 XML 本地化文件：

```text
ModuleData\Languages\EN\shouldercam_strings.xml
ModuleData\Languages\CNs\shouldercam_strings.xml
```

用户可以自由添加翻译：复制一个语言文件夹，修改 language_data.xml，然后翻译 shouldercam_strings.xml 中的 text 内容。

请保持字符串 ID 不变。

### 兼容性

此修订版目标版本为 Bannerlord v1.3.15-v1.4.x。

本 Mod 补丁目标是 MissionScreen.UpdateCamera，因此可能与以下类型 Mod 冲突：

* 大幅修改第三人称相机行为的 Mod。
* 大幅修改 MissionScreen 相机逻辑的 Mod。
* 修改第一人称/第三人称切换的 Mod。
* 修改战斗相机效果的 Mod。
* 大型战斗或相机 overhaul。

本 Mod 不添加战役数据、兵种、物品、定居点、英雄或自定义存档记录。

### 卸载

更稳妥的卸载步骤：

1. 保存游戏。
2. 完全退出 Bannerlord。
3. 禁用或移除 Shoulder Cam。

本 Mod 只修改相机运行逻辑，不向存档写入自定义战役数据，因此通常可以在退出游戏后安全移除。

不要在游戏运行时替换、删除或禁用 DLL。
