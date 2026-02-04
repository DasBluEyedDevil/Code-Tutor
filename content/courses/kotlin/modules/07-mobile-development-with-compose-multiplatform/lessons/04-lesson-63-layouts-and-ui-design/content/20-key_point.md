---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Use `Column` for vertical layouts, `Row` for horizontal, and `Box` for stacking**. These are Compose's fundamental layout primitives—combine them to create any UI structure.

**`LazyColumn` and `LazyRow` efficiently display large lists** by only composing visible items. Unlike `Column` with `forEach`, lazy components recycle composables and provide built-in scrolling performance.

**Alignment and arrangement control spacing and positioning** within layouts. `verticalArrangement = Arrangement.SpaceBetween` distributes children evenly; `horizontalAlignment = Alignment.CenterHorizontally` centers them.
