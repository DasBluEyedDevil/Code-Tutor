---
phase: 01-foundation-and-content-normalization
plan: 05
subsystem: content-infrastructure
tags: [version-manifest, runtime-versions, framework-targets, course-metadata]

dependency-graph:
  requires: []
  provides:
    - "Version manifest with pinned runtime and framework targets for all 6 courses"
    - "minimumRuntimeVersion field in all course.json files"
  affects:
    - "Phase 2-7: All course audit phases reference version-manifest.json for code example verification"
    - "Phase 8: AI tutor RAG must align with pinned runtime versions"

tech-stack:
  added: []
  patterns:
    - "Centralized version manifest as single source of truth for all course version targets"
    - "minimumRuntimeVersion field in course.json for runtime requirements"

file-tracking:
  key-files:
    created:
      - content/version-manifest.json
    modified:
      - content/courses/java/course.json
      - content/courses/python/course.json
      - content/courses/csharp/course.json
      - content/courses/javascript/course.json
      - content/courses/kotlin/course.json
      - content/courses/flutter/course.json

decisions:
  - id: "version-targets"
    description: "Pinned version targets for all 6 courses based on ROADMAP.md and RESEARCH.md"
  - id: "manifest-note"
    description: "Added note to manifest that framework versions should be re-verified when each course audit begins"
  - id: "schema-skip"
    description: "Skipped course.schema.json update because schemas directory does not exist yet (Plan 02 pending)"

metrics:
  duration: "2 min"
  completed: "2026-02-02"
---

# Phase 01 Plan 05: Version Manifest Summary

**One-liner:** Centralized version manifest pinning Java 21, Python 3.12, .NET 8.0, Node.js 22, Kotlin 2.0, Flutter 3.27 with framework targets for all 6 courses.

## What Was Done

### Task 1: Create version manifest (22eb84c1)

Created `content/version-manifest.json` with pinned runtime and framework version targets for all 6 courses:

| Course | Runtime | Key Frameworks |
|--------|---------|----------------|
| Java | Java 21 (LTS) | Spring Boot 3.4.x, Gradle 8.x, JUnit 5.x, JPA/Hibernate 6.x |
| Python | Python 3.12+ | FastAPI 0.115.x, Django 5.1.x, SQLAlchemy 2.0.x, Pydantic 2.x, pytest 8.x, Alembic 1.x |
| C# | .NET 8.0 (LTS) / C# 12 | ASP.NET Core 8.0, EF Core 8.0, Blazor 8.0, xUnit 2.x, .NET Aspire 9.x |
| JavaScript | Node.js 22 (LTS) | Bun 1.x, Hono 4.x, React 19.x, Prisma 6.x, TypeScript 5.x |
| Kotlin | Kotlin 2.0+ (K2) | Ktor 3.x, Compose Multiplatform 1.7.x, SQLDelight 2.x, Koin 4.x, Gradle 8.x |
| Flutter | Flutter 3.27.x / Dart 3.6.x | Riverpod 2.x, GoRouter 14.x, Dart Frog 1.x, Serverpod 2.x |

Each entry includes:
- Runtime name, version, version type, and lastVerified date
- Framework array with name, version, notes, and lastVerified date
- Advisory note that framework versions should be re-verified at audit time

### Task 2: Add minimumRuntimeVersion to course.json files (d0ebd1e4)

Added `minimumRuntimeVersion` field to all 6 course.json files:
- `java/course.json`: "Java 21"
- `python/course.json`: "Python 3.12"
- `csharp/course.json`: ".NET 8.0"
- `javascript/course.json`: "Node.js 22"
- `kotlin/course.json`: "Kotlin 2.0"
- `flutter/course.json`: "Flutter 3.27"

Checked all prerequisites arrays for outdated version references -- none found.

Skipped `content/schemas/course.schema.json` update because the schemas directory does not yet exist (Plan 02 creates it). The field will be supported by `additionalProperties: true` or explicitly added when the schema is created.

## Deviations from Plan

None -- plan executed exactly as written.

## Decisions Made

1. **Version targets from planning docs**: Used versions from ROADMAP.md and RESEARCH.md as baseline targets. Added note to manifest that each course audit phase should re-verify latest stable versions.
2. **Spring Boot 3.4.x (not 3.3.x)**: Course description already references "Spring Boot 3.4+" so manifest uses 3.4.x to match existing content.
3. **Schema update skipped**: `content/schemas/course.schema.json` does not exist yet. When Plan 02 creates the schema, `minimumRuntimeVersion` should be included as an optional string field.

## Requirements Satisfied

- **NORM-05**: Version targets pinned per language and documented in manifest file
- **INFR-03**: Minimum runtime versions updated in course.json metadata

## Verification Results

All 5 verification checks passed:
1. `content/version-manifest.json` exists with all 6 courses
2. All 6 `course.json` files have `minimumRuntimeVersion` field
3. Version targets match project requirements
4. Each manifest entry has `lastVerified` date
5. Framework versions included for each course (29 total across 6 courses)

## Next Phase Readiness

The version manifest is ready for use by all subsequent audit phases (2-7). Auditors should:
1. Consult `content/version-manifest.json` when verifying code examples
2. Update `lastVerified` dates after confirming framework API compatibility
3. Re-verify framework patch versions (e.g., FastAPI 0.115.x -> actual latest) at audit start
