# 综合实践第 36P：造塔点与制造流程

## 制作成果与分工

- 2026-10-24，用户反馈“塔制造相关完成了”。用户自行编写 `TowerPoint.CreateTower()`、GamePanel 的建塔/升级输入和 `TowerBtn` 展示逻辑；Codex AI Agent 负责静态复查与文档整理，没有代改这些脚本。
- 之前的重复配置由 Agent 协助：读取 Excel 生成数据类与 JSON、配置 9 个本地塔 Prefab 的枪口和炮头引用、同步特效与图片资源路径。分工详见 [数据记录](Practice-Tower-Data.md) 和 [塔逻辑对照](Practice-Tower-Logic-Comparison.md)。

## 当前流程

1. 制造初次复查时 `GameScene1` 保存了 2 个造塔点，各自配置可造塔 ID 为 `1、4、7`。后续打包前快照中，三个游戏场景各保存 1 个造塔点；对应三类一级塔。
2. 进入造塔点触发范围后，GamePanel 显示选择区域；空点展示三个塔的图标、价格和数字键提示。
3. 数字键 `1/2/3` 把对应 ID 交给 `CreateTower()`。造塔点读取配置、检查金币；不足时直接返回，足够时经玩家 `AddMoney()` 扣费。
4. 根据数据中的 `res` 在造塔点位置实例化 Prefab，并把配置交给 `TowerObject.InitInfo()`。升级沿用同一方法，先销毁旧塔，再创建下一级并更新 `nowSelTowerInfo`。
5. 已有塔时用空格键调用配置中的 `next`；`next == 0` 表示满级，关闭选择区域，再次进入时也不展示升级入口。离开造塔点时关闭选择区域和按键处理。
6. 玩家金币更新向 GamePanel 传入总额，当前 `UpdateMoney()` 已使用赋值而非重复累加。

## 本次确认的修正

- `TowerObject` 先遍历选择目标，再生成 `_realTargetPoint`；瞄准点每帧随最终选中的怪物更新。单体伤害现在也取 `_target`，不再固定伤害碰撞数组第一项。
- 塔位置由 `TowerPoint` 实例化时决定，`TowerObject.InitInfo()` 只接收配置；旧的玩家 `L` 键测试建塔入口已移除。
- Excel/JSON 的图片路径已对应 `TowerImg/1_1`～`TowerImg/3_3`。此前 9 张图均为 Multiple 且没有切片，无法取得 Sprite；用户现已将它们改为 Single，导入配置已逐一确认。

## 验证与当前边界

- 数据静态检查通过：9 个 ID 唯一，非零 `next` 都指向有效配置，9 条模型、图片和特效路径均能对应本地文件。
- `dotnet build UnityTeach3-Project/Assembly-CSharp.csproj --no-restore` 为 0 Error，存在原有依赖冲突、旧资源 API 等 Warning。本次没有修改运行代码或处理这些 Warning；编译通过不作为 Play Mode 验收。
- 用户的完成反馈作为制作进度依据；Codex 没有操作 Unity Play Mode，也没有验证三类塔逐级升级、金币不足和再次进入造塔点的全部运行组合。
- 初次发现升级展示问题：已有塔分支隐藏全部按钮后只初始化第一项，没有重新显示。用户随后已补上 `towerBtns[0].gameObject.SetActive(true)`，打包前检查确认修正。后续用户完成独立包并补充建造、升级画面，第 36P 的基础功能已完成；结算后输入按用户决定保留，详见 [检查与运行补充](Practice-PreBuild-Review.md)。
- 当前创建入口为键盘；`TowerBtn` 暂未绑定点击建造回调。先前设想的 Alt 呼出指针与鼠标选择不计为已实现。
- 用户已配置 Player 层（7）并应用到全部七个角色；进入、离开回调最终均改用 `NameToLayer("Player")` 比较层编号，静态修复通过。非玩家经过与交叠范围仍待开发包验证；塔攻击的死亡目标、炮头俯仰等边界仍见第 35P 记录。

## 下一步

- 2026-10-24 用户已反馈独立包运行正常，并提供建造三选项、费用不足提示、混合防守和升级外观截图，见 [09～15 号图](../Screenshots/Practice-Demo/README.md)。这些画面支持基础功能完成，不等于已逐一测试全部升级组合。
- 后续专项复盘可验证全部等级、满级入口、非玩家交叠及重复进入；结算输入不列为本练习的必须修复项。模块回顾见 [防御塔总结](Practice-Demo-Modules.md#8-防御塔)。
