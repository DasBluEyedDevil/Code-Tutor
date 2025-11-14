# Content Import Guide

This guide explains how to import your existing course content into Code Tutor.

## Quick Start

### Import a Single Course

```bash
# Navigate to project root
cd /home/user/Code-Tutor

# Install dependencies if you haven't
npm install

# Import a course (example with Python)
npx ts-node scripts/import-cli.ts \
  --source /path/to/your/python-course \
  --language python \
  --format markdown \
  --validate
```

### Import All Courses at Once

1. Edit `scripts/import-all.sh` and set your course paths:
```bash
PYTHON_SOURCE="/path/to/Python-Course"
JAVA_SOURCE="/path/to/Java-Course"
KOTLIN_SOURCE="/path/to/Kotlin-Course"
# ... etc
```

2. Run the bulk import:
```bash
chmod +x scripts/import-all.sh
./scripts/import-all.sh
```

---

## Supported Languages

- **python** - Python programming
- **java** - Java programming
- **javascript** - JavaScript programming
- **typescript** - TypeScript programming
- **kotlin** - Kotlin programming
- **rust** - Rust programming
- **csharp** - C# programming
- **flutter** - Flutter/Dart programming

---

## Supported Formats

### Markdown (Recommended)

The importer scans your source directory for `.md` files and automatically:
- Groups files into modules based on directory structure
- Extracts code examples from fenced code blocks
- Parses frontmatter for metadata
- Generates exercises from special markers

#### Directory Structure

Any structure works! Files are grouped by their parent directory:

```
python-course/
├── 01-basics/                    # Becomes "Basics" module
│   ├── 01-hello-world.md
│   ├── 02-variables.md
│   └── 03-data-types.md
├── 02-control-flow/              # Becomes "Control Flow" module
│   ├── 01-if-statements.md
│   ├── 02-loops.md
│   └── 03-functions.md
├── 03-advanced/                  # Becomes "Advanced" module
│   ├── 01-classes.md
│   └── 02-modules.md
└── README.md                     # Ignored or used for course intro
```

#### Markdown File Format

**With Frontmatter (Recommended):**

```markdown
---
title: Hello World
description: Your first Python program
type: lesson
difficulty: beginner
estimatedMinutes: 10
keyTakeaways: print function, strings, basic syntax
---

# Hello World

Welcome to Python! In this lesson, you'll write your first program.

## The Print Function

The `print()` function displays text to the console:

\`\`\`python
print("Hello, World!")
\`\`\`

Try it yourself!

## Exercise

<!-- EXERCISE
{
  "title": "Print Your Name",
  "instructions": "Modify the code to print your name instead of 'Hello, World!'",
  "starterCode": "print('Hello, World!')",
  "solution": "print('Your Name')",
  "hints": [
    "Replace 'Hello, World!' with your name",
    "Keep the quotes around the text"
  ],
  "testCases": []
}
-->

Try printing different messages!
```

**Without Frontmatter (Simple):**

```markdown
# Variables in Python

Variables store data that can be used later in your program.

\`\`\`python
name = "Alice"
age = 25
print(f"My name is {name} and I'm {age} years old")
\`\`\`

Variables can hold different types of data:
- Strings (text)
- Numbers (integers and floats)
- Booleans (True/False)
```

#### Frontmatter Options

| Field | Type | Description | Default |
|-------|------|-------------|---------|
| `title` | string | Lesson title | Extracted from `# heading` or filename |
| `description` | string | Short description | Generated from title |
| `type` | lesson\|tutorial\|challenge | Lesson type | lesson |
| `difficulty` | beginner\|intermediate\|advanced | Difficulty level | beginner |
| `estimatedMinutes` | number | Time to complete | 15 |
| `keyTakeaways` | comma-separated | Key concepts | Empty array |

#### Exercise Markers

Add exercises anywhere in your markdown with HTML comments:

```markdown
<!-- EXERCISE
{
  "title": "Exercise Title",
  "instructions": "What the student should do",
  "starterCode": "// Initial code here",
  "solution": "// Solution code",
  "hints": ["Hint 1", "Hint 2"],
  "testCases": [
    {
      "input": "test input",
      "expectedOutput": "expected result",
      "description": "Test description"
    }
  ]
}
-->
```

### JSON Format (Coming Soon)

Direct JSON import for courses already in Code Tutor format:

```bash
npx ts-node scripts/import-cli.ts \
  --source ./my-course.json \
  --language python \
  --format json
```

### YAML Format (Coming Soon)

Import from YAML course definitions.

### Custom Format (Coming Soon)

For unique legacy formats, we can create custom importers.

---

## Output

Courses are saved to: `apps/api/content/<language>.json`

Example output structure:
```json
{
  "courseMetadata": {
    "id": "python",
    "displayName": "Python",
    "description": "Complete Python course",
    "language": "python",
    "version": "1.0.0",
    "author": "Code Tutor",
    "tags": ["python", "programming"]
  },
  "languageConfig": {
    "executor": "python",
    "editorSettings": {
      "defaultTemplate": "print('Hello, World!')",
      "monacoLanguageId": "python",
      "fileExtension": ".py"
    }
  },
  "modules": [
    {
      "id": "module-1",
      "title": "Basics",
      "description": "Basics module",
      "lessons": [ /* ... */ ],
      "order": 0
    }
  ]
}
```

---

## Validation

The `--validate` flag checks for:
- ✅ Required metadata fields
- ✅ Module structure
- ✅ Lesson content
- ✅ Valid IDs
- ✅ Non-empty modules

Example:
```bash
npx ts-node scripts/import-cli.ts \
  --source ./python-course \
  --language python \
  --validate
```

If validation fails, you'll see specific errors:
```
❌ Validation failed:
   - Module 2 missing id
   - Lesson 0 in module module-1 missing content body
```

---

## CLI Reference

```bash
npx ts-node scripts/import-cli.ts [options]
```

### Options

| Option | Alias | Description | Required |
|--------|-------|-------------|----------|
| `--source` | `-s` | Source directory or file | ✅ Yes |
| `--language` | `-l` | Programming language | ✅ Yes |
| `--format` | `-f` | Content format (markdown, json, yaml, custom) | No (default: markdown) |
| `--output` | `-o` | Custom output path | No (default: apps/api/content/<lang>.json) |
| `--validate` | `-v` | Validate after import | No (default: true) |
| `--help` | `-h` | Show help message | No |

### Examples

```bash
# Basic import
npx ts-node scripts/import-cli.ts -s ~/Python-Course -l python

# Custom output location
npx ts-node scripts/import-cli.ts \
  -s ~/Java-Course \
  -l java \
  -o ./custom/java-course.json

# Skip validation
npx ts-node scripts/import-cli.ts \
  -s ~/Rust-Course \
  -l rust \
  --no-validate

# Get help
npx ts-node scripts/import-cli.ts --help
```

---

## Troubleshooting

### "Source directory not found"

- Check that the path is correct
- Use absolute paths: `/home/user/Python-Course` not `~/Python-Course`
- Verify the directory exists: `ls -la /path/to/course`

### "No markdown files found"

- Check that you have `.md` files in the source directory
- Verify file extensions (must be `.md`, not `.markdown` or `.txt`)
- Run: `find /path/to/course -name "*.md"` to list all markdown files

### "Failed to parse exercise"

- Verify JSON syntax in exercise markers
- Use a JSON validator to check your exercise data
- Make sure quotes are properly escaped

### "Module has no lessons"

- Ensure the directory has markdown files
- Check that markdown files are in subdirectories
- Files at the root level may be ignored (except if they're the only files)

### "TypeScript errors"

- Install dependencies: `npm install`
- Compile TypeScript: `npm run build`
- Use `npx ts-node` instead of `node`

---

## Best Practices

### 1. **Organize by Difficulty**

Structure folders from beginner to advanced:
```
course/
├── 01-beginner-basics/
├── 02-beginner-advanced/
├── 03-intermediate/
└── 04-advanced/
```

### 2. **Use Descriptive Filenames**

Good: `01-variables-and-data-types.md`
Bad: `lesson1.md`

### 3. **Include Code Examples**

Every lesson should have at least one code example:
```markdown
\`\`\`python
# Clear, runnable example
x = 10
print(x)
\`\`\`
```

### 4. **Add Exercises**

Include at least one exercise per lesson for hands-on practice.

### 5. **Test Before Bulk Import**

Import one course first to verify the format works:
```bash
npx ts-node scripts/import-cli.ts -s ~/Test-Course -l python -v
```

Then review the output before importing all courses.

### 6. **Keep Backups**

Always keep your original course files. The importer doesn't modify source files.

### 7. **Iterative Refinement**

1. Import
2. Review in the app
3. Adjust source markdown
4. Re-import
5. Repeat until satisfied

---

## Migration from Legacy Formats

If you have courses in other formats, here's how to prepare them:

### From Jupyter Notebooks

1. Export notebooks to markdown: `jupyter nbconvert --to markdown notebook.ipynb`
2. Organize into directory structure
3. Import as markdown

### From Google Docs

1. File → Download → Markdown
2. Clean up formatting
3. Add frontmatter
4. Import as markdown

### From WordPress/Blog Posts

1. Export as markdown (use plugin or manual copy)
2. Extract code blocks
3. Add exercise markers
4. Import as markdown

### From PDF/Word Documents

1. Convert to markdown (use pandoc or manual)
2. Restructure into lessons
3. Add metadata
4. Import as markdown

---

## Getting Help

If you encounter issues:

1. Check this documentation
2. Review example files in `docs/examples/`
3. Run with `--help` flag
4. Check the troubleshooting section above
5. Review generated files in `apps/api/content/`

---

## Next Steps

After importing:

1. **Review Generated Files**: Check `apps/api/content/<language>.json`
2. **Test in App**: Start dev server and navigate to each course
3. **Refine Content**: Make adjustments and re-import if needed
4. **Add Metadata**: Enhance course descriptions, tags, etc.
5. **Create Exercises**: Add more interactive exercises
6. **Test Code Execution**: Verify all code examples run correctly

---

## Example: Complete Import Workflow

```bash
# 1. Navigate to project
cd /home/user/Code-Tutor

# 2. Set up source paths in import-all.sh
nano scripts/import-all.sh
# (Edit PYTHON_SOURCE, JAVA_SOURCE, etc.)

# 3. Make script executable
chmod +x scripts/import-all.sh

# 4. Run bulk import
./scripts/import-all.sh

# 5. Review results
ls -lh apps/api/content/

# 6. Start dev server
npm run dev

# 7. Test in browser
# Open http://localhost:5173
```

Done! Your courses are now imported and ready to use! 🎉
