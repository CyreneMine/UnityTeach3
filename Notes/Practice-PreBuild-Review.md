# 综合实践打包前检查（2026-10-24）

> 下文“打包前”部分保留当时静态检查快照。用户之后已完成打包并反馈运行正常，实际操作与补图范围见文末“打包后运行补充”，不再将“尚未打包”视为当前状态。

## 结论与范围

- 用户反馈练习项目基本完成，准备打包。本次按当前 Windows 桌面工程进行静态检查；未修改脚本、Prefab、场景或项目配置，也未执行 Unity Player Build 或 Play Mode。
- 未发现本次检查范围内的 C# 编译阻断或数据资源路径缺失。用户明确接受结算后仍可处理输入的练习边界；选关编号同步和造塔玩家筛选均已通过最终静态复查。可开始开发包测试，独立包验收尚未完成。
- 主工程及 Editor 程序集编译为 0 Error、38 Warning，后者主要为旧资源 API、程序集依赖冲突及未使用字段。编译命令为 `dotnet build UnityTeach3-Project/Assembly-CSharp-Editor.csproj --no-restore -v:q`；生成工程的编译使用 Editor 定义，不代替独立 Player 构建验证。
- 用户修正第 2、3 项后再次编译主工程为 0 Error、36 Warning；编译通过未能发现层编号与位掩码比较的语义错误。

## 已检查的配置和引用

- 构建场景列表依次为 `BeginScene`、`GameScene1`、`GameScene2`、`GameScene3`，四项均启用，路径与 GUID 对应；第一项为开始场景。
- 三个游戏场景各有一个保护区、一个出生点 `HeroBornPos`、一个出怪点、一个造塔点和一个带跟随逻辑的主摄像机。本次保存的每个出怪点为 5 波、每波 5 只、首次等待 5 秒、波间隔 2 秒、单只间隔 1.5 秒，怪物 ID 1～4；与早期 2 波练习快照不同。
- 三个场景的 NavMeshSurface 均引用实际存在的 NavMeshData；这只确认资源引用，不代表已测试导航可达性。
- 核对 7 条角色、16 条怪物、9 条塔和 3 条场景数据：动态资源路径均能对应本地文件，无重复 Resources 路径键；塔的非零升级 ID 有效。
- 扫描四个构建场景及 Resources 下 Prefab/Controller/动画等共 55 个序列化文件，外部 GUID 均可解析；没有查到 `m_Script` 为空的组件。没有把它视为所有子资源 fileID、着色器或运行时引用均已验证。
- 七个角色 Prefab 都有 `PlayerObject` 和有效的 `gunPoint` 引用；九个编号塔 Prefab 均有 `TowerObject` 及有效的 `gunPoint/head` 引用。
- 所有九张塔图片已设置 Sprite Single；图片路径使用不带扩展名的 `TowerImg/1_1`～`TowerImg/3_3`。
- `activeInputHandler=2`（Both），与当前使用旧 `Input.GetAxis/GetKeyDown` 的脚本相容。
- 此前升级按钮隐藏问题已补上重新显示第一项；BasePanel 淡入/淡出已改用 `Time.unscaledDeltaTime`，GameOverPanel 显示时设 `timeScale=0`，BeginPanel 初始化时恢复为 1。
- 首次运行没有 PlayerData 文件时会使用默认 0 金币、空解锁列表；当前 1、4 号角色价格为 0，可以先免费解锁再开始，不会因初始金币不足而锁死入口。实际保存与重启读取仍需在包内测试。

## 初次发现与后续处理

### 1. 结算后仍处理建塔按键（用户接受保留）

- 位置：`Assets/Scripts/GamePanel/UI/GamePanel.cs` 的 `Update()`，以及 `PlayerObject.Update()` 的输入。
- 触发：玩家站在造塔点内时出现胜负结算框，再按数字键或空格。结束面板只暂停了游戏时间，GamePanel 的 `canCreateTower` 仍可能为真，输入分支仍执行，可继续扣费、建造或升级。
- 用户明确决定：第 1 点对当前练习项目不影响，不需要修复。此项作为已知边界保留，不再作为打包前必须处理或验收阻断项。

### 2. 造塔点的触发筛选（静态修复通过）

- 位置：`Assets/Scripts/GamePanel/Object/TowerPoint.cs` 的 `OnTriggerEnter/Exit()`。
- 初次两个回调没有判断身份。过程中曾出现 layer 与 GetMask 掩码直接比较，以及再次对 GetMask 左移的版本；两者都把层编号与掩码混用。
- 最终保存的两个回调均已使用 `other.gameObject.layer != LayerMask.NameToLayer("Player")`。TagManager 的 Player 层为 7，七个角色 Prefab 的对象均为 7，代码与配置对应，静态复查通过。`NameToLayer("Player")` 返回 7，`GetMask("Player")` 返回 128，两者用途不同。
- 非玩家经过、进入/离开及交叠区域行为仍在开发包中验证，不把静态修复记成运行验收。
- 验证：玩家在造塔点内时让怪物经过，确认选择区域不会因怪物进入或离开而变化。

### 3. 选关编号同步（静态修复通过）

- 位置：`Assets/Scripts/BeginPanel/UI/ChooseScenePanel.cs` 的开始按钮回调；`GameLevelMgr.InitInfo()`。
- 初次使用面板私有的 `nowSceneId` 加载场景，却未同步关卡初始化使用的编号。用户现已在开始按钮回调中、异步加载之前添加 `GameDataMgr.Instance.nowSelSceneId = nowSceneId`，与关卡管理器读取方式一致，静态复查通过。
- 当前三条场景配置都是 200 金币、100 血量，所以眼下数值相同会掩盖问题；以后差异化配置会错用第一关参数。
- 验证：在可恢复的测试配置中让第二关金币或血量与第一关不同，选择第二关确认读取正确，再恢复数据。

## 不阻断本次练习打包的边界

- 塔的死亡目标过滤、炮头方向和群攻多特效数量仍保留复盘；当前单体寻敌与伤害已经使用同一个 `_target`。
- UI 提供数字键建塔、空格升级；先前设想的 Alt/鼠标点击建造尚未实现，不应写进已完成功能。
- `BeginPanel` 的“关于”按钮回调为空。若保留展示，需明确这是练习占位入口。
- 当前 `JsonMgr` 优先 StreamingAssets，其次 persistentDataPath。PlayerData/MusicData 目前不随包提供初始 JSON，能够走本地存档；如果以后给这两者增加 StreamingAssets 默认文件，要避免默认文件一直覆盖保存结果。
- 分辨率、独立包的材质/字体/输入/音频和三关实际可达性尚无本次运行证据。

## 包内最小验收

1. 从 `BeginScene` 启动开发包：检查首次进入、免费解锁和选角，依次进入三关，确认参数、出生与摄像机正常。
2. 完成一次建塔→二级→三级，检查图片、扣费、金币不足拒绝及满级入口关闭。
3. 分别触发胜利和失败：确认场景暂停、UI 渐显/点击正常、返回菜单恢复时间；结算后的玩法输入按用户决定保留，不列为通过条件。
4. 同一进程返回后再开一局，确认波数/存活数清零、UI 不重复、角色和塔不串局。
5. 关闭程序再启动，确认金币、解锁和音量设置保存；不删除或覆盖用户已有存档。
6. 至少检查 16:9 与另一种宽高比的布局，检查 Development Build 的 Player.log 是否有 Error/Exception。

## 后续总结

- 已补充 [项目总览](Practice-Demo-Overview.md)、[八模块总结](Practice-Demo-Modules.md)、[简历介绍](Practice-Demo-Resume.md) 和 [截图索引](../Screenshots/Practice-Demo/README.md)；分别说明已实现、实际验证、待完善和 Agent 辅助范围。

## 打包后运行补充（2026-10-24）

- 用户完成 Windows 构建并反馈“打包完了，运行正常”。Agent 实际启动本机 `Builds/UnityTeachDemo3.exe`，不是由 Agent 重新执行 Unity 构建。
- Agent 操作并保存 01～08：主菜单、设置开关面板、两种角色预览、选关、加载战斗、失败结算、确认返回菜单与再次进入选角。失败奖励 100，同一进程金币由 6325 变为 6425。操作使用已有存档，没有删除存档或更改游戏代码。
- 用户补充 09～15：三类塔建造选择、炮塔与角色战斗、空格升级/费用提示、混合塔防守、升级后水晶塔外观、开火命中特效及成功结算。成功图显示剩余波数 0/10、保护区 15/100、奖励 6300；截图与用户说明共同作为这些基础功能的完成依据。
- 实际运行图中共 10 波；前文每点 5 波和旧文中的 2 波是各自检查时的配置快照，不能用静态快照推翻包内实际显示。
- 运行结果补充了基础闭环证据，但没有穷举包内最小验收表所有项目：跨进程存档、全新存档、全部三关/角色/塔组合、音频、多宽高比、非玩家交叠与日志专项检查仍未完整覆盖。用户 11 号图的升级提示底部文字有裁切，保留原图并记录为布局改进项。
- 结算后输入仍按用户决定接受保留。本次只整理画面与文档，没有修改脚本、Prefab、场景或配置。
