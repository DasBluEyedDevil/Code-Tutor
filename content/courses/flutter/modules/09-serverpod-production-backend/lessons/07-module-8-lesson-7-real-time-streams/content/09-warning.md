---
type: "WARNING"
title: "Common Streaming Mistakes"
---

**Mistake 1: Not Cleaning Up on Disconnect**

If you add sessions to collections but never remove them, you leak memory and send messages to dead connections.

```dart
// BAD: Never removes sessions
@override
Future<void> streamOpened(StreamingSession session) async {
  _allSessions.add(session); // Added here...
}
// streamClosed never implemented - memory leak!

// GOOD: Always clean up
@override
Future<void> streamClosed(StreamingSession session) async {
  _allSessions.remove(session); // ...removed here
}
```

**Mistake 2: Sending Non-Serializable Objects**

Only SerializableModel instances (from your protocol) can be sent through streams. Sending raw Maps or custom classes fails.

```dart
// BAD: Will fail at runtime
session.sendStreamMessage({'type': 'hello'}); // Not a SerializableModel

// GOOD: Use protocol models
session.sendStreamMessage(ChatMessage(content: 'hello', ...));
```

**Mistake 3: Blocking in Message Handlers**

Long-running operations in handleStreamMessage block the entire connection. Use async properly or offload work.

```dart
// BAD: Blocks the stream
@override
Future<void> handleStreamMessage(...) async {
  await expensiveDatabaseOperation(); // Blocks for 5 seconds
  await anotherSlowOperation(); // User is waiting
}

// GOOD: Respond quickly, process in background
@override
Future<void> handleStreamMessage(...) async {
  // Acknowledge immediately
  session.sendStreamMessage(MessageReceived(id: message.id));
  
  // Process in background (fire and forget)
  unawaited(_processMessageAsync(message));
}
```

**Mistake 4: No Connection State Tracking**

Sending messages before connected or after disconnected causes errors.

```dart
// BAD: No state check
void sendMessage(String text) {
  _client.chat.sendStreamMessage(message); // Might crash
}

// GOOD: Check state first
void sendMessage(String text) {
  if (!_isConnected) {
    throw StateError('Cannot send: not connected');
  }
  _client.chat.sendStreamMessage(message);
}
```

