---
type: "THEORY"
title: "The \"for\" Loop - Counting Repetitions"
---


When you know **exactly how many times** to repeat something, use a `for` loop.

**Conceptual Explanation**:
Think of it like counting:
- **Start** at 1
- **Keep going** while less than or equal to 5
- **Count up** by 1 each time

**The Pattern**:

```dart
for (var counter = start; counter <= end; counter++) {
  // Code to repeat
}
```

- `var counter = start`: Initialize a counter variable
- `counter <= end`: Continue while this condition is true
- `counter++`: Increase counter by 1 after each loop

**Real Example**:

```dart
void main() {
  for (var i = 1; i <= 3; i++) {
    print('This is repetition number $i');
  }
}
```

**Output**:



```dart
This is repetition number 1
This is repetition number 2
This is repetition number 3
```
