# Course Import Quick Start Guide

This guide helps you quickly import course content into Code Tutor.

## Prerequisites

- Node.js 18+ installed
- Course content ready in one of the supported formats
- Write access to the repository

## Supported Languages

- Python
- Java
- JavaScript/TypeScript
- Kotlin
- Rust
- C# (csharp)
- Flutter/Dart

---

## Method 1: Import from Existing course.json (Fastest)

If your course already has a `course.json` file with `bodyFile` references:

```bash
# Process a single course
npx ts-node scripts/process-courses.ts

# This will:
# 1. Read content/courses/<language>/course.json
# 2. Embed all markdown files from bodyFile references
# 3. Save to apps/api/content/<language>.json
```

**Current Status:** Python course is already imported this way!

---

## Method 2: Import from Markdown Directory

If you have a directory of markdown lesson files:

```bash
# Import a course from markdown
npx ts-node scripts/import-cli.ts \
  --source /path/to/markdown/lessons \
  --language python \
  --format markdown \
  --validate

# Example directory structure:
# lessons/
# ├── module-1/
# │   ├── lesson-1.md
# │   ├── lesson-2.md
# ├── module-2/
# │   ├── lesson-1.md
```

---

## Method 3: Bulk Import All Courses

Edit `scripts/import-all.sh` and set the paths:

```bash
#!/bin/bash

# Configuration - UPDATE THESE PATHS
PYTHON_SOURCE="/path/to/python/course"
JAVA_SOURCE="/path/to/java/course"
KOTLIN_SOURCE="/path/to/kotlin/course"
RUST_SOURCE="/path/to/rust/course"
CSHARP_SOURCE="/path/to/csharp/course"
FLUTTER_SOURCE="/path/to/flutter/course"
JS_TS_SOURCE="/path/to/javascript-typescript/course"

# Then run:
chmod +x scripts/import-all.sh
./scripts/import-all.sh
```

---

## Markdown Format

### Basic Lesson (Minimal)

```markdown
# Lesson Title

Lesson content goes here. You can use all standard markdown features.

## Code Examples

\`\`\`python
print("Hello, World!")
\`\`\`

## Key Takeaways

- Point 1
- Point 2
```

### Lesson with Frontmatter (Recommended)

```markdown
---
title: "Variables and Data Types"
description: "Learn about Python variables and different data types"
type: "interactive"
difficulty: "beginner"
estimatedMinutes: 30
tags: ["variables", "data-types", "basics"]
keyTakeaways:
  - "Variables store data"
  - "Python has multiple data types"
  - "Type conversion is important"
---

# Variables and Data Types

Content here...
```

### Lesson with Exercises

```markdown
# Your Lesson Title

Lesson content...

<!-- EXERCISE
{
  "id": "exercise-01",
  "type": "coding",
  "title": "Create Variables",
  "instructions": "Create three variables: name, age, and city. Then print them all.",
  "difficulty": "beginner",
  "estimatedMinutes": 10,
  "starterCode": "# Write your code here\n",
  "solution": "name = 'Alice'\nage = 25\ncity = 'NYC'\nprint(name, age, city)",
  "hints": [
    "Use the = operator to assign values",
    "Strings need quotes, numbers don't"
  ]
}
-->
```

---

## Verifying Imports

After importing, verify the course was created:

```bash
# Check if JSON was created
ls -lh apps/api/content/

# View the first few lines
head -100 apps/api/content/python.json

# Validate structure
npx ts-node scripts/import-cli.ts --source apps/api/content/python.json --validate
```

---

## Testing in the App

1. Start the API server:
```bash
cd apps/api
npm run dev
```

2. Start the web app:
```bash
cd apps/web
npm run dev
```

3. Open http://localhost:5173 and navigate to the course

---

## Common Issues

### "No course.json found"
- Make sure you're running from the project root
- Check that the course directory exists in `content/courses/<language>/`

### "Could not read markdown file"
- Verify the `bodyFile` paths in course.json
- Ensure markdown files exist at the specified paths

### "Invalid course structure"
- Run with `--validate` flag to see specific errors
- Check that all required fields are present

---

## Directory Structure

Your content should be organized like this:

```
content/courses/
└── python/
    ├── course.json              # Course metadata
    └── modules/
        └── module-00/
            └── lessons/
                ├── lesson-00-01.md
                ├── lesson-00-02.md
                └── lesson-00-03.md
```

After import, processed courses go here:

```
apps/api/content/
├── python.json           # Ready to serve
├── java.json
├── kotlin.json
└── ...
```

---

## Next Steps

1. **Add Your Content**: Place course content in `content/courses/<language>/`

2. **Import**: Run the appropriate import command

3. **Verify**: Check `apps/api/content/` for the output JSON

4. **Test**: Start the dev servers and test in the browser

5. **Commit**: Add and commit the imported JSON files

---

## Need Help?

- Full documentation: `docs/CONTENT_IMPORT.md`
- Developer guide: `docs/DEVELOPER_GUIDE.md`
- Create an issue on GitHub
