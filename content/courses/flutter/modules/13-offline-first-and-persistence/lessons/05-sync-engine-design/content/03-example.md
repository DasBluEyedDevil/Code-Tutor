---
type: "EXAMPLE"
title: "Background Sync"
---


**Processing Sync Queue in Background:**



```dart
class SyncQueue {
  final AppDatabase _db;
  final ConnectivityService _connectivity;
  Timer? _retryTimer;
  bool _isProcessing = false;
  
  /// Enqueue a sync operation
  Future<void> enqueue(SyncOperation op) async {
    await _db.into(_db.syncQueue).insert(
      SyncQueueCompanion(
        id: Value(const Uuid().v4()),
        type: Value(op.type.name),
        table_name: Value(op.table),
        localId: Value(op.localId),
        data: Value(jsonEncode(op.data)),
        createdAt: Value(DateTime.now()),
        retryCount: Value(0),
        status: Value('pending'),
      ),
    );
  }
  
  /// Process all pending operations
  Future<void> processQueue() async {
    // Prevent concurrent processing
    if (_isProcessing) return;
    _isProcessing = true;
    
    try {
      // Check connectivity first
      if (!await _connectivity.isConnected) {
        _scheduleRetry();
        return;
      }
      
      // Get pending operations in order
      final pending = await (_db.select(_db.syncQueue)
        ..where((q) => q.status.equals('pending'))
        ..orderBy([(q) => OrderingTerm.asc(q.createdAt)])
        ..limit(50)) // Batch size
        .get();
      
      for (final op in pending) {
        try {
          await _processOperation(op);
          
          // Mark as completed
          await (_db.delete(_db.syncQueue)
            ..where((q) => q.id.equals(op.id)))
            .go();
            
        } catch (e) {
          await _handleOperationError(op, e);
        }
      }
      
    } finally {
      _isProcessing = false;
    }
  }
  
  Future<void> _handleOperationError(SyncQueueEntry op, Object error) async {
    final newRetryCount = op.retryCount + 1;
    
    if (newRetryCount >= 5) {
      // Max retries reached - mark as failed
      await (_db.update(_db.syncQueue)
        ..where((q) => q.id.equals(op.id)))
        .write(SyncQueueCompanion(
          status: Value('failed'),
          errorMessage: Value(error.toString()),
        ));
    } else {
      // Increment retry count
      await (_db.update(_db.syncQueue)
        ..where((q) => q.id.equals(op.id)))
        .write(SyncQueueCompanion(
          retryCount: Value(newRetryCount),
          lastAttempt: Value(DateTime.now()),
        ));
    }
    
    _scheduleRetry();
  }
  
  void _scheduleRetry() {
    _retryTimer?.cancel();
    _retryTimer = Timer(
      const Duration(seconds: 30),
      processQueue,
    );
  }
}
```
