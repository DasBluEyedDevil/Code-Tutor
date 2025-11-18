# Quick Start Guide - Importing All Courses

## What's Been Done ✅

1. **Fixed TypeScript Import Script** - `scripts/import-content.ts` now compiles without errors
2. **Cloned All 7 Repositories** - Located in `temp/course-repos/`
3. **Created Import Automation** - PowerShell scripts for bulk and individual imports
4. **Installed Dependencies** - ts-node, typescript, and @types/node

## To Complete the Import (3 Simple Steps)

### Step 1: Run the Bulk Import

Open PowerShell in the project root and run:

```powershell
cd C:\Users\dasbl\WebstormProjects\Code-Tutor
.\scripts\import-all-v2.ps1 -SkipClone
```

This will:
- Import all 7 courses sequentially
- Generate JSON files in `apps/api/content/`
- Validate each course structure
- Show progress for each language

**Expected Output:**
```
================================================
  Code Tutor - Bulk Content Import
================================================

Starting course imports...

----------------------------------------
Processing: python
Repository: https://github.com/DasBluEyedDevil/Python-Training-Course
[INFO] Importing course content...
[SUCCESS] Successfully imported python

----------------------------------------
Processing: java
...
```

### Step 2: Verify the Generated Files

Check that JSON files were created:

```powershell
ls apps/api/content/
```

You should see:
- `python.json`
- `java.json`
- `kotlin.json`
- `rust.json`
- `csharp.json`
- `flutter.json`
- `javascript.json`

### Step 3: Test One Course

To verify a single course works:

```powershell
.\scripts\test-import.ps1 -Language python
```

## Alternative: Import One at a Time

If you prefer to import courses individually:

```powershell
# Python
npx ts-node scripts/import-cli.ts --source temp/course-repos/python --language python --format markdown --validate

# Java
npx ts-node scripts/import-cli.ts --source temp/course-repos/java --language java --format markdown --validate

# Kotlin
npx ts-node scripts/import-cli.ts --source temp/course-repos/kotlin --language kotlin --format markdown --validate

# Rust
npx ts-node scripts/import-cli.ts --source temp/course-repos/rust --language rust --format markdown --validate

# C#
npx ts-node scripts/import-cli.ts --source temp/course-repos/csharp --language csharp --format markdown --validate

# Flutter
npx ts-node scripts/import-cli.ts --source temp/course-repos/flutter --language flutter --format markdown --validate

# JavaScript
npx ts-node scripts/import-cli.ts --source temp/course-repos/javascript --language javascript --format markdown --validate
```

## What Happens During Import

The import script will:

1. **Scan** - Find all `.md` files in the repository
2. **Parse** - Extract frontmatter and content from each markdown file
3. **Group** - Organize lessons into modules based on directory structure
4. **Extract** - Pull out code examples from code blocks
5. **Process** - Parse exercise definitions
6. **Generate** - Create a complete Course JSON object
7. **Validate** - Check that all required properties exist
8. **Save** - Write to `apps/api/content/{language}.json`

## Expected Structure in JSON

Each generated JSON file will contain:

```json
{
  "courseMetadata": {
    "id": "python",
    "language": "python",
    "displayName": "Python",
    "description": "Complete python course",
    "totalModules": 10,
    "estimatedHours": 25,
    "difficulty": "beginner-to-advanced",
    "prerequisites": [],
    "learningOutcomes": [...],
    "icon": "📚",
    "color": "#3B82F6"
  },
  "languageConfig": {
    "executionEngine": "python",
    "compilerOptions": {...},
    "editorSettings": {...},
    "sandboxConstraints": {...}
  },
  "modules": [
    {
      "id": "module-1",
      "title": "Introduction",
      "description": "...",
      "order": 0,
      "estimatedHours": 2,
      "prerequisites": [],
      "lessons": [...]
    }
  ]
}
```

## Troubleshooting

### Import Fails with "Cannot find module"
Make sure you're in the project root:
```powershell
cd C:\Users\dasbl\WebstormProjects\Code-Tutor
```

### No Markdown Files Found
Check that repositories are cloned:
```powershell
ls temp/course-repos/
```

If empty, re-run without `-SkipClone`:
```powershell
.\scripts\import-all-v2.ps1
```

### TypeScript Compilation Errors
The import script should now be error-free. If you see errors, check:
```powershell
npx tsc --noEmit scripts/import-content.ts
```

### Validation Fails
The import may still succeed even if validation shows warnings. Check the generated JSON file to see if it looks correct.

## After Import

Once all courses are imported:

1. **Start the API server:**
   ```powershell
   cd apps/api
   npm run dev
   ```

2. **Start the web app:**
   ```powershell
   cd apps/web
   npm run dev
   ```

3. **Open your browser:**
   ```
   http://localhost:3000
   ```

4. **Select a language** and start learning!

## Files Reference

| File | Purpose |
|------|---------|
| `scripts/import-all-v2.ps1` | Bulk import all 7 courses |
| `scripts/test-import.ps1` | Test single course import |
| `scripts/import-content.ts` | Core import logic (TypeScript) |
| `scripts/import-cli.ts` | CLI wrapper |
| `apps/api/content/*.json` | Generated course files |
| `temp/course-repos/*` | Cloned source repositories |

## Summary

Everything is ready! Just run:

```powershell
.\scripts\import-all-v2.ps1 -SkipClone
```

And all 7 courses will be imported into the platform. 🚀

---

**Need Help?** Check `IMPORT_STATUS.md` for detailed status and troubleshooting.

