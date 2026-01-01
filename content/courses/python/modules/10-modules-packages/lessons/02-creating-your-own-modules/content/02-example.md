---
type: "EXAMPLE"
title: "Code Example: Creating a Module"
---

**Key concepts:**
1. Module = .py file with functions/classes/variables
2. Import with `import filename` (no .py extension!)
3. `if __name__ == '__main__':` - Code that runs only when file executed directly, not when imported
4. Can define constants, functions, classes in module

```python
# First, create a module file: math_utils.py
# (In real project, save as separate file)

math_utils_code = '''
def add(a, b):
    """Add two numbers."""
    return a + b

def multiply(a, b):
    """Multiply two numbers."""
    return a * b

def is_even(n):
    """Check if number is even."""
    return n % 2 == 0

PI = 3.14159

if __name__ == "__main__":
    # This code runs only when file is executed directly
    print("Testing math_utils...")
    print(f"5 + 3 = {add(5, 3)}")
    print(f"5 * 3 = {multiply(5, 3)}")
    print(f"Is 4 even? {is_even(4)}")
'''

# Save the module
from pathlib import Path
Path('math_utils.py').write_text(math_utils_code)

print("=== Created math_utils.py module ===")
print(math_utils_code)
print()

# Now import and use it
import math_utils

print("=== Using the Module ===")
print(f"10 + 5 = {math_utils.add(10, 5)}")
print(f"10 * 5 = {math_utils.multiply(10, 5)}")
print(f"Is 7 even? {math_utils.is_even(7)}")
print(f"Pi constant: {math_utils.PI}")
```
