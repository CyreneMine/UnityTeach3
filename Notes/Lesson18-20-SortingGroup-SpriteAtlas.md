# 第 18–20 课：Sorting Group 与 Sprite Atlas 复盘

## 完成情况

- 状态：已完成。
- 复查日期：2026-09-12。
- 第 18P：学习 Sorting Group 的基础用途。
- 第 19P：学习 Sprite Atlas 的创建、打包与使用。
- 第 20P：独立完成图集加载练习和 Draw Call 问答。

## 图集练习

- 在 `Assets/Resources` 下创建 `MyAtlas.spriteatlasv2`。
- 图集收录 `Assets/Standard Assets/2D/Sprites` 目录中的 Sprite。
- 通过 `Resources.Load<SpriteAtlas>("MyAtlas")` 加载图集。
- 通过 `GetSprite("Down1")` 取得图集中的一张 Sprite，并赋给运行时创建对象的 SpriteRenderer。
- 脚本已经由 Unity 编译，场景中的练习组件处于启用状态。

## Draw Call 问答

- 三张透明图片的渲染顺序为“图集 A、纹理 B、图集 A”时，答案是 3 个 Draw Call。
- 虽然只涉及两张纹理，但中间的纹理 B 打断了图集 A 的连续绘制，前后两个 A 不能跨过 B 合并。
- 如果纹理 B 位于最上层或最下层，顺序变成“A、A、B”或“B、A、A”，两个连续的 A 在满足其他批处理条件时可以合并，因此通常是 2 个 Draw Call。
- 判断 Draw Call 不能只数不同纹理的数量，还要看材质、图集页和实际渲染顺序是否允许组成连续批次。

## 已知边界

- 图集当前收录整个 Sprites 目录，练习中可以使用；正式项目通常只收录需要一起使用的资源，避免图集过大或包含无关 Sprite。
- 同一个 Sprite Atlas 可能因尺寸限制被拆成多个纹理页；位于不同页的 Sprite 仍可能产生额外 Draw Call。
- 教程素材位于已忽略的 `Standard Assets` 中，不提交到仓库；其他设备需要自行准备对应资源。

## 下一步

- 学习第 21 课“刚体”。
