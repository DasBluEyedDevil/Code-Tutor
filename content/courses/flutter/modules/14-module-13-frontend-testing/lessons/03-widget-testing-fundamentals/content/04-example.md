---
type: "EXAMPLE"
title: "User Interactions"
---




```dart
testWidgets('login form validation', (tester) async {
  await tester.pumpWidget(
    const MaterialApp(home: LoginScreen()),
  );

  // Enter text
  await tester.enterText(
    find.byKey(const Key('email-field')),
    'user@example.com',
  );

  await tester.enterText(
    find.byKey(const Key('password-field')),
    'password123',
  );

  // Tap button
  await tester.tap(find.text('Login'));

  // Wait for animations/async
  await tester.pumpAndSettle();

  // Verify navigation or state change
  expect(find.byType(HomeScreen), findsOneWidget);
});

// Other interactions:
await tester.longPress(finder);
await tester.drag(finder, const Offset(0, -300));
await tester.fling(finder, const Offset(0, -500), 1000);
```
