---
type: "WARNING"
title: "Database Operations Are Permanent"
---

Unlike variables in your program, database changes are PERMANENT:

- DELETE removes data forever (unless you have backups)
- UPDATE cannot be undone without restoring from backup
- No Ctrl+Z in production databases!

ALWAYS:
- Test queries on development data first
- Back up data before running DELETE or UPDATE
- Use WHERE clauses carefully - forgetting WHERE deletes ALL rows
- In production, use transactions so you can rollback mistakes