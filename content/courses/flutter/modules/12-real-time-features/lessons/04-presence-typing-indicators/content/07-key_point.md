---
type: KEY_POINT
---

- Track online/offline status by marking users as online on stream connect and offline on disconnect or heartbeat timeout
- Broadcast presence changes only to subscribers (e.g., contacts or conversation participants) to minimize unnecessary network traffic
- Debounce typing indicators: start a short timer on each keystroke and send "stopped typing" when the timer expires without new input
- Display typing status with animated dots ("Alice is typing...") below the message list for clear visual feedback
- Use heartbeat pings at regular intervals to detect stale connections that did not trigger a proper disconnect event
