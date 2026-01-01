---
type: "WARNING"
title: "Common Real-Time Chat Mistakes"
---


**1. Blocking on Message Broadcast**

Problem: Waiting for all recipients to receive message before responding.

```dart
// BAD - Blocks until all broadcasts complete
for (final recipient in recipients) {
  await sendToUser(recipient, message);
}
return message;

// GOOD - Fire and forget for broadcasts
for (final recipient in recipients) {
  sendToUser(recipient, message);  // No await
}
return message;
```

**2. No Message Deduplication**

Problem: Duplicate messages when client retries after network error.

```dart
// BAD - Creates duplicate messages on retry
final message = await Message.db.insertRow(session, newMessage);

// GOOD - Idempotent with client message ID
final existing = await Message.db.findFirstRow(
  session,
  where: (t) => t.clientMessageId.equals(clientId),
);
if (existing != null) {
  return existing;  // Already processed
}
final message = await Message.db.insertRow(session, newMessage);
```

**3. Broadcasting to Disconnected Users**

Problem: Trying to send to users who are no longer connected.

```dart
// BAD - No error handling, wastes resources
await session.messages.postMessage(sessionId, message);

// GOOD - Handle disconnections gracefully
try {
  await session.messages.postMessage(sessionId, message);
} catch (e) {
  connectionManager.removeConnection(sessionId);
  // Queue for push notification instead
}
```

**4. Typing Indicator Spam**

Problem: Sending typing event on every keystroke.

```dart
// BAD - Client floods server with typing events
onKeyPress: () {
  sendTypingIndicator();
}

// GOOD - Debounce typing events
onKeyPress: () {
  if (!_typingThrottled) {
    sendTypingIndicator();
    _typingThrottled = true;
    Future.delayed(Duration(seconds: 2), () {
      _typingThrottled = false;
    });
  }
}
```

**5. Memory Leak in Connection Manager**

Problem: Not cleaning up stale connections.

```dart
// BAD - Connections accumulate forever
void registerConnection(String id, Connection conn) {
  _connections[id] = conn;
}

// GOOD - Clean up on close and periodically
void registerConnection(String id, Connection conn) {
  _connections[id] = conn;
}

void removeConnection(String id) {
  _connections.remove(id);
}

void cleanupStale() {
  final stale = _connections.entries
      .where((e) => e.value.isStale)
      .map((e) => e.key)
      .toList();
  for (final id in stale) {
    _connections.remove(id);
  }
}
```

