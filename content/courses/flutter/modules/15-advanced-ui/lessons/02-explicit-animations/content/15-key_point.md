---
type: "KEY_POINT"
title: "Explicit Animation Checklist"
---


**Setup Checklist:**
1. Add `SingleTickerProviderStateMixin` (or `TickerProviderStateMixin` for multiple controllers)
2. Create `AnimationController` in `initState()`
3. Create `Tween` and `Animation` objects
4. Apply `CurvedAnimation` for easing
5. Use `AnimatedBuilder` for efficient rebuilds
6. Call `dispose()` on controller in `dispose()`

**Common Patterns:**

| Pattern | Code |
|---------|------|
| One-shot | `_controller.forward()` |
| Toggle | `_isActive ? forward() : reverse()` |
| Loop | `_controller.repeat()` |
| Ping-pong | `_controller.repeat(reverse: true)` |
| Delayed start | `Future.delayed(...).then((_) => forward())` |
| Staggered | Use `Interval` with different start/end times |

**Performance Tips:**
- Use `child` parameter in AnimatedBuilder for static widgets
- Avoid creating new Animation objects in build()
- Use `const` constructors where possible
- Consider `RepaintBoundary` for complex animations

