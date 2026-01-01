---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Forgetting __init__.py in Package**
```python
# WRONG - Directory without __init__.py (before Python 3.3)
# mypackage/
#   utils.py  # Not a package without __init__.py!

# CORRECT - Add __init__.py (can be empty)
# mypackage/
#   __init__.py
#   utils.py
```

**2. Incorrect Relative Import Syntax**
```python
# WRONG - Missing dot for current package
# mypackage/module_a.py
from module_b import func  # Looks for top-level module_b!

# CORRECT - Use dot for relative imports
# mypackage/module_a.py
from .module_b import func  # Same package
from ..other_package import func  # Parent's sibling
```

**3. Running Package Module Directly**
```python
# WRONG - Running module directly breaks relative imports
# python mypackage/module.py  # Relative imports fail!

# CORRECT - Run as module from parent directory
# python -m mypackage.module
```

**4. Circular Dependencies in Packages**
```python
# WRONG - Package imports create circular dependency
# mypackage/__init__.py
from .module_a import func_a  # module_a imports module_b
from .module_b import func_b  # module_b imports module_a - circular!

# CORRECT - Import lazily or restructure
# mypackage/__init__.py
def get_func_a():
    from .module_a import func_a
    return func_a
```

**5. Exposing Too Much in __init__.py**
```python
# WRONG - Importing everything, slow package load
# mypackage/__init__.py
from .heavy_module import *  # Loads everything on import!
from .another_heavy import *

# CORRECT - Keep __init__.py light, import explicitly
# mypackage/__init__.py
__all__ = ['main_function']
from .core import main_function  # Only essential exports
```