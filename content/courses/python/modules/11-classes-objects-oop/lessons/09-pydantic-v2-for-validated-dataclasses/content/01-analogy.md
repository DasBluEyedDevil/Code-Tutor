---
type: "ANALOGY"
title: "The Concept: Runtime Type Validation"
---

**Pydantic = Dataclasses with superpowers**

**The Problem:**
```python
from dataclasses import dataclass

@dataclass
class Transaction:
    amount: float  # Type hint, not enforced!

t = Transaction(amount="not a number")  # Works! No error
print(t.amount + 10)  # Runtime crash!
```

**The Solution - Pydantic:**
```python
from pydantic import BaseModel

class Transaction(BaseModel):
    amount: float  # Actually validated!

t = Transaction(amount="123")  # Converts to 123.0
t = Transaction(amount="abc")  # ValidationError!
```

**Why Pydantic v2?**

1. **Runtime Validation** ✅
   - Type hints actually enforced
   - Clear error messages
   - Auto-coercion ("123" -> 123)

2. **Blazing Fast** ⚡
   - Rust-powered core (pydantic-core)
   - 5-50x faster than v1
   - Comparable to dataclasses

3. **Rich Validation** 🎯
   - Email, URL, regex patterns
   - Custom validators
   - Field constraints (gt, lt, min_length)

4. **Serialization** 📦
   - JSON, dict conversion built-in
   - API-ready

**Perfect for:**
- API request/response models
- Configuration management
- Data pipeline validation
- Domain entities