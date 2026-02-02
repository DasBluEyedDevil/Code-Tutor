---
type: "THEORY"
title: "State Lifecycle: initState and dispose"
---

A `StatefulWidget` has a "lifecycle"—a sequence of events from when it's born to when it's destroyed. Two methods are essential:

### 1. `initState()`
This runs exactly **once** when the widget is first created. Use it for setup:
- Initializing controllers
- Starting timers
- Fetching initial data

```dart
@override
void initState() {
  super.initState(); // Always call super.initState() first!
  _controller = TextEditingController();
  print('Widget created!');
}
```

### 2. `dispose()`
This runs exactly **once** when the widget is permanently removed. Use it for cleanup:
- Closing controllers
- Canceling timers
- Unsubscribing from streams

```dart
@override
void dispose() {
  _controller.dispose(); // Save memory!
  super.dispose(); // Always call super.dispose() last!
  print('Widget destroyed!');
}
```