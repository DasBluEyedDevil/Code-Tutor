---
type: "THEORY"
title: "Serverpod Streaming Architecture"
---

Serverpod's streaming architecture consists of three main components: the StreamingSession, streaming endpoints, and the client-side connection.

**StreamingSession:**

Unlike regular method calls that use a Session, streaming endpoints receive a StreamingSession. This session maintains a persistent WebSocket connection and provides methods for sending messages to the client.

**How It Works:**

1. Client opens a streaming connection to the server
2. Server creates a StreamingSession for this connection
3. Either side can send messages through the connection
4. Connection stays open until explicitly closed
5. Server can broadcast to multiple connected clients

**Key Differences from Regular Endpoints:**

| Regular Endpoint | Streaming Endpoint |
|-----------------|-------------------|
| Request-Response | Continuous connection |
| Session object | StreamingSession object |
| Returns single value | Sends multiple messages |
| Short-lived | Long-lived |
| HTTP-based | WebSocket-based |

**Architecture Diagram:**

```
Flutter App          Serverpod Server
    |                      |
    |--openStreamingConnection-->|
    |                      |
    |<---StreamingSession---|
    |                      |
    |<----message 1--------|
    |<----message 2--------|
    |<----message 3--------|
    |                      |
    |----sendMessage------>|
    |                      |
    |<----response---------|
    |                      |
    |----close------------>|
```

