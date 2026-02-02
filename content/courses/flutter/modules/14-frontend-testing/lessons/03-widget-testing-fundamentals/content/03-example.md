---
type: "EXAMPLE"
title: "Common Matchers"
---




```dart
// Widget existence
expect(find.text('Hello'), findsOneWidget);
expect(find.text('Hello'), findsNothing);
expect(find.byType(ListTile), findsNWidgets(3));
expect(find.byType(Card), findsAtLeastNWidgets(1));

// Widget properties (using widgetWithText finder)
final textWidget = tester.widget<Text>(find.text('Hello'));
expect(textWidget.style?.fontSize, 24);

// Check if widget is enabled
final button = tester.widget<ElevatedButton>(find.byType(ElevatedButton));
expect(button.onPressed, isNotNull); // Button is enabled
```
