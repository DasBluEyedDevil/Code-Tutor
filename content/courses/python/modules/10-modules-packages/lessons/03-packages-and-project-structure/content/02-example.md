---
type: "EXAMPLE"
title: "Code Example: Creating a Package"
---

**Key points:**
1. __init__.py makes directory a package
2. Can import from __init__.py for convenience
3. __all__ controls what `from package import *` imports
4. Packages can be nested (sub-packages)
5. Use relative imports (from .module) inside packages

```python
from pathlib import Path

# Create package structure
print("=== Creating Package Structure ===")

base = Path('my_tools')
base.mkdir(exist_ok=True)

# Create __init__.py (makes it a package)
(base / '__init__.py').write_text('''
"""My Tools Package - Utility functions."""

from .string_ops import reverse, uppercase
from .math_ops import add, multiply

__version__ = '1.0.0'
__all__ = ['reverse', 'uppercase', 'add', 'multiply']
''')

# Create string_ops.py module
(base / 'string_ops.py').write_text('''
def reverse(text):
    return text[::-1]

def uppercase(text):
    return text.upper()

def lowercase(text):  # Not exported by default
    return text.lower()
''')

# Create math_ops.py module
(base / 'math_ops.py').write_text('''
def add(a, b):
    return a + b

def multiply(a, b):
    return a * b

def subtract(a, b):  # Not exported by default
    return a - b
''')

print("✓ Created my_tools package")
print("  - my_tools/__init__.py")
print("  - my_tools/string_ops.py")
print("  - my_tools/math_ops.py\n")

# Import and use the package
print("=== Using the Package ===")

import my_tools

print(f"Version: {my_tools.__version__}")
print(f"Reverse 'hello': {my_tools.reverse('hello')}")
print(f"Uppercase 'world': {my_tools.uppercase('world')}")
print(f"Add 5 + 3: {my_tools.add(5, 3)}")
print(f"Multiply 4 * 7: {my_tools.multiply(4, 7)}\n")

# Import specific module
from my_tools import string_ops

print("=== Using Specific Module ===")
print(f"Lowercase (direct): {string_ops.lowercase('HELLO')}")

# Nested package
print("\n=== Creating Nested Package ===")

utils = base / 'utils'
utils.mkdir(exist_ok=True)
(utils / '__init__.py').write_text('"""Utility subpackage."""')
(utils / 'file_ops.py').write_text('''
def read_file(path):
    with open(path) as f:
        return f.read()
''')

print("✓ Created nested package: my_tools/utils/")

# Use nested package
from my_tools.utils import file_ops
print("✓ Can import: from my_tools.utils import file_ops")
```
