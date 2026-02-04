---
phase: 05-flutter-dart-course-audit
plan: 01
subsystem: content-structure
tags: [flutter, dart, version-manifest, module-titles, file-rename, archived-lessons]

# Dependency graph
requires:
  - phase: 01-foundation-and-content-normalization
    provides: Version manifest structure, schema definitions, content type standards
provides:
  - Flutter 3.38.x/Dart 3.10.x version targets in version-manifest.json
  - All 18 module titles are descriptive (zero generic)
  - All 18 experiment files renamed to example (standard naming)
  - 9 archived Firebase/Supabase lessons annotated with "archived": true
  - 5 misplaced lessons annotated with placement notes
  - Module descriptions updated to reflect actual content
affects: [05-02 through 05-07 accuracy passes, future Flutter version upgrades]

# Tech tracking
tech-stack:
  added: [Drift 2.x framework entry in version manifest]
  patterns: ["archived" boolean field in lesson.json for legacy content]

key-files:
  created: []
  modified:
    - content/version-manifest.json
    - content/courses/flutter/course.json
    - content/courses/flutter/modules/01-flutter-setup/module.json
    - content/courses/flutter/modules/02-dart-programming-basics/module.json
    - content/courses/flutter/modules/03-flutter-widget-fundamentals/module.json
    - content/courses/flutter/modules/04-layouts-and-scrolling/module.json
    - content/courses/flutter/modules/05-user-interaction/module.json
    - content/courses/flutter/modules/07-navigation-and-routing/module.json
    - content/courses/flutter/modules/09-serverpod-production-backend/module.json
    - content/courses/flutter/modules/10-backend-testing/module.json
    - content/courses/flutter/modules/11-api-integration-and-auth-flows/module.json
    - 18 experiment.md files renamed to example.md
    - 9 archived lesson.json files in M09
    - 5 misplaced lesson.json files in M10/M11

key-decisions:
  - "Flutter 3.38.x/Dart 3.10.x as course target (matches setup content and current stable)"
  - "Riverpod 2.x patterns retained (Riverpod 4.0 expected; migration deferred)"
  - "Serverpod 2.x patterns retained (3.0 too new, Dec 2025; migration deferred)"
  - "GoRouter updated to 17.x (requires Flutter 3.32+/Dart 3.8+)"
  - "Drift 2.x added to version manifest (used in M13 offline persistence)"
  - "M03/M04/M05 descriptions written from scratch (originals were generic placeholders)"
  - "course.json estimatedHours kept at 150 (153 lessons; ratio ~1h/lesson reasonable for full-stack course)"

patterns-established:
  - "archived boolean: lesson.json field to mark legacy content that should not be audited"
  - "[Note:] prefix: description annotation for misplaced lessons that cannot be moved"

# Metrics
duration: 5min
completed: 2026-02-04
---

# Phase 5 Plan 01: Version Alignment and Structural Cleanup Summary

**Flutter course aligned to 3.38.x/Dart 3.10.x with 6 module titles fixed, 18 experiment files renamed, and 14 archived/misplaced lessons annotated**

## Performance

- **Duration:** 5 min
- **Started:** 2026-02-04T04:02:23Z
- **Completed:** 2026-02-04T04:07:07Z
- **Tasks:** 2
- **Files modified:** 41

## Accomplishments
- Version manifest and course.json aligned to Flutter 3.38.x/Dart 3.10.x with comprehensive framework notes
- All 18 module titles are now descriptive (zero "Module X: Flutter Development" remaining)
- All 18 non-standard experiment filenames renamed to example (matching EXAMPLE frontmatter type)
- 9 archived Firebase/Supabase lessons in M09 marked with `"archived": true`
- 5 misplaced lessons in M10/M11 annotated with `[Note:]` placement context
- M09/M10/M11 module descriptions updated to reflect mixed content

## Task Commits

Each task was committed atomically:

1. **Task 1: Version alignment and course metadata** - `e930aff1` (feat)
2. **Task 2: Fix module titles, rename experiments, annotate archived lessons** - `679af9ae` (feat)

**Plan metadata:** (pending)

## Files Created/Modified
- `content/version-manifest.json` - Flutter 3.38.x/Dart 3.10.x targets, Drift added, framework notes updated
- `content/courses/flutter/course.json` - minimumRuntimeVersion Flutter 3.38, difficulty beginner-to-advanced, updated description
- `content/courses/flutter/modules/01-flutter-setup/module.json` - Title: "Flutter Setup and Environment"
- `content/courses/flutter/modules/02-dart-programming-basics/module.json` - Title: "Dart Programming Basics"
- `content/courses/flutter/modules/03-flutter-widget-fundamentals/module.json` - Title: "Flutter Widget Fundamentals" + new description
- `content/courses/flutter/modules/04-layouts-and-scrolling/module.json` - Title: "Layouts and Scrolling" + new description
- `content/courses/flutter/modules/05-user-interaction/module.json` - Title: "User Interaction" + new description
- `content/courses/flutter/modules/07-navigation-and-routing/module.json` - Title: "Navigation and Routing"
- `content/courses/flutter/modules/09-serverpod-production-backend/module.json` - Description notes archived lessons 10-19
- `content/courses/flutter/modules/10-backend-testing/module.json` - Description notes supplementary lessons 6-8
- `content/courses/flutter/modules/11-api-integration-and-auth-flows/module.json` - Description notes supplementary lessons 9-10
- 18 `*-experiment.md` files renamed to `*-example.md` across M02, M04, M07, M09, M10, M11
- 9 archived `lesson.json` files in M09 (lessons 10-17, 19) with `"archived": true`
- 5 misplaced `lesson.json` files in M10 (L06-L08) and M11 (L09-L10) with `[Note:]` annotations

## Decisions Made
- **Flutter 3.38.x/Dart 3.10.x target:** Matches setup lesson content ("tested with Flutter 3.38+") and current stable release
- **Riverpod 2.x retained:** Course teaches Notifier/AsyncNotifier patterns that work in 3.x via legacy imports; Riverpod 4.0 expected soon
- **Serverpod 2.x retained:** 3.0 released Dec 2025 with breaking auth changes; too new for full migration
- **GoRouter 17.x:** Already referenced in some course content; requires Flutter 3.32+
- **Drift 2.x added:** Used in Module 13 offline persistence; compatible within 2.x line
- **estimatedHours kept at 150:** 153 lessons at ~1h/lesson is reasonable for a full-stack course (Java 96L=80h, C# 132L=100h, JS 132L=120h)
- **M03/M04/M05 descriptions:** Written from scratch since originals were generic placeholders; derived from directory slugs and lesson content analysis

## Deviations from Plan

None - plan executed exactly as written.

## Issues Encountered
None

## User Setup Required
None - no external service configuration required.

## Next Phase Readiness
- Structural foundation is clean for accuracy passes (plans 05-02 through 05-07)
- All modules have descriptive titles for context during content review
- Archived lessons clearly marked -- accuracy passes should skip these 9 lessons
- Misplaced lessons annotated -- accuracy passes should note but not move content
- Version targets established -- all content can be verified against Flutter 3.38.x/Dart 3.10.x

---
*Phase: 05-flutter-dart-course-audit*
*Completed: 2026-02-04*
