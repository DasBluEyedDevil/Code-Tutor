---
type: "THEORY"
title: "Backup Strategies"
---

Data is the most valuable asset. A solid backup strategy is non-negotiable:

**Logical Backups (pg_dump)**
- SQL statements to recreate database
- Human-readable, portable
- Can restore to different PostgreSQL versions
- Slower for large databases

**Physical Backups (pg_basebackup)**
- Binary copy of data files
- Faster backup and restore
- Must match exact PostgreSQL version
- Enables point-in-time recovery (PITR)

**Backup Types:**

| Type | Speed | Size | Granularity |
|------|-------|------|-------------|
| Full | Slow | Large | Complete |
| Incremental | Fast | Small | Changes only |
| Differential | Medium | Medium | Since last full |

**Finance Tracker Strategy:**
1. **Daily:** Full logical backup (pg_dump)
2. **Continuous:** WAL archiving for PITR
3. **Weekly:** Full physical backup
4. **Retain:** 30 days of backups
5. **Test:** Monthly restore verification