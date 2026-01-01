---
type: "THEORY"
title: "Likes and Interactions"
---


**Implementing Like/Unlike Functionality**

Likes seem simple but have several challenges at scale:

**Like System Requirements**

| Requirement | Implementation |
|-------------|---------------|
| Toggle behavior | Like if not liked, unlike if liked |
| Unique constraint | One like per user per post |
| Count accuracy | Denormalized + async reconciliation |
| Reaction types | Support multiple reaction types |
| Real-time updates | WebSocket broadcast |

**Count Update Strategies**

| Strategy | Pros | Cons |
|----------|------|------|
| **Count on read** | Always accurate | Slow at scale |
| **Trigger update** | Automatic | DB overhead |
| **App-level update** | Fast, controlled | Race conditions |
| **Async queue** | Scalable | Eventually consistent |

**Handling Race Conditions**

```
User taps like rapidly:
Request 1: Read count = 10, increment to 11
Request 2: Read count = 10, increment to 11
Result: Count = 11 (should be 12!)

Solution: Use atomic updates:
UPDATE posts SET likes_count = likes_count + 1 WHERE id = ?
```

**Reaction Types**

```
Reaction Types:
├── like      (👍)
├── love      (❤️)
├── laugh     (😂)
├── wow       (😮)
├── sad       (😢)
└── angry     (😠)
```

**Notification Strategy**

| Scenario | Notification |
|----------|-------------|
| First like | Immediate notification |
| Subsequent likes | Batch: "10 people liked your post" |
| Unlike | No notification |
| Reaction change | No notification |

