---
type: "THEORY"
title: "Tweens and Curves"
---


**Tween** (short for "in-between") maps the 0.0-1.0 range of AnimationController to any value range you need.

**Common Tween Types:**

| Tween | Description | Example |
|-------|-------------|--------|
| `Tween<double>` | Numeric values | Scale 1.0 to 2.0 |
| `ColorTween` | Color transitions | Red to blue |
| `SizeTween` | Size changes | Size(100,100) to Size(200,200) |
| `RectTween` | Rectangle animations | Position and size |
| `IntTween` | Integer values | Counter 0 to 100 |
| `AlignmentTween` | Alignment changes | topLeft to bottomRight |
| `BorderRadiusTween` | Corner radius | Circular to rounded |

**Creating a Tween:**
```dart
final sizeTween = Tween<double>(begin: 100.0, end: 200.0);
final currentSize = sizeTween.evaluate(_controller); // Returns value between 100-200
```

**Curves** modify the rate of change. Instead of linear progression, curves add easing, bouncing, or elastic effects.

**CurvedAnimation** applies a curve to your animation:
```dart
final curvedAnimation = CurvedAnimation(
  parent: _controller,
  curve: Curves.easeInOut,
);
```

