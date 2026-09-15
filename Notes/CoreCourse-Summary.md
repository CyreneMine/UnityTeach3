# Unity 核心主课程阶段总结

## 阶段结论

- 96P 主课程的理论阶段已经结束，并按当前以 3D Gameplay/客户端开发为主的路线完成取舍。
- 状态统计：73 课已完成，22 课已跳过，1 课待复盘。
- 已跳过内容集中在 Tilemap、2D 骨骼、2D 换装、Spine 和旧版 Animation 练习。这些内容不是判定为无价值，而是根据近期 3D 学习目标暂缓，需要时再补。
- 第 88 课 CharacterController 的代码与坐标空间概念已经复查通过；移动碰撞以及角色处于不同位置、朝向时的实际画面仍需补充运行验证。

## 已形成的能力

### 资源、渲染与 2D 物理

- 能区分 Texture、Sprite、Multiple 切分、Polygon、Sprite Mask、Sorting Group 和 Sprite Atlas 的职责。
- 能定位资源名、字典键、Inspector 引用和场景保存导致的空引用或查找失败。
- 能使用 Rigidbody2D、Collider2D、力和速度完成基础移动、跳跃与碰撞练习，并理解 Update 与 FixedUpdate 的职责边界。
- 理解图集能减少纹理切换，但 Draw Call 仍会受到渲染顺序和批处理是否连续的影响。

### 3D 模型与动画

- 完成 Model、Rig、Avatar、Animation Clip、Materials 和预览窗口等模型导入流程学习。
- 能建立 Animator Controller，配置参数、双向过渡、Exit Time、子状态机、动画分层、Avatar Mask、1D/2D Blend Tree 和 IK。
- 理解教程方案只是演示实现；能够对比直接旋转、状态机约束、Prefab 实例化和运行时组件组装等不同做法。
- 在 IK 练习中明确区分方向向量与世界位置：先围绕角色自身轴旋转方向，最后加目标基准位置，避免把平移量一起绕世界原点旋转。

### 角色控制与导航

- 能使用 CharacterController 组织移动、转向和重力，并理解输入方向在世界空间与本地空间之间的差异。
- 能安装并使用 AI Navigation，完成 NavMeshSurface 烘焙、NavMeshAgent 点击寻路、动画切换和 NavMeshLink 基础学习。
- 能使用 NavMeshObstacle 的 Carve 制作动态阻路，并通过 LayerMask 限制射线检测；障碍被禁用后，Agent 可重新规划路径。

## 主要调试收获

- 先区分代码、场景、Prefab、Inspector 和运行时状态，再判断错误来源。
- 场景修改必须保存；退出 Play Mode 后丢失的改动不能视为已完成。
- 空引用通常意味着对象或组件引用尚未建立；字典键异常意味着实际导入后的名称与代码假设不一致。
- 能运行只说明基本链路接通，仍需检查输入边界、坐标空间、Animator 参数、碰撞配置和重复运行表现。
- 使用 LayerMask 在查询阶段过滤目标，比命中所有碰撞体后再判断 Layer 更直接。

## 已知边界与后续补充

- 第 88 课仍需验证 CharacterController 在不同位置和朝向下的移动、碰撞及 LookAt IK 画面。
- Tilemap、2D 骨骼、换装和 Spine 暂不投入练习时间；未来项目真正需要时，再按对应路线记录补学。
- 当前练习以理解单个系统为主，还没有验证跨场景状态清理、数据持久化、完整 UI 流程和完整玩法闭环。

## 下一阶段

进入 38P 综合实践。先从需求分析和最小可玩闭环开始，再依次串联 UI 管理、设置数据、场景选择、摄像机、玩家、怪物、关卡、防御塔以及游戏结束流程。每个模块继续保留独立验证、问题复盘和 Git 记录。
