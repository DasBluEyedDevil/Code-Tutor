# Phase 6 Plan 08: KMP Capstone Project Summary

**One-liner**: TaskFlow KMP capstone with Ktor 3.4/Exposed 1.0/H2 server + CMP 1.10/SQLDelight 2.2 offline-first client replacing ShopKotlin Android-only app

---
phase: 06
plan: 08
subsystem: kotlin-course-content
tags: [capstone, kmp, ktor, compose-multiplatform, sqldelight, exposed, koin, h2]
depends_on:
  requires: [06-03, 06-04]
  provides: [kotlin-capstone-kmp, m12-capstone-rewrite]
  affects: [06-09, 06-10]
tech-stack:
  added: [h2-database, bcrypt]
  patterns: [offline-first, shared-dto, kmp-server-client]
key-files:
  created:
    - content/courses/kotlin/modules/12-professional-development-deployment/lessons/08-lesson-78-final-capstone-full-stack-e-commerce-platform/content/14-key_point.md
    - content/courses/kotlin/modules/12-professional-development-deployment/lessons/08-lesson-78-final-capstone-full-stack-e-commerce-platform/content/15-theory.md
  modified:
    - content/courses/kotlin/modules/12-professional-development-deployment/lessons/08-lesson-78-final-capstone-full-stack-e-commerce-platform/content/01-theory.md
    - content/courses/kotlin/modules/12-professional-development-deployment/lessons/08-lesson-78-final-capstone-full-stack-e-commerce-platform/content/02-theory.md
    - content/courses/kotlin/modules/12-professional-development-deployment/lessons/08-lesson-78-final-capstone-full-stack-e-commerce-platform/content/03-theory.md
    - content/courses/kotlin/modules/12-professional-development-deployment/lessons/08-lesson-78-final-capstone-full-stack-e-commerce-platform/content/04-theory.md
    - content/courses/kotlin/modules/12-professional-development-deployment/lessons/08-lesson-78-final-capstone-full-stack-e-commerce-platform/content/05-theory.md
    - content/courses/kotlin/modules/12-professional-development-deployment/lessons/08-lesson-78-final-capstone-full-stack-e-commerce-platform/content/06-theory.md
    - content/courses/kotlin/modules/12-professional-development-deployment/lessons/08-lesson-78-final-capstone-full-stack-e-commerce-platform/content/07-theory.md
    - content/courses/kotlin/modules/12-professional-development-deployment/lessons/08-lesson-78-final-capstone-full-stack-e-commerce-platform/content/08-theory.md
    - content/courses/kotlin/modules/12-professional-development-deployment/lessons/08-lesson-78-final-capstone-full-stack-e-commerce-platform/content/09-theory.md
    - content/courses/kotlin/modules/12-professional-development-deployment/lessons/08-lesson-78-final-capstone-full-stack-e-commerce-platform/content/10-theory.md
    - content/courses/kotlin/modules/12-professional-development-deployment/lessons/08-lesson-78-final-capstone-full-stack-e-commerce-platform/content/11-theory.md
    - content/courses/kotlin/modules/12-professional-development-deployment/lessons/08-lesson-78-final-capstone-full-stack-e-commerce-platform/content/12-theory.md
    - content/courses/kotlin/modules/12-professional-development-deployment/lessons/08-lesson-78-final-capstone-full-stack-e-commerce-platform/content/13-theory.md
    - content/courses/kotlin/modules/12-professional-development-deployment/lessons/08-lesson-78-final-capstone-full-stack-e-commerce-platform/lesson.json
    - content/courses/kotlin/modules/12-professional-development-deployment/lessons/08-lesson-78-final-capstone-full-stack-e-commerce-platform/challenges/01-capstone-architecture-quiz/challenge.json
  deleted:
    - content/courses/kotlin/modules/12-professional-development-deployment/lessons/08-lesson-78-final-capstone-full-stack-e-commerce-platform/content/14-theory.md
decisions:
  - id: "06-08-01"
    decision: "TaskFlow (task manager) chosen over e-commerce for capstone complexity"
    rationale: "Task management is familiar, focuses on architecture over domain complexity, easier to complete in 12-16 hours"
  - id: "06-08-02"
    decision: "H2 embedded database instead of PostgreSQL"
    rationale: "Zero-setup requirement; students should not need Docker, external DBs, or cloud accounts to complete capstone"
  - id: "06-08-03"
    decision: "Desktop target demonstrated instead of iOS (alongside Android comments)"
    rationale: "Desktop target (JVM) requires no macOS or Xcode setup; Android target included as comments ready to uncomment"
  - id: "06-08-04"
    decision: "Plain ViewModel pattern instead of AndroidViewModel"
    rationale: "KMP ViewModels must work in commonMain; AndroidViewModel is Android-only and was the core problem with ShopKotlin"
  - id: "06-08-05"
    decision: "Old ShopKotlin completely replaced (not kept as alternative)"
    rationale: "ShopKotlin used Jetpack Compose, AndroidViewModel, PostgreSQL, Stripe -- fundamentally incompatible with KMP teaching goals"
metrics:
  duration: "10 min"
  completed: "2026-02-04"
---

## What Was Done

### Task 1: Design capstone architecture and update M12 capstone lessons

Completely rewrote the M12 L08 capstone lesson from scratch:

**Before**: ShopKotlin -- an Android-only e-commerce platform using Jetpack Compose, AndroidViewModel, PostgreSQL, Stripe payments, Heroku deployment. 14 mostly-empty theory files with minimal code snippets.

**After**: TaskFlow -- a full-stack KMP task management application with complete, runnable code for every layer:

- **Architecture**: `server/` (Ktor 3.4 + Exposed 1.0 + H2) + `shared/` (commonMain @Serializable models) + `composeApp/` (CMP 1.10 + SQLDelight 2.2)
- **Content files**: 01 (project overview) through 14 (key points), each with full code including all imports
- **Technologies**: Every version matches version-manifest.json (Kotlin 2.3.0, Ktor 3.4.0, Exposed 1.0.0, Koin 4.1.0, SQLDelight 2.2.0, CMP 1.10.0)
- **Zero external dependencies**: H2 in-memory database, no Docker/PostgreSQL/cloud required

Updated lesson.json title and estimatedMinutes (30 -> 960 for 12-16 hours). Updated challenge quiz to reference TaskFlow architecture.

### Task 2: Create capstone step-by-step implementation guide

Added TaskDetailScreen and CreateTaskDialog as the 15th content file, completing the implementation guide:

1. **Project Setup** (01-04): Overview, architecture diagram, libs.versions.toml, all build.gradle.kts files
2. **Shared Domain Layer** (05): Priority, TaskStatus, Task, User data classes; AuthRequest, TaskRequest, ApiResponse DTOs
3. **Server Implementation** (06-08): Exposed tables (Users, Tasks), Database plugin (H2 connect + SchemaUtils), JWT Security plugin, UserDao (register/login with bcrypt), TaskDao (CRUD), Auth + Task routes, Koin server module, Application.kt entry point
4. **Client-Side Cache** (09): SQLDelight .sq file with synced column, expect/actual DriverFactory, SyncManager
5. **API Client and Repository** (10): Ktor HttpClient TaskFlowApi, offline-first TaskRepository, Koin client module
6. **Compose UI** (11-12, 15): AuthViewModel, TaskViewModel, LoginScreen, TaskListScreen with TaskCard, PriorityChip, TaskDetailScreen with StatusSelector, CreateTaskDialog, App.kt navigation, Desktop Main.kt
7. **Testing and Running** (13): Server integration tests with testApplication, Gradle run commands for server/desktop/Android
8. **Key Points** (14): How capstone connects to each course module, alternative capstone ideas

All code snippets are complete with package declarations and imports (289+ import statements across 15 files). No abbreviations or ellipses.

## Deviations from Plan

### Auto-fixed Issues

**1. [Rule 1 - Bug] Deleted stale 14-theory.md file**
- **Found during:** Task 1
- **Issue:** After replacing all 14 content files, the old 14-theory.md (Resources with Jetpack Compose links) still existed alongside the new 14-key_point.md
- **Fix:** Deleted 14-theory.md to remove stale content with "Jetpack Compose" link reference
- **Files modified:** content/14-theory.md (deleted)
- **Commit:** b8c9ba78

## Verification Results

| Check | Result |
|-------|--------|
| Uses Compose Multiplatform (not Jetpack Compose) | PASS -- 8 CMP references, 0 Jetpack Compose references |
| KMP structure (shared/, server/, composeApp/) | PASS -- 56 references across 13 files |
| ShopKotlin removed | PASS -- zero references in any file |
| No external service dependencies | PASS -- H2 only, no PostgreSQL/Docker/cloud |
| Code includes all imports | PASS -- 289+ import statements |
| Versions match version-manifest.json | PASS -- all 8 framework versions verified |
| Content file count | 15 files (13 theory + 1 key_point + 1 theory for TaskDetailScreen) |

## Next Phase Readiness

Plan 06-08 delivers the KMP capstone. The remaining plans (06-09 content enrichment, 06-10 final verification) can proceed without blockers.
