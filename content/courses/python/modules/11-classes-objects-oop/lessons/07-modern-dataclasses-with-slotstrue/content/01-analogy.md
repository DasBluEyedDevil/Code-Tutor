---
type: "ANALOGY"
title: "The Concept: Efficient Data Containers"
---

**Dataclasses = Less boilerplate, more productivity**

**Think of it like:**
- Regular class: Build your car from scratch - engine, chassis, wheels...
- Dataclass: Order a pre-configured car - just specify color and features

**What dataclasses provide automatically:**

1. **__init__()** - Constructor generated from field definitions
2. **__repr__()** - Nice string representation
3. **__eq__()** - Compare by values, not identity
4. **__hash__()** - If frozen=True

**Python 3.10+ slots=True:**
- Uses __slots__ instead of __dict__
- **35-50% less memory per instance**
- **10-20% faster attribute access**
- Prevents adding new attributes (catches typos!)

**Traditional class:**
```python
class Transaction:
    def __init__(self, id, amount, category):
        self.id = id
        self.amount = amount
        self.category = category
    
    def __repr__(self):
        return f"Transaction(id={self.id}, amount={self.amount})"
    
    def __eq__(self, other):
        # ... lots of code
```

**Modern dataclass:**
```python
@dataclass(slots=True, frozen=True)
class Transaction:
    id: int
    amount: float
    category: str
```

**Same functionality, 80% less code!**