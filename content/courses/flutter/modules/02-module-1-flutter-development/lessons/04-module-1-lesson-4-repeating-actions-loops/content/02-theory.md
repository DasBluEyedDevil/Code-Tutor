---
type: "THEORY"
title: "Why Do We Need Loops?"
---


Look at this code:

```dart
// Without a loop - repetitive and hard to maintain!
void main() {
  print('Welcome user 1!');
  print('Welcome user 2!');
  print('Welcome user 3!');
  print('Welcome user 4!');
  print('Welcome user 5!');
}
```

What if you have 100 users? Or 1000? You can't write 1000 lines!

With a loop:

```dart
// With a loop - clean and scalable!
void main() {
  for (var i = 1; i <= 5; i++) {
    print('Welcome user $i!');
  }
}
```

**Output**:

Same result, way less code!



```dart
Welcome user 1!
Welcome user 2!
Welcome user 3!
Welcome user 4!
Welcome user 5!
```
