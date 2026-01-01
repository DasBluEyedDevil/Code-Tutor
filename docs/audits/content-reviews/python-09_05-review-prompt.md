# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Python Backend Development (python)
- **Module:** File I/O
- **Lesson:** File Paths and Directory Operations (ID: 09_05)
- **Difficulty:** intermediate
- **Estimated Time:** 30 minutes

## Current Lesson Content

{
    "id":  "09_05",
    "contentSections":  [
                            {
                                "type":  "ANALOGY",
                                "title":  "The Concept: GPS for Your File System",
                                "content":  "**The Problem: Hardcoded Paths Break**\n\n```python\n# Windows path\nfile = open(\u0027C:\\\\Users\\\\Alice\\\\Documents\\\\data.txt\u0027)\n\n# Mac/Linux path  \nfile = open(\u0027/home/alice/documents/data.txt\u0027)\n\n# Breaks when you share code!\n```\n\n**Real-world analogy: Street Addresses**\n\nImagine giving directions:\n- ❌ \"Go to 123 Main St, Apartment 4B\" (hardcoded - only works in one city)\n- ✅ \"Go to my_home/living_room/couch\" (relative - works anywhere)\n\n**pathlib is your GPS for files:**\n- Cross-platform (works on Windows, Mac, Linux)\n- Relative paths (\"start from current location\")\n- Path operations (join, split, check existence)\n- Directory operations (create, list, delete)\n\n**Key concepts:**\n\n**1. Absolute vs Relative Paths:**\n```python\n# Absolute (full address from root)\n/home/alice/projects/myapp/data.txt\nC:\\Users\\Alice\\Projects\\myapp\\data.txt\n\n# Relative (from current location)\ndata.txt\n./data.txt\n../other_folder/file.txt\n```\n\n**2. Path Components:**\n```python\n/home/alice/projects/myapp/data.txt\n│     │     │        │      │\n│     │     │        │      └─ filename\n│     │     │        └─ parent directory\n│     │     └─ grandparent directory\n│     └─ great-grandparent\n└─ root\n```\n\n**3. Special paths:**\n- `.` = current directory\n- `..` = parent directory  \n- `~` = home directory\n- `/` = root directory (Unix)\n- `C:\\` = drive root (Windows)\n\n**Why pathlib over string concatenation:**\n\n❌ **Don\u0027t do this:**\n```python\npath = \u0027folder\u0027 + \u0027/\u0027 + \u0027file.txt\u0027  # Breaks on Windows!\npath = \u0027C:\\\\Users\\\\\u0027 + name  # Escape chars nightmare\n```\n\n✅ **Do this:**\n```python\nfrom pathlib import Path\npath = Path(\u0027folder\u0027) / \u0027file.txt\u0027  # Works everywhere!\n```"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Code Example: Working with Paths",
                                "content":  "**Key Path operations:**\n\n1. **Path.cwd()** - Current working directory\n2. **Path.home()** - User\u0027s home directory\n3. **Path() / \u0027file\u0027** - Join paths (cross-platform!)\n4. **.exists()** - Check if path exists\n5. **.is_file() / .is_dir()** - Check type\n6. **.mkdir()** - Create directory\n7. **.glob(pattern)** - Find files matching pattern\n8. **.rglob(pattern)** - Recursive glob (search subdirectories)\n9. **.read_text() / .write_text()** - Quick file I/O\n10. **.resolve()** - Convert to absolute path\n\n**mkdir parameters:**\n- `parents=True` - Create parent directories if needed\n- `exist_ok=True` - Don\u0027t error if directory already exists",
                                "code":  "from pathlib import Path\nimport os\n\n# Example 1: Creating paths\nprint(\"=== Creating Paths ===\")\n\n# Current directory\ncurrent = Path.cwd()  # cwd = current working directory\nprint(f\"Current directory: {current}\")\n\n# Home directory\nhome = Path.home()\nprint(f\"Home directory: {home}\")\n\n# Build path with / operator (cross-platform!)\ndata_file = Path(\u0027data\u0027) / \u0027users.txt\u0027\nprint(f\"Data file path: {data_file}\")\n\n# Multiple levels\nconfig_path = Path(\u0027config\u0027) / \u0027settings\u0027 / \u0027app.json\u0027\nprint(f\"Config path: {config_path}\\n\")\n\n# Example 2: Path information\nprint(\"=== Path Information ===\")\n\npath = Path(\u0027projects/myapp/src/main.py\u0027)\n\nprint(f\"Full path: {path}\")\nprint(f\"Name: {path.name}\")  # main.py\nprint(f\"Stem: {path.stem}\")  # main (without extension)\nprint(f\"Suffix: {path.suffix}\")  # .py\nprint(f\"Parent: {path.parent}\")  # projects/myapp/src\nprint(f\"Parents[0]: {path.parents[0]}\")  # immediate parent\nprint(f\"Parents[1]: {path.parents[1]}\")  # grandparent\nprint(f\"Parts: {path.parts}\\n\")  # (\u0027projects\u0027, \u0027myapp\u0027, \u0027src\u0027, \u0027main.py\u0027)\n\n# Example 3: Checking existence\nprint(\"=== Checking Existence ===\")\n\n# Create a test file\ntest_file = Path(\u0027test.txt\u0027)\ntest_file.write_text(\u0027Hello, World!\u0027)\n\nprint(f\"test.txt exists: {test_file.exists()}\")\nprint(f\"test.txt is file: {test_file.is_file()}\")\nprint(f\"test.txt is directory: {test_file.is_dir()}\")\n\n# Check non-existent\nfake = Path(\u0027nonexistent.txt\u0027)\nprint(f\"nonexistent.txt exists: {fake.exists()}\\n\")\n\n# Example 4: Creating directories\nprint(\"=== Creating Directories ===\")\n\n# Create single directory\nPath(\u0027output\u0027).mkdir(exist_ok=True)\nprint(\"✓ Created \u0027output\u0027 directory\")\n\n# Create nested directories\nPath(\u0027data/processed/2024\u0027).mkdir(parents=True, exist_ok=True)\nprint(\"✓ Created nested \u0027data/processed/2024\u0027 directories\\n\")\n\n# Example 5: Listing directory contents\nprint(\"=== Listing Directory Contents ===\")\n\n# List all items in current directory\nprint(\"Files in current directory:\")\nfor item in Path(\u0027.\u0027).iterdir():\n    if item.is_file():\n        print(f\"  📄 {item.name}\")\n\nprint(\"\\nDirectories in current directory:\")\nfor item in Path(\u0027.\u0027).iterdir():\n    if item.is_dir():\n        print(f\"  📁 {item.name}\")\n\n# Example 6: Glob patterns (finding files)\nprint(\"\\n=== Finding Files with Glob ===\")\n\n# Create some test files\nfor i in range(3):\n    (Path(\u0027output\u0027) / f\u0027file{i}.txt\u0027).write_text(f\u0027Content {i}\u0027)\n    (Path(\u0027output\u0027) / f\u0027data{i}.json\u0027).write_text(\u0027{}\u0027)\n\nprint(\"Created test files in output/\")\n\n# Find all .txt files\nprint(\"\\nAll .txt files in output/:\")\nfor file in Path(\u0027output\u0027).glob(\u0027*.txt\u0027):\n    print(f\"  - {file.name}\")\n\n# Find all .json files\nprint(\"\\nAll .json files in output/:\")\nfor file in Path(\u0027output\u0027).glob(\u0027*.json\u0027):\n    print(f\"  - {file.name}\")\n\n# Recursive glob (search subdirectories too)\nprint(\"\\nAll .txt files (recursive):\")\nfor file in Path(\u0027.\u0027).rglob(\u0027*.txt\u0027):\n    print(f\"  - {file}\")\n\nprint(\"\")\n\n# Example 7: Reading and writing with Path\nprint(\"=== Reading/Writing with Path ===\")\n\ndata_path = Path(\u0027data.txt\u0027)\n\n# Write text\ndata_path.write_text(\u0027Line 1\\nLine 2\\nLine 3\\n\u0027)\nprint(\"✓ Wrote data.txt\")\n\n# Read text\ncontent = data_path.read_text()\nprint(\"\\nContent:\")\nprint(content)\n\n# Read lines\nlines = data_path.read_text().splitlines()\nprint(f\"Number of lines: {len(lines)}\\n\")\n\n# Example 8: Absolute vs Relative paths\nprint(\"=== Absolute vs Relative Paths ===\")\n\nrelative = Path(\u0027data/file.txt\u0027)\nprint(f\"Relative: {relative}\")\nprint(f\"Absolute: {relative.resolve()}\")\nprint(f\"Is absolute: {relative.is_absolute()}\")\n\nabsolute = Path.cwd() / \u0027data\u0027 / \u0027file.txt\u0027\nprint(f\"\\nAbsolute: {absolute}\")\nprint(f\"Is absolute: {absolute.is_absolute()}\\n\")\n\n# Example 9: Joining paths safely\nprint(\"=== Joining Paths (Cross-Platform) ===\")\n\nbase = Path(\u0027projects\u0027)\nsubdir = \u0027myapp\u0027\nfilename = \u0027config.json\u0027\n\n# Method 1: / operator\npath1 = base / subdir / filename\nprint(f\"Using /: {path1}\")\n\n# Method 2: joinpath\npath2 = base.joinpath(subdir, filename)\nprint(f\"Using joinpath: {path2}\")\n\nprint(\"\\n✓ Both create correct path for your OS!\")\nprint(\"(Forward slashes on Unix, backslashes on Windows)\")",
                                "language":  "python"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "Syntax Breakdown: pathlib Operations",
                                "content":  "**Import pathlib:**\n```python\nfrom pathlib import Path\n```\n\n**Creating paths:**\n```python\n# Current directory\nPath.cwd()\n\n# Home directory\nPath.home()\n\n# From string\nPath(\u0027folder/file.txt\u0027)\nPath(\u0027/absolute/path/file.txt\u0027)\n\n# Join paths (cross-platform!)\nPath(\u0027folder\u0027) / \u0027subfolder\u0027 / \u0027file.txt\u0027\n```\n\n**Path information:**\n```python\npath = Path(\u0027projects/myapp/src/main.py\u0027)\n\npath.name      # \u0027main.py\u0027 (filename)\npath.stem      # \u0027main\u0027 (filename without extension)\npath.suffix    # \u0027.py\u0027 (extension)\npath.parent    # Path(\u0027projects/myapp/src\u0027)\npath.parts     # (\u0027projects\u0027, \u0027myapp\u0027, \u0027src\u0027, \u0027main.py\u0027)\n```\n\n**Checking existence:**\n```python\npath.exists()   # True if exists\npath.is_file()  # True if file\npath.is_dir()   # True if directory\npath.is_absolute()  # True if absolute path\n```\n\n**Creating directories:**\n```python\n# Create directory\nPath(\u0027output\u0027).mkdir()\n\n# Create with parents\nPath(\u0027data/processed/2024\u0027).mkdir(parents=True)\n\n# Don\u0027t error if exists\nPath(\u0027output\u0027).mkdir(exist_ok=True)\n\n# Both options\nPath(\u0027data/logs\u0027).mkdir(parents=True, exist_ok=True)\n```\n\n**Listing directory:**\n```python\n# List all items\nfor item in Path(\u0027.\u0027).iterdir():\n    print(item)\n\n# List only files\nfor item in Path(\u0027.\u0027).iterdir():\n    if item.is_file():\n        print(item)\n\n# List only directories\nfor item in Path(\u0027.\u0027).iterdir():\n    if item.is_dir():\n        print(item)\n```\n\n**Finding files (glob):**\n```python\n# All .txt files in directory\nfor file in Path(\u0027folder\u0027).glob(\u0027*.txt\u0027):\n    print(file)\n\n# All .py files (recursive)\nfor file in Path(\u0027.\u0027).rglob(\u0027*.py\u0027):\n    print(file)\n\n# Specific pattern\nfor file in Path(\u0027.\u0027).glob(\u0027data_*.csv\u0027):\n    print(file)\n```\n\n**Reading/Writing:**\n```python\npath = Path(\u0027file.txt\u0027)\n\n# Write text\npath.write_text(\u0027Hello, World!\u0027)\n\n# Read text\ncontent = path.read_text()\n\n# Write bytes\npath.write_bytes(b\u0027\\x00\\x01\\x02\u0027)\n\n# Read bytes\ndata = path.read_bytes()\n```\n\n**Path conversions:**\n```python\n# Relative to absolute\nrelative = Path(\u0027data/file.txt\u0027)\nabsolute = relative.resolve()\n\n# Path to string\npath_str = str(path)\n```\n\n**Deleting:**\n```python\n# Delete file\nPath(\u0027file.txt\u0027).unlink()\n\n# Delete empty directory\nPath(\u0027folder\u0027).rmdir()\n\n# Delete directory with contents (need shutil)\nimport shutil\nshutil.rmtree(\u0027folder\u0027)\n```\n\n**Common patterns:**\n\n**1. Process all files in directory:**\n```python\nfor file in Path(\u0027data\u0027).glob(\u0027*.csv\u0027):\n    # Process each CSV file\n    content = file.read_text()\n    process(content)\n```\n\n**2. Create directory structure:**\n```python\nbase = Path(\u0027project\u0027)\n(base / \u0027src\u0027).mkdir(parents=True, exist_ok=True)\n(base / \u0027tests\u0027).mkdir(exist_ok=True)\n(base / \u0027data\u0027).mkdir(exist_ok=True)\n```\n\n**3. Safe file operations:**\n```python\npath = Path(\u0027data.txt\u0027)\n\nif path.exists():\n    content = path.read_text()\nelse:\n    print(\u0027File not found\u0027)\n```\n\n**Why pathlib is better:**\n\n❌ **Old way (os module):**\n```python\nimport os\npath = os.path.join(\u0027folder\u0027, \u0027file.txt\u0027)\nif os.path.exists(path):\n    with open(path) as f:\n        content = f.read()\n```\n\n✅ **New way (pathlib):**\n```python\nfrom pathlib import Path\npath = Path(\u0027folder\u0027) / \u0027file.txt\u0027\nif path.exists():\n    content = path.read_text()\n```"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "Key Takeaways",
                                "content":  "- **from pathlib import Path** - Modern, cross-platform way to work with file paths. Replaces old os.path module.\n- **Path() / \u0027file\u0027** - Join paths with / operator. Works on Windows, Mac, Linux automatically. Better than string concatenation.\n- **Path.cwd()** - Current working directory. **Path.home()** - User\u0027s home directory. Starting points for paths.\n- **.mkdir(parents=True, exist_ok=True)** - Safely create directories. parents=True creates parent dirs, exist_ok=True doesn\u0027t error if exists.\n- **.glob(pattern)** - Find files matching pattern. **.rglob(pattern)** - Recursive glob (search subdirectories too).\n- **.exists(), .is_file(), .is_dir()** - Check if path exists and what type it is. Always check before operations.\n- **.name, .stem, .suffix, .parent** - Get path components. name=filename, stem=filename without extension, suffix=extension, parent=parent directory.\n- **.read_text() / .write_text()** - Quick file I/O. For simple text files, easier than open().\n- **.iterdir()** - List directory contents. Returns Path objects for each item. Use .is_file() or .is_dir() to filter.\n- **Cross-platform:** pathlib handles path differences between OS automatically. Write once, works everywhere!"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "09_05-challenge-3",
                           "title":  "Interactive Exercise: Project File Organizer",
                           "description":  "Create a file organizer that:\n1. Creates a project directory structure\n2. Finds all files of specific types\n3. Organizes files into folders by type\n4. Lists all files in the organized structure\n\n**Project structure to create:**\n```\nproject/\n  ├── images/\n  ├── documents/\n  ├── code/\n  └── data/\n```\n\n**Your task:**\nImplement the functions below.\n\n**Starter code:**",
                           "instructions":  "Create a file organizer that:\n1. Creates a project directory structure\n2. Finds all files of specific types\n3. Organizes files into folders by type\n4. Lists all files in the organized structure\n\n**Project structure to create:**\n```\nproject/\n  ├── images/\n  ├── documents/\n  ├── code/\n  └── data/\n```\n\n**Your task:**\nImplement the functions below.\n\n**Starter code:**",
                           "starterCode":  "from pathlib import Path\n\ndef create_project_structure(base_path):\n    \"\"\"Create project directory structure.\n    \n    Args:\n        base_path: Base directory path (string or Path)\n    \"\"\"\n    # TODO: Convert to Path object\n    # TODO: Create base directory\n    # TODO: Create subdirectories: images, documents, code, data\n    # Use mkdir(parents=True, exist_ok=True)\n    pass\n\ndef find_files_by_extension(directory, extension):\n    \"\"\"Find all files with specific extension.\n    \n    Args:\n        directory: Directory to search\n        extension: File extension (e.g., \u0027.txt\u0027, \u0027.py\u0027)\n        \n    Returns:\n        list: List of Path objects\n    \"\"\"\n    # TODO: Convert directory to Path\n    # TODO: Use glob to find files with extension\n    # TODO: Return list of matching files\n    pass\n\ndef organize_files(source_dir, dest_dir):\n    \"\"\"Organize files by type into folders.\n    \n    Args:\n        source_dir: Source directory with mixed files\n        dest_dir: Destination directory (project structure)\n    \"\"\"\n    # TODO: Define file type mappings\n    # Images: .jpg, .png, .gif → images/\n    # Documents: .txt, .pdf, .doc → documents/\n    # Code: .py, .js, .html → code/\n    # Data: .csv, .json, .xml → data/\n    \n    # TODO: For each file in source_dir:\n    #   - Get file extension\n    #   - Determine destination folder\n    #   - Copy or move file to destination\n    pass\n\ndef list_project_files(project_dir):\n    \"\"\"List all files in project structure.\n    \n    Args:\n        project_dir: Project directory path\n    \"\"\"\n    # TODO: For each subdirectory:\n    #   - Print directory name\n    #   - List files in that directory\n    pass\n\n# Test your functions\nprint(\"=== File Organizer ===\")\n\nprint(\"\\n1. Creating project structure...\")\ncreate_project_structure(\u0027my_project\u0027)\n\nprint(\"\\n2. Creating test files...\")\n# Create some test files\ntest_files = [\n    \u0027photo1.jpg\u0027, \u0027photo2.png\u0027,\n    \u0027document.txt\u0027, \u0027report.pdf\u0027,\n    \u0027script.py\u0027, \u0027app.js\u0027,\n    \u0027data.csv\u0027, \u0027config.json\u0027\n]\n\nfor filename in test_files:\n    Path(filename).write_text(f\u0027Content of {filename}\u0027)\nprint(f\"✓ Created {len(test_files)} test files\")\n\nprint(\"\\n3. Finding .jpg files...\")\njpg_files = find_files_by_extension(\u0027.\u0027, \u0027.jpg\u0027)\nfor f in jpg_files:\n    print(f\"  - {f.name}\")\n\nprint(\"\\n4. Organizing files...\")\norganize_files(\u0027.\u0027, \u0027my_project\u0027)\n\nprint(\"\\n5. Project structure:\")\nlist_project_files(\u0027my_project\u0027)",
                           "solution":  "from pathlib import Path\nimport shutil\n\n# Project File Organizer\n# This solution demonstrates pathlib for file system operations\n\ndef create_project_structure(base_path):\n    \"\"\"Create project directory structure.\"\"\"\n    # Step 1: Convert to Path object\n    base = Path(base_path)\n    \n    # Step 2: Define subdirectories\n    subdirs = [\u0027images\u0027, \u0027documents\u0027, \u0027code\u0027, \u0027data\u0027]\n    \n    # Step 3: Create base and all subdirectories\n    for subdir in subdirs:\n        (base / subdir).mkdir(parents=True, exist_ok=True)\n    \n    print(f\"Created project structure at \u0027{base_path}\u0027\")\n\ndef find_files_by_extension(directory, extension):\n    \"\"\"Find all files with specific extension.\"\"\"\n    # Convert to Path and use glob\n    path = Path(directory)\n    \n    # Make sure extension starts with dot\n    if not extension.startswith(\u0027.\u0027):\n        extension = \u0027.\u0027 + extension\n    \n    # Use glob to find matching files\n    return list(path.glob(\u0027*\u0027 + extension))\n\ndef organize_files(source_dir, dest_dir):\n    \"\"\"Organize files by type into folders.\"\"\"\n    # Step 1: Define file type mappings\n    type_mappings = {\n        \u0027images\u0027: [\u0027.jpg\u0027, \u0027.png\u0027, \u0027.gif\u0027, \u0027.jpeg\u0027, \u0027.bmp\u0027],\n        \u0027documents\u0027: [\u0027.txt\u0027, \u0027.pdf\u0027, \u0027.doc\u0027, \u0027.docx\u0027, \u0027.md\u0027],\n        \u0027code\u0027: [\u0027.py\u0027, \u0027.js\u0027, \u0027.html\u0027, \u0027.css\u0027, \u0027.java\u0027],\n        \u0027data\u0027: [\u0027.csv\u0027, \u0027.json\u0027, \u0027.xml\u0027, \u0027.yaml\u0027, \u0027.xlsx\u0027]\n    }\n    \n    source = Path(source_dir)\n    dest = Path(dest_dir)\n    \n    # Step 2: Process each file in source directory\n    for file_path in source.iterdir():\n        if file_path.is_file():\n            ext = file_path.suffix.lower()\n            \n            # Find matching folder\n            for folder, extensions in type_mappings.items():\n                if ext in extensions:\n                    # Copy file to appropriate folder\n                    dest_path = dest / folder / file_path.name\n                    shutil.copy2(file_path, dest_path)\n                    print(f\"  Copied {file_path.name} to {folder}/\")\n                    break\n\ndef list_project_files(project_dir):\n    \"\"\"List all files in project structure.\"\"\"\n    project = Path(project_dir)\n    \n    for subdir in sorted(project.iterdir()):\n        if subdir.is_dir():\n            print(f\"\\n{subdir.name}/\")\n            files = list(subdir.iterdir())\n            if files:\n                for file in files:\n                    print(f\"  - {file.name}\")\n            else:\n                print(\"  (empty)\")\n\n# Test the functions\nprint(\"=== File Organizer ===\")\n\nprint(\"\\n1. Creating project structure...\")\ncreate_project_structure(\u0027my_project\u0027)\n\nprint(\"\\n2. Creating test files...\")\ntest_files = [\n    \u0027photo1.jpg\u0027, \u0027photo2.png\u0027,\n    \u0027document.txt\u0027, \u0027report.pdf\u0027,\n    \u0027script.py\u0027, \u0027app.js\u0027,\n    \u0027data.csv\u0027, \u0027config.json\u0027\n]\n\nfor filename in test_files:\n    Path(filename).write_text(f\u0027Content of {filename}\u0027)\nprint(f\"Created {len(test_files)} test files\")\n\nprint(\"\\n3. Finding .jpg files...\")\njpg_files = find_files_by_extension(\u0027.\u0027, \u0027.jpg\u0027)\nfor f in jpg_files:\n    print(f\"  - {f.name}\")\n\nprint(\"\\n4. Organizing files...\")\norganize_files(\u0027.\u0027, \u0027my_project\u0027)\n\nprint(\"\\n5. Project structure:\")\nlist_project_files(\u0027my_project\u0027)",
                           "language":  "python",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Code runs without errors",
                                                 "expectedOutput":  "",
                                                 "isVisible":  true
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "Use Path(dir).mkdir(parents=True, exist_ok=True) to create directories. Use Path(dir).glob(\u0027*\u0027 + extension) to find files. Use file.rename() or shutil.copy() to move files."
                                         }
                                     ],
                           "commonMistakes":  [
                                                  {
                                                      "mistake":  "Forgetting the colon after if/for/while",
                                                      "consequence":  "SyntaxError",
                                                      "correction":  "Add : at the end of the line"
                                                  },
                                                  {
                                                      "mistake":  "Using = instead of == for comparison",
                                                      "consequence":  "Assignment instead of comparison",
                                                      "correction":  "Use == for equality checks"
                                                  },
                                                  {
                                                      "mistake":  "Incorrect indentation",
                                                      "consequence":  "IndentationError",
                                                      "correction":  "Use consistent 4-space indentation"
                                                  }
                                              ],
                           "difficulty":  "beginner"
                       }
                   ],
    "difficulty":  "intermediate",
    "title":  "File Paths and Directory Operations",
    "estimatedMinutes":  30
}

## Review Instructions

Perform the following analysis:

### 1. Accuracy Check
- Verify all code examples are syntactically correct
- Confirm technical explanations match current python documentation
- Search the web for the latest python version and verify examples work with it
- Flag any deprecated APIs, syntax, or patterns

### 2. Completeness Check
- Does the lesson cover all concepts needed for a beginner to understand this topic?
- Are there missing explanations between concepts that would confuse a learner?
- Does the lesson have:
  - [ ] A clear analogy or real-world example (ANALOGY section)
  - [ ] Theoretical explanation (THEORY section)
  - [ ] Working code example (EXAMPLE section)
  - [ ] Common mistakes to avoid (WARNING section)
  - [ ] At least one practice challenge

### 3. Freshness Check
- Search for "python File Paths and Directory Operations 2024 2025" to find latest practices
- Compare lesson content against current best practices
- Identify any outdated patterns or recommendations

### 4. Pedagogical Gap Analysis
- What prerequisite knowledge is assumed but not explained?
- What follow-up questions would a learner likely have?
- What practical applications or use cases are missing?
- Are the challenges appropriately scaffolded for the difficulty level?

## Output Format

Provide your review as structured JSON:

```json
{
  "lessonId": "09_05",
  "reviewDate": "YYYY-MM-DD",
  "overallScore": 1-10,
  "accuracy": {
    "score": 1-10,
    "issues": ["issue 1", "issue 2"],
    "recommendations": ["fix 1", "fix 2"]
  },
  "completeness": {
    "score": 1-10,
    "missingSections": ["section type needed"],
    "gaps": ["gap 1", "gap 2"],
    "recommendations": ["add X", "expand Y"]
  },
  "freshness": {
    "score": 1-10,
    "outdatedItems": ["item 1"],
    "currentVersion": "language version checked",
    "recommendations": ["update X to Y"]
  },
  "pedagogicalGaps": {
    "score": 1-10,
    "missingPrerequisites": ["concept 1"],
    "unansweredQuestions": ["question learner would have"],
    "missingUseCases": ["practical application"],
    "recommendations": ["add section on X"]
  },
  "contentLengthIssues": {
    "shortSections": [
      {"sectionTitle": "title", "currentLength": 42, "recommendation": "expand to explain X"}
    ]
  },
  "suggestedNewContent": [
    {
      "sectionType": "THEORY|EXAMPLE|WARNING|etc",
      "title": "suggested title",
      "contentOutline": "what this section should cover"
    }
  ],
  "priority": "HIGH|MEDIUM|LOW"
}
```

