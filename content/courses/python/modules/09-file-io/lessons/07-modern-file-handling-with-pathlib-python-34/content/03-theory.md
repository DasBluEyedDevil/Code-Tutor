---
type: "THEORY"
title: "Syntax Breakdown"
---

### Creating Paths:

```python
from pathlib import Path

# From string
path = Path('/home/user/file.txt')

# From multiple parts
path = Path('folder', 'subfolder', 'file.txt')

# Using / operator (recommended!)
path = Path('folder') / 'subfolder' / 'file.txt'

# Current directory
current = Path('.')  # or Path.cwd()

# Home directory
home = Path.home()  # e.g., /home/alice or C:\Users\Alice
```

### Path Properties:

| Property | Example for `/home/user/data.csv` |
|----------|-----------------------------------|
| `.name` | `data.csv` |
| `.stem` | `data` |
| `.suffix` | `.csv` |
| `.parent` | `/home/user` |
| `.parts` | `('/', 'home', 'user', 'data.csv')` |
| `.anchor` | `/` (root) |

### Checking Paths:

```python
path = Path('some/path')

path.exists()     # Does it exist?
path.is_file()    # Is it a file?
path.is_dir()     # Is it a directory?
path.is_absolute()  # Is it absolute path?
```

### Reading and Writing:

```python
path = Path('file.txt')

# Write text (creates file)
path.write_text('Hello world')

# Read text
content = path.read_text()

# Write bytes
path.write_bytes(b'\x00\x01\x02')

# Read bytes
data = path.read_bytes()
```

### Directory Operations:

```python
dir_path = Path('my_folder')

# Create directory
dir_path.mkdir()  # Error if parent doesn't exist
dir_path.mkdir(parents=True)  # Create parents too
dir_path.mkdir(exist_ok=True)  # No error if exists

# List contents
for item in dir_path.iterdir():
    print(item)

# Delete empty directory
dir_path.rmdir()
```

### Finding Files (glob):

```python
path = Path('project')

# Find by pattern
path.glob('*.py')  # All .py files in directory
path.glob('**/*.py')  # All .py files recursively
path.glob('data_*.csv')  # Files starting with data_
path.glob('**/test_*.py')  # Test files anywhere
```

### File Operations:

```python
path = Path('file.txt')

# Rename/move
path.rename('new_name.txt')
path.rename(Path('other_folder') / 'file.txt')

# Delete file
path.unlink()
path.unlink(missing_ok=True)  # No error if missing (3.8+)

# Get file info
stats = path.stat()
print(stats.st_size)  # File size in bytes
print(stats.st_mtime)  # Modification time
```

### pathlib vs os.path Comparison:

| Task | os.path (old) | pathlib (new) |
|------|---------------|---------------|
| Join paths | `os.path.join('a', 'b')` | `Path('a') / 'b'` |
| Get filename | `os.path.basename(p)` | `path.name` |
| Get extension | `os.path.splitext(p)[1]` | `path.suffix` |
| Check exists | `os.path.exists(p)` | `path.exists()` |
| Read file | `open(p).read()` | `path.read_text()` |
| Create dir | `os.makedirs(p)` | `path.mkdir(parents=True)` |
| List files | `os.listdir(p)` | `path.iterdir()` |
| Find files | `glob.glob('*.py')` | `path.glob('*.py')` |