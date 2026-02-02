---
type: "THEORY"
title: "Staggered Animations with Intervals"
---


**Staggered animations** create sequential or overlapping effects where different elements animate at different times.

**Interval** specifies when during the overall animation a specific part should run:

```dart
// Animation runs from 0% to 50% of total duration
const Interval(0.0, 0.5)

// Animation runs from 25% to 75% of total duration
const Interval(0.25, 0.75)

// Animation runs from 50% to 100% of total duration
const Interval(0.5, 1.0)
```

**Example Timeline:**
With a 1000ms total duration:
- Interval(0.0, 0.3) runs from 0ms to 300ms
- Interval(0.2, 0.6) runs from 200ms to 600ms (overlaps!)
- Interval(0.5, 1.0) runs from 500ms to 1000ms

**Stagger Pattern:**
For sequential animations on a list of items:
```dart
for (int i = 0; i < items.length; i++) {
  final start = i * 0.1;  // Each item starts 10% later
  final end = start + 0.3; // Each animation takes 30%
  // Use Interval(start, end.clamp(0, 1))
}
```

