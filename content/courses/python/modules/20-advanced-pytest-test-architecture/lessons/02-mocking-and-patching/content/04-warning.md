---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Patching Where It's Defined, Not Where It's Used**
```python
# WRONG - Patches the original module
with patch("requests.get"):  # Patches requests module
    from myapp import fetch_data
    fetch_data()  # Still uses real requests!

# CORRECT - Patch where it's imported
with patch("myapp.requests.get"):  # Patches myapp's import
    from myapp import fetch_data
    fetch_data()  # Uses mock
```

**2. Forgetting return_value for Method Chains**
```python
# WRONG - Mock returns Mock, not expected value
mock_db = MagicMock()
mock_db.query.filter.all = [user1]  # Wrong!

# CORRECT - Chain return_value for each call
mock_db = MagicMock()
mock_db.query.return_value.filter.return_value.all.return_value = [user1]
```

**3. Using assert_called() Without Parentheses**
```python
# WRONG - This is a property access, not assertion!
mock.assert_called_once  # Always truthy, never fails!

# CORRECT - Call the assertion method
mock.assert_called_once()  # Actually checks
```

**4. Mock Scope Not Matching Test**
```python
# WRONG - Mock only active during setup
def test_something():
    with patch("module.func") as mock:
        mock.return_value = 42
    # Mock is gone here!
    result = call_func()  # Uses real function!

# CORRECT - Keep mock active during test
def test_something():
    with patch("module.func") as mock:
        mock.return_value = 42
        result = call_func()  # Uses mock
        assert result == 42
```

**5. Over-Mocking Your Own Code**
```python
# WRONG - Mocking the code you're testing
def test_calculate():
    with patch("myapp.calculate") as mock:  # Testing the mock!
        mock.return_value = 42
        assert myapp.calculate(1, 2) == 42  # Meaningless!

# CORRECT - Mock dependencies, not the code under test
def test_calculate():
    with patch("myapp.external_api") as mock_api:
        mock_api.return_value = {"factor": 2}
        assert myapp.calculate(1, 2) == 6  # Tests real logic
```