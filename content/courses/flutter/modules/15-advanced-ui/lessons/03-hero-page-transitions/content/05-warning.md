---
type: "WARNING"
title: "Hero Text Rendering Issues"
---


**Problem:** Text in Hero widgets can look broken during flight.

**Cause:** The Hero animation removes the widget from the normal widget tree, which can cause `DefaultTextStyle` inheritance to break.

**Solution:** Always wrap Text widgets in a `Material` widget:

```dart
// WRONG - Text may render incorrectly during flight
Hero(
  tag: 'title',
  child: Text('Product Name'),
)

// CORRECT - Material provides proper text styling
Hero(
  tag: 'title',
  child: Material(
    color: Colors.transparent,
    child: Text('Product Name'),
  ),
)
```

**Also watch for:**
- Container backgrounds disappearing (wrap in Material)
- BoxDecoration not animating (use explicit color properties)
- Clipped content during flight (adjust ClipBehavior)

