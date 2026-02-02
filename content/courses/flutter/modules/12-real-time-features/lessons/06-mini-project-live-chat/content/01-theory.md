---
type: "THEORY"
title: "Project Overview"
---

In this mini-project, you will build a complete live chat feature that combines everything learned in this module. This is a real-world feature found in marketplace apps, customer support systems, and social platforms.

**What We're Building:**

A fully functional chat system with:
- Real-time message delivery using Serverpod streams
- Typing indicators that show when others are composing
- Online/offline presence status
- Push notifications for new messages
- Offline message queuing
- Read receipts

**Architecture Recap:**

```
+------------------+     WebSocket      +------------------+
|   Flutter App    | <================> |    Serverpod     |
|                  |                    |     Server       |
| - ChatScreen     |     REST API       |                  |
| - MessageList    | <----------------> | - ChatEndpoint   |
| - TypingIndicator|                    | - MessageCentral |
| - ChatInput      |        FCM         | - PushService    |
+------------------+ <----------------- +------------------+
```

**Key Components:**

1. **Backend (Serverpod)**:
   - Chat endpoint with streaming methods
   - Message storage and retrieval
   - Presence tracking
   - Push notification triggers

2. **Frontend (Flutter)**:
   - Chat screen with reverse-scrolling list
   - Real-time message updates
   - Typing indicator integration
   - Optimistic message sending

**Data Flow for Sending a Message:**

1. User types and taps send
2. Message added to UI immediately (optimistic)
3. Message sent via stream to server
4. Server persists and broadcasts to recipient
5. Recipient receives via stream, UI updates
6. If recipient offline, push notification sent
7. Sender receives confirmation, status updates

