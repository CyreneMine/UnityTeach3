# 综合实践游戏场景搭建与 UI 复盘

## 当前进度

- 第 21P“游戏场景：场景搭建”已经完成。
- 第 22P“游戏界面：拼界面”已经完成。
- 第 23P“游戏界面：界面逻辑”已经完成。
- 已建立 `GameScene1`、导航表面及对应烘焙数据，并拼出游戏阶段使用的基础界面。
- 下一步进入第 24P“摄像机跟随逻辑”。

## 第 21P：游戏场景

- `Assets/Scenes/GameScene1.unity` 已建立并保存，名称与第一条 `SceneInfo.sceneName` 配置一致。
- 场景中存在 `NavMeshSurface`，工程已经加入 AI Navigation `2.0.14`，并生成场景对应的 NavMesh 数据。
- NavMesh Agent 半径已从默认值调整为 `0.2`，后续需要结合玩家、怪物体积和道路宽度实际验证。
- 场景主体引用本地教学美术资源；仓库仍只保存场景结构和配置，不提交 `ArtRes` 原始资源。

## 第 22P：游戏界面

- `GamePanel` 已包含血量、金钱、波次和底部防御塔选项等基础内容。
- 血条使用 `Slider`，范围为 0～100，当前值为 100，并移除了拖动手柄。
- 底部 `Bottom` 容器使用 `HorizontalLayoutGroup` 排列三个防御塔选项。
- 每个 `option` 内部使用 `VerticalLayoutGroup` 排列塔图标、名称或价格等子内容。
- 每个防御塔选项采用“文字＋按钮＋文字”的复合结构，为后续鼠标点击和按钮状态反馈提供统一入口。

## 与教程不同的设计

### 血条使用 Slider

教程使用两张纯色图片，通过修改前景图片宽度表现血量。当前实现改用 `Slider`：

- Slider 本身就表达最小值、最大值和当前值，后续血量逻辑只需修改 `value`。
- Fill Rect 会根据比例自动更新宽度，不需要手动计算 RectTransform 尺寸。
- `SliderFillMinToHide` 负责在值为 0 时隐藏 Fill，并同步显示 `当前血量/100` 文本。

血条已经关闭 Interactable，并将 Navigation 设置为 None；它现在只负责显示血量，不会响应鼠标或键盘选择输入。

### 防御塔选项使用自动布局

教程通过手动坐标排列底部防御塔选项。当前实现使用父级水平布局和子级垂直布局：

- 新增、删除或调整防御塔选项时，不需要重新手动计算每个元素的位置。
- 相同类型元素的间距与对齐由布局组件统一控制，减少重复调整。
- 当前布局关闭了 Child Control Size，但开启了 Child Force Expand，主要负责自动排列现有固定尺寸元素。若后续需要布局组件统一控制宽高，应结合 `LayoutElement` 或开启对应的 Child Control Size，并在多种宽高比下重新验证。

### 防御塔图片改为按钮

教程使用三个“文字＋图片＋文字”复合对象表示可放置的防御塔。当前实现把中间图片改成 `Button`：

- 防御塔图标仍可通过 Button 的目标 Image 显示，同时获得点击事件和 Selectable 状态。
- 后续可以统一表现普通、悬停、按下、选中和不可购买状态，不需要额外为普通 Image 编写射线点击入口。
- 按钮点击和数字键选择应共同调用同一个选择方法，例如按索引选择塔，避免维护两套逻辑。

计划中的交互方式尚未实现：正常游戏时隐藏并锁定指针；按住 Alt 时显示并释放指针，用于点击防御塔按钮；松开 Alt 后恢复游戏输入。同时提供数字键 1、2、3 快速选择三个塔位。实现时需要在 Alt 按住期间暂停摄像机观察或瞄准输入，避免移动鼠标选择 UI 时镜头同时转动，并为当前选中的塔提供明确视觉反馈。

## 第 23P：游戏界面逻辑

- `GamePanel.Init()` 从玩家数据读取当前金币，以 `maxHp` 初始化当前生命值和 Slider，并更新生命值文本。
- 退出按钮会请求隐藏 GamePanel，并加载 `BeginScene` 返回开始场景。
- `UpdateHpBar(int hp)` 用于扣除生命值并同步 Slider 与生命值文本。
- `UpdateWaveNumber(int nowWave, int maxWave)` 用于更新当前波次与最大波次。
- `UpdateMoney(int count)` 用于增减当前金币并刷新文本。
- `choiceTowerArea` 初始化为隐藏，为后续选择建塔位置时显示塔选项预留入口。
- 三个 `TowerBtn` 均保存价格文本、说明文本和 Button 引用，Prefab 序列化引用完整。

本课实现继续沿用前两课的自主方案：血量通过 Slider 数值驱动；防御塔入口使用 Button；塔选项由布局组件排列。教程与当前实现的差异属于主动设计选择，不是遗漏。

## 检查与边界

- `SliderFillMinToHide.cs` 已进入 Unity 生成的 `Assembly-CSharp.csproj`，当前独立编译结果为 0 Error；Editor 日志中的两条语法错误属于修正前的历史记录。
- `SliderFillMinToHide` 当前把最大血量文本写死为 100，而 `GamePanel` 使用可配置的 `maxHp`；如果最大生命值以后不是 100，需要统一文本职责。
- `GamePanel.Init()` 已读取 `playerData.money`，但尚未把初始金币写入 `txtMoney`，因此存档金币与 Prefab 默认文字不一致时，首次显示可能不正确。
- `UpdateHpBar()` 尚未限制最低值，连续扣血可能使 `nowHp` 低于 0；接入玩家死亡流程时应统一处理生命值边界。
- `GamePanel` 中公开的 `isShow` 与 `BasePanel` 内部私有 `_isShow` 不是同一字段，当前没有参与显示逻辑。
- 第 20P 的第一条场景数据已经与 `GameScene1` 对齐；`GameScene2`、`GameScene3` 尚未建立。
- Build Settings 已包含 `BeginScene` 和 `GameScene1`。
- GamePanel Prefab 当前不会在进入 `GameScene1` 后自动创建。用户决定暂时保留该集成项，后续通过游戏场景入口调用 `UIManager.ShowPanel<GamePanel>()`；手动放入场景会因 `BasePanel` 默认隐藏状态而淡出，也不会进入 UIManager 字典。

## 下一步

进入第 24P“摄像机跟随逻辑”。后续串联游戏场景入口时，再完成 GamePanel 自动创建、初始金币显示和生命值边界处理。
