---
type: "EXAMPLE"
title: "Step 3: Package Initialization Files"
---

**__init__.py files** define package interfaces:
- Main `__init__.py`: Package metadata, version, convenient imports
- `utils/__init__.py`: Exports utility functions
- `templates/__init__.py`: Template registry and getter functions
- `__all__` controls what's exported
- Runs initialization code when package is imported

```python
# project_initializer/__init__.py
"""Python Project Initializer Package.

A tool for quickly setting up new Python projects with proper structure,
dependencies, and example code.
"""

__version__ = '1.0.0'
__author__ = 'Your Name'

# Import main functionality
from .utils.file_utils import create_directory_structure, write_file
from .templates import web_template, data_template

# Define what gets imported with 'from project_initializer import *'
__all__ = [
    'create_directory_structure',
    'write_file',
    'web_template',
    'data_template',
    'create_project'
]

def create_project(project_name, project_type='general'):
    """Convenience function to create a new project."""
    print(f"Creating {project_type} project: {project_name}")
    # Implementation in main.py

print(f"Project Initializer v{__version__} loaded")

# project_initializer/utils/__init__.py
"""Utility modules for project initialization."""

from .file_utils import (
    create_directory_structure,
    write_file,
    create_init_file
)

__all__ = ['create_directory_structure', 'write_file', 'create_init_file']

# project_initializer/templates/__init__.py
"""Project templates for different types of Python projects."""

from . import web_template
from . import data_template

TEMPLATES = {
    'web': web_template,
    'data': data_template
}

def get_available_templates():
    """Return list of available project templates."""
    return list(TEMPLATES.keys())

def get_template(template_type):
    """Get template configuration by type."""
    if template_type in TEMPLATES:
        return TEMPLATES[template_type].get_template()
    raise ValueError(f"Unknown template: {template_type}")

__all__ = ['TEMPLATES', 'get_available_templates', 'get_template']
```
