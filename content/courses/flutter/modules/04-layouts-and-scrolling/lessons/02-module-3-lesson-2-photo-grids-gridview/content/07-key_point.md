---
type: KEY_POINT
---

- `GridView.count` creates a fixed-column grid; set `crossAxisCount` to control how many columns appear
- `GridView.builder` lazily builds grid items on demand -- use it for large or dynamic data sets
- `crossAxisSpacing` and `mainAxisSpacing` control spacing between grid cells; `padding` adds space around the entire grid
- `childAspectRatio` controls the width-to-height ratio of each cell -- adjust it to make cells taller or wider than square
- Wrap grid items in `Card` or `Container` with decoration for consistent visual styling across the grid
