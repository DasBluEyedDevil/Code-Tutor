---
phase: 06-kotlin-course-audit
plan: 03
subsystem: content
tags: [kotlin, ktor, exposed, compose-multiplatform, cross-platform, android-only-apis]
requires:
  - phase: 06-01
    provides: version alignment (Kotlin 2.3, Ktor 3.4.0, CMP 1.10.x) and M06 lesson reorder
provides:
  - M06 (15 lessons) verified against Ktor 3.4.0, Exposed 1.0.0, kotlinx.serialization
  - M07 (10 lessons) Android-only APIs annotated, cross-platform alternatives shown
  - Forward reference integrity confirmed post-M06 reorder
affects: [06-08]
tech-stack:
  added: []
  patterns: []
key-files:
  created: []
  modified:
    - content/courses/kotlin/modules/06-backend-development-with-ktor/lessons/15-lesson-515-part-5-capstone-project-task-management-api/content/13-theory.md
    - content/courses/kotlin/modules/07-mobile-development-with-compose-multiplatform/lessons/02-lesson-610-part-6-capstone-task-manager-app/content/06-theory.md
    - content/courses/kotlin/modules/07-mobile-development-with-compose-multiplatform/lessons/03-lesson-62-introduction-to-compose-multiplatform-ui/content/16-theory.md
    - content/courses/kotlin/modules/07-mobile-development-with-compose-multiplatform/lessons/04-lesson-63-layouts-and-ui-design/content/09-theory.md
    - content/courses/kotlin/modules/07-mobile-development-with-compose-multiplatform/lessons/04-lesson-63-layouts-and-ui-design/content/13-theory.md
    - content/courses/kotlin/modules/07-mobile-development-with-compose-multiplatform/lessons/04-lesson-63-layouts-and-ui-design/content/17-theory.md
    - content/courses/kotlin/modules/07-mobile-development-with-compose-multiplatform/lessons/07-lesson-66-networking-and-apis/content/03-theory.md
    - content/courses/kotlin/modules/07-mobile-development-with-compose-multiplatform/lessons/07-lesson-66-networking-and-apis/content/05-theory.md
    - content/courses/kotlin/modules/07-mobile-development-with-compose-multiplatform/lessons/08-lesson-67-local-data-storage/content/04-theory.md
    - content/courses/kotlin/modules/07-mobile-development-with-compose-multiplatform/lessons/08-lesson-67-local-data-storage/content/09-theory.md
    - content/courses/kotlin/modules/07-mobile-development-with-compose-multiplatform/lessons/09-lesson-68-mvvm-architecture/content/06-theory.md
    - content/courses/kotlin/modules/07-mobile-development-with-compose-multiplatform/lessons/09-lesson-68-mvvm-architecture/content/07-theory.md
    - content/courses/kotlin/modules/07-mobile-development-with-compose-multiplatform/lessons/09-lesson-68-mvvm-architecture/content/08-theory.md
    - content/courses/kotlin/modules/07-mobile-development-with-compose-multiplatform/lessons/09-lesson-68-mvvm-architecture/content/11-theory.md
    - content/courses/kotlin/modules/07-mobile-development-with-compose-multiplatform/lessons/09-lesson-68-mvvm-architecture/content/13-theory.md
  deleted: []
key-decisions:
  - "M06 forward references all correct post-reorder (zero broken cross-references)"
  - "Exposed 1.0.0 v1 namespace (org.jetbrains.exposed.v1.*) confirmed correct in M06 L08"
  - "M07 L07-L09 teach Android-specific libraries (Retrofit, Room, Hilt) -- annotated as Android-only, not replaced"
  - "Cross-platform alternatives (Ktor Client, SQLDelight, Koin) referenced in Android-only sections"
  - "R.drawable references replaced with cross-platform alternatives (Box placeholders, initials)"
  - "hiltViewModel() removed from shared/commonMain code, constructor injection used instead"
  - "Dynamic color theming labeled as androidMain with commonMain fallback shown"
duration: 12min
completed: 2026-02-04
---

# Phase 6 Plan 03: M06-M07 Accuracy Pass Summary

M06 Ktor 3.4.0/Exposed 1.0.0 verified with zero API errors and zero broken forward references post-reorder; M07 CMP annotated with Android-only labels for Retrofit/Room/Hilt code and cross-platform alternatives.

## What Was Done

### Task 1: Accuracy Pass M06 (15 lessons -- Ktor 3.4.0, Exposed 1.0.0)

- **Forward reference audit**: All "previous lesson" / "next lesson" references verified correct after Plan 01 reorder. Zero broken cross-references.
- **Ktor 3.4.0 patterns**: Plugin installation (`install(ContentNegotiation)`, `install(Authentication)`), routing (`routing { get("/") { } }`), JWT auth, CIO engine -- all current.
- **Ktor dependency format**: All use correct `io.ktor:ktor-server-*-jvm:3.4.0` format (with `-jvm` suffix).
- **Exposed 1.0.0**: L08 uses `org.jetbrains.exposed.v1.core.*` (new v1 namespace, correct for 1.0.0). Dependencies correctly declare `1.0.0`.
- **kotlinx.serialization**: `@Serializable`, `@SerialName`, `Json { }` configuration all current.
- **Application.module()**: Found in L10, L11, L12 -- confirmed current Ktor 3.x extension function pattern (NOT deprecated Ktor 2.x features approach).
- **1 fix applied**: L15 capstone "Next Steps" referenced "Part 6: Android Development" with Jetpack Compose/Retrofit -- fixed to "Part 6: Mobile Development with Compose Multiplatform" with Ktor Client.
- **Result**: 15 lessons, 234 content files reviewed, 1 correction applied, 0 forward-reference fixes needed.

### Task 2: Accuracy Pass M07 (10 lessons -- Compose Multiplatform 1.10.x)

- **Cross-platform content review**: L01, L03-L06, L10 are genuinely cross-platform CMP content (correct patterns).
- **Android-only content identified**: L07 (Retrofit), L08 (Room, DataStore), L09 (Hilt) primarily teach Android-specific libraries.
- **Design decision**: L07-L09 content teaches valid Android patterns that apply when building the Android target of a CMP app. Modules M08 (SQLDelight), M10 (Koin) teach the cross-platform equivalents. Content kept but annotated.
- **Fixes applied (14 files across 6 lessons)**:
  - L02 capstone: `hiltViewModel()` -> constructor injection
  - L03: `R.drawable.ic_launcher_foreground` -> initials-based cross-platform placeholders
  - L04: `R.drawable` + `painterResource` -> `Box` with Material 3 colors; `Build.VERSION.SDK_INT` dynamic color labeled as `androidMain` with `commonMain` alternative added
  - L07: Retrofit/OkHttp sections titled "Android-Only" with note pointing to Ktor Client; `AndroidManifest.xml` labeled Android-only
  - L08: Room titled "Android-Only" with note pointing to SQLDelight; DataStore titled "Android-Only" with note pointing to multiplatform-settings
  - L09: Hilt DI titled "Android-Only" with note pointing to Koin; `@HiltViewModel`/`@Inject` labeled, Koin cross-platform alternative shown; `hiltViewModel()` removed from code presented as shared
- **No deprecated Compose patterns found**: Zero instances of `Modifier.preferredSize`, `Ambients`, or `Providers`.
- **Material 3**: Used throughout (MaterialTheme.typography.*, MaterialTheme.colorScheme.*).
- **Coil 3**: L07/09-theory uses `coil3.compose.AsyncImage` (CMP-compatible, correct).
- **Navigation**: L06 uses `org.jetbrains.androidx.navigation` (CMP navigation library, correct).
- **Result**: 10 lessons, 201 content files reviewed, 14 corrections applied across 14 files.

## Verification Results

| Check | Result |
|-------|--------|
| All 15 M06 lessons reviewed for Ktor 3.4.0 accuracy | PASS |
| Forward references correct post-reorder (grep "previous lesson", "earlier") | PASS |
| No deprecated Ktor 2.x patterns ("install(Routing)", ".features") | PASS |
| Application.module() confirmed as Ktor 3.x pattern (not deprecated) | PASS |
| Exposed dependency declarations = 1.0.0 | PASS |
| Exposed v1 namespace used in implementation files | PASS |
| All 10 M07 lessons reviewed for CMP 1.10.x accuracy | PASS |
| No deprecated Compose patterns (preferredSize, Ambients, Providers) | PASS |
| Zero R.drawable references remaining in shared code | PASS |
| hiltViewModel() removed from code presented as commonMain | PASS |
| Android-only APIs annotated with platform labels | PASS |

## Deviations from Plan

### Auto-fixed Issues

**1. [Rule 2 - Missing Critical] M07 Android-only API annotations**

- **Found during:** Task 2
- **Issue:** M07 content files presented Android-only APIs (Hilt, Room, Retrofit, R.drawable, Build.VERSION) in code positioned as general/shared code, which would not compile in `commonMain` of a CMP project.
- **Fix:** Added "Android-only" labels to section titles, added blockquote notes pointing to cross-platform alternatives (Koin, SQLDelight, Ktor Client), replaced R.drawable with cross-platform UI patterns, removed hiltViewModel() from shared code examples.
- **Files modified:** 14 files across L02, L03, L04, L07, L08, L09
- **Commits:** c663fdf5 (Task 1), 7739dabf (Task 2)

## Commits

| Task | Commit | Description |
|------|--------|-------------|
| 1 | c663fdf5 | fix(06-03): accuracy pass M06 Ktor 3.4.0 and Exposed 1.0.0 (15 lessons) |
| 2 | 7739dabf | fix(06-03): accuracy pass M07 Compose Multiplatform 1.10.x (10 lessons) |

## Notable Findings

### M07 Android-vs-CMP Content Architecture

Lessons 07-09 in Module 07 ("Mobile Development with Compose Multiplatform") primarily teach Android-specific libraries:
- **L07 Networking**: Retrofit/OkHttp as primary, Ktor Client as alternative section
- **L08 Local Storage**: Room + DataStore as primary, cross-platform notes appended
- **L09 MVVM**: Hilt DI as primary, Koin shown as alternative

This is a deliberate course design -- the Kotlin course teaches both Android-specific and cross-platform approaches, with dedicated modules for the cross-platform libraries (M08: SQLDelight, M10: Koin). The content is accurate for Android development but needed platform annotations for clarity in a module titled "Compose Multiplatform."

### Exposed Namespace Compatibility

M06 uses `org.jetbrains.exposed.v1.core.*` (new namespace) in L08 (the main implementation file) while other lessons show conceptual code without full import blocks. The v1 namespace is correct for Exposed 1.0.0. The old namespace (`org.jetbrains.exposed.sql.*`) still works via backward compatibility but the course correctly uses the new namespace.

## Next Phase Readiness

Plan 06-03 completes the M06-M07 accuracy pass. M06 is now verified as the most structurally sound backend module (post-reorder). M07's Android-only annotations will be important context for Plan 06-08 (cross-cutting concerns) which may need to assess whether the Android-specific content in M07 adequately serves the course's CMP narrative.
