---
type: "WARNING"
title: "Soft Delete vs Hard Delete"
---

**Hard Delete** (actually removes the row):

```dart
await User.db.deleteRow(session, user);
```

- Data is permanently gone
- Cannot be recovered without database backup
- Foreign key constraints may block deletion
- Fast and saves storage space

**Soft Delete** (marks as deleted but keeps the row):

```dart
await User.db.updateRow(session, user.copyWith(isDeleted: true));
```

- Data remains in database but is filtered from queries
- Easy to recover deleted data
- Maintains all relationships
- Requires adding `isDeleted` field to your model
- All queries must filter out deleted records

**Best Practice:**

For most production applications, use soft delete for user-facing data:

1. Add `isDeleted: bool` field to your models
2. Add `deletedAt: DateTime?` for audit trail
3. Filter deleted records in all read queries:
   ```dart
   where: (t) => t.isDeleted.equals(false)
   ```
4. Only hard delete when legally required (GDPR, data retention policies)

