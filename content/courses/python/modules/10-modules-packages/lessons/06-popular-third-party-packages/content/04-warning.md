---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Not Reading Package Documentation**
```python
# WRONG - Assuming API without checking docs
import requests
requests.fetch("https://api.example.com")  # AttributeError!

# CORRECT - Read docs, use correct API
import requests
requests.get("https://api.example.com")  # Correct method
```

**2. Ignoring Package Deprecation Warnings**
```python
# WRONG - Using deprecated functions
import pandas as pd
df.append(new_row)  # DeprecationWarning, removed in future!

# CORRECT - Use recommended alternatives
import pandas as pd
df = pd.concat([df, new_row])  # Current approach
```

**3. Installing Wrong Package (Typosquatting)**
```python
# WRONG - Typo could install malicious package!
uv add reqeusts  # Misspelled - could be malware!

# CORRECT - Double-check package names
uv add requests  # Correct spelling
# Also verify on pypi.org before installing
```

**4. Not Checking Package Compatibility**
```python
# WRONG - Package doesn't support your Python version
# Python 3.8 project
uv add package-requiring-3.10  # Installation fails!

# CORRECT - Check Python version requirements first
# Read package docs or pypi.org for version compatibility
# Use: uv add "package>=1.0,<2.0; python_version>='3.10'"
```

**5. Importing Unused Heavy Packages**
```python
# WRONG - Importing everything slows startup
import pandas  # Heavy import even if barely used
import numpy

# CORRECT - Import only what you need, when you need it
def process_data(file):
    import pandas as pd  # Import only when function called
    return pd.read_csv(file)
```