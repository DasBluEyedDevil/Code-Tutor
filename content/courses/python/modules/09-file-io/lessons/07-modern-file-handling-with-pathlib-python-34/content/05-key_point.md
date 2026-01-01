---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **pathlib is the modern way** to handle file paths in Python 3
- **Use `/` operator** to join paths: `Path('folder') / 'file.txt'`
- **Path objects have properties:** `.name`, `.stem`, `.suffix`, `.parent`
- **Built-in file operations:** `read_text()`, `write_text()`, `unlink()`
- **Check paths:** `exists()`, `is_file()`, `is_dir()`
- **Create directories:** `mkdir(parents=True, exist_ok=True)`
- **Find files with glob:** `path.glob('*.py')`, `path.glob('**/*.txt')`
- **Cross-platform automatically** - no need to worry about `/` vs `\`
- **Prefer pathlib over os.path** for all new Python 3 code

### Quick Reference:

```python
from pathlib import Path

# Create paths
path = Path('folder') / 'file.txt'

# Path info
path.name      # filename with extension
path.stem      # filename without extension
path.suffix    # extension (.txt)
path.parent    # parent directory

# Check path
path.exists()  # does it exist?
path.is_file() # is it a file?
path.is_dir()  # is it a directory?

# Read/write
path.read_text()       # read file content
path.write_text('hi')  # write to file

# Directory ops
path.mkdir(parents=True, exist_ok=True)
path.iterdir()  # list contents

# Find files
path.glob('*.py')      # find .py files
path.glob('**/*.txt')  # recursive search
```