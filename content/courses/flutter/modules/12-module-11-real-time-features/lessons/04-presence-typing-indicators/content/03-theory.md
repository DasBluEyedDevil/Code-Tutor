---
type: "THEORY"
title: "Presence Broadcasting"
---

When a user's presence changes, you need to notify other users who care about that status. This requires an efficient pub/sub system that only sends updates to relevant subscribers.

**Subscription Model:**

Users subscribe to presence updates for specific users (e.g., contacts, conversation participants). When someone's status changes, only their subscribers receive the update.

**Efficient Broadcasting:**

- Maintain a map of userId -> subscribers
- When status changes, iterate only through subscribers
- Batch updates when multiple users change status
- Use separate channels for presence vs. chat messages

**Presence Stream Endpoint:**

Create a dedicated stream for presence updates that clients subscribe to.



```dart
// presence_endpoint.dart (Server)
class PresenceEndpoint extends Endpoint {
  // Subscribers: userId -> Set of streaming sessions watching them
  static final Map<int, Set<StreamingSession>> _subscribers = {};
  
  // Subscribe to presence updates for specific users
  Future<void> subscribeToPresence(
    Session session,
    List<int> userIds,
  ) async {
    final streamingSession = session as StreamingSession;
    
    for (final userId in userIds) {
      _subscribers.putIfAbsent(userId, () => {});
      _subscribers[userId]!.add(streamingSession);
    }
    
    // Send current presence state immediately
    final currentPresence = PresenceManager.getPresenceForUsers(userIds);
    for (final presence in currentPresence.values) {
      await streamingSession.sendMessage(
        PresenceUpdate(
          userId: presence.userId,
          status: presence.status,
          lastSeen: presence.lastSeen,
        ),
      );
    }
  }
  
  // Unsubscribe from presence updates
  Future<void> unsubscribeFromPresence(
    Session session,
    List<int> userIds,
  ) async {
    final streamingSession = session as StreamingSession;
    
    for (final userId in userIds) {
      _subscribers[userId]?.remove(streamingSession);
    }
  }
  
  // Broadcast presence change to all subscribers
  static Future<void> broadcastPresenceChange(
    int userId,
    PresenceStatus status,
  ) async {
    final subscribers = _subscribers[userId] ?? {};
    final update = PresenceUpdate(
      userId: userId,
      status: status,
      lastSeen: DateTime.now(),
    );
    
    // Send to all subscribers
    for (final session in subscribers.toList()) {
      try {
        await session.sendMessage(update);
      } catch (e) {
        // Session may be closed, remove it
        subscribers.remove(session);
      }
    }
  }
}

// Client-side subscription
class PresenceService {
  final Client _client;
  StreamSubscription? _presenceSubscription;
  final _presenceController = StreamController<PresenceUpdate>.broadcast();
  
  Stream<PresenceUpdate> get presenceUpdates => _presenceController.stream;
  
  PresenceService(this._client);
  
  Future<void> subscribeToUsers(List<int> userIds) async {
    _presenceSubscription?.cancel();
    
    final stream = await _client.presence.subscribeToPresence(userIds);
    _presenceSubscription = stream.listen(
      (update) => _presenceController.add(update),
      onError: (e) => print('Presence error: $e'),
    );
  }
  
  void dispose() {
    _presenceSubscription?.cancel();
    _presenceController.close();
  }
}
```
