---
type: "THEORY"
title: "Understanding the Concept"
---

Imagine you're giving directions to your house:

**Old way (os.path):** "Go to drive C, then folder Users, then folder Alice, then folder Documents, then file notes.txt"
```python
import os
path = os.path.join('C:', 'Users', 'Alice', 'Documents', 'notes.txt')
```

**New way (pathlib):** Just describe the path naturally!
```python
from pathlib import Path
path = Path('C:/Users/Alice/Documents') / 'notes.txt'
```

### Why pathlib is Better:

**1. Object-Oriented Design**
Instead of passing strings to functions, paths are objects with methods:
```python
# Old way (os.path)
import os
if os.path.exists(filepath):
    content = open(filepath).read()
    print(os.path.basename(filepath))

# New way (pathlib)
from pathlib import Path
path = Path(filepath)
if path.exists():
    content = path.read_text()
    print(path.name)
```

**2. The / Operator for Joining Paths**
```python
# Old way - clunky
path = os.path.join('folder', 'subfolder', 'file.txt')

# New way - natural!
path = Path('folder') / 'subfolder' / 'file.txt'
```

**3. Cross-Platform by Default**
Pathlib automatically uses the right separator:
- Windows: `C:\Users\Alice`
- Mac/Linux: `/home/alice`

**4. Built-in File Operations**
```python
path = Path('data.txt')
path.write_text('Hello!')  # Write file
content = path.read_text()  # Read file
path.unlink()  # Delete file
```

### Modern pathlib Patterns (Python 3.9+):

**1. Directory creation with exist_ok:**
```python
data_dir = Path("data")
data_dir.mkdir(exist_ok=True)  # No error if exists
```

**2. Glob for pattern matching:**
```python
files = list(data_dir.glob("*.json"))  # All JSON files
all_csvs = list(Path(".").rglob("*.csv"))  # Recursive
```

**3. with_suffix and with_stem (Python 3.9+):**
```python
path = Path("report.txt")
path.with_suffix(".md")  # report.md
path.with_stem("summary")  # summary.txt
```

### When to Use pathlib:
- Any new Python 3 project
- File system operations (create, delete, rename)
- Path manipulation (joining, splitting)
- Finding files (glob patterns)
- Reading/writing files

**pathlib is the modern standard** - prefer it over os.path for all new code!