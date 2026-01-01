---
type: "ANALOGY"
title: "Syntax Breakdown"
---

**Class definition:**
```python
class ClassName:
    def __init__(self, parameters):
        self.attribute = value
    
    def method_name(self):
        # do something
        return result
```

**Creating objects:**
```python
obj = ClassName(arguments)
```

**Key terms:**
- **Class** - The blueprint/template
- **Object/Instance** - Specific creation from the class
- **`__init__()`** - Constructor method (initializer)
- **`self`** - Reference to the current instance
- **Attribute** - Variable belonging to object (self.name)
- **Method** - Function belonging to class

**Naming conventions:**
- Classes: `CapitalCase` (Dog, BankAccount)
- Methods: `snake_case` (bark, get_balance)
- Attributes: `snake_case` (name, account_number)