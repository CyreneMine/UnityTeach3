# 综合实践 UI、设置面板与摄像机动画复盘

## 当前范围

- 已完成综合实践需求准备、UI 面板基类、UIManager、开始场景、设置面板、背景音乐设置和开始按钮摄像机动画。
- `BeginScene` 通过 `Main.Start()` 请求显示 `BeginPanel`。
- 开始面板包含开始、设置、关于和退出四个按钮。
- 设置按钮已经接入 SettingPanel；开始按钮已经接入摄像机动画，动画结束后会显示 `ChoosePanel`。关于面板仍待后续制作。

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
- 设置按钮已能打开 SettingPanel；开始按钮会隐藏 BeginPanel 并播放摄像机左转动画。
- 开始按钮动画结束后的选角面板显示已经接入；关于按钮仍需等待对应面板完成。

### 设置面板与音乐数据

- `SettingPanel` 已保存关闭按钮、音乐与音效 Toggle、音乐与音效 Slider 的 Inspector 引用。
- `ShowMe()` 会读取 `GameDataMgr.musicData`，把静音状态和音量回填到控件。
- 音乐 Toggle 和 Slider 会通过 `BKMusic` 即时修改 AudioSource 的静音与音量。
- 关闭面板时重新整理 `MusicData`，通过 `JsonMgr` 写入 `Application.persistentDataPath`，再调用 UIManager 淡出并销毁面板。
- 音效设置目前完成了数据保存；实际静音和音量控制需要等音效系统建立后接入。

### 摄像机动画

- 已制作摄像机 Idle、左转、右转动画与 Animator Controller。
- `CameraAnimator` 通过 `Left`、`Right` Trigger 播放镜头动画，并缓存动画结束后需要执行的回调。
- 动画末尾事件调用 `PlayOver()`，执行并清空回调，避免同一回调残留到下一次动画。
- 开始按钮触发左转动画后会通过回调显示 `ChoosePanel`；选角面板返回时播放右转动画并重新显示开始面板。

## 调试记录

1. Canvas 已由 `UIManager` 构造函数成功创建；调用 `DontDestroyOnLoad(canvasObj)` 后，Unity 会把它移入运行时的 `DontDestroyOnLoad` 特殊场景，因此它不会继续显示在原来的 `BeginScene` 层级下。此前认为 Canvas 没有创建属于观察位置错误；UIManager 自己仍是普通托管对象，不会作为 GameObject 显示在 Hierarchy 中。
2. 面板曾因淡入条件错误而保持 `CanvasGroup.alpha = 0`；判断透明度后恢复显示。
3. `Transform.SetParent(parent)` 默认保留世界坐标，会使 UI Prefab 换父节点后产生位置偏差；传入 `false` 后按 Prefab 的局部 RectTransform 布局显示。
4. 当前 `EditorBuildSettings` 尚未登记场景，后续实现按钮切换场景前需要补充构建场景列表。
5. 设置面板淡出时曾使用 `CanvasGroup.alpha < 0` 判断隐藏完成，误以为透明度会继续减到负数。实际透明度到达下限 `0` 后不会满足该条件，导致隐藏回调未执行，面板既没有销毁，也没有从 UIManager 字典移除；完全透明的全屏面板仍会拦截射线，使开始面板按钮无法继续点击。结束条件应检查 `CanvasGroup.alpha <= 0`。这次问题说明使用 Unity 属性前需要先确认其有效范围，不能依赖超出范围的值触发流程。
6. 摄像机动画开发期间出现过回调变量名和 `Camera.print` 编译错误，现已修正；最新一次 Unity 脚本编译成功，动画事件也已进入 `CameraAnimator.PlayOver()`。

## 资源与仓库边界

- `Assets/ArtRes/`、`Assets/TextMesh Pro/`、`Assets/Resources/Music/` 和教程附带的 `Assets/Scripts/Json/LitJson/` 只在本地使用，不提交第三方内容。
- 仓库保留自己编写的脚本、场景、Prefab、Animator Controller、项目配置和学习记录。
- 克隆仓库后需要在本地重新导入对应资源，场景和 Prefab 中的第三方引用才能完整恢复。

## 下一步

进入提示面板制作；选角面板的金币不足提示和开始游戏跳转在对应面板完成后补齐。
