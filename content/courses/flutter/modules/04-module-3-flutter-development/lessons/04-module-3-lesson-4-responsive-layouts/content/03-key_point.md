---
type: "KEY_POINT"
title: "Efficient MediaQuery Usage (Flutter 3.10+)"
---


**Old pattern (causes unnecessary rebuilds):**
```dart
final size = MediaQuery.of(context).size;
```

**New pattern (more efficient):**
```dart
final size = MediaQuery.sizeOf(context);
final padding = MediaQuery.paddingOf(context);
final viewInsets = MediaQuery.viewInsetsOf(context);
```

**Why?** The `.of(context)` version rebuilds when ANY MediaQuery property changes. The specific methods only rebuild when that property changes.

