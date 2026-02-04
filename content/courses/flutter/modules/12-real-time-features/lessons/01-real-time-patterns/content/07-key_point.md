---
type: KEY_POINT
---

- Polling sends repeated requests at fixed intervals -- simple but wasteful of bandwidth and battery
- Long-polling holds the connection open until the server has new data, reducing unnecessary requests
- WebSockets provide full-duplex, persistent connections for true real-time communication with minimal latency
- Server-Sent Events (SSE) allow one-way server-to-client streaming over HTTP -- simpler than WebSockets when bidirectional flow is not needed
- Choose the pattern based on requirements: polling for infrequent updates, WebSockets for chat/gaming, SSE for live feeds and notifications
