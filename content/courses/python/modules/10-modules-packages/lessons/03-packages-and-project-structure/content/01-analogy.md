---
type: "ANALOGY"
title: "The Concept: Organizing Your Library"
---

**Module = single file. Package = folder of modules.**

Imagine a library:
- **Module** = single book
- **Package** = bookshelf with related books organized together

**Package structure:**
```
my_package/
  __init__.py       ← Makes it a package!
  module1.py
  module2.py
  sub_package/
    __init__.py
    module3.py
```

**Real example - web framework:**
```
flask/
  __init__.py
  app.py
  routing.py
  templating/
    __init__.py
    jinja.py
```

**The magic __init__.py:**
- Empty file that tells Python "this directory is a package"
- Can contain initialization code
- Controls what `from package import *` imports