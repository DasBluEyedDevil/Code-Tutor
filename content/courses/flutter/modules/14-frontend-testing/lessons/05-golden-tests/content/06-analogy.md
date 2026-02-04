---
type: "ANALOGY"
title: "The Before-and-After Photo"
---

Golden tests work exactly like before-and-after photos in home renovation. Before you start any work, you take a photo of the kitchen (the "golden" reference image). After every change, you take a new photo and hold it up next to the original. If the photos match, your renovation went as planned. If they differ, something changed that you need to review -- maybe intentionally (you upgraded the countertops) or accidentally (you scratched the wall).

In Flutter, golden tests render a widget to a pixel-perfect image and compare it against a saved reference file. When you first create the test, it generates the golden image. On every subsequent run, it renders the widget again and compares pixel-by-pixel. A mismatch means the visual output changed.

**The power is in catching unintentional visual regressions.** Someone updates a padding value or changes a theme color, and the golden test flags the difference before it ships. When the change is intentional, you simply update the golden file -- taking a new "before" photo for future comparisons.
