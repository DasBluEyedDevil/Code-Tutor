---
type: KEY_POINT
---

- `GestureDetector` wraps any widget to detect taps, long presses, drags, and swipes -- it adds no visual feedback
- `InkWell` provides the Material ripple effect on tap -- prefer it over GestureDetector for Material Design apps
- `Dismissible` makes list items swipeable for delete or archive actions; always provide a `key` and `onDismissed` callback
- `onTap`, `onLongPress`, `onDoubleTap`, and `onPanUpdate` handle distinct gestures on the same widget
- Combine gestures with `setState()` to update UI in response to user interactions (e.g., drag to reposition a widget)
