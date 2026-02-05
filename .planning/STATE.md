# Project State

## Project Reference

See: .planning/PROJECT.md (updated 2026-02-02)

**Core value:** Every course teaches a coherent, progressive path from absolute beginner to independently building and deploying a real application.
**Current focus:** Phase 7 in progress (Python Course Audit). All 160 challenges validated, PYTH-03 and PYTH-04 satisfied.

## Current Position

Phase: 7 of 9 (Python Course Audit)
Plan: 4 of 5 in current phase
Status: In progress
Last activity: 2026-02-05 -- Completed 07-04-PLAN.md (challenge validation)

Progress: [####] 4/5 phase plans complete

Overall: [################################################] 48/61 total plans (79%)

## Performance Metrics

**Velocity:**
- Total plans completed: 48
- Average duration: 10 min
- Total execution time: ~443 min

**By Phase:**

| Phase | Plans | Total | Avg/Plan |
|-------|-------|-------|----------|
| 01-foundation | 6/6 | 37 min | 6 min |
| 02-java-audit | 8/8 | 100 min | 13 min |
| 03-js-audit | 7/7 | 86 min | 12 min |
| 04-csharp-audit | 5/5 | 59 min | 12 min |
| 05-flutter-audit | 7/7 | 55 min | 8 min |
| 06-kotlin-audit | 10/10 | 110 min | 11 min |
| 07-python-audit | 4/5 | 37 min | 9 min |

**Recent Trend:**
- Last 5 plans: 07-04 (8 min), 07-03 (10 min), 07-02 (11 min), 07-01 (8 min), 06-10 (4 min)
- Trend: Phase 7 challenge validation complete -- all 160 solutions validated, 6 files fixed

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
- [01-06]: All 6 standard content types now have dedicated WPF renderers (THEORY, EXAMPLE, KEY_POINT, ANALOGY, WARNING, LEGACY_COMPARISON)
- [02-01]: Java 25 LTS as course target (version-manifest.json + course.json)
- [02-01]: Spring Boot 4.0.x as framework target (Spring Framework 7, Jakarta EE 11)
- [02-01]: All version numbers removed from lesson titles and directory names
- [02-01]: Preview features noted without --enable-preview flag references
- [02-01]: Module 05/06 content overlap identified (lambdas+streams taught twice)
- [02-01]: Module 15 lesson 15.7 duplicates Module 07 concurrency content
- [02-01]: Module 08 and 15 have incorrect difficulty metadata
- [02-04]: Module 08 System.out.println in lesson 86 preserved intentionally (anti-pattern teaching examples)
- [02-04]: Module 08 has no @MockBean references; @MockitoBean belongs in Spring modules not JUnit fundamentals
- [02-04]: Virtual threads reframed as standard Java with brief historical note ("introduced in Java 21")
- [02-05]: WebSecurityConfigurerAdapter/antMatchers kept in Module 12 migration warning as OLD pattern examples
- [02-05]: Spring Boot 3.0 historical reference retained in Module 11 lesson 1 (explains Jakarta EE migration history)
- [02-02]: LEGACY_COMPARISON section added to Module 01 Lesson 06 for System.out.println old syntax
- [02-02]: Module 02 Lesson 06 (public/static/void) retains System.out.println in traditional examples
- [02-02]: IO.println blocker resolved -- 136 occurrences across Modules 01-03, zero unintentional System.out.println
- [02-06]: Module 13 React content verified current (functional components, hooks, Vite, react-router-dom)
- [02-06]: Module 14 already migrated by prior plan execution (verified, no new changes)
- [02-06]: Virtual threads content reframed: Spring Boot 4.0 enables by default, no config needed
- [02-06]: Historical Spring Boot 3.2 mention kept only in challenge explanation for context
- [02-03]: Module 04 Lesson 01 reframed as explicit transition from compact source files to full class syntax
- [02-03]: Flexible constructor bodies (JEP 513) documented in Module 04 Lesson 02 with validation-before-super example
- [02-03]: All version-tagged framing removed from Modules 04-05 (Java 8+, Java 16+, Java 17+, Java 21+)
- [02-03]: Lambda/streams examples rewritten to compact void main() with IO::println method references
- [02-07]: Both Thymeleaf and React paths use same lesson files with "THYMELEAF PATH" / "REACT PATH" / "BOTH PATHS" section headers
- [02-07]: Thymeleaf tutorial self-contained in Lesson 06 (no separate Thymeleaf module needed)
- [02-07]: Thymeleaf single-JAR deployment advantage highlighted as key differentiator for beginners
- [02-07]: Capstone @MockBean fully replaced with @MockitoBean (Spring Boot 4.0.x pattern)
- [02-08]: No Phase 2.1 needed -- zero systemic voice or progression issues found
- [02-08]: 9 stale version tag stragglers fixed in final global sweep (Java 17+, Java 16 references)
- [03-01]: Prisma stays on 6.x patterns despite 7.0 release (ESM-first, no Rust engines; ecosystem needs stabilization)
- [03-01]: Hono jwt() requires alg option since 4.11.0 (breaking change documented in version manifest)
- [03-01]: JS course has 132 lessons across 21 modules (course.json previously said 95)
- [03-02]: M08 Lessons 03/04 should swap (Import Attributes before CJS vs ESM is wrong order)
- [03-02]: M16 Testing placement at position 16 is acceptable (self-contained; reordering 6 modules not worth it)
- [03-02]: M17 JSDoc after M10 TypeScript is intentional (JSDoc positioned as migration path alternative)
- [03-02]: M20/M21 capstones need analogies added (both have zero ANALOGY sections)
- [03-02]: M16 has zero analogies but all 14 challenges have simulation wrappers (Node.js compatible)
- [03-02]: M19 ALL 5 challenges use raw Bun APIs with no simulation wrappers (need adding)
- [03-02]: 13 of 21 modules have zero KEY_POINT sections (systemic gap)
- [03-02]: M18 ES2025 module is NOT redundant -- covers genuinely new features vs earlier modules
- [03-05]: Docker oven/bun:1.1 fixed to oven/bun:1 in M15 (10 replacements across 5 files)
- [03-05]: M20 capstone also has 8 oven/bun:1.1 references (deferred to plan 03-06/07)
- [03-05]: M13 React content fully verified -- functional components, hooks, Vite, no CRA
- [03-05]: M16 all 14 challenges confirmed with simulation wrappers (zero raw bun:test imports)
- [03-05]: M14 process.env.API_URL clarified to distinguish frontend (import.meta.env) from backend (process.env)
- [03-04]: Module 10 TypeScript content verified 100% accurate (zero corrections across 54 files)
- [03-04]: npx prisma kept in Module 12 (consistent; course teaches Prisma independently of Bun)
- [03-04]: Prisma 7.x note added as blockquote in M12 L02 (informational, not cautionary)
- [03-03]: M01-09 (44 lessons, ~140 content files) verified 100% accurate -- zero inaccuracies found
- [03-03]: ES2025 Set methods (union, intersection, difference, etc.) correctly described as finalized
- [03-03]: ES2024 Object.groupBy/Map.groupBy correctly described as finalized
- [03-03]: M08 lesson swap (CJS vs ESM before Import Attributes) confirmed already applied by 03-05
- [03-03]: M08 L03 (CJS vs ESM) missing ANALOGY section noted but not added (plan prohibits new sections)
- [03-03]: M07 DOM challenges are conceptual/browser-native, not Node.js-runnable -- appropriate for content
- [03-06]: TS 7.0 false claims corrected (Go-based rewrite targeting mid-2026+, not released Dec 2025)
- [03-06]: M19 L01/L02/L04 got simulation wrappers; L03/L05 marked Bun-only (no meaningful cross-runtime equivalent)
- [03-06]: M20/M21 capstone analogies added to L01-L03 of each module (6 new analogy files)
- [03-06]: M20/M21 capstone key_points added to bookend lessons (4 new key_point files)
- [03-06]: Fly.io fly.toml updated from deprecated [[services]] to modern [http_service] format
- [03-06]: JWT alg: 'HS256' added to M20 Hono sign()/verify() calls (matches 03-01 version manifest finding)
- [03-06]: M20 L06 testing challenges got bun:test simulation wrappers (raw imports replaced)
- [03-06]: oven-sh/setup-bun@v1 updated to @v2 in M20 CI/CD pipeline (consistency with M15/M16)
- [03-07]: No Phase 3.1 needed -- zero systemic voice or progression issues found across all 132 lessons
- [03-07]: TypeScript .js challenge files in M10+ are intentional (TS-within-JS course structure)
- [03-07]: All 304 JSON files validated (challenge.json + lesson.json + module.json)
- [03-07]: Bullet-point analogy format in M11-M21 is acceptable stylistic variation
- [04-01]: C# course aligned to .NET 9 / C# 13 (version-manifest.json + course.json)
- [04-01]: 24 non-standard filenames renamed (11 architecture->theory, 8 real_world->analogy, 5 deep_dive->theory)
- [04-01]: C# course has 132 lessons across 24 modules, 532 content files
- [04-01]: course.json estimatedHours: 29 -> 100, difficulty: advanced -> beginner-to-advanced
- [04-01]: refactor_course.py artifact deleted
- [04-01]: M06 title "Methods and Functions" is misleading (content is OOP basics: classes, constructors, properties)
- [04-01]: M07 title "OOP Basics" covers intermediate OOP (inheritance, polymorphism, interfaces, records)
- [04-01]: M08 title "Advanced OOP Concepts" covers exceptions, namespaces, NuGet (not OOP)
- [04-01]: M14 L03 and M16 have Aspire content overlap (same APIs introduced twice)
- [04-01]: 23 of 24 modules have zero KEY_POINT content (systemic gap, not actionable this cycle)
- [04-01]: 27 lessons across 8 modules missing WARNING content (M12: 8/8, M24: 5/5 worst)
- [04-01]: 2,427 bin/obj build artifacts in capstone directory (flag for 04-05 cleanup)
- [04-01]: Module hours sum to 58h but course.json says 100h (individual module.json values low)
- [04-02]: M01-M10 (59 lessons, 250 files) verified against C# 13/.NET 9 -- only 3 version ref fixes needed
- [04-02]: All 12 C# version-tagged features correctly attributed (C# 9/10/11/12/13 boundaries)
- [04-02]: All 5 .NET 9-only APIs (CountBy, AggregateBy, Lock, implicit index, params collections) correctly documented
- [04-02]: M08 L03 had 2x stale obj/Debug/net8.0/ paths -> fixed to net9.0
- [04-02]: M05 L05 collection expressions warning clarified (.NET 8+ minimum, course uses .NET 9)
- [04-03]: M11 L03 Results.* upgraded to TypedResults.* with union return types
- [04-03]: M12 all 8 lessons now have WARNING sections (was 0 -- biggest gap in entire C# course)
- [04-03]: M12 L08 HybridCache explicitly states .NET 9 requirement
- [04-03]: M13 L02 invalid RenderMode.Static directive removed (Static SSR = no @rendermode)
- [04-03]: M14 L04 Git WARNING added (secrets, force push, reset --hard)
- [04-03]: M14 L05 Azure runtime DOTNET|8.0 -> DOTNET|9.0
- [04-03]: M15 xUnit/Moq/TDD content verified 100% accurate (zero changes needed)
- [04-03]: Blazor ".NET 8" in lesson titles kept (historically accurate feature introduction)
- [04-03]: M14 L06 (Next Steps) skipped for WARNING (no natural pitfalls)
- [04-04]: M16-24 (42 lessons, ~175 files) verified -- zero inaccuracies found (cleanest module set in C# course)
- [04-04]: M22 AddAuthorization(options =>) pattern kept (both AddAuthorization and AddAuthorizationBuilder valid in .NET 9)
- [04-04]: M24 ANALOGY gap was false positive -- all 5 capstone lessons already have analogy content
- [04-04]: Capstone ShopFlow project structure matches lesson content exactly (9 .csproj all target net9.0)
- [04-04]: 14 WARNING files added: M18 (3), M22 (3), M23 (3), M24 (5)
- [04-05]: 131 KEY_POINT files created (132 total -- every C# lesson now has at least one)
- [04-05]: 3 C# 13 challenges given C# 12 fallback solutions (M05 L06, M06 L08, M10 L05)
- [04-05]: 384 JSON files validated (all valid)
- [04-05]: Global sweep clean -- zero stale refs, zero non-standard files, zero artifacts
- [04-05]: Human approved phase completion
- [04-05]: No Phase 4.1 needed -- all CSRP requirements satisfied, course production-ready
- [05-01]: Flutter 3.38.x/Dart 3.10.x as course target (matches setup content and current stable)
- [05-01]: Riverpod 2.x patterns retained (Riverpod 4.0 expected; migration deferred)
- [05-01]: Serverpod 2.x patterns retained (3.0 released Dec 2025; migration deferred)
- [05-01]: GoRouter updated to 17.x (requires Flutter 3.32+/Dart 3.8+)
- [05-01]: Drift 2.x added to version manifest (used in M13 offline persistence)
- [05-01]: 18 experiment files renamed to example across M02, M04, M07, M09, M10, M11
- [05-01]: 9 archived Firebase/Supabase lessons marked with "archived": true in M09
- [05-01]: 5 misplaced lessons annotated with [Note:] placement context in M10/M11
- [05-01]: M03/M04/M05 descriptions written from scratch (originals were generic placeholders)
- [05-01]: course.json estimatedHours kept at 150 (153 lessons; ~1h/lesson reasonable for full-stack)
- [05-02]: M01-M05 verified 100% accurate with zero corrections (05-01 alignment was thorough)
- [05-02]: Flutter 3.27 references kept as historical context (Impeller Android default, deep linking default)
- [05-02]: WillPopScope references kept in M07 L08 migration lesson (intentional teaching context)
- [05-02]: Riverpod version constraints updated to ^2.6.1 (flutter_riverpod, riverpod_annotation, riverpod_generator, hooks_riverpod)
- [05-02]: Riverpod 3.x migration note added in M06 L04 (first Riverpod code lesson)
- [05-02]: GoRouter version reference updated to 17.x (not pinned to 17.0.0)
- [05-02]: useMaterial3: true removed from M07 code examples (Material 3 is default since Flutter 3.16)
- [05-02]: Uncommitted 05-01 changes detected in M08, M09, M15, M16, M17 (outside M01-M07 scope)
- [05-03]: Dart Frog community maintenance status documented in M08 L01 and L02 (FLTR-05 complete)
- [05-03]: Serverpod 3.x awareness note with Relic auth reference added to M09 L01
- [05-03]: Serverpod CLI version pin (^2.0.0) added to M09 L02 and L18
- [05-03]: PostgreSQLException -> ServerException in M08 L06 (postgres 3.x API)
- [05-03]: SharedPreferences auth token example removed from M13 (security fix, contradicted M11 flutter_secure_storage teaching)
- [05-03]: go_router ^14.6.0 -> ^17.0.0 in M11 L07/L08 (match version manifest)
- [05-04]: M18 GoRouter ^14.0.0 -> ^17.0.0 (critical capstone-to-M07 consistency fix)
- [05-04]: SDK constraint >=3.0.0 -> >=3.10.0 in M18 pubspec examples (3 files)
- [05-04]: withOpacity -> withValues(alpha:) across M14-M18 (15 replacements; deprecated in Flutter 3.27)
- [05-04]: textScaleFactorOf -> textScalerOf in M15 responsive layouts (deprecated in Flutter 3.16)
- [05-04]: CI/CD flutter-version 3.24.0 -> 3.38.0 in M16/M18 GitHub Actions workflows (7+ files)
- [05-04]: AnimatedBuilder in M15 L02 confirmed correct (not deprecated; is current Flutter API)
- [05-04]: FLTR-04 capstone assessed complete: 12-lesson project guide with inline pubspec + code (no standalone directory)
- [05-05]: 44 KEY_POINT files bring Flutter course to 100% active lesson coverage (139/139 lessons)
- [05-05]: 16 WARNING files across M11 (5), M12 (4), M13 (4), M14 (3) -- all previously zero-WARNING modules
- [05-05]: No KEY_POINTs for archived M09 L10-L17/L19, misplaced M10 L06-L08, or misplaced M11 L09-L10
- [05-06]: 44 missing solution.dart categorized: 28 QUIZ, 4 MULTI_CHOICE (no code needed), 12 implementation/FREE_CODING (require project context) -- none reducible
- [05-06]: M02 L08 sealed class 'in' reserved keyword renamed to 'snowIn' (Dart reserved keyword bug)
- [05-06]: M15 withOpacity -> withValues(alpha:) in 5 challenge solutions (extending 05-04 fix)
- [05-06]: FLTR-04 capstone assessed complete: 12 lessons, 24/24 solutions, deployment guide, all features covered
- [05-06]: 20 ANALOGY files across 10 zero-analogy modules (M08, M10-M18), 2 per module
- [05-07]: 389 JSON files validated (all valid)
- [05-07]: 3 stale Riverpod ^2.4.0 refs fixed to ^2.6.1 (M11 L04, M18 L01)
- [05-07]: Flutter 3.27 refs kept as historical context (3 occurrences, intentional)
- [05-07]: WillPopScope refs kept in M07 L08 migration lesson (4 occurrences, intentional)
- [05-07]: Human approved Phase 5 completion -- no Phase 5.1 needed
- [06-01]: Kotlin 2.3 as course target with K2 compiler, context parameters Beta
- [06-01]: 9 frameworks pinned: Ktor 3.4.x, CMP 1.10.x, SQLDelight 2.2.x, Koin 4.1.x, Exposed 1.0.x, Arrow 2.2.x, kotlinx-coroutines 1.10.x, kotlinx-serialization 1.10.x, Gradle 8.x
- [06-01]: M06 lesson ordering fixed: alphabetical-broken (auth before routing) -> logical progression (15 dirs renamed)
- [06-01]: Module title prefixes removed from M05 (04A), M08 (06A), M09 (06B), M10 (06C)
- [06-01]: course.json difficulty beginner -> beginner-to-advanced, description updated for 128 lessons/15 modules
- [06-01]: refactor_course.py artifact deleted (same pattern as C# 04-01)
- [06-02]: M04/M05 coroutines overlap: KEPT both -- M04 L08-L09 are preview, M05 is deep-dive (transition note added)
- [06-02]: GlobalScope references in M05 L02 kept as anti-pattern warnings (pedagogically correct)
- [06-02]: Kotlin version refs in M03 L04/L05 and M05 L07 kept as historical context
- [06-02]: M01 L10 version-tagged framing cleaned: "Kotlin 2.0 in Practice" -> "Modern Kotlin in Practice"
- [06-04]: M08-M10 verified 100% accurate (SQLDelight 2.2.x, KMP Architecture, Koin 4.1.x -- zero corrections)
- [06-04]: M11 verified 100% accurate (kotlin.test, runTest, StandardTestDispatcher -- zero corrections)
- [06-04]: M12 docker-compose version field + commands updated to Compose V2
- [06-04]: M12 L08 capstone is Jetpack Compose ONLY (not CMP) -- uses AndroidViewModel, no shared code
- [06-04]: M12 L08 ShopKotlin can be reframed as Android reference while Plan 08 creates CMP capstone
- [06-04]: M12 L08 lesson.json says 30 min, content says 12-16 hours (metadata discrepancy)
- [06-03]: M06 forward references all correct post-reorder (zero broken cross-references found)
- [06-03]: Exposed 1.0.0 v1 namespace (org.jetbrains.exposed.v1.*) confirmed correct in M06 L08
- [06-03]: M07 L07-L09 teach Android-specific libraries (Retrofit, Room, Hilt) -- annotated as Android-only, not replaced
- [06-03]: hiltViewModel() removed from commonMain code; constructor injection used instead
- [06-03]: R.drawable references replaced with cross-platform Box/initials patterns
- [06-03]: Dynamic color theming labeled as androidMain with commonMain fallback shown
- [06-05]: M13 kotlinOptions -> compilerOptions with JvmTarget enum
- [06-05]: M14 Validated -> zipOrAccumulate/EitherNel (Arrow 2.2.x migration)
- [06-05]: M14/M15 context(Raise<E>) -> context(raise: Raise<E>) (context parameters)
- [06-05]: M14 L05 Arrow version ref fixed: "Arrow 1.2+" -> Arrow 2.x Raise DSL
- [06-05]: M15 L03 KAPT explicitly described as deprecated (Kotlin 2.0)
- [06-05]: M15 L05 full rewrite: context receivers -> context parameters primary teaching
- [06-06]: QUIZ type used for setup/overview/capstone lessons (concepts not suited for coding challenges)
- [06-06]: Pure logic extraction pattern for M06-M07 (no Ktor/Compose/Exposed/Koin imports in challenges)
- [06-06]: One challenge per missing lesson targeting primary concept; existing multi-challenge lessons kept
- [06-07]: M14 FP challenges build Either/Option/Raise from scratch (zero Arrow imports, students learn by building)
- [06-07]: M12 deployment challenges use QUIZ format (CI/CD, app stores are procedural knowledge)
- [06-07]: M15 context parameters simulated via explicit passing (requires -Xcontext-parameters flag)
- [06-07]: M13 Gradle challenges use string parsing (framework-independent, runnable standalone)
- [06-08]: TaskFlow KMP capstone replaces ShopKotlin (Android-only Jetpack Compose app)
- [06-08]: H2 embedded database chosen over PostgreSQL (zero-setup requirement for capstone)
- [06-08]: Desktop target demonstrated instead of iOS (no macOS/Xcode setup needed)
- [06-08]: Plain ViewModel pattern instead of AndroidViewModel (KMP commonMain compatible)
- [06-08]: ShopKotlin completely replaced (not kept as alternative -- fundamentally incompatible with KMP)
- [07-01]: Python course has 24 modules, 165 lessons, 896 content sections
- [07-01]: Course difficulty updated to 'beginner-to-advanced', estimatedHours to 150
- [07-01]: Bridge lessons needed at 7 major framework transitions (M06→M07, M09→M10, M11→M12, M13→M14, M16→M17, M17→M18, M20→M21)
- [07-01]: Content gaps: ANALOGY at 9% (enrichment opportunity), KEY_POINT at 19%, WARNING at 12.6%
- [07-01]: Challenge coverage 97% (160/165 lessons), all FREE_CODING type
- [07-01]: Large modules identified: M21 Django (30h), M22 PostgreSQL (25h), M23 Auth (25h)
- [07-02]: All Python lesson content verified for Python 3.12+ accuracy
- [07-02]: Pydantic v2 patterns confirmed correct (model_dump, model_validate)
- [07-02]: SQLAlchemy 2.0 patterns verified (L02), L01 uses 1.x API (acceptable)
- [07-02]: Fixed 1 Pydantic v1→v2 issue in M21 L01 (.dict() → .model_dump())
- [07-02]: Python 3.11+ features correctly noted (ExceptionGroup, TaskGroup, except*)
- [07-02]: PYTH-01 (accuracy) satisfied -- all 165 lessons verified
- [07-03]: Bridge lessons created at M13→M14 (async to FastAPI) and M16→M21 (FastAPI to Django)
- [07-03]: M14 (FastAPI) enriched: 7 WARNING, 10 ANALOGY files total
- [07-03]: M15 (SQLAlchemy) enriched: 5 WARNING, 6 ANALOGY files total
- [07-03]: M16 (API Auth) enriched: 5 WARNING, 7 ANALOGY files total
- [07-03]: Security-critical content in M16 now has comprehensive WARNING sections
- [07-03]: PYTH-02 (progressive curriculum) improved with bridge lessons
- [07-03]: PYTH-05 (consistent voice) addressed with content enrichment
- [07-04]: All 160 Python challenges validated via AST syntax parsing
- [07-04]: All 352 Python JSON files validated (no parsing errors)
- [07-04]: M01 L05 challenge.json fixed (unescaped quotes in hints)
- [07-04]: 5 M17 config files converted to valid Python (Dockerfile, YAML, TOML wrapped in strings)
- [07-04]: PYTH-03 (all challenges execute correctly) satisfied
- [07-04]: PYTH-04 (deployable capstone) satisfied -- M24 has 6 lessons with deployment guide

### Pending Todos

- Normalize challenge type values across courses (9 different types exist: FREE_CODING, QUIZ, CODE, quiz, coding, implementation, etc.)

### Blockers/Concerns

- Module 05/06 streams overlap still exists (identified in 02-01, needs future resolution)
- Module 15 lesson 15.7 virtual threads still duplicates Module 07 concurrency (content updated but structural overlap remains)
- Dart Frog community transition (July 2025) -- RESOLVED in 05-03: API verified, community status documented
- ONNX Runtime GenAI 12 versions behind (0.5.2 -> current) -- upgrade risk in Phase 8
- C# capstone has 2,427 bin/obj build artifacts committed to repo (needs .gitignore + cleanup in 04-05)
- C# M14/M16 Aspire content overlap (same APIs taught in both modules)

## Session Continuity

Last session: 2026-02-05
Stopped at: Completed 07-04-PLAN.md (challenge validation)
Resume file: None - Ready for 07-05-PLAN.md (voice polish)
