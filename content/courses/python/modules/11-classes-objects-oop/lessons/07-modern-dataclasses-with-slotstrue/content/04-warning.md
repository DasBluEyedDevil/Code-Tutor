---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Mutable Default Arguments**
```python
# WRONG - Shared list between instances!
@dataclass
class Team:
    name: str
    members: list = []  # DANGER!

t1 = Team("A")
t1.members.append("Alice")
t2 = Team("B")
print(t2.members)  # ['Alice'] - Shared!

# CORRECT - Use field with default_factory
@dataclass
class Team:
    name: str
    members: list = field(default_factory=list)
```

**2. Inheritance with slots=True**
```python
# WRONG - Parent without slots, child with slots
@dataclass
class Parent:
    x: int

@dataclass(slots=True)
class Child(Parent):  # Still has __dict__ from parent!
    y: int

# CORRECT - Both must use slots
@dataclass(slots=True)
class Parent:
    x: int

@dataclass(slots=True)
class Child(Parent):
    y: int
```

**3. Frozen Dataclass with Mutable Fields**
```python
# MISLEADING - frozen doesn't deep-freeze!
@dataclass(frozen=True)
class Container:
    items: list

c = Container(items=[1, 2, 3])
c.items.append(4)  # Works! List itself is mutable
print(c.items)  # [1, 2, 3, 4]

# SOLUTION - Use immutable types
from typing import Tuple

@dataclass(frozen=True)
class Container:
    items: tuple  # Immutable
```

**4. Comparing Dataclasses with compare=False Fields**
```python
# UNEXPECTED - Fields excluded from comparison
@dataclass
class User:
    id: int
    name: str
    cache: dict = field(default_factory=dict, compare=False)

u1 = User(1, "Alice", {"key": "value1"})
u2 = User(1, "Alice", {"key": "value2"})
print(u1 == u2)  # True! Cache is ignored
```

**5. slots=True Prevents Dynamic Attributes**
```python
@dataclass(slots=True)
class Transaction:
    id: int
    amount: float

t = Transaction(1, 100.0)
t.description = "Test"  # AttributeError!
# This is actually a FEATURE - catches typos!
```