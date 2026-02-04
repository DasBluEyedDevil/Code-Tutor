---
type: WARNING
---

**Stale presence data creates ghost users.** If a client crashes or loses network without a clean disconnect, the server still shows that user as "online" indefinitely. Relying solely on connection close events is not reliable.

Implement server-side heartbeat validation:
- Require clients to send a heartbeat ping every 15-30 seconds
- On the server, mark users as offline if no heartbeat is received within 2x the interval
- Run a periodic cleanup task that checks for stale sessions

Without heartbeats, your app may show users as "online" hours after they closed the app. This erodes user trust in the presence feature. Similarly, debounce typing indicators -- send "stopped typing" after 3-5 seconds of inactivity, and never show "typing" for more than 30 seconds regardless of input (in case the stop event was lost).
