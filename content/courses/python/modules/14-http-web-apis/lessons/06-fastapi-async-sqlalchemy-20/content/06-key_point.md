---
type: "KEY_POINT"
title: "Async SQLAlchemy 2.0 Takeaways"
---

**Core Concepts:**

1. **Async Engine Setup:**
```python
engine = create_async_engine(DATABASE_URL)
async_session = async_sessionmaker(engine)
```

2. **Mapped Types for Models:**
```python
class User(Base):
    id: Mapped[int] = mapped_column(primary_key=True)
    name: Mapped[str] = mapped_column(String(100))
```

3. **Select API:**
```python
result = await db.execute(select(User).where(User.id == 1))
user = result.scalar_one_or_none()
```

4. **Eager Loading:**
```python
select(User).options(selectinload(User.transactions))
```

**Migration from 1.x:**
- `declarative_base()` -> `DeclarativeBase`
- `Column(Type)` -> `Mapped[type] = mapped_column()`
- `session.query()` -> `select()`
- Sync functions -> async/await everywhere

**Database Drivers:**
- SQLite: `sqlite+aiosqlite:///./app.db`
- PostgreSQL: `postgresql+asyncpg://user:pass@host/db`

**Best Practices:**
- Use `expire_on_commit=False` in sessionmaker
- Always use `selectinload()` for collections to avoid N+1
- Use Pydantic's `from_attributes = True` for ORM models
- Wrap session in async context manager for cleanup