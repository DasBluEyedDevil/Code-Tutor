---
type: "KEY_POINT"
title: "Running Tests"
---

**Run tests with pytest:**

```bash
# Run all tests
uv run pytest

# Run with verbose output
uv run pytest -v

# Run specific test file
uv run pytest tests/test_models.py

# Run specific test class or method
uv run pytest tests/test_api.py::TestTransactionsAPI::test_create_transaction

# Run with coverage
uv run pytest --cov=finance_tracker --cov-report=html

# Show slowest tests
uv run pytest --durations=10
```

**Test database setup:**
```bash
# Create test database
createdb finance_tracker_test

# Run migrations on test database
DATABASE_URL=postgresql://localhost/finance_tracker_test alembic upgrade head
```