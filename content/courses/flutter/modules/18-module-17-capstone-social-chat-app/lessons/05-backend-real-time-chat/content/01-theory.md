---
type: "THEORY"
title: "Real-Time Architecture for Chat"
---


**Building Real-Time Chat with WebSockets**

Chat applications demand instant message delivery. Unlike traditional HTTP request/response, chat requires persistent connections where the server can push messages to clients the moment they arrive. In this lesson, we'll build a production-ready real-time chat backend using Serverpod's streaming capabilities.

**WebSocket vs HTTP Polling Trade-offs**

| Approach | How it Works | Latency | Server Load | Battery Impact |
|----------|--------------|---------|-------------|----------------|
| **HTTP Polling** | Client requests every N seconds | High (N seconds) | High (constant requests) | High |
| **Long Polling** | Server holds request until data ready | Medium | Medium | Medium |
| **WebSocket** | Persistent bidirectional connection | Low (instant) | Low (event-driven) | Low |
| **Server-Sent Events** | Server pushes, client receives | Low | Low | Low (one-way only) |

WebSockets are ideal for chat because:
- **Bidirectional**: Both client and server can send messages anytime
- **Low latency**: Messages arrive instantly without polling delay
- **Efficient**: Single connection reused for all messages
- **Real-time events**: Typing indicators, read receipts, presence updates

**Serverpod's Streaming Model**

Serverpod provides `StreamingSession` for real-time communication:

```
Client                          Server
   |                              |
   |-------- Connect ------------>|
   |                              |
   |<------- Stream Open ---------|  (StreamingSession created)
   |                              |
   |-------- Send Message ------->|  (handleStreamMessage)
   |                              |
   |<------- Broadcast -----------|  (sendStreamMessage)
   |<------- Broadcast -----------|
   |                              |
   |-------- Close -------------->|  (streamClosed)
   |                              |
```

**Message Delivery Guarantees**

| Guarantee | Description | Use Case |
|-----------|-------------|----------|
| **At-most-once** | Message may be lost, never duplicated | Non-critical notifications |
| **At-least-once** | Message delivered, may be duplicated | Most chat apps (with dedup) |
| **Exactly-once** | Message delivered exactly once | Financial transactions |

We'll implement **at-least-once** delivery with client-side deduplication.

**Connection Lifecycle Management**

```
┌─────────────────────────────────────────────────────────┐
│                  Connection States                       │
├─────────────────────────────────────────────────────────┤
│                                                          │
│   DISCONNECTED ──connect()──> CONNECTING                │
│        ↑                           │                     │
│        │                      (success)                  │
│        │                           ↓                     │
│   (error/timeout)            CONNECTED                  │
│        │                           │                     │
│        │                   (network loss)                │
│        │                           ↓                     │
│        └─────────────── RECONNECTING                    │
│                                    │                     │
│                             (max retries)                │
│                                    ↓                     │
│                               FAILED                     │
└─────────────────────────────────────────────────────────┘
```

**Key Architecture Decisions**

1. **Message persistence first**: Store message before broadcasting
2. **Eventual consistency**: Clients sync on reconnect
3. **Optimistic updates**: Show sent message immediately
4. **Idempotent operations**: Handle duplicate messages gracefully
5. **Graceful degradation**: Fall back to polling if WebSocket fails

