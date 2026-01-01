---
type: "WARNING"
title: "Migration Safety Checklist"
---

### Before Running Migrations:

**1. Backup First**
```bash
pg_dump -Fc finance_tracker > pre_migration_backup.dump
```

**2. Test on Copy**
```bash
createdb finance_tracker_test
pg_restore -d finance_tracker_test pre_migration_backup.dump
alembic -c test_config.ini upgrade head
```

**3. Estimate Lock Time**
```sql
-- Check table size before ALTER TABLE
SELECT pg_size_pretty(pg_total_relation_size('transactions'));
```

**4. Schedule Wisely**
- Run during low-traffic periods
- Have rollback plan ready
- Monitor during migration

**5. Never in Production:**
- `DROP TABLE` without backup
- `ALTER COLUMN TYPE` on large tables without testing
- `TRUNCATE` on important data
- Add NOT NULL to large tables without backfill

**6. Always Have Rollback:**
```python
def downgrade():
    # Every upgrade needs a downgrade!
    op.drop_column('transactions', 'new_column')
```