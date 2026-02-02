import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class TaskEndpoint extends Endpoint {

  /// Get all tasks ordered by creation date.
  Future<List<Task>> getTasks(Session session) async {
    return await Task.db.find(
      session,
      orderBy: (t) => t.createdAt,
      orderDescending: true,
    );
  }

  /// Get a single task by ID.
  Future<Task?> getTask(Session session, int taskId) async {
    return await Task.db.findById(session, taskId);
  }

  /// Create a new task.
  Future<Task> createTask(Session session, Task task) async {
    if (task.title.trim().isEmpty) {
      throw ArgumentError('Task title cannot be empty');
    }

    final taskToCreate = task.copyWith(
      createdAt: DateTime.now(),
      isCompleted: false,
    );

    return await Task.db.insertRow(session, taskToCreate);
  }

  /// Update an existing task.
  Future<Task> updateTask(Session session, Task task) async {
    if (task.id == null) {
      throw ArgumentError('Task ID is required for update');
    }

    final existing = await Task.db.findById(session, task.id!);
    if (existing == null) {
      throw Exception('Task not found');
    }

    return await Task.db.updateRow(session, task);
  }

  /// Delete a task by ID.
  Future<bool> deleteTask(Session session, int taskId) async {
    final task = await Task.db.findById(session, taskId);
    if (task == null) {
      return false;
    }

    await Task.db.deleteRow(session, task);
    return true;
  }
}