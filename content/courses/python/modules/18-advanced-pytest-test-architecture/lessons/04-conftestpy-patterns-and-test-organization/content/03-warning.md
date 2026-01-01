---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Putting conftest.py in Wrong Location**
```python
# WRONG - conftest.py in project root, not tests/
project/
  conftest.py  # Fixtures not found by tests!
  src/
  tests/
    test_app.py

# CORRECT - conftest.py alongside tests
project/
  src/
  tests/
    conftest.py  # Available to all tests in tests/
    test_app.py
```

**2. autouse=True with Side Effects**
```python
# WRONG - autouse fixture modifies global state
@pytest.fixture(autouse=True)
def setup_db():
    db.create_tables()  # Runs for EVERY test!
    # Even tests that don't need a database

# CORRECT - Only autouse for lightweight, universal setup
@pytest.fixture(autouse=True)
def reset_env(monkeypatch):
    monkeypatch.setenv("TESTING", "true")  # Cheap, always needed
```

**3. Fixture Name Shadowing**
```python
# WRONG - Child conftest shadows parent fixture
# tests/conftest.py
@pytest.fixture
def db():
    return production_db()  # Shared database

# tests/unit/conftest.py
@pytest.fixture
def db():  # Shadows parent! Tests get this one only
    return mock_db()

# CORRECT - Use different names or explicit scope
@pytest.fixture
def mock_db():  # Different name, no shadowing
    return MockDB()
```

**4. Heavy Setup in Session-Scoped Fixtures Without Cleanup**
```python
# WRONG - Resources leak across test runs
@pytest.fixture(scope="session")
def test_database():
    db = create_database("test_db")
    populate_test_data(db)
    return db  # Never cleaned up!

# CORRECT - Use yield for cleanup
@pytest.fixture(scope="session")
def test_database():
    db = create_database("test_db")
    populate_test_data(db)
    yield db
    db.drop()  # Cleanup after session
```

**5. Importing Test Modules in conftest.py**
```python
# WRONG - Circular imports
# conftest.py
from tests.test_app import helper_function  # Circular!

@pytest.fixture
def setup():
    return helper_function()

# CORRECT - Keep conftest.py self-contained
# conftest.py
def helper_function():  # Define locally
    return "setup"

@pytest.fixture
def setup():
    return helper_function()
```