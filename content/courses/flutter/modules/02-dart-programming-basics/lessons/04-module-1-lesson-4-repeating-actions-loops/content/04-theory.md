---
type: "THEORY"
title: "Breaking Down the \"for\" Loop"
---



**The Three Parts**:

1. **`var i = 1`** - **Start**: Create a counter starting at 1
2. **`i <= 3`** - **Condition**: Keep looping while i is ≤ 3
3. **`i++`** - **Increment**: Add 1 to i after each loop

**`i++` is shorthand for `i = i + 1`**

**What Happens**:
- First time: i = 1, prints "Count: 1", then i becomes 2
- Second time: i = 2, prints "Count: 2", then i becomes 3
- Third time: i = 3, prints "Count: 3", then i becomes 4
- Fourth time: i = 4, but 4 is not ≤ 3, so STOP



```dart
for (var i = 1; i <= 3; i++) {
  print('Count: $i');
}
```
