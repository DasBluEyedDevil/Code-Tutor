---
type: "THEORY"
title: "GoRouter Custom Transitions"
---


**GoRouter** is Flutter's recommended declarative routing package. It supports custom transitions through the `pageBuilder` parameter.

**Default Transitions:**
GoRouter uses platform-appropriate transitions by default:
- **iOS:** Slide from right (Cupertino style)
- **Android:** Fade/slide (Material style)

**Custom Transitions with GoRouter:**
Use `CustomTransitionPage` to define custom animations for specific routes.

**Key Properties:**

| Property | Description |
|----------|-------------|
| `child` | The destination page widget |
| `transitionsBuilder` | Custom transition animation |
| `transitionDuration` | Animation duration |
| `reverseTransitionDuration` | Pop animation duration |
| `maintainState` | Keep page state when not visible |
| `fullscreenDialog` | Present as modal dialog |

