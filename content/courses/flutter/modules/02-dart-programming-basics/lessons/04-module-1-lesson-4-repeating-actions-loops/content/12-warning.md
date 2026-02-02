---
type: "WARNING"
title: "Loop Pitfalls to Avoid"
---


**The Infinite Loop Trap**

The #1 mistake in loops! If your condition never becomes false, the loop runs forever:

❌ Infinite loop - i never changes:
```dart
var i = 0;
while (i < 5) {
  print(i);  // Prints 0 forever!
  // Forgot i++!
}
```

✅ Fixed - i increments:
```dart
var i = 0;
while (i < 5) {
  print(i);
  i++;  // Now it will stop at 5
}
```

**Off-By-One Errors**

These are subtle and common:
- `for (var i = 0; i < 5; i++)` → runs 5 times (0,1,2,3,4)
- `for (var i = 0; i <= 5; i++)` → runs 6 times (0,1,2,3,4,5)
- `for (var i = 1; i <= 5; i++)` → runs 5 times (1,2,3,4,5)

**Modifying Collections While Iterating**

❌ Don't add/remove items while looping through a list:
```dart
for (var item in items) {
  items.remove(item);  // CRASH or unexpected behavior!
}
```

✅ Create a copy or use removeWhere:
```dart
items.removeWhere((item) => shouldRemove(item));
```

