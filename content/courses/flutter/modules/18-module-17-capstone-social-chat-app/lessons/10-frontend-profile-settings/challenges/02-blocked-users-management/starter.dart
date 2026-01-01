// lib/features/settings/presentation/screens/blocked_users_screen.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../domain/blocked_user.dart';
import '../../providers/blocked_users_provider.dart';

class BlockedUsersScreen extends ConsumerWidget {
  const BlockedUsersScreen({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final blockedUsersAsync = ref.watch(blockedUsersProvider);

    // TODO: Implement blocked users screen
    // 1. AppBar with title
    // 2. Handle loading/error/empty states
    // 3. Display list of blocked users
    // 4. Pull-to-refresh functionality
    throw UnimplementedError();
  }

  Widget _buildUserTile(
    BuildContext context,
    WidgetRef ref,
    BlockedUser user,
  ) {
    // TODO: Build user tile with avatar, name, block date, unblock button
    throw UnimplementedError();
  }

  Widget _buildEmptyState(BuildContext context) {
    // TODO: Build empty state when no blocked users
    throw UnimplementedError();
  }

  void _confirmUnblock(
    BuildContext context,
    WidgetRef ref,
    BlockedUser user,
  ) {
    // TODO: Show confirmation dialog and unblock user
    throw UnimplementedError();
  }
}

// lib/features/settings/providers/blocked_users_provider.dart
class BlockedUsersNotifier extends AsyncNotifier<List<BlockedUser>> {
  @override
  Future<List<BlockedUser>> build() async {
    // TODO: Fetch blocked users from API
    throw UnimplementedError();
  }

  Future<void> unblockUser(String userId) async {
    // TODO: Optimistically remove user, call API, revert on failure
    throw UnimplementedError();
  }

  Future<void> blockUser(String userId) async {
    // TODO: Add user to blocked list
    throw UnimplementedError();
  }
}

// lib/features/settings/domain/blocked_user.dart
class BlockedUser {
  final String id;
  final String username;
  final String displayName;
  final String? avatarUrl;
  final DateTime blockedAt;

  BlockedUser({
    required this.id,
    required this.username,
    required this.displayName,
    this.avatarUrl,
    required this.blockedAt,
  });
}