# Phase 5: Flutter/Dart Course Audit - Research

**Researched:** 2026-02-03
**Domain:** Flutter 3.x/Dart 3.x course content (153 lessons), Dart Frog backend framework, Serverpod, Riverpod state management, Dart execution in WPF app
**Confidence:** HIGH (course structure verified via filesystem analysis; version discrepancies verified via content search and web research)

## Summary

The Flutter course contains 18 modules with 153 lessons, 1,580 content files, and 217 challenges (173 with solution.dart). It teaches a comprehensive path from Dart basics through Flutter widgets, state management (Riverpod), backend development (Dart Frog + Serverpod), testing, deployment, and a social chat capstone project. The course is the largest by lesson count among all six courses.

The most critical finding is a **massive version mismatch between the version manifest and both the actual course content and current stable releases**. The version manifest targets Flutter 3.27.x/Dart 3.6.x with Riverpod 2.x, GoRouter 14.x, and Serverpod 2.x. However: (a) some course content already references Flutter 3.38+ and Dart 3.10+ (especially setup and dot-shorthands lessons), (b) the current Flutter stable is 3.38.6/Dart 3.10.3, (c) Riverpod 3.0 was released September 2025 with significant breaking changes, (d) Serverpod 3.0 "Industrial" was released December 2025 with a completely new auth system and web server, (e) GoRouter is now at 17.0.1. This means the version manifest, much of the course content, AND the current ecosystem are all at different version levels, creating a three-way version decision.

The second critical finding is **severe structural problems** in Module 09 (Serverpod Production Backend), which has 19 lessons but 9 are [Archive] Firebase/Supabase legacy content. Module 10 (Backend Testing) has 3 misplaced lessons about device features and background tasks that are not related to testing. Module 11 (API Integration & Auth Flows) has 2 misplaced lessons about CI/CD and testing. These structural issues affect 14 of 153 lessons.

The third finding is the **Piston executor Dart version mismatch**: the WPF app's PistonExecutor maps Dart to version "2.19.6" (Dart 2.x, pre-null-safety era). When using local execution, `dart run` uses whatever SDK is installed locally. Challenges using Dart 3 features (records, patterns, sealed classes, class modifiers) will fail in Piston but work locally with a current Dart SDK.

**Primary recommendation:** Update the version manifest to Flutter 3.38.x/Dart 3.10.x as the target, since some content already references these versions. However, the Riverpod 2.x -> 3.x and Serverpod 2.x -> 3.x upgrades are massive undertakings that would require rewriting large portions of content. The pragmatic approach is: (1) target Flutter 3.38/Dart 3.10 for the core framework, (2) keep Riverpod at 2.x patterns with a migration note (Riverpod 3 is a "transition version" and 4.0 is expected), (3) keep Serverpod at 2.x with deprecation notes and update to 3.x patterns where feasible, (4) update Dart Frog content to reflect community maintenance status, (5) update GoRouter references to 17.x.

## Standard Stack

The established technologies for this course domain:

### Core Runtime (Version Decision Required)

| Technology | Manifest Version | Course Content Uses | Current Stable | Resolution Needed |
|------------|-----------------|---------------------|----------------|-------------------|
| Flutter | 3.27.x | Mixed (3.27, 3.29, 3.38+) | **3.38.6** | **YES -- content already references 3.38** |
| Dart | 3.6.x | Mixed (3.0, 3.6, 3.10+) | **3.10.3** | **YES -- dot shorthands lesson requires 3.10** |

**Flutter/Dart release timeline (critical context):**
- Flutter 3.27 / Dart 3.6 -- December 2024
- Flutter 3.29 / Dart 3.7 -- Q1 2025 (Impeller default on iOS, no Skia fallback)
- Flutter 3.32 / Dart 3.8 -- mid 2025
- Flutter 3.38 / Dart 3.10 -- November 2025 (dot shorthands, build hooks stable)
- Course `minimumRuntimeVersion` in course.json: "Flutter 3.27"
- Course content setup lesson: "The course is tested with Flutter 3.38+ and Dart 3.10+"

### Framework Stack (Major Version Conflicts)

| Technology | Manifest Version | Course Content Uses | Current Stable | Breaking Changes |
|------------|-----------------|---------------------|----------------|-----------------|
| Riverpod (flutter_riverpod) | 2.x | ^2.4.0 to ^2.6.0 | **3.2.0** | Unified Ref, legacy providers, AsyncValue changes |
| Riverpod Generator | 2.x | ^2.4.0 to ^2.6.0 | **4.0.2** | Part of Riverpod 3 ecosystem |
| GoRouter | 14.x | ^14.0.0 to ^17.0.0 (mixed) | **17.0.1** | Requires Flutter 3.32/Dart 3.8 minimum |
| Dart Frog | 1.x | ^1.0.0 | **1.2.6** | Minor version updates; community-maintained |
| Dart Frog CLI | 1.x | 1.x.x | **1.2.11** | Compatible within 1.x |
| Serverpod | 2.x | ^2.0.0 | **3.2.0** | New auth system (Relic), new web server, enum serialization |
| Drift | 2.x | ^2.14.0 | **2.30.1** | Compatible within 2.x (minor updates) |
| flutter_hooks | -- | ^2.5.1 (hooks_riverpod) | **0.21.3** | Tied to Riverpod version |

### Version Decision: What to Target

| Option | Pros | Cons |
|--------|------|------|
| A: Update to Flutter 3.38/Dart 3.10, keep framework versions at 2.x | Matches setup content; dot shorthands work; minimal Riverpod/Serverpod rewrite | Framework versions stale; students may encounter Riverpod 3 in the wild |
| B: Update everything to current (Flutter 3.38, Riverpod 3, Serverpod 3, GoRouter 17) | Fully current; no stale content | Massive rewrite of Riverpod module (10 lessons), Serverpod module (9+ lessons), capstone (12 lessons); Riverpod 4 may come soon |
| C: Update core + Dart Frog + GoRouter; add Riverpod 3 / Serverpod 3 migration notes | Balanced approach; current where it matters | Partial staleness; migration notes add complexity |

**Recommendation:** Option A with targeted updates. Update to Flutter 3.38/Dart 3.10 as the runtime target. Update GoRouter to 17.x (the content already has ^17.0.0 in some places). Update Dart Frog references to 1.2.x with community status documentation. Keep Riverpod at 2.x patterns (they still work with Riverpod 3.x with legacy imports) and add a "What's Coming" note about Riverpod 3. For Serverpod, keep the 2.x content but note that Serverpod 3.x is now available with a new auth system. The rationale: Riverpod 3.0 is explicitly described as a "transition version" with 4.0 expected "relatively soon," and Serverpod 3.0 is very new (December 2025). Rewriting 30+ lessons for APIs that may change again shortly is high risk.

## Architecture Patterns

### Course Module Progression
```
Module 01: Flutter Setup (5 lessons)                  -- Setup/tooling
Module 02: Dart Programming Basics (8 lessons)        -- Dart language fundamentals
Module 03: Flutter Widget Fundamentals (8 lessons)    -- StatelessWidget, Text, Image, Container, Column/Row
Module 04: Layouts and Scrolling (8 lessons)          -- ListView, GridView, Stack, responsive, Material 3
Module 05: User Interaction (5 lessons)               -- Buttons, forms, gestures, StatefulWidget
Module 06: MVVM Architecture with Riverpod (10 lessons) -- State management, code gen
Module 07: Navigation and Routing (9 lessons)         -- GoRouter, deep linking, nav patterns
Module 08: Dart Frog Backend Fundamentals (8 lessons) -- REST APIs, routing, middleware, JWT
Module 09: Serverpod Production Backend (19 lessons)  -- MEGA-MODULE: 9 Serverpod + 9 [Archive] Firebase + 1 [Archive] Supabase
Module 10: Backend Testing (8 lessons)                -- 5 testing + 3 MISPLACED (device features, background tasks)
Module 11: API Integration & Auth Flows (10 lessons)  -- 8 relevant + 2 MISPLACED (CI/CD, testing)
Module 12: Real-Time Features (6 lessons)             -- WebSockets, chat, presence
Module 13: Offline-First & Persistence (7 lessons)    -- Drift, sync engine
Module 14: Frontend Testing (7 lessons)               -- Unit, widget, golden, integration, TDD
Module 15: Advanced UI (9 lessons)                    -- Animations, responsive, accessibility, i18n
Module 16: Deployment & DevOps (8 lessons)            -- Build, store submission, CI/CD
Module 17: Production Operations (6 lessons)          -- Crash reporting, analytics, feature flags
Module 18: Capstone - Social Chat App (12 lessons)    -- Full-stack project
```

### Content File Distribution (1,580 files across 153 lessons)
| Type (Frontmatter) | Count | Notes |
|---------------------|-------|-------|
| THEORY | 861 | Primary content type |
| EXAMPLE | 449 | Includes 18 files with "experiment" filename but EXAMPLE frontmatter |
| KEY_POINT | 160 | Present in 109/153 lessons (71%) -- 44 lessons missing |
| WARNING | 73 | Present in some modules, absent from M05, M11-M13 |
| ANALOGY | 37 | **Severe gap**: only modules 01-02, 03, 04, 07, 09 have any |

**Non-standard filename count: 18 files** with "experiment" in filename but "EXAMPLE" type in frontmatter.

### Critical Structural Issues

**Issue 1: Module 09 Mega-Module (19 lessons)**
Module 09 "Serverpod Production Backend" contains:
- Lessons 01-09: Actual Serverpod content (setup, models, endpoints, ORM, auth, streams, storage, tasks, chat project)
- Lessons 10-17: **[Archive] Firebase content** (8 lessons marked [Archive] with [Legacy content] descriptions)
- Lesson 18: Serverpod mini-project (Chat Backend)
- Lesson 19: **[Archive] Supabase** (marked [Archive])

The archived lessons have NO challenges, but still count toward the 153 total. This module needs to either have the archived lessons removed/relocated or formally excluded from audit scope.

**Issue 2: Module 10 Misplaced Content**
Module 10 "Backend Testing" contains:
- Lessons 01-05: Actual backend testing content (philosophy, unit testing, Dart Frog tests, Serverpod tests, contract testing)
- Lesson 06: "Device Features (Sensors & Biometrics)" -- **NOT testing content**
- Lesson 07: "Background Tasks & Workmanager" -- **NOT testing content**
- Lesson 08: "Mini-Project: Fitness Tracker App" -- **NOT testing content**

**Issue 3: Module 11 Misplaced Content**
Module 11 "API Integration & Auth Flows" contains:
- Lessons 01-08: Actual API/auth content
- Lesson 09: "CI/CD for Flutter Apps" -- **NOT API/auth content** (belongs in M16)
- Lesson 10: "Testing Best Practices Mini-Project" -- **NOT API/auth content** (belongs in M14)

**Issue 4: Generic Module Titles**
6 of 18 modules still have generic "Module X: Flutter Development" titles:
- Module 01: "Module 0: Flutter Development" (should be "Flutter Setup & Environment")
- Module 02: "Module 1: Flutter Development" (should be "Dart Programming Basics")
- Module 03: "Module 2: Flutter Development" (should be "Flutter Widget Fundamentals")
- Module 04: "Module 3: Flutter Development" (should be "Layouts and Scrolling")
- Module 05: "Module 4: Flutter Development" (should be "User Interaction")
- Module 07: "Module 6: Flutter Development" (should be "Navigation and Routing")

Note: The directory slugs (01-flutter-setup, 02-dart-programming-basics, etc.) ARE descriptive -- it's only the module.json title field that's generic.

### Content Coverage Gaps

**ANALOGY coverage (37 total, 8 modules have zero):**
| Module | Analogies | Lessons | Coverage |
|--------|-----------|---------|----------|
| 01-flutter-setup | 10 | 5 | 200% |
| 02-dart-programming-basics | 5 | 8 | 63% |
| 03-flutter-widget-fundamentals | 2 | 8 | 25% |
| 04-layouts-and-scrolling | 1 | 8 | 13% |
| 05-user-interaction | 1 | 5 | 20% |
| 06-mvvm-architecture-with-riverpod | 0 | 10 | 0% |
| 07-navigation-and-routing | 4 | 9 | 44% |
| 08-dart-frog-backend-fundamentals | 0 | 8 | 0% |
| 09-serverpod-production-backend | 14 | 19 | 74% (mostly from Firebase archive) |
| 10-backend-testing | 0 | 8 | 0% |
| 11-api-integration-and-auth-flows | 0 | 10 | 0% |
| 12-real-time-features | 0 | 6 | 0% |
| 13-offline-first-and-persistence | 0 | 7 | 0% |
| 14-frontend-testing | 0 | 7 | 0% |
| 15-advanced-ui | 0 | 9 | 0% |
| 16-deployment-and-devops | 0 | 8 | 0% |
| 17-production-operations | 0 | 6 | 0% |
| 18-capstone-social-chat-app | 0 | 12 | 0% |

Only modules 01-07 and 09 have ANY analogies. All modules from 08 onward (except 09's archived content) have zero. This is a systemic gap.

**KEY_POINT coverage (160 across 109/153 lessons):**
Missing in 44 lessons. Notable gaps:
- M05 (User Interaction): 0/5 lessons
- M12 (Real-Time): 0/6 lessons
- M13 (Offline-First): 0/7 lessons
- M18 (Capstone): 6/12 lessons

**WARNING coverage (73 across modules):**
Zero warnings in M11, M12, M13, M14. Modules M06, M16, M17 have reasonable coverage.

### Challenge Statistics
- 217 total challenges across 153 lessons
- 173 solution.dart files (44 challenges missing solutions)
- 184 starter.dart files
- 15 lessons have NO challenges at all:
  - M04 L07 (Material 3 theming)
  - M07 L03 (GoRouter)
  - M09 L10-L17 (8 [Archive] Firebase lessons)
  - M10 L06-L08 (3 misplaced lessons)
  - M11 L09-L10 (2 misplaced lessons)

### Code Execution Architecture (Dart)
The WPF app executes Dart challenges via two paths:
1. **Piston API** (Docker container): Maps Dart to version **2.19.6** (Dart 2.x). Does NOT support Dart 3 features (records, patterns, sealed classes, class modifiers, null safety enforcement).
2. **Local execution** (`dart run`): Uses whatever Dart SDK is installed locally via `InteractiveProcessSession`. The command is `dart run <tempfile.dart>`.

**Critical implications:**
- Piston path: ANY challenge using Dart 3 features will fail (records, patterns, sealed classes, `switch` expressions, class modifiers)
- Local path: Works if user has current Dart SDK installed
- Challenges for backend modules (Dart Frog, Serverpod) cannot execute in either path (require project context, dependencies, database)
- Only Modules 01-07 challenges (pure Dart/Flutter logic) can realistically execute
- Module 02 L08 "Dart 3 Modern Features" challenges specifically teach records, patterns, and sealed classes -- these WILL fail on Piston

### Anti-Patterns to Avoid
- **Attempting Riverpod 3 migration:** Riverpod 3 is a "transition version" with 4.0 expected; large rewrite for unstable target
- **Attempting Serverpod 3 migration:** Serverpod 3 has new auth module, new web server, enum serialization changes -- too many moving parts for a content audit
- **Ignoring the 14 misplaced/archived lessons:** These inflate metrics and create false progression gaps
- **Treating all 217 challenges as executable:** Only ~80 (Modules 01-07) can realistically run
- **Adding analogies in the accuracy pass:** Analogy coverage is a massive gap (120+ lessons need analogies) and should be its own plan

## Don't Hand-Roll

Problems that have existing solutions -- do not create custom approaches:

| Problem | Don't Build | Use Instead | Why |
|---------|-------------|-------------|-----|
| Generic module title fix | Manual JSON editing | Scripted batch update of 6 module.json files | Same fix applied to other courses in Phase 1 |
| Version reference sweep | Reading each file manually | Grep/search across 1,580 content files | Automated sweep catches all version strings |
| KEY_POINT generation | Manual per-lesson | Batch template with per-lesson customization | 44 new KEY_POINT files needed |
| Content type verification | Manual frontmatter check | E2E validation tests from Phase 1 | CourseContentValidationTests.cs validates all 6 standard types |
| Dart Frog API verification | Manual testing | Check dart_frog package changelog + pub.dev | API has been stable since 1.0; verify specific imports used in course |
| Challenge validation (pure Dart) | Manual review | Execute solution.dart files via `dart run` | 80+ challenges can be batch-validated |
| Challenge validation (backend) | Trying to execute | Manual code review only | Dart Frog/Serverpod challenges need project context |
| Non-standard filename fix | Manual rename | Scripted batch rename (18 experiment files) | Same approach used for C# and JS courses |

**Key insight:** The Flutter course has significantly MORE structural issues than the prior three courses (14 misplaced/archived lessons, 6 generic module titles, massive analogy gap). But the content itself (where it exists and is on-topic) appears reasonably well-written with good code examples. The version staleness is the primary technical issue.

## Common Pitfalls

### Pitfall 1: Three-Way Version Mismatch
**What goes wrong:** The manifest says Flutter 3.27, some content says Flutter 3.38, and current stable is Flutter 3.38.6. Auditors may use any of these as the reference, leading to inconsistent corrections.
**Why it happens:** Content was written incrementally -- setup lessons were updated to 3.38 but deeper modules retained earlier version references. The manifest was set during Phase 1 and never updated.
**How to avoid:** Resolve the version target FIRST (recommend Flutter 3.38/Dart 3.10 to match the most recent content), update the manifest, THEN audit content.
**Evidence:**
- course.json: `minimumRuntimeVersion: "Flutter 3.27"`
- Setup lesson 01-01: "The course is tested with Flutter 3.38+ and Dart 3.10+"
- Dot shorthands lesson requires Dart 3.10
- GoRouter lesson says "Current version: 17.0.0 (Flutter 3.29+, Dart 3.7+)"

### Pitfall 2: Riverpod 2.x Code Still Works (But Is "Legacy")
**What goes wrong:** Auditors may flag working Riverpod 2.x code as "incorrect" because Riverpod 3.x is released.
**Why it happens:** Riverpod 3.x maintains backward compatibility for 2.x patterns via `flutter_riverpod/legacy.dart` imports. The APIs work but are considered "legacy."
**How to avoid:** Decide explicitly: teach Riverpod 2.x Notifier patterns (which still work) OR rewrite for Riverpod 3.x unified Ref. Document the decision. If keeping 2.x, add a note about Riverpod 3.
**Impact:** 10 lessons in Module 06, plus all Riverpod usage in M11, M12, M18 (capstone).

### Pitfall 3: Serverpod 2.x vs 3.x Auth System
**What goes wrong:** Students following Serverpod setup content encounter Serverpod 3.x CLI but course teaches 2.x patterns. The auth module has completely changed.
**Why it happens:** `dart pub global activate serverpod_cli` installs the latest (3.x), but course content teaches 2.x APIs.
**How to avoid:** Either pin Serverpod version in content (e.g., `serverpod_cli ^2.0.0`) OR update content to 3.x patterns. At minimum, add a warning about the version mismatch.
**Impact:** 9 Serverpod lessons in Module 09, plus M11, M12, M18 capstone.

### Pitfall 4: Piston Dart 2.19.6 Cannot Run Dart 3 Code
**What goes wrong:** Challenges using Dart 3 features (records, patterns, sealed classes, `switch` expressions) fail when executed via Piston.
**Why it happens:** PistonExecutor hardcodes Dart version as "2.19.6" which is Dart 2.x.
**How to avoid:** Either (a) update Piston Dart version to 3.x (app change, possibly out of scope), or (b) ensure challenge validation tests use local `dart run` instead of Piston, or (c) accept that Dart 3 challenges only work via local execution.
**Affected challenges:** Module 02 L08 (Dart 3 features), any challenge using records/patterns/sealed.

### Pitfall 5: Archived Firebase Content Inflates Metrics
**What goes wrong:** Counting 153 lessons and expecting all to be auditable. Actually 139 are live content and 14 are archived/misplaced.
**Why it happens:** [Archive] lessons were kept in the directory structure rather than removed.
**How to avoid:** Explicitly exclude [Archive] lessons from audit scope. Count active lessons separately.
**Scope:** 9 archived + 5 misplaced = 14 lessons not part of the active curriculum. Active count: ~139 lessons.

### Pitfall 6: Massive Analogy Gap
**What goes wrong:** Planning analogy creation as part of the accuracy pass, vastly underestimating the scope.
**Why it happens:** Modules 06-18 (except some Firebase archive content) have virtually zero analogies. That's ~120 lessons needing analogies.
**How to avoid:** Make analogy addition a separate plan or accept the gap. Prior courses (Java, JS, C#) had much better analogy coverage.

## Code Examples

### Current Dart Frog Route Handler Pattern (verified in course content)
```dart
// routes/hello.dart -> GET /hello
import 'package:dart_frog/dart_frog.dart';

Response onRequest(RequestContext context) {
  return Response(body: 'Hello, World!');
}
```
This pattern is still correct for dart_frog 1.2.x. The `RequestContext` and `Response` APIs are unchanged.

### Riverpod 2.x Notifier Pattern (current course content)
```dart
class CounterNotifier extends Notifier<int> {
  @override
  int build() => 0;

  void increment() => state++;
}

final counterNotifierProvider = NotifierProvider<CounterNotifier, int>(() {
  return CounterNotifier();
});
```
This pattern still works in Riverpod 3.x but is considered the "legacy" way. Riverpod 3.x unifies `Ref` (no type parameter), merges `AutoDisposeNotifier` into `Notifier`, and changes `AsyncValue.value` behavior.

### Riverpod 3.x Equivalent (for reference, NOT recommended for migration)
```dart
// In Riverpod 3.x, AutoDisposeNotifier is just Notifier
// Ref has no type parameter
// StateNotifierProvider is in legacy imports
import 'package:flutter_riverpod/flutter_riverpod.dart';
// OR for legacy: import 'package:flutter_riverpod/legacy.dart';
```

### Dart 3.10 Dot Shorthands (already in course content, correct)
```dart
// Before Dart 3.10:
Column(
  mainAxisAlignment: MainAxisAlignment.center,
  crossAxisAlignment: CrossAxisAlignment.start,
)

// Dart 3.10+:
Column(
  mainAxisAlignment: .center,
  crossAxisAlignment: .start,
)
```

### Serverpod 2.x Setup (current course content)
```bash
dart pub global activate serverpod_cli
serverpod create social_chat
```
In Serverpod 3.x, the CLI and project structure are similar, but the auth module has been completely reworked and the web server uses "Relic" instead of Shelf.

## State of the Art

| Old Approach (Course Content) | Current Approach | When Changed | Impact on Course |
|-------------------------------|------------------|--------------|-----------------|
| Flutter 3.27 / Dart 3.6 | Flutter 3.38 / Dart 3.10 | Nov 2025 | HIGH -- setup content already updated; dot shorthands lesson requires 3.10 |
| Riverpod 2.x (flutter_riverpod ^2.4.0) | Riverpod 3.x (flutter_riverpod ^3.2.0) | Sep 2025 | MEDIUM -- 2.x patterns work via legacy import; 10+ lessons affected |
| Serverpod 2.x (serverpod ^2.0.0) | Serverpod 3.x (serverpod ^3.2.0) | Dec 2025 | HIGH -- new auth system, but very new; 9+ lessons affected |
| GoRouter 14.x | GoRouter 17.x | 2025 | LOW -- API similar; requires Flutter 3.32+ minimum SDK |
| Dart Frog under VGV | Dart Frog under community (dart-frog-dev) | Jul 2025 | LOW -- API unchanged (1.0.0 -> 1.2.6); governance changed |
| Drift ^2.14.0 | Drift ^2.30.1 | 2025 | LOW -- compatible within 2.x line |
| Material 3 as option | Material 3 as default (since Flutter 3.16) | Nov 2023 | NONE -- course already teaches Material 3 correctly |
| WillPopScope | PopScope (since Flutter 3.12) | 2023 | NONE -- course already teaches PopScope migration |
| Impeller optional | Impeller default (iOS no fallback since 3.29, Android API 29+ since 3.27) | 2025 | LOW -- course mentions Impeller correctly in setup |

**Deprecated/outdated in course:**
- Riverpod ^2.4.0-^2.6.0: Still functional via legacy imports in 3.x, but version constraint strings should be updated
- Serverpod ^2.0.0: Functional but new projects will get 3.x CLI
- hooks_riverpod ^2.5.1: Should be ^3.2.0 if updating Riverpod
- go_router ^14.0.0 (in capstone): Should be ^17.0.0 (already ^17.0.0 in some GoRouter lessons)
- Drift ^2.14.0: Should be ^2.30.1 (compatible update)
- sdk: '>=3.0.0 <4.0.0' in pubspec examples: Should be '>=3.10.0 <4.0.0' if targeting Dart 3.10
- Piston Dart 2.19.6: Cannot run Dart 3 code

## Dart Frog Verification (FLTR-05)

### Current Status (HIGH confidence)
- **Dart Frog** transitioned to community maintenance in **July 2025**
- New GitHub organization: **dart-frog-dev** (previously under VeryGoodOpenSource)
- Same core maintainers from VGV era (Alejandro Santiago, Felix Angelov, et al.)
- VGV engineers continue contributing as community members
- **No breaking API changes** since the transition
- Current versions: dart_frog 1.2.6, dart_frog_cli 1.2.11

### API Verification
The course teaches these Dart Frog APIs (Module 08, 8 lessons):
1. `dart pub global activate dart_frog_cli` -- STILL CORRECT
2. `dart_frog create` -- STILL CORRECT
3. `dart_frog dev` -- STILL CORRECT
4. File-based routing (`routes/`) -- STILL CORRECT
5. `RequestContext`, `Response` -- STILL CORRECT
6. Middleware via `_middleware.dart` -- STILL CORRECT
7. Database integration patterns -- STILL CORRECT (uses standard Dart packages)
8. JWT authentication -- STILL CORRECT

### What Needs Updating
1. **Community status documentation:** The course should mention that Dart Frog is now community-maintained (was VGV)
2. **Version reference:** dart_frog: ^1.0.0 should be ^1.2.0 or ^1.0.0 (compatible)
3. **Serverpod relationship:** The course has a "Dart Frog vs Serverpod" comparison lesson (M09 L01) that should be reviewed for accuracy given Serverpod's major 3.0 release
4. **No API rewrites needed:** The dart_frog API surface is stable and unchanged

### Serverpod Relationship
The course teaches both Dart Frog (lightweight, learning) and Serverpod (production). The comparison lesson (M09 L01) frames this as:
- Dart Frog = "Swiss Army knife" for quick jobs
- Serverpod = "full workshop" for production

This framing is still valid but the comparison details need updating for Serverpod 3.x capabilities (new Relic web server, new auth system, polymorphism support).

## Open Questions

Things that couldn't be fully resolved:

1. **Should archived Firebase lessons be removed or kept?**
   - What we know: 9 lessons are marked [Archive] with [Legacy content] descriptions
   - What's unclear: Whether the app displays [Archive] lessons to users or hides them
   - Recommendation: Check if the app has filtering logic; if not, consider removing archived content from the lesson list

2. **Should Riverpod be updated to 3.x or kept at 2.x?**
   - What we know: Riverpod 3.0 is a "transition version" and 4.0 is expected; 2.x patterns work via legacy import
   - What's unclear: How soon Riverpod 4.0 will release; whether teaching 2.x patterns is acceptable for students
   - Recommendation: Keep 2.x patterns, update version constraints to ^3.2.0 (which accepts 2.x API via legacy imports), add migration note

3. **Should Serverpod content be updated to 3.x?**
   - What we know: Serverpod 3.0 released Dec 2025; new auth system; migration tools available
   - What's unclear: Whether 2.x content is still installable/usable; whether students will encounter 3.x CLI
   - Recommendation: Keep 2.x content with strong version pinning and add deprecation notes; full 3.x migration is a future cycle task

4. **What to do with misplaced lessons (M10 L06-L08, M11 L09-L10)?**
   - What we know: 5 lessons are in wrong modules (device features in testing, CI/CD in auth)
   - What's unclear: Whether moving lessons between modules causes ID/reference breakage
   - Recommendation: Move or relabel; check lesson IDs for cross-references

5. **Is there a standalone capstone project?**
   - What we know: Module 18 teaches building a social chat app across 12 lessons, but no standalone project directory exists (no pubspec.yaml)
   - What's unclear: Whether FLTR-04 requires a runnable project or just well-documented lesson content
   - Recommendation: The capstone exists as lesson content; a standalone project scaffold may need to be created for FLTR-04

## Sources

### Primary (HIGH confidence)
- Course filesystem analysis: 18 modules, 153 lessons, 1,580 content files, 217 challenges verified
- version-manifest.json: Flutter 3.27.x/Dart 3.6.x target (needs updating)
- course.json: minimumRuntimeVersion "Flutter 3.27" (needs updating)
- PistonExecutor.cs: Dart version hardcoded to "2.19.6"
- CodeExecutionService.cs: Local Dart execution via `dart run`

### Secondary (MEDIUM confidence)
- [Flutter 3.38 release notes](https://docs.flutter.dev/release/release-notes/release-notes-3.38.0) -- Flutter 3.38.6 / Dart 3.10.3 current
- [Dart 3.10 dot shorthands](https://dart.dev/language/dot-shorthands) -- Feature docs
- [Riverpod 3.0 migration guide](https://riverpod.dev/docs/3.0_migration) -- Breaking changes documented
- [Serverpod 3.0 "Industrial"](https://medium.com/serverpod/serverpod-3-industrial-robust-authentication-and-a-new-web-server-5b1152863beb) -- New auth and web server
- [Dart Frog community transition](https://www.verygood.ventures/blog/dart-frog-has-found-a-new-pond) -- July 2025 transition to dart-frog-dev
- [GoRouter 17.0.1](https://pub.dev/packages/go_router) -- Current version on pub.dev
- [Drift 2.30.1](https://pub.dev/packages/drift) -- Current version on pub.dev
- [Material 3 migration](https://docs.flutter.dev/release/breaking-changes/material-3-migration) -- Material 3 default since Flutter 3.16
- [Impeller status](https://docs.flutter.dev/perf/impeller) -- Default on iOS (no fallback) and Android API 29+

### Tertiary (LOW confidence)
- Piston Dart 3.x support: Could not verify exact versions available in Piston package repository. The 2.19.6 version in PistonExecutor.cs is pre-Dart-3 and likely does not support records/patterns/sealed classes. Self-hosted Piston may support newer versions via custom packages.
- Riverpod 4.0 timeline: Author Remi Rousselet mentioned 4.0 is expected "relatively soon" but no specific date
- Serverpod 3.x adoption: Very new (Dec 2025), unclear how many projects have migrated

## Metadata

**Confidence breakdown:**
- Standard stack: HIGH -- versions verified via pub.dev and official docs
- Course structure: HIGH -- filesystem analysis complete, all 153 lessons examined
- Architecture/progression: HIGH -- module contents, lesson titles, content types all analyzed
- Dart Frog status: HIGH -- community transition well-documented with official blog posts
- Pitfalls: HIGH -- version conflicts verified, execution architecture confirmed via source code
- Riverpod 3 migration scope: MEDIUM -- breaking changes documented but practical impact on course content estimated
- Serverpod 3 migration scope: MEDIUM -- new features documented but course-specific impact estimated

**Research date:** 2026-02-03
**Valid until:** 2026-03-03 (30 days -- Flutter/Dart ecosystem moves fast but within a stable release cycle)
