---
type: "THEORY"
title: "Testing Accessibility - Flutter's Built-in Tools"
---


**Flutter's Accessibility Testing Tools**

Flutter provides several tools to test and debug accessibility:

**1. SemanticsDebugger**

Visualizes the semantics tree:

```dart
runApp(
  SemanticsDebugger(
    child: MyApp(),
  ),
);
```

Shows:
- All semantic nodes
- Their labels and properties
- Bounding boxes

**2. showSemanticsDebugger Flag**

```dart
MaterialApp(
  showSemanticsDebugger: true, // Enable in debug
  home: MyHomePage(),
)
```

**3. Flutter DevTools**

- Widget Inspector shows semantics properties
- Accessibility tab (in newer versions)
- Layout Explorer for touch targets

**4. Integration Tests**

```dart
testWidgets('button is accessible', (tester) async {
  await tester.pumpWidget(MyApp());
  
  // Find by semantics label
  expect(
    find.bySemanticsLabel('Submit'),
    findsOneWidget,
  );
  
  // Verify semantics properties
  final semantics = tester.getSemantics(
    find.byType(ElevatedButton),
  );
  expect(semantics.hasFlag(SemanticsFlag.isButton), true);
});
```

**5. Accessibility Scanner (Android)**

- Google's Accessibility Scanner app
- Analyzes your app on device
- Reports touch target sizes, contrast issues

**6. VoiceOver/TalkBack Testing**

- Enable screen reader on test device
- Navigate through your app
- Listen for clear, helpful announcements

