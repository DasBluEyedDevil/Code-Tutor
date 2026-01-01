---
type: "EXAMPLE"
title: "Testing Loading and Error States"
---




```dart
testWidgets('shows loading indicator', (tester) async {
  await tester.pumpWidget(
    ProviderScope(
      overrides: [
        usersProvider.overrideWith((ref) async {
          // Delay to keep loading state
          await Future.delayed(const Duration(seconds: 10));
          return [];
        }),
      ],
      child: const MaterialApp(home: UsersScreen()),
    ),
  );

  // Don't settle - check loading state
  await tester.pump();

  expect(find.byType(CircularProgressIndicator), findsOneWidget);
});

testWidgets('shows error message on failure', (tester) async {
  await tester.pumpWidget(
    ProviderScope(
      overrides: [
        usersProvider.overrideWith((ref) async {
          throw Exception('Network error');
        }),
      ],
      child: const MaterialApp(home: UsersScreen()),
    ),
  );

  await tester.pumpAndSettle();

  expect(find.textContaining('error'), findsOneWidget);
  expect(find.byIcon(Icons.refresh), findsOneWidget);
});
```
