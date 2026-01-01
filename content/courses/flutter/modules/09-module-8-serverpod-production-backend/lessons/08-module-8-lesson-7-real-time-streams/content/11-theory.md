---
type: "THEORY"
title: "Real-Time Notifications Pattern"
---

Notifications are a perfect use case for streaming. Users expect instant delivery without refreshing. Here is how to design a notification system.

**Notification Types:**

1. **User-specific**: New follower, new message, order shipped
2. **Group-specific**: New post in group, team announcement
3. **Global**: System maintenance, new feature announcement

**Server-Side Design:**

```yaml
# protocol/notification.yaml
class: Notification
fields:
  id: int?
  userId: int        # Target user
  type: String       # 'follow', 'message', 'order', etc.
  title: String
  body: String
  data: Map<String, String>?  # Extra data (orderId, messageId, etc.)
  isRead: bool
  createdAt: DateTime
```

**Notification Flow:**

```
1. Event occurs (new follower)
        |
        v
2. Create Notification in database
        |
        v
3. Check if recipient is connected (streaming)
        |
   Yes /   \ No
      v     v
4a. Push via    4b. Store for later
    stream          (shown on next app open)
        |
        v
5. Client displays notification
        |
        v
6. User taps -> Navigate to relevant screen
```

**Hybrid Approach:**

Real-time streaming is great when users are active, but you also need:
- Push notifications (FCM/APNs) for when app is closed
- Persisted notifications for history
- Read/unread status sync across devices

