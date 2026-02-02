# Project State

## Project Reference

See: .planning/PROJECT.md (updated 2026-02-02)

**Core value:** Every course teaches a coherent, progressive path from absolute beginner to independently building and deploying a real application.
**Current focus:** Phase 1 - Foundation and Content Normalization

## Current Position

Phase: 1 of 9 (Foundation and Content Normalization)
Plan: 2 of 5 in current phase (01-01, 01-05 complete)
Status: In progress
Last activity: 2026-02-02 -- Completed 01-05-PLAN.md (Version Manifest)

Progress: [####......] 2/5 phase plans (40%)

## Performance Metrics

**Velocity:**
- Total plans completed: 2
- Average duration: 2 min
- Total execution time: 4 min

**By Phase:**

| Phase | Plans | Total | Avg/Plan |
|-------|-------|-------|----------|
| 01-foundation | 2/5 | 4 min | 2 min |

**Recent Trend:**
- Last 5 plans: 01-01 (2 min), 01-05 (2 min)
- Trend: stable

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

### Pending Todos

None yet.

### Blockers/Concerns

- Java IO.println vs System.out.println decision needed in Phase 2 (affects 75+ files)
- Dart Frog community transition (July 2025) needs API verification in Phase 5
- ONNX Runtime GenAI 12 versions behind (0.5.2 -> current) -- upgrade risk in Phase 8

## Session Continuity

Last session: 2026-02-02T20:53:48Z
Stopped at: Completed 01-05-PLAN.md (Version Manifest)
Resume file: None
