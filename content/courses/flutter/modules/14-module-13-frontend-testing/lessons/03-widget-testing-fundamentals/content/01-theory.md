---
type: "THEORY"
title: "What Are Widget Tests?"
---


Widget tests verify that your UI components render correctly and respond to user interactions.

**Key Concepts:**
- **testWidgets()** - The main function for widget tests
- **WidgetTester** - Provides methods to interact with widgets
- **Finders** - Locate widgets in the tree
- **Matchers** - Verify widget properties

**Anatomy of a Widget Test:**
```dart
testWidgets('description', (tester) async {
  // 1. Build the widget
  await tester.pumpWidget(MyWidget());

  // 2. Find widgets
  final button = find.byType(ElevatedButton);

  // 3. Interact
  await tester.tap(button);
  await tester.pump();

  // 4. Assert
  expect(find.text('Tapped!'), findsOneWidget);
});
```

