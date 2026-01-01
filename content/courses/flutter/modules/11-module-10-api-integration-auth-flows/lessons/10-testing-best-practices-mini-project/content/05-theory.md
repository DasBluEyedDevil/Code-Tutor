---
type: "THEORY"
title: "Section 4: Repository and Tests"
---


### Task Repository


### Unit Tests for Repository (with Mocking)




```dart
// test/repositories/task_repository_test.dart
import 'package:flutter_test/flutter_test.dart';
import 'package:task_master_pro/models/task.dart';
import 'package:task_master_pro/repositories/task_repository.dart';
import 'package:hive_flutter/hive_flutter.dart';

void main() {
  group('HiveTaskRepository', () {
    late HiveTaskRepository repository;

    setUpAll(() async {
      // Initialize Hive for testing (in-memory)
      await Hive.initFlutter();
      Hive.registerAdapter(TaskAdapter());
    });

    setUp(() async {
      repository = HiveTaskRepository();
      await repository.init();
    });

    tearDown() async {
      // Clear all tasks after each test
      final box = await Hive.openBox<Task>(HiveTaskRepository.boxName);
      await box.clear();
    });

    tearDownAll() async {
      await Hive.close();
    });

    test('getTasks returns empty list initially', () async {
      // Act
      final tasks = await repository.getTasks();

      // Assert
      expect(tasks, isEmpty);
    });

    test('addTask adds task to repository', () async {
      // Arrange
      final task = Task.create(
        title: 'Test Task',
        description: 'Test Desc',
        dueDate: DateTime.now(),
      );

      // Act
      await repository.addTask(task);
      final tasks = await repository.getTasks();

      // Assert
      expect(tasks.length, 1);
      expect(tasks.first.id, task.id);
      expect(tasks.first.title, task.title);
    });

    test('updateTask updates existing task', () async {
      // Arrange
      final task = Task.create(
        title: 'Original',
        description: 'Desc',
        dueDate: DateTime.now(),
      );
      await repository.addTask(task);

      // Act
      final updated = task.copyWith(title: 'Updated');
      await repository.updateTask(updated);
      final tasks = await repository.getTasks();

      // Assert
      expect(tasks.length, 1);
      expect(tasks.first.title, 'Updated');
    });

    test('deleteTask removes task from repository', () async {
      // Arrange
      final task = Task.create(
        title: 'Task to Delete',
        description: 'Desc',
        dueDate: DateTime.now(),
      );
      await repository.addTask(task);

      // Act
      await repository.deleteTask(task.id);
      final tasks = await repository.getTasks();

      // Assert
      expect(tasks, isEmpty);
    });

    test('deleteAllCompletedTasks removes only completed tasks', () async {
      // Arrange
      final task1 = Task.create(
        title: 'Task 1',
        description: 'Desc',
        dueDate: DateTime.now(),
      ).copyWith(isCompleted: true);

      final task2 = Task.create(
        title: 'Task 2',
        description: 'Desc',
        dueDate: DateTime.now(),
      ); // Not completed

      final task3 = Task.create(
        title: 'Task 3',
        description: 'Desc',
        dueDate: DateTime.now(),
      ).copyWith(isCompleted: true);

      await repository.addTask(task1);
      await repository.addTask(task2);
      await repository.addTask(task3);

      // Act
      await repository.deleteAllCompletedTasks();
      final tasks = await repository.getTasks();

      // Assert
      expect(tasks.length, 1);
      expect(tasks.first.id, task2.id); // Only incomplete task remains
    });
  });
}
```
