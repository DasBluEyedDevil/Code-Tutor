---
type: "THEORY"
title: "HTTP vs WebSocket: Understanding the Difference"
---

Before diving into Serverpod streams, you need to understand why WebSockets exist and when to use them.

**Traditional HTTP: Request-Response Model**

HTTP is like sending letters. Your app (the client) sends a request, waits for a response, and the connection closes. If you want new data, you must send another request.

HTTP Characteristics:
- Client initiates every interaction
- Server cannot push data to client
- Connection closes after each response
- Simple, stateless, cacheable

**WebSocket: Persistent Two-Way Connection**

WebSockets are like a phone call. Once connected, both sides can speak at any time without hanging up and calling again.

WebSocket Characteristics:
- Connection stays open (persistent)
- Server CAN push data to client anytime
- Both sides can send messages freely
- Lower latency for real-time updates
- More complex to manage

**When to Use Each:**

Use HTTP for:
- Fetching data on user action (load product list)
- Submitting forms (create account)
- CRUD operations (create, read, update, delete)
- Operations that happen occasionally

Use WebSocket for:
- Chat messages (instant delivery)
- Live notifications (new follower, new message)
- Collaborative editing (multiple users, same document)
- Live dashboards (stock prices, game scores)
- Presence indicators (who is online)
- Real-time sync (changes appear instantly everywhere)

