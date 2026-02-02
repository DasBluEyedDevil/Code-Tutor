// lib/core/sync/conflict_detector.dart
import 'package:flutter_riverpod/flutter_riverpod.dart';

enum ConflictType {
  editConflict,    // Both edited same entity
  deleteConflict,  // Deleted on one side, edited on other
  createConflict,  // Same ID created on both (rare)
}

class SyncConflict {
  final String entityId;
  final String entityType;
  final ConflictType type;
  final dynamic localVersion;
  final dynamic serverVersion;
  final DateTime localModifiedAt;
  final DateTime serverModifiedAt;

  SyncConflict({
    required this.entityId,
    required this.entityType,
    required this.type,
    required this.localVersion,
    required this.serverVersion,
    required this.localModifiedAt,
    required this.serverModifiedAt,
  });
}

enum MergeStrategy { lastWriteWins, serverWins, clientWins, manual }

class ConflictDetector {
  // Detect if there's a conflict between local and server version
  SyncConflict? detectConflict({
    required dynamic localEntity,
    required dynamic serverEntity,
  }) {
    // TODO: Compare timestamps and detect conflict type
    throw UnimplementedError();
  }

  // Resolve conflict based on strategy
  Future<dynamic> resolveConflict(
    SyncConflict conflict,
    MergeStrategy strategy,
  ) async {
    // TODO: Apply resolution strategy
    throw UnimplementedError();
  }
}

// lib/core/sync/conflict_resolution_dialog.dart
import 'package:flutter/material.dart';

class ConflictResolutionDialog extends StatelessWidget {
  final SyncConflict conflict;

  const ConflictResolutionDialog({super.key, required this.conflict});

  @override
  Widget build(BuildContext context) {
    // TODO: Show both versions side by side
    // TODO: Highlight differences
    // TODO: Provide resolution options
    throw UnimplementedError();
  }

  Widget _buildDiffView() {
    // TODO: Show text diff between versions
    throw UnimplementedError();
  }
}