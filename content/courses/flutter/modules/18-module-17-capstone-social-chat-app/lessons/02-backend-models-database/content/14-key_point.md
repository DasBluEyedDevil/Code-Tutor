---
type: "KEY_POINT"
title: "Model Design Principles"
---


**Summary: Best Practices for Serverpod Models**

**1. Use Relations Properly**
```yaml
# Parent relation (foreign key)
userId: int, relation(parent=user_profiles)

# Optional relation
replyToId: int?, relation(parent=messages, optional)

# Named relation (for multiple refs to same table)
targetUserId: int, relation(parent=user_profiles, field=targetUser)
```

**2. Index Common Queries**
```yaml
indexes:
  # Unique constraint
  user_email_idx:
    fields: email
    unique: true
  
  # Composite index for common queries
  message_convo_time_idx:
    fields: conversationId, createdAt
```

**3. Denormalize for Performance**
- Store counts (likeCount, commentCount) to avoid COUNT queries
- Cache last message preview in conversation
- Store unread count per participant

**4. Use Enums for Fixed Values**
```yaml
enum: MessageType
values:
  - text
  - image
  - file
```

**5. Soft Delete Pattern**
```yaml
isDeleted: bool
deletedAt: DateTime?
```

**6. Audit Timestamps**
```yaml
createdAt: DateTime
updatedAt: DateTime?
```

**7. Generate and Test**
```bash
# After any protocol change
serverpod generate

# Start server with migrations
dart run bin/main.dart --apply-migrations
```

