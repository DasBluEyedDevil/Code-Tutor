---
type: "EXAMPLE"
title: "Real-World Examples"
---


### Temperature Converter


### Discount Calculator


### Password Validator




```dart
bool isPasswordStrong(String password) {
  if (password.length < 8) {
    return false;
  }
  if (!password.contains(RegExp(r'[0-9]'))) {
    return false;  // Must have a number
  }
  return true;
}

void main() {
  print(isPasswordStrong('weak'));          // Output: false
  print(isPasswordStrong('strong123'));     // Output: true
}
```
