---
type: "THEORY"
title: "Mocking Patterns"
---

**1. Return values:**
```python
mock.return_value = {"data": "fake"}
```

**2. Side effects (exceptions, sequences):**
```python
mock.side_effect = ValueError("oops")
mock.side_effect = [1, 2, 3]  # Returns 1, then 2, then 3
```

**3. Checking calls:**
```python
mock.assert_called_once()
mock.assert_called_with("arg1", key="value")
assert mock.call_count == 3
```

**4. MagicMock for complex objects:**
```python
mock_db = MagicMock()
mock_db.query.return_value.filter.return_value.all.return_value = [user1, user2]
```

**5. Patching where it's used, not where it's defined:**
```python
# If weather.py imports httpx.get, patch "weather.httpx.get"
patch("weather.httpx.get")  # Correct
patch("httpx.get")          # Wrong - patches the module, not the import
```