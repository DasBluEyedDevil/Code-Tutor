# Phase 6 Plan 6: M01-M07 Challenge Creation Summary

**One-liner:** 40 new challenges (34 FREE_CODING + 6 QUIZ) bring M01-M07 to 100% lesson coverage with pure Kotlin solutions

## Metadata

| Field | Value |
|-------|-------|
| Phase | 06-kotlin-course-audit |
| Plan | 06 |
| Subsystem | challenges |
| Started | 2026-02-04T17:47:18Z |
| Completed | 2026-02-04T17:58:40Z |
| Duration | ~11 min |
| Tasks | 2/2 |

## What Was Done

### Task 1: Create challenges for M01-M04 (pure Kotlin, 20 challenges)

Created 20 new challenges across 4 modules:

**M01 - The Absolute Basics (5 new):**
- L01: Kotlin Hello (FREE_CODING) - first program with main/println
- L02: Variable Swap (FREE_CODING) - var/val, temp variable pattern
- L06: Safe Greeting (FREE_CODING) - nullable types, Elvis operator
- L09: Profile Card (FREE_CODING) - string templates, functions
- L10: K2 Features Quiz (QUIZ) - K2 compiler and context parameters

**M02 - Controlling the Flow (2 new):**
- L05: Countdown Timer (FREE_CODING) - while loop with decrement
- L07: Word Counter (FREE_CODING) - mutableMapOf, getOrDefault, sorted output

**M03 - Object-Oriented Programming (3 new):**
- L05: Result Sealed Class (FREE_CODING) - sealed class, exhaustive when
- L06: ID Generator Singleton (FREE_CODING) - object keyword, encapsulation
- L07: Library Book Tracker (FREE_CODING) - data class, class with methods, filter

**M04 - Advanced Kotlin (10 new):**
- L04: Scope Functions Practice (FREE_CODING) - apply, also, let chaining
- L05: Function Composition (FREE_CODING) - compose with generics
- L06: Data Pipeline (FREE_CODING) - filter, average, String.format
- L07: Generic Stack (FREE_CODING) - Stack<T> with push/pop/peek
- L08: Coroutines Basics Quiz (QUIZ) - suspend, launch vs runBlocking
- L09: Async Task Simulation (FREE_CODING) - combining function results
- L10: Lazy Configuration (FREE_CODING) - by lazy delegation
- L11: Annotations and Reflection Quiz (QUIZ) - AnnotationTarget, KClass
- L12: Simple HTML DSL (FREE_CODING) - lambda with receiver, extension functions
- L13: Priority Task Queue (FREE_CODING) - data class, sortedBy

### Task 2: Create challenges for M06-M07 (framework-dependent, 20 challenges)

Created 20 new challenges using pure logic extraction (no Ktor/Compose/Exposed/Koin imports):

**M06 - Backend Development with Ktor (12 new):**
- L04: Parse Query Parameters (FREE_CODING) - URL string parsing
- L05: Manual JSON Builder (FREE_CODING) - data class to JSON string
- L06: SQL Query Builder (FREE_CODING) - builder pattern, method chaining
- L07: In-Memory CRUD (FREE_CODING) - TodoStore with full CRUD
- L08: Repository Pattern (FREE_CODING) - generic interface + implementation
- L09: Input Validator (FREE_CODING) - error collection pattern
- L10: Simple Password Hasher (FREE_CODING) - Caesar shift simulation
- L11: Token Builder (FREE_CODING) - JWT structure simulation
- L12: Route Guard Simulation (FREE_CODING) - role-based access check
- L13: Simple DI Container (FREE_CODING) - factory map pattern
- L14: Test Assertion Library (FREE_CODING) - assertEquals/assertTrue
- L15: REST API Design Quiz (QUIZ) - HTTP methods, status codes

**M07 - Mobile Development with Compose Multiplatform (8 new):**
- L02: Compose Multiplatform Capstone Quiz (QUIZ) - commonMain, MVVM
- L04: Layout Size Calculator (FREE_CODING) - position computation
- L05: State Holder Pattern (FREE_CODING) - generic StateHolder<T>
- L06: Route Matcher (FREE_CODING) - pattern matching with path params
- L07: API Response Parser (FREE_CODING) - key=value string parsing
- L08: Key-Value Store (FREE_CODING) - typed getters with safe casting
- L09: ViewModel Simulation (FREE_CODING) - MVVM without framework
- L10: Animation Easing Functions (FREE_CODING) - linear, easeIn, easeOut

## Decisions Made

| Decision | Context |
|----------|---------|
| QUIZ type for setup/overview/capstone lessons | Lessons like L01 (setup), L10 (K2 overview), capstone summaries don't lend themselves to coding; QUIZ tests conceptual understanding |
| Pure logic extraction for M06-M07 | Framework-dependent modules get challenges that test the underlying concept (URL parsing, state management) without requiring Ktor/Compose dependencies |
| One challenge per missing lesson | Each lesson gets exactly one challenge targeting its primary concept; existing multi-challenge lessons kept as-is |
| Caesar shift for password hashing simulation | Real hashing (bcrypt) requires libraries; simple char shift demonstrates the concept of one-way transformation |

## File Statistics

| Category | Count |
|----------|-------|
| New challenge.json files | 40 |
| New starter.kt files | 34 |
| New solution.kt files | 34 |
| Total new files | 108 (2 files were challenge.json only for QUIZ) |
| Total insertions | 3,244 lines |

## Coverage After This Plan

| Module | Before | After | Status |
|--------|--------|-------|--------|
| M01 | 5/10 lessons | 10/10 | 100% |
| M02 | 5/7 lessons | 7/7 | 100% |
| M03 | 4/7 lessons | 7/7 | 100% |
| M04 | 3/13 lessons | 13/13 | 100% |
| M05 | 7/7 lessons | 7/7 | 100% (unchanged) |
| M06 | 3/15 lessons | 15/15 | 100% |
| M07 | 2/10 lessons | 10/10 | 100% |

## Deviations from Plan

### Adjusted Scope

**1. Plan said ~29 challenges for M01-M04, actual was 20**
- Plan estimated M01: 5/10 missing (50%), M02: 2/7 (71%), M03: 3/7 (57%), M04: 10/13 (23%) needing challenges
- Actual missing: M01: 5, M02: 2, M03: 3, M04: 10 = 20 total (plan overestimated)
- The "~29" estimate in the plan was incorrect; actual gap was exactly 20 lessons

**2. Plan said ~11 challenges for M05-M07 (with ~19 total), actual was 20 for M06-M07**
- M05 confirmed 100% (7/7) -- correctly skipped
- M06 had 12 missing (not the plan's stated 12/15 -- 3 existing, 12 new is correct)
- M07 had 8 missing (not 7/10 -- 2 existing with 3 challenges each, 8 lessons needed challenges)
- Plan's total of ~19 was close; actual was 20

### No Other Deviations

All challenges follow the schema established by existing challenge.json files. No bugs encountered. No architectural changes needed.

## Commits

| Hash | Message |
|------|---------|
| fca2ebd3 | feat(06-06): create challenges for M01-M04 (20 new, pure Kotlin) |
| 8e77787b | feat(06-06): create challenges for M06-M07 (20 new, pure logic extraction) |

## Next Phase Readiness

- M01-M07 now have 100% challenge coverage
- M08-M12 already had 100% coverage (verified during execution)
- M13-M15 still need challenges (0% coverage) -- handled by plans 06-08/09/10
- Total course challenge count: 140 challenge.json files, 250 .kt files
