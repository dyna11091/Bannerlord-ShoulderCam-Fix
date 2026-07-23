# 越肩视角 Shoulder Cam

Shoulder Cam 会把《骑马与砍杀 2：霸主》的第三人称相机改为越肩视角，并提供偏移、视野、远程武器行为、骑乘行为、换肩、相机震动、MCM 菜单和 XML 多语言支持。

此修订版版本号为 `1.0.0`，支持 Bannerlord `v1.3.15–v1.4.x`。

## 作者与鸣谢

- Xorberax：原始 Shoulder Cam 核心逻辑作者。
- RitchieRitteer：Nexus 更新版发布者/维护者，制作了带 MCM 菜单与扩展相机逻辑的版本。
- Firefly Julius：`v1.0.0` 修订版作者，完成 Bannerlord `v1.3.15–v1.4.x` 兼容性处理、依赖清理、MCM 恢复、多语言支持、远程模式修正、配置覆盖修正等工作。

本修订版 Nexus 页面：https://www.nexusmods.com/mountandblade2bannerlord/mods/11295

上游更新版页面：https://www.nexusmods.com/mountandblade2bannerlord/mods/7515

## 本修订版做了什么

- 重新构建并适配 Bannerlord `v1.3.15–v1.4.x`。
- 在 `SubModule.xml` 中增加 `Bannerlord.Harmony` 与 `Bannerlord.MBOptionScreen` 依赖。
- 移除旧版随包分发的 `0Harmony.dll`，改为依赖 `Bannerlord.Harmony`。
- 以更稳定的方式恢复 MCM 设置菜单。
- MCM 设置补齐所有 `config.json` 字段，避免保存时覆盖未显示的配置。
- MCM 菜单支持 XML 多语言，首批支持英文与简体中文。
- 修正 `revertWhenAiming` 与 `revertWhenAimingReturnDelay`。
- 修正远程模式 `1`：现在只有“切换到远程武器并瞄准时”才恢复为原版相机。
- 补上临时换肩的时间戳逻辑。

## 依赖

请先安装并启用：

- Bannerlord.Harmony `v2.4.2.225` 或兼容版本
- Bannerlord.MBOptionScreen / MCM `v5.11.3` 或兼容版本

本 Mod 仅支持单人模式。

不要在本 Mod 内额外分发 `0Harmony.dll`。Harmony 应由 `Bannerlord.Harmony` 提供。

## 兼容性

此修订版目标游戏版本为 Mount & Blade II: Bannerlord `v1.3.15–v1.4.x`。

主要运行时补丁目标是 `TaleWorlds.MountAndBlade.View.Screens.MissionScreen.UpdateCamera`。如果 TaleWorlds 在后续版本改动相机内部字段与流程，兼容性仍可能受到影响。

本 Mod 只修改相机运行逻辑，不添加战役数据、兵种、物品、定居点或自定义存档记录。因此通常可以在退出游戏后，对已有存档添加或移除此 Mod。不要在游戏进程运行时启用、禁用、替换或删除 DLL。

## 安装

将模块文件夹放到：

```text
Mount & Blade II Bannerlord\Modules\ShoulderCam
```

启动器中按依赖顺序启用：

```text
Bannerlord.Harmony
Bannerlord.MBOptionScreen
Shoulder Cam
```

## 文件结构

重要文件：

```text
ShoulderCam\SubModule.xml
ShoulderCam\bin\Win64_Shipping_Client\ShoulderCam.dll
ShoulderCam\bin\Win64_Shipping_Client\config.json
ShoulderCam\ModuleData\Languages\EN\shouldercam_strings.xml
ShoulderCam\ModuleData\Languages\CNs\shouldercam_strings.xml
```

如果 `config.json` 不存在，Mod 会自动创建默认配置。

## 实现流程

模块加载时，Shoulder Cam 会读取 `config.json`，应用 Harmony 补丁，并注册 MCM 设置页面。

进入任务/战斗后，Mod 会补丁游戏相机更新流程，修改第三人称相机的偏移、视野、水平角、俯仰角、肩位和相机震动。本 Mod 不影响第一人称相机、旁观模式，或角色查看/自由视角按键状态。

开启实时配置更新后，游戏运行时会持续重新读取 `config.json`。MCM 中的修改也会保存回同一个文件。

## MCM 与 config.json

你可以通过游戏内 MCM 菜单配置，也可以手动编辑：

```text
ShoulderCam\bin\Win64_Shipping_Client\config.json
```

MCM 中模式选项使用数字：

- 远程模式：`0`、`1`、`2`
- 骑乘模式：`0`、`1`
- 换肩模式：`0`、`1`、`2`

在 `config.json` 中，也支持这些枚举名称：

- `noRevert`
- `revertWhenAiming`
- `revertWhenEquipped`
- `revertWhenMounted`
- `noSwitching`
- `matchAttackAndBlockDirection`
- `temporarilyMatchAttackAndBlockDirection`

## 选项说明

### 步行相机

`onFootPositionXOffset`

步行时相机的横向肩位偏移。正数偏向右肩，负数偏向左肩。

`onFootPositionYOffset`

步行时相机的前后距离。

`onFootPositionZOffset`

步行时相机的高度偏移。

### 骑乘相机

`mountedPositionXOffset`

骑乘时相机的横向肩位偏移。

`mountedPositionYOffset`

骑乘时相机的前后距离。

`mountedPositionZOffset`

骑乘时相机的高度偏移。

### 旋转与视野

`bearingOffset`

水平旋转偏移，单位为弧度。用于让相机向左或向右转。

`elevationOffset`

俯仰旋转偏移，单位为弧度。用于让相机向上或向下转。

`thirdPersonFieldOfView`

第三人称相机视野角度，单位为度。

`torsoTrackedCameraSwayAmount`

相机跟随角色躯干旋转的强度。数值越高，相机越明显跟随身体摆动。

### 模式行为

`shoulderCamRangedMode`

控制远程武器时是否恢复为原版相机。

- `0` / `noRevert`：远程武器不恢复原版相机。大幅偏移时可能导致远程瞄准不准。
- `1` / `revertWhenAiming`：只有当前装备远程武器并正在瞄准时，才恢复原版相机。停止瞄准后，会继续保持 `revertWhenAimingReturnDelay` 秒。
- `2` / `revertWhenEquipped`：当前主动武器是远程武器时，始终恢复原版相机。

`revertWhenAimingReturnDelay`

停止远程瞄准后，继续保持原版相机的秒数。仅在远程模式 `1` 生效。

`shoulderCamMountedMode`

控制骑乘时是否恢复为原版相机。

- `0` / `noRevert`：骑乘时继续使用越肩相机。
- `1` / `revertWhenMounted`：骑乘时使用原版相机。

`shoulderSwitchMode`

控制自动换肩行为。

- `0` / `noSwitching`：保持默认肩位。
- `1` / `matchAttackAndBlockDirection`：根据攻击/格挡方向切换肩位。
- `2` / `temporarilyMatchAttackAndBlockDirection`：根据攻击/格挡方向临时切换肩位，然后在 `temporaryShoulderSwitchDuration` 后回到默认肩位。

`temporaryShoulderSwitchDuration`

临时换肩后，回到默认肩位前等待的秒数。

### 相机震动

`minimumPlayerHitCamShake`

玩家受击时的基础相机震动强度。

`playerHitCamShakeMultiplier`

玩家受击时，按受到伤害缩放的额外震动倍率。

`playerHitCamShakeDuration`

玩家受击相机震动持续秒数。

`minimumEnemyHitCamShakeAmount`

玩家命中敌人时的基础相机震动强度。

`enemyHitCamShakeMultiplier`

玩家命中敌人时，按造成伤害缩放的额外震动倍率。

`enemyHitCamShakeDuration`

命中敌人相机震动持续秒数。

`maxCamShakeAmount`

相机震动角度上限，单位为弧度。所有震动效果都会被此值限制。

### 高级

`enableLiveConfigUpdates`

设为 `true` 时，游戏运行中会重新读取 `config.json`。这允许手动修改 JSON 后无需重启即可生效。MCM 中的修改也会保存到 `config.json`。

## 多语言

MCM 文本通过 XML 本地化：

```text
ModuleData\Languages\EN\shouldercam_strings.xml
ModuleData\Languages\CNs\shouldercam_strings.xml
```

如果要添加其他语言，可以复制一个语言文件夹，修改 `language_data.xml`，然后翻译 `shouldercam_strings.xml` 中的 `text` 内容。请保持 `id` 不变。

## 热插拔说明

- 退出游戏后添加/移除：通常可以。本 Mod 是相机运行时 Mod，不写入自定义战役存档数据。
- 游戏进程运行时替换 DLL：不可以。不要在 Bannerlord 已打开时替换或删除 DLL。
- 游戏中修改配置：可以，但需要 `enableLiveConfigUpdates` 为 `true`。
- 更改依赖、启用或禁用模块：需要重启游戏。
