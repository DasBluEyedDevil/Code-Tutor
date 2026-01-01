---
type: "EXAMPLE"
title: "Message Protocol Definition"
---


**Message Model with Read Receipts**

Complete message structure with typing indicators:



```yaml
# server/lib/src/protocol/message.yaml
class: Message
table: messages
fields:
  # References
  conversationId: int, relation(parent=conversations)
  senderId: int, relation(parent=user_profiles)
  
  # Content
  content: String
  messageType: MessageType
  
  # Media attachments
  mediaUrls: List<String>?
  thumbnailUrl: String?
  fileName: String?
  fileSize: int?
  
  # Reply threading
  replyToId: int?, relation(parent=messages, optional)
  
  # State
  isEdited: bool
  isDeleted: bool
  deletedAt: DateTime?
  
  # Timestamps
  createdAt: DateTime
  updatedAt: DateTime?

indexes:
  message_conversation_idx:
    fields: conversationId, createdAt
  message_sender_idx:
    fields: senderId
  message_reply_idx:
    fields: replyToId

---

# server/lib/src/protocol/message_type.yaml
enum: MessageType
values:
  - text
  - image
  - video
  - file
  - audio
  - location
  - system    # Join, leave, renamed, etc.

---

# server/lib/src/protocol/message_read_receipt.yaml
class: MessageReadReceipt
table: message_read_receipts
fields:
  messageId: int, relation(parent=messages)
  userId: int, relation(parent=user_profiles)
  readAt: DateTime

indexes:
  read_receipt_unique_idx:
    fields: messageId, userId
    unique: true
  read_receipt_message_idx:
    fields: messageId

---

# server/lib/src/protocol/typing_indicator.yaml
# Note: This is a non-persistent model for real-time updates
class: TypingIndicator
fields:
  conversationId: int
  userId: int
  username: String
  isTyping: bool
  timestamp: DateTime
```
