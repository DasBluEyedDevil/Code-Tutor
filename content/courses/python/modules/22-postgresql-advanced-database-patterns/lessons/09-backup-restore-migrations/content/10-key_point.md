---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **pg_dump** for logical backups (portable, SQL format)
- **pg_restore** with `-j` flag for parallel restore
- Use **custom format** (-Fc) for compressed, efficient backups
- **Automate backups** with retention policies (30+ days)
- **Alembic** manages schema migrations with version control
- Every migration needs both **upgrade()** and **downgrade()**
- **Zero-downtime:** Add columns nullable, backfill in batches, then add constraints
- Use **CREATE INDEX CONCURRENTLY** to avoid locking
- **Expand-contract pattern** for column renames
- **Always test migrations** on a copy of production data