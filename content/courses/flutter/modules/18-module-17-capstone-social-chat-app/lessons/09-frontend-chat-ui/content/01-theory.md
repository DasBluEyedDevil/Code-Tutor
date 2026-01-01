---
type: "THEORY"
title: "Chat UI Architecture"
---


**Real-Time Messaging Patterns**

Building a chat UI requires careful attention to real-time data handling, efficient rendering of message lists, and responsive user feedback. The architecture must handle WebSocket connections, message state management, and graceful degradation when connectivity is poor.

**WebSocket Connection Management**

Maintaining a persistent WebSocket connection is critical for real-time chat:

| State | Behavior | UI Feedback |
|-------|----------|-------------|
| **Connecting** | Initial handshake | Show connecting indicator |
| **Connected** | Ready to send/receive | Green status dot |
| **Reconnecting** | Auto-retry with backoff | Yellow warning banner |
| **Disconnected** | Manual retry required | Red offline indicator |

**Connection Lifecycle**

```
┌─────────────────────────────────────────────────────────────┐
│                  WebSocket Connection Flow                  │
├─────────────────────────────────────────────────────────────┤
│                                                             │
│   App Start  ──>  Connect to WS Server                      │
│         │                │                                  │
│         v                v                                  │
│   Auth Token  ──>  Authenticate Session                     │
│         │                │                                  │
│         v                v                                  │
│   Subscribe   ──>  Join Conversation Channels               │
│         │                │                                  │
│         v                v                                  │
│   Listen      ──>  Handle Incoming Messages                 │
│         │                                                   │
│         v                                                   │
│   Disconnect? ──>  Exponential Backoff Retry                │
│                                                             │
└─────────────────────────────────────────────────────────────┘
```

**Reversed ListView Pattern**

Chat messages are displayed with newest at the bottom using a reversed ListView:

| Property | Purpose |
|----------|--------|
| `reverse: true` | Starts scroll at bottom, newest messages visible |
| `shrinkWrap` | Avoid when possible for performance |
| `cacheExtent` | Preload messages above viewport |
| `ScrollController` | Programmatic scroll-to-bottom |

**Message List Optimization**

- Use `ListView.builder` with `itemExtent` hint when possible
- Implement message grouping by date
- Virtualize long conversations
- Lazy load images in messages
- Cache rendered message widgets

**Typing Indicators**

Typing indicators provide real-time feedback about active participants:

| Scenario | Display |
|----------|--------|
| One person typing | "John is typing..." |
| Two people typing | "John and Jane are typing..." |
| Multiple people | "Several people are typing..." |
| Timeout | Clear after 5 seconds of no updates |

**Read Receipts Display**

Message status progression:

```
Sending (clock icon) → Sent (single check) → Delivered (double check) → Read (blue double check)
```

**State Management for Chat**

| Provider | Responsibility |
|----------|---------------|
| `connectionProvider` | WebSocket connection state |
| `conversationsProvider` | List of all conversations |
| `messagesProvider(chatId)` | Messages for specific chat |
| `typingProvider(chatId)` | Who is currently typing |
| `unreadCountProvider` | Total unread message count |

