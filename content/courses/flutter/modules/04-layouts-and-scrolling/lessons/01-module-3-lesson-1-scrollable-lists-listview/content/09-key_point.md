---
type: KEY_POINT
---

- Use `ListView.builder` for dynamic lists -- it only builds visible items, keeping performance constant regardless of list size
- `ListTile` provides a ready-made list item with `leading`, `title`, `subtitle`, and `trailing` slots
- `ListView` scrolls by default; never put a `ListView` inside a `Column` without constraining its height or it will throw an error
- `itemCount` tells the builder how many items to render -- always set it to prevent infinite scrolling bugs
- `Divider()` or `ListView.separated` add visual separators between items without manual padding
