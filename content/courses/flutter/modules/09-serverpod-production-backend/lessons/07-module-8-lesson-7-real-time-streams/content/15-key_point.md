---
type: "KEY_POINT"
title: "Summary: Real-Time Streaming Best Practices"
---

After completing this lesson, remember these key principles:

**Architecture:**
- Use WebSocket streaming for real-time updates (chat, notifications, presence)
- Use HTTP endpoints for CRUD operations and fetching history
- Combine both: HTTP for reliability, WebSocket for speed

**Server-Side:**
- Always implement streamClosed to clean up resources
- Use a central broadcast manager for multi-client messaging
- Keep message handlers fast - offload heavy work
- Persist important messages to the database
- Track user sessions for targeted delivery

**Client-Side:**
- Implement reconnection with exponential backoff
- Show connection status to users
- Use optimistic UI for better perceived performance
- Queue messages when disconnected, send when reconnected
- Clean up subscriptions on dispose

**Patterns:**
- Channel-based subscriptions for organizing conversations
- Typing indicators with debouncing and auto-timeout
- Read receipts for message delivery confirmation
- Presence system for online/offline status

**In the Next Lesson:**
You will learn about Serverpod authentication, including email/password login, OAuth providers, and securing your streaming endpoints with user authentication.

