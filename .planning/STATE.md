# Project State

## Project Reference

See: .planning/PROJECT.md (updated 2026-02-02)

**Core value:** Every course teaches a coherent, progressive path from absolute beginner to independently building and deploying a real application.
**Current focus:** Phase 1 - Foundation and Content Normalization

## Current Position

Phase: 1 of 9 (Foundation and Content Normalization)
Plan: 5 of 5 in current phase (01-01, 01-02, 01-03, 01-04, 01-05 complete)
Status: Phase complete
Last activity: 2026-02-02 -- Completed 01-03-PLAN.md (Content Type Migration and Directory Cleanup)

Progress: [##########] 5/5 phase plans (100%)

## Performance Metrics

**Velocity:**
- Total plans completed: 5
- Average duration: 7 min
- Total execution time: 36 min

**By Phase:**

| Phase | Plans | Total | Avg/Plan |
|-------|-------|-------|----------|
| 01-foundation | 5/5 | 36 min | 7 min |

**Recent Trend:**
- Last 5 plans: 01-01 (2 min), 01-05 (2 min), 01-02 (5 min), 01-04 (10 min), 01-03 (17 min)
- Trend: increasing (larger plans take longer as expected)

*Updated after each plan completion*

## Accumulated Context

### Decisions

Decisions are logged in PROJECT.md Key Decisions table.
Recent decisions affecting current work:

- [Roadmap]: Content normalization must complete before any course audit begins
- [Roadmap]: Course audit order: Java -> JS -> C# -> Flutter -> Kotlin -> Python (least to most structural work)
- [Roadmap]: AI tutor enhancement deferred until all 6 courses are stable (RAG on unstable content = rework)
- [01-01]: *.bak rule placed in .gitignore Backup files section (line 79) alongside *.backup.json and *.backup
- [01-01]: INFR-02 confirmed resolved: no compiled binaries ever existed in git history
- [01-05]: Version manifest created at content/version-manifest.json -- single source of truth for all course version targets
- [01-05]: Spring Boot target set to 3.4.x (matching existing course description "3.4+"), not 3.3.x from original plan estimate
- [01-05]: Schema update skipped (Plan 02 creates schemas directory); minimumRuntimeVersion to be added to course.schema.json when created
- [01-02]: Flutter module-00 renumbered to module-01 (1-based sequential from directory order)
- [01-02]: C# lesson IDs confirmed already correct (zero ID changes needed)
- [01-02]: challenge.schema.json allows 9 different type values reflecting actual usage across courses
- [01-02]: Module order field added to all 116 module.json files for explicit ordering
- [01-02]: minimumRuntimeVersion included as optional string in course.schema.json per Plan 01-05 requirement
- [01-03]: Mapped 7 non-standard types to 6 standard types (CODE->EXAMPLE, CONCEPT->THEORY, ARCHITECTURE->THEORY, REAL_WORLD->ANALOGY, DEEP_DIVE->THEORY, EXPERIMENT->EXAMPLE, PITFALLS->WARNING)
- [01-03]: Flutter generic module names replaced with descriptive slugs derived from lesson content
- [01-03]: beginner-to-advanced added as valid difficulty value in E2E tests
- [01-04]: Python Module 14 split into 3 modules (not 4) -- Django content better served by existing Module 19/21
- [01-04]: Downstream modules 19/20/21 kept as separate advanced modules -- not merged with intro-level splits from M14
- [01-04]: Python course now has 24 modules (was 22), with modules 15-22 renumbered to 17-24
- [01-04]: Capstone module metadata fixed (difficulty: advanced, estimatedHours: 10)

### Pending Todos

- Fix malformed challenge.json in Python module 01 lesson 05 (invalid JSON with unescaped quotes in hints)
- Normalize challenge type values across courses (9 different types exist: FREE_CODING, QUIZ, CODE, quiz, coding, implementation, etc.)

### Blockers/Concerns

- Java IO.println vs System.out.println decision needed in Phase 2 (affects 75+ files)
- Dart Frog community transition (July 2025) needs API verification in Phase 5
- ONNX Runtime GenAI 12 versions behind (0.5.2 -> current) -- upgrade risk in Phase 8

## Session Continuity

Last session: 2026-02-02T21:23:00Z
Stopped at: Completed 01-03-PLAN.md (Content Type Migration and Directory Cleanup) -- Phase 1 complete
Resume file: None
