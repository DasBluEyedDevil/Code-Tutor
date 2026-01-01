---
type: "EXAMPLE"
title: "Testing with ProviderScope Overrides"
---




```dart
testWidgets('displays user list', (tester) async {
  await tester.pumpWidget(
    ProviderScope(
      overrides: [
        // Override the usersProvider with mock data
        usersProvider.overrideWith((ref) async {
          return [
            User(id: '1', name: 'Alice'),
            User(id: '2', name: 'Bob'),
          ];
        }),
      ],
      child: const MaterialApp(home: UsersScreen()),
    ),
  );

  // Wait for async provider to resolve
  await tester.pumpAndSettle();

  // Verify users are displayed
  expect(find.text('Alice'), findsOneWidget);
  expect(find.text('Bob'), findsOneWidget);
});
```
