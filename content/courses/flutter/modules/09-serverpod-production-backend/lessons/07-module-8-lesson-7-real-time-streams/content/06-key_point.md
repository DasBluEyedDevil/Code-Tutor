---
type: "KEY_POINT"
title: "The Three Streaming Lifecycle Methods"
---

Every streaming endpoint in Serverpod can override three critical methods:

**1. streamOpened(StreamingSession session)**
Called once when a client establishes a streaming connection. Use this for:
- Logging connection events
- Initializing client-specific state
- Sending welcome messages
- Validating authentication
- Adding to global connection tracking

**2. handleStreamMessage(StreamingSession session, SerializableModel message)**
Called every time a client sends a message. Use this for:
- Routing messages by type
- Validating message content
- Broadcasting to other clients
- Persisting messages to database
- Triggering business logic

**3. streamClosed(StreamingSession session)**
Called when the connection ends (client disconnects, network failure, etc.). Use this for:
- Cleanup of client state
- Removing from subscription lists
- Notifying other users (user went offline)
- Logging disconnection events
- Releasing resources

**Important**: Always implement streamClosed to prevent memory leaks. If you add sessions to lists or maps in streamOpened, remove them in streamClosed.

