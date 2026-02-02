---
type: "EXAMPLE"
title: "Conversation and Message Models"
---


**Defining Chat Data Models**

A robust chat system needs well-designed models for conversations and messages. We'll support both direct messages and group chats.



```yaml
# server/lib/src/protocol/conversation.yaml
class: Conversation
table: conversations
fields:
  # Conversation type
  type: String  # 'direct' or 'group'
  
  # Group chat metadata (null for direct messages)
  name: String?
  description: String?
  avatarUrl: String?
  
  # Creator (for groups)
  createdById: int?
  
  # Settings
  isArchived: bool
  isMuted: bool
  
  # Denormalized for quick display
  lastMessageAt: DateTime?
  lastMessagePreview: String?
  lastMessageSenderId: int?
  
  # Participant count (for groups)
  participantCount: int
  
  # Timestamps
  createdAt: DateTime
  updatedAt: DateTime?

indexes:
  conversation_updated_idx:
    fields: lastMessageAt
  conversation_type_idx:
    fields: type, createdAt

---

# server/lib/src/protocol/conversation_participant.yaml
class: ConversationParticipant
table: conversation_participants
fields:
  conversationId: int, relation(parent=conversations)
  userId: int, relation(parent=user_profiles)
  
  # Role in conversation
  role: String  # 'owner', 'admin', 'member'
  
  # User-specific settings
  nickname: String?  # Custom name for this chat
  isMuted: bool
  mutedUntil: DateTime?
  
  # Read tracking
  lastReadAt: DateTime?
  lastReadMessageId: int?
  unreadCount: int
  
  # Status
  isActive: bool  # False if user left the conversation
  leftAt: DateTime?
  
  # Timestamps
  joinedAt: DateTime
  updatedAt: DateTime?

indexes:
  participant_user_idx:
    fields: userId, isActive, lastReadAt
  participant_conversation_idx:
    fields: conversationId, isActive
  participant_unique_idx:
    fields: conversationId, userId
    unique: true

---

# server/lib/src/protocol/message.yaml
class: Message
table: messages
fields:
  conversationId: int, relation(parent=conversations)
  senderId: int, relation(parent=user_profiles)
  
  # Content
  content: String
  messageType: String  # 'text', 'image', 'video', 'audio', 'file', 'system'
  
  # Media attachment (for non-text messages)
  mediaUrl: String?
  mediaThumbnailUrl: String?
  mediaMetadata: String?  # JSON: {width, height, duration, size, mimeType}
  
  # Reply to another message
  replyToMessageId: int?
  
  # Delivery status
  status: String  # 'sending', 'sent', 'delivered', 'read', 'failed'
  
  # Client-generated ID for deduplication
  clientMessageId: String
  
  # Editing
  isEdited: bool
  editedAt: DateTime?
  originalContent: String?  # Store original if edited
  
  # Deletion
  isDeleted: bool
  deletedAt: DateTime?
  deletedBy: int?  # Who deleted it
  
  # Timestamps
  createdAt: DateTime
  sentAt: DateTime?  # When server confirmed receipt

indexes:
  message_conversation_idx:
    fields: conversationId, createdAt
  message_sender_idx:
    fields: senderId, createdAt
  message_client_id_idx:
    fields: clientMessageId
    unique: true
  message_status_idx:
    fields: conversationId, status

---

# server/lib/src/protocol/message_status.yaml
# Tracks delivery and read status per recipient
class: MessageStatus
table: message_statuses
fields:
  messageId: int, relation(parent=messages)
  userId: int, relation(parent=user_profiles)
  
  # Status tracking
  deliveredAt: DateTime?
  readAt: DateTime?
  
indexes:
  message_status_idx:
    fields: messageId, userId
    unique: true
  user_status_idx:
    fields: userId, messageId

---

# server/lib/src/protocol/enums/message_status_enum.dart
/// Message delivery status enum
enum MessageStatusEnum {
  /// Message is being sent from client
  sending,
  
  /// Message received by server, stored in database
  sent,
  
  /// Message delivered to recipient's device
  delivered,
  
  /// Message has been read by recipient
  read,
  
  /// Message failed to send
  failed,
}

extension MessageStatusEnumExtension on MessageStatusEnum {
  String get value {
    switch (this) {
      case MessageStatusEnum.sending:
        return 'sending';
      case MessageStatusEnum.sent:
        return 'sent';
      case MessageStatusEnum.delivered:
        return 'delivered';
      case MessageStatusEnum.read:
        return 'read';
      case MessageStatusEnum.failed:
        return 'failed';
    }
  }
  
  static MessageStatusEnum fromString(String value) {
    switch (value) {
      case 'sending':
        return MessageStatusEnum.sending;
      case 'sent':
        return MessageStatusEnum.sent;
      case 'delivered':
        return MessageStatusEnum.delivered;
      case 'read':
        return MessageStatusEnum.read;
      case 'failed':
        return MessageStatusEnum.failed;
      default:
        return MessageStatusEnum.sending;
    }
  }
  
  /// Check if status indicates message was successfully sent
  bool get isSent => this != MessageStatusEnum.sending && 
                     this != MessageStatusEnum.failed;
  
  /// Check if message reached the recipient
  bool get isDelivered => this == MessageStatusEnum.delivered ||
                          this == MessageStatusEnum.read;
}
```
