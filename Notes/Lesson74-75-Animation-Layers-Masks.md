# 第 74–75 课：动画分层和遮罩复盘

## 完成情况

- 第 74 课动画分层和 Avatar Mask 知识点已完成。
- 第 75 课两项练习均已独立完成并复查通过。
- 由于资料中没有明确的受伤动画，使用下蹲动画模拟受伤状态，验证重点仍然是动画层对基础动画的替换。

## 上半身动画层

- 新增 `MyLayer2` 动画层，权重为 1，使用 Override 混合。
- Avatar Mask 排除了 Root、双腿和脚部 IK，保留躯干、头部、双臂、手指和手部 IK，因此该层只覆盖上半身。
- 默认进入空状态 `Null`；按 J 设置 `OtherAnimation` Trigger，进入 `HumanoidCrouchIdle`，播放结束后通过 Exit Time 返回空状态。
- 下半身继续由 Base Layer 播放原动画，可以在移动时叠加上半身动作。

## 受伤基础动画替换

- 新增与 Base Layer 同步的 `IsHurt` 层，默认权重为 0。
- 同步层为待机、前进、后退和左右转向状态配置了 `HumanoidCrouch` 资源中的替代 Motion。
- 按 C 后通过 `SetLayerWeight` 把 `IsHurt` 层权重设为 1，使基础移动动画切换为下蹲版本，用来模拟角色受伤后的动作变化。
- 当前实现属于持续受伤模式；没有恢复按键或自动恢复逻辑，符合本次“基础动画改变”的验证范围。

## 复查与调试

- 初次运行日志曾出现 `Invalid Layer Index '-1'`，原因是脚本查询 `IsHurt` 时 Animator 中还没有同名有效层。
- 当前 Controller 已存在 `IsHurt` 层，名称与 `GetLayerIndex("IsHurt")` 完全一致；后续 Play Mode 日志未再次出现该错误。
- Avatar Mask 的 GUID 引用完整，Controller、脚本和原场景绑定关系保持有效。
- Unity 最近的日志中未发现本次实现导致的编译错误或空引用异常。

## 已知边界

- `Foot Avatar Mask` 的名称与实际用途不一致；它实际是上半身遮罩。功能不受影响，但更名为 `UpperBodyMask` 会更容易理解。
- `MyLayer2` 名称没有表达职责，正式项目中可使用 `UpperBodyAction` 等名称。
- `GetLayerIndex` 当前在按 C 时查询一次，练习中足够；正式脚本可在初始化时缓存索引并检查是否为 `-1`。
- `IsHurt` 权重设为 1 后不会恢复。以后实现真正的受伤反馈时，需要区分短暂受击动画与持续重伤移动状态。
- 下蹲动画来自本地 Standard Assets，只用于练习，不提交该第三方动画资源。

## 下一步

- 学习第 76 课“动画 1D 混合”。
