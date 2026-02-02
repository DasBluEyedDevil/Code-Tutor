---
type: "KEY_POINT"
title: "Real-Time Chat Checklist"
---


**Production-Ready Chat Implementation Checklist**

**WebSocket Streaming**
- [ ] Authenticate connections on stream open
- [ ] Track connection lifecycle (open, close, reconnect)
- [ ] Handle graceful disconnection and cleanup
- [ ] Implement heartbeat/ping-pong for connection health
- [ ] Support message catchup on reconnection

**Message Handling**
- [ ] Client-generated message IDs for deduplication
- [ ] Idempotent message creation
- [ ] Message persistence before broadcasting
- [ ] Optimistic UI updates with confirmation
- [ ] Error handling with retry logic

**Delivery Status**
- [ ] Track sent, delivered, and read states
- [ ] Bulk read status updates for efficiency
- [ ] Real-time status broadcasting to senders
- [ ] Handle offline message queuing

**Typing Indicators**
- [ ] Debounce typing events on client
- [ ] Auto-expire typing status (5 second timeout)
- [ ] Only broadcast to conversation participants
- [ ] Stop indicator on message send or input clear

**Presence System**
- [ ] Track online/offline/away/busy states
- [ ] Heartbeat-based presence verification
- [ ] Last seen timestamps for offline users
- [ ] Broadcast presence changes to relevant users

**Scalability**
- [ ] Stateless server design for horizontal scaling
- [ ] External session store (Redis) for multi-server
- [ ] Connection manager for routing messages
- [ ] Rate limiting for spam prevention

**Security**
- [ ] Verify conversation membership on every action
- [ ] Validate message content and size
- [ ] Sanitize user input before storage
- [ ] Encrypt sensitive data in transit and at rest

