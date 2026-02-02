---
type: "EXAMPLE"
title: "Complete CRUD Operations Example"
---


Here is a complete example showing Create, Read, Update, and Delete operations:



```dart
// lib/services/task_service.dart
import 'package:my_app_client/my_app_client.dart';

/// Service class that wraps Serverpod client calls for Task operations.
/// This pattern keeps your UI code clean and makes testing easier.
class TaskService {
  final Client _client;
  
  TaskService(this._client);
  
  /// Fetch all tasks for the current user
  Future<List<Task>> getAllTasks() async {
    return await _client.task.listTasks();
  }
  
  /// Fetch a single task by ID
  Future<Task?> getTask(int id) async {
    return await _client.task.getTask(id);
  }
  
  /// Create a new task
  Future<Task> createTask({
    required String title,
    String? description,
    DateTime? dueDate,
  }) async {
    // Create a Task object using the generated class
    final task = Task(
      title: title,
      description: description,
      dueDate: dueDate,
      isCompleted: false,
      createdAt: DateTime.now(),
    );
    
    // Server returns the created task with its ID
    return await _client.task.createTask(task);
  }
  
  /// Update an existing task
  Future<Task> updateTask(Task task) async {
    return await _client.task.updateTask(task);
  }
  
  /// Mark a task as completed
  Future<Task> completeTask(int taskId) async {
    // Fetch current task
    final task = await _client.task.getTask(taskId);
    if (task == null) {
      throw Exception('Task not found');
    }
    
    // Update the completion status
    final updatedTask = task.copyWith(
      isCompleted: true,
      completedAt: DateTime.now(),
    );
    
    return await _client.task.updateTask(updatedTask);
  }
  
  /// Delete a task
  Future<void> deleteTask(int id) async {
    await _client.task.deleteTask(id);
  }
  
  /// Search tasks by title
  Future<List<Task>> searchTasks(String query) async {
    return await _client.task.searchTasks(query);
  }
  
  /// Get tasks due today
  Future<List<Task>> getTasksDueToday() async {
    final now = DateTime.now();
    final startOfDay = DateTime(now.year, now.month, now.day);
    final endOfDay = startOfDay.add(const Duration(days: 1));
    
    return await _client.task.getTasksByDateRange(
      start: startOfDay,
      end: endOfDay,
    );
  }
}
```
