---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Hardcoding Paths in Project Templates**
```python
# WRONG - Paths won't work on other systems
PROJECT_DIR = "C:/Users/me/projects/new_project"

# CORRECT - Use relative paths or user input
from pathlib import Path
PROJECT_DIR = Path.cwd() / project_name
```

**2. Not Handling Existing Files**
```python
# WRONG - Overwrites without asking
def create_project(name):
    Path(name).mkdir()  # FileExistsError if exists!
    (Path(name) / "main.py").write_text(template)  # Overwrites!

# CORRECT - Check and ask before overwriting
def create_project(name):
    path = Path(name)
    if path.exists():
        response = input(f"{name} exists. Overwrite? (y/n): ")
        if response.lower() != 'y':
            return
    path.mkdir(exist_ok=True)
```

**3. Forgetting Cross-Platform Compatibility**
```python
# WRONG - Unix-only commands in templates
TEMPLATE = '''#!/bin/bash
chmod +x run.py
./run.py
'''  # Fails on Windows!

# CORRECT - Use Python for cross-platform operations
TEMPLATE = '''#!/usr/bin/env python3
import subprocess
import sys
subprocess.run([sys.executable, "run.py"])
'''
```

**4. Not Validating User Input**
```python
# WRONG - Using user input directly in paths
project_name = input("Project name: ")
Path(project_name).mkdir()  # "../../../etc" could be dangerous!

# CORRECT - Validate and sanitize input
import re
project_name = input("Project name: ")
if not re.match(r'^[a-zA-Z][a-zA-Z0-9_-]*$', project_name):
    print("Invalid name. Use letters, numbers, -, _ only")
    sys.exit(1)
```

**5. Not Including Essential Files in Templates**
```python
# WRONG - Missing important files
def create_project(name):
    Path(name).mkdir()
    (Path(name) / "main.py").write_text("")  # No .gitignore, README, etc!

# CORRECT - Include all essential files
def create_project(name):
    base = Path(name)
    base.mkdir()
    (base / "main.py").write_text(main_template)
    (base / ".gitignore").write_text(gitignore_template)
    (base / "README.md").write_text(readme_template)
    (base / "pyproject.toml").write_text(pyproject_template)
```