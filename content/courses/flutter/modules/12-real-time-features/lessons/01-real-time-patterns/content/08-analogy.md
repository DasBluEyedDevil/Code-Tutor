---
type: "ANALOGY"
title: "Phone Calls vs. Text Messages vs. Walkie-Talkies"
---

Real-time communication patterns map directly to how humans communicate. **Polling** is like texting someone "Any news?" every 30 seconds -- it works, but it wastes effort and you always hear about things with a delay. **Server-Sent Events (SSE)** is like subscribing to a news channel that pushes updates to your phone whenever something happens -- efficient, but one-directional. **WebSockets** are like a phone call -- once connected, both sides can talk and listen simultaneously with no delay.

In your app, the pattern you choose depends on the conversation. A stock ticker that only sends price updates? SSE is perfect. A live chat where both users type and receive messages? WebSockets are the right fit. A dashboard that checks for new data every few minutes? Simple polling might be enough.

**The key insight is that "real-time" is a spectrum.** Not every feature needs the lowest latency possible. Choosing the right pattern means matching the communication style to the actual user experience you are building, not defaulting to the most complex option.
