---
type: "THEORY"
title: "Broadcasting to Multiple Clients"
---

For real-time features like chat rooms, you need to broadcast messages to multiple connected clients. Serverpod's MessageCentral provides this capability.

**MessageCentral:**

The server's message central allows you to create channels that multiple clients can subscribe to. When you post a message to a channel, all subscribed clients receive it.

**Channel Patterns:**

- `chat-room-{roomId}`: Room-specific channels
- `user-{userId}`: User-specific notifications
- `global`: Broadcast to all users
- `topic-{topic}`: Topic-based subscriptions



```dart
// Broadcasting with MessageCentral
class NotificationEndpoint extends Endpoint {
  @override
  Future<void> streamOpened(StreamingSession session) async {
    // Get user from session authentication
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      session.close();
      return;
    }
    
    // Subscribe to user-specific channel
    session.messages.addListener(
      'user-$userId',
      (message) {
        // Forward to this client
        session.messages.postMessage('notification', message);
      },
    );
    
    // Subscribe to global announcements
    session.messages.addListener(
      'global',
      (message) {
        session.messages.postMessage('announcement', message);
      },
    );
  }
  
  // Regular endpoint to send notification
  Future<void> sendNotification(
    Session session,
    int targetUserId,
    String message,
  ) async {
    // Post to user's channel - all their connected devices receive it
    await server.messageCentral.postMessage(
      'user-$targetUserId',
      NotificationMessage(
        text: message,
        timestamp: DateTime.now(),
      ),
    );
  }
  
  // Broadcast to everyone
  Future<void> broadcastAnnouncement(
    Session session,
    String message,
  ) async {
    await server.messageCentral.postMessage(
      'global',
      AnnouncementMessage(
        text: message,
        timestamp: DateTime.now(),
      ),
    );
  }
}
```
