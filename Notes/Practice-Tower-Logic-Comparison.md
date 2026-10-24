# 综合实践第 35P：TowerObject 与教程版对照

## 来源与进度

- 用户自行编写了当前工程的 `Assets/Scripts/GamePanel/Object/TowerObject.cs`，并提供教程版 `TowerObject.cs` 供对照。教程源码只作参考，不复制到仓库，也不据此覆盖用户代码。
- 本记录依据两份脚本及当前数据、造塔点与本地资源路径进行静态比较。用户在 2026-10-24 反馈塔制造相关制作完成，下表已更新为本次保存的脚本。Codex 编译检查为 0 Error，初次未操作 Unity Play Mode；后续用户打包运行正常并提供炮塔/水晶塔战斗画面，第 35P 基础功能记为已完成，保留攻击边界验证。
- 教程版依赖的 `GameLevelMgr.FindMonster/FindMonsters` 和 `MonsterObject.isDead` 在当前工程中没有同名接口；当前版改用 `Physics.OverlapSphere` 寻敌，并不能直接用教程脚本替换。

## 主要实现差异

| 环节 | 教程版 | 用户当前版 |
| --- | --- | --- |
| 初始化 | `InitInfo(TowerInfo)` 只保存配置 | 现已同样只保存配置；由 `TowerPoint` 按数据中的资源路径在造塔点位置实例化，再初始化 |
| 搜索目标 | 通过 `GameLevelMgr.FindMonster/FindMonsters` 获取单体或范围目标，并在单体目标为空、死亡、出范围时重新查找 | 根据攻击类型调用 `CheckEnemy()`，用 `Physics.OverlapSphere` 和 `Monster` 层取 `Collider[]`；单体分支遍历比较距离，旧 `_target` 仅在为空时用首个结果初始化 |
| 瞄准 | 将怪物位置复制到局部变量，把副本的 Y 设为炮头高度；用 `monsterPos - head.position` 计算水平方向 | 先遍历选目标，再把最终 `_target.position` 复制给 `_realTargetPoint`，将副本的 Y 设为炮头高度；朝向仍用 `_realTargetPoint - transform.position` 计算 |
| 单体攻击 | 朝向误差小于 5° 且冷却结束后，对当前目标伤害一次；播放音效、生成并短暂显示特效 | 对准后调用 `Atk(type)`；冷却结束时伤害最终选中的 `_target`；特效挂在 `gunPoint` 下，存活时间使用 `offsetTime`，暂未播放教程中的塔音效 |
| 群体攻击 | `Update()` 的另一分支取得范围内所有怪物，冷却结束后逐个伤害 | `Update()` 已增加 `type == 2` 入口；`Atk()` 在循环外检查一次冷却，然后遍历 Collider 造成伤害，每个命中目标生成一次特效 |
| 特效方向与清理 | 使用 `gunPoint.rotation`，特效约 0.2 秒后销毁 | 单体特效使用 `Quaternion.AngleAxis(90, gunPoint.right)`，然后设 `gunPoint` 为父对象；群体分支使用 `gunPoint.rotation`。两个分支目前都按 `offsetTime` 销毁特效 |
| 数据字段 | 教程版使用 `atkType` 判断类型 | 当前数据类与 JSON 使用 `type`，1～6 号为 1，7～9 号为 2；这属于字段命名和分组约定的变化 |

## 修改后的复查结论

- 已修正：首次对照时 `_target.position = vector3` 会直接移动怪物；现在只给 `_realTargetPoint` 副本赋值，不再改怪物 Transform。
- 已修正：`type == 2` 已有 `Update()` 调用入口。群体分支的冷却判断也已移到 `foreach` 外，因此循环内更新时间不会阻止同次攻击伤害后续怪物。两个分支的特效现在都会被销毁。本地 `Resources/eff/Fire2.prefab` 也已出现；此前“缺少 Fire2”的资源结论已过时。
- 已修正：过程中曾尝试在 `CheckEnemy()` 初始化瞄准点，也出现过在选目标之前记录旧位置的版本；当前已把赋值统一放在选目标循环之后，每帧使用最终目标位置，避免未初始化和切换目标时位置落后一帧的问题。
- 已修正：`Atk()` 的单体伤害已改为取得 `_target` 的 `MonsterObject`，与炮头选中的对象一致，不再使用数组第一项。
- 仍需验证：瞄准向量仍从塔根节点 `transform.position` 而非炮头 `head.position` 起算，炮头高于根节点时可能出现不想要的俯仰角；寻敌尚未主动过滤死亡状态，旧目标也并非每次从本次查询结果重选。需测试怪物死亡动画期间的锁定与转火行为。
- 已修正：早期 `PlayerObject` 的临时 `L` 键入口曾混搭塔模型与配置等级；该测试入口现已移除，正式制造使用同一条 `TowerInfo.res` 和配置实例初始化。
- 仍需留意：`collider.GetComponent<MonsterObject>()` 假设碰撞体与怪物脚本在同一对象；若后续 Collider 放在子对象上，可能得到空引用。此前发现的图片路径不匹配已于 2026-10-24 同步修正到 Excel 与 JSON：`imgRes` 现为 `TowerImg/1_1`～`TowerImg/3_3`。

## 下一步验证

- 用户已补充建造、升级和防守的实际截图，见 [截图索引](../Screenshots/Practice-Demo/README.md)。后续仍可专项测试最近怪物死亡后的转火，以及不同高度下炮头是否只进行预期的水平旋转；不把静态比较和普通防守截图当作这些边界已全部通过。
