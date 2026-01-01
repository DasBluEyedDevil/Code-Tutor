---
type: "THEORY"
title: "Section 3: Implementation - Models and Tests"
---


### Task Model

```dart
// lib/features/tasks/domain/task.dart
import 'package:freezed_annotation/freezed_annotation.dart';

part 'task.freezed.dart';
part 'task.g.dart';

enum TaskPriority { low, medium, high }

enum TaskStatus { pending, inProgress, completed }

@freezed
class Task with _$Task {
  const factory Task({
    required String id,
    required String title,
    required String description,
    required TaskPriority priority,
    required TaskStatus status,
    required DateTime createdAt,
    DateTime? dueDate,
    DateTime? completedAt,
    @Default([]) List<String> tags,
  }) = _Task;

  factory Task.fromJson(Map<String, dynamic> json) => _$TaskFromJson(json);
}

// Extension for business logic
extension TaskExtension on Task {
  bool get isOverdue {
    if (dueDate == null) return false;
    if (status == TaskStatus.completed) return false;
    return DateTime.now().isAfter(dueDate!);
  }

  bool get isHighPriority => priority == TaskPriority.high;

  Task markAsCompleted() => copyWith(
        status: TaskStatus.completed,
        completedAt: DateTime.now(),
      );

  Task updatePriority(TaskPriority newPriority) => copyWith(
        priority: newPriority,
      );
}
```

### Unit Tests for Task Model

```dart
// test/features/tasks/domain/task_test.dart
import 'package:flutter_test/flutter_test.dart';
import 'package:your_app/features/tasks/domain/task.dart';

void main() {
  group('Task', () {
    late Task sampleTask;

    setUp(() {
      sampleTask = Task(
        id: '1',
        title: 'Test Task',
        description: 'A test task description',
        priority: TaskPriority.medium,
        status: TaskStatus.pending,
        createdAt: DateTime(2024, 1, 1),
        dueDate: DateTime(2024, 12, 31),
        tags: ['work', 'urgent'],
      );
    });

    group('isOverdue', () {
      test('returns false when no due date', () {
        final task = sampleTask.copyWith(dueDate: null);
        expect(task.isOverdue, isFalse);
      });

      test('returns false when completed', () {
        final task = sampleTask.copyWith(
          status: TaskStatus.completed,
          dueDate: DateTime(2020, 1, 1), // Past date
        );
        expect(task.isOverdue, isFalse);
      });

      test('returns true when past due date and not completed', () {
        final task = sampleTask.copyWith(
          dueDate: DateTime(2020, 1, 1), // Past date
          status: TaskStatus.pending,
        );
        expect(task.isOverdue, isTrue);
      });
    });

    group('markAsCompleted', () {
      test('sets status to completed', () {
        final completed = sampleTask.markAsCompleted();
        expect(completed.status, TaskStatus.completed);
      });

      test('sets completedAt timestamp', () {
        final completed = sampleTask.markAsCompleted();
        expect(completed.completedAt, isNotNull);
      });
    });

    group('isHighPriority', () {
      test('returns true for high priority', () {
        final task = sampleTask.copyWith(priority: TaskPriority.high);
        expect(task.isHighPriority, isTrue);
      });

      test('returns false for low priority', () {
        final task = sampleTask.copyWith(priority: TaskPriority.low);
        expect(task.isHighPriority, isFalse);
      });
    });

    group('JSON serialization', () {
      test('can be serialized and deserialized', () {
        final json = sampleTask.toJson();
        final restored = Task.fromJson(json);
        expect(restored, sampleTask);
      });
    });
  });
}
```

**Run tests:**

```bash
flutter test test/features/tasks/domain/task_test.dart
```

**Expected output:**

```dart
00:02 +22: All tests passed!
```
