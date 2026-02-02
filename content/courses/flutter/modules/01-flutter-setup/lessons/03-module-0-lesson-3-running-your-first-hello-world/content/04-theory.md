---
type: "THEORY"
title: "📁 What's the test/ Folder For?"
---


You might be wondering about that `test/` folder. This is where you'll put **Widget Tests** - automated checks that verify your app works correctly!

### Widget Testing: A Quick Preview

Think of it like having a robot that clicks buttons and checks results for you:

```dart
// test/counter_test.dart
testWidgets('Counter increments when + is tapped', (tester) async {
  // 1. BUILD: Create the widget
  await tester.pumpWidget(MyApp());
  
  // 2. FIND: Locate the counter text
  expect(find.text('0'), findsOneWidget);  // Starts at 0
  
  // 3. ACT: Tap the + button
  await tester.tap(find.byIcon(Icons.add));
  await tester.pump();  // Rebuild after state change
  
  // 4. VERIFY: Check the counter increased
  expect(find.text('1'), findsOneWidget);  // Now shows 1!
});
```

**Key concepts** (we'll learn these in Module 10):
- `testWidgets()` - Runs a widget test
- `tester.pumpWidget()` - Builds your widget
- `find.text('0')` - Locates text on screen
- `tester.tap()` - Simulates a tap
- `expect()` - Checks if something is true

**Run tests with:** `flutter test`

**Don't worry!** You don't need to write tests now. Just know that the `test/` folder exists for automated quality checks. We cover testing in depth in Module 10!

