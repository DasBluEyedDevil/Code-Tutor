---
type: "ANALOGY"
title: "Syntax Breakdown"
---

**Package structure:**
```
my_package/
  __init__.py      ← Required! Makes it a package
  module1.py
  module2.py
```

**Basic __init__.py:**
```python
# my_package/__init__.py
"""Package docstring."""

# Import for convenience
from .module1 import function1
from .module2 import function2

__version__ = '1.0.0'
```

**Importing from packages:**
```python
# Import package
import my_package
my_package.function1()

# Import module from package
from my_package import module1
module1.function1()

# Import function directly
from my_package.module1 import function1
function1()
```

**Relative imports (inside package):**
```python
# In my_package/module1.py
from . import module2  # Same package
from .module2 import function  # Specific import
from ..other_package import something  # Parent package
```