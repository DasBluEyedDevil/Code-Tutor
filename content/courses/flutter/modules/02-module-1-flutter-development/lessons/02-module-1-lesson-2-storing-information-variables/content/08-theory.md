---
type: "THEORY"
title: "Explicit Types (Being More Specific)"
---


Instead of using `var` (where Dart guesses the type), you can be explicit:


**When to use `var` vs explicit types?**
- `var`: When the type is obvious from the value
- Explicit (`String`, `int`, etc.): When you want to be extra clear

Both work! It's mostly personal preference.



```dart
void main() {
  String name = 'Sarah';      // This box only holds text
  int age = 25;               // This box only holds integers
  double price = 19.99;       // This box only holds decimals
  bool isActive = true;       // This box only holds true/false
}
```
