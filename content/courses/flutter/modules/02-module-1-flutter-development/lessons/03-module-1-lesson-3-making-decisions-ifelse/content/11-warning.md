---
type: "WARNING"
title: "if/else Pitfalls"
---


**The = vs == Trap**

This is the #1 mistake with conditionals:
- `=` means ASSIGN (put value into variable)
- `==` means COMPARE (check if equal)

❌ `if (age = 18)` - This ASSIGNS 18 to age, not compare!
✅ `if (age == 18)` - This COMPARES age to 18

**Order Matters with else if**

Conditions are checked top-to-bottom. First match wins!

❌ Wrong order:
```dart
if (score >= 60) { print('Pass'); }
else if (score >= 90) { print('A'); }  // Never reached!
```

✅ Correct order:
```dart
if (score >= 90) { print('A'); }  // Check highest first
else if (score >= 60) { print('Pass'); }
```

**Always Use Braces**

Even for single lines, use `{ }` to avoid bugs:
```dart
// Dangerous - only first line is conditional
if (isLoggedIn)
  print('Welcome');
  showDashboard();  // This ALWAYS runs!

// Safe - both lines are conditional
if (isLoggedIn) {
  print('Welcome');
  showDashboard();
}
```

