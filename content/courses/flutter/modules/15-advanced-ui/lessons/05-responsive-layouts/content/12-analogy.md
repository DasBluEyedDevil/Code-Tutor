---
type: "ANALOGY"
title: "The Expandable Dining Table"
---

Responsive layouts work like an expandable dining table. When just two people are eating, you keep the table compact. When guests arrive, you pull out the extension leaves to make room for more place settings. The table adapts to how many people need to sit, and the dishes rearrange themselves to fill the available space.

In Flutter, your layout does the same thing. On a phone screen (small table), you might show a single column of content. On a tablet (medium table), you add a side panel. On a desktop (large table with all extensions), you might show three columns with a navigation rail. `LayoutBuilder` and `MediaQuery` tell you the table size, and your code decides how to arrange the dishes.

**Accessibility is the wheelchair ramp to your dining table.** It does not matter how beautifully your table is set if some guests cannot reach it. Semantic labels, sufficient contrast, and touch target sizes ensure that every user -- regardless of how they interact with their device -- can use your app effectively.
