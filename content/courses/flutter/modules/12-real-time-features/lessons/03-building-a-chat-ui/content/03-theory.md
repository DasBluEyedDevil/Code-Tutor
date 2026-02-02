---
type: "THEORY"
title: "Message Bubbles"
---

Message bubbles need to clearly distinguish between sent and received messages while providing status information and maintaining visual hierarchy.

**Design Principles:**

- Sent messages: Right-aligned, colored background (your brand color)
- Received messages: Left-aligned, neutral background
- Group consecutive messages from the same sender
- Show timestamps intelligently (not on every message)
- Display read receipts and delivery status

**Status Indicators:**

- Clock icon: Message sending
- Single check: Message sent to server
- Double check: Message delivered to recipient
- Double check (blue/colored): Message read



```dart
class MessageBubble extends StatelessWidget {
  final ChatMessage message;
  final bool showAvatar;
  final bool showTimestamp;
  
  const MessageBubble({
    required this.message,
    this.showAvatar = true,
    this.showTimestamp = false,
    super.key,
  });
  
  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final isMe = message.senderId == currentUserId;
    
    return Padding(
      padding: EdgeInsets.only(
        top: showTimestamp ? 16 : 2,
        bottom: 2,
      ),
      child: Column(
        children: [
          if (showTimestamp)
            _buildTimestampDivider(context),
          Row(
            mainAxisAlignment:
                isMe ? MainAxisAlignment.end : MainAxisAlignment.start,
            crossAxisAlignment: CrossAxisAlignment.end,
            children: [
              if (!isMe && showAvatar)
                _buildAvatar()
              else if (!isMe)
                const SizedBox(width: 40),
              const SizedBox(width: 8),
              Flexible(
                child: Container(
                  constraints: BoxConstraints(
                    maxWidth: MediaQuery.of(context).size.width * 0.75,
                  ),
                  decoration: BoxDecoration(
                    color: isMe
                        ? theme.colorScheme.primary
                        : theme.colorScheme.surfaceVariant,
                    borderRadius: BorderRadius.only(
                      topLeft: const Radius.circular(16),
                      topRight: const Radius.circular(16),
                      bottomLeft: Radius.circular(isMe ? 16 : 4),
                      bottomRight: Radius.circular(isMe ? 4 : 16),
                    ),
                  ),
                  padding: const EdgeInsets.symmetric(
                    horizontal: 12,
                    vertical: 8,
                  ),
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.end,
                    children: [
                      Text(
                        message.text,
                        style: TextStyle(
                          color: isMe
                              ? theme.colorScheme.onPrimary
                              : theme.colorScheme.onSurfaceVariant,
                        ),
                      ),
                      const SizedBox(height: 4),
                      Row(
                        mainAxisSize: MainAxisSize.min,
                        children: [
                          Text(
                            _formatTime(message.timestamp),
                            style: TextStyle(
                              fontSize: 11,
                              color: (isMe
                                      ? theme.colorScheme.onPrimary
                                      : theme.colorScheme.onSurfaceVariant)
                                  .withOpacity(0.7),
                            ),
                          ),
                          if (isMe) ...[
                            const SizedBox(width: 4),
                            _buildStatusIcon(theme),
                          ],
                        ],
                      ),
                    ],
                  ),
                ),
              ),
              const SizedBox(width: 8),
              if (isMe && showAvatar)
                _buildAvatar()
              else if (isMe)
                const SizedBox(width: 40),
            ],
          ),
        ],
      ),
    );
  }
  
  Widget _buildAvatar() {
    return CircleAvatar(
      radius: 16,
      backgroundImage: message.senderAvatarUrl != null
          ? NetworkImage(message.senderAvatarUrl!)
          : null,
      child: message.senderAvatarUrl == null
          ? Text(message.senderName[0].toUpperCase())
          : null,
    );
  }
  
  Widget _buildTimestampDivider(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 8),
      child: Text(
        _formatDate(message.timestamp),
        style: Theme.of(context).textTheme.bodySmall?.copyWith(
              color: Theme.of(context).colorScheme.outline,
            ),
      ),
    );
  }
  
  Widget _buildStatusIcon(ThemeData theme) {
    IconData icon;
    Color color = theme.colorScheme.onPrimary.withOpacity(0.7);
    
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
        color = Colors.lightBlueAccent;
        break;
      case MessageStatus.failed:
        icon = Icons.error_outline;
        color = Colors.redAccent;
        break;
    }
    
    return Icon(icon, size: 14, color: color);
  }
  
  String _formatTime(DateTime timestamp) {
    return '${timestamp.hour.toString().padLeft(2, '0')}:'
        '${timestamp.minute.toString().padLeft(2, '0')}';
  }
  
  String _formatDate(DateTime timestamp) {
    final now = DateTime.now();
    final today = DateTime(now.year, now.month, now.day);
    final messageDate = DateTime(
      timestamp.year,
      timestamp.month,
      timestamp.day,
    );
    
    if (messageDate == today) {
      return 'Today';
    } else if (messageDate == today.subtract(const Duration(days: 1))) {
      return 'Yesterday';
    } else {
      return '${timestamp.day}/${timestamp.month}/${timestamp.year}';
    }
  }
}

enum MessageStatus {
  sending,
  sent,
  delivered,
  read,
  failed,
}
```
