---
type: "THEORY"
title: "AnimatedBuilder - Efficient Rebuilds"
---


**AnimatedBuilder** is the recommended way to build animated widgets. It rebuilds only the animated part of the widget tree, not the entire subtree.

**Why Use AnimatedBuilder?**
1. **Performance** - Only rebuilds what changes
2. **Separation** - Animation logic separate from widget building
3. **Reusability** - Same animation can drive different widgets

**The Child Parameter:**
Pass static widgets as the `child` parameter. These won't rebuild during animation, improving performance.

```dart
AnimatedBuilder(
  animation: _controller,
  builder: (context, child) {
    // child is the static widget passed below
    return Transform.scale(
      scale: _animation.value,
      child: child, // This doesn't rebuild!
    );
  },
  child: const ExpensiveWidget(), // Built once, reused
)
```

**Alternative: AnimatedWidget**
For reusable animated widgets, extend `AnimatedWidget` instead of using AnimatedBuilder.

