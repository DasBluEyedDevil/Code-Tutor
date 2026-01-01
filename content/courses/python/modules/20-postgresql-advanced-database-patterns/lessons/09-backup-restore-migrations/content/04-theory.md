---
type: "THEORY"
title: "Database Migrations with Alembic"
---

Schema changes need to be tracked, versioned, and reversible. Alembic is the standard for SQLAlchemy/asyncpg projects:

**Why Migrations?**
- Track schema changes in version control
- Deploy changes consistently across environments
- Roll back if something goes wrong
- Team members stay in sync

**Alembic Setup:**
```bash
pip install alembic asyncpg
alembic init migrations
```

**Migration Lifecycle:**
1. **Generate:** `alembic revision --autogenerate -m "add budgets table"`
2. **Review:** Check generated migration code
3. **Apply:** `alembic upgrade head`
4. **Rollback:** `alembic downgrade -1`

**Best Practices:**
- Always test migrations on a copy of production data
- Keep migrations small and focused
- Write both upgrade() and downgrade()
- Use transactions for atomic migrations
- Document breaking changes