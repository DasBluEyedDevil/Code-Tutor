import 'package:flutter/material.dart';

enum SyncState { synced, pending, failed }

class SyncBadge extends StatelessWidget {
  final SyncState state;
  
  const SyncBadge({required this.state});
  
  @override
  Widget build(BuildContext context) {
    switch (state) {
      case SyncState.synced:
        return const Icon(
          Icons.check_circle,
          color: Colors.green,
          size: 16,
        );
      case SyncState.pending:
        return const Icon(
          Icons.access_time,
          color: Colors.orange,
          size: 16,
        );
      case SyncState.failed:
        return const Icon(
          Icons.cancel,
          color: Colors.red,
          size: 16,
        );
    }
  }
}