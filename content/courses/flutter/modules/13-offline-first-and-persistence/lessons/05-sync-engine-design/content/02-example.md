---
type: "EXAMPLE"
title: "Conflict Resolution"
---


**Handling Sync Conflicts:**



```dart
enum ConflictResolution {
  serverWins,
  clientWins,
  merge,
  manual,
}

class ConflictResolver {
  /// Resolve conflict between local and server versions
  Future<Note> resolveNoteConflict(
    Note local,
    Note server,
    ConflictResolution strategy,
  ) async {
    switch (strategy) {
      case ConflictResolution.serverWins:
        // Server version overwrites local
        return server.copyWith(syncStatus: SyncStatus.synced);
        
      case ConflictResolution.clientWins:
        // Keep local, push to server again
        return local.copyWith(syncStatus: SyncStatus.pending);
        
      case ConflictResolution.merge:
        // Merge changes field by field
        return _mergeNotes(local, server);
        
      case ConflictResolution.manual:
        // Store both versions for user to resolve
        throw ConflictException(local: local, server: server);
    }
  }
  
  Note _mergeNotes(Note local, Note server) {
    // Last-write-wins per field
    return Note(
      id: local.id,
      localId: local.localId,
      serverId: server.serverId,
      // Use whichever was updated more recently
      title: local.updatedAt.isAfter(server.updatedAt)
          ? local.title
          : server.title,
      content: local.updatedAt.isAfter(server.updatedAt)
          ? local.content
          : server.content,
      // Always use server for certain fields
      createdAt: server.createdAt,
      updatedAt: DateTime.now(),
      syncStatus: SyncStatus.synced,
    );
  }
}

class SyncService {
  Future<void> syncNote(SyncOperation op) async {
    try {
      final response = await _api.syncNote(op.data);
      
      if (response.hasConflict) {
        final resolved = await _conflictResolver.resolveNoteConflict(
          op.data,
          response.serverVersion,
          ConflictResolution.merge,
        );
        
        await _db.update(_db.notes).replace(resolved);
        
        if (resolved.syncStatus == SyncStatus.pending) {
          // Need to push again after client-wins
          await _syncQueue.enqueue(op.copyWith(data: resolved));
        }
      } else {
        // No conflict, update local with server ID
        await _db.updateNoteServerId(op.localId, response.serverId);
      }
    } catch (e) {
      // Network error - will retry later
      await _syncQueue.markFailed(op);
    }
  }
}
```
