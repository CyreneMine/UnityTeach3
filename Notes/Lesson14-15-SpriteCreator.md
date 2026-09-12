# 第 14–15 课：Sprite Creator 练习复盘

## 完成情况

- 状态：已完成。
- 复查日期：2026-09-12。
- 第 14P：学习 Sprite Creator，并完成按空格发射菱形子弹的练习。
- 第 15P：观看练习答案，比较 Prefab 实例化与运行时直接组装对象的实现方式。

## 自主实现

- 在角色脚本中监听空格键按下。
- 运行时创建子弹 `GameObject`，设置出生位置并添加 `SpriteRenderer`。
- 通过序列化的 `bulletSprite` 引用为子弹指定菱形 Sprite。
- 添加 `bulletObj` 组件并根据角色水平朝向设置移动方向。
- 子弹移动量乘以 `Time.deltaTime`，并在 5 秒后自动销毁。

## 与教程答案的比较

- 当前实现直接通过 `new GameObject`、`AddComponent` 和 Sprite 引用组装子弹，组件少时直观有效，也能练习运行时创建对象。
- 教程把子弹制作成 Prefab，再通过 `Instantiate` 创建。Prefab 可以统一保存 Sprite、脚本和参数，组件增多或需要多种子弹时更容易复用和调整。
- 两种方式都会创建新的运行时对象；Prefab 的主要优势是配置和维护，不代表当前直接组装方案错误。

## 复查与验证

- `PlayerObject` 场景对象已启用，并挂载 `SpriteRenderer` 与角色控制脚本。
- `bulletSprite` 已在场景中赋值，引用指向 Sprite Creator 生成的菱形资源。
- 项目输入模式为 `Both`，`Input.GetKeyDown(KeyCode.Space)` 可以与当前旧输入 API 配置共同工作。
- `bulletObj.ChangeDic` 在添加组件后立即设置方向，早于该组件首次执行 `Update`。
- 用户确认练习完成并已观看教程演示。

## 已知边界

- `bulletObj` 类名和 `nowDic` 字段名表达不够准确；后续可使用 PascalCase 类名和 `direction` 一类语义名称。
- 子弹的 Sprite、移动脚本、速度和生命周期分散在代码中；组件或参数增加后更适合改为 Prefab。
- 当前每次发射都会创建和销毁对象。练习规模可以接受，高频大量子弹场景再考虑对象池。
- 场景中还存在一个只有 Transform、名称为 `GameObject` 的空对象，不影响练习运行，但后续整理场景时可以删除或赋予明确职责。

## 下一步

- 学习第 16 课“Sprite Mask 精灵遮罩”。
