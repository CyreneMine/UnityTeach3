# 第 21–23 课：Rigidbody2D 与 Collider2D 练习复盘

## 完成情况

- 状态：已完成。
- 复查日期：2026-09-12。
- 第 21P：学习 2D 刚体基础。
- 第 22P：学习 2D 碰撞器基础。
- 第 23P：制作能够在平台上水平移动和跳跃的玩家。

## 自主实现

- 玩家使用 Dynamic `Rigidbody2D` 和 `CapsuleCollider2D`，重力比例为 1，并冻结 Z 轴旋转。
- 水平输入用于设置 `Rigidbody2D.linearVelocity.x`，同时保留当前的竖直速度。
- 按 K 键时通过 `Rigidbody2D.AddForce` 施加向上的力，实现跳跃。
- 玩家根据水平移动方向切换 SpriteRenderer 的 `flipX`。
- 平台使用非 Trigger 的 `BoxCollider2D` 或 `EdgeCollider2D`，与玩家参与同一套 2D 物理模拟。

## 调试过程

- 初始现象：玩家受重力影响持续下落，无法停在可见平台上。
- 根本原因：玩家使用 `Rigidbody2D` 与 `CapsuleCollider2D`，部分平台却使用 3D `BoxCollider`。Unity 的 2D 与 3D 物理系统彼此独立，即使双方都未开启 Trigger，也不会发生碰撞。
- 修正方法：将平台的 3D `BoxCollider` 替换为 `BoxCollider2D`。
- 验证结果：玩家可以落在平台上、进行水平移动并通过刚体 API 跳跃。

## 已知边界

- 当前没有检测玩家是否着地，因此在空中也能再次跳跃；教程明确说明本题只要求制作跳跃功能，本课不处理连续跳跃限制。
- 水平速度目前在 `Update` 中直接设置。练习规模下能够工作；后续学习物理更新节奏时，应继续区分输入采集与 `FixedUpdate` 中的物理操作。
- 字段名 `rigidbody2D` 会触发隐藏旧版继承成员的编译警告，后续可以使用 `rb` 或 `playerRigidbody` 等名称提高可读性。

## 下一步

- 学习第 24 课“物理材质”。
