# 综合实践第 28P：怪物数据准备

## 当前状态

- 学习中。`MonsterInfo.xlsx` 由用户先写好 4 条基础记录，Codex AI Agent 参照这些记录补齐另外 12 条，形成 `Z1`～`Z4` 四个 Prefab 与 `Aggresive`、`Broken`、`Calm`、`Crawl` 四个动画控制器的全部 16 种组合。
- Excel 文件位于 `UnityTeach3-Project/Assets/ArtRes/Excel/MonsterInfo.xlsx`，按仓库规则只在本地保留，不提交第三方教学资源。表内的 `res`、`animator` 沿用现有的 `Resources` 相对路径写法。

## 参数规则

原有第 1～4 条记录保持不变。新增记录的 `atk`、`moveSpeed`、`roundSpeed`、`hp`、`atkOffset` 随动画类别取值，同一类别的四种 Prefab 使用相同参数：

| 动画类别 | atk | moveSpeed | roundSpeed | hp | atkOffset |
| --- | ---: | ---: | ---: | ---: | ---: |
| Aggresive | 5 | 100 | 100 | 10 | 0.8 |
| Broken | 5 | 50 | 50 | 10 | 1.2 |
| Calm | 5 | 60 | 60 | 10 | 1 |
| Crawl | 5 | 150 | 150 | 10 | 0.5 |

`id` 连续为 1～16；新增行从 5 开始。`Generic` 是共用动画素材，不作为独立怪物动画类别生成记录。这里的数值是用户原始四行的设置，Agent 没有重新平衡数值或改写字段名。

## 数据结构

按用户要求，`Assets/Scripts/Data/MonsterInfo.cs` 建立了与 JSON 对应的八个公开字段。后来用户把攻击间隔字段统一改为 `atkOffset`，当前 Excel、`MonsterInfo.json` 和 C# 类已一致。速度、转速和攻击间隔使用 `float`，其余数值字段使用 `int`。`GameDataMgr` 现已加载 `List<MonsterInfo>`。

## 验证与边界

- 已重新打开写回后的工作簿，核对 16 个唯一的 Prefab × 动画组合、连续 ID、按动画类别匹配的参数、Prefab 与控制器文件存在。原有四行、另外两个空工作表和列宽样式保持不变。
- `MonsterInfo.cs` 的八个字段与当前 `MonsterInfo.json` 键名逐项一致，`Assembly-CSharp` 编译为 0 Error；LitJson 已提供 JSON 整数到 `float` 的转换。
- 当前运行时读取的 JSON 将所有怪物 `moveSpeed` 设为 5，而 Excel 仍按动画类别保留 100/50/60/150。需要确认这是否为有意的运行参数覆盖；再次从 Excel 导出可能把 JSON 的 5 改回表内数值。
- `Resources/Monster/Z1～Z4.prefab` 是本地教学模型 Prefab，连同 `.meta` 排除在 Git 之外；仓库只保存自写数据、控制器配置和引用，拉取仓库后需自行在本地准备这些 Prefab 才能运行出怪逻辑。
- 这些是表格与资源路径的静态检查；Unity 中的数据加载和怪物实际行为还未验证，不能据此把第 28P 记为已完成。
