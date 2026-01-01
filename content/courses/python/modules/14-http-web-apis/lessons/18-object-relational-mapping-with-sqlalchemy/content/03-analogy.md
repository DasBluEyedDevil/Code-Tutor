---
type: "ANALOGY"
title: "Syntax Breakdown"
---

**Model Definition:**
```python
from sqlalchemy import Column, Integer, String
from sqlalchemy.orm import declarative_base

Base = declarative_base()

class User(Base):
    __tablename__ = 'users'  # Table name
    
    id = Column(Integer, primary_key=True)  # Auto-increment
    name = Column(String(100), nullable=False)  # Required
    email = Column(String(100), unique=True)  # Unique constraint
```

**Column Types:**
- `Integer` - Whole numbers
- `String(length)` - Text with max length
- `Text` - Long text
- `Boolean` - True/False
- `DateTime` - Date and time
- `Float` - Decimal numbers

**Query Patterns:**
```python
# Get all
User.query.all()
session.query(User).all()

# Get by ID
User.query.get(1)

# Filter
User.query.filter_by(name='Alice').first()
User.query.filter(User.email.like('%@gmail.com')).all()

# Order
User.query.order_by(User.name.desc()).all()

# Limit
User.query.limit(10).all()

# Count
User.query.count()
```

**Relationships:**
```python
# One-to-Many
class User(Base):
    posts = relationship('Post', back_populates='author')

class Post(Base):
    author_id = Column(Integer, ForeignKey('users.id'))
    author = relationship('User', back_populates='posts')

# Usage
user.posts  # Get all posts by user
post.author  # Get author of post
```