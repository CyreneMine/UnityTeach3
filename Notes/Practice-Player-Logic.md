# 综合实践第 25P：玩家逻辑与角色 Prefab 配置

## 当前进度

- 第 25P“玩家逻辑”仍记为学习中。基础代码、角色 Prefab 绑定、关卡内游戏界面初始化和命中后的 `Wound()` 调用已接通；刀角色当前配置的命中由用户报告正常。枪械角色、选角预览及多次进入的运行边界尚未完整验证。
- `PlayerObject` 的输入、动画、旋转、攻击事件与金币方法由用户编写；Codex 按用户要求完成 1～7 号角色 Prefab 的重复组件配置与引用检查。

## 当前实现与配置

- 1～7 号角色 Prefab 的根对象均添加并启用 `PlayerObject`，该对象已有 Animator，`Start()` 可从同一对象读取 Animator。
- 2～7 号枪械角色的 `gunPoint` 均引用对应武器模型下的空对象。4、7 号原先新建的空对象名称仍是 `GameObject`，已统一更名为 `gunPoint`；3 号枪口点原是武器模型的同级对象，已调整为该模型的子对象。枪口点的位置和朝向数值没有修改。
- 1 号角色使用 `weapon_knife`，没有枪口点，`PlayerObject.gunPoint` 保持空引用；刀的命中入口是 `KnifeEvent()`。
- `GameScene1` 的 2 号角色原先在场景实例上另加了一个 `PlayerObject`。现在由 Prefab 自带的组件提供玩家逻辑，场景专属的重复组件已移除。

| 角色 | 武器 | `gunPoint` |
| ---: | --- | --- |
| 1 | 刀 | 空引用，使用近战事件 |
| 2 | 火焰喷射器 | 已绑定 |
| 3 | 狙击枪 | 已绑定 |
| 4 | 手枪 | 已绑定 |
| 5 | 突击步枪 A | 已绑定 |
| 6 | 火箭筒 | 已绑定 |
| 7 | 突击步枪 B | 已绑定 |

## 金币初始化时机

用户要求把 `UpdateMoney()` 从 `PlayerObject.Start()` 移入 `InitPlayerInfo(int atk, int money)`，现已完成。这样选角面板为了预览而实例化角色时，不会因 `Start()` 访问尚未创建的 `GamePanel` 而直接报空引用。第 31P 的 `GameLevelMgr.InitInfo()` 已先调用 `UIManager.ShowPanel<GamePanel>()` 建立并登记面板，再实例化玩家并调用 `InitPlayerInfo()`；原先仅在场景中放置 GamePanel、无法被 UIManager 查询的问题已通过这条新流程修正。

## 验证与边界

- 七个 Prefab 均已静态核对：每个根对象恰有一个 `PlayerObject`，脚本 GUID、Animator 同级关系、枪口引用和枪口所在武器层级都正确；1 号的空引用是近战角色的有意配置。
- C# 独立编译为 0 Error；用户确认第 31P 的单局关卡流程已完成，刀攻击命中正常。Codex 尚未独立复现 Unity Play Mode；选角预览、射击射线和重复进入等边界尚未完整验证。
- `PlayerObject.Update()` 目前对选角预览角色也会执行，因此菜单中按键或点击可能触发预览角色的旋转及动画。后续需要区分预览角色与正式玩家的输入启用状态。
- `KnifeEvent()` 和 `ShootEvent()` 现已在命中 `MonsterObject` 后调用 `Wound(atk)`，此前“伤害结算仍是 TODO”的结论已失效。刀角色曾出现半径 1 时 `OverlapSphere` 返回 0、半径 3 时能检测到的空间范围问题；用户随后将角色缩放改为 0.5 并调整 CharacterController，报告当前命中正常。当前保存的刀攻击代码仍使用 `transform.position + transform.forward + transform.up`、半径 1；枪械角色的实际运行仍需单独验证。
- 1 号角色若错误触发 `ShootEvent()`，空的 `gunPoint` 会报空引用；当前按近战角色处理，后续应核对动画事件只调用 `KnifeEvent()`。

## 下一步

在 Unity 中验证一个枪械角色的射线是否由对应 `gunPoint` 发出并正确触发怪物受伤；选角预览与重复进入关卡的边界另行复盘。
