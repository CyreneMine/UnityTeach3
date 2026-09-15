# UnityTeach3

这是一个用于系统学习 Unity 核心功能的长期练习项目。仓库重点保存每节课的实际操作、问题复盘、运行验证和阶段性成果，而不是追求快速完成一个成品游戏。

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

## 项目环境

- Unity：`6000.3.10f1`
- 项目模板：2D（本地重新创建，后续以当前项目配置为准）
- Input System：`1.18.0`
- UGUI：`2.0.0`

## 当前阶段

- Unity 核心主课程 96P 已按当前学习路线完成：73 课完成，22 课因 2D 专项或旧版内容主动暂缓，第 88 课保留运行边界复盘。
- 已完成的重点实践包括资源加载、2D 物理、Animator 状态机与混合树、动画分层与 IK、CharacterController、NavMesh 寻路和动态障碍。
- 下一阶段进入 38P 综合实践，从需求分析开始串联 UI、数据、场景、角色、怪物、关卡和防御塔系统。
- 主课程阶段总结见 [Notes/CoreCourse-Summary.md](Notes/CoreCourse-Summary.md)。

## 学习方式

1. 在 [LearningProgress.md](LearningProgress.md) 中确认当前课次。
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

- 不提交 `Library/`、`Temp/`、`Logs/`、`UserSettings/` 等本地生成目录。
- 不提交教程视频、付费资源或没有明确分发许可的第三方素材。
- 默认使用中文提交信息。
- Git 提交、推送、标签和 Release 都需要明确请求后执行。
