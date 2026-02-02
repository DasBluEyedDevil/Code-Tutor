---
type: "THEORY"
title: "Why Do We Need Variables?"
---


Look at this code:


What if we want to change the name from "Sarah" to "John"? We'd have to change it in 3 places!

Now look at this:


Now if we want to use a different name, we only change it in **one place**! That's the power of variables.



```dart
void main() {
  var name = 'Sarah';
  print('Hello, $name!');
  print('Welcome back, $name!');
  print('$name, you have 3 new messages.');
}
```
