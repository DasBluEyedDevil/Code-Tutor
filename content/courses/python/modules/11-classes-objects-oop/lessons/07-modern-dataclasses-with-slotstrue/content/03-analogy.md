---
type: "ANALOGY"
title: "Syntax Breakdown"
---

**Basic dataclass:**
```python
from dataclasses import dataclass, field

@dataclass
class MyClass:
    required_field: str
    optional_field: int = 0
    computed: list = field(default_factory=list)
```

**Dataclass options:**
```python
@dataclass(
    slots=True,      # Use __slots__ for memory efficiency
    frozen=True,     # Make instances immutable
    order=True,      # Generate <, <=, >, >= methods
    kw_only=True,    # All fields keyword-only (3.10+)
)
class MyClass:
    ...
```

**Field options:**
```python
from dataclasses import field

@dataclass
class Example:
    # Regular field with default
    name: str = "default"
    
    # Mutable default (use factory!)
    items: list = field(default_factory=list)
    
    # Exclude from repr
    internal: str = field(default="", repr=False)
    
    # Exclude from comparison
    cache: dict = field(default_factory=dict, compare=False)
    
    # Init=False means not in constructor
    computed: float = field(init=False)
    
    def __post_init__(self):
        self.computed = len(self.items) * 2
```

**Type annotations required but not enforced:**
```python
@dataclass
class Transaction:
    amount: float  # Type hint required
    # Runtime: amount can still be any type
    # Use Pydantic for runtime validation!
```