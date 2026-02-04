---
type: KEY_POINT
---

- `Container` combines padding, margin, decoration, and sizing into a single widget -- it is the most versatile layout primitive
- `padding` adds space inside the container (between border and child); `margin` adds space outside (between container and siblings)
- `BoxDecoration` enables rounded corners, gradients, shadows, and borders -- pass it via the `decoration` property, not `color`
- A Container takes exactly one `child`; to hold multiple widgets, wrap them in a `Column` or `Row` first
- Prefer `SizedBox` over `Container` when you only need fixed width/height with no styling -- it is more lightweight
