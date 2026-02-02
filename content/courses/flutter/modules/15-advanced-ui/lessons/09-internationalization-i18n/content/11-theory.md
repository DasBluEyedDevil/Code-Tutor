---
type: "THEORY"
title: "RTL Support - Right-to-Left Languages"
---


**RTL Languages**

Some languages read right-to-left (RTL):
- Arabic (ar)
- Hebrew (he)
- Persian/Farsi (fa)
- Urdu (ur)

**Flutter's RTL Support:**

Flutter automatically handles RTL when you use:
- `flutter_localizations` delegates
- Logical positioning (start/end instead of left/right)

**Automatic Behavior:**

| LTR Layout | RTL Layout |
|------------|------------|
| Text flows left to right | Text flows right to left |
| Start = Left | Start = Right |
| End = Right | End = Left |
| Icons/arrows point right | Icons/arrows point left |

**Key Classes:**

- `TextDirection` - LTR or RTL
- `Directionality` - Widget that sets text direction for subtree
- `Localizations.localeOf(context)` - Current locale

**Checking Text Direction:**

```dart
final direction = Directionality.of(context);
if (direction == TextDirection.rtl) {
  // RTL layout adjustments
}
```

**EdgeInsetsDirectional vs EdgeInsets:**

```dart
// DON'T: Fixed left/right (wrong in RTL)
padding: EdgeInsets.only(left: 16, right: 8);

// DO: Logical start/end (correct in both)
padding: EdgeInsetsDirectional.only(start: 16, end: 8);
```

