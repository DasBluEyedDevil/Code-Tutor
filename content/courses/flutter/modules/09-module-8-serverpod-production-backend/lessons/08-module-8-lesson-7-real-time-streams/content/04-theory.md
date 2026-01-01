---
type: "THEORY"
title: "Serverpod Streaming Architecture"
---

Serverpod provides a sophisticated streaming system built on WebSockets. Understanding its architecture helps you design better real-time features.

**Core Components:**

1. **StreamingSession**: The server-side representation of a connected client. Each client that opens a streaming connection gets a StreamingSession object. This session persists for the lifetime of the connection.

2. **Message Channels**: Named channels for organizing communication. Instead of one big pipe, you have separate channels like 'chat', 'notifications', 'presence'. Clients subscribe to channels they care about.

3. **SerializableModel Messages**: All messages sent through streams must be Serverpod models (defined in protocol YAML). This ensures type safety on both ends.

4. **Connection Lifecycle**: Serverpod manages connection state, including opening, message routing, error handling, and cleanup on disconnect.

**Data Flow:**

```
Flutter App                    Serverpod Server
    |                               |
    |---- WebSocket Connect ------->|
    |                               | (creates StreamingSession)
    |                               |
    |<--- Welcome Message ----------|
    |                               |
    |---- Subscribe to 'chat' ----->|
    |                               | (adds to channel subscribers)
    |                               |
    |<--- Chat Message 1 -----------|
    |<--- Chat Message 2 -----------|
    |                               |
    |---- Send Message ------------>|
    |                               | (broadcasts to all in channel)
    |                               |
    |---- Disconnect -------------->|
    |                               | (cleanup StreamingSession)
```

**Key Insight**: Unlike HTTP endpoints where each request is independent, streaming maintains state. The server knows which clients are connected, what channels they follow, and can push targeted messages to specific clients or groups.

