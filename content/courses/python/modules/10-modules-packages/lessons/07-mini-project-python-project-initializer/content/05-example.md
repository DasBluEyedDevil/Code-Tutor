---
type: "EXAMPLE"
title: "Step 4: Main Application"
---

**Main application** ties everything together:
- Imports from our custom packages
- Uses template modules to get configurations
- Uses file_utils to create structure
- Demonstrates practical module usage
- CLI interface with `if __name__ == '__main__':`
- Can be run as script or imported as module

```python
# project_initializer/main.py
"""Main entry point for project initializer."""

from pathlib import Path
import sys

# Import from our package
from utils.file_utils import create_directory_structure, write_file, create_init_file
from templates import get_available_templates, get_template

def create_project(project_name, template_type='web'):
    """Create a new Python project from template.
    
    Args:
        project_name: Name of the project
        template_type: Type of template ('web' or 'data')
    """
    print(f"\n{'='*50}")
    print(f"Creating {template_type.upper()} project: {project_name}")
    print('='*50 + '\n')
    
    # Get template configuration
    try:
        template = get_template(template_type)
    except ValueError as e:
        print(f"Error: {e}")
        print(f"Available templates: {', '.join(get_available_templates())}")
        return
    
    # Create base directory
    project_path = Path(project_name)
    if project_path.exists():
        print(f"Error: Directory '{project_name}' already exists!")
        return
    
    project_path.mkdir()
    print(f"✓ Created project directory: {project_name}\n")
    
    # Create folder structure
    print("Creating directory structure...")
    create_directory_structure(project_path, template['structure'])
    
    # Create __init__.py files for Python packages
    print("\nCreating package files...")
    for folder in ['app', 'src', 'tests'] if template_type == 'web' else ['src', 'tests']:
        folder_path = project_path / folder
        if folder_path.exists():
            create_init_file(folder_path)
    
    # Write main application file
    print("\nCreating main application...")
    main_file = project_path / ('app.py' if template_type == 'web' else 'main.py')
    write_file(main_file, template['main_code'])
    
    # Write requirements.txt
    print("\nCreating requirements.txt...")
    write_file(project_path / 'requirements.txt', template['requirements'])
    
    # Write .env file for web projects
    if template_type == 'web' and 'env' in template:
        print("Creating .env template...")
        write_file(project_path / '.env.example', template['env'])
    
    # Create README
    readme_content = f"""# {project_name}

{template['description']}

## Setup

1. Create virtual environment:
   ```bash
   python -m venv venv
   source venv/bin/activate  # On Windows: venv\\Scripts\\activate
   ```

2. Install dependencies:
   ```bash
   pip install -r requirements.txt
   ```

3. Run the application:
   ```bash
   python {'app.py' if template_type == 'web' else 'main.py'}
   ```

## Project Structure

Generated using Python Project Initializer.
"""
    write_file(project_path / 'README.md', readme_content)
    
    # Success message
    print("\n" + "="*50)
    print("✓ Project created successfully!")
    print("="*50)
    print(f"\nNext steps:")
    print(f"1. cd {project_name}")
    print(f"2. python -m venv venv")
    print(f"3. source venv/bin/activate")
    print(f"4. pip install -r requirements.txt")
    print(f"5. python {'app.py' if template_type == 'web' else 'main.py'}")
    print("\nHappy coding! 🚀\n")

def main():
    """Main CLI interface."""
    print("\n" + "="*50)
    print("Python Project Initializer")
    print("="*50)
    
    # Get project name
    if len(sys.argv) > 1:
        project_name = sys.argv[1]
    else:
        project_name = input("\nProject name: ").strip()
    
    if not project_name:
        print("Error: Project name required!")
        return
    
    # Get project type
    print(f"\nAvailable templates: {', '.join(get_available_templates())}")
    if len(sys.argv) > 2:
        template_type = sys.argv[2]
    else:
        template_type = input("Template type (web/data) [web]: ").strip() or 'web'
    
    # Create the project
    create_project(project_name, template_type)

if __name__ == '__main__':
    main()
```
