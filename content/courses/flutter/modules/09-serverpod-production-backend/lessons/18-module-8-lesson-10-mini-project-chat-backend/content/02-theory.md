---
type: "THEORY"
title: "Project Architecture Overview"
---

Before writing code, let us understand the architecture of our chat backend. A well-designed architecture makes the code easier to understand, maintain, and extend.

**Data Models:**

We need four core models:

1. **User**: Represents registered users with authentication credentials
   - id, email, username, passwordHash, avatarUrl, createdAt, lastSeenAt

2. **ChatRoom**: Represents a conversation space (direct message or group)
   - id, name, isGroup, createdAt, createdByUserId

3. **ChatMember**: Links users to chat rooms (many-to-many relationship)
   - id, chatRoomId, userId, joinedAt, role (admin/member)

4. **ChatMessage**: Individual messages within a room
   - id, chatRoomId, senderId, content, messageType, attachmentUrl, createdAt, editedAt

**Endpoints:**

We will create four endpoint classes:

1. **UserEndpoint**: Registration, profile updates, user search
2. **ChatRoomEndpoint**: Create rooms, list rooms, manage members
3. **ChatMessageEndpoint**: Send messages, get history, edit/delete messages
4. **ChatStreamEndpoint**: Real-time messaging, typing indicators, presence

**Data Flow:**

```
[Flutter App] <--WebSocket--> [ChatStreamEndpoint]
                                      |
                              [BroadcastManager]
                                      |
                    +--------+--------+--------+
                    |        |        |        |
               [Room 1] [Room 2] [Room 3] [Room N]
                    |        |        |        |
              [Users]   [Users]  [Users]  [Users]
```

Messages flow through the stream endpoint to a broadcast manager that routes them to the correct chat room subscribers. This design allows efficient message delivery without unnecessary database queries for each message.

