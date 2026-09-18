# 综合实践提示面板复盘

## 当前进度

- 第 16P“提示面板：拼界面”已经完成。
- 第 17P“提示面板：界面逻辑”已经完成。
- 提示面板已经接入第 15P 选角面板的金币不足分支。
- 本阶段功能代码和 Prefab 由用户完成；Codex 负责检查引用、调用链、编译结果和仓库边界。

## 界面结构

- `TipPanel` 根节点使用全屏拉伸的 `RectTransform`，可以覆盖当前界面。
- 根节点的半透明 `Image` 启用了射线检测，在提示显示期间阻止点击后方选角面板。
- 面板包含提示背景、标题、动态提示文本和确定按钮。
- Prefab 保存于 `Assets/Resources/UI/TipPanel.prefab`，名称与 `UIManager.ShowPanel<TipPanel>()` 的 Resources 加载约定一致。

## 逻辑实现

- `TipPanel` 继承 `BasePanel`，继续使用统一的 `CanvasGroup` 淡入淡出流程。
- `Init()` 为确定按钮注册关闭事件，调用 `UIManager.HidePanel<TipPanel>()`。
- `ChangeInfo(string info)` 修改提示文本，因此同一个面板可以复用来显示不同消息。
- `ChoosePanel` 在金币不足时创建提示面板，并立即传入“金币不足！”作为本次提示内容。
- 淡出结束后，`UIManager` 会销毁面板并从缓存字典移除，下一次提示可以重新实例化。

## 检查与验证

- `TipPanel` 脚本中的 `btnSure` 和 `txtInfo` 均已绑定到 Prefab 内的有效组件。
- 根节点、遮罩、提示文本和按钮层级完整，没有发现 Missing 脚本或空序列化引用。
- `TipPanel.cs` 已进入 Unity 生成的 `Assembly-CSharp.csproj`；独立编译结果为 0 Error，现有 Warning 来自本地第三方旧资源。
- 最新 Unity Editor 日志未发现 `TipPanel` 相关异常。
- `Assets/ArtRes/`、`Assets/TextMesh Pro/`、`Assets/Resources/Music/` 和 `Assets/Scripts/Json/LitJson/` 仍受 `.gitignore` 排除，本次提交不包含第三方资源。

## 已知边界

- 当前调用点传入固定的“金币不足！”文本；后续其他系统也可以复用 `ChangeInfo()`，无需为每种提示创建新的 Prefab。
- `UIManager.ShowPanel<T>()` 对已经存在的同类型面板会直接返回缓存实例。当前提示面板会阻挡后方按钮，正常操作下不会在淡出期间重复触发；如果以后允许并发提示，需要再定义覆盖、排队或刷新策略。
- 静态文件检查不能替代 Game 视图交互验证。应至少确认提示出现、后方按钮被阻止、确定后淡出消失，以及再次金币不足时能够重新显示。

## 下一步

进入第 18P“场景选择面板：拼面板”；该面板完成后，回到第 15P 接入开始游戏后的场景选择跳转。
