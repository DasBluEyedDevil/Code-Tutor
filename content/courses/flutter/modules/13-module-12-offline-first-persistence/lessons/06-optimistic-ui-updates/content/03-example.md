---
type: "EXAMPLE"
title: "Sync Status Indicators"
---


**Showing Sync State to Users:**



```dart
enum SyncStatus {
  synced,     // Confirmed on server
  pending,    // Waiting to sync
  syncing,    // Currently syncing
  failed,     // Sync failed
}

class SyncStatusIndicator extends StatelessWidget {
  final SyncStatus status;
  final VoidCallback? onRetry;
  
  const SyncStatusIndicator({
    required this.status,
    this.onRetry,
  });
  
  @override
  Widget build(BuildContext context) {
    switch (status) {
      case SyncStatus.synced:
        return const Icon(
          Icons.cloud_done,
          color: Colors.green,
          size: 16,
        );
        
      case SyncStatus.pending:
        return const Icon(
          Icons.cloud_upload_outlined,
          color: Colors.orange,
          size: 16,
        );
        
      case SyncStatus.syncing:
        return const SizedBox(
          width: 16,
          height: 16,
          child: CircularProgressIndicator(
            strokeWidth: 2,
          ),
        );
        
      case SyncStatus.failed:
        return GestureDetector(
          onTap: onRetry,
          child: const Icon(
            Icons.cloud_off,
            color: Colors.red,
            size: 16,
          ),
        );
    }
  }
}

// Note list item with sync indicator
class NoteListTile extends StatelessWidget {
  final Note note;
  final VoidCallback onTap;
  final VoidCallback onRetrySync;
  
  const NoteListTile({
    required this.note,
    required this.onTap,
    required this.onRetrySync,
  });
  
  @override
  Widget build(BuildContext context) {
    return ListTile(
      title: Text(
        note.title,
        style: TextStyle(
          // Dim pending items slightly
          color: note.syncStatus == SyncStatus.pending
              ? Colors.grey
              : null,
        ),
      ),
      subtitle: Text(
        _formatDate(note.updatedAt ?? note.createdAt),
      ),
      trailing: SyncStatusIndicator(
        status: note.syncStatus,
        onRetry: onRetrySync,
      ),
      onTap: onTap,
    );
  }
}

// Global sync status banner
class SyncStatusBanner extends StatelessWidget {
  final int pendingCount;
  final int failedCount;
  final bool isOnline;
  
  const SyncStatusBanner({
    required this.pendingCount,
    required this.failedCount,
    required this.isOnline,
  });
  
  @override
  Widget build(BuildContext context) {
    if (!isOnline) {
      return Container(
        color: Colors.orange,
        padding: const EdgeInsets.all(8),
        child: const Row(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Icon(Icons.wifi_off, color: Colors.white, size: 16),
            SizedBox(width: 8),
            Text(
              'Offline - changes will sync when connected',
              style: TextStyle(color: Colors.white),
            ),
          ],
        ),
      );
    }
    
    if (failedCount > 0) {
      return Container(
        color: Colors.red,
        padding: const EdgeInsets.all(8),
        child: Row(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            const Icon(Icons.error, color: Colors.white, size: 16),
            const SizedBox(width: 8),
            Text(
              '$failedCount items failed to sync',
              style: const TextStyle(color: Colors.white),
            ),
          ],
        ),
      );
    }
    
    if (pendingCount > 0) {
      return Container(
        color: Colors.blue,
        padding: const EdgeInsets.all(8),
        child: Row(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            const SizedBox(
              width: 16,
              height: 16,
              child: CircularProgressIndicator(
                color: Colors.white,
                strokeWidth: 2,
              ),
            ),
            const SizedBox(width: 8),
            Text(
              'Syncing $pendingCount items...',
              style: const TextStyle(color: Colors.white),
            ),
          ],
        ),
      );
    }
    
    return const SizedBox.shrink();
  }
}
```
