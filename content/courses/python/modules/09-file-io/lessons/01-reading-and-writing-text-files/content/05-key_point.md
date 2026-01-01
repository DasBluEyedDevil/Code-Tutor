---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Files persist data** beyond program execution. Unlike variables, file content stays after the program ends.
- **open(filename, mode)** opens a file. Returns a file object you can read from or write to.
- **Always close files** with file.close() to save changes and free resources. Without closing, changes may not be saved!
- **Read methods:** read() (entire file as string), readline() (one line), readlines() (list of lines), or iterate with for line in file:
- **Write method:** write(string) writes content. Must add \n yourself for new lines! write() doesn't add it automatically.
- **File modes:** 'r' (read, file must exist), 'w' (write, OVERWRITES existing), 'a' (append, adds to end without erasing).
- **'w' mode is destructive:** Opens in write mode ERASES all existing content! Use 'a' to add without erasing.
- **Handle FileNotFoundError** when reading files that might not exist. Use try/except for robust code.
- **strip() removes \n:** When reading lines, use line.strip() to remove trailing newline characters.
- **Next lesson:** Context managers with 'with' statement - a better way that auto-closes files!