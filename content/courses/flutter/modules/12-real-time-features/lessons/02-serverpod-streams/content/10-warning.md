---
type: WARNING
---

**WebSocket connections drop silently.** Network changes (Wi-Fi to cellular, entering a tunnel, device sleep) close WebSocket connections without triggering an error callback. If you do not implement reconnection logic, your app will appear connected while receiving no updates.

Implement automatic reconnection with exponential backoff:

```dart
// Reconnection pattern
int retryCount = 0;
void connect() async {
  try {
    await client.openStreamingConnection();
    retryCount = 0; // Reset on success
  } catch (e) {
    final delay = Duration(seconds: min(30, pow(2, retryCount).toInt()));
    retryCount++;
    await Future.delayed(delay);
    connect(); // Retry
  }
}
```

Also add a heartbeat ping at regular intervals (e.g., every 30 seconds) and treat a missing pong as a disconnection. Show a "reconnecting..." indicator in the UI so users know the app is recovering.
