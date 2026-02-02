---
type: "THEORY"
title: "Hero Widget Fundamentals"
---


**The Hero Widget** creates shared element transitions between routes. It works by:

1. Finding widgets with matching `tag` values on source and destination routes
2. Calculating the size and position difference
3. Animating the widget smoothly between positions during navigation

**Basic Requirements:**
- Same `tag` value on both source and destination Hero widgets
- The tag must be unique within each route
- Both Heroes must be visible when navigation occurs

**Hero Properties:**

| Property | Description |
|----------|-------------|
| `tag` | Unique identifier matching source and destination |
| `child` | The widget to animate |
| `flightShuttleBuilder` | Custom widget during flight |
| `placeholderBuilder` | Widget shown while Hero is in flight |
| `createRectTween` | Custom animation path |
| `transitionOnUserGestures` | Enable for swipe-back gestures |

**Important Notes:**
- Heroes work with `Navigator.push()` and `Navigator.pop()`
- The child widget should have similar structure on both screens
- Avoid wrapping Hero in widgets that change size (like Expanded)

