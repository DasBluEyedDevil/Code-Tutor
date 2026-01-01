---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Missing if __name__ == '__main__' Guard**
```python
# WRONG - Code runs on import!
# my_module.py
print("Module loading...")  # Runs when imported!
do_setup()  # Also runs!

# CORRECT - Guard execution code
# my_module.py
def do_setup():
    print("Setting up...")

if __name__ == '__main__':
    do_setup()  # Only runs when executed directly
```

**2. Importing Module From Same Directory Fails**
```python
# WRONG - Current directory not always in path
# main.py
import my_module  # ModuleNotFoundError sometimes!

# CORRECT - Ensure directory is in path
import sys
from pathlib import Path
sys.path.insert(0, str(Path(__file__).parent))
import my_module
```

**3. Modifying Module-Level Variables**
```python
# WRONG - Changes not shared between imports
# config.py
DEBUG = False

# main.py
import config
config.DEBUG = True  # Only changes in this import!

# CORRECT - Use functions or classes for mutable state
# config.py
_config = {'DEBUG': False}
def set_debug(value):
    _config['DEBUG'] = value
def is_debug():
    return _config['DEBUG']
```

**4. Using __all__ Incorrectly**
```python
# WRONG - __all__ doesn't restrict direct imports
# my_module.py
__all__ = ['public_func']
def public_func(): pass
def _private_func(): pass

# user.py
from my_module import _private_func  # Still works!

# CORRECT - __all__ only affects "from module import *"
from my_module import *  # Only gets public_func
```

**5. Forgetting .py Extension in Import**
```python
# WRONG - Including file extension
import my_module.py  # SyntaxError!

# CORRECT - Just the module name
import my_module
```