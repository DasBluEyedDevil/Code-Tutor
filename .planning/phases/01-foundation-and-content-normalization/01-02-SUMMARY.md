---
phase: 01-foundation-and-content-normalization
plan: 02
subsystem: content-metadata
tags: [json-schema, id-standardization, content-normalization, validation]

# Dependency graph
requires:
  - "01-01: Clean git baseline"
provides:
  - "4 JSON Schema files defining canonical content metadata structure"
  - "All 812 lesson IDs standardized to lesson-{MM}-{LL} format"
  - "All 116 module IDs standardized to module-{NN} format"
  - "All moduleId cross-references validated"
  - "All module.json files have order field"
affects:
  - "01-03: Directory renaming operates on standardized IDs"
  - "01-04: Python restructuring builds on standardized ID baseline"
  - "Phase 2+: All course audits can rely on consistent ID format"
  - "Phase 8+: AI tutor RAG can use predictable lesson-{MM}-{LL} identifiers"

# Tech tracking
tech-stack:
  added:
    - "JSON Schema (Draft 07) for content validation"
  patterns:
    - "Shared schema directory at content/schemas/"
    - "Self-documenting IDs encoding module and lesson position"
    - "ID pattern: module-{NN} for modules, lesson-{MM}-{LL} for lessons"

key-files:
  created:
    - "content/schemas/course.schema.json"
    - "content/schemas/module.schema.json"
    - "content/schemas/lesson.schema.json"
    - "content/schemas/challenge.schema.json"
  modified:
    - "content/courses/*/modules/*/module.json (116 files)"
    - "content/courses/*/modules/*/lessons/*/lesson.json (680 files modified out of 812)"

key-decisions:
  - "Flutter module-00 renumbered to module-01 (1-based sequential from directory order)"
  - "C# lesson IDs confirmed already correct -- zero ID changes needed"
  - "challenge.schema.json allows 9 different type values reflecting actual usage across courses"
  - "Module order field added to all 116 module.json files for explicit ordering"
  - "minimumRuntimeVersion included as optional string in course.schema.json per Plan 01-05 requirement"

metrics:
  duration: "5 min"
  completed: "2026-02-02"
---

# Phase 01 Plan 02: JSON Schemas and ID Standardization Summary

**One-liner:** JSON Schema validation for 4 content types plus standardized lesson-{MM}-{LL} and module-{NN} IDs across all 6 courses (812 lessons, 116 modules).

## What Was Done

### Task 1: Created 4 JSON Schema files

Created `content/schemas/` directory with 4 Draft-07 JSON Schema files:

- **course.schema.json**: Required fields (id, language, title, description, difficulty, estimatedHours, prerequisites) with difficulty enum including "beginner-to-advanced". Optional minimumRuntimeVersion (string) per Plan 01-05.
- **module.schema.json**: Required fields with id pattern `^module-\d{2}$`. Optional order field.
- **lesson.schema.json**: Required fields with id pattern `^lesson-\d{2}-\d{2}$` and moduleId pattern `^module-\d{2}$`.
- **challenge.schema.json**: Permissive schema with 9 known type values (FREE_CODING, MULTIPLE_CHOICE, QUIZ, CODE, etc.) and optional testCases, hints, commonMistakes arrays. 928 challenge.json files analyzed (1 malformed JSON found in Python module 01).

### Task 2: Standardized all IDs and validated

Executed Node.js migration script that processed all 6 courses in a single atomic pass:

| Course | Modules | Lessons | ID Changes | Format Before |
|--------|---------|---------|------------|---------------|
| Java | 16 | 96 | 96 | epoch-0-lesson-N, module-git |
| C# | 24 | 132 | 0 | already lesson-NN-NN |
| Python | 22 | 171 | 171 | module-NN-lesson-NN |
| JavaScript | 21 | 132 | 132 | N.N |
| Kotlin | 15 | 128 | 128 | N.N |
| Flutter | 18 | 153 | 153 | N.N, module-00 |
| **Total** | **116** | **812** | **680** | |

Additional fixes:
- Added `order` field to all 116 module.json files (was missing from all non-Java modules)
- All course.json files validated against schema (all passed)
- Zero validation errors after migration

## Deviations from Plan

None -- plan executed exactly as written.

## Verification Results

All 5 final checks passed:
1. 4 schema files exist in content/schemas/
2. All 812 lesson.json IDs match `^lesson-\d{2}-\d{2}$`
3. All 116 module.json IDs match `^module-\d{2}$`
4. All lesson moduleId references point to valid modules in the same course
5. No temporary script files remain

## Known Issues for Future Plans

- **1 malformed challenge.json**: `content/courses/python/modules/01-the-absolute-basics/lessons/05-mini-project-a-conversation-program/challenges/01-practice-exercise/challenge.json` has invalid JSON (unescaped quotes in hints text). Should be fixed in Python course audit (Phase 3).
- **Python Module 14**: Has duplicate directory prefixes (known issue). IDs assigned sequentially from current directory order. Plan 04 will restructure and reassign.
- **Challenge type inconsistency**: 9 different type values exist across courses (FREE_CODING, QUIZ, CODE, quiz, coding, implementation, etc.). Normalization deferred to course audit phases.

## Next Phase Readiness

Plan 01-03 (directory renaming) can proceed. All IDs are now standardized and self-documenting, so directory names can be aligned to match.
