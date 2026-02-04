---
type: WARNING
---

**`pumpAndSettle()` hangs indefinitely on widgets with repeating animations.** This method waits until all animations complete and no more frames are scheduled. Widgets like `CircularProgressIndicator`, `AnimatedContainer` in a loop, or `Shimmer` effects never "settle," causing your test to time out.

```dart
// WRONG - hangs forever if a spinner is visible
await tester.pumpWidget(MyScreenWithLoadingSpinner());
await tester.pumpAndSettle(); // Never returns

// RIGHT - pump a specific duration instead
await tester.pumpWidget(MyScreenWithLoadingSpinner());
await tester.pump(const Duration(seconds: 1));
```

Use `pump()` with an explicit duration when testing screens that display loading indicators, animated placeholders, or any continuously running animation. Reserve `pumpAndSettle()` for screens where all animations are finite and expected to complete.
