---
type: "THEORY"
title: "Two Types of Widgets"
---


### 1. StatelessWidget - Doesn't Change

Think of a street sign - it always shows the same information.


**When to use**: Static content that doesn't change.

### 2. StatefulWidget - Can Change

Think of a digital clock - it updates every second.


**When to use**: Dynamic content that changes (we'll cover this in detail later).



```dart
class Counter extends StatefulWidget {
  @override
  _CounterState createState() => _CounterState();
}

class _CounterState extends State<Counter> {
  int count = 0;

  @override
  Widget build(BuildContext context) {
    return Text('Count: $count');
  }
}
```
