---
type: "ANALOGY"
title: "Real-World Analogy: Restaurant Service Styles"
---

**HTTP is like a Buffet Restaurant:**
- You (client) must walk to the buffet (send request)
- You get your food (receive response)
- You return to your table (connection closes)
- Want more? Walk to the buffet again (new request)
- The kitchen never brings food to you

**WebSocket is like Table Service:**
- You sit down and the waiter stays nearby (connection opens)
- You can order anytime (client sends message)
- The waiter brings food as soon as it is ready (server pushes data)
- The kitchen can send specials without you asking (server-initiated)
- You stay connected until you leave (persistent connection)

**Chat Application Example:**

With HTTP (buffet approach):
- Your app asks the server every 2 seconds: 'Any new messages?'
- Server responds: 'No' or 'Yes, here they are'
- This is called polling - inefficient and adds latency
- 100 users polling every 2 seconds = 50 requests/second to server

With WebSocket (table service approach):
- Your app connects once and stays connected
- Server pushes new messages instantly when they arrive
- No wasted requests asking 'anything new?'
- 100 users connected = 0 requests until someone sends a message
- Messages arrive in milliseconds, not seconds

