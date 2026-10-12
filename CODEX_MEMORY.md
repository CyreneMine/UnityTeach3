# UnityTeach3 工作区记忆

## 目录映射

- 仓库根目录：`E:\UnityLearn\UnityTeach3`。
- 理论工程：`E:\UnityLearn\UnityTeach3\UnityTeach3-Theory`。
- 综合实践工程：`E:\UnityLearn\UnityTeach3\UnityTeach3-Project`。
- `UnityTeach3-Project` 的原始创建位置是 `E:\UnityLearn\UnityTeachDemo3`，整理后以仓库内路径为准。

## 工程职责

- `UnityTeach3-Theory` 保存已经结束的 96P 理论练习，默认只读，仅用于参考和补充验证。
- `UnityTeach3-Project` 是后续 38P 3D 项目实践的主工程。
- 新增代码、场景、Prefab、材质和项目配置默认写入 `UnityTeach3-Project`，不能误写到理论工程。
- `README.md`、`LearningProgress.md`、`AGENTS.md`、`CODEX_MEMORY.md` 和 `Notes/` 保留在仓库根目录，统一管理两个工程。

## 当前状态

- 两个工程均使用 Unity `6000.3.10f1`。
- 96P 主课程理论阶段已经结束：73 课已完成，22 课按 3D 学习路线主动暂缓，第 88 课保留运行边界复盘。
- 38P 综合实践的第 21P～23P 已完成，第 24P“摄像机跟随逻辑”学习中。`CameraMove` 已挂到 `GameScene1` 摄像机，以角色位置和朝向计算后上方目标点；已修正 `target` 空值判断写反及以摄像机自身位置计算跟随点的问题，编译 0 Error，仍需 Play Mode 验证移动与转向跟随。GamePanel 的场景启动创建按用户决定暂缓。
- 第 25P“玩家逻辑”仍记为学习中：用户编写 `PlayerObject`，Codex 为 `Resources/Role/1～7.prefab` 根对象配置该组件；2～7 号的 `gunPoint` 指向各自武器模型子对象，1 号刀角色保持空引用。`UpdateMoney()` 已从 `Start()` 移到 `InitPlayerInfo()`；第 31P 关卡流程现会先经 UIManager 创建 GamePanel，再初始化玩家。刀角色缩放与 CharacterController 经用户调整后，用户报告半径 1 的刀攻击命中正常；枪械与选角预览仍待验证。详见 `Notes/Practice-Player-Logic.md`。
- 第 26～30P 初次静态复查时发现的保护区初始化、GamePanel 登记和玩家攻击伤害 TODO，后来随第 31P 的关卡初始化及战斗联调得到修正；初次检查过程与 `Invoke` 非阻塞调度复盘保留在 `Notes/Practice-Monster-Logic-Review.md`。第 26～30P 的其余运行边界仍待单独复盘。Excel/JSON/C# 的 `atkOffset` 字段已同步；JSON 的 `moveSpeed` 全部为 5，与 Excel 分类数值不同，需确认是否有意。`Resources/Monster/` 的本地模型 Prefab 及 `.meta` 不入库。
- 用户确认综合实践第 31P“关卡管理器逻辑”已完成。`ChooseScenePanel` 场景加载回调调用 `GameLevelMgr.InitInfo()`，后者创建/登记 GamePanel、实例化玩家、设置摄像机目标和保护区血量；出怪点登记、波次统计、怪物存活数及死亡后的胜利检查已连通。Codex 静态检查与 C# 编译 0 Error；Unity 单局运行以用户反馈为准。当前 `nowSelSceneId` 未随场景选择同步，静态 `GameLevelMgr` 在重复进入关卡时未重置，属于后续边界。详见 `Notes/Practice-GameLevelMgr.md`。

## 协作约定

- 回答综合实践问题前，优先检查 `UnityTeach3-Project` 的真实文件、场景、Prefab、Inspector 配置和运行日志。
- `UnityTeach3-Theory` 默认只读，除非用户明确要求修改。
- 未经明确授权，不修改 C#、场景、Prefab、资源、`Packages` 或 `ProjectSettings`。
- 每课完成后更新 `LearningProgress.md`，关键误区和复用结论写入 `Notes/`。
- 只有用户明确要求时才提交或推送；推送前排除生成目录和无分发许可的第三方资源。
- `UnityTeach3-Project/Assets/ArtRes/` 是本地教学与第三方资源目录，必须始终排除在 Git 之外；对应的 `ArtRes.meta` 也不提交。
- `UnityTeach3-Project/Assets/_TerrainAutoUpgrade/` 是资源导入产生的本地升级结果，不提交。
- `UnityTeach3-Project/Assets/TextMesh Pro/` 和教程附带的 `UnityTeach3-Project/Assets/Scripts/Json/LitJson/` 只保留在本地，不提交。
- `UnityTeach3-Project/Assets/Resources/Music/` 中的教学音频素材及其 `.meta` 只保留在本地，不提交。
- `UnityTeach3-Project/Assets/Resources/SceneImg/` 中的教学场景预览图及其 `.meta` 只保留在本地，不提交。
- 综合实践结束时，为整个小 Demo 编写总览总结，并分别总结 UI、数据、场景、摄像机、玩家、怪物、关卡和防御塔等模块。
