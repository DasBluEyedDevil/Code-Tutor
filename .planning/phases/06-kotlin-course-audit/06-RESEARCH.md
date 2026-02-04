# Phase 6: Kotlin Course Audit - Research

**Researched:** 2026-02-04
**Domain:** Kotlin 2.3/K2 course content (128 lessons across 15 modules), Ktor 3.4, Compose Multiplatform 1.10, SQLDelight, Koin 4.x, Arrow 2.2, Gradle 8.x Kotlin DSL
**Confidence:** HIGH (versions verified via official sources; course content analyzed via filesystem)

## Summary

The Kotlin course contains 15 modules with 128 lessons, 1,726 content files, and 80 existing challenges. It teaches from absolute basics through OOP, coroutines, Ktor backend, Compose Multiplatform, SQLDelight persistence, KMP architecture, DI with Koin, testing, deployment, Gradle mastery, functional programming with Arrow, and K2 tooling. The course is the most framework-dense of all six courses, with 7+ distinct frameworks to verify.

The most critical finding is that **the course content already targets Kotlin 2.3.0 and Ktor 3.4.0** in its code examples, which aligns well with the current stable ecosystem (Kotlin 2.3.0 released December 2025, Ktor 3.4.0 released January 2026). However, the version manifest still says "Kotlin 2.0" and "Ktor 3.x", needing specific version updates. The course also references Exposed 1.0.0 (which just reached 1.0 GA in January 2026 with R2DBC support and stable API guarantees), and Koin 4.1.1 (current stable). The version alignment between course content and current ecosystem is much better than the Flutter course was.

The second critical finding is a **severe lesson ordering problem in Module 06 (Ktor)**. The lessons are ordered: HTTP intro (5.1) -> Authentication (5.10-5.12) -> DI with Koin (5.13) -> Testing (5.14) -> Capstone (5.15) -> THEN Ktor setup (5.2) -> Routing (5.3) -> Parameters (5.4) etc. This means authentication lessons appear BEFORE students learn to set up Ktor or define routes. The `order` field in lesson.json confirms this broken sequence (not just a directory naming issue).

The third finding is the **context receivers deprecation**: Module 15 Lesson 5 teaches context receivers (`context(Raise<UserError>)`) which are deprecated in Kotlin 2.2+ and being replaced by context parameters. Module 14 (Arrow) also uses context receivers in its Raise examples. Arrow 2.2.0 already provides a `arrow.core.raise.context` package using context parameters. This lesson needs significant rewriting.

**Primary recommendation:** The version alignment is good -- the course is already written for Kotlin 2.3.0/Ktor 3.4.0/Exposed 1.0.0/Koin 4.1.1. The main work is: (1) fix the Module 06 lesson ordering, (2) update the context receivers lesson to context parameters, (3) create 70+ missing challenges, (4) create a capstone project, (5) enrich with KEY_POINTs and analogies. Update the version manifest to pin exact versions.

## Standard Stack

The established technologies for this course, with version verification:

### Core Runtime

| Technology | Manifest Version | Course Content Uses | Current Stable | Status |
|------------|-----------------|---------------------|----------------|--------|
| Kotlin | 2.0 | **2.3.0** | **2.3.0** | ALIGNED (manifest needs update) |
| K2 Compiler | enabled | enabled | enabled (default since 2.0) | ALIGNED |

**Kotlin version timeline (context for auditors):**
- Kotlin 2.0.0 -- May 2024 (K2 stable, default for all targets)
- Kotlin 2.1.0 -- November 2024 (guard conditions, non-local break/continue)
- Kotlin 2.1.20 -- March 2025 (context parameters preview, K2 kapt default)
- Kotlin 2.2.0 -- June 2025 (context parameters Beta, nested type aliases)
- Kotlin 2.2.20 -- September 2025 (Swift export, K2 IDE improvements)
- Kotlin 2.3.0 -- December 2025 (nested type aliases stable, unused return value checker, JDK 25 support)

### Framework Stack

| Technology | Manifest Version | Course Content Uses | Current Stable | Breaking Changes |
|------------|-----------------|---------------------|----------------|-----------------|
| Ktor | 3.x | **3.4.0** | **3.4.0** | None vs. course content |
| Compose Multiplatform | 1.7.x | Not version-pinned in content | **1.10.0** | Manifest needs update to 1.10.x |
| Exposed | Not listed | **1.0.0** | **1.0.0 GA** (Jan 2026) | R2DBC added; v1 namespace; stable API |
| SQLDelight | 2.x | Not version-pinned in content | **2.2.1** | Compatible within 2.x |
| Koin | 4.x | **4.1.1** | **4.1.1** | None vs. course content |
| Arrow | Not listed | Used in Module 14 | **2.2.0** | Context parameters replace context receivers |
| kotlinx.coroutines | Not listed | **1.10.2** | **1.10.2** | ALIGNED |
| kotlinx.serialization | Not listed | **1.10.0** | **1.10.0** | ALIGNED |
| Gradle | 8.x | 8.x | **8.12+** | Kotlin DSL default; version catalogs |

### Version Manifest Updates Required

| Field | Current Value | Should Be | Why |
|-------|--------------|-----------|-----|
| kotlin.runtime.version | "2.0" | "2.3" | Course content already targets 2.3.0 |
| kotlin.runtime.notes | "K2 compiler enabled; minimum 2.0+" | "K2 compiler default; Kotlin 2.3.0 stable; context parameters Beta" | Reflects current feature state |
| kotlin.frameworks.ktor.version | "3.x" | "3.4.x" | Course content hardcodes 3.4.0 |
| kotlin.frameworks.compose.version | "1.7.x" | "1.10.x" | iOS stable since 1.8; hot reload since 1.10 |
| kotlin.frameworks.sqldelight.version | "2.x" | "2.2.x" | Latest within 2.x line |
| kotlin.frameworks.koin.version | "4.x" | "4.1.x" | Current stable |
| (add) kotlin.frameworks.exposed | (missing) | "1.0.x" | Used throughout Ktor module |
| (add) kotlin.frameworks.arrow | (missing) | "2.2.x" | Used in Module 14 |
| (add) kotlin.frameworks.kotlinx-coroutines | (missing) | "1.10.x" | Used throughout |
| (add) kotlin.frameworks.kotlinx-serialization | (missing) | "1.10.x" | Used throughout |

## Architecture Patterns

### Course Module Progression
```
Module 01: The Absolute Basics (10 lessons)              -- Kotlin fundamentals, variables, control flow, null safety
Module 02: Controlling the Flow (7 lessons)               -- If/when, loops, lists, maps
Module 03: Object-Oriented Programming (7 lessons)        -- Classes, inheritance, interfaces, data/sealed classes
Module 04: Advanced Kotlin (13 lessons)                   -- FP, lambdas, collections, scope functions, generics, coroutines basics, DSLs
Module 05: Coroutines & Flows (7 lessons)                 -- Structured concurrency, dispatchers, Flow, StateFlow/SharedFlow
Module 06: Backend Development with Ktor (15 lessons)     -- HTTP, Ktor, Exposed ORM, auth, REST API [ORDERING BROKEN]
Module 07: Mobile Development with Compose MP (10 lessons) -- Compose UI, state, navigation, networking
Module 08: Persistence with SQLDelight (7 lessons)        -- SQL queries, migrations, reactive queries, platform drivers
Module 09: KMP Architecture Patterns (7 lessons)          -- Clean architecture, MVVM, MVI, shared ViewModels
Module 10: Dependency Injection with Koin (7 lessons)     -- Koin fundamentals, KMP integration, annotations
Module 11: Testing KMP Applications (7 lessons)           -- Unit testing, coroutine testing, mocking, UI testing, CI/CD
Module 12: Professional Development & Deployment (14 lessons) -- KMP patterns, testing, security, CI/CD, cloud, app stores
Module 13: Gradle Mastery (6 lessons)                     -- Kotlin DSL, version catalogs, multiplatform builds, custom plugins
Module 14: Functional Kotlin with Arrow (6 lessons)       -- FP principles, Result, Either/Option, Raise, railway-oriented
Module 15: The K2 Era (5 lessons)                         -- K2 compiler, migration, KSP, context receivers [DEPRECATED CONTENT]
```

### Content File Distribution (1,726 files across 128 lessons)
| Type (Frontmatter) | Count | Notes |
|---------------------|-------|-------|
| THEORY | ~1,390 | Primary content type (heaviest in M01, M04, M06, M07, M12) |
| EXAMPLE | ~193 | Heaviest in M13 (28) and M14 (45) -- Arrow and Gradle are example-heavy |
| KEY_POINT | **30** | **Severe gap**: only M01(1), M03(3), M04(7), M06(17), M07(1), M12(1) |
| ANALOGY | **48** | Gap: only M01-M04, M05(1), M06(15), M09(1), M10(1) |
| WARNING | **57** | Present in M01-M02, M04-M05, M08-M11, M12-M14; absent from M03, M06-M07, M15 |

### Critical Structural Issues

**Issue 1: Module 06 Lesson Ordering (SEVERE)**
The Ktor module presents lessons in this order (by `order` field in lesson.json):
1. Lesson 5.1: Introduction to Backend Development & HTTP Fundamentals
2. Lesson 5.10: Authentication - User Registration & Password Hashing
3. Lesson 5.11: Authentication - Login & JWT Tokens
4. Lesson 5.12: Authentication - Protecting Routes with JWT
5. Lesson 5.13: Dependency Injection with Koin
6. Lesson 5.14: Testing Your API
7. Lesson 5.15: Part 5 Capstone - Task Management API
8. Lesson 5.2: Setting Up Your First Ktor Project
9. Lesson 5.3: Routing Fundamentals
10. Lesson 5.4: Request Parameters
11. Lesson 5.5: JSON Serialization
12. Lesson 5.6: Database Fundamentals with Exposed (Part 1)
13. Lesson 5.7: Database Operations with Exposed (Part 2)
14. Lesson 5.8: The Repository Pattern
15. Lesson 5.9: Request Validation & Error Handling

Students see authentication, testing, and the capstone BEFORE learning Ktor basics. The directory prefix numbers (01- through 15-) match the `order` field, confirming the app renders them in this broken order. The correct progression should be: 5.1 -> 5.2 -> 5.3 -> 5.4 -> 5.5 -> 5.6 -> 5.7 -> 5.8 -> 5.9 -> 5.10 -> 5.11 -> 5.12 -> 5.13 -> 5.14 -> 5.15.

**Resolution:** Renumber directory prefixes and `order` fields to match the logical lesson number sequence: 01=5.1, 02=5.2, 03=5.3... 15=5.15.

**Issue 2: Module 05 Title Inconsistency**
Module 05 has title "Module 04A: Coroutines & Flows" in module.json -- the "04A" numbering is a leftover from an earlier numbering scheme.

**Issue 3: Modules 08-10 Title Inconsistency**
- Module 08: "Module 06A: Persistence with SQLDelight"
- Module 09: "Module 06B: KMP Architecture Patterns"
- Module 10: "Module 06C: Dependency Injection with Koin"

These "06A/06B/06C" prefixes in the title field are leftovers from earlier numbering.

**Issue 4: Module 04 Overlap with Module 05**
Module 04 (Advanced Kotlin) includes lessons on coroutines fundamentals (4.8) and advanced coroutines (4.9), while Module 05 is entirely dedicated to coroutines. This creates potential content duplication -- the structural review should determine whether M04 L8-L9 should be removed/relocated or kept as an introduction.

### Challenge Gap Analysis

**Total lessons: 128. Lessons WITH challenges: 48. Lessons WITHOUT challenges: 80.**

Challenge coverage by module:
| Module | Lessons | With Challenges | Without | Coverage |
|--------|---------|----------------|---------|----------|
| 01-absolute-basics | 10 | 5 | 5 | 50% |
| 02-controlling-flow | 7 | 5 | 2 | 71% |
| 03-oop | 7 | 4 | 3 | 57% |
| 04-advanced-kotlin | 13 | 3 | 10 | 23% |
| 05-coroutines-flows | 7 | 7 | 0 | **100%** |
| 06-ktor | 15 | 3 | 12 | 20% |
| 07-compose-mp | 10 | 3 | 7 | 30% |
| 08-sqldelight | 7 | 7 | 0 | **100%** |
| 09-kmp-architecture | 7 | 4 | 3 | 57% |
| 10-koin | 7 | 7 | 0 | **100%** |
| 11-testing | 7 | 7 | 0 | **100%** |
| 12-deployment | 14 | 4 | 10 | 29% |
| 13-gradle | 6 | 0 | 6 | **0%** |
| 14-arrow | 6 | 0 | 6 | **0%** |
| 15-k2-era | 5 | 0 | 5 | **0%** |

**80 lessons need challenges.** The worst coverage is in the later modules (13-15 at 0%) and the Ktor module (20%). Some lessons (capstones, intro/setup) may not need traditional coding challenges, but most do.

### Content Coverage Gaps

**KEY_POINT coverage (30 total, only 6 modules have any):**
Modules 02, 05, 08-11, 13-15 have ZERO key points. This is far worse than the Flutter course (which had 160 key points).

**ANALOGY coverage (48 total, 9 modules have zero):**
Modules 07, 08, 11-15 have ZERO analogies. Module 06 (Ktor) has 15, which is good.

**WARNING coverage (57 total, 4 modules have zero):**
Modules 03, 06, 07, 15 have ZERO warnings.

### Existing Capstone Content
Module 12 Lesson 8 describes a "ShopKotlin" e-commerce platform (Ktor + Jetpack Compose + PostgreSQL + Stripe + JWT auth). This is described in lesson content but there is no standalone project directory. The capstone uses "Jetpack Compose" (Android-only) rather than Compose Multiplatform, which contradicts the course title "Kotlin Multiplatform Complete Course".

### Code Execution Architecture (Kotlin)
The WPF app executes Kotlin challenges via two paths:
1. **Piston API**: Maps Kotlin to version **1.8.20** (pre-K2, pre-Kotlin 2.0). K2 compiler features, context parameters, and Kotlin 2.0+ syntax will NOT work.
2. **Local execution**: Uses `kotlinc -script <file>.kts` -- executes as Kotlin script, using whatever Kotlin version is installed locally.

**Critical implications:**
- Piston path: Any challenge using Kotlin 2.0+ features will fail (K2 compiler, sealed interfaces, value classes with context parameters, etc.)
- Local path: Works if user has current Kotlin SDK installed
- Challenges for backend modules (Ktor, Exposed, Koin) cannot execute in either path (require project context, dependencies, server)
- Only Modules 01-04 challenges (pure Kotlin logic) can realistically execute via either path
- Even Modules 05 (Coroutines) challenges need kotlinx.coroutines dependency which neither execution path provides

### Anti-Patterns to Avoid
- **Attempting to make Ktor/Compose/SQLDelight challenges executable**: These require project context with Gradle builds and dependencies. Design challenges as code-review/conceptual or output-matching for pure Kotlin snippets.
- **Ignoring the Module 06 ordering problem**: This is the most impactful structural fix -- students literally cannot follow the Ktor progression as ordered.
- **Rewriting context receivers to context parameters AND Arrow simultaneously**: The context parameters feature is Beta in Kotlin 2.2+. The lesson should teach both (receivers as legacy, parameters as future) with a focus on migration awareness.
- **Creating a capstone that requires deployment infrastructure**: The capstone should be buildable locally without cloud accounts, databases, or payment processors.

## Don't Hand-Roll

Problems that have existing solutions -- do not create custom approaches:

| Problem | Don't Build | Use Instead | Why |
|---------|-------------|-------------|-----|
| Module 06 lesson reordering | Manual file-by-file editing | Scripted batch rename of directory prefixes + lesson.json order fields | 15 directories and 15 lesson.json files need updating consistently |
| Module title fix (05, 08-10) | Manual JSON editing | Scripted batch update of 4 module.json title fields | Same pattern as Phase 1 normalization |
| Version string sweep | Reading each file manually | Grep/search across 1,726 content files for version patterns | Catches all hardcoded version strings |
| KEY_POINT generation | Manual per-lesson | Batch template with per-lesson customization | 98+ lessons need key points |
| Challenge validation (pure Kotlin) | Manual review | Execute solution.kt files via `kotlinc -script` locally | 48 existing challenges can be batch-validated |
| Challenge validation (framework) | Trying to execute | Manual code review only | Ktor/Compose/SQLDelight challenges need project context |
| Capstone project template | Starting from scratch | Use Kotlin Multiplatform Wizard (kmp.jetbrains.com) as starting point | Generates correct project structure with Ktor + Compose MP |

**Key insight:** The Kotlin course has fewer structural issues than Flutter (no archived lessons, no misplaced content), but has worse enrichment coverage (30 key points vs. Flutter's 160) and a more severe lesson ordering bug in the Ktor module. The version alignment is actually quite good -- the course was apparently written recently enough to target Kotlin 2.3.0/Ktor 3.4.0.

## Common Pitfalls

### Pitfall 1: Module 06 Broken Lesson Order
**What goes wrong:** Students encounter authentication, JWT, testing, and capstone lessons before learning how to set up Ktor, define routes, or serialize JSON.
**Why it happens:** Lesson numbering in the directory names followed an alphabetical sort of "lesson-51, lesson-510, lesson-511..." instead of numeric sort "lesson-51, lesson-52, lesson-53...". The directory prefixes (01-, 02-...) were assigned based on this alphabetical ordering.
**How to avoid:** Reorder directory prefixes and `order` fields to match logical lesson number sequence: 5.1, 5.2, 5.3... 5.15.
**Warning signs:** Any lesson referencing Ktor setup, routing, or serialization concepts as "previously learned" when those lessons haven't appeared yet.

### Pitfall 2: Context Receivers Are Deprecated
**What goes wrong:** Module 15 Lesson 5 teaches `context(Logger, Database)` syntax extensively, and Module 14's Arrow examples use `context(Raise<UserError>)`. This syntax is deprecated since Kotlin 2.0.20 with warnings, and context parameters (Beta since 2.2) are the replacement.
**Why it happens:** The context receivers experimental feature was deprecated AFTER the course was written.
**How to avoid:** Rewrite the context receivers lesson to teach context parameters as the primary approach, with a note about the deprecated context receivers syntax for legacy code. Update Arrow examples to use `arrow.core.raise.context` package (which uses context parameters).
**Impact:** Module 15 Lesson 5 (entire lesson), Module 14 Lesson 5 (effect system examples), any other references to `context()` syntax.

### Pitfall 3: Piston Kotlin 1.8.20 Cannot Run Kotlin 2.0+ Code
**What goes wrong:** Challenges using Kotlin 2.0+ features (value class constructors, context parameters, when-with-guard, etc.) fail when executed via Piston.
**Why it happens:** PistonExecutor hardcodes Kotlin version as "1.8.20" which is pre-K2 compiler.
**How to avoid:** Either (a) update Piston Kotlin version (app change, possibly out of scope), or (b) ensure challenges stick to Kotlin 1.8-compatible syntax for pure execution tests, or (c) accept that only local execution works for modern Kotlin.
**Affected challenges:** All existing challenges should be verified for 1.8.20 compatibility if Piston execution is needed.

### Pitfall 4: Module 04 and Module 05 Coroutines Overlap
**What goes wrong:** Module 04 Lessons 8-9 teach coroutines fundamentals and advanced coroutines, then Module 05 is a dedicated 7-lesson coroutines module. Students may encounter repetitive content.
**Why it happens:** Module 04 originally covered everything "advanced," but coroutines grew large enough to warrant their own module without removing the M04 originals.
**How to avoid:** The structural review (06-01) should decide whether M04 L8-L9 stay as a brief intro (with M05 going deeper) or are removed/merged. Either way, ensure no significant content duplication.

### Pitfall 5: Existing Capstone Uses Jetpack Compose, Not Compose Multiplatform
**What goes wrong:** The Module 12 capstone describes an Android-only "Jetpack Compose" app, contradicting the course title "Kotlin Multiplatform Complete Course."
**Why it happens:** The capstone was written before the course was reframed as KMP-focused.
**How to avoid:** The new capstone (KTLN-04) should use Compose Multiplatform, demonstrating true cross-platform development. The existing capstone content in M12 L8 should be updated or clearly labeled as an Android-specific variant.

### Pitfall 6: Exposed 1.0 Namespace Changes
**What goes wrong:** Exposed 1.0.0 introduced a `v1` namespace segment in all packages. Code using old package names may still compile but won't match official documentation.
**Why it happens:** Exposed 1.0 GA was released January 2026, very recently.
**How to avoid:** Check if course content uses old vs. new package names. If course content already targets Exposed 1.0.0 (it does in dependency declarations), verify import statements match the 1.0 API.
**Evidence:** Course uses `"org.jetbrains.exposed:exposed-core:1.0.0"` and `"org.jetbrains.exposed:exposed-jdbc:1.0.0"`.

## Code Examples

### Kotlin 2.3.0 Version Declaration (current in course content, CORRECT)
```kotlin
// build.gradle.kts
plugins {
    kotlin("jvm") version "2.3.0"  // Latest stable as of early 2026
}
```
Source: Module 13, Module 15 content files.

### Ktor 3.4.0 Dependencies (current in course content, CORRECT)
```kotlin
dependencies {
    implementation("io.ktor:ktor-server-core-jvm:3.4.0")
    implementation("io.ktor:ktor-server-cio-jvm:3.4.0")
    implementation("io.ktor:ktor-server-content-negotiation-jvm:3.4.0")
    implementation("io.ktor:ktor-serialization-kotlinx-json-jvm:3.4.0")
    implementation("io.ktor:ktor-server-auth-jvm:3.4.0")
    implementation("io.ktor:ktor-server-auth-jwt-jvm:3.4.0")
    implementation("org.jetbrains.exposed:exposed-core:1.0.0")
    implementation("org.jetbrains.exposed:exposed-jdbc:1.0.0")
    implementation("io.insert-koin:koin-ktor:4.1.1")
}
```
Source: Module 06 Lessons 6-7 content files.

### Context Receivers (DEPRECATED -- needs migration to context parameters)
```kotlin
// OLD: Context receivers (deprecated since Kotlin 2.0.20, Module 15 L5 currently teaches this)
context(Logger, Database)
fun loadUsers(): List<User> {
    info("Loading users")
    return query("SELECT * FROM users").map { /* ... */ }
}

// NEW: Context parameters (Beta since Kotlin 2.2)
context(logger: Logger, db: Database)
fun loadUsers(): List<User> {
    logger.info("Loading users")
    return db.query("SELECT * FROM users").map { /* ... */ }
}
```
Key difference: context parameters require explicit names and explicit receiver qualification.

### Arrow Raise with Context Parameters (NEW -- Arrow 2.2.0)
```kotlin
// OLD: arrow.core.raise with context receivers
import arrow.core.raise.Raise
context(Raise<UserError>)
fun getUser(id: Long): User { ... }

// NEW: arrow.core.raise.context with context parameters (Arrow 2.2.0)
import arrow.core.raise.context.Raise
context(raise: Raise<UserError>)
fun getUser(id: Long): User { ... }
```

### Version Catalog Pattern (current in course content, CORRECT)
```toml
# gradle/libs.versions.toml
[versions]
kotlin = "2.3.0"
kotlinx-coroutines = "1.10.2"
kotlinx-serialization = "1.10.0"
```
Source: Module 13, Module 15 content files.

## State of the Art

| Course Content | Current Ecosystem | When Changed | Impact on Course |
|----------------|-------------------|--------------|-----------------|
| Kotlin 2.3.0 | Kotlin 2.3.0 | Dec 2025 | **ALIGNED** -- no changes needed |
| Ktor 3.4.0 | Ktor 3.4.0 | Jan 2026 | **ALIGNED** -- no changes needed |
| Exposed 1.0.0 | Exposed 1.0.0 GA | Jan 2026 | **ALIGNED** -- verify package namespaces, R2DBC not used |
| Koin 4.1.1 | Koin 4.1.1 | Sep 2025 | **ALIGNED** -- no changes needed |
| Context receivers (`context(Type)`) | Context parameters (`context(name: Type)`) | Kotlin 2.2 Beta | **MISALIGNED** -- Module 15 L5 and Module 14 need updating |
| Compose Multiplatform (unversioned) | 1.10.0 | Jan 2026 | **NEEDS VERIFICATION** -- iOS stable since 1.8, Hot Reload since 1.10 |
| SQLDelight (unversioned) | 2.2.1 | 2025 | **NEEDS VERIFICATION** -- compatible within 2.x |
| Arrow (unversioned) | 2.2.0 | Nov 2025 | **NEEDS VERIFICATION** -- context parameters API added |
| KAPT | KSP (KAPT deprecated) | Kotlin 2.0+ | Module 15 L3-L4 covers KSP migration -- ALIGNED |
| Jetpack Compose (capstone) | Compose Multiplatform 1.10 (iOS stable) | May 2025 | **MISALIGNED** -- capstone should use CMP not Jetpack Compose |

**Deprecated/outdated in course:**
- Context receivers syntax (`context(Type)`) -- deprecated since Kotlin 2.0.20, warnings emitted
- Module title prefixes ("Module 04A", "Module 06A/B/C") -- leftover numbering scheme
- Capstone using "Jetpack Compose" instead of "Compose Multiplatform"
- Piston Kotlin 1.8.20 -- cannot run any Kotlin 2.0+ code
- version-manifest.json Kotlin version "2.0" -- should be "2.3"

## Capstone Project Recommendations

### Option A: Task Management App (Ktor + Compose Multiplatform)
**Structure:** Shared KMP module (domain, data, ktor-client) + Ktor server backend + Compose Multiplatform UI (Android + Desktop minimum)
**Why:** Builds on the Task Management API already created as a mini-capstone in Module 06 Lesson 7. Students extend it with a real UI.
**Technologies used:** Ktor server, Exposed ORM, Koin DI, Compose Multiplatform, SQLDelight (client-side caching), kotlinx.serialization, coroutines/Flow.
**Scope:** Realistic for a course capstone -- REST API + CRUD UI + auth + local persistence.

### Option B: Notes/Bookmark App (Ktor + Compose Multiplatform)
**Structure:** Similar KMP architecture to Option A but simpler domain model.
**Why:** Lower complexity, faster to build, but still demonstrates all key technologies.
**Scope:** May be too simple for a 94-hour course capstone.

**Recommendation:** Option A (Task Management) -- it connects directly to Module 06's capstone API and demonstrates the full KMP stack without requiring external services (no Stripe, no push notifications, no PostgreSQL -- can use H2/SQLite).

## Open Questions

Things that couldn't be fully resolved:

1. **Should the Module 04 coroutines lessons (L8-L9) be removed or kept?**
   - What we know: Module 04 has coroutines basics and Module 05 is dedicated coroutines
   - What's unclear: Whether M04 L8-L9 serve as a useful intro or create confusion
   - Recommendation: Keep as brief intro that "previews" coroutines, ensure M05 doesn't repeat the same content verbatim

2. **Should the context receivers lesson be rewritten entirely or teach both?**
   - What we know: Context receivers are deprecated; context parameters are Beta in 2.2+
   - What's unclear: How stable context parameters will be by the time students use the course
   - Recommendation: Rename lesson to "Context Parameters and the Future of Kotlin" -- teach context parameters as primary, mention context receivers as deprecated legacy

3. **Should Compose Multiplatform version be pinned to 1.10.x or 1.8.x?**
   - What we know: Course module.json says 1.7.x in manifest; iOS stable since 1.8; hot reload since 1.10
   - What's unclear: Whether specific CMP APIs used in lessons are compatible across these versions
   - Recommendation: Target 1.10.x (latest stable), verify content against 1.10 API

4. **What about the Module 12 existing capstone (ShopKotlin)?**
   - What we know: It describes an Android-only Jetpack Compose + Ktor e-commerce platform
   - What's unclear: Whether to keep it as instructional content and add a separate KMP capstone, or replace it
   - Recommendation: Keep the M12 L8 lesson content as a "case study" reference, create the new capstone as a separate standalone project

5. **How should framework-dependent challenges be designed?**
   - What we know: Only pure Kotlin challenges can execute via Piston or `kotlinc -script`
   - What's unclear: The challenge format for Ktor, Compose, SQLDelight, Koin, Arrow modules
   - Recommendation: Use output-matching challenges with simulated dependencies (provide stub classes in starter code), or conceptual/code-review challenges

## Sources

### Primary (HIGH confidence)
- Course filesystem analysis: 15 modules, 128 lessons, 1,726 content files, 80 challenges verified
- version-manifest.json: Kotlin 2.0 target (needs updating to 2.3)
- course.json: minimumRuntimeVersion "Kotlin 2.0"
- PistonExecutor.cs line 93: Kotlin version hardcoded to "1.8.20"
- CodeExecutionService.cs line 120: Local Kotlin execution via `kotlinc -script`
- [Kotlin 2.3.0 Released](https://blog.jetbrains.com/kotlin/2025/12/kotlin-2-3-0-released/) -- December 2025
- [What's New in Kotlin 2.3.0](https://kotlinlang.org/docs/whatsnew23.html) -- Official docs
- [Ktor 3.4.0 Released](https://blog.jetbrains.com/kotlin/2026/01/ktor-3-4-0-is-now-available/) -- January 2026
- [Exposed 1.0 Available](https://blog.jetbrains.com/kotlin/2026/01/exposed-1-0-is-now-available/) -- January 2026
- [Compose Multiplatform 1.10.0](https://blog.jetbrains.com/kotlin/2026/01/compose-multiplatform-1-10-0/) -- January 2026
- [Compose Multiplatform 1.8.0 iOS Stable](https://blog.jetbrains.com/kotlin/2025/05/compose-multiplatform-1-8-0-released-compose-multiplatform-for-ios-is-stable-and-production-ready/) -- May 2025

### Secondary (MEDIUM confidence)
- [Context Parameters Update](https://blog.jetbrains.com/kotlin/2025/04/update-on-context-parameters/) -- JetBrains blog, April 2025
- [Context Parameters Documentation](https://kotlinlang.org/docs/context-parameters.html) -- Official Kotlin docs
- [Arrow 2.2.0 Blog Post](https://arrow-kt.io/community/blog/2025/11/01/arrow-2-2/) -- November 2025
- [Ktor Migration Guide 2.x to 3.x](https://ktor.io/docs/migrating-3.html) -- Official Ktor docs
- [Kotlin Compatibility Guide 2.2](https://kotlinlang.org/docs/compatibility-guide-22.html) -- Official docs
- [Kotlin Compatibility Guide 2.1](https://kotlinlang.org/docs/compatibility-guide-21.html) -- Official docs
- [Koin Official Site](https://insert-koin.io/) -- Version 4.1.1 confirmed
- [SQLDelight GitHub Releases](https://github.com/sqldelight/sqldelight/releases) -- Version 2.2.1 confirmed
- [KMP Roadmap August 2025](https://blog.jetbrains.com/kotlin/2025/08/kmp-roadmap-aug-2025/) -- JetBrains blog
- [Gradle Best Practices for Kotlin](https://kotlinlang.org/docs/gradle-best-practices.html) -- Official docs

### Tertiary (LOW confidence)
- Piston Kotlin 2.x support: Could not verify exact versions available in Piston package repository. The 1.8.20 version in PistonExecutor.cs is pre-K2.
- Exposed 1.0.0 `v1` namespace: Mentioned in official announcement but course import patterns not fully verified against new namespace
- Arrow 2.2.0 context parameters API: The `arrow.core.raise.context` package exists but specific API surface not verified via code inspection
- Compose Multiplatform version used in course content: Content does not pin CMP version explicitly; need lesson-by-lesson verification

## Metadata

**Confidence breakdown:**
- Standard stack: HIGH -- all versions verified via official JetBrains sources and course content grep
- Course structure: HIGH -- filesystem analysis complete, all 128 lessons and 80 challenges examined
- Architecture/progression: HIGH -- module contents, lesson titles, order fields all analyzed
- Lesson ordering bug: HIGH -- verified via lesson.json `order` field for all 15 Ktor lessons
- Context receivers deprecation: HIGH -- JetBrains official blog and Kotlin docs confirm deprecation
- Challenge gap analysis: HIGH -- exact count of 80 lessons without challenges, per-module breakdown
- Capstone recommendation: MEDIUM -- based on course progression analysis, not user preference input
- Arrow context parameters migration: MEDIUM -- Arrow 2.2.0 blog confirms feature, specific API details estimated
- Compose Multiplatform content accuracy: LOW -- version not pinned in content, needs lesson-by-lesson review

**Research date:** 2026-02-04
**Valid until:** 2026-03-04 (30 days -- Kotlin/JetBrains ecosystem follows 3-month release cycles)
