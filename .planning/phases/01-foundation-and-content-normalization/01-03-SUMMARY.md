---
phase: 01-foundation-and-content-normalization
plan: 03
subsystem: content-normalization
tags: [content-types, directory-naming, numbering, e2e-tests]
dependency-graph:
  requires: [01-02]
  provides: [standardized-content-types, clean-directory-naming, sequential-numbering]
  affects: [01-04, phase-02, phase-03, phase-04, phase-05, phase-06, phase-07]
tech-stack:
  added: []
  patterns: [standard-content-types, NN-topic-slug-naming]
key-files:
  created: []
  modified:
    - native-app.Tests/E2E/ContentValidation/CourseContentValidationTests.cs
    - content/courses/javascript/modules/*/  (21 modules renamed)
    - content/courses/flutter/modules/*/  (18 modules renamed)
    - content/courses/kotlin/modules/*/  (4 modules renamed)
    - content/courses/flutter/modules/09-serverpod-production-backend/lessons/*/  (17 lessons renumbered)
    - content/courses/**/content/*.md  (186 type migrations)
decisions:
  - id: norm-02-type-mapping
    description: "Mapped 7 non-standard types to 6 standard types: CODE->EXAMPLE, CONCEPT->THEORY, ARCHITECTURE->THEORY, REAL_WORLD->ANALOGY, DEEP_DIVE->THEORY, EXPERIMENT->EXAMPLE, PITFALLS->WARNING"
  - id: norm-03-flutter-module-slugs
    description: "Derived descriptive slugs for generic Flutter modules from lesson content: flutter-setup, dart-programming-basics, flutter-widget-fundamentals, layouts-and-scrolling, user-interaction, navigation-and-routing"
  - id: norm-03-difficulty-values
    description: "Added beginner-to-advanced as valid difficulty value in E2E tests to support Java course's range format"
metrics:
  duration: 17 min
  completed: 2026-02-02
---

# Phase 01 Plan 03: Content Type Migration and Directory Cleanup Summary

**One-liner:** Migrated 186 non-standard content section types to 6 standard names, cleaned up 43 module directory names, renumbered 17 lesson directories, and updated E2E validation tests.

## What Was Done

### Task 1: Migrate non-standard content section types (186 files)

Wrote and executed a Node.js script that read all 6,119 content section markdown files, parsed YAML frontmatter, and migrated 186 files with non-standard types to standard names.

**Migration map applied:**

| Old Type | New Type | Count | Courses |
|----------|----------|-------|---------|
| CODE | EXAMPLE | 109 | JavaScript |
| CONCEPT | THEORY | 33 | JavaScript |
| EXPERIMENT | EXAMPLE | 19 | Kotlin (1), Flutter (18) |
| ARCHITECTURE | THEORY | 11 | C# |
| REAL_WORLD | ANALOGY | 8 | C# |
| DEEP_DIVE | THEORY | 5 | C# |
| PITFALLS | WARNING | 1 | JavaScript |

**Post-migration type distribution (6,119 files):**
- THEORY: 3,172 (51.8%)
- EXAMPLE: 1,424 (23.3%)
- KEY_POINT: 558 (9.1%)
- WARNING: 541 (8.8%)
- ANALOGY: 405 (6.6%)
- LEGACY_COMPARISON: 19 (0.3%)

Commit: `0ba7c4d7`

### Task 2: Directory cleanup and E2E test updates

**Part A: Module directory renames (43 directories)**

Renamed module directories to `{NN}-{topic-slug}` convention:
- JavaScript: 21 modules (removed `module-N-` prefix, simplified verbose slugs)
- Flutter: 18 modules (removed `module-N-` infix, replaced generic "flutter-development" names with descriptive slugs derived from lesson content)
- Kotlin: 4 modules (removed `module-NNx` infixes like `04a`, `06a`, `06b`, `06c`)

**Part B: Lesson directory renumbering (17 directories)**

Fixed lesson numbering gaps in Flutter serverpod module (module 09). Original lesson directories had prefixes 01-02, 04-11, 99-107 with a gap at 03. Renumbered to sequential 01-19.

Updated `order` field in each affected `lesson.json` to match new sequential position.

Python Module 14 was correctly skipped (deferred to Plan 04 due to duplicate lesson prefixes requiring content-level decisions).

**Part C: Content section file numbering (0 files)**

Scanned all lesson content directories across all 6 courses. No content section file numbering gaps were found.

**Part D: E2E test updates**

Updated `CourseContentValidationTests.cs`:
- `validSectionTypes` reduced from 12 entries to 6: `THEORY, EXAMPLE, KEY_POINT, LEGACY_COMPARISON, ANALOGY, WARNING`
- Removed non-standard types: `INTRODUCTION, TIP, NOTE, EXERCISE, SUMMARY, EXPERIMENT`
- Added `beginner-to-advanced` to `validDifficulties` array to support Java course format

All 64 CourseContentValidationTests pass after changes.

## Deviations from Plan

### Auto-fixed Issues

**1. [Rule 1 - Bug] Discovered PITFALLS type not in research inventory**

- **Found during:** Task 1
- **Issue:** JavaScript course had 1 file with type `PITFALLS` (content about common interop issues) not listed in the RESEARCH.md inventory of 185 files
- **Fix:** Mapped PITFALLS -> WARNING (content is about common pitfalls/issues, which aligns with WARNING type semantics)
- **Files modified:** `content/courses/javascript/modules/08-.../lessons/04-.../content/08-pitfalls.md`
- **Impact:** Total migrations became 186 instead of expected 185

**2. [Rule 3 - Blocking] Concurrent Plan 01-04 execution absorbed Task 2 changes**

- **Found during:** Task 2 commit
- **Issue:** A concurrent Plan 01-04 agent executed between Task 1 and Task 2 commits. Its `git add` captured our directory renames and E2E test edits, committing them as part of `2e47c873` (docs for Plan 01-04).
- **Fix:** Verified all changes are correct in HEAD. Task 2 changes are in the repository but attributed to a different commit.
- **Impact:** Task 2 lacks its own dedicated commit. The changes are correct but commit attribution is mixed.

## Authentication Gates

None.

## Decisions Made

1. **PITFALLS -> WARNING mapping:** Content about common interop issues/pitfalls maps naturally to WARNING (cautionary information).
2. **Flutter module slug derivation:** For generic "Flutter Development" module names, derived descriptive slugs from the actual lesson content within each module.
3. **beginner-to-advanced difficulty:** Added as valid value rather than normalizing Java's course.json, since a range value is semantically meaningful.

## Verification Results

| Check | Result |
|-------|--------|
| Non-standard content types remaining | 0 of 6,119 files |
| Module directories with redundant infixes | 0 |
| Lesson numbering gaps (excl. Python M14) | 0 |
| Content section numbering gaps | 0 |
| CourseContentValidationTests passing | 64/64 |
| Migration scripts remaining | 0 |

## Next Phase Readiness

- NORM-02 (standardized content types) is fully satisfied
- NORM-03 (sequential numbering) is satisfied except Python Module 14 (deferred to Plan 04)
- All 6 courses now have consistent `{NN}-{topic-slug}` module directory naming
- E2E tests enforce the new standard going forward
