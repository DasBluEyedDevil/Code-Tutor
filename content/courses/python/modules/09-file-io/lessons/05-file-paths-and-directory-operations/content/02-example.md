---
type: "EXAMPLE"
title: "Code Example: Working with Paths"
---

**Key Path operations:**

1. **Path.cwd()** - Current working directory
2. **Path.home()** - User's home directory
3. **Path() / 'file'** - Join paths (cross-platform!)
4. **.exists()** - Check if path exists
5. **.is_file() / .is_dir()** - Check type
6. **.mkdir()** - Create directory
7. **.glob(pattern)** - Find files matching pattern
8. **.rglob(pattern)** - Recursive glob (search subdirectories)
9. **.read_text() / .write_text()** - Quick file I/O
10. **.resolve()** - Convert to absolute path

**mkdir parameters:**
- `parents=True` - Create parent directories if needed
- `exist_ok=True` - Don't error if directory already exists

```python
from pathlib import Path
import os

# Example 1: Creating paths
print("=== Creating Paths ===")

# Current directory
current = Path.cwd()  # cwd = current working directory
print(f"Current directory: {current}")

# Home directory
home = Path.home()
print(f"Home directory: {home}")

# Build path with / operator (cross-platform!)
data_file = Path('data') / 'users.txt'
print(f"Data file path: {data_file}")

# Multiple levels
config_path = Path('config') / 'settings' / 'app.json'
print(f"Config path: {config_path}\n")

# Example 2: Path information
print("=== Path Information ===")

path = Path('projects/myapp/src/main.py')

print(f"Full path: {path}")
print(f"Name: {path.name}")  # main.py
print(f"Stem: {path.stem}")  # main (without extension)
print(f"Suffix: {path.suffix}")  # .py
print(f"Parent: {path.parent}")  # projects/myapp/src
print(f"Parents[0]: {path.parents[0]}")  # immediate parent
print(f"Parents[1]: {path.parents[1]}")  # grandparent
print(f"Parts: {path.parts}\n")  # ('projects', 'myapp', 'src', 'main.py')

# Example 3: Checking existence
print("=== Checking Existence ===")

# Create a test file
test_file = Path('test.txt')
test_file.write_text('Hello, World!')

print(f"test.txt exists: {test_file.exists()}")
print(f"test.txt is file: {test_file.is_file()}")
print(f"test.txt is directory: {test_file.is_dir()}")

# Check non-existent
fake = Path('nonexistent.txt')
print(f"nonexistent.txt exists: {fake.exists()}\n")

# Example 4: Creating directories
print("=== Creating Directories ===")

# Create single directory
Path('output').mkdir(exist_ok=True)
print("✓ Created 'output' directory")

# Create nested directories
Path('data/processed/2024').mkdir(parents=True, exist_ok=True)
print("✓ Created nested 'data/processed/2024' directories\n")

# Example 5: Listing directory contents
print("=== Listing Directory Contents ===")

# List all items in current directory
print("Files in current directory:")
for item in Path('.').iterdir():
    if item.is_file():
        print(f"  📄 {item.name}")

print("\nDirectories in current directory:")
for item in Path('.').iterdir():
    if item.is_dir():
        print(f"  📁 {item.name}")

# Example 6: Glob patterns (finding files)
print("\n=== Finding Files with Glob ===")

# Create some test files
for i in range(3):
    (Path('output') / f'file{i}.txt').write_text(f'Content {i}')
    (Path('output') / f'data{i}.json').write_text('{}')

print("Created test files in output/")

# Find all .txt files
print("\nAll .txt files in output/:")
for file in Path('output').glob('*.txt'):
    print(f"  - {file.name}")

# Find all .json files
print("\nAll .json files in output/:")
for file in Path('output').glob('*.json'):
    print(f"  - {file.name}")

# Recursive glob (search subdirectories too)
print("\nAll .txt files (recursive):")
for file in Path('.').rglob('*.txt'):
    print(f"  - {file}")

print("")

# Example 7: Reading and writing with Path
print("=== Reading/Writing with Path ===")

data_path = Path('data.txt')

# Write text
data_path.write_text('Line 1\nLine 2\nLine 3\n')
print("✓ Wrote data.txt")

# Read text
content = data_path.read_text()
print("\nContent:")
print(content)

# Read lines
lines = data_path.read_text().splitlines()
print(f"Number of lines: {len(lines)}\n")

# Example 8: Absolute vs Relative paths
print("=== Absolute vs Relative Paths ===")

relative = Path('data/file.txt')
print(f"Relative: {relative}")
print(f"Absolute: {relative.resolve()}")
print(f"Is absolute: {relative.is_absolute()}")

absolute = Path.cwd() / 'data' / 'file.txt'
print(f"\nAbsolute: {absolute}")
print(f"Is absolute: {absolute.is_absolute()}\n")

# Example 9: Joining paths safely
print("=== Joining Paths (Cross-Platform) ===")

base = Path('projects')
subdir = 'myapp'
filename = 'config.json'

# Method 1: / operator
path1 = base / subdir / filename
print(f"Using /: {path1}")

# Method 2: joinpath
path2 = base.joinpath(subdir, filename)
print(f"Using joinpath: {path2}")

print("\n✓ Both create correct path for your OS!")
print("(Forward slashes on Unix, backslashes on Windows)")
```
