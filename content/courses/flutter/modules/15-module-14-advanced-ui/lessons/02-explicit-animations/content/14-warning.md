---
type: "WARNING"
title: "Animation Memory Management"
---


**Critical: Always Dispose Controllers!**

Forgetting to dispose AnimationController causes memory leaks:

```dart
// WRONG - Memory leak!
class _MyWidgetState extends State<MyWidget>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(vsync: this, duration: ...);
  }
  // Missing dispose()!
}

// CORRECT
class _MyWidgetState extends State<MyWidget>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(vsync: this, duration: ...);
  }

  @override
  void dispose() {
    _controller.dispose(); // Always dispose!
    super.dispose();
  }
}
```

**Also Remove Listeners:**
If you add status or value listeners with anonymous functions, remove them in dispose to prevent callbacks on disposed widgets.

