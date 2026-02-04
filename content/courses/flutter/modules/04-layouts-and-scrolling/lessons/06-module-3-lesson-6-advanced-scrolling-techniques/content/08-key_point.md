---
type: KEY_POINT
---

- `SingleChildScrollView` makes any widget scrollable -- use it for forms or content that might overflow the screen
- Set `scrollDirection: Axis.horizontal` on ListView or SingleChildScrollView for horizontal scrolling (e.g., category chips)
- `CustomScrollView` with `SliverList` and `SliverGrid` lets you mix different scroll layouts in a single scrollable area
- `SliverAppBar` collapses and expands as the user scrolls, creating polished parallax-style header effects
- `NeverScrollableScrollPhysics()` disables scrolling on a nested list so the parent ScrollView controls all scrolling
