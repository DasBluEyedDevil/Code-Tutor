---
type: "THEORY"
title: "Post CRUD Endpoints"
---


**Implementing Post Operations**

Every post operation needs proper validation, authorization, and error handling.

**Create Post Flow**

```
1. Authenticate user
         ↓
2. Validate content
   - Length limits
   - Content moderation
   - Media validation
         ↓
3. Process media
   - Upload to storage
   - Generate thumbnails
   - Extract metadata
         ↓
4. Create post record
         ↓
5. Create media records
         ↓
6. Notify followers (async)
         ↓
7. Return created post
```

**Authorization Rules**

| Operation | Rule |
|-----------|------|
| Create | Authenticated users |
| Read | Based on visibility + relationship |
| Update | Author only |
| Delete | Author or moderator |
| Like | Authenticated users |
| Comment | Based on post settings |

**Content Validation**

| Field | Validation |
|-------|------------|
| Content | 1-5000 characters, no banned words |
| Media | Max 10 items, valid types |
| Visibility | Valid enum value |
| Location | Valid coordinates (optional) |

**Soft Delete Strategy**

Why soft delete instead of hard delete?

1. **Audit trail** - Track what was deleted and when
2. **Recovery** - Allow undo within grace period
3. **References** - Comments/likes still reference post
4. **Moderation** - Review deleted content if needed

