# 综合实践第 27P：怪物动画状态机准备

## 当前状态

- 学习中。`Aggresive.controller` 原版由用户制作；Codex 根据它创建其余可对应的动画分类覆盖控制器。尚未在 Unity Play Mode 中验证怪物的实际播放与切换。
- 资源在当前工作区的实际路径为 `UnityTeach3-Project/Assets/ArtRes/Toon_Zombies_extended/animation/`。该第三方资源及其 `.meta` 不进入 Git；控制器存放在 `UnityTeach3-Project/Assets/Resources/Animator/Monster/`。

## 分类与动作选择

| 分类 | 控制器 | 待机 / 移动 | 受伤 / 攻击 | 出场 / 死亡 |
| --- | --- | --- | --- | --- |
| Aggresive | 用户制作的原控制器 | 原有 aggressive idle / 普通 run | 原有 aggressive damage A / attack A、B | Generic 的 emerge / death A |
| Broken | `Broken.overrideController` | broken idle / 普通 run | broken damage A / attack A、B | 沿用 Generic 的 emerge / death A |
| Calm | `Calm.overrideController` | calm-A idle A / 普通 walk，映射到原控制器的 `Run` 状态 | Calm 文件夹没有这两类动作，暂沿用原控制器的 aggressive 动画 | 沿用 Generic 的 emerge / death A |
| Crawl | `Crawl.overrideController` | crawl idle / 普通 move，映射到 `Run` 状态 | crawl damage / attack A、B | 沿用 Generic 的 emerge / 使用 crawl death |

`Generic` 是几类动画共用的素材集合，不单独创建覆盖控制器。用户允许在同一动作存在多个版本时任选一版；移动动作选普通版，不选 `_rm` 根位移版。`Calm` 目前只采用 A 组作为该分类的代表，B/C/Hulk/Tank 的差异化控制器尚未制作。

## AI Agent 协作过程（2026-09-23）

1. 我先在 Unity 中手动制作 `Aggresive.controller`，确定状态、参数、过渡和动画层，作为其他类别共用的原控制器。
2. 为跳过重复拖拽和逐项替换的步骤，我让 Codex（AI Agent）读取本地动画文件夹、原控制器和 `.meta`，整理可替换的动作槽位。实际资源目录与我最初给出的路径不同，Agent 先核对磁盘结构再操作。
3. 我明确了选择规则：同类动画有多个版本时可任选一版；移动使用不带 `_rm` 根位移的普通版本；`Generic` 是共用动画集合，无需独立控制器。Agent 按这些规则生成 `Broken`、`Calm`、`Crawl` 三个 Animator Override Controller 及对应 `.meta`，没有改动我手工制作的原控制器或第三方动画资源。
4. Agent 静态检查了三个控制器与 `Aggresive` 的关联、每个控制器的 7 个原动画槽位、替换动画的 GUID 和资源 `.meta`，并确认第三方 `ArtRes` 仍被 Git 排除。Unity 编辑器中的实际播放与状态切换由我继续验证，不能把静态检查写成运行验收。

这次协作的可核对产物是原控制器、三个覆盖控制器及其 `.meta`、本记录和后续 Unity 验证结果。求职时可据此说明：我负责状态机结构与动作选择规则，用 AI Agent 完成重复配置和引用检查，并亲自负责最终播放验证；不把 Agent 生成的内容冒称为全部手工制作。

## 验证与下一步

- 静态核对了三个覆盖控制器的基控制器 GUID、每组 7 个原动画槽位、所有替换动画 GUID 和 `.meta`。没有改动原控制器、第三方动画和怪物 Prefab。
- 在 Unity 中依次打开三个覆盖控制器，确认没有 Missing Clip；随后在怪物模型上测试待机、移动、受伤、攻击和死亡。尤其检查 Calm 攻击/受伤沿用 aggressive 动作是否符合预期，以及 Crawl 使用原版出场动画是否适配爬行姿态。
