---
type: "EXAMPLE"
title: "Conversations List Screen"
---


**Building the Conversations List**

The ConversationsScreen displays all active chats with last message preview, unread badges, online status indicators, and timestamps.



```dart
// lib/features/chat/presentation/screens/conversations_screen.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../providers/conversations_provider.dart';
import '../../domain/conversation.dart';
import '../widgets/conversation_tile.dart';
import '../widgets/conversations_shimmer.dart';

class ConversationsScreen extends ConsumerWidget {
  const ConversationsScreen({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final conversationsAsync = ref.watch(conversationsProvider);
    final theme = Theme.of(context);

    return Scaffold(
      appBar: AppBar(
        title: const Text('Messages'),
        actions: [
          IconButton(
            icon: const Icon(Icons.search),
            onPressed: () => _openSearch(context),
            tooltip: 'Search conversations',
          ),
          IconButton(
            icon: const Icon(Icons.edit_square),
            onPressed: () => _startNewConversation(context),
            tooltip: 'New message',
          ),
        ],
      ),
      body: conversationsAsync.when(
        loading: () => const ConversationsShimmer(),
        error: (error, stack) => _buildErrorState(context, ref, error),
        data: (conversations) => conversations.isEmpty
            ? _buildEmptyState(context, theme)
            : _buildConversationsList(context, ref, conversations),
      ),
    );
  }

  Widget _buildConversationsList(
    BuildContext context,
    WidgetRef ref,
    List<Conversation> conversations,
  ) {
    return RefreshIndicator(
      onRefresh: () => ref.refresh(conversationsProvider.future),
      child: ListView.separated(
        itemCount: conversations.length,
        separatorBuilder: (_, __) => const Divider(height: 1, indent: 72),
        itemBuilder: (context, index) {
          final conversation = conversations[index];
          return ConversationTile(
            key: ValueKey(conversation.id),
            conversation: conversation,
            onTap: () => _openChat(context, conversation),
            onLongPress: () => _showOptions(context, conversation),
          );
        },
      ),
    );
  }

  Widget _buildEmptyState(BuildContext context, ThemeData theme) {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Icon(
            Icons.chat_bubble_outline,
            size: 80,
            color: theme.colorScheme.onSurfaceVariant.withValues(alpha: 0.5),
          ),
          const SizedBox(height: 24),
          Text(
            'No conversations yet',
            style: theme.textTheme.headlineSmall,
          ),
          const SizedBox(height: 8),
          Text(
            'Start chatting with someone!',
            style: theme.textTheme.bodyMedium?.copyWith(
              color: theme.colorScheme.onSurfaceVariant,
            ),
          ),
          const SizedBox(height: 24),
          FilledButton.icon(
            onPressed: () => _startNewConversation(context),
            icon: const Icon(Icons.add),
            label: const Text('New Message'),
          ),
        ],
      ),
    );
  }

  Widget _buildErrorState(BuildContext context, WidgetRef ref, Object error) {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Icon(
            Icons.error_outline,
            size: 64,
            color: Theme.of(context).colorScheme.error,
          ),
          const SizedBox(height: 16),
          Text('Failed to load conversations'),
          const SizedBox(height: 16),
          FilledButton(
            onPressed: () => ref.invalidate(conversationsProvider),
            child: const Text('Retry'),
          ),
        ],
      ),
    );
  }

  void _openChat(BuildContext context, Conversation conversation) {
    Navigator.of(context).pushNamed(
      '/chat/${conversation.id}',
      arguments: conversation,
    );
  }

  void _openSearch(BuildContext context) {
    Navigator.of(context).pushNamed('/chat/search');
  }

  void _startNewConversation(BuildContext context) {
    Navigator.of(context).pushNamed('/chat/new');
  }

  void _showOptions(BuildContext context, Conversation conversation) {
    showModalBottomSheet(
      context: context,
      builder: (context) => SafeArea(
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            ListTile(
              leading: const Icon(Icons.push_pin_outlined),
              title: Text(conversation.isPinned ? 'Unpin' : 'Pin'),
              onTap: () => Navigator.pop(context),
            ),
            ListTile(
              leading: const Icon(Icons.notifications_off_outlined),
              title: Text(conversation.isMuted ? 'Unmute' : 'Mute'),
              onTap: () => Navigator.pop(context),
            ),
            ListTile(
              leading: Icon(
                Icons.delete_outline,
                color: Theme.of(context).colorScheme.error,
              ),
              title: Text(
                'Delete',
                style: TextStyle(
                  color: Theme.of(context).colorScheme.error,
                ),
              ),
              onTap: () => Navigator.pop(context),
            ),
          ],
        ),
      ),
    );
  }
}

---

// lib/features/chat/presentation/widgets/conversation_tile.dart
import 'package:flutter/material.dart';
import 'package:cached_network_image/cached_network_image.dart';
import '../../domain/conversation.dart';
import '../../../../core/utils/time_formatter.dart';

class ConversationTile extends StatelessWidget {
  final Conversation conversation;
  final VoidCallback onTap;
  final VoidCallback onLongPress;

  const ConversationTile({
    super.key,
    required this.conversation,
    required this.onTap,
    required this.onLongPress,
  });

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final hasUnread = conversation.unreadCount > 0;

    return ListTile(
      onTap: onTap,
      onLongPress: onLongPress,
      contentPadding: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
      leading: _buildAvatar(theme),
      title: Row(
        children: [
          Expanded(
            child: Text(
              conversation.displayName,
              style: theme.textTheme.titleMedium?.copyWith(
                fontWeight: hasUnread ? FontWeight.bold : FontWeight.normal,
              ),
              maxLines: 1,
              overflow: TextOverflow.ellipsis,
            ),
          ),
          const SizedBox(width: 8),
          Text(
            TimeFormatter.formatRelative(conversation.lastMessageAt),
            style: theme.textTheme.bodySmall?.copyWith(
              color: hasUnread
                  ? theme.colorScheme.primary
                  : theme.colorScheme.onSurfaceVariant,
              fontWeight: hasUnread ? FontWeight.bold : FontWeight.normal,
            ),
          ),
        ],
      ),
      subtitle: Row(
        children: [
          // Typing indicator or last message
          Expanded(
            child: conversation.isTyping
                ? _buildTypingIndicator(theme)
                : _buildLastMessage(theme, hasUnread),
          ),
          // Unread badge
          if (hasUnread) ...[
            const SizedBox(width: 8),
            _buildUnreadBadge(theme),
          ],
          // Muted indicator
          if (conversation.isMuted) ...[
            const SizedBox(width: 8),
            Icon(
              Icons.notifications_off,
              size: 16,
              color: theme.colorScheme.onSurfaceVariant,
            ),
          ],
        ],
      ),
    );
  }

  Widget _buildAvatar(ThemeData theme) {
    return Stack(
      children: [
        CircleAvatar(
          radius: 28,
          backgroundColor: theme.colorScheme.primaryContainer,
          backgroundImage: conversation.avatarUrl != null
              ? CachedNetworkImageProvider(conversation.avatarUrl!)
              : null,
          child: conversation.avatarUrl == null
              ? Text(
                  conversation.displayName[0].toUpperCase(),
                  style: TextStyle(
                    color: theme.colorScheme.onPrimaryContainer,
                    fontSize: 20,
                    fontWeight: FontWeight.bold,
                  ),
                )
              : null,
        ),
        // Online indicator
        if (conversation.isOnline)
          Positioned(
            right: 0,
            bottom: 0,
            child: Container(
              width: 16,
              height: 16,
              decoration: BoxDecoration(
                color: Colors.green,
                shape: BoxShape.circle,
                border: Border.all(
                  color: theme.colorScheme.surface,
                  width: 2,
                ),
              ),
            ),
          ),
      ],
    );
  }

  Widget _buildTypingIndicator(ThemeData theme) {
    return Row(
      children: [
        Text(
          'typing',
          style: theme.textTheme.bodyMedium?.copyWith(
            color: theme.colorScheme.primary,
            fontStyle: FontStyle.italic,
          ),
        ),
        const SizedBox(width: 4),
        SizedBox(
          width: 20,
          child: _TypingDots(color: theme.colorScheme.primary),
        ),
      ],
    );
  }

  Widget _buildLastMessage(ThemeData theme, bool hasUnread) {
    final prefix = conversation.lastMessageIsFromMe ? 'You: ' : '';
    return Text(
      '$prefix${conversation.lastMessagePreview}',
      style: theme.textTheme.bodyMedium?.copyWith(
        color: hasUnread
            ? theme.colorScheme.onSurface
            : theme.colorScheme.onSurfaceVariant,
        fontWeight: hasUnread ? FontWeight.w500 : FontWeight.normal,
      ),
      maxLines: 1,
      overflow: TextOverflow.ellipsis,
    );
  }

  Widget _buildUnreadBadge(ThemeData theme) {
    final count = conversation.unreadCount;
    final displayCount = count > 99 ? '99+' : count.toString();

    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 4),
      decoration: BoxDecoration(
        color: theme.colorScheme.primary,
        borderRadius: BorderRadius.circular(12),
      ),
      child: Text(
        displayCount,
        style: theme.textTheme.labelSmall?.copyWith(
          color: theme.colorScheme.onPrimary,
          fontWeight: FontWeight.bold,
        ),
      ),
    );
  }
}

// Animated typing dots
class _TypingDots extends StatefulWidget {
  final Color color;

  const _TypingDots({required this.color});

  @override
  State<_TypingDots> createState() => _TypingDotsState();
}

class _TypingDotsState extends State<_TypingDots>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      vsync: this,
      duration: const Duration(milliseconds: 1200),
    )..repeat();
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return AnimatedBuilder(
      animation: _controller,
      builder: (context, child) {
        return Row(
          mainAxisSize: MainAxisSize.min,
          children: List.generate(3, (index) {
            final delay = index * 0.2;
            final value = (_controller.value + delay) % 1.0;
            final opacity = value < 0.5 ? value * 2 : (1 - value) * 2;
            return Container(
              margin: const EdgeInsets.symmetric(horizontal: 1),
              width: 4,
              height: 4,
              decoration: BoxDecoration(
                color: widget.color.withValues(alpha: opacity.clamp(0.3, 1.0)),
                shape: BoxShape.circle,
              ),
            );
          }),
        );
      },
    );
  }
}

---

// lib/features/chat/domain/conversation.dart
import 'package:freezed_annotation/freezed_annotation.dart';

part 'conversation.freezed.dart';
part 'conversation.g.dart';

@freezed
class Conversation with _$Conversation {
  const Conversation._();

  const factory Conversation({
    required String id,
    required String displayName,
    String? avatarUrl,
    required String lastMessagePreview,
    required DateTime lastMessageAt,
    @Default(false) bool lastMessageIsFromMe,
    @Default(0) int unreadCount,
    @Default(false) bool isOnline,
    @Default(false) bool isTyping,
    @Default(false) bool isMuted,
    @Default(false) bool isPinned,
    @Default(false) bool isGroup,
    List<String>? participantIds,
  }) = _Conversation;

  factory Conversation.fromJson(Map<String, dynamic> json) =>
      _$ConversationFromJson(json);

  bool get hasUnread => unreadCount > 0;
}

---

// lib/features/chat/providers/conversations_provider.dart
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../domain/conversation.dart';
import '../data/chat_repository.dart';

final conversationsProvider =
    AsyncNotifierProvider<ConversationsNotifier, List<Conversation>>(() {
  return ConversationsNotifier();
});

class ConversationsNotifier extends AsyncNotifier<List<Conversation>> {
  @override
  Future<List<Conversation>> build() async {
    // Subscribe to real-time updates
    _subscribeToUpdates();
    return _fetchConversations();
  }

  ChatRepository get _repository => ref.read(chatRepositoryProvider);

  Future<List<Conversation>> _fetchConversations() async {
    return await _repository.getConversations();
  }

  void _subscribeToUpdates() {
    ref.listen(chatStreamProvider, (previous, next) {
      next.whenData((event) {
        if (event.type == ChatEventType.newMessage ||
            event.type == ChatEventType.conversationUpdated) {
          ref.invalidateSelf();
        }
      });
    });
  }

  void markAsRead(String conversationId) {
    state = state.whenData((conversations) {
      return conversations.map((c) {
        if (c.id == conversationId) {
          return c.copyWith(unreadCount: 0);
        }
        return c;
      }).toList();
    });
  }

  void updateTypingStatus(String conversationId, bool isTyping) {
    state = state.whenData((conversations) {
      return conversations.map((c) {
        if (c.id == conversationId) {
          return c.copyWith(isTyping: isTyping);
        }
        return c;
      }).toList();
    });
  }
}
```
