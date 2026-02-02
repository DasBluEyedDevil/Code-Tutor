---
type: "THEORY"
title: "Replacing Current Screen"
---



**Use case**: Login → Home (don't want back button to go to login)



```dart
// Go to new screen and remove current from stack
Navigator.pushReplacement(
  context,
  MaterialPageRoute(builder: (context) => HomeScreen()),
);
```
