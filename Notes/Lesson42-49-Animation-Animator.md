# 第 42–49 课：Animation 与 Animator 复盘

## 完成情况

- 状态：第 42–44、46–49 课已完成，第 45 课已跳过。
- 复查日期：2026-09-13。
- 第 45P 使用旧版 Animation 系统，练习内容较简单；按学习方向主动跳过手动制作。
- 第 49P 独立完成几何体机器人、待机与走路动画，以及键盘控制移动和动画切换。

## 自主实现

- 使用 Unity 自带几何体拼成机器人，并在 `Root` 下组织身体、头部、手臂和腿部层级。
- 创建循环播放的 `Idle` 和 `walk` Animation Clip。
- 创建 `Root` Animator Controller，使用 Bool 参数 `isWalk` 控制状态。
- 默认状态为 `Idle`；`isWalk == true` 切换到 `walk`，`isWalk == false` 返回 `Idle`。
- 两条过渡均关闭 Has Exit Time，因此输入改变后不必等待动画播放到指定出口时间。
- 按下 W 时设置行走状态，松开 W 时恢复待机；行走状态下使用 `Time.deltaTime` 控制机器人向前移动。

## 复查与验证

- `Animator`、控制脚本和机器人层级位于同一个 `Root` 对象，组件均已启用。
- Animator Controller、Idle Clip、Walk Clip 和脚本 GUID 引用完整。
- 动画绑定路径与场景中的 `RightHand`、`LeftHand`、`RightFoot`、`LeftFoot` 及其子对象一致。
- 两个 Animation Clip 均不是 Legacy Clip，并已开启循环。
- Animator 未启用 Apply Root Motion，位移由脚本统一控制，不会与动画根运动重复。
- 脚本已经由 Unity 重新编译，未发现当前实现导致的编译错误。

## FSM 理解情况

- 已理解 FSM 由有限个状态、当前状态、各状态的行为和状态切换条件组成。
- 已理解 `A → B` 只是一次状态切换，完整 FSM 还包括状态本身、状态行为和切换规则；状态也不一定能够双向返回，例如死亡状态可以是单向状态。
- 已通过代码修改 Animator 参数，独立完成 `Idle → Walk → Idle` 的双向切换，能够区分状态、条件、Transition 和代码驱动关系。
- 因此 FSM 基础概念和最基础的 Animator 状态机实践均已掌握。后续重点是把同一思想用于 Gameplay 代码状态机，并学习 `Enter`、`Update`、`Exit` 等状态职责，以及与敌人 AI、Animator、NavMesh 的组合。

## 已知边界

- 当前只使用 W 键向本地前方移动，满足本题要求；尚未实现后退、转向或完整方向输入。
- 参数名 `isWalk` 以字符串形式在代码中使用，初学练习足够清楚；大型项目可缓存 `Animator.StringToHash` 的结果。
- 机器人部分末端对象都命名为 `Hand`，其中腿部子对象也使用该名称。动画绑定可以正常工作，但更明确的 `Foot` 等命名更利于后续维护。
- `Lesson34` 文件夹和脚本名称与课程第 49P 不一致，不影响运行，但后续查找课程文件时容易混淆。

## 下一步

- 从第 63 课开始学习 3D 模型导入；后续出现状态机内容时，重点关注代码组织和系统组合。
