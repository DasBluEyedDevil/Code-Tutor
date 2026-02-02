---
type: "THEORY"
title: "Chat-Style Messaging Patterns"
---

Chat is the classic real-time use case. Here are the patterns that make chat applications work well.

**Message Ordering:**

Messages must appear in the correct order, even when network is unreliable.

- Use server timestamps, not client timestamps
- Include sequence numbers for ordering within a conversation
- Handle out-of-order delivery gracefully

**Optimistic UI:**

Show the message immediately in the UI before server confirmation:

```dart
void sendMessage(String text) {
  // 1. Create local message with pending status
  final localMessage = ChatMessage(
    id: tempId,
    content: text,
    status: MessageStatus.sending,
    timestamp: DateTime.now(),
  );
  
  // 2. Add to UI immediately
  _messages.add(localMessage);
  notifyListeners();
  
  // 3. Send to server
  try {
    final confirmed = await _client.chat.send(localMessage);
    // 4. Update with server response
    _updateMessage(tempId, confirmed);
  } catch (e) {
    // 5. Mark as failed
    _updateMessageStatus(tempId, MessageStatus.failed);
  }
}
```

**Typing Indicators:**

Show when other users are typing:

```yaml
# protocol/typing_indicator.yaml
class: TypingIndicator
fields:
  channelId: String
  userId: int
  userName: String
  isTyping: bool
```

Send typing start/stop events, with debouncing:

```dart
Timer? _typingTimer;

void onUserTyping() {
  if (_typingTimer == null) {
    // Send 'started typing'
    _sendTypingIndicator(true);
  }
  
  // Reset timer
  _typingTimer?.cancel();
  _typingTimer = Timer(Duration(seconds: 3), () {
    // Send 'stopped typing' after 3s of no activity
    _sendTypingIndicator(false);
    _typingTimer = null;
  });
}
```

**Read Receipts:**

Track which messages have been seen:

```yaml
# protocol/read_receipt.yaml
class: ReadReceipt
fields:
  channelId: String
  userId: int
  lastReadMessageId: int
  readAt: DateTime
```

**Presence (Online Status):**

Show who is online in the chat:

```dart
// On streamOpened: broadcast 'user came online'
// On streamClosed: broadcast 'user went offline'
// Periodically: send heartbeat to confirm still connected
```

