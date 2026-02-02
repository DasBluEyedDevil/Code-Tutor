---
type: "EXAMPLE"
title: "Sync Integration"
---


**Sync Service Implementation:**



```dart
// lib/services/sync_service.dart
import 'dart:async';
import 'dart:convert';
import 'package:uuid/uuid.dart';

class SyncService {
  final AppDatabase _db;
  final NotesApi _api;
  final ConnectivityService _connectivity;
  
  Timer? _syncTimer;
  bool _isSyncing = false;
  
  final _syncStatusController = StreamController<SyncProgress>.broadcast();
  Stream<SyncProgress> get syncStatus => _syncStatusController.stream;
  
  SyncService(this._db, this._api, this._connectivity) {
    // Listen for connectivity changes
    _connectivity.onConnectivityChanged.listen((isConnected) {
      if (isConnected) {
        sync();
      }
    });
    
    // Periodic sync every 30 seconds when online
    _syncTimer = Timer.periodic(
      const Duration(seconds: 30),
      (_) => sync(),
    );
  }
  
  Future<void> queueOperation({
    required String type,
    required String noteId,
    required Map<String, dynamic> data,
  }) async {
    await _db.addToSyncQueue(SyncQueueCompanion(
      id: Value(const Uuid().v4()),
      operationType: Value(type),
      noteId: Value(noteId),
      payload: Value(jsonEncode(data)),
      createdAt: Value(DateTime.now()),
    ));
  }
  
  Future<void> sync() async {
    if (_isSyncing || !await _connectivity.isConnected) return;
    _isSyncing = true;
    
    try {
      final pendingOps = await _db.getPendingSyncOps();
      
      _syncStatusController.add(SyncProgress(
        total: pendingOps.length,
        completed: 0,
        status: SyncingStatus.inProgress,
      ));
      
      int completed = 0;
      
      for (final op in pendingOps) {
        try {
          // Mark note as syncing
          final note = await _db.getNoteById(op.noteId);
          if (note != null) {
            await _db.updateNote(
              note.copyWith(syncStatus: SyncStatus.syncing),
            );
          }
          
          // Execute sync operation
          await _executeSyncOp(op);
          
          // Remove from queue on success
          await _db.removeSyncOp(op.id);
          
          // Mark note as synced
          if (note != null) {
            await _db.updateNote(
              note.copyWith(syncStatus: SyncStatus.synced),
            );
          }
          
          completed++;
          _syncStatusController.add(SyncProgress(
            total: pendingOps.length,
            completed: completed,
            status: SyncingStatus.inProgress,
          ));
          
        } catch (e) {
          // Mark as failed after max retries
          if (op.retryCount >= 5) {
            final note = await _db.getNoteById(op.noteId);
            if (note != null) {
              await _db.updateNote(
                note.copyWith(syncStatus: SyncStatus.failed),
              );
            }
          }
        }
      }
      
      _syncStatusController.add(SyncProgress(
        total: pendingOps.length,
        completed: completed,
        status: SyncingStatus.completed,
      ));
      
    } finally {
      _isSyncing = false;
    }
  }
  
  Future<void> _executeSyncOp(SyncQueueData op) async {
    final data = jsonDecode(op.payload) as Map<String, dynamic>;
    
    switch (op.operationType) {
      case 'create':
        await _api.createNote(data);
        break;
      case 'update':
        await _api.updateNote(op.noteId, data);
        break;
      case 'delete':
        await _api.deleteNote(op.noteId);
        break;
    }
  }
  
  void dispose() {
    _syncTimer?.cancel();
    _syncStatusController.close();
  }
}

class SyncProgress {
  final int total;
  final int completed;
  final SyncingStatus status;
  
  SyncProgress({
    required this.total,
    required this.completed,
    required this.status,
  });
  
  double get progress => total > 0 ? completed / total : 0;
}

enum SyncingStatus { idle, inProgress, completed, failed }
```
