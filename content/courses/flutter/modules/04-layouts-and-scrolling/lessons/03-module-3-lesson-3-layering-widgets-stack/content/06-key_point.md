---
type: KEY_POINT
---

- `Stack` layers children on top of each other -- the first child is the bottom layer, the last child is on top
- `Positioned` places a child at exact coordinates within the stack using `top`, `bottom`, `left`, and `right`
- `Alignment` on the Stack itself sets the default alignment for non-positioned children (e.g., `Alignment.center`)
- Use Stack for overlays like text on images, notification badges on icons, or floating action buttons over content
- `Positioned.fill` stretches a child to cover the entire stack -- useful for background layers or gradient overlays
