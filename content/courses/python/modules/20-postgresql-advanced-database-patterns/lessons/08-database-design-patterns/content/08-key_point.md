---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Start normalized** (3NF), denormalize based on performance data
- **Soft deletes** (deleted_at) preserve data and enable recovery
- **Audit fields** (created_at, updated_at, created_by) track changes
- **Audit log table** stores complete change history with old/new values
- **Triggers** automate audit trail and updated_at maintenance
- **Row-Level Security** (RLS) enforces data isolation at database level
- **Partial indexes** on `WHERE deleted_at IS NULL` optimize active record queries
- **JSONB metadata** provides flexibility for variable fields
- Design for **your query patterns** - index what you filter/sort by