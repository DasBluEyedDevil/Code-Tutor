---
type: "THEORY"
title: "AnimationController Basics"
---


**AnimationController** is the engine that drives explicit animations. It generates values from 0.0 to 1.0 over a specified duration.

**Key Properties:**
- `duration` - How long the animation takes
- `value` - Current position (0.0 to 1.0)
- `status` - Current state (forward, reverse, completed, dismissed)

**Key Methods:**
- `forward()` - Animate from current value to 1.0
- `reverse()` - Animate from current value to 0.0
- `repeat()` - Loop the animation continuously
- `stop()` - Halt the animation at current position
- `reset()` - Jump to 0.0 without animating
- `animateTo(value)` - Animate to a specific value

**The vsync Parameter:**
Every AnimationController requires a `vsync` (vertical sync) parameter. This synchronizes the animation with the screen refresh rate for smooth 60fps performance.

To provide vsync, your State class must use `SingleTickerProviderStateMixin` (for one controller) or `TickerProviderStateMixin` (for multiple controllers).

**Important:** Always dispose of AnimationController in the dispose() method to prevent memory leaks!

