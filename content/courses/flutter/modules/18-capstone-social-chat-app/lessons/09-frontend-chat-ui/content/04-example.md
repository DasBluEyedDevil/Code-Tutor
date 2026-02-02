---
type: "EXAMPLE"
title: "Message Bubbles"
---


**Building Message Bubble Widget**

The MessageBubble widget handles sent/received styling, tail shapes, timestamps, delivery status icons, and image message display.



```dart
// lib/features/chat/presentation/widgets/message_bubble.dart
import 'package:flutter/material.dart';
import 'package:cached_network_image/cached_network_image.dart';
import '../../domain/message.dart';
import '../../../../core/utils/time_formatter.dart';

class MessageBubble extends StatelessWidget {
  final Message message;
  final bool isFirstInGroup;
  final bool isLastInGroup;
  final VoidCallback? onLongPress;

  const MessageBubble({
    super.key,
    required this.message,
    this.isFirstInGroup = true,
    this.isLastInGroup = true,
    this.onLongPress,
  });

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final isFromMe = message.isFromMe;

    return Padding(
      padding: EdgeInsets.only(
        top: isFirstInGroup ? 8 : 2,
        bottom: isLastInGroup ? 8 : 2,
        left: isFromMe ? 64 : 8,
        right: isFromMe ? 8 : 64,
      ),
      child: Row(
        mainAxisAlignment:
            isFromMe ? MainAxisAlignment.end : MainAxisAlignment.start,
        crossAxisAlignment: CrossAxisAlignment.end,
        children: [
          if (!isFromMe && isLastInGroup) ...[
            _buildAvatar(theme),
            const SizedBox(width: 8),
          ] else if (!isFromMe) ...[
            const SizedBox(width: 40),
          ],
          Flexible(
            child: GestureDetector(
              onLongPress: onLongPress,
              child: _buildBubble(context, theme, isFromMe),
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildAvatar(ThemeData theme) {
    return CircleAvatar(
      radius: 16,
      backgroundColor: theme.colorScheme.primaryContainer,
      backgroundImage: message.senderAvatarUrl != null
          ? CachedNetworkImageProvider(message.senderAvatarUrl!)
          : null,
      child: message.senderAvatarUrl == null
          ? Text(
              message.senderName[0].toUpperCase(),
              style: TextStyle(
                color: theme.colorScheme.onPrimaryContainer,
                fontSize: 12,
              ),
            )
          : null,
    );
  }

  Widget _buildBubble(BuildContext context, ThemeData theme, bool isFromMe) {
    final bubbleColor = isFromMe
        ? theme.colorScheme.primary
        : theme.colorScheme.surfaceContainerHighest;
    final textColor = isFromMe
        ? theme.colorScheme.onPrimary
        : theme.colorScheme.onSurface;

    // Determine corner radii based on position in group
    final topLeft = isFromMe || isFirstInGroup ? 16.0 : 4.0;
    final topRight = !isFromMe || isFirstInGroup ? 16.0 : 4.0;
    final bottomLeft = isFromMe || isLastInGroup ? 16.0 : 4.0;
    final bottomRight = !isFromMe || isLastInGroup ? 16.0 : 4.0;

    return Container(
      decoration: BoxDecoration(
        color: bubbleColor,
        borderRadius: BorderRadius.only(
          topLeft: Radius.circular(topLeft),
          topRight: Radius.circular(topRight),
          bottomLeft: Radius.circular(bottomLeft),
          bottomRight: Radius.circular(bottomRight),
        ),
      ),
      child: message.type == MessageType.image
          ? _buildImageContent(context, theme, isFromMe)
          : _buildTextContent(theme, textColor, isFromMe),
    );
  }

  Widget _buildTextContent(
    ThemeData theme,
    Color textColor,
    bool isFromMe,
  ) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 8),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.end,
        children: [
          Text(
            message.content,
            style: theme.textTheme.bodyMedium?.copyWith(color: textColor),
          ),
          const SizedBox(height: 4),
          Row(
            mainAxisSize: MainAxisSize.min,
            children: [
              Text(
                TimeFormatter.formatTime(message.createdAt),
                style: theme.textTheme.labelSmall?.copyWith(
                  color: textColor.withOpacity(0.7),
                ),
              ),
              if (isFromMe) ...[
                const SizedBox(width: 4),
                _buildStatusIcon(theme, textColor),
              ],
            ],
          ),
        ],
      ),
    );
  }

  Widget _buildImageContent(
    BuildContext context,
    ThemeData theme,
    bool isFromMe,
  ) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.end,
      children: [
        ClipRRect(
          borderRadius: BorderRadius.circular(12),
          child: GestureDetector(
            onTap: () => _openImageViewer(context),
            child: ConstrainedBox(
              constraints: const BoxConstraints(
                maxWidth: 250,
                maxHeight: 300,
              ),
              child: CachedNetworkImage(
                imageUrl: message.imageUrl!,
                fit: BoxFit.cover,
                placeholder: (context, url) => Container(
                  width: 200,
                  height: 150,
                  color: theme.colorScheme.surfaceContainerHighest,
                  child: const Center(child: CircularProgressIndicator()),
                ),
                errorWidget: (context, url, error) => Container(
                  width: 200,
                  height: 150,
                  color: theme.colorScheme.errorContainer,
                  child: const Icon(Icons.broken_image),
                ),
              ),
            ),
          ),
        ),
        Padding(
          padding: const EdgeInsets.all(8),
          child: Row(
            mainAxisSize: MainAxisSize.min,
            children: [
              Text(
                TimeFormatter.formatTime(message.createdAt),
                style: theme.textTheme.labelSmall?.copyWith(
                  color: isFromMe
                      ? theme.colorScheme.onPrimary.withOpacity(0.7)
                      : theme.colorScheme.onSurfaceVariant,
                ),
              ),
              if (isFromMe) ...[
                const SizedBox(width: 4),
                _buildStatusIcon(
                  theme,
                  isFromMe
                      ? theme.colorScheme.onPrimary
                      : theme.colorScheme.onSurface,
                ),
              ],
            ],
          ),
        ),
      ],
    );
  }

  Widget _buildStatusIcon(ThemeData theme, Color color) {
    IconData icon;
    Color iconColor = color.withOpacity(0.7);

    switch (message.status) {
      case MessageStatus.sending:
        icon = Icons.access_time;
        break;
      case MessageStatus.sent:
        icon = Icons.check;
        break;
      case MessageStatus.delivered:
        icon = Icons.done_all;
        break;
      case MessageStatus.read:
        icon = Icons.done_all;
        iconColor = Colors.blue;
        break;
      case MessageStatus.failed:
        icon = Icons.error_outline;
        iconColor = theme.colorScheme.error;
        break;
    }

    return Icon(icon, size: 16, color: iconColor);
  }

  void _openImageViewer(BuildContext context) {
    Navigator.of(context).push(
      MaterialPageRoute(
        builder: (context) => Scaffold(
          backgroundColor: Colors.black,
          appBar: AppBar(
            backgroundColor: Colors.black,
            foregroundColor: Colors.white,
          ),
          body: Center(
            child: InteractiveViewer(
              child: CachedNetworkImage(
                imageUrl: message.imageUrl!,
                fit: BoxFit.contain,
              ),
            ),
          ),
        ),
      ),
    );
  }
}

---

// lib/features/chat/domain/message.dart
import 'package:freezed_annotation/freezed_annotation.dart';

part 'message.freezed.dart';
part 'message.g.dart';

enum MessageType { text, image, voice, video, file }

enum MessageStatus { sending, sent, delivered, read, failed }

@freezed
class Message with _$Message {
  const Message._();

  const factory Message({
    required String id,
    required String conversationId,
    required String senderId,
    required String senderName,
    String? senderAvatarUrl,
    required String content,
    String? imageUrl,
    String? voiceUrl,
    int? voiceDuration,
    @Default(MessageType.text) MessageType type,
    @Default(MessageStatus.sent) MessageStatus status,
    required DateTime createdAt,
    DateTime? readAt,
    String? replyToId,
    Message? replyTo,
  }) = _Message;

  factory Message.fromJson(Map<String, dynamic> json) =>
      _$MessageFromJson(json);

  bool get isFromMe => senderId == 'currentUserId'; // Replace with actual check
  bool get isRead => status == MessageStatus.read;
  bool get isFailed => status == MessageStatus.failed;
}

---

// lib/features/chat/presentation/widgets/chat_date_separator.dart
import 'package:flutter/material.dart';
import '../../../../core/utils/time_formatter.dart';

class ChatDateSeparator extends StatelessWidget {
  final DateTime date;

  const ChatDateSeparator({super.key, required this.date});

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);

    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 16),
      child: Center(
        child: Container(
          padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 6),
          decoration: BoxDecoration(
            color: theme.colorScheme.surfaceContainerHighest,
            borderRadius: BorderRadius.circular(16),
          ),
          child: Text(
            TimeFormatter.formatDate(date),
            style: theme.textTheme.labelSmall?.copyWith(
              color: theme.colorScheme.onSurfaceVariant,
            ),
          ),
        ),
      ),
    );
  }
}

---

// lib/features/chat/presentation/widgets/typing_indicator_bar.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../providers/typing_provider.dart';

class TypingIndicatorBar extends ConsumerWidget {
  const TypingIndicatorBar({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final typingUsers = ref.watch(typingUsersProvider);
    final theme = Theme.of(context);

    if (typingUsers.isEmpty) {
      return const SizedBox.shrink();
    }

    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
      child: Row(
        children: [
          _TypingAnimation(color: theme.colorScheme.primary),
          const SizedBox(width: 8),
          Text(
            _buildTypingText(typingUsers),
            style: theme.textTheme.bodySmall?.copyWith(
              color: theme.colorScheme.onSurfaceVariant,
              fontStyle: FontStyle.italic,
            ),
          ),
        ],
      ),
    );
  }

  String _buildTypingText(List<String> users) {
    if (users.length == 1) {
      return '${users.first} is typing...';
    } else if (users.length == 2) {
      return '${users[0]} and ${users[1]} are typing...';
    } else {
      return 'Several people are typing...';
    }
  }
}

class _TypingAnimation extends StatefulWidget {
  final Color color;

  const _TypingAnimation({required this.color});

  @override
  State<_TypingAnimation> createState() => _TypingAnimationState();
}

class _TypingAnimationState extends State<_TypingAnimation>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      vsync: this,
      duration: const Duration(milliseconds: 1500),
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
            final value = ((_controller.value + delay) % 1.0);
            final scale = 0.5 + (value < 0.5 ? value : 1 - value);
            return Transform.scale(
              scale: scale,
              child: Container(
                margin: const EdgeInsets.symmetric(horizontal: 2),
                width: 8,
                height: 8,
                decoration: BoxDecoration(
                  color: widget.color,
                  shape: BoxShape.circle,
                ),
              ),
            );
          }),
        );
      },
    );
  }
}
```
