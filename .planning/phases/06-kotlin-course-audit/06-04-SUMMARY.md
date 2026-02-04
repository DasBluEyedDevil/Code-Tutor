# Phase 6 Plan 04: M08-M12 Accuracy Pass Summary

**One-liner:** SQLDelight 2.2.x / Koin 4.1.x / kotlin.test verified across 42 lessons; capstone is Jetpack Compose (not CMP) with AndroidViewModel

## Completion

- **Tasks:** 2/2
- **Duration:** 8 min
- **Completed:** 2026-02-04
- **Commit:** 877a5b77

## What Was Done

### Task 1: Accuracy pass M08-M10 (21 lessons)

**M08 SQLDelight 2.2.x (7 lessons):** Verified accurate.
- Plugin DSL: `app.cash.sqldelight` (not old `com.squareup.sqldelight`)
- Version catalog: `sqldelight = "2.2.1"` with proper library declarations
- Platform drivers: `AndroidSqliteDriver`, `NativeSqliteDriver`, `JdbcSqliteDriver` all correctly imported from `app.cash.sqldelight.driver.*`
- `.sq` files with proper named query syntax
- `.sqm` migration files with versioned naming
- Reactive queries: `asFlow().mapToList(Dispatchers.IO)` from `app.cash.sqldelight.coroutines.*`
- `generateAsync.set(true)` in Gradle config
- Zero legacy 1.x patterns

**M09 KMP Architecture (7 lessons):** Verified accurate.
- Pure Kotlin ViewModel pattern (no `androidx.lifecycle.ViewModel` in shared code)
- `CoroutineScope(Dispatchers.Main + SupervisorJob())` for scope management
- `MutableStateFlow` / `StateFlow` for state
- MVVM and MVI patterns implemented without Android-specific deps
- `androidx.lifecycle.ViewModel` mentioned only in comparison table (L05) explaining platform differences -- intentional and educational
- Navigation patterns use Compose Multiplatform, not Android Navigation Component

**M10 Koin 4.1.x (7 lessons):** Verified accurate.
- Version: `koin = "4.1.1"` in version catalog
- Dependencies: `io.insert-koin:koin-core`, `koin-compose`, `koin-compose-viewmodel`, `koin-android`, `koin-test`
- DSL: `module { single { } factory { } viewModel { } scoped { } }` -- all current
- KMP: `startKoin` in commonMain, `androidContext(this)` only in androidMain
- Compose: `koinViewModel<T>()`, `koinInject<T>()` from `org.koin.compose.*`
- Annotations: `@Single`, `@Factory`, `@KoinViewModel` (koin-annotations)
- Testing: `startKoin` + `stopKoin` pattern
- Zero Koin 3.x patterns (no `androidContext` in shared code)

### Task 2: Accuracy pass M11-M12 (21 lessons)

**M11 Testing KMP (7 lessons):** Verified accurate.
- Uses `kotlin.test` (`@Test`, `assertEquals`, `assertFailsWith`, `assertNotNull`, `assertTrue`)
- Uses `kotlinx-coroutines-test`: `runTest`, `StandardTestDispatcher(testScheduler)`, `UnconfinedTestDispatcher`
- Zero deprecated `runBlockingTest`
- Zero `@org.junit` or `Assert.assert` in shared test code
- MockK used for mocking (KMP-compatible)
- Compose testing uses `compose.uiTestJUnit4` as test runner (correct -- Compose test framework requires JUnit4 runner on JVM/Android)
- Flow testing with Turbine (`awaitItem()`, `cancelAndConsumeRemainingEvents()`)

**M12 Deployment (14 lessons):** Reviewed with 2 fixes.
- GitHub Actions, Docker, Heroku, AWS, GCP deployment covered
- JUnit 5 (`org.junit.jupiter`) used in backend/JVM tests -- appropriate for Ktor server testing
- Docker Compose `version: '3.8'` removed from 2 files (deprecated in Compose V2)
- `docker-compose` commands updated to `docker compose` (V2 syntax)
- CI/CD uses `actions/checkout@v4`, `actions/setup-java@v4`, `actions/upload-artifact@v4` -- current

**M12 L08 Capstone Assessment (CRITICAL for Plan 08):**

| Question | Finding |
|----------|---------|
| How much code/content exists? | 14 THEORY files, substantial inline code (Ktor routes, ViewModels, tests, CI/CD, Docker, deployment) |
| Compose Multiplatform or Jetpack Compose? | **Jetpack Compose only** -- `androidx.lifecycle.ViewModel`, `viewModelScope`, Android-only project structure |
| Technologies described? | Ktor backend + Jetpack Compose Android app + PostgreSQL + Stripe + JWT + Docker + Heroku |
| Can it be reframed as case study? | Yes -- the backend (Ktor) and general architecture are valid. Only the Android client needs CMP reframing. Existing code can serve as "Android-specific" reference while Plan 08 creates CMP-aligned capstone |
| CMP alignment? | **NOT aligned** -- zero shared code, zero KMP structure, Android-only ViewModel pattern |

**Additional capstone notes:**
- lesson.json says 30 min, content says 12-16 hours (metadata discrepancy)
- ShopKotlin project has no standalone directory (all code inline in content files)
- Extension challenges mention Room database for offline mode (Android-only)
- "What's Next?" section already suggests "Learn Compose Multiplatform (desktop, web)" -- acknowledges CMP is separate from capstone

## Deviations from Plan

### Auto-fixed Issues

**1. [Rule 1 - Bug] Docker Compose V2 syntax updates**
- **Found during:** Task 2
- **Issue:** M12 L05 and L08 used deprecated `version: '3.8'` field and `docker-compose` command
- **Fix:** Removed version field, changed `docker-compose` to `docker compose` (4 command updates)
- **Files modified:** M12 L05 `09-theory.md`, M12 L08 `10-theory.md`
- **Commit:** 877a5b77

## Decisions Made

| Decision | Rationale |
|----------|-----------|
| M08 LiveData in comparison table kept | Describes Room's capabilities, not teaching LiveData for SQLDelight |
| M09 `androidx.lifecycle.ViewModel` mention kept | In comparison table explaining platform differences -- educational context |
| M10 `androidContext` references kept | All in androidMain code blocks -- correct platform-specific Koin usage |
| M11 `uiTestJUnit4` kept | Correct dependency for Compose UI test runner on JVM/Android |
| M12 JDK 17 in CI/CD kept | Valid for Kotlin/Ktor deployment; version manifest pins Kotlin version, not JVM |
| M12 Heroku references kept | Still a valid deployment target for learning; alternatives (AWS, GCP) also covered |
| M12 L08 capstone NOT reframed | Plan 08 will handle CMP capstone creation; documenting findings is sufficient for now |

## Verification Results

| Check | Result |
|-------|--------|
| All 42 lessons across M08-M12 reviewed | PASS (7+7+7+7+14) |
| SQLDelight 2.2.x verified | PASS (plugin DSL, drivers, .sq/.sqm, reactive) |
| Koin 4.1.x verified | PASS (module DSL, annotations, KMP, Compose integration) |
| Testing uses kotlin.test (not JUnit in shared code) | PASS (zero @org.junit in M11 shared tests) |
| M12 L08 capstone assessed for CMP alignment | PASS (documented: NOT CMP-aligned, Jetpack Compose only) |
| No deprecated runBlockingTest | PASS (all use runTest) |
| No BroadcastChannel | PASS (zero occurrences) |

## Key Findings for Downstream Plans

### For Plan 08 (Capstone):
- M12 L08 ShopKotlin is Android-only (Jetpack Compose + AndroidViewModel)
- Backend (Ktor) portion is reusable -- clean REST API patterns
- Inline code exists but no standalone project directory
- CMP capstone needs: shared business logic, shared UI, platform-specific drivers
- Consider reframing existing L08 as "Android Reference Implementation" while Plan 08 creates proper CMP capstone

### Content file counts:
- M08: 49 files (7 lessons)
- M09: 45 files (7 lessons)
- M10: 54 files (7 lessons)
- M11: 43 files (7 lessons)
- M12: 200 files (14 lessons)
- **Total: 391 files reviewed**
