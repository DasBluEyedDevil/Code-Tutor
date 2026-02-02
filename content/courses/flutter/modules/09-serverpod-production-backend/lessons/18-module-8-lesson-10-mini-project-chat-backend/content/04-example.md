---
type: "EXAMPLE"
title: "Step 2: Define the ChatRoom Model"
---

Chat rooms are containers for conversations. They can be direct messages (2 users) or group chats (multiple users).



```yaml
// File: lib/src/protocol/chat_room.yaml
// Defines the chat room structure

class: ChatRoom
table: chat_rooms
fields:
  # Room identification
  name: String?          # null for DMs, set for groups
  description: String?
  avatarUrl: String?
  
  # Room type
  isGroup: bool          # true = group chat, false = direct message
  
  # Creator tracking
  createdByUserId: int
  
  # Timestamps
  createdAt: DateTime
  lastMessageAt: DateTime?

indexes:
  # Sort rooms by recent activity
  chat_room_last_message_idx:
    fields: lastMessageAt

---

// File: lib/src/protocol/chat_member.yaml
// Links users to chat rooms (many-to-many)

class: ChatMember
table: chat_members
fields:
  # Foreign keys
  chatRoomId: int
  userId: int            # References ChatUser.id
  
  # Member metadata
  role: String           # 'admin', 'member'
  nickname: String?      # Custom nickname in this room
  
  # Notification settings
  isMuted: bool
  
  # Timestamps
  joinedAt: DateTime
  lastReadAt: DateTime?  # For unread message tracking

indexes:
  # Fast lookup: which rooms is a user in?
  chat_member_user_idx:
    fields: userId
  
  # Fast lookup: who is in this room?
  chat_member_room_idx:
    fields: chatRoomId
  
  # Prevent duplicate memberships
  chat_member_unique_idx:
    fields: chatRoomId, userId
    unique: true
```
