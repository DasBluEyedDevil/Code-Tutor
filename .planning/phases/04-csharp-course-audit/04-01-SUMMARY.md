# Phase 4 Plan 1: Version Alignment and Structural Foundation Summary

**One-liner:** .NET 9/C# 13 version alignment, 24 non-standard filenames renamed, course metadata updated, structural review of 24-module progression.

---

## Changes Made

### Version Alignment

**version-manifest.json (C# section):**
- `runtime.version`: "8.0" -> "9.0"
- `runtime.versionType`: "LTS" -> "STS"
- `runtime.languageVersion`: "C# 12" -> "C# 13"
- `runtime.notes` added: "Course written for .NET 9/C# 13. Both .NET 8 and .NET 9 EOL November 2026. .NET 10 LTS available; upgrade planned for future cycle."
- `runtime.lastVerified`: "2026-02-03"
- ASP.NET Core: "8.0" -> "9.0" with updated notes (MapOpenApi, Minimal API patterns)
- Entity Framework Core: "8.0" -> "9.0" with updated notes (HybridCache, compiled models, code-first migrations)
- Blazor: "8.0" -> "9.0" with updated notes (Server and WebAssembly rendering modes)
- xUnit: kept "2.x" (no change)
- .NET Aspire: kept "9.x" with updated notes (Aspire 13.0 for .NET 10 noted; course targets 9.x)
- All framework `lastVerified` dates updated to 2026-02-03

**course.json:**
- `minimumRuntimeVersion`: ".NET 8.0" -> ".NET 9.0"
- `estimatedHours`: 29 -> 100
- `difficulty`: "advanced" -> "beginner-to-advanced"
- `description`: Updated to comprehensive description mentioning .NET 9, C# 13, 132 lessons, 24 modules, and ShopFlow capstone

### Filename Migration (24 files)

| Rename Type | Count | Modules Affected |
|---|---|---|
| `*-architecture.md` -> `*-theory.md` | 11 | M02, M03, M05, M07, M18 (x2), M20, M21, M22, M23, M24 |
| `*-real_world.md` -> `*-analogy.md` | 8 | M01, M02, M03, M06, M18, M24 (x3) |
| `*-deep_dive.md` -> `*-theory.md` | 5 | M01, M04, M18, M20, M24 |
| **Total** | **24** | |

All renames used `git mv` to preserve history. No naming conflicts encountered. No frontmatter changes required (all files already had correct type values in frontmatter).

### Artifact Deletion

- `content/courses/csharp/refactor_course.py` deleted via `git rm` (Python build script, no longer needed)

---

## Structural Review Findings

### Course Overview

- **24 modules, 132 lessons, 532 content files**
- **Module hours sum: 58 hours** (individual module.json values) vs **course.json: 100 hours** (includes self-study, exercises, capstone work)
- **Content types present:** THEORY, EXAMPLE, ANALOGY, WARNING (4 of 6 standard types)
- **Content types missing:** KEY_POINT (23/24 modules have zero), LEGACY_COMPARISON (not applicable for this course)

### Module Progression Assessment (23 transitions)

| Transition | Assessment | Notes |
|---|---|---|
| M01 -> M02 | SMOOTH | Getting Started -> Variables: natural first step |
| M02 -> M03 | SMOOTH | Variables -> Control Flow: uses variables in conditions |
| M03 -> M04 | SMOOTH | Control Flow -> Loops: iteration follows branching |
| M04 -> M05 | SMOOTH | Loops -> Collections: iterating needs things to iterate over |
| M05 -> M06 | ACCEPTABLE | Collections -> "Methods and Functions" (actually OOP intro): M06 title is misleading -- content is classes, constructors, properties. The progression from data structures to objects is reasonable but the module title is confusing |
| M06 -> M07 | SMOOTH | OOP intro -> OOP continuation (inheritance, polymorphism): logical progression |
| M07 -> M08 | ACCEPTABLE | OOP advanced -> Exceptions/namespaces/NuGet: M08 title "Advanced OOP Concepts" is misleading -- content covers error handling and project organization, not advanced OOP |
| M08 -> M09 | SMOOTH | Error handling -> LINQ: LINQ benefits from understanding collections (M05) and methods (M06) |
| M09 -> M10 | SMOOTH | LINQ -> Async: both are advanced C# language features |
| M10 -> M11 | MODERATE JUMP | Pure C# -> ASP.NET Core web framework. M11 L01 introduces web concepts well. Students need the async knowledge from M10 for API handlers. Jump is managed |
| M11 -> M12 | SMOOTH | Web APIs -> Databases: APIs need data persistence |
| M12 -> M13 | SMOOTH | Databases -> Blazor UI: backend complete, now frontend |
| M13 -> M14 | SMOOTH | Blazor basics -> Blazor + Aspire + deployment: extends Blazor skills |
| M14 -> M15 | ACCEPTABLE | Blazor/Aspire/deploy -> Testing: testing after building real apps is intentional (test what you've built). Slightly unusual ordering but pedagogically sound |
| M15 -> M16 | OVERLAP CONCERN | Testing -> Aspire deep dive: M14 L03 already introduced Aspire basics. M16 goes deeper into service discovery, observability, resilience. Content overlap exists but M16 is deeper |
| M16 -> M17 | ACCEPTABLE | Aspire -> Native AOT: AOT is performance optimization, natural after understanding deployment |
| M17 -> M18 | MODERATE JUMP | Native AOT -> Clean Architecture: jump from low-level optimization to high-level architecture. Could benefit from a bridge but each is self-contained |
| M18 -> M19 | SMOOTH | Clean Architecture -> Modern APIs with OpenAPI: applies clean architecture to API design |
| M19 -> M20 | SMOOTH | API documentation -> Authentication: securing the APIs you documented |
| M20 -> M21 | SMOOTH | Auth fundamentals -> External auth providers: builds on M20's foundation |
| M21 -> M22 | SMOOTH | External auth -> Authorization patterns: authentication before authorization is correct |
| M22 -> M23 | SMOOTH | Authorization -> CI/CD: automating quality gates after securing the app |
| M23 -> M24 | SMOOTH | CI/CD -> Capstone: final project draws on everything |

### Key Structural Concerns

**1. Module Title Mismatches (flag for accuracy passes)**
- M06 "Methods and Functions" -- actual content is OOP basics (classes, constructors, properties, methods-as-members)
- M07 "OOP Basics" -- actual content is intermediate OOP (inheritance, polymorphism, interfaces, records)
- M08 "Advanced OOP Concepts" -- actual content is exceptions, namespaces, NuGet packages

**2. Aspire Content Overlap (M14 L03 vs M16)**
- M14 L03 introduces `DistributedApplication.CreateBuilder()`, `AddProject<T>()`, `WithReference()`, `AddRedis/Postgres()`, `AddServiceDefaults()`
- M16 L01 re-introduces the exact same APIs with slightly different examples
- Impact: Students see nearly identical code explanations in both modules
- Recommendation: M14 L03 should be a brief preview; M16 should be the authoritative Aspire module

**3. KEY_POINT Content Type Completely Missing**
- 23 of 24 modules have zero KEY_POINT files across all lessons
- Only M14 L03 has a single key_point file (out of 132 lessons)
- This is a systemic gap unlike the JS/Java courses
- Recommendation: Not actionable in this audit cycle (adding 132 key_point files is scope creep)

**4. Capstone bin/obj Artifacts in Repository**
- 2,427 compiled files found under `content/courses/csharp/capstone/src/*/bin/` and `content/courses/csharp/capstone/src/*/obj/`
- These are build artifacts that should not be in version control
- Recommendation: Flag for Plan 04-05 (capstone build verification) to clean up with `.gitignore`

### WARNING Content Gaps

| Module | Lessons Missing WARNING | Total Lessons | Gap % |
|---|---|---|---|
| M12 File I/O, Databases & Caching | 8 | 8 | 100% |
| M14 Blazor, .NET Aspire & Deployment | 2 | 6 | 33% |
| M18 Clean Architecture | 3 | 4 | 75% |
| M20 Authentication Fundamentals | 1 | 5 | 20% |
| M21 External Auth Providers | 2 | 4 | 50% |
| M22 Authorization Patterns | 3 | 4 | 75% |
| M23 CI/CD with GitHub Actions | 3 | 5 | 60% |
| M24 Capstone Completion | 5 | 5 | 100% |
| **Total** | **27** | **132** | **20%** |

Note: M12 and M24 have zero WARNING content across all lessons. These modules deal with databases and deployment respectively -- areas where warnings about common pitfalls would be valuable.

### ANALOGY Content Gaps

No modules are completely missing ANALOGY content. All 132 lessons have at least one analogy file.

### Estimated Hours Recommendations

| Module Group | Current Sum | Recommended Range | Rationale |
|---|---|---|---|
| M01-M04 (Fundamentals) | 8h | 8-10h | Current values reasonable for beginners |
| M05-M09 (Core C#) | 9h | 12-18h | M05 (1h for 6 lessons on collections) is too low; M09 (LINQ, 7 lessons) at 2h is too low |
| M10 (Async) | 1h | 3-4h | Async is a notoriously difficult topic; 5 lessons including thread safety deserves more |
| M11-M14 (Web/Blazor) | 8h | 14-20h | New frameworks require setup time; M12 (8 lessons on DB/caching) at 2h is very low |
| M15-M17 (Testing/Aspire/AOT) | 6h | 8-12h | Specialized topics with significant tooling setup |
| M18-M22 (Architecture/Auth) | 17h | 16-20h | Current values mostly reasonable |
| M23 (CI/CD) | 4h | 4-5h | Appropriate |
| M24 (Capstone) | 6h | 8-10h | Full project assembly and deployment needs more time |
| **Total** | **58h** | **80-100h** | Course.json says 100h; module sum should be closer |

### Capstone Project Structure Assessment

**ShopFlow Capstone: COMPLETE**

| Component | Status | Path |
|---|---|---|
| Solution file | Present | `capstone/ShopFlow.sln` |
| ShopFlow.Api | Present | `capstone/src/ShopFlow.Api/` |
| ShopFlow.Application | Present | `capstone/src/ShopFlow.Application/` |
| ShopFlow.Core | Present | `capstone/src/ShopFlow.Core/` |
| ShopFlow.Domain | Present | `capstone/src/ShopFlow.Domain/` |
| ShopFlow.Infrastructure | Present | `capstone/src/ShopFlow.Infrastructure/` |
| ShopFlow.Web | Present | `capstone/src/ShopFlow.Web/` |
| ShopFlow.Web.Client | Present | `capstone/src/ShopFlow.Web.Client/` |
| Unit Tests | Present | `capstone/tests/ShopFlow.Tests.Unit/` |
| Integration Tests | Present | `capstone/tests/ShopFlow.Tests.Integration/` |
| CI Pipeline | Present | `capstone/.github/workflows/ci.yml` |
| dotnet-tools.json | Present | `capstone/.config/dotnet-tools.json` |
| README | MISSING | No README.md found |

CI pipeline targets .NET 9.0.x with PostgreSQL 16 service container. Runs both unit and integration tests.

---

## Issues Flagged for Downstream Plans

### For Plan 04-02 (Modules 01-08 Accuracy Pass)
- M06 title "Methods and Functions" is misleading (content is OOP basics)
- M07 title "OOP Basics" covers inheritance/polymorphism (intermediate level)
- M08 title "Advanced OOP Concepts" covers exceptions/NuGet (not OOP)
- M05 estimatedHours: 1 -> recommend 2-3 (6 lessons on collections)
- M06/M07/M08 difficulty all say "beginner" -- M07/M08 should be "intermediate"

### For Plan 04-03 (Modules 09-16 Accuracy Pass)
- M10 estimatedHours: 1 -> recommend 3-4 (async + thread safety)
- M12 has zero WARNING content across 8 lessons (database/caching content)
- M14/M16 Aspire content overlap needs review
- M14 L03 and M16 L01 teach nearly identical Aspire API surface

### For Plan 04-04 (Modules 17-24 Accuracy Pass)
- M18-M24 WARNING gaps (27 total missing across these modules)
- M24 has zero WARNING content (deployment/production concerns)
- M24 capstone lessons should reference the actual ShopFlow solution structure

### For Plan 04-05 (Capstone Build Verification)
- 2,427 bin/obj artifacts need cleanup (.gitignore addition)
- No README.md in capstone directory
- CI pipeline uses .NET 9.0.x (correct for current version target)
- Build verification needed: `dotnet build` and `dotnet test`

---

## Deviations from Plan

None -- plan executed exactly as written.

---

## Metrics

- **Duration:** ~4 minutes
- **Completed:** 2026-02-03
- **Files modified:** 2 (version-manifest.json, course.json)
- **Files renamed:** 24 (11 architecture, 8 real_world, 5 deep_dive)
- **Files deleted:** 1 (refactor_course.py)
- **Total content files in course:** 532
- **Total lessons:** 132
- **Commits:** 1 (feat: version alignment + renames + metadata + artifact cleanup)
