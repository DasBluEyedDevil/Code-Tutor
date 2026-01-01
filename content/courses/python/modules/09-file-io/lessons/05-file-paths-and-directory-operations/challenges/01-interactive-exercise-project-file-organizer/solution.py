from pathlib import Path
import shutil

# Project File Organizer
# This solution demonstrates pathlib for file system operations

def create_project_structure(base_path):
    """Create project directory structure."""
    # Step 1: Convert to Path object
    base = Path(base_path)
    
    # Step 2: Define subdirectories
    subdirs = ['images', 'documents', 'code', 'data']
    
    # Step 3: Create base and all subdirectories
    for subdir in subdirs:
        (base / subdir).mkdir(parents=True, exist_ok=True)
    
    print(f"Created project structure at '{base_path}'")

def find_files_by_extension(directory, extension):
    """Find all files with specific extension."""
    # Convert to Path and use glob
    path = Path(directory)
    
    # Make sure extension starts with dot
    if not extension.startswith('.'):
        extension = '.' + extension
    
    # Use glob to find matching files
    return list(path.glob('*' + extension))

def organize_files(source_dir, dest_dir):
    """Organize files by type into folders."""
    # Step 1: Define file type mappings
    type_mappings = {
        'images': ['.jpg', '.png', '.gif', '.jpeg', '.bmp'],
        'documents': ['.txt', '.pdf', '.doc', '.docx', '.md'],
        'code': ['.py', '.js', '.html', '.css', '.java'],
        'data': ['.csv', '.json', '.xml', '.yaml', '.xlsx']
    }
    
    source = Path(source_dir)
    dest = Path(dest_dir)
    
    # Step 2: Process each file in source directory
    for file_path in source.iterdir():
        if file_path.is_file():
            ext = file_path.suffix.lower()
            
            # Find matching folder
            for folder, extensions in type_mappings.items():
                if ext in extensions:
                    # Copy file to appropriate folder
                    dest_path = dest / folder / file_path.name
                    shutil.copy2(file_path, dest_path)
                    print(f"  Copied {file_path.name} to {folder}/")
                    break

def list_project_files(project_dir):
    """List all files in project structure."""
    project = Path(project_dir)
    
    for subdir in sorted(project.iterdir()):
        if subdir.is_dir():
            print(f"\n{subdir.name}/")
            files = list(subdir.iterdir())
            if files:
                for file in files:
                    print(f"  - {file.name}")
            else:
                print("  (empty)")

# Test the functions
print("=== File Organizer ===")

print("\n1. Creating project structure...")
create_project_structure('my_project')

print("\n2. Creating test files...")
test_files = [
    'photo1.jpg', 'photo2.png',
    'document.txt', 'report.pdf',
    'script.py', 'app.js',
    'data.csv', 'config.json'
]

for filename in test_files:
    Path(filename).write_text(f'Content of {filename}')
print(f"Created {len(test_files)} test files")

print("\n3. Finding .jpg files...")
jpg_files = find_files_by_extension('.', '.jpg')
for f in jpg_files:
    print(f"  - {f.name}")

print("\n4. Organizing files...")
organize_files('.', 'my_project')

print("\n5. Project structure:")
list_project_files('my_project')