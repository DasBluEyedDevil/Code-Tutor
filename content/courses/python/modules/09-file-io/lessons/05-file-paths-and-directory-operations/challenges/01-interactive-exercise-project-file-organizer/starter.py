from pathlib import Path

def create_project_structure(base_path):
    """Create project directory structure.
    
    Args:
        base_path: Base directory path (string or Path)
    """
    # TODO: Convert to Path object
    # TODO: Create base directory
    # TODO: Create subdirectories: images, documents, code, data
    # Use mkdir(parents=True, exist_ok=True)
    pass

def find_files_by_extension(directory, extension):
    """Find all files with specific extension.
    
    Args:
        directory: Directory to search
        extension: File extension (e.g., '.txt', '.py')
        
    Returns:
        list: List of Path objects
    """
    # TODO: Convert directory to Path
    # TODO: Use glob to find files with extension
    # TODO: Return list of matching files
    pass

def organize_files(source_dir, dest_dir):
    """Organize files by type into folders.
    
    Args:
        source_dir: Source directory with mixed files
        dest_dir: Destination directory (project structure)
    """
    # TODO: Define file type mappings
    # Images: .jpg, .png, .gif → images/
    # Documents: .txt, .pdf, .doc → documents/
    # Code: .py, .js, .html → code/
    # Data: .csv, .json, .xml → data/
    
    # TODO: For each file in source_dir:
    #   - Get file extension
    #   - Determine destination folder
    #   - Copy or move file to destination
    pass

def list_project_files(project_dir):
    """List all files in project structure.
    
    Args:
        project_dir: Project directory path
    """
    # TODO: For each subdirectory:
    #   - Print directory name
    #   - List files in that directory
    pass

# Test your functions
print("=== File Organizer ===")

print("\n1. Creating project structure...")
create_project_structure('my_project')

print("\n2. Creating test files...")
# Create some test files
test_files = [
    'photo1.jpg', 'photo2.png',
    'document.txt', 'report.pdf',
    'script.py', 'app.js',
    'data.csv', 'config.json'
]

for filename in test_files:
    Path(filename).write_text(f'Content of {filename}')
print(f"✓ Created {len(test_files)} test files")

print("\n3. Finding .jpg files...")
jpg_files = find_files_by_extension('.', '.jpg')
for f in jpg_files:
    print(f"  - {f.name}")

print("\n4. Organizing files...")
organize_files('.', 'my_project')

print("\n5. Project structure:")
list_project_files('my_project')