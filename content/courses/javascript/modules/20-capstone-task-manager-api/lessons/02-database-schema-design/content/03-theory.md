---
type: "THEORY"
title: "Schema Design Decisions Explained"
---

**Why cuid() instead of autoincrement?**
- CUIDs are URL-safe, unique across systems
- No information leakage (can't guess total users from ID)
- Safe for distributed systems

**Why separate passwordHash field?**
- Never store plain passwords
- Name reminds developers this is a hash, not password

**Why onDelete: Cascade for tasks?**
- When user is deleted, their tasks are automatically deleted
- Maintains referential integrity

**Why onDelete: SetNull for category on tasks?**
- If category is deleted, tasks remain but lose their category
- Alternative: Cascade would delete tasks too (usually not desired)

**Why indexes on userId, categoryId, status, dueDate?**
- These are the fields we'll filter/sort by
- Indexes make queries much faster

**Why @@unique([userId, name]) on Category?**
- Prevents duplicate category names per user
- Different users can have categories with the same name