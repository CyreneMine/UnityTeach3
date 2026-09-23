# 综合实践第 26～30P：保护区、怪物与出怪点复查

## 复查范围与结论

用户已做到第 30P，且表示这一课已做完。本次检查了 `MainTowerObject`、`MonsterObject`、`MonsterPoint`、`GameDataMgr`、16 条怪物 JSON、四个怪物 Prefab、四类 Animator Controller 和 `GameScene1` 的序列化配置。`Assembly-CSharp` 编译为 0 Error；没有完成 Unity Play Mode 运行验收，因此第 26～30P 暂记为“待复盘”。本次只检查并记录，没有修改用户的游戏脚本、Prefab 或场景。

## 已核对的部分

- `GameScene1` 有一个启用的 `MonsterPoint`：`maxSpawnCount=5`、`totalWave=3`、`startTime=5`、`waveWaitTime=5`、`spawnWaitTime=1.5`，可选怪物 ID 为 1～4；场景里也有 `MainTowerObject`。
- 运行时 `MonsterInfo.json` 有 ID 1～16 的全部 16 种 Prefab × Animator 组合；资源路径在本地存在，Excel、JSON、C# 字段名当前均为 `atkOffset`。`GameDataMgr` 已加载怪物列表；怪物模型 Prefab 按资源排除规则只在本地保留。
- 四个怪物 Prefab 根对象均有 Animator 和 NavMeshAgent。出场、攻击和死亡动画文件中的事件名分别与 `BornOver`、`AtkEvent`、`DeadEvent` 方法对应；覆盖控制器仍需在 Unity 中实际播放检查。

## 待修正与待确认

1. **保护区内部血量与 UI 引用。** `MonsterObject.AtkEvent()` 第 66 行已调用 `MainTowerObject.Wound(_monsterInfo.atk)`，攻击事件到受伤方法的连接存在。用户随后修正了 `MainTowerObject.UpdateHp(int hp, int maxHp)`，现在会把传入值赋给 `_hp`、`_maxHp`；先前“方法忽略参数”的结论已失效。当前脚本中仍没有调用它来设置初始血量，用户计划在后续整体联调时单独调用。场景中的 `GamePanel` 是直接放置的 Prefab，虽然其 `Start()` 会调用 `Init()`，但代码没有调用 `UIManager.ShowPanel<GamePanel>()` 将它登记进管理器；因此在现有链路中调用 `UpdateHp()` 时，`GetPanel<GamePanel>()` 会返回空值。这两处留待整体联调验证。
2. **攻击层已修正，仍待运行验证。** 前一次复查时，`FrostShield` 场景实例的层与 `MonsterObject.AtkEvent()` 查询的 `MainTower` 层不匹配。用户已修改场景实例；再次检查 `GameScene1`，其根对象及相关子对象的 `m_Layer` 覆盖值均为 3（`MainTower`）。旧的层不匹配结论已失效，后续只需在 Play Mode 验证攻击范围和碰撞体命中。
3. **出怪波次交叠与单只波次边界已修正。** 旧版在 `CreateWave()` 生成第一只后就预约下一波，导致当前 5 只 × 1.5 秒的生成过程与 5 秒后的下一波重叠。用户现已把下一波的 `Invoke` 移到 `CreateMonster()` 的 `_waitSpawnCount == 0` 分支，并在首次调用 `CreateMonster()` 之前递减 `_remainingWave`；这样每波只有 1 只时也不会额外预约一波。按场景目前每波 5 只、共 3 波的配置，第一波在第 5、6.5、8、9.5、11 秒生成，第二波从第 16 秒开始。静态复查与 C# 编译为 0 Error，仍待 Play Mode 验证。
4. **表格与运行数据速度不同。** Excel 的四类 `moveSpeed` 为 100、50、60、150；`StreamingAssets/MonsterInfo.json` 中 16 条均为 5。游戏从 JSON 读取，所以目前实际速度是 5。若这是有意调整，应确定哪份文件为权威；否则下次导出 Excel 会覆盖运行值。
5. **运行边界。** 怪物死亡事件的销毁及奖励仍是 TODO，用户标注计划在后续关卡管理器完成。四个怪物 Prefab 当前没有 Collider 且根对象在 Default 层；玩家脚本的命中后伤害逻辑也还是 TODO，因此暂时不能用玩家攻击来验证怪物受伤、死亡的完整闭环。

## `Invoke` 调用顺序复盘

我的原先理解是：遇到延迟调用后，线程会停下来，等 `CreateMonster()` 连同后续几只怪物全部生成完，才返回 `CreateWave()` 并开始等待下一波。这把“一次方法调用结束”和“一整波生成结束”误当成了同一件事。

现在我的理解是：`CreateWave()` 会等待当前这一次 `CreateMonster()` 返回；但 `CreateMonster()` 中的 `Invoke("CreateMonster", spawnWaitTime)` 只登记一次未来调用，不会让主线程在这里等待，也不会等待这次未来调用可能继续预约的其他调用。当前方法生成一只怪物并登记下一只后就返回；Unity 到时间后再在主线程上执行被预约的方法。因此旧版 `CreateWave()` 在第一只生成后，就会继续运行并预约下一波，而不是等本波五只全部生成。

这会影响共享字段的读取时机：`Invoke` 保存的是要调用的方法和延迟，不会把 `_monsterId`、`_waitSpawnCount` 当时的值一起保存。旧版第一波尚有预约的 `CreateMonster()` 未执行时，第二波已经重设这两个字段；旧回调到时间后读取的是重设后的当前值，怪物类型和波次数量就可能错乱。例如第一波从第 5 秒开始，最后一只原定第 11 秒生成，而下一波在第 10 秒已经开始。这里不是两个线程同时修改字段，而是不同时刻的回调在同一个对象上读写同一组字段。

这次我把预约下一波移到 `_waitSpawnCount == 0` 的分支，并在首次生怪前递减 `_remainingWave`。这样下一波的等待从本波最后一只生成时才开始，单只波次也不会多预约一波。当前仅完成静态复查和编译检查；还要在 Play Mode 核对三波的生成时间与只数。

## 最小运行验证

先处理保护区血量/UI 初始化，再进入 `GameScene1` Play Mode：观察首波 5 只是否按 1.5 秒间隔生成、出场后是否开始寻路、靠近保护区时攻击事件是否命中并正确扣血。随后确认第二波在首波最后一只生成约 5 秒后开始，以及受伤和死亡事件是否符合当前阶段的预期。
