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
- 2026-10-24 综合实践已形成 Windows 可运行 Demo《丧尸围城》。用户完成打包并反馈运行正常；本机包为 `Builds/UnityTeachDemo3.exe`，Builds 继续忽略，不把构建产物或教学素材上传。
- Agent 使用独立包实际操作主菜单、设置、角色切换、选关、进入战斗、失败结算、确认返回及再次进入选角；用户补充建造、升级、角色开火和成功结算截图。共保存 15 张原始画面，索引为 `Screenshots/Practice-Demo/README.md`，区分截图来源并保留原水印。
- 同一进程失败结算奖励 100 后，选角金币从 6325 增为 6425。此结果不等于关闭程序后重读存档通过；音频、多宽高比、全配置组合和特殊边界未专项覆盖。
- 选关编号同步、Player 层（7）和 TowerPoint 两个 NameToLayer 判断最终静态通过；结算后仍可能接受玩法按键是用户明确接受的练习边界，不继续要求修复。Alt/鼠标建造、关于内容、对象池和性能测试不计入当前成果。
- 第 32P、35P、36P 的基础功能随打包运行与用户补图补充完成依据，第 37P 基础打包运行、第 38P 总结已完成；早期专项待复盘项没有全部关闭，以 LearningProgress 为准。
- 展示资料入口：`Notes/Practice-Demo-Overview.md`（带图功能说明）、`Notes/Practice-Demo-Resume.md`（简历与口述）、`Notes/Practice-Demo-Modules.md`（八模块总结）。须保留本人逻辑、Agent 重复配置和第三方美术的分工说明；不添加无证据的性能/商业成果。

## 历史进度与检查快照

以下保留当时检查过程，其中“学习中”“待打包”和初次错误写法均属于历史时点；当前交付结果以上节及项目总览为准，不用历史配置覆盖现有截图。

- 38P 综合实践的第 21P～23P 已完成，第 24P“摄像机跟随逻辑”学习中。`CameraMove` 已挂到 `GameScene1` 摄像机，以角色位置和朝向计算后上方目标点；已修正 `target` 空值判断写反及以摄像机自身位置计算跟随点的问题，编译 0 Error，仍需 Play Mode 验证移动与转向跟随。GamePanel 的场景启动创建按用户决定暂缓。
- 第 25P“玩家逻辑”仍记为学习中：用户编写 `PlayerObject`，Codex 为 `Resources/Role/1～7.prefab` 根对象配置该组件；2～7 号的 `gunPoint` 指向各自武器模型子对象，1 号刀角色保持空引用。`UpdateMoney()` 已从 `Start()` 移到 `InitPlayerInfo()`；第 31P 关卡流程现会先经 UIManager 创建 GamePanel，再初始化玩家。刀角色缩放与 CharacterController 经用户调整后，用户报告半径 1 的刀攻击命中正常；枪械与选角预览仍待验证。详见 `Notes/Practice-Player-Logic.md`。
- 第 26～30P 初次静态复查时发现的保护区初始化、GamePanel 登记和玩家攻击伤害 TODO，后来随第 31P 的关卡初始化及战斗联调得到修正；初次检查过程与 `Invoke` 非阻塞调度复盘保留在 `Notes/Practice-Monster-Logic-Review.md`。第 26～30P 的其余运行边界仍待单独复盘。Excel/JSON/C# 的 `atkOffset` 字段已同步；JSON 的 `moveSpeed` 全部为 5，与 Excel 分类数值不同，需确认是否有意。`Resources/Monster/` 的本地模型 Prefab 及 `.meta` 不入库。
- 用户确认综合实践第 31P“关卡管理器逻辑”已完成。`ChooseScenePanel` 场景加载回调调用 `GameLevelMgr.InitInfo()`，后者创建/登记 GamePanel、实例化玩家、设置摄像机目标和保护区血量；出怪点登记、波次统计、怪物存活数及死亡后的胜利检查已连通。Codex 静态检查与 C# 编译 0 Error；Unity 单局运行以用户反馈为准。之后发生的波数偶发 2/4 是“开始”按钮连点造成场景重复加载，用户在回调开头禁用按钮并复测确认修复；游戏界面和结算界面返回时也会调用 `GameLevelMgr.ClearInfo()`。当前 `nowSelSceneId` 未随场景选择同步，其他重复进入边界仍待验证。详见 `Notes/Practice-GameLevelMgr.md`。
- 第 34P 数据准备已完成：用户提供 Excel，Codex AI Agent 生成数据类与 JSON（9 条、11 列），之后按用户要求同步小写 `eff/` 与 `TowerImg/1_1`～`TowerImg/3_3` 路径。用户已接入 `GameDataMgr`；2026-10-24 复查确认全部模型/图片/特效路径与升级 ID 有效，图片已由用户改为 Single。分工见 `Notes/Practice-Tower-Data.md`。
- 第 35P 塔逻辑待复盘：用户反馈制作完成，现已先遍历选目标再生成瞄准点，单体伤害使用 `_target`，群体攻击入口与循环外冷却已接通；塔位置交给造塔点，玩家临时 `L` 键建塔入口已移除。0 Error 编译通过，死亡目标转火与炮头俯仰等 Play Mode 边界尚未验证。Agent 此前为本地 9 个 Prefab 配置 `GunPoint` 和 `gunPoint/head`：1、2 类枪口随 `Barrel`，`head=Turret`；3 类在 `Crystal` 中心附近，`head=Crystal`。本地 `Resources/Tower/`、`TowerImg/`、`eff/` 连同 `.meta` 继续排除于 Git。详见 `Notes/Practice-Tower-Logic-Comparison.md`。
- 第 36P 造塔点逻辑待独立包验收：用户完成数字键建塔、扣费、空格升级及满级/离开关闭选择区，升级按钮显示已修正；三关各有 1 个点。Player 层（7）及七个角色已配置，进入/离开回调最终均使用 NameToLayer 比较层编号，静态复查通过。结算输入用户明确接受保留；鼠标点击建造与 Alt 指针操作未实现。详见 `Notes/Practice-Tower-Building.md`。
- 2026-10-24 用户反馈练习项目基本完成、准备打包。第 37P 进入学习中，第 32P/33P/35P/36P 待复盘。主工程及 Editor 编译 0 Error/38 Warning，四个构建场景、35 条数据资源路径及 55 个主要序列化文件外部 GUID 静态通过；没有执行 Unity Player Build/Play Mode。三项功能缺口：GamePanel 结算后仍处理建塔输入；TowerPoint 触发回调未筛选玩家；ChooseScenePanel 选关后未同步 GameDataMgr.nowSelSceneId。BasePanel 已用 unscaledDeltaTime，GameOverPanel 暂停时间，BeginPanel 恢复。三关当前各 5 波、每波 5 只。详见 `Notes/Practice-PreBuild-Review.md`；包内验收后再做 Demo 和各模块总结，不把静态检查写成实际运行通过。
- 打包检查后续：用户明确第 1 项结算输入不修，作为练习接受的边界，不再要求修复；第 3 项已在场景加载前同步 nowSelSceneId，静态通过。第 2 项添加了 `other.gameObject.layer != LayerMask.GetMask("Player")`，但层编号与掩码混用且 TagManager 无 Player 层，仍不正确；建议直接比较 GetComponentInParent<PlayerObject>() 与 GameLevelMgr.player，或完整配置层并用 NameToLayer。复查主工程 0 Error/36 Warning，未代改脚本。

- 最终静态复查：用户已保存两处 `other.gameObject.layer != LayerMask.NameToLayer("Player")`，与 Player 层（7）和七个角色 Prefab 对应。选关编号同步也正确；主工程编译 0 Error/36 Warning。初次提出的三项中，第 1 项用户接受不修，第 2、3 项静态修复通过；之后的独立包运行结果见“当前状态”。

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
