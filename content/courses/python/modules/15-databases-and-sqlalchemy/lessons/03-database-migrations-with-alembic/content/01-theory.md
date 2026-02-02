---
type: "THEORY"
title: "What are Database Migrations?"
---

**Database Migrations = Version Control for Your Database Schema**

As your application evolves, your database schema changes too. Migrations let you:

**Why Migrations Matter:**

1. **Track Changes** - Every schema change is versioned and documented
2. **Team Collaboration** - Multiple developers can evolve the schema safely
3. **Reproducibility** - Recreate any database state from scratch
4. **Rollback** - Undo changes if something goes wrong
5. **Production Safety** - Apply changes reliably across environments

**Without Migrations (Bad):**
```python
# Editing production database directly
Base.metadata.create_all(engine)  # Only creates, never updates!
```

**With Migrations (Good):**
```bash
# Tracked, reversible, safe
alembic revision --autogenerate -m "add user email column"
alembic upgrade head
```

**Alembic is SQLAlchemy's Migration Tool:**
- Created by the same author (Mike Bayer)
- Generates migrations from model changes automatically
- Supports async SQLAlchemy
- Up/down migrations for safe rollbacks

**Up vs Down Migrations:**

```python
def upgrade():    # Apply changes
    op.add_column('users', sa.Column('email', sa.String()))

def downgrade():  # Reverse changes
    op.drop_column('users', 'email')
```