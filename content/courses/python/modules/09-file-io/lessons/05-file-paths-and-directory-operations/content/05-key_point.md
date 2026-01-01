---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **from pathlib import Path** - Modern, cross-platform way to work with file paths. Replaces old os.path module.
- **Path() / 'file'** - Join paths with / operator. Works on Windows, Mac, Linux automatically. Better than string concatenation.
- **Path.cwd()** - Current working directory. **Path.home()** - User's home directory. Starting points for paths.
- **.mkdir(parents=True, exist_ok=True)** - Safely create directories. parents=True creates parent dirs, exist_ok=True doesn't error if exists.
- **.glob(pattern)** - Find files matching pattern. **.rglob(pattern)** - Recursive glob (search subdirectories too).
- **.exists(), .is_file(), .is_dir()** - Check if path exists and what type it is. Always check before operations.
- **.name, .stem, .suffix, .parent** - Get path components. name=filename, stem=filename without extension, suffix=extension, parent=parent directory.
- **.read_text() / .write_text()** - Quick file I/O. For simple text files, easier than open().
- **.iterdir()** - List directory contents. Returns Path objects for each item. Use .is_file() or .is_dir() to filter.
- **Cross-platform:** pathlib handles path differences between OS automatically. Write once, works everywhere!