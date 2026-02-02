---
type: "THEORY"
title: "Animation Lifecycle and Status"
---


**AnimationStatus** tells you where the animation is in its lifecycle:

| Status | Meaning |
|--------|--------|
| `dismissed` | At beginning (value = 0.0) |
| `forward` | Animating toward end |
| `completed` | At end (value = 1.0) |
| `reverse` | Animating toward beginning |

**Listening to Status Changes:**
```dart
_controller.addStatusListener((status) {
  if (status == AnimationStatus.completed) {
    // Animation finished going forward
  } else if (status == AnimationStatus.dismissed) {
    // Animation finished reversing or was reset
  }
});
```

**Listening to Value Changes:**
```dart
_controller.addListener(() {
  print('Current value: ${_controller.value}');
  // Called every frame during animation
});
```

**Common Patterns:**

```dart
// Ping-pong: reverse when complete
_controller.addStatusListener((status) {
  if (status == AnimationStatus.completed) {
    _controller.reverse();
  } else if (status == AnimationStatus.dismissed) {
    _controller.forward();
  }
});

// One-shot: dispose after animation
_controller.addStatusListener((status) {
  if (status == AnimationStatus.completed) {
    Navigator.of(context).pop();
  }
});
```

