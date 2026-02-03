---
phase: 04-csharp-course-audit
plan: 04
subsystem: content
tags: [csharp, dotnet9, aspire, aot, clean-architecture, openapi, scalar, authentication, jwt, oauth, authorization, cicd, github-actions, docker, capstone, shopflow]

# Dependency graph
requires:
  - phase: 04-csharp-course-audit
    provides: "04-01 normalization (filenames, metadata, content type inventory)"
provides:
  - "Modules 16-24 accuracy verified against .NET 9 / C# 13"
  - "14 new WARNING files across M18, M22, M23, M24"
  - "Capstone lesson content confirmed aligned with actual ShopFlow project"
  - "All CI/CD workflows verified (actions v4, .NET 9.0.x, Docker 9.0)"
affects: [04-05-global-verification, future csharp course maintenance]

# Tech tracking
tech-stack:
  added: []
  patterns:
    - ".NET 9 built-in OpenAPI (AddOpenApi/MapOpenApi) replaces Swashbuckle"
    - "Aspire 9.x service discovery and resource orchestration"
    - "Native AOT with PublishAot and source generators"
    - "Clean Architecture four-layer pattern (Domain, Application, Infrastructure, Presentation)"
    - "Policy-based authorization with AddAuthorization"
    - "GitHub Actions v4 with setup-dotnet@v4 and dotnet-version 9.0.x"
    - "Multi-stage Docker builds with mcr.microsoft.com/dotnet/aspnet:9.0"

key-files:
  created:
    - "content/courses/csharp/modules/18-clean-architecture/lessons/01-*/content/05-warning.md"
    - "content/courses/csharp/modules/18-clean-architecture/lessons/02-*/content/05-warning.md"
    - "content/courses/csharp/modules/18-clean-architecture/lessons/04-*/content/05-warning.md"
    - "content/courses/csharp/modules/22-authorization-patterns/lessons/01-*/content/05-warning.md"
    - "content/courses/csharp/modules/22-authorization-patterns/lessons/03-*/content/05-warning.md"
    - "content/courses/csharp/modules/22-authorization-patterns/lessons/04-*/content/05-warning.md"
    - "content/courses/csharp/modules/23-cicd-with-github-actions/lessons/01-*/content/05-warning.md"
    - "content/courses/csharp/modules/23-cicd-with-github-actions/lessons/03-*/content/05-warning.md"
    - "content/courses/csharp/modules/23-cicd-with-github-actions/lessons/04-*/content/05-warning.md"
    - "content/courses/csharp/modules/24-capstone-completion-shopflow-launch/lessons/01-*/content/05-warning.md"
    - "content/courses/csharp/modules/24-capstone-completion-shopflow-launch/lessons/02-*/content/04-warning.md"
    - "content/courses/csharp/modules/24-capstone-completion-shopflow-launch/lessons/03-*/content/05-warning.md"
    - "content/courses/csharp/modules/24-capstone-completion-shopflow-launch/lessons/04-*/content/05-warning.md"
    - "content/courses/csharp/modules/24-capstone-completion-shopflow-launch/lessons/05-*/content/06-warning.md"
  modified: []

key-decisions:
  - "M22 AddAuthorization(options =>) pattern kept as-is (still valid in .NET 9, AddAuthorizationBuilder is newer but both work)"
  - "M24 ANALOGY sections not added (all 5 lessons already have analogies -- no gap)"
  - "No .NET 8 stale references found in M16-24 (zero corrections needed for version drift)"

patterns-established:
  - "WARNING files follow module-specific pitfall patterns (security, deployment, architecture)"

# Metrics
duration: 15min
completed: 2026-02-03
---

# Phase 04 Plan 04: Accuracy Pass Modules 16-24 Summary

**Advanced .NET 9 topics verified (Aspire 9.x, AOT, OpenAPI, Auth, CI/CD) with 14 WARNING files added across M18/M22/M23/M24 and capstone content aligned with actual ShopFlow project structure**

## Performance

- **Duration:** ~15 min
- **Started:** 2026-02-03T22:58:00Z
- **Completed:** 2026-02-03T23:13:40Z
- **Tasks:** 2
- **Files created:** 14

## Accomplishments

- Verified all 42 lessons across Modules 16-24 against .NET 9 / C# 13 with zero inaccuracies found in existing content
- Added 14 WARNING files closing gaps in M18 (3 files), M22 (3 files), M23 (3 files), and M24 (5 files)
- Confirmed capstone ShopFlow project structure matches lesson content (all 9 .csproj target net9.0, CI pipeline uses actions v4)
- Verified .NET 9 built-in OpenAPI (AddOpenApi/MapOpenApi) correctly described as .NET 9 feature in Module 19
- Verified all GitHub Actions workflows use checkout@v4, setup-dotnet@v4, dotnet-version: '9.0.x', Docker 9.0

## Task Commits

Each task was committed atomically:

1. **Task 1: Accuracy pass Modules 16-19** - `b6ab40e7` (fix) -- Aspire 9.x, AOT, Clean Architecture, OpenAPI .NET 9; 3 WARNING files for M18
2. **Task 2: Accuracy pass Modules 20-24** - `68124116` (fix) -- Auth/.NET 9, CI/CD actions v4, capstone alignment; 11 WARNING files for M22/M23/M24

## Files Created

### Module 18 -- Clean Architecture (3 WARNING files, Task 1)

| File | Topics |
|------|--------|
| `M18/L01/.../05-warning.md` | Over-engineering for small projects, architecture astronaut syndrome, premature pattern adoption |
| `M18/L02/.../05-warning.md` | Leaking infrastructure into domain, shared DTOs coupling layers, infrastructure dumping ground, circular dependencies |
| `M18/L04/.../05-warning.md` | Generic repository anti-pattern, ignoring CancellationToken, Unit of Work misuse, DI lifetime errors |

### Module 22 -- Authorization Patterns (3 WARNING files, Task 2)

| File | Topics |
|------|--------|
| `M22/L01/.../05-warning.md` | Authorization vs authentication confusion, hardcoded role strings, over-restrictive policies, policy evaluation order |
| `M22/L03/.../05-warning.md` | Trusting claims without validation, stale claims after role changes, claim type mismatches, token bloat |
| `M22/L04/.../05-warning.md` | Forgetting resource-level checks, N+1 authorization queries, inconsistent logic, missing auth on related endpoints |

### Module 23 -- CI/CD with GitHub Actions (3 WARNING files, Task 2)

| File | Topics |
|------|--------|
| `M23/L01/.../05-warning.md` | No rollback strategy, skipping tests in pipeline, not pinning action versions, CI as afterthought |
| `M23/L03/.../05-warning.md` | SDK image in production, running as root, baked-in secrets, missing health checks, layer caching order |
| `M23/L04/.../05-warning.md` | Production database in staging, missing env-specific config, no approval gates, inconsistent environments |

### Module 24 -- Capstone ShopFlow (5 WARNING files, Task 2)

| File | Topics |
|------|--------|
| `M24/L01/.../05-warning.md` | Capstone scope creep, skipping production checklist, missing environment-specific configuration |
| `M24/L02/.../04-warning.md` | Not validating server-side, returning domain entities from API, missing pagination, forgetting CancellationToken |
| `M24/L03/.../05-warning.md` | Race conditions on stock checks, cart data loss on session expiry, not testing cart changes |
| `M24/L04/.../05-warning.md` | Database migrations without plan, missing transaction boundaries, connection string security, idempotency |
| `M24/L05/.../06-warning.md` | Deploying without monitoring, forgetting HTTPS, no rollback strategy, ignoring logs, not celebrating launch |

## Module-by-Module Accuracy Results

| Module | Lessons | Files Read | Inaccuracies | WARNINGs Added | Notes |
|--------|---------|------------|--------------|----------------|-------|
| M16 Aspire Advanced | 5 | 20 | 0 | 0 | All Aspire 9.x patterns current |
| M17 Native AOT | 5 | 20 | 0 | 0 | AOT patterns accurate for .NET 9 |
| M18 Clean Architecture | 4 | 16 | 0 | 3 | Content accurate; 3/4 lessons missing WARNING |
| M19 OpenAPI/Scalar | 5 | 20 | 0 | 0 | Built-in OpenAPI correctly described as .NET 9 feature |
| M20 Authentication | 5 | ~20 | 0 | 0 | Identity/JWT/refresh patterns current |
| M21 External Auth | 4 | ~16 | 0 | 0 | OAuth/OIDC patterns current |
| M22 Authorization | 4 | ~16 | 0 | 3 | 3/4 lessons missing WARNING |
| M23 CI/CD | 5 | ~20 | 0 | 3 | All actions v4, .NET 9.0.x; 3/5 lessons missing WARNING |
| M24 Capstone | 5 | ~20 | 0 | 5 | All 5 lessons missing WARNING; content matches ShopFlow project |
| **Total** | **42** | **~175** | **0** | **14** | |

## Capstone Content-to-Project Alignment

Verified actual ShopFlow project structure against M24 lesson descriptions:

- **Project structure matches:** ShopFlow.Api, ShopFlow.Application, ShopFlow.Core, ShopFlow.Domain, ShopFlow.Infrastructure, ShopFlow.Web, ShopFlow.Web.Client
- **Test projects match:** ShopFlow.Tests.Unit, ShopFlow.Tests.Integration
- **All 9 .csproj files target net9.0** (confirmed via search)
- **CI pipeline** (`.github/workflows/ci.yml`): uses actions/checkout@v4, setup-dotnet@v4, dotnet-version: '9.0.x', PostgreSQL 16
- **No mismatches found** between lesson content and actual project structure

## Decisions Made

1. **M22 AddAuthorization pattern kept:** Module 22 uses `AddAuthorization(options => ...)` rather than the newer `AddAuthorizationBuilder()`. Both patterns are valid in .NET 9. The traditional pattern is well-documented and widely used. Updating would require rewriting multiple code examples with no correctness benefit.

2. **M24 ANALOGY gap was not real:** Research (04-01) flagged M24 as having 2/5 lessons missing ANALOGY sections. Upon review, all 5 lessons already have analogy content. No additions needed.

3. **Zero version drift in M16-24:** No stale .NET 8 references found anywhere in Modules 16-24. All version references, Docker images, and CI configurations already target .NET 9. This is the cleanest set of modules in the entire course.

## Deviations from Plan

### Auto-fixed Issues

**1. [Rule 2 - Missing Critical] Added 14 WARNING files across 4 modules**

- **Found during:** Tasks 1 and 2
- **Issue:** M18 had 3/4 lessons without WARNING sections. M22 had 3/4 missing. M23 had 3/5 missing. M24 had 5/5 missing. These modules cover production topics (architecture, security, deployment) where pitfall awareness is critical.
- **Fix:** Created 14 WARNING files with module-specific pitfall content covering security, deployment, architecture, and testing risks.
- **Files created:** 14 new `*-warning.md` files (listed above)
- **Verification:** All files follow standard WARNING frontmatter format with actionable pitfall descriptions
- **Committed in:** `b6ab40e7` (Task 1, 3 files) and `68124116` (Task 2, 11 files)

---

**Total deviations:** 1 category (14 files) -- all planned in research findings. WARNING gap closure was an explicit plan objective.
**Impact on plan:** No scope creep. WARNING additions were required by plan specification.

## Issues Encountered

None -- all 175 content files read without issues. Zero inaccuracies found in existing content. All WARNING files created successfully.

## User Setup Required

None - no external service configuration required.

## Next Phase Readiness

- **Plan 04-05 (Global Verification)** is ready to execute
- Remaining known issues for 04-05:
  - 2,427 bin/obj build artifacts in capstone directory need .gitignore + cleanup
  - M14/M16 Aspire content overlap (same APIs taught in both modules)
  - 23 of 24 modules have zero KEY_POINT content (systemic gap, noted but not actionable this cycle)
  - Module hours sum to 58h vs course.json 100h (individual module.json values low)
- All 132 lessons across 24 modules now reviewed (Plans 02 + 03 + 04 = complete coverage)

---
*Phase: 04-csharp-course-audit*
*Completed: 2026-02-03*
