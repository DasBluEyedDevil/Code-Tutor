---
type: "EXAMPLE"
title: "Code Example: What Ruff Catches"
---

**Ruff catches many types of issues.** This example shows common problems and how Ruff identifies them with rule codes.

**Rule code format:**
- Letter(s) = category (E=pycodestyle, F=pyflakes, I=isort, etc.)
- Number = specific rule within category
- Example: F401 = unused import, E501 = line too long

```python
# This file contains intentional errors for Ruff to catch
# Run: ruff check example.py

print("="*60)
print("COMMON ISSUES RUFF CATCHES")
print("="*60)

print("""
# ============================================
# EXAMPLE CODE WITH ISSUES
# ============================================
""")

bad_code = '''
# Save this as example.py and run: ruff check example.py

import os              # F401: unused import
import json           # Used below
from typing import List  # UP006: Use list instead of List (Python 3.9+)
import sys            # I001: imports should be sorted

def calculate(x,y):   # E231: missing whitespace after ','
    result=x+y        # E225: missing whitespace around operator
    unused_var = 10   # F841: local variable never used
    return result

def long_function_with_very_long_name_that_exceeds_line_length_limit(parameter_one, parameter_two, parameter_three):  # E501: line too long
    pass

if True == True:      # E712: comparison to True should be 'if True:'
    print("hello")    

x = []                
if len(x) == 0:       # SIM103: use 'if not x:' instead
    print("empty")

data = json.loads('{"a": 1}')
'''
print(bad_code)

print("""
# ============================================
# RUFF OUTPUT FOR THIS CODE
# ============================================

$ ruff check example.py

example.py:3:8: F401 [*] `os` imported but unused
example.py:5:20: UP006 [*] Use `list` instead of `List` for type annotation
example.py:6:1: I001 [*] Import block is un-sorted or un-formatted
example.py:8:17: E231 [*] Missing whitespace after ','
example.py:9:11: E225 [*] Missing whitespace around operator
example.py:10:5: F841 [*] Local variable `unused_var` is assigned but never used
example.py:13:89: E501 Line too long (120 > 88 characters)
example.py:16:4: E712 [*] Comparison to `True` should be `cond is True` or `if cond:`
example.py:20:4: SIM103 [*] Return the condition directly

[*] = auto-fixable with --fix

# ============================================
# AFTER RUNNING: ruff check --fix example.py
# ============================================
""")

fixed_code = '''
import json
import sys  # Now sorted, unused 'os' removed


def calculate(x, y):  # Proper spacing
    result = x + y    # Proper spacing
    return result     # Unused variable removed


def long_function(  # Split long line
    parameter_one,
    parameter_two,
    parameter_three,
):
    pass


if True:              # Simplified comparison
    print("hello")

x = []
if not x:             # Simplified check
    print("empty")

data = json.loads('{"a": 1}')
'''
print("\nFixed code:")
print(fixed_code)
```
