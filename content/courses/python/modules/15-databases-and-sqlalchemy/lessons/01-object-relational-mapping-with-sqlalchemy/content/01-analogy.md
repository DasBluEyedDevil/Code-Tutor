---
type: "ANALOGY"
title: "The Concept: Why ORMs?"
---

**ORM = Object-Relational Mapper**

**The Problem with Raw SQL:**
```python
# Raw SQL is error-prone and tedious
cursor.execute(
    'INSERT INTO users (name, email) VALUES (?, ?)',
    (name, email)
)
```

**With an ORM:**
```python
# Work with Python objects instead!
user = User(name='Alice', email='alice@test.com')
db.session.add(user)
db.session.commit()
```

**Think of ORM like a translator:**
- You speak Python
- Database speaks SQL
- ORM translates between them

**Benefits:**
- Write Python, not SQL strings
- Type safety and autocompletion
- Database-agnostic (switch from SQLite to PostgreSQL easily)
- Prevents SQL injection automatically
- Relationships handled elegantly

**Popular Python ORMs:**
- **SQLAlchemy** - Most powerful, industry standard
- **SQLModel** - SQLAlchemy + Pydantic (great for FastAPI)
- **Django ORM** - Built into Django
- **Tortoise ORM** - Async-first

**We'll use SQLAlchemy** - it's the most widely used and powers many production applications.