---
type: "THEORY"
title: "Functions with Parameters - Making Them Flexible"
---


What if you want to greet different people?

**Without parameters** (rigid):

**With parameters** (flexible):

**Conceptual Explanation**:
Parameters are like **placeholders** or **blank spaces** in your recipe that you fill in when you use it.



```dart
void greet(String name) {
  print('Hello, $name!');
}

void main() {
  greet('Alice');  // Output: Hello, Alice!
  greet('Bob');    // Output: Hello, Bob!
  greet('Charlie'); // Output: Hello, Charlie!
}
```
