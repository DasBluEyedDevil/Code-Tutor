---
type: "EXAMPLE"
title: "Setting Up Alembic"
---

**Installing and initializing Alembic for your FastAPI project:**

Alembic provides templates for different setups. For async SQLAlchemy, use the async template.

**Folder Structure After Init:**
```
project/
├── alembic.ini           # Alembic config (database URL, etc.)
├── migrations/
│   ├── env.py            # Migration environment setup
│   ├── script.py.mako    # Template for new migrations
│   └── versions/         # Migration files go here
│       └── abc123_initial.py
```

**Key Files:**
- `alembic.ini` - Main configuration (edit database URL here)
- `env.py` - Python code that runs migrations (customize for async)
- `versions/` - Each migration is a Python file with up/down functions

```python
# Installation
# Using uv (recommended)
# uv add alembic

# Or using pip
# pip install alembic

# Initialize Alembic with async template
# alembic init -t async migrations

import os

print("=== Alembic Project Structure ===")
print("""
After running: alembic init -t async migrations

project/
├── alembic.ini           # Main config file
├── migrations/
│   ├── env.py            # Migration environment
│   ├── script.py.mako    # Migration template
│   └── versions/         # Migration files
│       └── (empty initially)
│
├── app/
│   ├── models.py         # Your SQLAlchemy models
│   └── config.py         # Database URL, settings
│
└── main.py               # FastAPI app
""")

print("\n=== alembic.ini Key Settings ===")
print("""
[alembic]
script_location = migrations
prepend_sys_path = .

# Database URL (or set in env.py)
sqlalchemy.url = sqlite+aiosqlite:///./app.db

[logging]
# Logging configuration
""")

print("\n=== Common Commands ===")
print("""
# Create new migration from model changes
alembic revision --autogenerate -m "add users table"

# Apply all pending migrations
alembic upgrade head

# Rollback one step
alembic downgrade -1

# Show current revision
alembic current

# Show migration history
alembic history
""")
```
