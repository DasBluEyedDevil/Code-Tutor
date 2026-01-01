---
type: "KEY_POINT"
title: "Testing Notifier Interactions"
---


Test that widget interactions trigger the right notifier methods:

```dart
testWidgets('add button calls addTodo', (tester) async {
  final container = ProviderContainer();

  await tester.pumpWidget(
    UncontrolledProviderScope(
      container: container,
      child: const MaterialApp(home: TodoScreen()),
    ),
  );

  await tester.enterText(find.byType(TextField), 'New Todo');
  await tester.tap(find.byIcon(Icons.add));
  await tester.pump();

  // Verify state changed
  final todos = container.read(todoProvider);
  expect(todos, hasLength(1));
  expect(todos.first.title, 'New Todo');

  container.dispose();
});
```

