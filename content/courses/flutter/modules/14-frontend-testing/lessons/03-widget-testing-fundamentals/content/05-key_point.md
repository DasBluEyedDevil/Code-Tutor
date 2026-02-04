---
type: KEY_POINT
---

- `testWidgets()` sets up the Flutter test environment and provides a `WidgetTester` for interacting with widgets
- Wrap widgets in `MaterialApp` inside `tester.pumpWidget()` to provide theme, navigation, and media query context
- Use finders (`find.text()`, `find.byType()`, `find.byKey()`) to locate widgets and matchers (`findsOneWidget`, `findsNothing`) to verify them
- `await tester.tap(find.byType(ElevatedButton))` simulates user taps; call `await tester.pump()` after to process the resulting state change
- `pumpAndSettle()` waits for all animations to complete -- use `pump(Duration)` instead when animations run indefinitely
