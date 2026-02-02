---
type: "EXAMPLE"
title: "Key Differences from SQLite"
---

**PostgreSQL-Specific Features in SQLAlchemy:**

PostgreSQL offers powerful data types not available in SQLite.

**1. JSONB Columns:**
Store JSON with full indexing and query support.

**2. Array Columns:**
Store lists directly without separate tables.

**3. Full-Text Search:**
Built-in search without external services.

**4. UUID Primary Keys:**
Native UUID support for distributed systems.

**Note:** These features are PostgreSQL-specific. If you use them, your code won't work with SQLite. For development/production parity, consider using Docker to run PostgreSQL locally.

```python
# PostgreSQL-specific features in SQLAlchemy
# These features are NOT available in SQLite

from sqlalchemy import Column, Integer, String
from sqlalchemy.orm import declarative_base, Mapped, mapped_column
from sqlalchemy.dialects.postgresql import JSONB, ARRAY, UUID
import uuid

Base = declarative_base()

class Transaction(Base):
    """Example model using PostgreSQL-specific types"""
    __tablename__ = 'transactions'
    
    # UUID primary key (great for distributed systems)
    id: Mapped[uuid.UUID] = mapped_column(
        UUID(as_uuid=True), 
        primary_key=True, 
        default=uuid.uuid4
    )
    
    # JSONB column - store structured data with indexing
    metadata_: Mapped[dict] = mapped_column(
        JSONB, 
        default={},
        nullable=False
    )
    
    # Array column - store lists directly
    tags: Mapped[list[str]] = mapped_column(
        ARRAY(String),
        default=[]
    )
    
    amount: Mapped[int] = mapped_column(Integer)

print("=== PostgreSQL-Specific Features ===")
print("")
print("1. JSONB Columns:")
print("   - Store JSON with full query support")
print("   - Example: metadata_->>'key' = 'value'")
print("   - Can be indexed for performance")
print("")
print("2. Array Columns:")
print("   - Store lists without separate tables")
print("   - Example: tags = ['python', 'api']")
print("   - Query: ANY(tags) = 'python'")
print("")
print("3. UUID Primary Keys:")
print("   - Globally unique identifiers")
print("   - Great for distributed systems")
print("   - No central ID generator needed")
print("")
print("4. Full-Text Search:")
print("   - Built into PostgreSQL")
print("   - No external service needed")
print("   - Supports ranking and highlighting")
```
