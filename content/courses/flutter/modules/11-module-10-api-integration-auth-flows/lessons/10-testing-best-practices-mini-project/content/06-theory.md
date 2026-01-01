---
type: "THEORY"
title: "Section 5: Widget Tests"
---


### Task Item Widget


### Widget Test for Task Item




```dart
// test/widgets/task_item_test.dart
import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:task_master_pro/models/task.dart';
import 'package:task_master_pro/widgets/task_item.dart';

void main() {
  group('TaskItem Widget', () {
    late Task testTask;

    setUp(() {
      testTask = Task(
        id: '123',
        title: 'Test Task',
        description: 'Test Description',
        dueDate: DateTime(2025, 12, 31),
      );
    });

    Widget createWidgetUnderTest(Task task) {
      return MaterialApp(
        home: Scaffold(
          body: TaskItem(
            task: task,
            onTap: () {},
            onToggleComplete: (_) {},
            onDelete: () {},
          ),
        ),
      );
    }

    testWidgets('displays task title and description', (tester) async {
      // Act
      await tester.pumpWidget(createWidgetUnderTest(testTask));

      // Assert
      expect(find.text('Test Task'), findsOneWidget);
      expect(find.text('Test Description'), findsOneWidget);
    });

    testWidgets('displays formatted due date', (tester) async {
      // Act
      await tester.pumpWidget(createWidgetUnderTest(testTask));

      // Assert
      expect(find.textContaining('Due: Dec 31, 2025'), findsOneWidget);
    });

    testWidgets('checkbox reflects completion status', (tester) async {
      // Arrange
      final completedTask = testTask.copyWith(isCompleted: true);

      // Act
      await tester.pumpWidget(createWidgetUnderTest(completedTask));
      final checkbox = tester.widget<Checkbox>(
        find.byKey(Key('checkbox_${completedTask.id}')),
      );

      // Assert
      expect(checkbox.value, true);
    });

    testWidgets('completed task has strikethrough text', (tester) async {
      // Arrange
      final completedTask = testTask.copyWith(isCompleted: true);

      // Act
      await tester.pumpWidget(createWidgetUnderTest(completedTask));
      final textWidget = tester.widget<Text>(find.text('Test Task'));

      // Assert
      expect(
        textWidget.style?.decoration,
        TextDecoration.lineThrough,
      );
    });

    testWidgets('tapping checkbox calls onToggleComplete', (tester) async {
      // Arrange
      bool toggleCalled = false;
      bool? toggledValue;

      final widget = MaterialApp(
        home: Scaffold(
          body: TaskItem(
            task: testTask,
            onTap: () {},
            onToggleComplete: (value) {
              toggleCalled = true;
              toggledValue = value;
            },
            onDelete: () {},
          ),
        ),
      );

      // Act
      await tester.pumpWidget(widget);
      await tester.tap(find.byKey(Key('checkbox_${testTask.id}')));

      // Assert
      expect(toggleCalled, true);
      expect(toggledValue, true); // Toggled from false to true
    });

    testWidgets('tapping delete button calls onDelete', (tester) async {
      // Arrange
      bool deleteCalled = false;

      final widget = MaterialApp(
        home: Scaffold(
          body: TaskItem(
            task: testTask,
            onTap: () {},
            onToggleComplete: (_) {},
            onDelete: () {
              deleteCalled = true;
            },
          ),
        ),
      );

      // Act
      await tester.pumpWidget(widget);
      await tester.tap(find.byKey(Key('delete_${testTask.id}')));

      // Assert
      expect(deleteCalled, true);
    });

    testWidgets('overdue task shows due date in red', (tester) async {
      // Arrange
      final overdueTask = testTask.copyWith(
        dueDate: DateTime.now().subtract(const Duration(days: 1)),
      );

      // Act
      await tester.pumpWidget(createWidgetUnderTest(overdueTask));

      // Find the due date text
      final dueDateFinder = find.textContaining('Due:');
      final textWidget = tester.widget<Text>(dueDateFinder);

      // Assert
      expect(textWidget.style?.color, Colors.red);
      expect(textWidget.style?.fontWeight, FontWeight.bold);
    });
  });
}
```
