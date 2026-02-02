---
type: "EXAMPLE"
title: "Task Queues for Heavy Processing"
---

For CPU-intensive or I/O-heavy work, task queues distribute the load and provide better control over concurrency.



```dart
// File: lib/src/services/task_queue.dart

import 'dart:async';
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

/// A simple in-memory task queue for processing work items.
/// For production, consider using a dedicated queue like Redis or RabbitMQ.
class TaskQueue<T> {
  final String name;
  final int concurrency;
  final Future<void> Function(Session session, T task) processor;
  
  final List<_QueuedTask<T>> _queue = [];
  int _activeWorkers = 0;
  bool _isRunning = false;
  final Serverpod _pod;
  
  TaskQueue({
    required this.name,
    required this.concurrency,
    required this.processor,
    required Serverpod pod,
  }) : _pod = pod;
  
  /// Add a task to the queue.
  void enqueue(T task, {int priority = 0, int maxRetries = 3}) {
    _queue.add(_QueuedTask(
      task: task,
      priority: priority,
      maxRetries: maxRetries,
      attempts: 0,
    ));
    
    // Sort by priority (higher first)
    _queue.sort((a, b) => b.priority.compareTo(a.priority));
    
    // Start processing if not already running
    _processQueue();
  }
  
  /// Start the queue processor.
  void start() {
    _isRunning = true;
    _processQueue();
  }
  
  /// Stop accepting new tasks.
  void stop() {
    _isRunning = false;
  }
  
  /// Get current queue status.
  QueueStatus get status => QueueStatus(
    name: name,
    queuedTasks: _queue.length,
    activeWorkers: _activeWorkers,
    maxConcurrency: concurrency,
    isRunning: _isRunning,
  );
  
  void _processQueue() {
    // Do not start if stopped or already at max concurrency
    if (!_isRunning) return;
    if (_activeWorkers >= concurrency) return;
    if (_queue.isEmpty) return;
    
    // Take the next task
    final queuedTask = _queue.removeAt(0);
    _activeWorkers++;
    
    // Process asynchronously
    _processTask(queuedTask).then((_) {
      _activeWorkers--;
      _processQueue(); // Process next task
    });
    
    // If we can run more workers, start them
    if (_activeWorkers < concurrency && _queue.isNotEmpty) {
      _processQueue();
    }
  }
  
  Future<void> _processTask(_QueuedTask<T> queuedTask) async {
    final session = await _pod.createSession();
    
    try {
      session.log('Processing task from queue "$name"');
      await processor(session, queuedTask.task);
      session.log('Task completed successfully');
    } catch (e, stackTrace) {
      queuedTask.attempts++;
      
      if (queuedTask.attempts < queuedTask.maxRetries) {
        // Retry with exponential backoff
        final delay = Duration(seconds: pow(2, queuedTask.attempts).toInt());
        session.log(
          'Task failed (attempt ${queuedTask.attempts}/${queuedTask.maxRetries}), '
          'retrying in ${delay.inSeconds}s: $e',
          level: LogLevel.warning,
        );
        
        await Future.delayed(delay);
        _queue.add(queuedTask); // Re-add to queue
      } else {
        session.log(
          'Task failed permanently after ${queuedTask.maxRetries} attempts: $e',
          level: LogLevel.error,
          stackTrace: stackTrace,
        );
        // Could notify, save to dead letter queue, etc.
      }
    } finally {
      await session.close();
    }
  }
}

class _QueuedTask<T> {
  final T task;
  final int priority;
  final int maxRetries;
  int attempts;
  
  _QueuedTask({
    required this.task,
    required this.priority,
    required this.maxRetries,
    required this.attempts,
  });
}

int pow(int base, int exponent) {
  int result = 1;
  for (int i = 0; i < exponent; i++) {
    result *= base;
  }
  return result;
}

// Usage example:
// File: lib/server.dart

late TaskQueue<ImageProcessingTask> imageQueue;

void run(List<String> args) async {
  final pod = Serverpod(
    args,
    Protocol(),
    Endpoints(),
  );
  
  // Create task queue for image processing
  imageQueue = TaskQueue<ImageProcessingTask>(
    name: 'image-processing',
    concurrency: 4, // Process 4 images at a time
    pod: pod,
    processor: (session, task) async {
      await ImageProcessor.processImage(session, task);
    },
  );
  
  imageQueue.start();
  
  await pod.start();
}

// File: lib/src/endpoints/image_endpoint.dart

class ImageEndpoint extends Endpoint {
  Future<void> uploadImage(
    Session session,
    String imageUrl,
    int userId,
  ) async {
    // Save original image reference
    final image = await Image.db.insertRow(
      session,
      Image(url: imageUrl, userId: userId, status: 'pending'),
    );
    
    // Queue for background processing (thumbnail, resize, etc.)
    imageQueue.enqueue(
      ImageProcessingTask(imageId: image.id!, operations: ['thumbnail', 'resize']),
      priority: 1,
    );
    
    session.log('Queued image ${image.id} for processing');
  }
}
```
