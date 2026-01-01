---
type: "EXAMPLE"
title: "Shared Models Structure"
---


**Protocol Definition**

Serverpod uses protocol files to define shared models. These generate serialization code automatically:



```yaml
# shared/lib/src/protocol/user_profile.yaml
class: UserProfile
fields:
  id: int
  email: String
  username: String
  displayName: String?
  avatarUrl: String?
  bio: String?
  isOnline: bool
  lastSeenAt: DateTime?
  createdAt: DateTime

---

# shared/lib/src/protocol/message.yaml
class: Message
fields:
  id: int
  roomId: int
  senderId: int
  content: String
  messageType: MessageType
  replyToId: int?
  isEdited: bool
  isDeleted: bool
  readBy: List<int>
  createdAt: DateTime
  updatedAt: DateTime?

---

# shared/lib/src/protocol/message_type.yaml  
enum: MessageType
values:
  - text
  - image
  - file
  - system

---

# shared/lib/src/protocol/chat_room.yaml
class: ChatRoom
fields:
  id: int
  name: String?
  description: String?
  roomType: RoomType
  createdById: int
  avatarUrl: String?
  memberIds: List<int>
  lastMessageAt: DateTime?
  createdAt: DateTime

---

# shared/lib/src/protocol/room_type.yaml
enum: RoomType
values:
  - direct     # One-on-one chat
  - group      # Group chat
  - channel    # Public channel
```
