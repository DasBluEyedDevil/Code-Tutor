---
type: "THEORY"
title: "Project Structure"
---

**Clean Architecture for Finance Tracker:**

```
finance_tracker/
├── pyproject.toml           # Project config with uv
├── .env                     # Environment variables
├── .env.example             # Template for env vars
├── alembic/                 # Database migrations
│   ├── alembic.ini
│   └── versions/
├── src/
│   └── finance_tracker/
│       ├── __init__.py
│       ├── main.py          # FastAPI app entry point
│       ├── config.py        # Settings with Pydantic
│       ├── database.py      # asyncpg connection pool
│       ├── models/          # Domain models
│       │   ├── __init__.py
│       │   ├── user.py
│       │   ├── transaction.py
│       │   ├── category.py
│       │   └── budget.py
│       ├── schemas/         # Pydantic schemas (API)
│       │   ├── __init__.py
│       │   ├── user.py
│       │   ├── transaction.py
│       │   └── budget.py
│       ├── repositories/    # Data access layer
│       │   ├── __init__.py
│       │   ├── base.py
│       │   ├── user.py
│       │   └── transaction.py
│       ├── services/        # Business logic
│       │   ├── __init__.py
│       │   ├── auth.py
│       │   ├── transaction.py
│       │   └── budget.py
│       ├── api/             # FastAPI routers
│       │   ├── __init__.py
│       │   ├── auth.py
│       │   ├── transactions.py
│       │   ├── categories.py
│       │   └── budgets.py
│       └── utils/           # Helpers
│           ├── __init__.py
│           ├── security.py
│           └── date_utils.py
└── tests/
    ├── conftest.py          # Pytest fixtures
    ├── test_models.py
    ├── test_repositories.py
    ├── test_services.py
    └── test_api.py
```

**Why This Structure?**
- **Separation of Concerns**: Each layer has one job
- **Testability**: Mock any layer independently
- **Scalability**: Easy to add new features
- **Type Safety**: Full typing throughout