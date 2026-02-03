# Project State

## Project Reference

See: .planning/PROJECT.md (updated 2026-02-02)

**Core value:** Every course teaches a coherent, progressive path from absolute beginner to independently building and deploying a real application.
**Current focus:** Phase 3 in progress (JavaScript Course Audit)

## Current Position

Phase: 3 of 9 (JavaScript Course Audit)
Plan: 1 of 7 in current phase
Status: In progress
Last activity: 2026-02-03 -- Completed 03-01-PLAN.md (Filename Normalization, Version Manifest, Metadata)

Progress: [#.......] 1/7 phase plans (14%)

Overall: [###############.................] 15/44 total plans (34%)

## Performance Metrics

**Velocity:**
- Total plans completed: 15
- Average duration: 10 min
- Total execution time: ~143 min

**By Phase:**

| Phase | Plans | Total | Avg/Plan |
|-------|-------|-------|----------|
| 01-foundation | 6/6 | 37 min | 6 min |
| 02-java-audit | 8/8 | 100 min | 13 min |
| 03-js-audit | 1/7 | 3 min | 3 min |

**Recent Trend:**
- Last 5 plans: 03-01 (3 min), 02-08 (8 min), 02-07 (15 min), 02-03 (8 min), 02-06 (20 min)
- Trend: Structural rename plans very fast; content audit plans expected longer

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

### Pending Todos

- Fix malformed challenge.json in Python module 01 lesson 05 (invalid JSON with unescaped quotes in hints)
- Normalize challenge type values across courses (9 different types exist: FREE_CODING, QUIZ, CODE, quiz, coding, implementation, etc.)

### Blockers/Concerns

- Module 05/06 streams overlap still exists (identified in 02-01, needs future resolution)
- Module 15 lesson 15.7 virtual threads still duplicates Module 07 concurrency (content updated but structural overlap remains)
- Dart Frog community transition (July 2025) needs API verification in Phase 5
- ONNX Runtime GenAI 12 versions behind (0.5.2 -> current) -- upgrade risk in Phase 8

## Session Continuity

Last session: 2026-02-03T01:31:47Z
Stopped at: Completed 03-01-PLAN.md (Filename Normalization, Version Manifest, Metadata)
Resume file: None
