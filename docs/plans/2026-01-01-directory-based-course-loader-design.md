# Directory-Based Course Loader Design

## Overview

Refactor `CourseService` to load courses from the new directory-based structure instead of monolithic `course.json` files.

## Decisions

- **Clean break**: No backward compatibility with old monolithic format
- **Lazy loading**: Course/module metadata loaded upfront; lesson content loaded on-demand
- **In-memory caching**: Once a lesson's content is loaded, it stays in memory for the session

## Directory Structure

```
courses/{language}/
├── course.json                    # Course metadata only
└── modules/
    └── {NN}-{module-slug}/
        ├── module.json            # Module metadata only
        └── lessons/
            └── {NN}-{lesson-slug}/
                ├── lesson.json    # Lesson metadata only
                ├── content/
                │   ├── 01-theory.md
                │   ├── 02-key_point.md
                │   └── ...
                └── challenges/
                    └── {NN}-{challenge-slug}/
                        ├── challenge.json
                        ├── starter.*
                        └── solution.*
```

## Loading Strategy

### Phase 1: Eager Structure Loading (GetAllCoursesAsync)

```
For each course directory:
├── Load course.json (metadata)
└── Scan modules/ directory
    └── Load each module.json
        └── Scan lessons/ directory
            └── Load each lesson.json (metadata only)
                └── Store lessonId → directory path mapping
                └── Leave ContentSections = [] and Challenges = []
```

### Phase 2: Lazy Content Loading (GetLessonAsync)

```
Find lesson in cache
If ContentSections.Count == 0:
├── Load content/*.md files → parse YAML frontmatter → ContentSections
└── Load challenges/*/ → challenge.json + starter.* + solution.* → Challenges
Return lesson
```

## File Parsing

### Markdown Content Files

```markdown
---
type: "THEORY"
title: "The Problem"
---

Markdown content here...
```

Maps to `ContentSection`:
- `Type` ← frontmatter `type`
- `Title` ← frontmatter `title`
- `Content` ← markdown body (after second `---`)

### Challenge Directories

- `challenge.json` → base Challenge object
- `starter.*` → `StarterCode` field
- `solution.*` → `Solution` field
- File extension → `Language` (fallback if not in JSON)

## Implementation

Single file modification: `native-app-wpf/Services/CourseService.cs`

### New Private Fields

```csharp
private readonly ConcurrentDictionary<string, string> _lessonPaths = new();
```

### New/Modified Methods

| Method | Purpose |
|--------|---------|
| `GetAllCoursesAsync()` | Load course + module metadata, create lesson stubs |
| `LoadModulesAsync(course, courseDir)` | Scan modules/, load module.json files |
| `LoadLessonStubsAsync(module, moduleDir)` | Scan lessons/, load lesson.json, store paths |
| `GetLessonAsync(...)` | Lazy-load content if empty, return lesson |
| `LoadLessonContentAsync(lesson, lessonDir)` | Parse content/*.md and challenges/*/ |
| `ParseMarkdown(content)` | Split YAML frontmatter from body |
| `GetLanguageFromExtension(ext)` | Map file extension to language name |

## Edge Cases

- Missing directories → empty lists (graceful degradation)
- Missing starter/solution files → empty strings
- Multiple starter files → take first alphabetically
- Ordering → alphabetical sort on directory/file names (NN- prefix ensures order)

## Estimated Scope

~150-200 lines of code changes in a single file.
