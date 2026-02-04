---
phase: 05-flutter-dart-course-audit
plan: 03
subsystem: backend
tags: [dart-frog, serverpod, drift, testing, api-integration, real-time, offline-first]

requires:
  - phase: 05-01
    provides: "Version alignment, structural cleanup, archived/misplaced lesson annotations"
  - phase: 01-05
    provides: "Version manifest with Flutter/Dart/Serverpod/GoRouter targets"
provides:
  - "M08 Dart Frog 1.2.x verified with community maintenance status documented (FLTR-05)"
  - "M09 Serverpod 2.x verified with 3.x migration warnings and CLI version pin"
  - "M10-M13 backend testing, API integration, real-time, offline verified"
affects: [05-04, 05-05, 05-06, 05-07]

tech-stack:
  added: []
  patterns:
    - "Serverpod 2.x version pin pattern (serverpod_cli ^2.0.0)"
    - "Community maintenance status documentation pattern"

key-files:
  created: []
  modified:
    - "content/courses/flutter/modules/08-dart-frog-backend-fundamentals/lessons/02-lesson-72-dart-frog-setup/content/01-theory.md"
    - "content/courses/flutter/modules/09-serverpod-production-backend/lessons/01-*/content/01-theory.md"
    - "content/courses/flutter/modules/09-serverpod-production-backend/lessons/02-*/content/03-theory.md"

key-decisions:
  - "Dart Frog community status note placed in L02 (setup lesson) rather than L01 (overview) for visibility at point of CLI installation"
  - "Serverpod 3.x note references Relic auth system and web server as the specific breaking changes"
  - "go_router ^14.6.0 updated to ^17.0.0 in M11 to match version manifest"
  - "SharedPreferences auth token example in M13 replaced with non-sensitive setting (security accuracy)"
  - "PostgreSQLException fixed to ServerException (postgres 3.x package API)"

patterns-established:
  - "Version pin warning pattern: blockquote with IMPORTANT label and exact CLI command"
  - "Community maintenance notice: blockquote with GitHub organization link"

duration: 9min
completed: 2026-02-04
---

# Phase 5 Plan 3: Backend Modules (M08-M13) Accuracy Audit Summary

**Dart Frog 1.2.x and Serverpod 2.x backend content verified across 44 active lessons with community transition status, 3.x migration warnings, and version pin guidance added**

## Performance

- **Duration:** 9 min
- **Started:** 2026-02-04T04:11:11Z
- **Completed:** 2026-02-04T04:20:04Z
- **Tasks:** 2/2
- **Files modified:** 14

## Accomplishments

- M08 Dart Frog (8 lessons, 46 content files) verified against dart_frog 1.2.x with community maintenance status documented (FLTR-05 requirement)
- M09 Serverpod (10 active lessons) verified for 2.x patterns with Serverpod 3.0 awareness notes and CLI version pin warnings
- M10-M13 (26 active lessons) verified: testing, API integration, real-time, offline persistence all current
- 9 archived Firebase/Supabase lessons and 5 misplaced lessons appropriately skipped (14 lessons excluded per plan)

## Task Commits

1. **Task 1: M08 Dart Frog + M09 Serverpod verification** - `5daf93be` (feat)
2. **Task 2: M10-M13 testing, API, real-time, offline verification** - `8dd78175` (feat)

## Files Created/Modified

- `M08 L01/02-theory.md` - Dart Frog VGV reference updated to community maintenance
- `M08 L02/01-theory.md` - Community maintenance notice blockquote added
- `M08 L01/05-theory.md` - Module numbering 7 -> 8
- `M08 L06/01-theory.md` - Removed stale "covered in Module 8" Firebase cross-reference
- `M08 L06/05-key_point.md` - PostgreSQLException -> ServerException (postgres 3.x)
- `M08 L08/07-key_point.md` - Module 7 -> 8, Firebase -> Serverpod next-module reference
- `M09 L01/01-theory.md` - Serverpod 3.x awareness note with Relic auth and web server changes
- `M09 L02/02-theory.md` - Dart version 3.0 -> 3.10
- `M09 L02/03-theory.md` - CLI version pin warning (serverpod_cli ^2.0.0)
- `M09 L18/01-theory.md` - Version pin note for chat backend mini-project
- `M10 L04/03-theory.md` - serverpod_test ^1.2.0 -> ^2.0.0
- `M11 L07/02-example.md` - go_router ^14.6.0 -> ^17.0.0
- `M11 L08/02-theory.md` - go_router ^14.6.0 -> ^17.0.0
- `M13 L02/01-theory.md` - Auth token removed from SharedPreferences use cases, security note added

## Decisions Made

- **Dart Frog community note placement:** Added to L02 (setup/CLI installation) for maximum visibility at the point where students first interact with the framework, plus updated the VGV description in L01
- **Serverpod 3.x specifics:** Named Relic authentication system and web server as the specific breaking changes (not generic "breaking changes") to help students understand what differs
- **go_router version update:** ^14.6.0 -> ^17.0.0 to match version manifest from 05-01; API patterns (redirect, refreshListenable, ShellRoute) are identical between versions
- **SharedPreferences security fix:** The M13 lesson listed auth tokens as a valid SharedPreferences use case, contradicting M11 which correctly teaches flutter_secure_storage; fixed for consistency and security accuracy
- **Module numbering fixes:** Multiple references to "Module 7" in M08 content fixed to "Module 8" (artifact of original lesson numbering before renumbering)

## Deviations from Plan

### Auto-fixed Issues

**1. [Rule 1 - Bug] Module numbering inconsistencies in M08**
- **Found during:** Task 1
- **Issue:** M08 L01 and L08 referenced "Module 7" and "Lesson 7.x" instead of Module 8
- **Fix:** Updated all module number references to 8
- **Files modified:** M08 L01/05-theory.md, M08 L08/07-key_point.md
- **Committed in:** 5daf93be

**2. [Rule 1 - Bug] Stale next-module reference in M08 L08**
- **Found during:** Task 1
- **Issue:** "Module 8 Coming Up: Firebase integration" -- Firebase lessons are archived, Module 9 is Serverpod
- **Fix:** Changed to "Module 9 Coming Up: Serverpod"
- **Files modified:** M08 L08/07-key_point.md
- **Committed in:** 5daf93be

**3. [Rule 1 - Bug] PostgreSQLException in M08 L06**
- **Found during:** Task 1
- **Issue:** `PostgreSQLException` is the old postgres 2.x exception class; postgres 3.x uses `ServerException`
- **Fix:** Updated to `ServerException`
- **Files modified:** M08 L06/05-key_point.md
- **Committed in:** 5daf93be

**4. [Rule 1 - Bug] SharedPreferences auth token recommendation in M13**
- **Found during:** Task 2
- **Issue:** M13 L02 listed "Authentication tokens" as valid SharedPreferences use case and showed token storage code, contradicting M11's correct flutter_secure_storage teaching
- **Fix:** Removed auth tokens from use cases, replaced code example with non-sensitive setting, added security warning
- **Files modified:** M13 L02/01-theory.md
- **Committed in:** 8dd78175

---

**Total deviations:** 4 auto-fixed (4 bugs)
**Impact on plan:** All fixes were factual accuracy corrections. No scope creep.

## Issues Encountered

None -- all content verified smoothly. The Dart Frog, Serverpod 2.x, Drift 2.x, and package:test APIs are accurately documented in the course content.

## Verification Results

| Check | Result |
|-------|--------|
| Dart Frog community status in M08 | Present in L01 and L02 |
| Serverpod 3.x awareness in M09 L01 | Present |
| Serverpod CLI version pin in M09 L02 | Present |
| Serverpod CLI version pin in M09 L18 | Present |
| Firebase/Supabase in active M09 lessons | Comparison context only (appropriate) |
| M10-M13 package versions current | All updated |
| Archived lessons skipped | 9 Firebase/Supabase (M09 L10-L17, L19) |
| Misplaced lessons skipped | 5 (M10 L06-L08, M11 L09-L10) |
| Total active lessons audited | 44 (8 + 10 + 5 + 8 + 6 + 7) |

## User Setup Required

None - no external service configuration required.

## Next Phase Readiness

- Backend modules (M08-M13) fully verified and current
- Ready for remaining module audits (M14-M18) in plans 05-04 through 05-07
- No blockers identified

---
*Phase: 05-flutter-dart-course-audit*
*Completed: 2026-02-04*
