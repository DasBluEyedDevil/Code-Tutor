# Phase 1: Foundation and Content Normalization - Research

**Researched:** 2026-02-02
**Domain:** Content schema normalization, git hygiene, directory restructuring, version manifesting
**Confidence:** HIGH

## Summary

Phase 1 is a mechanical normalization pass that standardizes 812 lessons across 6 courses before any content editing begins. Research focused on (1) the actual current state of every data structure the planner needs to transform, (2) how the WPF app parses and renders content, (3) which changes are safe vs which break user-facing behavior, and (4) the right tooling for schema validation.

The core challenge is that 6 different AI sessions produced 6 different conventions for every aspect of the content: lesson ID formats, module ID patterns, title formats, directory naming, content section types, metadata fields, and course-level schemas. The app is remarkably tolerant of this inconsistency (it just reads whatever JSON it finds), but the inconsistency multiplies work in every subsequent phase.

**Primary recommendation:** Normalize schemas and IDs in a single atomic pass per course, log every change, and run existing E2E validation tests after each step. The progress.json risk from ID changes is LOW because this is a pre-release product with no deployed users -- but the planner must still define the new ID format precisely before any scripts run.

## Standard Stack

### Core Tools for This Phase

| Tool | Version | Purpose | Why Standard |
|------|---------|---------|--------------|
| PowerShell / Node.js scripts | N/A | Batch JSON manipulation across 812 lessons | Project already has Node.js; PowerShell is native on Windows. Either works for find-and-replace JSON operations |
| JSON Schema (Draft 2020-12) | N/A | Define the canonical lesson.json / module.json / course.json schemas | Industry standard for declarative JSON validation |
| NJsonSchema | 11.x | .NET JSON Schema validation in E2E tests | Free, MIT license, System.Text.Json compatible, no maintenance fee (unlike JsonSchema.Net 8.x which added a paid EULA as of Feb 1, 2026) |
| git filter-repo | latest | Purge binary files from git history (INFR-02 if needed) | The standard tool for history rewriting; replaces deprecated `git filter-branch` |
| BFG Repo-Cleaner | latest | Alternative to git-filter-repo for binary purge | Simpler for "just delete big files" use case |

### Alternatives Considered

| Instead of | Could Use | Tradeoff |
|------------|-----------|----------|
| NJsonSchema | JsonSchema.Net 8.x | JsonSchema.Net added a commercial maintenance fee (EULA) starting Feb 1 2026. NJsonSchema is fully MIT with no fee. |
| NJsonSchema | Manual validation in C# | Works for simple checks but does not scale to 812 files with complex schemas. Schema file is reusable by other tools. |
| git filter-repo | BFG Repo-Cleaner | BFG is simpler but less flexible. filter-repo is the git-project-recommended replacement. Use whichever is easier to install. |
| Node.js scripts | Python scripts | Either works. Node.js is already in the project dependencies (node_modules exists). |

## Architecture Patterns

### How the App Loads Content (Critical for Safe Changes)

The WPF app (`CourseService.cs`) loads content as follows:

1. **Course loading:** Reads `content/courses/{id}/course.json`, deserializes to `Course` model. The `id` field from JSON becomes the cache key.
2. **Module loading:** Reads `content/courses/{id}/modules/*/module.json`, sorts directories alphabetically by name (`OrderBy(d => Path.GetFileName(d))`). The directory sort order determines display order, NOT the `order` field in module.json (the Module model does not even have an `order` field in the WPF app).
3. **Lesson stub loading:** Reads `content/courses/{id}/modules/*/lessons/*/lesson.json`, sorts directories alphabetically. Lesson IDs are cached in `_lessonPaths[lesson.Id] = lessonDir` for lazy loading.
4. **Lesson content loading (lazy):** On first access, reads `content/*/lessons/*/content/*.md` files, sorted alphabetically. Parses YAML frontmatter for `type` and `title`. The filename number prefix controls display order.
5. **Challenge loading:** Reads `content/*/lessons/*/challenges/*/challenge.json`, loads `starter.*` and `solution.*` files.

**Critical implications for renaming:**
- **Module display order** is controlled by directory name sort, NOT by module.json `order` field. Renaming `01-xyz` to `02-xyz` changes display order.
- **Lesson ID** is the key for user progress tracking (`ProgressService` stores `CompletedLessons` as `HashSet<string>` of lesson IDs). Changing IDs invalidates any existing progress.json files.
- **Content section display order** is controlled by the filename number prefix (e.g., `01-theory.md`, `02-example.md`). The type in the filename is only for human readability -- the actual type comes from YAML frontmatter.
- **Content section type** is read from frontmatter and matched case-insensitively (`ToUpperInvariant()`) against a switch statement. Only THEORY, EXAMPLE, KEY_POINT, and LEGACY_COMPARISON have dedicated controls. Everything else falls through to `CreateDefaultSection()`.

### Content Section Type Rendering

```
App switch statement (LessonPage.xaml.cs line 105-112):
  "THEORY"            -> TheorySection control (dedicated)
  "EXAMPLE"           -> CodeExampleSection control (dedicated)
  "KEY_POINT"         -> KeyPointSection control (dedicated)
  "LEGACY_COMPARISON" -> LegacyComparisonSection control (dedicated)
  _                   -> CreateDefaultSection() (generic StackPanel with title + content)
```

WARNING, ANALOGY, and all other types render through the generic default. This means renaming CODE->EXAMPLE or CONCEPT->THEORY changes the rendering from default to dedicated control, which is an improvement but should be verified visually.

### Recommended Project Structure for Schema Files

```
content/
  schemas/
    course.schema.json         # JSON Schema for course.json
    module.schema.json         # JSON Schema for module.json
    lesson.schema.json         # JSON Schema for lesson.json
    challenge.schema.json      # JSON Schema for challenge.json
    version-manifest.json      # Pinned version targets
  courses/
    java/
    python/
    ...
```

### Recommended Version Manifest Location

```
content/version-manifest.json
```

This sits alongside `courses/` and `schemas/` as a peer in the content root. The app does not need to read it (it is for human auditors and CI), so it does not need to be in a location the WPF app discovers.

## Don't Hand-Roll

| Problem | Don't Build | Use Instead | Why |
|---------|-------------|-------------|-----|
| JSON Schema validation | Custom field-by-field C# checks | JSON Schema file + NJsonSchema library | Schema files are reusable, self-documenting, and can be updated without recompiling |
| Batch JSON rewriting | Manual file-by-file editing | Node.js script with `glob` + `JSON.parse`/`JSON.stringify` | 812 lessons cannot be manually edited. Script can be re-run if schema changes. |
| Git history rewriting | Manual commit archaeology | `git filter-repo` or BFG Repo-Cleaner | These tools are purpose-built and handle edge cases (packed refs, reflogs, etc.) |
| Content section type inventory | Manually reading file lists | Script that reads all `*.md` frontmatter `type:` fields | Need exact counts per course to plan migration |
| Numbering gap detection | Visual inspection of directories | Script that compares directory prefixes to sequential integers | Must be 100% reliable across 6 courses |

## Common Pitfalls

### Pitfall 1: Changing Lesson IDs Breaks User Progress

**What goes wrong:** `ProgressService` stores completed lesson IDs in `%LOCALAPPDATA%\CodeTutor\progress.json` as a `HashSet<string>`. If lesson IDs change, all stored progress becomes orphaned.
**Why it happens:** The ID normalization task changes IDs like `epoch-0-lesson-1` to a new format.
**How to avoid:** This is a pre-release product with no deployed users, so progress loss risk is LOW. However, if the developer has personal progress, either (a) create a migration script that maps old IDs to new IDs and patches progress.json, or (b) document that progress.json should be deleted after Phase 1.
**Warning signs:** Any task that changes a `lesson.json` `id` field.

### Pitfall 2: Module Display Order Depends on Directory Name Sort

**What goes wrong:** Renaming module directories changes display order because `CourseService.cs` sorts by `Path.GetFileName(d)`, not by any `order` field.
**Why it happens:** The Module C# model has no `order` property (even though some module.json files have one). Directory name sort IS the order.
**How to avoid:** When renaming directories, always use zero-padded numeric prefixes (e.g., `01-`, `02-`, ..., `22-`) to ensure alphabetical sort equals numeric sort. Verify sort order after every rename.
**Warning signs:** Module directories starting with non-padded numbers (e.g., `1-` vs `10-` would sort incorrectly).

### Pitfall 3: Content Type Rename Changes Rendering

**What goes wrong:** Renaming `CODE` to `EXAMPLE` in frontmatter changes the renderer from `CreateDefaultSection()` to `CodeExampleSection`. The CodeExampleSection control may expect a `code` field that the old `CODE` section does not have.
**Why it happens:** `EXAMPLE` type has a dedicated control that may parse content differently than the default renderer.
**How to avoid:** After migrating content types, spot-check that the renamed sections still render correctly in the app. Specifically: ensure any section renamed to EXAMPLE has either a `code` field in frontmatter or embeds code in markdown that the CodeExampleSection can display.
**Warning signs:** The existing test `CourseContentValidationTests.cs` line 180 asserts that EXAMPLE sections must have `section.Code` non-empty. This will fail for CODE->EXAMPLE renamed sections unless the code field is populated.

### Pitfall 4: Python Module 14 Has Conflicting Directory Names

**What goes wrong:** Python Module 14 has duplicate directory number prefixes: two `02-` dirs, two `03-` dirs, two `04-` dirs, two `05-` dirs, two `06-` dirs, two `11-` dirs, two `12-` dirs, two `13-` dirs. The OS may handle this differently on case-sensitive vs case-insensitive filesystems.
**Why it happens:** Two separate AI sessions generated content for the same module, and both were committed.
**How to avoid:** This module needs manual analysis to determine which lessons keep, which merge, which move to other modules. It cannot be automated -- the content must be read and judged. The split decision is in Claude's discretion per the CONTEXT.md.
**Warning signs:** Any script that iterates Python Module 14 lessons will see duplicates and may behave unpredictably.

### Pitfall 5: The .gitignore Does Not Block .bak Files

**What goes wrong:** `*.bak` files are currently tracked in git. The .gitignore has `*.backup.json` and `*.backup` but NOT `*.bak`.
**Why it happens:** The gitignore was likely written before the .bak files were created by a different tool.
**How to avoid:** Add `*.bak` to `.gitignore` BEFORE deleting .bak files from tracking, so they do not get re-added.
**Warning signs:** `git status` showing .bak files as tracked.

### Pitfall 6: Existing E2E Tests Have Different Valid Type Lists

**What goes wrong:** `CourseContentValidationTests.cs` line 163 defines valid types as: `THEORY, EXAMPLE, KEY_POINT, LEGACY_COMPARISON, ANALOGY, INTRODUCTION, WARNING, TIP, NOTE, EXERCISE, SUMMARY, EXPERIMENT`. This is a DIFFERENT set from the standardized types decided in CONTEXT.md (`THEORY, EXAMPLE, KEY_POINT, ANALOGY, WARNING, LEGACY_COMPARISON`). The tests must be updated alongside the content migration.
**Why it happens:** Tests were written to match whatever existed at the time, not against a planned standard.
**How to avoid:** Update the test's `validSectionTypes` list as part of the content type migration task.

### Pitfall 7: Java course.json Difficulty "beginner-to-advanced" Fails Existing Test

**What goes wrong:** `CourseContentValidationTests.cs` line 362 expects difficulty to be one of `beginner, intermediate, advanced`. Java's course.json uses `beginner-to-advanced`.
**Why it happens:** Java course.json was generated with a range value; the test was written with discrete values.
**How to avoid:** Include `beginner-to-advanced` in the allowed difficulty values during schema standardization, or standardize all course.json difficulty values to a consistent format.

## Code Examples

### Current ID Format Inventory (Verified)

```
Course      | Lesson ID Format              | Example               | Module ID Format
------------|-------------------------------|-----------------------|------------------
Java        | epoch-{X}-lesson-{Y}          | epoch-0-lesson-1      | module-{NN}, module-{name}
Python      | module-{NN}-lesson-{NN}       | module-01-lesson-01   | module-{NN}
C#          | lesson-{NN}-{NN}              | lesson-01-01          | module-{NN}
JavaScript  | {N}.{N}                       | 1.1                   | module-{NN}, module-{name}
Kotlin      | {N}.{N}                       | 1.1                   | module-{NN}, module-{NNx}
Flutter     | {N}.{N}                       | 0.1                   | module-{NN}
```

**Recommendation for standardized lesson ID:** Use `lesson-{MM}-{LL}` format (e.g., `lesson-01-01`) because:
- The C# course already uses this pattern (132 lessons already compliant)
- It is self-documenting (encodes module and lesson position)
- It avoids collision across courses (the course ID is resolved by the directory path, not embedded in lesson ID)
- It sorts correctly alphabetically
- Python's `module-XX-lesson-YY` is close but verbose; `lesson-XX-YY` is shorter and just as clear

**Recommendation for standardized module ID:** Use `module-{NN}` format (e.g., `module-01`) because:
- Python, C#, and Flutter already use this pattern (most courses already compliant)
- Named IDs like `module-git`, `module-streams`, `module-concurrency` (Java) and `module-04a` (Kotlin) are inconsistent and do not sort numerically

### Current Course.json Schema Differences (Verified)

```json
// JAVA (fullest schema):
{
  "id": "java",
  "language": "java",
  "title": "...",
  "description": "...",
  "difficulty": "beginner-to-advanced",   // UNIQUE format
  "estimatedHours": 85,
  "totalModules": 16,                     // JAVA ONLY
  "totalLessons": 96,                     // JAVA ONLY
  "prerequisites": ["..."],
  "learningOutcomes": ["..."]             // JAVA ONLY
}

// PYTHON/JS/KOTLIN/CSHARP (minimal schema):
{
  "id": "python",
  "language": "python",
  "title": "...",
  "description": "...",
  "difficulty": "beginner",               // Single value
  "estimatedHours": 56,
  "prerequisites": []                     // Empty array
  // NO totalModules, totalLessons, learningOutcomes
}

// FLUTTER (slightly different):
{
  "id": "flutter",
  "language": "dart",                     // language != id (flutter vs dart)
  "title": "...",
  "description": "...",
  "difficulty": "beginner",
  "estimatedHours": 150,
  "prerequisites": ["Basic programming knowledge recommended"]
}
```

### Current Module.json Schema Differences (Verified)

```json
// JAVA (has order field):
{
  "id": "module-01",
  "title": "Java Fundamentals",
  "description": "...",
  "difficulty": "beginner",
  "estimatedHours": 1.5,
  "order": 1                              // JAVA ONLY has this
}

// PYTHON/CSHARP/JS/KOTLIN/FLUTTER (no order field):
{
  "id": "module-01",
  "title": "...",
  "description": "...",
  "difficulty": "beginner",
  "estimatedHours": 2
  // NO order field
}
```

Note: The WPF `Module` C# model does NOT have an `order` property. The Java module.json `order` field is simply ignored by the app. Including `order` in the standardized schema is harmless but also unused by the app.

### Current Lesson.json Schema Differences (Verified)

```json
// JAVA and FLUTTER (have description field):
{
  "id": "...",
  "title": "...",
  "description": "...",                   // PRESENT in Java and Flutter
  "moduleId": "module-01",
  "order": 1,
  "estimatedMinutes": 15,
  "difficulty": "beginner"
}

// PYTHON, CSHARP, JAVASCRIPT, KOTLIN (NO description field):
{
  "id": "...",
  "title": "...",
  // NO description field
  "moduleId": "module-01",
  "order": 1,
  "estimatedMinutes": 10,
  "difficulty": "beginner"
}
```

The WPF `Lesson` C# model does NOT have a `description` property, so this field is ignored at runtime. But it is useful metadata. The standardized schema should include `description` as optional.

### Content Section Type Distribution (From Architecture Research)

```
Type               | Java | Python | C#  | JS  | Kotlin | Flutter | Has Control?
-------------------|------|--------|-----|-----|--------|---------|-------------
THEORY             | 389  | 236    | 129 | 125 | 1390   | 861     | YES
EXAMPLE            | 49   | 317    | 143 | 180 | 193    | 431     | YES
KEY_POINT          | 177  | 179    | 1   | 17  | 30     | 160     | YES
WARNING            | 62   | 113    | 105 | 123 | 64     | 73      | NO (default)
ANALOGY            | 1    | 81     | 130 | 100 | 48     | 37      | NO (default)
LEGACY_COMPARISON  | 0    | 0      | 0   | 19  | 0      | 0       | YES
CODE               | 0    | 0      | 0   | 109 | 0      | 0       | NO (default)
CONCEPT            | 0    | 0      | 0   | 33  | 0      | 0       | NO (default)
ARCHITECTURE       | 0    | 0      | 11  | 0   | 0      | 0       | NO (default)
REAL_WORLD         | 0    | 0      | 8   | 0   | 0      | 0       | NO (default)
DEEP_DIVE          | 0    | 0      | 5   | 0   | 0      | 0       | NO (default)
EXPERIMENT         | 0    | 0      | 0   | 0   | 1      | 18      | NO (default)
```

**Migration plan:**
- CODE (109 in JS) -> EXAMPLE (may need `code` field populated for dedicated control)
- CONCEPT (33 in JS) -> THEORY (simple rename)
- ARCHITECTURE (11 in C#) -> THEORY (simple rename)
- REAL_WORLD (8 in C#) -> ANALOGY (simple rename, content is already analogous)
- DEEP_DIVE (5 in C#) -> THEORY (simple rename)
- EXPERIMENT (19 in Kotlin/Flutter) -> EXAMPLE (similar to CODE migration)

Total non-standard sections to migrate: **185 files**

### Python Module 14 Structure (Verified)

```
14-http-web-apis/lessons/
  01-http-basics-and-the-requests-library
  02-data-validation-with-pydantic           <- DUPLICATE PREFIX
  02-fastapi-fundamentals                    <- DUPLICATE PREFIX
  03-alternative-building-apis-with-flask    <- DUPLICATE PREFIX
  03-modern-apis-with-fastapi               <- DUPLICATE PREFIX
  04-fastapi-routes-and-models              <- DUPLICATE PREFIX
  04-pydantic-v2-deep-dive                  <- DUPLICATE PREFIX
  05-dependency-injection-in-fastapi        <- DUPLICATE PREFIX
  05-fastapi-advanced-patterns              <- DUPLICATE PREFIX
  06-fastapi-async-sqlalchemy-20            <- DUPLICATE PREFIX
  06-mini-project-fastapi-crud-api          <- DUPLICATE PREFIX
  07-database-migrations-with-alembic
  08-sqlite-for-development
  09-postgresql-for-production
  10-password-hashing
  11-authentication-and-api-security        <- DUPLICATE PREFIX
  11-jwt-authentication                     <- DUPLICATE PREFIX
  12-api-testing-and-documentation          <- DUPLICATE PREFIX
  12-oauth2-with-fastapi                    <- DUPLICATE PREFIX
  13-mini-project-complete-blog-api-with-authentication  <- DUPLICATE PREFIX
  13-why-django                             <- DUPLICATE PREFIX
  14-django-models-admin
  15-django-views-templates
  16-django-rest-framework-basics
  17-django-async-views-52
  18-object-relational-mapping-with-sqlalchemy
```

**26 total entries, 16 with duplicate prefixes (8 pairs). Topics span:**
- HTTP basics (1 lesson)
- Pydantic/validation (2 lessons)
- FastAPI fundamentals and patterns (6 lessons)
- Databases: SQLAlchemy, Alembic, SQLite, PostgreSQL (5 lessons)
- Auth: passwords, JWT, OAuth2 (4 lessons)
- Django: intro, models, views, DRF, async (5 lessons)
- Mini-projects (2 lessons)
- Flask (1 lesson)

**Natural split boundaries (recommendation for Claude's discretion):**
1. **HTTP & FastAPI** (HTTP basics, FastAPI fundamentals, routes, dependency injection, advanced patterns, Pydantic) -- ~8 lessons after dedup
2. **Databases** (SQLAlchemy, Alembic, SQLite, PostgreSQL) -- ~5 lessons
3. **Auth & Security** (passwords, JWT, OAuth2, API security) -- ~4 lessons
4. **Django** (intro, models, views, DRF, async) -- ~5 lessons

The Flask lesson is an outlier that could be a single comparison lesson in the FastAPI module.

### Directory Naming Convention Recommendation

Current patterns:
- Java: `01-java-fundamentals` (number + topic slug)
- Python: `01-the-absolute-basics` (number + topic slug)
- C#: `01-getting-started-with-c` (number + topic slug)
- JavaScript: `01-module-1-the-absolute-basics-the-what` (number + redundant module number + topic)
- Kotlin: `01-the-absolute-basics` (number + topic slug) for some, `05-module-04a-coroutines-flows` (mixed) for others
- Flutter: `01-module-0-flutter-development` (number + redundant module number + generic name)

**Recommendation:** `{NN}-{topic-slug}` (e.g., `01-java-fundamentals`). This is what Java, Python, and C# already use. JavaScript and Flutter's redundant `module-N-` prefix and Kotlin's `module-NNx` suffixes should be cleaned up.

## State of the Art

| Old Approach | Current Approach | When Changed | Impact |
|--------------|------------------|--------------|--------|
| JsonSchema.Net (free MIT) | JsonSchema.Net 8.x (paid EULA) | Feb 1, 2026 | Use NJsonSchema instead for free MIT JSON Schema validation in .NET |
| `git filter-branch` | `git filter-repo` | git 2.24+ (2019) | filter-branch is deprecated; filter-repo is the official replacement |
| Manual content file editing | Scripted batch operations | Always | 812 files cannot be manually edited reliably |

## Open Questions

1. **Should `description` be required or optional in lesson.json?**
   - What we know: Java and Flutter have it; Python, C#, JS, Kotlin do not. The WPF Lesson model has no description field (it is ignored at runtime).
   - What's unclear: Whether adding descriptions to 4 courses is worth the effort in Phase 1 vs deferring to audit phases.
   - Recommendation: Make `description` optional in the schema. Java and Flutter keep theirs. Other courses add descriptions during their audit phase (Phases 2-7), not in Phase 1. Phase 1 focuses on structural consistency, not content creation.

2. **Should `learningOutcomes` and `totalModules`/`totalLessons` be added to all course.json files?**
   - What we know: Only Java has these fields. They are not used by the app.
   - What's unclear: Whether these are valuable enough to require across all courses in Phase 1.
   - Recommendation: Make them optional in the schema. They are metadata for humans, not app functionality. Add during audit phases when someone is already reviewing each course holistically.

3. **Are the csharp capstone bin/obj directories actually in git history (INFR-02)?**
   - What we know: bin/obj directories exist on disk but are NOT currently tracked by git (already in .gitignore via `**/bin/` and `**/obj/` rules). No evidence found of them ever being committed to git history.
   - What's unclear: Whether an earlier branch or force-push removed them, or whether INFR-02 is based on stale information.
   - Recommendation: Verify with `git log --all --stat -- "*.dll" "*.exe"` more thoroughly. If nothing found, mark INFR-02 as already resolved (the gitignore already prevents future checkins). The local bin/obj directories are build artifacts and do not need to be deleted from the working tree.

4. **Should the `order` field be added to the Module C# model?**
   - What we know: The app sorts modules by directory name, ignoring any `order` field. Java's module.json has `order`; others do not.
   - What's unclear: Whether the app should be changed to read module `order`, or whether directory sort is sufficient.
   - Recommendation: Keep directory sort as the source of truth (it is simple and works). Include `order` in the JSON schema as optional (it mirrors the directory prefix and serves as documentation).

5. **What happens when JavaScript/Kotlin lesson IDs (e.g., "1.1") collide across courses?**
   - What we know: JavaScript lesson "1.1" and Kotlin lesson "1.1" are different lessons. The app resolves this because `GetLessonAsync` takes both `courseId` and `lessonId` as parameters. `_lessonPaths` dictionary is global but since JavaScript and Kotlin have different lesson directories, the last-loaded course wins the path mapping.
   - What's unclear: Whether the shared `_lessonPaths` dictionary causes bugs when switching between courses with identical lesson IDs.
   - Recommendation: Standardizing to `lesson-{MM}-{LL}` format resolves this because each course's directory structure ensures uniqueness. But verify the app does not have latent bugs from ID collisions.

## Sources

### Primary (HIGH confidence)
- Direct codebase analysis of `CourseService.cs`, `LessonPage.xaml.cs`, `Course.cs` models, `ProgressService.cs`, `CourseContentValidationTests.cs`
- Filesystem analysis of all 6 courses' directory structures, lesson.json files, module.json files, course.json files, content section markdown files
- Git status and git ls-files analysis of tracked files, .gitignore rules, .bak file locations

### Secondary (MEDIUM confidence)
- [NuGet: JsonSchema.Net 8.0.5](https://www.nuget.org/packages/JsonSchema.Net) -- verified Feb 2026 maintenance fee introduction
- [NuGet: NJsonSchema](https://github.com/RicoSuter/NJsonSchema) -- free MIT alternative
- Architecture research from `.planning/research/ARCHITECTURE.md` (content type counts, module analysis)
- Pitfalls research from `.planning/research/PITFALLS.md` (ID change risks, version claim issues)
- Stack research from `.planning/research/STACK.md` (version targets)

### Tertiary (LOW confidence)
- None. All findings in this document are based on direct codebase analysis or verified documentation.

## Metadata

**Confidence breakdown:**
- Standard stack: HIGH -- based on codebase analysis of actual JSON structures and C# models
- Architecture: HIGH -- based on reading actual CourseService.cs, LessonPage.xaml.cs source code
- Pitfalls: HIGH -- based on verified codebase evidence (ID formats confirmed, .gitignore rules confirmed, test code confirmed)
- Content type inventory: HIGH -- from prior architecture research verified against actual filesystem

**Research date:** 2026-02-02
**Valid until:** No expiration -- this is codebase analysis, not library research. Valid until the codebase changes.
