# 第 63–73 课：3D 模型导入与动画复盘

## 完成情况

- 第 63–72 课的模型导入、Model、Rig、Animation、Materials 和 3D 动画使用知识点已完成。
- 第 73 课练习已独立完成并复查通过。
- 使用 Ethan 模型创建 Animator Controller，通过键盘输入控制前进、后退、左右转向和跳跃动画。

## 自主实现

- 使用 Float 参数 `Speed` 接收垂直轴输入：正值进入前进状态，负值进入后退状态，接近零时返回 Idle。
- 使用 Float 参数 `horizontalOffset` 接收水平轴输入：负值进入左转状态，正值进入右转状态，接近零时返回 Idle。
- 使用 Trigger 参数 `jump` 响应空格键，从 Any State 进入跳跃状态。
- 当前移动和转向依赖动画 Root Motion；脚本只负责向 Animator 写入输入参数。
- 后退状态通过倒放行走动画实现，满足本课练习范围。

## 复查与修正

- Ethan 模型、Avatar、Animator Controller 和控制脚本引用完整，脚本组件已启用。
- Controller 默认状态为 Idle，前进、后退和左右转向均配置了进入与返回条件。
- 初次复查时，跳跃状态没有出口，静止跳跃后会停在跳跃状态，且无法再次触发跳跃。
- 已添加 `HumanoidIdleJumpUp → HumanoidIdle` 的无条件过渡，开启 Has Exit Time，动画播放到 Exit Time 后返回 Idle。
- 跳跃动画中的 `Jump` Animation Event 由脚本中的同名方法接收；修正后最近一次 Play Mode 日志未发现新的接收器警告、脚本异常或编译错误。

## 与教程答案的实现差异

- 教程没有为 A/D 单独配置旋转动画，而是在脚本中调用 `transform.Rotate` 直接改变角色朝向。
- 教程的 Animator 主要保留待机、前进、后退和跳跃状态，并从 Any State 直接连接这些状态，通过更多条件约束决定切换目标。
- 当前自主实现增加了左转和右转动画状态，由 `horizontalOffset` 驱动，并通过动画 Root Motion 完成转向。
- 当前方案能够满足练习目标，但更加依赖动画资源是否包含合适的根旋转；教程方案的旋转速度和方向更容易由代码精确控制，也更容易在没有转向动画时复用。
- 这两种方案体现了职责划分的差异：教程把“角色旋转”放在移动代码中，Animator 负责表现；当前方案让 Animator 和动画数据同时承担转向表现及实际旋转。

## 已知边界

- `Jump()` 当前为空，只用于接收资源自带的 Animation Event；本课实现的是动画与 Root Motion 演示，没有加入 Rigidbody、CharacterController 或落地检测。
- `horizontalOffset` 字段没有参与控制，`moveSpeed` 也因手动位移代码被注释而未使用；不影响本课功能，但后续正式脚本应删除无效字段或实现对应职责。
- 目录和脚本使用 `Lesson50` 命名，与课程第 73P 不一致，运行不受影响，但会增加后续查找成本。
- Ethan、FBX 动画和从教程资源复制出的 Animation Clip 不应提交到远程仓库；推送时需要继续选择性排除第三方资源。

## 下一步

- 学习第 74 课“动画分层和遮罩”。
