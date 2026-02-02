---
type: "THEORY"
title: "Auto-Reconnection Handling"
---

Network connections are unreliable. Users move between WiFi and cellular, go through tunnels, or have momentary dropouts. A robust real-time app must handle disconnections gracefully.

**Serverpod's Built-in Reconnection:**

Serverpod's client includes automatic reconnection logic. When you open a streaming connection, you can configure reconnection behavior:

```dart
await client.openStreamingConnection(
  disconnectOnLostInternetConnection: false, // Keep trying to reconnect
);
```

**Manual Reconnection Strategy:**

For more control, implement your own reconnection logic:

```dart
class ReconnectingChatService {
  final Client _client;
  Timer? _reconnectTimer;
  int _reconnectAttempts = 0;
  static const _maxReconnectAttempts = 5;
  static const _reconnectDelays = [1, 2, 4, 8, 16]; // Seconds
  
  Future<void> _handleDisconnection() async {
    if (_reconnectAttempts >= _maxReconnectAttempts) {
      // Give up and notify user
      _notifyConnectionFailed();
      return;
    }
    
    // Exponential backoff
    final delay = _reconnectDelays[_reconnectAttempts];
    _reconnectAttempts++;
    
    _reconnectTimer = Timer(Duration(seconds: delay), () async {
      try {
        await connect();
        _reconnectAttempts = 0; // Reset on success
      } catch (e) {
        _handleDisconnection(); // Try again
      }
    });
  }
}
```

**Best Practices for Reconnection:**

1. **Exponential Backoff**: Wait longer between each attempt (1s, 2s, 4s, 8s...). This prevents overwhelming the server when it comes back online.

2. **Maximum Attempts**: Set a limit. After 5-10 failures, stop trying and show the user an error.

3. **Visual Feedback**: Show connection status in your UI. Users should know if they are offline.

4. **Queue Messages**: Store unsent messages locally. Send them when reconnected.

5. **Request Missed Messages**: After reconnecting, ask the server for messages sent while you were offline.

