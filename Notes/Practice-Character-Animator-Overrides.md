# 综合实践角色 Animator Override 配置记录

## 本次目标

- 为 `UnityTeach3-Project/Assets/Resources/Role/` 下的 1～7 号角色 Prefab 配置可复用的 Animator。
- 角色共用同一套状态机、参数、Layer 和 Avatar Mask，只按各自武器替换 Animation Clip。
- 第三方动画继续从本地 `Assets/ArtRes/Toon_Soldiers/ToonSoldiers_2/animation/` 引用，不把素材提交到仓库。

## 人工配置与 AI Agent 协助范围

- 角色 1：由用户手动建立并配置原版 `1.controller`，使用 Knife 动画。
- 角色 2：由用户手动建立第一个 `2.overrideController`，根据火焰喷射器把 Knife 动画替换为 Heavy 动画；这也是后续批量配置的参考模板。
- 角色 3～7：由 AI Agent 识别各 Prefab 的 `WeaponContainer` 子对象武器，沿用角色 2 的覆盖结构创建并绑定 Animator Override Controller。
- AI Agent 没有修改角色 1、2 的控制器结构或动画映射。

## 武器与动画分类

| 角色 | `WeaponContainer` 下的武器 | 动画分类 | 攻击动画 |
| ---: | --- | --- | --- |
| 1 | `weapon_knife` | Knife | `knife_combat_attack_A` |
| 2 | `weapon_flamethrower` | Heavy | `heavy_combat_shoot_loop` |
| 3 | `weapon_sniper_rifle` | Infantry | `infantry_combat_shoot_bolt` |
| 4 | `weapon_handgun` | Handgun | `handgun_combat_shoot` |
| 5 | `weapon_assault_rifle_A` | Infantry | `infantry_combat_shoot` |
| 6 | `weapon_rocket_launcher` | RocketLauncher | `rocketlauncher_combat_shoot` |
| 7 | `weapon_assault_rifle_B` | Infantry | `infantry_combat_shoot` |

角色 3～7 的待机、下蹲待机、翻滚、前后左右跑步、前后左右行走及下蹲移动均按相同武器分类替换，总计每个控制器 16 项覆盖。角色 5、7 当前使用单次射击动作；如果后续玩法需要连发或点射表现，可以只替换对应攻击 Clip，无需复制状态机。

## 文件关系

- 基础状态机：`Assets/Resources/Animator/Role/1.controller`。
- 覆盖控制器：`Assets/Resources/Animator/Role/2.overrideController` 至 `7.overrideController`。
- 角色 Prefab：`Assets/Resources/Role/1.prefab` 至 `7.prefab`。
- 角色 1 直接绑定基础控制器；角色 2～7 分别绑定同编号覆盖控制器。

## 已完成的静态校验

- 1～7 号 Prefab 的 Animator 均已有控制器引用。
- 2～7 号覆盖控制器均以 `1.controller` 为基础控制器。
- 每个覆盖控制器均包含 16 项 Animation Clip 映射。
- 所有覆盖动画 GUID 均能解析到本地 FBX 及其 `.meta`。
- 角色编号、武器对象与控制器编号一致。

## 验证结果

- Unity 已成功导入角色 3～7 的 Override Controller，没有出现相关资源导入 Error。
- 用户确认综合实践第 13P“选角面板：角色资源准备”已经完成。
- 后续若需要把突击步枪改为连发或点射表现，只替换对应 Override Controller 的攻击 Clip，不需要复制或修改基础状态机。

本课已标记为完成，下一步进入第 14P“选角面板：数据准备”。
