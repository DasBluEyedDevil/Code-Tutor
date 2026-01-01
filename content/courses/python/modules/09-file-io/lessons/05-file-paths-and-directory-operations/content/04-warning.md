---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Hardcoding Path Separators**
```python
# WRONG - Breaks on different operating systems
path = "data\\files\\report.txt"  # Only works on Windows!
path = "data/files/report.txt"  # May fail on Windows

# CORRECT - Use pathlib or os.path.join
from pathlib import Path
path = Path("data") / "files" / "report.txt"  # Works everywhere
```

**2. Not Checking if Path Exists Before Operations**
```python
# WRONG - Crashes if directory doesn't exist
from pathlib import Path
Path("new_dir/file.txt").write_text("data")  # FileNotFoundError!

# CORRECT - Create parent directories first
from pathlib import Path
path = Path("new_dir/file.txt")
path.parent.mkdir(parents=True, exist_ok=True)
path.write_text("data")
```

**3. Using String Concatenation for Paths**
```python
# WRONG - Missing separator, fragile
folder = "data"
filename = "report.txt"
path = folder + filename  # "datareport.txt" - wrong!

# CORRECT - Use proper path joining
from pathlib import Path
path = Path(folder) / filename  # "data/report.txt"
```

**4. Not Handling Permissions Errors**
```python
# WRONG - Crashes on permission denied
from pathlib import Path
Path("/etc/passwd").write_text("hacked")  # PermissionError!

# CORRECT - Handle permission errors
from pathlib import Path
try:
    Path("somefile.txt").write_text("data")
except PermissionError:
    print("Cannot write to this location")
```

**5. Confusing Relative and Absolute Paths**
```python
# WRONG - Relative path changes with working directory
from pathlib import Path
path = Path("data/file.txt")
# If script runs from different directory, path is wrong!

# CORRECT - Use absolute path relative to script
from pathlib import Path
script_dir = Path(__file__).parent
path = script_dir / "data" / "file.txt"
```