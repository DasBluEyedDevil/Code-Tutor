---
type: KEY_POINT
---

- Combine REST endpoints (for loading message history) with WebSocket streams (for real-time delivery) in a single chat module
- Implement optimistic message sending: show the message in the UI immediately and confirm or rollback after server response
- Store conversations, messages, and participants in separate database tables with proper foreign key relationships
- Typing indicators, presence status, and push notifications layer on top of the core messaging to create a polished chat experience
- Test the full flow end-to-end: connect, send message, receive message, disconnect, reconnect, and verify message persistence
