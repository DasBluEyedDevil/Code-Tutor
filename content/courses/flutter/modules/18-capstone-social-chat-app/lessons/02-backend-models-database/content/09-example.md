---
type: "EXAMPLE"
title: "Conversation Protocol Definition"
---


**Conversation and Participant Models**

Define the conversation structure:



```yaml
# server/lib/src/protocol/conversation.yaml
class: Conversation
table: conversations
fields:
  # Conversation metadata
  conversationType: ConversationType
  name: String?          # For group chats
  description: String?   # Group description
  avatarUrl: String?     # Group avatar
  
  # Creator (for groups)
  createdById: int?, relation(parent=user_profiles, optional)
  
  # Cached last message for list display
  lastMessageId: int?, relation(parent=messages, optional)
  lastMessageAt: DateTime?
  lastMessagePreview: String?
  
  # State
  isArchived: bool
  isDeleted: bool
  
  # Timestamps
  createdAt: DateTime
  updatedAt: DateTime?

indexes:
  conversation_last_message_idx:
    fields: lastMessageAt
  conversation_type_idx:
    fields: conversationType

---

# server/lib/src/protocol/conversation_type.yaml
enum: ConversationType
values:
  - direct    # One-on-one
  - group     # Multiple participants
  - channel   # Public broadcast

---

# server/lib/src/protocol/participant.yaml
class: Participant
table: participants
fields:
  # References
  conversationId: int, relation(parent=conversations)
  userId: int, relation(parent=user_profiles)
  
  # Role in conversation
  role: ParticipantRole
  
  # Read tracking
  lastReadAt: DateTime?
  lastReadMessageId: int?
  unreadCount: int
  
  # Notification settings
  isMuted: bool
  mutedUntil: DateTime?
  
  # State
  hasLeft: bool
  leftAt: DateTime?
  
  # Timestamps
  joinedAt: DateTime

indexes:
  participant_unique_idx:
    fields: conversationId, userId
    unique: true
  participant_user_idx:
    fields: userId, hasLeft
  participant_conversation_idx:
    fields: conversationId

---

# server/lib/src/protocol/participant_role.yaml
enum: ParticipantRole
values:
  - owner     # Created the group, full control
  - admin     # Can manage members
  - member    # Regular participant
```
