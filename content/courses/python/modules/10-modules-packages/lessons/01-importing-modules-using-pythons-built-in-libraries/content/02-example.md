---
type: "EXAMPLE"
title: "Code Example: Import Styles"
---

**Import styles:**
1. `import math` - Use as math.sqrt()
2. `from math import sqrt` - Use directly as sqrt()
3. `import math as m` - Alias for shorter names
4. `from math import *` - Import everything (avoid this!)

**Best practice:** Use `import module` or `from module import specific_items`

```python
# Style 1: Import entire module
import math

print("=== Import Entire Module ===")
print(f"Square root of 16: {math.sqrt(16)}")
print(f"Pi: {math.pi}")
print(f"Sin(90°): {math.sin(math.radians(90))}\n")

# Style 2: Import specific functions
from math import sqrt, pi

print("=== Import Specific Functions ===")
print(f"Square root of 25: {sqrt(25)}")
print(f"Pi: {pi}\n")

# Style 3: Import with alias
import datetime as dt

print("=== Import with Alias ===")
now = dt.datetime.now()
print(f"Current time: {now}")
print(f"Year: {now.year}\n")

# Style 4: Import all (not recommended)
from random import *

print("=== Import All (use sparingly) ===")
print(f"Random number: {randint(1, 100)}")
print(f"Random choice: {choice(['apple', 'banana', 'cherry'])}\n")

# Popular modules
print("=== Common Built-in Modules ===")

import os
print(f"Current directory: {os.getcwd()}")

import sys
print(f"Python version: {sys.version.split()[0]}")

import time
start = time.time()
time.sleep(0.1)
print(f"Elapsed: {time.time() - start:.2f}s")
```
