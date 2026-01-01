---
type: "ANALOGY"
title: "The Concept: Contracts for Code"
---

**Interfaces = Contracts that classes must fulfill**

**Think of it like:**
- **USB Port** = Interface definition
  - Any device that fits the port works
  - Phone, keyboard, mouse - all implement USB interface
  - You don't care about internals, just that it fits

**Two approaches in Python:**

**1. ABC (Abstract Base Class)**
- Explicit inheritance required
- Checked at class definition time
- Traditional OOP approach
```python
from abc import ABC, abstractmethod

class Repository(ABC):
    @abstractmethod
    def save(self, item): ...
```

**2. Protocol (Structural Subtyping)**
- No inheritance required!
- Checked at type-checking time (mypy)
- "Duck typing" made formal
```python
from typing import Protocol

class Repository(Protocol):
    def save(self, item): ...
```

**When to use each:**

| ABC | Protocol |
|-----|----------|
| Shared implementation | Pure interface |
| Runtime enforcement | Static type checking |
| Internal code | Third-party integration |
| Template Method pattern | Dependency injection |

**Real-world Finance example:**
```python
class PaymentProcessor(Protocol):
    def process(self, amount: float) -> bool: ...
    def refund(self, transaction_id: str) -> bool: ...

# Any class with these methods works!
# Stripe, PayPal, Square - no inheritance needed
```