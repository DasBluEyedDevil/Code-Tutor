---
type: "THEORY"
title: "Arrow Functions (Shorthand)"
---


For simple, one-line functions:

**Long way**:

**Short way** (arrow function):

**More examples**:




```dart
String shout(String text) => text.toUpperCase();
bool isEven(int number) => number % 2 == 0;
int square(int x) => x * x;

void main() {
  print(shout('hello'));    // Output: HELLO
  print(isEven(4));         // Output: true
  print(square(5));         // Output: 25
}
```
