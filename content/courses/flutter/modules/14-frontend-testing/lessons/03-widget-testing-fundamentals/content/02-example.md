---
type: "EXAMPLE"
title: "Common Finders"
---




```dart
// Find by text content
find.text('Submit')
find.textContaining('Hello')

// Find by widget type
find.byType(ElevatedButton)
find.byType(TextField)

// Find by key
find.byKey(const Key('login-button'))

// Find by icon
find.byIcon(Icons.add)

// Find by semantic label (accessibility)
find.bySemanticsLabel('Close dialog')

// Find descendants/ancestors
find.descendant(
  of: find.byType(Card),
  matching: find.text('Title'),
)

// Find by predicate
find.byWidgetPredicate(
  (widget) => widget is Text && widget.data!.startsWith('Error'),
)
```
