# 综合实践 UI 起步与开始面板复盘

## 当前范围

- 已完成综合实践需求准备、UI 面板基类、UIManager、开始场景和开始面板主体。
- `BeginScene` 通过 `Main.Start()` 请求显示 `BeginPanel`。
- 开始面板包含开始、设置、关于和退出四个按钮。
- 设置、关于和后续游戏入口依赖尚未制作的面板或场景，因此当前只保留入口回调，后续模块完成后再串联。

## 当前实现

### 面板基类

- `BasePanel` 在 `Awake()` 中取得或补充 `CanvasGroup`。
- `ShowMe()` 和 `HideMe()` 配合透明度实现基础淡入淡出。
- 派生面板通过 `Init()` 注册自己的按钮事件。
- 淡入淡出的判断对象应是 `CanvasGroup.alpha`，速度字段 `_alphaSpeed` 只负责决定每秒变化量。

### UIManager

- `UIManager.Instance` 提供普通 C# 单例入口，不作为组件显示在 Hierarchy 中。
- 构造时从 `Resources/UI/Canvas` 创建全局 Canvas，并调用 `DontDestroyOnLoad` 保留。
- 面板按类型名从 `Resources/UI/` 加载，以字典缓存已经显示的面板。
- UI Prefab 挂到 Canvas 时使用 `SetParent(_canvas, false)`，保留 Prefab 的局部锚点、位置、尺寸和缩放。

### 开始面板

- `BeginPanel` 已继承 `BasePanel`，四个 Button 引用均保存在 Prefab 中。
- 运行日志验证了 `Main.Start()`、`UIManager.ShowPanel<BeginPanel>()` 和 `BeginPanel(Clone)` 的创建链路。
- 退出按钮调用 `Application.Quit()`；该行为需要在构建后的播放器中验证，Unity Editor 内不会直接退出应用。
- 开始、设置和关于按钮的最终行为需要目标场景或目标面板存在后才能完整验证。

## 调试记录

1. Canvas 已由 `UIManager` 构造函数成功创建；调用 `DontDestroyOnLoad(canvasObj)` 后，Unity 会把它移入运行时的 `DontDestroyOnLoad` 特殊场景，因此它不会继续显示在原来的 `BeginScene` 层级下。此前认为 Canvas 没有创建属于观察位置错误；UIManager 自己仍是普通托管对象，不会作为 GameObject 显示在 Hierarchy 中。
2. 面板曾因淡入条件错误而保持 `CanvasGroup.alpha = 0`；判断透明度后恢复显示。
3. `Transform.SetParent(parent)` 默认保留世界坐标，会使 UI Prefab 换父节点后产生位置偏差；传入 `false` 后按 Prefab 的局部 RectTransform 布局显示。
4. 当前 `EditorBuildSettings` 尚未登记场景，后续实现按钮切换场景前需要补充构建场景列表。

## 资源与仓库边界

- `Assets/ArtRes/`、`Assets/TextMesh Pro/` 和教程附带的 `Assets/Scripts/Json/LitJson/` 只在本地使用，不提交第三方内容。
- 仓库保留自己编写的脚本、场景、Prefab、Animator Controller、项目配置和学习记录。
- 克隆仓库后需要在本地重新导入对应资源，场景和 Prefab 中的第三方引用才能完整恢复。

## 下一步

制作设置面板并接入 UIManager，再为开始界面的设置按钮补齐显示与返回流程。
