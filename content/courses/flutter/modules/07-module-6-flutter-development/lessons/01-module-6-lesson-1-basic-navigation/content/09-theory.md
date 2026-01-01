---
type: "THEORY"
title: "Removing All Previous Screens"
---



**Use case**: Logout → Login (clear all app screens)



```dart
// Clear entire stack and go to new screen
Navigator.pushAndRemoveUntil(
  context,
  MaterialPageRoute(builder: (context) => HomeScreen()),
  (route) => false,  // Remove all previous routes
);
```
