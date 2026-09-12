# 第 13 课：SpriteRenderer 练习复盘

## 完成情况

- 状态：已完成。
- 复查日期：2026-09-12。
- 第一题：使用 `Resources.LoadAll<Sprite>` 加载 Multiple 类型图片，将图集名和子 Sprite 名作为两层字典键进行缓存，并把指定 Sprite 赋给运行时创建的 `SpriteRenderer`。
- 第二题：角色通过 W、A、S、D 控制上下左右移动，移动量乘以 `Time.deltaTime`；水平输入同时控制 `SpriteRenderer.flipX`。

## 调试记录

1. `MultipleMgr.Instance` 最初只返回未初始化的静态字段，调用 `GetSprite` 时触发 `NullReferenceException`。
2. 构造函数最初再次创建 `MultipleMgr`，存在无限递归风险。最终改为静态字段初始化实例，私有构造函数不再创建自身。
3. 调用时使用了不存在的子 Sprite 名 `RobotBoyCrouchSprite_5`，字典索引器触发 `KeyNotFoundException`。核对 Sprite Editor 和 `.meta` 后改为真实名称 `RobotBoyCrouch05`。
4. `PlayerObject` 最初没有保存进场景，导致磁盘中的练习场景缺少移动组件。退出运行状态并保存场景后，挂载和参数已写入场景文件。
5. 场景最初引用了被 Git 忽略的本地 Store 素材。为完成本地验证，重新选择了 `Assets/Resources` 中的副本；考虑版权风险，该图片及其 `.meta` 仍不提交到公开仓库。

## 复查与验证

- `Lesson10` 场景中存在启用的 `PlayerObject`，并同时挂载 `SpriteRenderer` 与 `PlayerObject` 脚本。
- `moveSpeed` 保存值为 `5`，角色 Transform 和 Sprite 引用均已序列化。
- 项目输入模式为 `Both`，当前使用的旧输入 API `Input.GetAxis` 可以工作。
- 三个练习脚本修改后，`Assembly-CSharp.dll` 已重新生成，未发现当前编译阻断。
- 用户确认两道题均已运行完成。

## 已知边界

- `GetSprite` 首次加载后使用字典索引器读取名称；传入错误名称仍会抛出 `KeyNotFoundException`。后续可用 `TryGetValue` 统一处理未找到的情况。
- 水平和垂直轴分别移动，斜向输入速度会高于单轴速度。本课需求可以接受，后续角色移动课程应使用方向向量归一化或限长。
- 练习仍使用字符串资源路径和旧输入 API，适合当前知识点练习，不作为大型项目的最终资源与输入架构。
- 教程角色图片仅保留在本地。重新克隆仓库后，场景中的 Sprite 引用会缺失，第一题也需要自行放入一张切分为 Multiple 的同名练习图片才能运行验证；代码和复盘记录不受影响。

## 下一步

- 学习第 14 课“Sprite Creator 精灵创造者”。
