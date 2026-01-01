---
type: "THEORY"
title: "Comparison Operators"
---


These are the symbols we use to compare things:

| Operator | Meaning | Example |
|----------|---------|---------|
| `==` | Equal to | `age == 18` |
| `!=` | Not equal to | `age != 18` |
| `>` | Greater than | `age > 18` |
| `<` | Less than | `age < 18` |
| `>=` | Greater than or equal | `age >= 18` |
| `<=` | Less than or equal | `age <= 18` |

**Common Mistake**: Using `=` instead of `==`
- `=` means "assign a value" (putting something in a box)
- `==` means "compare for equality" (checking if two things are equal)




```dart
var age = 18;      // ✅ Assignment (setting age to 18)
if (age == 18) {   // ✅ Comparison (checking if age equals 18)
  print('Age is 18');
}
```
