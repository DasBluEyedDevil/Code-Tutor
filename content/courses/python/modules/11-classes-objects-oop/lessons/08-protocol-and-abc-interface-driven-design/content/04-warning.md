---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Forgetting @runtime_checkable for isinstance()**
```python
# WRONG - isinstance() doesn't work without decorator
class MyProtocol(Protocol):
    def method(self): ...

class Impl:
    def method(self): pass

print(isinstance(Impl(), MyProtocol))  # Error!

# CORRECT - Add @runtime_checkable
@runtime_checkable
class MyProtocol(Protocol):
    def method(self): ...

print(isinstance(Impl(), MyProtocol))  # True
```

**2. Protocol with Implementation (Use ABC Instead)**
```python
# WRONG - Protocols shouldn't have implementations
class Repository(Protocol):
    def save(self): ...
    
    def log(self):  # Implementation in Protocol!
        print("Logging")  # This won't be inherited!

# CORRECT - Use ABC for shared implementation
class Repository(ABC):
    @abstractmethod
    def save(self): pass
    
    def log(self):  # Inherited by all subclasses
        print("Logging")
```

**3. ABC Without abstractmethod Decorator**
```python
# WRONG - Method not marked abstract
class Base(ABC):
    def must_override(self):  # Missing @abstractmethod!
        pass

class Child(Base):  # Can instantiate without implementing!
    pass

c = Child()  # Works - but shouldn't!

# CORRECT - Use @abstractmethod
class Base(ABC):
    @abstractmethod
    def must_override(self):
        pass

class Child(Base):
    pass

c = Child()  # TypeError: Can't instantiate abstract class
```

**4. Protocol Method Signature Mismatch**
```python
@runtime_checkable
class Saveable(Protocol):
    def save(self, path: str) -> bool: ...

class Document:
    def save(self, path):  # Missing return type annotation
        pass  # Returns None, not bool

# Type checker will catch this, but runtime won't!
print(isinstance(Document(), Saveable))  # True at runtime!
```

**5. Not Using Protocols for Dependency Injection**
```python
# WRONG - Concrete dependency
class UserService:
    def __init__(self):
        self.db = PostgresDatabase()  # Hard-coded!

# CORRECT - Protocol-based injection
class Database(Protocol):
    def query(self, sql: str): ...

class UserService:
    def __init__(self, db: Database):  # Accepts any DB!
        self.db = db
```