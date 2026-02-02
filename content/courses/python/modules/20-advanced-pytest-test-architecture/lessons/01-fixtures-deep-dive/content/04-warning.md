---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Using return Instead of yield for Teardown**
```python
# WRONG - Teardown code never runs
@pytest.fixture
def database():
    conn = connect()
    return conn  # Teardown below is unreachable!
    conn.close()  # Never executed!

# CORRECT - Use yield for setup/teardown
@pytest.fixture
def database():
    conn = connect()
    yield conn  # Test runs here
    conn.close()  # Runs after test
```

**2. Wrong Scope for Shared State**
```python
# WRONG - Session scope modifies shared state
@pytest.fixture(scope="session")
def users_list():
    return []  # Same list shared across ALL tests!

# CORRECT - Use function scope for mutable state
@pytest.fixture(scope="function")  # or just @pytest.fixture
def users_list():
    return []  # Fresh list for each test
```

**3. Forgetting to Request the Fixture**
```python
# WRONG - Fixture never runs, test uses wrong db
@pytest.fixture
def database():
    return create_test_db()

def test_query():  # Missing database parameter!
    result = query(real_db)  # Uses production DB!

# CORRECT - Request fixture as parameter
def test_query(database):  # Fixture is injected
    result = query(database)
```

**4. Circular Fixture Dependencies**
```python
# WRONG - Circular dependency causes error
@pytest.fixture
def fixture_a(fixture_b):
    return setup_a(fixture_b)

@pytest.fixture
def fixture_b(fixture_a):  # Error: circular!
    return setup_b(fixture_a)

# CORRECT - Break the cycle with intermediate fixture
@pytest.fixture
def shared_setup():
    return common_config()

@pytest.fixture
def fixture_a(shared_setup):
    return setup_a(shared_setup)
```

**5. Factory Fixture Returning Function Instead of Yielding**
```python
# WRONG - No cleanup possible
@pytest.fixture
def make_user():
    users = []
    def _make(name):
        u = User(name)
        users.append(u)
        return u
    return _make  # Can't clean up users!

# CORRECT - Yield enables cleanup
@pytest.fixture
def make_user():
    users = []
    def _make(name):
        u = User(name)
        users.append(u)
        return u
    yield _make
    for u in users:  # Cleanup runs
        u.delete()
```