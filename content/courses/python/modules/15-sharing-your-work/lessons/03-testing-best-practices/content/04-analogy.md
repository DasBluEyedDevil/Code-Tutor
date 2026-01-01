---
type: "ANALOGY"
title: "Test-Driven Development (TDD)"
---

**TDD = Write tests before code**

**The cycle:**

```
1. 🔴 Red: Write failing test
   ↓
2. 🟢 Green: Write minimal code to pass
   ↓
3. 🔵 Refactor: Improve code quality
   ↓
   Repeat
```

**Example workflow:**

**Step 1: Red (failing test)**
```python
def test_validate_email():
    assert validate_email("user@example.com") == True
    assert validate_email("invalid") == False
# Test fails - function doesn't exist
```

**Step 2: Green (make it pass)**
```python
def validate_email(email):
    return '@' in email
# Test passes (barely)
```

**Step 3: Refactor (improve)**
```python
import re

def validate_email(email):
    pattern = r'^[\w\.-]+@[\w\.-]+\.\w+$'
    return bool(re.match(pattern, email))
# Better implementation, tests still pass
```

**TDD benefits:**
- Forces you to think about requirements
- Every line of code is tested
- No over-engineering
- Confidence to refactor
- Design emerges naturally