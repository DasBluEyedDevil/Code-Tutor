---
type: "THEORY"
title: "The \"runApp()\" Function - Showing Something on Screen"
---


Now look at what's *inside* the `main()` function:


**Conceptual Explanation**:
- `runApp()` is a special Flutter function that says "Put this on the screen"
- `MyApp()` is what we want to show
- Together they mean: "Take MyApp and display it"

**The Technical Term**: `runApp()` is the function that tells Flutter to inflate your app's widget tree and attach it to the screen.

(Don't worry about "widget tree" yet - we'll get there!)



```dart
runApp(MyApp());
```
