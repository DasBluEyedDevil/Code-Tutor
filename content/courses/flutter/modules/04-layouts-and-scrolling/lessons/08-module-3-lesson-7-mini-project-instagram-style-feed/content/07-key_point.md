---
type: KEY_POINT
---

- Combine ListView, GridView, Stack, and custom widgets in a single screen to build complex, real-world layouts
- Horizontal story bars use `ListView` with `scrollDirection: Axis.horizontal` inside a `SizedBox` with fixed height
- Each post is a custom widget (`PostWidget`) composed of smaller widgets for header, image, actions, and caption
- `ListView.builder` in the main feed ensures only visible posts are built, keeping scroll performance smooth
- Separate layout structure (Column/Row) from styling (Container/BoxDecoration) to keep widget trees readable
