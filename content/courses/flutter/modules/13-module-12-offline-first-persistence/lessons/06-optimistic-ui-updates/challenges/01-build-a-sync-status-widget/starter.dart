import 'package:flutter/material.dart';

enum SyncState { synced, pending, failed }

class SyncBadge extends StatelessWidget {
  final SyncState state;
  
  const SyncBadge({required this.state});
  
  @override
  Widget build(BuildContext context) {
    // TODO: Return appropriate icon based on state
    return Container();
  }
}