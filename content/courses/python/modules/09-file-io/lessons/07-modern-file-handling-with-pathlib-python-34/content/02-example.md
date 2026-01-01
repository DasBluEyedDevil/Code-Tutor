---
type: "EXAMPLE"
title: "Code Example: Pathlib Essentials"
---

**Expected Output:**
```
=== Creating Paths ===
Path: folder/subfolder/file.txt
Using / operator: data/users/config.json

=== Path Properties ===
Full path: /home/user/project/data/report.csv
Name: report.csv
Stem (no extension): report
Suffix: .csv
Parent: /home/user/project/data
Parts: ('/', 'home', 'user', 'project', 'data', 'report.csv')

=== Checking Paths ===
Current directory exists: True
Is it a directory: True
Is it a file: False

=== Reading and Writing ===
Wrote: pathlib_demo.txt
Read content: Hello from pathlib!
This is line 2.

=== Finding Files ===
Python files: ['script.py', 'utils.py', 'main.py']
All text files (recursive): ['notes.txt', 'data/log.txt']

=== Directory Operations ===
Created: output/reports/2024
Files in current dir: [list of files]
```

```python
# Modern File Handling with Pathlib
from pathlib import Path

print("=== Creating Paths ===")

# Create a path using / operator (works on all platforms!)
path = Path('folder') / 'subfolder' / 'file.txt'
print(f"Path: {path}")

# Multiple ways to create paths
path1 = Path('data', 'users', 'config.json')  # Using arguments
path2 = Path('data/users/config.json')  # Using string
path3 = Path('data') / 'users' / 'config.json'  # Using / operator
print(f"Using / operator: {path3}")
print()

print("=== Path Properties ===")

# Path has useful properties
file_path = Path('/home/user/project/data/report.csv')
print(f"Full path: {file_path}")
print(f"Name: {file_path.name}")  # report.csv
print(f"Stem (no extension): {file_path.stem}")  # report
print(f"Suffix: {file_path.suffix}")  # .csv
print(f"Parent: {file_path.parent}")  # /home/user/project/data
print(f"Parts: {file_path.parts}")  # All path components
print()

print("=== Checking Paths ===")

# Check if paths exist
current = Path('.')
print(f"Current directory exists: {current.exists()}")
print(f"Is it a directory: {current.is_dir()}")
print(f"Is it a file: {current.is_file()}")
print()

print("=== Reading and Writing ===")

# Write text to file (creates if doesn't exist)
demo_file = Path('pathlib_demo.txt')
demo_file.write_text('Hello from pathlib!\nThis is line 2.')
print(f"Wrote: {demo_file}")

# Read text from file
content = demo_file.read_text()
print(f"Read content: {content}")
print()

print("=== Finding Files ===")

# glob() finds files matching a pattern
current_dir = Path('.')

# Find all .py files in current directory
py_files = list(current_dir.glob('*.py'))
print(f"Python files: {[f.name for f in py_files[:3]]}")

# Find files recursively with **
all_txt = list(current_dir.glob('**/*.txt'))
print(f"All text files (recursive): {[str(f) for f in all_txt[:3]]}")
print()

print("=== Directory Operations ===")

# Create directories (including parents)
output_dir = Path('output') / 'reports' / '2024'
output_dir.mkdir(parents=True, exist_ok=True)
print(f"Created: {output_dir}")

# List directory contents
files_in_current = list(Path('.').iterdir())
print(f"Files in current dir: {[f.name for f in files_in_current[:5]]}")
print()

print("=== Practical Example: Organize Files ===")

def organize_downloads(download_folder):
    """Organize files by extension."""
    downloads = Path(download_folder)
    
    # Define categories
    categories = {
        '.pdf': 'Documents',
        '.jpg': 'Images',
        '.png': 'Images',
        '.mp3': 'Music',
        '.mp4': 'Videos',
    }
    
    for file in downloads.iterdir():
        if file.is_file():
            # Get the category folder
            suffix = file.suffix.lower()
            category = categories.get(suffix, 'Other')
            
            # Create category folder
            dest_folder = downloads / category
            dest_folder.mkdir(exist_ok=True)
            
            # Move file (in real code)
            # file.rename(dest_folder / file.name)
            print(f"Would move {file.name} -> {category}/")

# Demo (won't actually run without a downloads folder)
print("File organizer function created!")

# Cleanup demo file
demo_file.unlink()  # Delete the file
print(f"Cleaned up: {demo_file}")
```
