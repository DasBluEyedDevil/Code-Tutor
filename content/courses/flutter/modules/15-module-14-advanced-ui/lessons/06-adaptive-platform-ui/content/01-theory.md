---
type: "THEORY"
title: "Introduction - Platform Conventions and User Expectations"
---


**Every Platform Has Its Own Design Language**

Flutter's superpower is running on multiple platforms from a single codebase. But users on each platform have different expectations:

| Platform | Design System | Key Characteristics |
|----------|--------------|---------------------|
| Android | Material Design | Floating action buttons, bottom sheets, snackbars |
| iOS | Human Interface Guidelines | Navigation bars, segmented controls, action sheets |
| Web | Varies | Hover states, keyboard navigation, responsive layouts |
| Desktop | Platform-specific | Menu bars, keyboard shortcuts, window management |

**Why This Matters:**

- **iOS users** expect the back button on the left of a navigation bar, not a drawer
- **Android users** expect a floating action button for primary actions
- **Both platforms** have different date picker, alert, and navigation patterns

**The Goal:**

Share 100% of your business logic while adapting UI components to feel native on each platform. Users shouldn't notice they're using a cross-platform app.

**Two Approaches:**

1. **Adaptive:** Different widgets per platform (CupertinoButton vs ElevatedButton)
2. **Consistent:** Same design everywhere (brand-focused apps like Spotify)

This lesson focuses on the adaptive approach.

