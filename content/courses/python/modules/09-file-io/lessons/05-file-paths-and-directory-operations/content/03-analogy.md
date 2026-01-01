---
type: "ANALOGY"
title: "Syntax Breakdown: pathlib Operations"
---

**Import pathlib:**
```python
from pathlib import Path
```

**Creating paths:**
```python
# Current directory
Path.cwd()

# Home directory
Path.home()

# From string
Path('folder/file.txt')
Path('/absolute/path/file.txt')

# Join paths (cross-platform!)
Path('folder') / 'subfolder' / 'file.txt'
```

**Path information:**
```python
path = Path('projects/myapp/src/main.py')

path.name      # 'main.py' (filename)
path.stem      # 'main' (filename without extension)
path.suffix    # '.py' (extension)
path.parent    # Path('projects/myapp/src')
path.parts     # ('projects', 'myapp', 'src', 'main.py')
```

**Checking existence:**
```python
path.exists()   # True if exists
path.is_file()  # True if file
path.is_dir()   # True if directory
path.is_absolute()  # True if absolute path
```

**Creating directories:**
```python
# Create directory
Path('output').mkdir()

# Create with parents
Path('data/processed/2024').mkdir(parents=True)

# Don't error if exists
Path('output').mkdir(exist_ok=True)

# Both options
Path('data/logs').mkdir(parents=True, exist_ok=True)
```

**Listing directory:**
```python
# List all items
for item in Path('.').iterdir():
    print(item)

# List only files
for item in Path('.').iterdir():
    if item.is_file():
        print(item)

# List only directories
for item in Path('.').iterdir():
    if item.is_dir():
        print(item)
```

**Finding files (glob):**
```python
# All .txt files in directory
for file in Path('folder').glob('*.txt'):
    print(file)

# All .py files (recursive)
for file in Path('.').rglob('*.py'):
    print(file)

# Specific pattern
for file in Path('.').glob('data_*.csv'):
    print(file)
```

**Reading/Writing:**
```python
path = Path('file.txt')

# Write text
path.write_text('Hello, World!')

# Read text
content = path.read_text()

# Write bytes
path.write_bytes(b'\x00\x01\x02')

# Read bytes
data = path.read_bytes()
```

**Path conversions:**
```python
# Relative to absolute
relative = Path('data/file.txt')
absolute = relative.resolve()

# Path to string
path_str = str(path)
```

**Deleting:**
```python
# Delete file
Path('file.txt').unlink()

# Delete empty directory
Path('folder').rmdir()

# Delete directory with contents (need shutil)
import shutil
shutil.rmtree('folder')
```

**Common patterns:**

**1. Process all files in directory:**
```python
for file in Path('data').glob('*.csv'):
    # Process each CSV file
    content = file.read_text()
    process(content)
```

**2. Create directory structure:**
```python
base = Path('project')
(base / 'src').mkdir(parents=True, exist_ok=True)
(base / 'tests').mkdir(exist_ok=True)
(base / 'data').mkdir(exist_ok=True)
```

**3. Safe file operations:**
```python
path = Path('data.txt')

if path.exists():
    content = path.read_text()
else:
    print('File not found')
```

**Why pathlib is better:**

❌ **Old way (os module):**
```python
import os
path = os.path.join('folder', 'file.txt')
if os.path.exists(path):
    with open(path) as f:
        content = f.read()
```

✅ **New way (pathlib):**
```python
from pathlib import Path
path = Path('folder') / 'file.txt'
if path.exists():
    content = path.read_text()
```