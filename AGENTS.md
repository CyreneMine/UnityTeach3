# UnityTeach3 项目协作规则

## 1. 项目定位

- 本项目用于系统学习 Unity 核心功能，并保存每一课的练习、验证和复盘记录。
- 当前课程主线共 96P，另有 38P 综合实践内容。
- 学习范围涵盖资源导入、2D 精灵、2D 物理、Tilemap、动画、骨骼、3D 模型、角色控制和导航寻路。
- 项目目标不是尽快做完一个成品，而是理解原理、保留练习、建立调试习惯并形成可复查的 GitHub 学习仓库。
- 教程中的实现是学习材料，不自动等同于唯一正确或适合真实项目的方案。

## 2. 当前技术环境

- Unity 编辑器版本：`6000.3.10f1`。
- 当前本地工作区使用 2D 模板重新创建；远程仓库早期版本曾使用 3D 模板。处理仓库差异时以当前本地 2D 项目的 Unity 资源和设置为准，不直接用远程旧版 `Assets/`、`Packages/` 或 `ProjectSettings/` 覆盖。
- 输入方案：Input System `1.18.0`。
- UI：UGUI `2.0.0`。
- 项目包含 2D、动画、物理、AI、Timeline 等内置模块。
- Unity 版本或关键包版本改变后，要更新本节并说明原因。
- 不要因为教程版本较旧就盲目照搬已弃用 API；先解释版本差异，再给兼容方案。

## 3. Codex 的职责

- 在回答项目前，读取真实脚本、场景、Prefab、Inspector 配置和学习记录。
- 解释 Unity、C#、组件、生命周期和相关 API 的原理。
- 检查练习是否满足本课目标，指出错误、误解和不良习惯。
- 对比“能运行”“满足需求”“可维护”“适合真实项目”这几个层次。
- 按需更新 `README.md`、`LearningProgress.md` 和 `Notes/`。
- 只有用户明确要求时，才管理 Git 提交、推送、标签或 Release。
- 每次复查结束时，给出一个最小、明确、可执行的下一步。

## 4. 修改边界

### 4.1 默认可修改

- `README.md`。
- `LearningProgress.md`。
- `Notes/` 下的学习笔记。
- 课程目录、复盘表格和说明性文档。
- 用户明确要求维护的截图索引或验证记录。

### 4.2 默认禁止修改

- `.cs` 脚本。
- `.unity` 场景。
- Prefab、材质、动画、Animator Controller、Tilemap 等 Unity 资源。
- `ProjectSettings/`。
- `Packages/manifest.json` 和其他包配置。
- 任何会影响运行结果的文件。

### 4.3 何时可以修改受保护内容

- 用户明确说“直接修改”。
- 用户明确说“帮我改”。
- 用户明确说“可以动代码”。
- 用户明确说“直接修复”。
- 用户明确指定了要编辑的场景、Prefab、设置或资源。

### 4.4 发现问题但未获修改授权时

- 指明文件或 Unity 对象位置。
- 说明问题的根本原因。
- 说明可能造成的结果。
- 给出推荐改法。
- 只提供最小参考片段，不直接写入项目。
- 如果需要在 Inspector 中操作，写清对象、组件、字段和值。

## 5. 每课开始前的检查

- 读取 `LearningProgress.md`，确认当前课次和状态。
- 核对课程文件名与记录中的编号和标题。
- 查看 `git status`，区分用户改动、Unity 自动改动和生成文件。
- 确认 Unity 是否仍处于 Play Mode。
- 场景相关课程先确认当前打开场景和保存状态。
- 资源相关课程先确认 `.meta` 文件和 GUID 是否稳定。
- 包或旧 API 相关课程先确认当前 Unity 和包版本。

## 6. 练习复查流程

1. 先复述本课目标，不直接猜代码意图。
2. 检查真实文件、场景对象和 Inspector 配置。
3. 区分代码问题、Unity 配置问题、运行验证问题和资源问题。
4. 检查是否满足题目需求和边界条件。
5. 解释原因，再提出修复建议。
6. 指出做得好的地方和需要调整的习惯。
7. 给出最小验证步骤。
8. 验证完成后才更新课程状态。

## 7. 通用代码检查

- 类名、文件名和职责是否一致。
- 字段、属性、方法和局部变量命名是否表达真实含义。
- `public` 是否只是为了 Inspector 暴露；需要时考虑 `[SerializeField] private`。
- 是否存在空引用、越界、重复注册、重复实例化或未释放对象。
- `Awake`、`OnEnable`、`Start`、`Update`、`FixedUpdate`、`OnDisable`、`OnDestroy` 的职责是否合理。
- 是否把每帧不需要执行的逻辑放进了 `Update`。
- 是否用帧率相关的写法处理时间、移动或插值。
- 是否把场景查找、字符串路径或魔法数字散落在代码中。
- 是否存在可以简化的重复逻辑。
- 初学阶段优先清晰和正确，不提前引入复杂架构。

## 8. 场景、Prefab 与 Inspector 检查

- 场景中的对象和组件是否启用。
- Inspector 引用是否完整，有无 Missing。
- 修改发生在 Prefab 资源、Prefab 实例还是普通场景对象上。
- Prefab Override 是否是预期改动。
- 运行时改动是否在退出 Play Mode 后丢失。
- 场景是否已经保存到磁盘。
- 对象层级、命名和职责是否清楚。
- 移动 Unity 资源时必须连同 `.meta` 文件处理。
- 资源移动后检查 GUID 引用，不能只看文件是否存在。

## 9. 资源导入与 Sprite 检查

- Texture Type、Sprite Mode、Pixels Per Unit、Filter Mode 和 Compression 是否符合用途。
- 平台覆盖设置是否真的应用到目标平台。
- Single、Multiple、Polygon 的编辑结果是否与素材匹配。
- Sprite Pivot、Border、Mesh Type 是否影响显示或布局。
- SpriteRenderer 的 Sprite、Color、Flip、Draw Mode 和排序是否正确。
- Sorting Layer、Order in Layer、Sorting Group 的组合是否符合遮挡关系。
- Sprite Mask 的 Interaction、范围和排序是否正确。
- Sprite Atlas 是否真正收录目标资源，并检查打包后的引用和尺寸。
- 不把“Scene 视图看起来正常”当作构建结果已经验证。

## 10. 2D 物理检查

- Rigidbody2D 的 Body Type、Gravity Scale、Constraints 和碰撞检测模式是否合适。
- Collider2D 的形状、大小、偏移、Trigger 状态是否符合对象轮廓。
- 物理材质的摩擦与弹性是否产生预期效果。
- 力和速度的修改是否放在合适的生命周期中。
- `Update` 与 `FixedUpdate` 的职责是否混淆。
- Collision 与 Trigger 回调是否和 Collider 配置一致。
- Layer Collision Matrix 是否允许预期对象相互作用。
- 区域、浮力、点、平台、表面效应器所需组件是否齐全。
- 使用运行时速度、接触点和回调日志验证，不能只看 Inspector 数值。

## 11. Tilemap 与 Sprite Shape 检查

- Grid、Tilemap、Tilemap Renderer、Tilemap Collider 之间的层级是否正确。
- Tile Palette 是否使用正确的 Grid 类型和 Cell Size。
- Tile 资源、调色板和场景实例是否混淆。
- Composite Collider 与 Rigidbody2D 的配置是否完整。
- Collider 合并后边缘和拐角是否出现异常。
- 扩展 Tile 与 Brush 的依赖是否兼容当前 Unity 版本。
- 代码设置 Tile 时检查坐标转换、边界和空 Tile。
- Sprite Shape Profile、Controller 和 Renderer 的引用是否正确。
- 编辑路径、填充材质、边缘精灵和碰撞轮廓分别验证。

## 12. Animation 与 Animator 检查

- Animation Clip 的目标对象路径和属性绑定是否有效。
- Legacy Animation 与 Animator 系统不要混用概念。
- Animator Controller 的状态、参数、过渡和默认状态是否正确。
- Transition 的 Has Exit Time、Duration、Conditions 是否符合需求。
- Bool、Trigger、Float、Int 参数是否选择合理。
- 动画事件的时间点、函数名和接收组件是否有效。
- 代码切换状态时检查状态名、Hash、层级和 CrossFade 行为。
- 状态机、子状态机和 StateMachineBehaviour 的职责是否清楚。
- Blend Tree 的阈值、参数范围和输入值是否对应。
- Layer Weight、Avatar Mask、IK、Root Motion 和目标匹配分别验证。
- Play Mode 中观察当前状态、参数和过渡，不能只看 Controller 图。

## 13. 2D 骨骼与换装检查

- Sprite Editor 中骨骼、网格、权重和几何是否匹配图片。
- 单图、图集和 PSB 工作流使用的导入设置是否正确。
- Sprite Skin、Root Bone 和骨骼引用是否完整。
- IK Manager、Solver、Target 和 Effector 的层级是否合理。
- 换装资源的分类、名称和骨骼绑定是否一致。
- 同文件和跨文件换装分别验证缺失部件和错误映射。
- Spine 运行库版本要与导出资源匹配。
- 第三方资源不默认提交到仓库，先确认许可和体积。

## 14. 3D 模型与动画检查

- Model 页签中的 Scale、Read/Write、Mesh Compression 和法线设置是否符合用途。
- Rig 类型、Avatar Definition 和骨骼映射是否正确。
- Animation 页签中的 Clip 范围、Loop、Root Transform 和事件是否正确。
- Materials 页签的提取和重映射是否产生重复材质。
- 预览窗口正常不等于场景中 Animator 配置正常。
- 3D 动画练习要检查 Avatar、Controller、参数和运行时状态。
- 动画分层、遮罩、混合和 IK 要在目标角色上实际运行验证。

## 15. 角色控制与导航检查

- CharacterController 的 Center、Radius、Height、Slope Limit 和 Step Offset 是否匹配模型。
- 重力、落地、移动和旋转是否与帧率无关。
- 输入方向与世界/本地/摄像机空间的转换是否正确。
- NavMesh 是否在正确几何和 Agent 设置下生成。
- Agent 的 Radius、Height、Speed、Stopping Distance 与场景比例是否一致。
- 目标点是否在 NavMesh 上，路径是否完整。
- OffMeshLink 的方向、代价和端点是否有效。
- NavMeshObstacle 的 Carve 行为和动态更新是否符合预期。
- 至少测试不可达目标、动态障碍、重新寻路和停止距离。

## 16. 综合实践项目检查

- 先确认需求和最小可玩闭环，再检查具体实现。
- UI 面板基类、UI 管理器和各面板职责要清楚。
- 场景切换、界面显示、设置数据和音效状态需要实际串联验证。
- 摄像机动画和跟随逻辑要区分一次性过场与持续跟随。
- 玩家、保护区、怪物、出怪点和关卡管理器之间的状态流要可追踪。
- 怪物状态机要测试进入、退出、目标丢失、死亡和重置。
- 防御塔数据、塔逻辑和建造点不要互相承担全部职责。
- 游戏结束后重开、返回菜单或重新加载场景要验证状态清理。
- 数据文件要检查真实内容、编码、缺失和损坏情况。
- 实践总结要记录已完成、未完成、已知边界和可复用部分。

## 17. 运行验证规则

- 只通过编译不等于功能完成。
- 只看到 Console 日志不等于场景行为正确。
- 只在 Scene 视图正常不等于 Game 视图正常。
- 只测试一次不等于重复进入、退出或重载正常。
- 涉及分辨率时至少检查两种宽高比。
- 涉及物理时检查不同帧率或固定时间步下的表现。
- 涉及动画时观察参数、状态、过渡和实际画面。
- 涉及导航时测试可达、不可达和动态变化。
- 无法由 Codex 直接完成的 Unity 操作，要给出用户可执行的验证清单。
- 未完成验证时只能记录为“学习中”或“待复盘”，不能记录为“已完成”。

## 18. 学习记录规则

- `README.md` 保存项目定位、学习范围、使用方法和阶段目标。
- `LearningProgress.md` 保存课程表、状态和下一课。
- `Notes/` 保存误区、调试过程、版本差异和可复用结论。
- `Screenshots/` 可保存确有复盘价值的 Inspector 或运行结果。
- 每完成一课必须更新学习记录。
- 学习历史尽量追加，不随意覆盖已经发生的过程。
- 旧结论被修正后，要同时标记原因，避免保留误导信息。
- 每课至少记录：学了什么、哪里出错、为什么、如何验证、边界和下一步。
- 练习题必须记录独立完成程度，不能因看过答案而直接算完成。
- 课程编号或标题变化时，同时更新课程表和相关笔记链接。

## 19. 课程状态定义

- `未开始`：尚未观看或实践。
- `学习中`：已开始，但练习或验证未完成。
- `待复盘`：已做过，但仍有关键疑问或未验证边界。
- `已完成`：知识点、练习和必要运行验证均完成。
- `已跳过`：有明确原因暂不学习，并在备注中说明。
- 不允许仅因视频播放结束就标为 `已完成`。

## 20. Git 与 GitHub 规则

- 不自动提交，不自动推送。
- 用户要求提交或推送时，先总结本次变更范围。
- 默认使用中文提交信息，简洁说明本次学习内容。
- 提交前排除 `Library/`、`Temp/`、`Logs/`、`UserSettings/` 等生成内容。
- 不混入与当前课程无关的文件。
- 推送后检查本地分支与远程分支是否同步。
- 未经明确允许不改写历史、不强制推送。
- 确需强推且获得授权时优先使用 `--force-with-lease`。
- 课程结束后可按需创建语义化标签和中文 GitHub Release。
- Release 前验证说明正文编码和附件内容，不只确认命令成功。
- 教程视频、付费资源和无明确分发许可的第三方素材不得提交。

### 20.1 GitHub CLI 工作流

- 本机已安装 GitHub CLI，并登录 GitHub 账号 `CyreneMine`；首次执行 GitHub API 操作前用 `gh auth status` 核对当前账号。
- 普通本地版本管理继续使用 Git：检查状态与差异、选择性暂存、提交、`git push origin master`。当前仓库已能直接推送，不再默认设置临时 `GIT_SSH_COMMAND`。
- GitHub CLI 用于读取仓库信息，以及按用户要求管理 Pull Request、Issue、Actions、标签和 Release；普通推送不改用额外的同步命令。
- 提交前依次检查 `git status --short`、相关差异、暂存区差异和资源排除结果；不要用 `git add .` 混入未审查文件。
- 推送后比较 `HEAD` 与 `origin/master` 的提交号，并检查 `git status -sb`，确认分支同步且没有误漏文件。
- 不运行或展示 `gh auth token`，不把令牌、私钥路径或认证信息写入仓库、文档、命令输出或提交记录。
- `gh` 因安装后进程未刷新而暂时不在 PATH 时，优先重启 Codex 或定位已安装的可执行文件；不自动修改系统 PATH、全局 Git 配置或认证协议。
- 当前认证和推送已经可用，不主动执行 `gh auth setup-git`，也不为使用 GitHub CLI 改写项目远程地址。

## 21. 建议提交粒度

- 一节知识点和对应练习可作为一次提交。
- 大型实践可按界面、数据、玩法系统和收尾拆分。
- 文档更新可与对应课程代码放在同一次提交中。
- 纯记录修正可以单独提交。
- 提交信息示例：`完成第 22 课刚体学习与验证`。
- 提交信息示例：`修正 Tilemap 碰撞复盘记录`。

## 22. 回答风格

- 默认使用中文。
- 先给结论，再解释原因。
- 结合项目中的真实文件、场景和组件，不给脱离上下文的泛泛答案。
- 对初学者友好，但不掩盖错误和风险。
- 明确指出好习惯与坏习惯。
- 解释必要的 Unity 版本差异。
- 避免为了“架构感”提前增加复杂抽象。
- 复查结尾给出一个最小下一步。

## 23. 最终原则

- 这是学习项目，理解与验证优先于速度。
- 未经授权不代写完整功能，不修改运行代码或 Unity 资产。
- 未经请求不提交、不推送、不发布。
- 未完成验证不得把课程标为完成。
- 最终成果应包括可运行练习、清晰复盘、稳定 Git 历史和可验证的作品记录。
