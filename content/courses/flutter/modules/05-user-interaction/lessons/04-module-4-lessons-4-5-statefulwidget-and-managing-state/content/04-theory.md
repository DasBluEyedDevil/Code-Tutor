---
type: "THEORY"
title: "The setState() Magic"
---



**What setState does:**
1. Runs the code inside  
2. Marks widget as "dirty"
3. Schedules a rebuild
4. Calls `build()` again with new values



```dart
setState(() {
  // Make changes here
  counter++;
  name = 'New Name';
  isVisible = !isVisible;
});
```
