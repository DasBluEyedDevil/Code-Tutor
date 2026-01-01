---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Mixing Path Objects and Strings Incorrectly**
```python
# WRONG - String methods don't work on Path
from pathlib import Path
path = Path("folder/file.txt")
path.split("/")  # AttributeError!

# CORRECT - Use Path methods or convert to string
from pathlib import Path
path = Path("folder/file.txt")
parts = path.parts  # ('folder', 'file.txt')
# Or: str(path).split("/") if you need string ops
```

**2. Forgetting Path() Returns New Object**
```python
# WRONG - Original path unchanged
from pathlib import Path
path = Path("folder")
path.with_suffix(".txt")  # Returns new Path, doesn't modify!
print(path)  # Still "folder"

# CORRECT - Assign the result
from pathlib import Path
path = Path("folder")
path = path.with_suffix(".txt")  # Assign new path
```

**3. Using os.path When pathlib Would Be Cleaner**
```python
# WRONG - Verbose and less readable
import os
base = os.path.dirname(os.path.abspath(__file__))
path = os.path.join(base, "data", "file.txt")
name = os.path.splitext(os.path.basename(path))[0]

# CORRECT - pathlib is cleaner
from pathlib import Path
base = Path(__file__).parent
path = base / "data" / "file.txt"
name = path.stem
```

**4. Not Using resolve() for Symlinks**
```python
# WRONG - Symlinks not resolved, may cause issues
from pathlib import Path
path = Path("link_to_file")
print(path.exists())  # True, but where does it point?

# CORRECT - Resolve to real path
from pathlib import Path
path = Path("link_to_file").resolve()
print(path)  # Shows actual file location
```

**5. Iterating Directory Without Error Handling**
```python
# WRONG - Crashes on permission denied
from pathlib import Path
for f in Path("/root").iterdir():  # PermissionError!
    print(f)

# CORRECT - Handle permission errors
from pathlib import Path
try:
    for f in Path("/root").iterdir():
        print(f)
except PermissionError:
    print("Cannot access directory")
```