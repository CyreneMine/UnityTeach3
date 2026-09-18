# UnityTeach3

> 本仓库分为两个独立 Unity 工程：`UnityTeach3-Theory` 保存已结束的 96P 理论练习并默认只读；`UnityTeach3-Project` 是当前 38P 3D 综合实践主工程。

## 快速入口

| 入口 | 用途 | 当前状态 |
| --- | --- | --- |
| [UnityTeach3-Project](UnityTeach3-Project/) | 38P 3D 综合实践主工程 | 当前使用 |
| [UnityTeach3-Theory](UnityTeach3-Theory/) | 96P 理论练习工程 | 已结束，默认只读 |
| [学习进度](LearningProgress.md) | 主课程与综合实践课次记录 | 第 14P 已完成，第 15P 学习中 |
| [主课程总结](Notes/CoreCourse-Summary.md) | 96P 学习成果、取舍和待补边界 | 已完成 |
| [协作规则](AGENTS.md) | 双工程检查、修改和 Git 规则 | 当前有效 |
| [工作区记忆](CODEX_MEMORY.md) | 路径映射与跨会话稳定约定 | 当前有效 |

这是一个用于系统学习 Unity 核心功能的长期练习仓库。仓库同时保存已经结束的 96P 理论练习工程和接下来使用的 38P 3D 综合实践工程。

## 工作区结构

```text
UnityTeach3/
├─ UnityTeach3-Theory/   # 96P 理论练习，默认只读
└─ UnityTeach3-Project/  # 38P 3D 综合实践，当前主工程
```

仓库根目录保存 `AGENTS.md`、`CODEX_MEMORY.md`、`LearningProgress.md`、`Notes/` 等跨工程管理资料。

## 学习范围

- 图片与模型资源导入
- Sprite、Sprite Atlas、Sprite Shape 与 2D 渲染
- Rigidbody2D、Collider2D、物理材质与 2D 效应器
- Tilemap、扩展瓦片和自定义画笔
- Animation、Animator、状态机、混合树、IK 与目标匹配
- 2D 骨骼、换装与 Spine 基础
- 3D 模型、Rig、动画剪辑、分层和遮罩
- CharacterController 与导航寻路
- UI、数据、角色、怪物、防御塔等综合实践

## 工程环境

- 两个工程的 Unity 版本均为 `6000.3.10f1`。
- `UnityTeach3-Theory`：之前重新创建的 2D 模板工程，保留 96P 理论练习，仅供复查。
- `UnityTeach3-Project`：由 `E:\UnityLearn\UnityTeachDemo3` 整理而来的 3D 模板工程，是 38P 综合实践的主工程。
- 两个工程当前均使用 Input System `1.18.0` 和 UGUI `2.0.0`。

## 当前阶段

- Unity 核心主课程 96P 已按当前学习路线完成：73 课完成，22 课因 2D 专项或旧版内容主动暂缓，第 88 课保留运行边界复盘。
- 已完成的重点实践包括资源加载、2D 物理、Animator 状态机与混合树、动画分层与 IK、CharacterController、NavMesh 寻路和动态障碍。
- 当前进入 38P 综合实践：第 14P“选角面板：数据准备”已完成，第 15P 已完成角色展示、切换、解锁、返回和数据保存主体逻辑；金币不足提示与开始游戏后的场景选择跳转等待后续面板完成后接入。
- 主课程阶段总结见 [Notes/CoreCourse-Summary.md](Notes/CoreCourse-Summary.md)。
- 综合实践 UI 起步阶段复盘见 [Notes/Practice-UI-Foundation-BeginPanel.md](Notes/Practice-UI-Foundation-BeginPanel.md)。
- 角色 Animator Override 配置见 [Notes/Practice-Character-Animator-Overrides.md](Notes/Practice-Character-Animator-Overrides.md)。
- 选角面板与角色数据复盘见 [Notes/Practice-ChoosePanel.md](Notes/Practice-ChoosePanel.md)。

## 学习方式

1. 在 [LearningProgress.md](LearningProgress.md) 中确认当前课次和目标工程。
2. 完成知识点学习和对应练习。
3. 在 Unity 中检查场景、Inspector 和运行结果。
4. 记录错误原因、验证过程、适用边界和下一步。
5. 只有在练习与必要验证完成后，才把课程标记为“已完成”。

## 文档结构

- [AGENTS.md](AGENTS.md)：Codex 在本项目中的协作、检查与 Git 规则。
- [LearningProgress.md](LearningProgress.md)：主课程和综合实践课程表。
- [Notes/CoreCourse-Summary.md](Notes/CoreCourse-Summary.md)：96P 主课程完成情况、能力沉淀和待补边界。
- [Notes/](Notes/)：后续按主题保存调试记录、版本差异和复盘笔记。

## 阶段目标

- 理解各核心模块的用途、生命周期、常用 API 和限制。
- 能独立完成练习，并区分代码、Inspector、场景和运行时问题。
- 能发现教程实现放进真实项目后的边界与风险。
- 完成一套可复用的核心功能练习和综合实践项目。
- 在课程结束时形成清晰的项目总结、已知边界和可验证成果。

## 仓库约定

- 两个 Unity 工程都不提交 `Library/`、`Temp/`、`Logs/`、`obj/`、`UserSettings/` 等本地生成目录。
- `UnityTeach3-Project/Assets/ArtRes/` 仅保存本地导入的教学与第三方资源，整个目录及其 `.meta` 不进入 Git。
- Unity 导入的 `TextMesh Pro/` 资源以及教程附带的 `LitJson/` 源码库只保留在本地，不进入 Git。
- `UnityTeach3-Project/Assets/Resources/Music/` 中的教学音频只保留在本地，不进入 Git；音乐控制脚本和设置数据仍纳入版本管理。
- 不提交教程视频、付费资源或没有明确分发许可的第三方素材；仓库只保留自己的脚本、场景、Prefab、配置和学习文档。
- 综合实践完成后，补充一份小 Demo 总结，并按 UI、数据、场景、摄像机、玩家、怪物、关卡、防御塔等模块分别整理实现、验证结果、问题和可复用结论。
- 默认使用中文提交信息。
- Git 提交、推送、标签和 Release 都需要明确请求后执行。
