---
type: "ANALOGY"
title: "Syntax Breakdown"
---

**Protocol (structural typing):**
```python
from typing import Protocol, runtime_checkable

@runtime_checkable  # Optional: enables isinstance()
class MyProtocol(Protocol):
    """Interface definition."""
    
    def required_method(self, arg: int) -> str:
        ...  # Ellipsis = abstract
    
    @property
    def required_property(self) -> float:
        ...

# Any class with these methods/properties works!
class MyClass:  # No inheritance needed
    def required_method(self, arg: int) -> str:
        return str(arg)
    
    @property
    def required_property(self) -> float:
        return 1.0
```

**ABC (nominal typing):**
```python
from abc import ABC, abstractmethod

class MyABC(ABC):
    """Abstract base class."""
    
    @abstractmethod
    def must_implement(self) -> str:
        pass
    
    def shared_method(self) -> str:
        """Concrete method - inherited by all subclasses."""
        return "Shared implementation"

# Must explicitly inherit
class MyClass(MyABC):  # Inheritance required!
    def must_implement(self) -> str:
        return "Implemented"
```

**Key differences:**
```python
# Protocol - "If it walks like a duck..."
def process(repo: Repository):  # Accepts any matching class
    repo.save(item)

# ABC - Explicit inheritance
class SQLRepo(Repository):  # Must inherit
    pass
```

**Combining both:**
```python
class Storage(Protocol):
    def save(self): ...

class BaseStorage(ABC):
    @abstractmethod
    def save(self): pass
    
    def log(self):  # Shared implementation
        print("Logging...")
```