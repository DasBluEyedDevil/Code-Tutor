---
type: KEY_POINT
---

- Serverpod streaming endpoints use `StreamingSession` instead of `Session` to maintain persistent WebSocket connections
- Register message handlers on the server with `session.messages.addListener()` to process incoming client messages
- Send data from server to client with `session.messages.postMessage()` for targeted or broadcast delivery
- On the client side, use `client.openStreamingConnection()` and listen to the returned stream for incoming messages
- Always implement reconnection logic on the client -- WebSocket connections can drop silently due to network changes
