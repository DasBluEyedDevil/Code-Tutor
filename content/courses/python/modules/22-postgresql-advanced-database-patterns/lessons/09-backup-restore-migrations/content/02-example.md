---
type: "EXAMPLE"
title: "pg_dump and pg_restore"
---

**Command-line backup and restore operations:**

```bash
# pg_dump Examples - Run these in your terminal

# 1. Basic SQL dump (human-readable)
pg_dump -U finance_user -d finance_tracker > backup.sql

# 2. Custom format (compressed, supports parallel restore)
pg_dump -U finance_user -d finance_tracker -Fc -f backup.dump

# 3. Directory format (parallel backup, large databases)
pg_dump -U finance_user -d finance_tracker -Fd -j 4 -f backup_dir/

# 4. Dump specific tables
pg_dump -U finance_user -d finance_tracker -t transactions -t accounts > tables.sql

# 5. Schema only (no data)
pg_dump -U finance_user -d finance_tracker --schema-only > schema.sql

# 6. Data only (no schema)
pg_dump -U finance_user -d finance_tracker --data-only > data.sql

# ========== RESTORE ==========

# 1. Restore SQL dump
psql -U finance_user -d finance_tracker_new < backup.sql

# 2. Restore custom format (parallel)
pg_restore -U finance_user -d finance_tracker_new -j 4 backup.dump

# 3. Restore to new database
createdb -U finance_user finance_tracker_restored
pg_restore -U finance_user -d finance_tracker_restored backup.dump

# 4. Restore specific tables
pg_restore -U finance_user -d finance_tracker -t transactions backup.dump

# 5. List contents of backup
pg_restore -l backup.dump

# ========== AUTOMATION SCRIPT ==========
```
