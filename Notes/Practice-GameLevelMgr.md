# 综合实践第 31P：关卡管理器逻辑

## 当前结论

用户确认第 31P 已完整完成。本次核对 `GameLevelMgr`、`ChooseScenePanel`、`MonsterPoint`、`MonsterObject`、`MainTowerObject` 和 `PlayerObject` 的实际调用链，`Assembly-CSharp` 静态编译为 0 Error。Unity 单局运行结果依据用户反馈；Codex 本次没有独立执行 Play Mode，也未修改代码或场景。

## 已接通的流程

1. 场景选择面板加载场景完成后调用 `GameLevelMgr.InitInfo()`。该方法读取当前关卡与角色数据，使用 `UIManager.ShowPanel<GamePanel>()` 创建并登记游戏面板，从 `HeroBornPos` 实例化玩家，初始化攻击力和金币，设置摄像机跟随目标，并调用 `MainTowerObject.UpdateHp()` 初始化保护区血量与 UI。
2. `MonsterPoint.Start()` 向关卡管理器登记出怪点并累加总波数；每波开始时更新剩余波数，每只怪物生成时增加存活数。
   当前 `GameScene1` 的出怪点为 2 波、每波 2 只，用于本阶段联调；旧复查文档中的 3 波、每波 5 只是第 30P 当时的配置。
3. 怪物死亡动画事件 `DeadEvent()` 减少存活数，再调用 `CheckOver()`。所有出怪点都已生成完毕且存活数为零时，当前阶段打印“游戏胜利”。游戏结束面板属于下一课。
4. 玩家刀攻击已在 `KnifeEvent()` 中查询 `Monster` 层并调用怪物 `Wound()`。此前半径 1 检测不到、临时放大到 3 能检测到，定位为空间范围不相交；用户将 1 号角色缩放调整为 0.5，并调整 CharacterController 后报告命中正常。当前磁盘代码的检测半径为 1，仍包含 `+ transform.up`，不应把“去掉 up”记录为最终修复。怪物本地 Prefab 已配置 `Monster` 层及根节点 CapsuleCollider。

## 边界与下一步

- `ChooseScenePanel` 使用 `nowSceneId` 加载场景，但没有同步 `GameDataMgr.nowSelSceneId`；`GameLevelMgr.InitInfo()` 读取后者。当前只有 `GameScene1` 加入 Build Settings，其他场景还不能按所选配置完成整局验证。
- `GameLevelMgr` 是持续存在的静态实例，`points`、波次与存活数没有在重新进入关卡时清零。单局通关逻辑与重新开始一局是两个验证场景，后续加入游戏结束界面和重开流程时需检查。
- 本次编译通过不能代替所有怪物类型、攻击方式和重复进入场景的运行验证；第 24～30P 的其他待复盘事项继续保留原状态。
- 下一课是第 32P“游戏结束界面”。
