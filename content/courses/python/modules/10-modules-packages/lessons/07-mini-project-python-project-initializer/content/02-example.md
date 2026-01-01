---
type: "EXAMPLE"
title: "Step 1: File Utilities Module"
---

**file_utils.py** provides reusable file operations:
- `create_directory_structure()`: Recursively creates folders
- `write_file()`: Safely writes files
- `create_init_file()`: Creates `__init__.py` files
- Uses pathlib for cross-platform paths
- `if __name__ == '__main__':` allows testing the module

```python
# project_initializer/utils/file_utils.py
"""Utility functions for file and directory operations."""

from pathlib import Path
import json

def create_directory_structure(base_path, structure):
    """Create directories from a nested dictionary structure.
    
    Args:
        base_path: Root directory path
        structure: Dict representing folder structure
    
    Example:
        structure = {
            'src': {
                'utils': {},
                'models': {}
            }
        }
    """
    base = Path(base_path)
    base.mkdir(exist_ok=True)
    
    for name, subdirs in structure.items():
        dir_path = base / name
        dir_path.mkdir(exist_ok=True)
        print(f"✓ Created: {dir_path}")
        
        if subdirs:
            create_directory_structure(dir_path, subdirs)

def write_file(path, content):
    """Write content to a file."""
    file_path = Path(path)
    file_path.parent.mkdir(parents=True, exist_ok=True)
    file_path.write_text(content)
    print(f"✓ Created: {file_path}")

def create_init_file(directory, content=""):
    """Create __init__.py file in directory."""
    init_path = Path(directory) / "__init__.py"
    init_path.write_text(content)
    print(f"✓ Created: {init_path}")

if __name__ == "__main__":
    # Test the module
    print("Testing file_utils module...\n")
    
    test_structure = {
        'test_project': {
            'src': {},
            'tests': {}
        }
    }
    
    create_directory_structure('temp', test_structure)
    print("\n✓ Module works correctly!")
```
