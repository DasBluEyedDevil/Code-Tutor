---
type: "THEORY"
title: "Message and Conversation Models"
---


**Real-time Chat Data Structure**

The messaging system requires careful design for real-time performance:

**Conversation Model**

A conversation represents a chat thread between participants:

| Field | Purpose |
|-------|--------|
| `type` | Direct (1:1) or Group |
| `name` | Display name (for groups) |
| `participants` | List of user IDs |
| `lastMessage` | Preview for chat list |
| `unreadCounts` | Per-participant counts |

**Message Model**

Individual messages with rich content support:

| Field | Purpose |
|-------|--------|
| `conversationId` | Parent conversation |
| `senderId` | Message author |
| `content` | Text content |
| `messageType` | Text, image, file, system |
| `replyToId` | For threaded replies |
| `readBy` | Read receipt tracking |

**Participant Model**

Tracks each user's relationship to a conversation:

| Field | Purpose |
|-------|--------|
| `userId` | The participant |
| `conversationId` | The conversation |
| `role` | Admin, member, etc. |
| `lastReadAt` | For unread counts |
| `isMuted` | Notification preference |

**Performance Considerations**

1. **Denormalized counts**: Store unread counts to avoid counting queries
2. **Last message caching**: Store last message for list display
3. **Indexed timestamps**: Enable efficient pagination
4. **Separate read receipts**: Track detailed read status

