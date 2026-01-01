---
type: "THEORY"
title: "Function Scope - Variable Visibility"
---


Variables inside a function can't be seen outside:


**Global vs Local**:




```dart
var globalVar = 'I am global';

void myFunction() {
  var localVar = 'I am local';
  print(globalVar);  // ✅ Can access global
  print(localVar);   // ✅ Can access local
}

void main() {
  print(globalVar);  // ✅ Can access global
  // print(localVar);  // ❌ Error: localVar only exists inside myFunction
}
```
