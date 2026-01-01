---
type: "THEORY"
title: "Page Route Transitions with PageRouteBuilder"
---


**Beyond Hero animations**, you can customize the entire page transition using `PageRouteBuilder`.

**PageRouteBuilder Properties:**

| Property | Description |
|----------|-------------|
| `pageBuilder` | Builds the destination page |
| `transitionsBuilder` | Defines the transition animation |
| `transitionDuration` | How long the transition takes |
| `reverseTransitionDuration` | Duration when popping |
| `barrierColor` | Color behind the page during transition |
| `barrierDismissible` | Can tap outside to dismiss |

**Common Transition Types:**
- **Fade** - Page fades in/out
- **Slide** - Page slides from edge
- **Scale** - Page grows from center
- **Rotation** - Page rotates in
- **Combined** - Mix multiple effects

**The transitionsBuilder provides:**
- `animation` - Forward animation (0.0 to 1.0)
- `secondaryAnimation` - Animation of the route being replaced
- `child` - The destination page widget

