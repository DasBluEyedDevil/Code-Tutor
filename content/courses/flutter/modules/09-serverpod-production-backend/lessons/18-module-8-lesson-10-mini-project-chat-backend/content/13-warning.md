---
type: "WARNING"
title: "Production Considerations"
---

Before deploying to production, consider these enhancements:

**1. Multi-Server Support:**
The current implementation uses in-memory state for connected sessions. For multiple server instances, use Redis pub/sub:

```dart
// Store sessions in Redis
await redis.sAdd('room:$roomId:sessions', sessionId);

// Broadcast via Redis pub/sub
await redis.publish('chat:$roomId', messageJson);
```

**2. Rate Limiting:**
Protect against spam and abuse:

```dart
// In handleStreamMessage
final rateKey = 'rate:${user.userId}';
final count = await redis.incr(rateKey);
await redis.expire(rateKey, 60); // Per minute

if (count > 30) { // Max 30 messages per minute
  throw RateLimitExceededException();
}
```

**3. Message Validation:**
Sanitize content to prevent XSS and injection:

```dart
String sanitizeContent(String content) {
  // Remove HTML tags
  content = content.replaceAll(RegExp(r'<[^>]*>'), '');
  // Escape special characters
  content = HtmlEscape().convert(content);
  return content;
}
```

**4. Database Optimization:**
Add indexes for common queries:

```sql
-- Fast room lookup for user
CREATE INDEX idx_chat_member_user_room 
ON chat_members(user_id, chat_room_id);

-- Fast message history
CREATE INDEX idx_chat_message_room_time 
ON chat_messages(chat_room_id, created_at DESC);
```

**5. Monitoring:**
Add logging and metrics:

```dart
session.log('User ${user.username} sent message to room $roomId');
metrics.increment('messages.sent', tags: {'room_type': room.isGroup ? 'group' : 'dm'});
```

**6. Push Notifications:**
Notify offline users via FCM/APNs when they receive messages.

