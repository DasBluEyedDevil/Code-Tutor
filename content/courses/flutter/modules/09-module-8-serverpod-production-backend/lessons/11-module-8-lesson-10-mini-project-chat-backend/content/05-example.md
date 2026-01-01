---
type: "EXAMPLE"
title: "Step 3: Define the ChatMessage Model"
---

Messages are the core of any chat application. Our model supports text, images, files, and system messages.



```yaml
// File: lib/src/protocol/chat_message.yaml
// The core message model

class: ChatMessage
table: chat_messages
fields:
  # Relationships
  chatRoomId: int
  senderId: int          # References ChatUser.id
  
  # Content
  content: String        # Text content or system message
  messageType: String    # 'text', 'image', 'file', 'system'
  
  # Attachments
  attachmentUrl: String?
  attachmentName: String?
  attachmentSize: int?   # File size in bytes
  attachmentMimeType: String?
  
  # Reply threading (optional)
  replyToMessageId: int?
  
  # Timestamps
  createdAt: DateTime
  editedAt: DateTime?
  
  # Soft delete
  isDeleted: bool

indexes:
  # Get messages for a room, ordered by time
  chat_message_room_time_idx:
    fields: chatRoomId, createdAt
  
  # Get messages by sender
  chat_message_sender_idx:
    fields: senderId

---

// File: lib/src/protocol/typing_indicator.yaml
// Real-time typing status (not persisted, streaming only)

class: TypingIndicator
fields:
  chatRoomId: int
  userId: int
  username: String
  isTyping: bool

---

// File: lib/src/protocol/chat_event.yaml
// Generic event wrapper for streaming

class: ChatEvent
fields:
  eventType: String      # 'message', 'typing', 'presence', 'room_update'
  chatRoomId: int?
  payload: String        # JSON-encoded event data
  timestamp: DateTime
```
