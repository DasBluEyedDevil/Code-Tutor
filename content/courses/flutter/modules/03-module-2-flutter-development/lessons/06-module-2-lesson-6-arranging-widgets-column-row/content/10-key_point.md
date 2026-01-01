---
type: "KEY_POINT"
title: "Dart 3.10+ Dot Shorthands"
---


**New in Dart 3.10:** You can now use dot shorthands to write cleaner code! When the type is known, skip the type name:

**Before (verbose):**
```dart
Column(
  mainAxisAlignment: MainAxisAlignment.center,
  crossAxisAlignment: CrossAxisAlignment.start,
)
```

**After (with dot shorthands):**
```dart
Column(
  mainAxisAlignment: .center,
  crossAxisAlignment: .start,
)
```

The compiler knows `mainAxisAlignment` expects a `MainAxisAlignment`, so you can just write `.center`!

**Works with:**
- Enums: `Colors.blue` → `.blue`
- Alignment: `Alignment.center` → `.center`
- Any typed parameter where the type is unambiguous

**Note:** Dot shorthands are enabled by default in Dart 3.10+. Use them to reduce boilerplate!

