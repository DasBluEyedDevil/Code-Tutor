---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Using from module import * (Wildcard Imports)**
```python
# WRONG - Pollutes namespace, hides where names come from
from math import *
from random import *
sqrt(16)  # Which module? Hard to track!

# CORRECT - Import specifically
from math import sqrt, pi
from random import randint
sqrt(16)  # Clear it's from math
```

**2. Circular Imports**
```python
# WRONG - module_a imports module_b, module_b imports module_a
# module_a.py
from module_b import func_b  # Fails if module_b imports module_a!

# CORRECT - Import inside function or restructure
# module_a.py
def func_a():
    from module_b import func_b  # Import when needed
    return func_b()
```

**3. Shadowing Built-in Module Names**
```python
# WRONG - File named "random.py" shadows built-in
# random.py
import random  # Imports itself, not built-in!
print(random.randint(1, 10))  # AttributeError!

# CORRECT - Don't name files after built-in modules
# my_random.py or utils.py
import random
print(random.randint(1, 10))  # Works
```

**4. Importing Without Installing**
```python
# WRONG - Assuming package is installed
import requests  # ModuleNotFoundError if not installed!

# CORRECT - Check and provide helpful message
try:
    import requests
except ImportError:
    print("Please install requests: pip install requests")
    raise
```

**5. Relative vs Absolute Import Confusion**
```python
# WRONG - Relative import in script (not package)
from .utils import helper  # ImportError in script!

# CORRECT - Use absolute import in scripts
from mypackage.utils import helper
# Or relative only inside packages:
# from . import utils  # Only works inside a package
```