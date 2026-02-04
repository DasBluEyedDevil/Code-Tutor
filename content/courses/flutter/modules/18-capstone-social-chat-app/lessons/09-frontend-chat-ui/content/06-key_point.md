---
type: KEY_POINT
---

- Use a reversed `ListView.builder` for messages so the latest message is always visible at the bottom without manual scrolling
- Manage WebSocket connection lifecycle in a Riverpod provider: connect on screen open, reconnect on failure, close on dispose
- Show conversations list with last message preview, unread badge count, and online status indicator for each participant
- Message input supports multi-line text, send button enablement based on content, and optional image attachment
- Handle connection loss gracefully by queuing unsent messages locally and delivering them when the WebSocket reconnects
