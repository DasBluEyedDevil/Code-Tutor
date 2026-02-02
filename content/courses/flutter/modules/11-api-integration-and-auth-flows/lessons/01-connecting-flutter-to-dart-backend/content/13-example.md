---
type: "EXAMPLE"
title: "Repository Pattern with Riverpod"
---


Here is a complete example using Riverpod for dependency injection and state management:



```dart
// lib/repositories/task_repository.dart
import 'package:my_app_client/my_app_client.dart';
import '../services/api_caller.dart';

/// Abstract interface for task data operations.
/// This allows us to swap implementations (real API vs mock for testing).
abstract class TaskRepository {
  Future<ApiResult<List<Task>>> getTasks();
  Future<ApiResult<Task>> getTask(int id);
  Future<ApiResult<Task>> createTask(Task task);
  Future<ApiResult<Task>> updateTask(Task task);
  Future<ApiResult<void>> deleteTask(int id);
}

/// Implementation that uses the Serverpod client.
class ServerpodTaskRepository implements TaskRepository {
  final Client _client;
  final ApiCaller _apiCaller;
  
  // Simple in-memory cache
  final Map<int, Task> _cache = {};
  DateTime? _lastFetch;
  static const _cacheDuration = Duration(minutes: 5);
  
  ServerpodTaskRepository(this._client, {ApiCaller? apiCaller})
      : _apiCaller = apiCaller ?? const ApiCaller();
  
  @override
  Future<ApiResult<List<Task>>> getTasks() async {
    // Check cache freshness
    if (_shouldUseCache()) {
      return ApiSuccess(_cache.values.toList());
    }
    
    final result = await _apiCaller.callWithRetry(
      () => _client.task.listTasks(),
    );
    
    // Update cache on success
    if (result is ApiSuccess<List<Task>>) {
      _cache.clear();
      for (final task in result.data) {
        if (task.id != null) {
          _cache[task.id!] = task;
        }
      }
      _lastFetch = DateTime.now();
    }
    
    return result;
  }
  
  @override
  Future<ApiResult<Task>> getTask(int id) async {
    // Check cache first
    if (_cache.containsKey(id)) {
      return ApiSuccess(_cache[id]!);
    }
    
    final result = await _apiCaller.call(
      () => _client.task.getTask(id),
    );
    
    // Update cache on success
    if (result is ApiSuccess<Task?> && result.data != null) {
      _cache[id] = result.data!;
      return ApiSuccess(result.data!);
    }
    
    return result as ApiResult<Task>;
  }
  
  @override
  Future<ApiResult<Task>> createTask(Task task) async {
    final result = await _apiCaller.call(
      () => _client.task.createTask(task),
    );
    
    // Add to cache on success
    if (result is ApiSuccess<Task>) {
      final created = result.data;
      if (created.id != null) {
        _cache[created.id!] = created;
      }
    }
    
    return result;
  }
  
  @override
  Future<ApiResult<Task>> updateTask(Task task) async {
    final result = await _apiCaller.call(
      () => _client.task.updateTask(task),
    );
    
    // Update cache on success
    if (result is ApiSuccess<Task> && task.id != null) {
      _cache[task.id!] = result.data;
    }
    
    return result;
  }
  
  @override
  Future<ApiResult<void>> deleteTask(int id) async {
    final result = await _apiCaller.call(
      () => _client.task.deleteTask(id),
    );
    
    // Remove from cache on success
    if (result is ApiSuccess) {
      _cache.remove(id);
    }
    
    return result;
  }
  
  bool _shouldUseCache() {
    if (_lastFetch == null) return false;
    return DateTime.now().difference(_lastFetch!) < _cacheDuration;
  }
  
  /// Clear the cache (useful when user logs out)
  void clearCache() {
    _cache.clear();
    _lastFetch = null;
  }
}

// lib/providers/task_providers.dart
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:my_app_client/my_app_client.dart';
import '../repositories/task_repository.dart';
import 'client_provider.dart';

/// Provider for the Serverpod client
final clientProvider = Provider<Client>((ref) {
  // Initialize in main.dart and override this provider
  throw UnimplementedError('Client must be initialized in main.dart');
});

/// Provider for the task repository
final taskRepositoryProvider = Provider<TaskRepository>((ref) {
  final client = ref.watch(clientProvider);
  return ServerpodTaskRepository(client);
});

/// Provider that fetches and caches the task list
final tasksProvider = FutureProvider<List<Task>>((ref) async {
  final repository = ref.watch(taskRepositoryProvider);
  final result = await repository.getTasks();
  
  return switch (result) {
    ApiSuccess(:final data) => data,
    ApiFailure(:final error) => throw error,
  };
});

/// Provider for a single task by ID
final taskProvider = FutureProvider.family<Task?, int>((ref, id) async {
  final repository = ref.watch(taskRepositoryProvider);
  final result = await repository.getTask(id);
  
  return switch (result) {
    ApiSuccess(:final data) => data,
    ApiFailure(:final error) => throw error,
  };
});
```
