---
type: "THEORY"
title: "SQLAlchemy 2.0 - Modern ORM"
---

**SQLAlchemy 2.0** represents a major evolution with native async support, improved typing, and a cleaner API.

**What's New in 2.0:**

1. **Native Async Support** - First-class async/await without greenlet hacks
2. **Mapped Types** - Better type hints with `Mapped[T]` and `mapped_column()`
3. **DeclarativeBase** - New base class replaces `declarative_base()`
4. **Unified Query API** - `select()` everywhere, no more `query()`

**Old vs New:**

```python
# SQLAlchemy 1.x (OLD)
from sqlalchemy.ext.declarative import declarative_base
Base = declarative_base()

class User(Base):
    __tablename__ = 'users'
    id = Column(Integer, primary_key=True)
    name = Column(String(100))

# SQLAlchemy 2.0 (NEW)
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column

class Base(DeclarativeBase):
    pass

class User(Base):
    __tablename__ = 'users'
    id: Mapped[int] = mapped_column(primary_key=True)
    name: Mapped[str] = mapped_column(String(100))
```

**Async Drivers:**
- **SQLite**: `aiosqlite` (development)
- **PostgreSQL**: `asyncpg` (production)
- **MySQL**: `asyncmy` or `aiomysql`

**Connection URLs:**
```python
# Async SQLite
"sqlite+aiosqlite:///./app.db"

# Async PostgreSQL
"postgresql+asyncpg://user:pass@localhost/dbname"
```