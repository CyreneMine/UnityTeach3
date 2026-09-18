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
- 38P 综合实践的第 14P“选角面板：数据准备”已完成；第 15P 已完成角色展示、循环切换、解锁存档、按钮状态、返回镜头动画和隐藏清理，金币不足提示与场景选择跳转等待后续面板完成后接入，下一步进入第 16P。

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
- 综合实践结束时，为整个小 Demo 编写总览总结，并分别总结 UI、数据、场景、摄像机、玩家、怪物、关卡和防御塔等模块。
