---
type: "ANALOGY"
title: "Problem 3: \"Waiting for another flutter command...\""
---


### Error message:

### What happened:
A previous Flutter command didn't finish properly and left a lock file.

### Solution:




```bash
# Kill the lock file
cd <your-flutter-installation>
rm -f bin/cache/lockfile

# Windows PowerShell:
Remove-Item -Force bin/cache/lockfile

# Or just restart your computer (easiest)
```
