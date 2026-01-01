---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Package = directory with __init__.py** (collection of modules)
- **__init__.py required** to make directory a package (can be empty)
- **Import from package:** `from package.module import function`
- **Relative imports:** `from .module import function` (inside package)
- **__all__ in __init__.py** controls what `import *` imports
- **Nested packages allowed** for organizing large projects