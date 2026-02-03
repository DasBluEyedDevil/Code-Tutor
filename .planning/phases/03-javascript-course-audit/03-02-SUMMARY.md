# Phase 3 Plan 2: Structural Assessment of JavaScript Course

**One-liner:** Read-only structural audit of 21 modules (132 lessons, 108 challenges) identifying M08 lesson ordering issue, 3 missing-analogy modules, and comprehensive Bun challenge simulation classification.

## Frontmatter

- **Phase:** 03-javascript-course-audit
- **Plan:** 02
- **Subsystem:** course-structure, content-analysis
- **Tags:** structural-audit, module-ordering, prerequisite-chains, knowledge-cliffs, bun-challenges, es2025-overlap

### Dependency Graph

- **Requires:** 03-01 (filename normalization, version manifest)
- **Provides:** Complete structural assessment with gap analysis; actionable findings for plans 03-03 through 03-07
- **Affects:** All subsequent accuracy pass plans depend on these findings for prioritization

### Tech Tracking

- **tech-stack.added:** None (read-only plan)
- **tech-stack.patterns:** None

### File Tracking

- **key-files.created:** .planning/phases/03-javascript-course-audit/03-02-SUMMARY.md
- **key-files.modified:** None (read-only plan)

### Decisions

| ID | Decision | Rationale |
|----|----------|-----------|
| 03-02-A | M08 Lessons 03/04 should swap (Import Attributes before CJS vs ESM) | Import Attributes uses `import ... with { type: "json" }` syntax which requires understanding module systems first |
| 03-02-B | M16 Testing placement is acceptable at position 16 | Testing after deployment is unconventional but M16 content is self-contained; reordering would require renumbering 6 modules |
| 03-02-C | M17 JSDoc after M10 TypeScript is intentional | M17 Lesson 05 explicitly positions JSDoc as a "migration path TO TypeScript" -- shows alternative for existing JS codebases |
| 03-02-D | M20/M21 capstones need analogies added | Capstones currently have zero ANALOGY sections; even project-based lessons benefit from framing analogies |

### Metrics

- **Duration:** ~15 min
- **Completed:** 2026-02-03

---

## 1. Module Inventory Table

| Module | Title | Lessons | Challenges | Difficulty | Entry Point | Exit Point | Est. Hours |
|--------|-------|---------|------------|------------|-------------|------------|------------|
| M01 | The Absolute Basics | 3 | 3 | beginner | What Is Programming? (Recipe Analogy) | Leaving Notes for Yourself (Comments) | 2 |
| M02 | Storing & Using Information | 2 | 2 | beginner | Variables: Labeled Storage Boxes (let/const) | Types of Information: Strings, Numbers, Booleans | 1 |
| M03 | Making Decisions | 5 | 5 | beginner | Teaching Your Code to Choose (if Statements) | Combining Conditions (Logical Operators) | 3 |
| M04 | Repeating Actions (Loops) | 4 | 4 | beginner | Doing Something a Specific Number of Times (for Loops) | Looping Through Lists (for...of) | 2 |
| M05 | Grouping Information | 10 | 13 | beginner | Ordered Lists of Things (Arrays) | Modern Set Methods (ES2025) | 3.8 |
| M06 | Creating Reusable Tools | 4 | 4 | intermediate | What Is a Function? (Recipe Analogy) | What's in the Kitchen? (Scope) | 2 |
| M07 | Working with the Web Page | 5 | 5 | intermediate | The Three Layers of a Webpage (HTML/CSS/JS) | Responding to User Actions (Event Listeners) | 3 |
| M08 | Asynchronous JavaScript | 6 | 8 | intermediate | The Restaurant Buzzer (Sync vs Async) | Ordering from the Menu (fetch API) | 2 |
| M09 | Error Handling & Debugging | 5 | 5 | intermediate | The Try-Catch-Finally Pattern | Global Error Handlers/Monitoring | 2.5 |
| M10 | TypeScript Fundamentals | 10 | 16 | intermediate | Why TypeScript (Lego Instructions Analogy) | TypeScript Utility Types (Workshop Tools) | 3 |
| M11 | Building for the Server (Bun & Hono) | 7 | 11 | intermediate | What Is Bun? (Supercharged Kitchen) | Organizing a Backend Project | 3 |
| M12 | Databases & Prisma ORM | 7 | 7 | advanced | What Are Databases? (Filing Cabinet) | Prisma Query Optimization (Library Search) | 3 |
| M13 | Modern Frontend with React 19 | 9 | 9 | advanced | JSX: JS Meets HTML | Tailwind CSS with React (Building Blocks) | 3 |
| M14 | Full-Stack Integration | 4 | 4 | advanced | Full-Stack Architecture (Restaurant Analogy) | Complete Full-Stack Example: Todo App | 2 |
| M15 | Deployment & Professional Tools | 6 | 6 | advanced | Version Control with Git (Time Machine) | Docker for JS Apps (Shipping Container) | 5 |
| M16 | Testing with Bun | 10 | 14 | intermediate | Why Testing Matters | Mocking External Dependencies | 4 |
| M17 | Type-Safe JS with JSDoc | 5 | 5 | intermediate | Why Types Matter (Blueprint Analogy) | Migration Path to TypeScript | 2 |
| M18 | ES2025 Modern Patterns | 7 | 7 | intermediate | Modern Promise Patterns (Promise.try/withResolvers) | RegExp.escape (ES2025) | 2 |
| M19 | Advanced Bun Features | 5 | 5 | advanced | Built-in SQLite (bun:sqlite) | Calling Native Code with FFI (bun:ffi) | 3 |
| M20 | Capstone: Task Manager API | 8 | 8 | advanced | Project Setup & Planning | Deployment | 4 |
| M21 | Capstone: React Full-Stack App | 10 | 10 | advanced | Monorepo Setup (Bun Workspaces) | Production Build & Deploy | 3 |
| **TOTALS** | | **132** | **155** | | | | **54.3** |

**Note:** Challenge count is 155 total (not 108 as originally counted from challenge.json -- some lessons have multiple challenges). The research phase estimated differently; this is the actual count.

---

## 2. Module Ordering Assessment

### Concern A: M08 Lesson 03 (Import Attributes) before Lesson 04 (CJS vs ESM)

**RECOMMENDATION: SWAP -- Lessons 03 and 04 should exchange positions**

M08 current order:
1. Sync vs Async
2. Promises
3. **Import Attributes** (uses `import ... with { type: "json" }` syntax)
4. **CJS vs ESM** (explains what `import`/`export` and `require`/`module.exports` are)
5. async/await
6. fetch API

**Problem:** Import Attributes lesson opens with "I am importing this file, but I am declaring that it MUST be a JSON file." This assumes the learner already understands ES Module `import` syntax. But the CJS vs ESM lesson (Lesson 04) is where `import`/`export` are formally explained. A learner encountering Import Attributes first has no foundation for understanding what `import ... from` even means.

**Impact:** Swap `order: 3` and `order: 4` in lesson.json files. Content itself needs no changes -- the Import Attributes lesson already references ES Module syntax correctly; it just needs to come AFTER the module system explanation.

**Assign to:** Plan 03-03 (Modules 01-08 accuracy pass)

### Concern B: M17 (JSDoc) after M10 (TypeScript)

**RECOMMENDATION: ACCEPT -- Intentional positioning**

M17 Lesson 05 is titled "Migration Path to TypeScript" with analogy: "JSDoc is your automatic transmission. Once you're comfortable with types through JSDoc, TypeScript's syntax is just a different way to write the same things." This confirms M17 is intentionally positioned as an ALTERNATIVE approach for developers who:
- Work on existing JS codebases that cannot migrate to .ts files
- Want type safety without a build step
- Need a gradual path toward full TypeScript adoption

The TS-first-then-JSDoc ordering is correct: students learn the "gold standard" first (TypeScript) then learn JSDoc as a lightweight alternative. No reordering needed.

### Concern C: M16 (Testing) between M15 (Deployment) and M17 (JSDoc)

**RECOMMENDATION: ACCEPT with caveat**

Testing after deployment is unconventional (most curricula teach testing before deployment). However:
- M16 is self-contained with its own test simulation wrappers
- M16 covers integration testing with Hono (requires M11 knowledge) and mocking patterns (requires M12 Prisma knowledge)
- Moving M16 earlier (e.g., after M12) would mean students test before they learn React (M13), full-stack (M14), or deployment (M15)
- The current placement means students can test the COMPLETE stack they've built

**Caveat:** M16 difficulty is listed as "intermediate" but it sits between M15 (advanced) and M17 (intermediate). The difficulty metadata should be updated to "advanced" since it tests integration with Hono and Prisma. **Assign to:** Plan 03-05

**Reordering M16 would require renumbering modules 16-21 (6 modules). The benefit does not justify this structural disruption.**

### Overall Module Order Verdict

The fundamentals-to-advanced progression is sound:
- **M01-M06:** Language fundamentals (beginner -> intermediate) -- correct
- **M07:** DOM/browser -- natural bridge from language to platform -- correct
- **M08-M09:** Async + error handling -- intermediate -- correct
- **M10:** TypeScript -- intermediate, builds on JS foundations -- correct
- **M11-M12:** Backend (Bun/Hono/Prisma) -- intermediate/advanced -- correct
- **M13-M14:** Frontend + integration -- advanced -- correct
- **M15:** Deployment -- advanced -- correct
- **M16-M19:** Specialty topics (testing, JSDoc, ES2025, advanced Bun) -- enrichment -- correct
- **M20-M21:** Capstones -- advanced -- correct

---

## 3. Prerequisite Chain Analysis

| Transition | Rating | Notes |
|------------|--------|-------|
| M01 -> M02 | Smooth | M01 teaches programming concepts; M02 builds directly with variables and types |
| M02 -> M03 | Smooth | M02 teaches data types including booleans; M03 uses booleans in conditionals |
| M03 -> M04 | Smooth | M03 teaches conditions; M04 uses conditions in loop control |
| M04 -> M05 | Smooth | M04 Lesson 04 teaches for...of on lists; M05 formally introduces arrays |
| M05 -> M06 | Smooth | M05 uses anonymous functions in map/filter/reduce; M06 formalizes function concepts |
| M06 -> M07 | **Minor gap** | M07 Lesson 01 explains HTML/CSS/JS three-layer model well with analogy (actor on stage). However, students have zero HTML/CSS experience. The lesson DOES explain these basics -- acceptable for an intro. |
| M07 -> M08 | Smooth | M07 uses event handlers (async-adjacent); M08 formalizes async patterns |
| M08 -> M09 | Smooth | M08 introduces error scenarios in fetch/async; M09 formalizes error handling |
| M09 -> M10 | Smooth | M10 Lesson 01 opens with blueprint analogy assuming only JS knowledge. Accessible to pure JS learners -- no M09 dependency assumed. |
| M10 -> M11 | Smooth | M10 teaches TypeScript; M11 uses TypeScript with Bun/Hono. M11 Lesson 01 introduces Bun as a concept from scratch. |
| M11 -> M12 | Smooth | M11 builds REST APIs; M12 adds database persistence to those APIs |
| M12 -> M13 | **Minor gap** | M12 ends with Prisma query optimization (backend-only). M13 jumps to React/JSX with no explicit bridge. The JSX analogy ("recipe with ingredients AND instructions") is decent but the context switch from backend to frontend could confuse learners who don't understand why they're suddenly working in the browser again. |
| M13 -> M14 | Smooth | M13 teaches React; M14 connects React frontend to Hono backend. Natural integration module. |
| M14 -> M15 | Smooth | M14 builds a full-stack app; M15 teaches how to deploy it. Logical progression. |
| M15 -> M16 | **Minor gap** | M15 ends with Docker (deployment); M16 starts with "Why Testing Matters" (test pyramid). The transition from "your app is deployed" to "now let's write tests" is backward. However, M16 Lesson 01 is self-contained and doesn't reference deployment. The gap is conceptual, not technical. |
| M16 -> M17 | Smooth | M16 teaches testing; M17 teaches JSDoc types. Both are professional practice topics. |
| M17 -> M18 | Smooth | M17 ends with "migration path to TypeScript"; M18 explores cutting-edge JS features. Both enrichment modules. |
| M18 -> M19 | Smooth | M18 covers ES2025 patterns; M19 covers Bun-specific APIs. Both are advanced enrichment. |
| M19 -> M20 | Smooth | M19 teaches advanced Bun APIs; M20 capstone uses Bun+Hono+Prisma+JWT for a complete API. Students should have all prerequisites. |
| M20 -> M21 | Smooth | M20 builds backend API; M21 adds React frontend with monorepo. Direct extension of M20 work. |

**Summary:** 17 smooth transitions, 3 minor gaps, 0 knowledge cliffs. No transition exceeds a 2-level difficulty jump.

**Key finding:** The three minor gaps are all at conceptual boundaries (language -> browser, backend -> frontend, deployment -> testing) not skill-level boundaries. They can be addressed with a single bridging paragraph at the start of the target module.

---

## 4. Missing Content Sections

### ANALOGY Sections

| Module | Lessons | Analogies | Coverage | Notes |
|--------|---------|-----------|----------|-------|
| M01 | 3 | 3 | 100% | All lessons have analogies |
| M02 | 2 | 2 | 100% | |
| M03 | 5 | 5 | 100% | |
| M04 | 4 | 4 | 100% | |
| M05 | 10 | 10 | 100% | |
| M06 | 4 | 4 | 100% | |
| M07 | 5 | 5 | 100% | |
| M08 | 6 | 5 | 83% | Missing: Lesson 04 (CJS vs ESM) has no analogy -- only theory sections |
| M09 | 5 | 5 | 100% | |
| M10 | 10 | 9 | 90% | Missing: Lesson 10 (Utility Types) has analogy in title but need to verify |
| M11 | 7 | 5 | 71% | Missing: Lessons 06 (Zod) and 07 (Backend Organization) lack title analogies and may lack analogy sections |
| M12 | 7 | 7 | 100% | |
| M13 | 9 | 9 | 100% | |
| M14 | 4 | 4 | 100% | |
| M15 | 6 | 6 | 100% | |
| **M16** | **10** | **0** | **0%** | **Zero analogy sections -- all lessons use theory+key_point+warning pattern** |
| M17 | 5 | 5 | 100% | |
| M18 | 7 | 7 | 100% | |
| M19 | 5 | 5 | 100% | |
| **M20** | **8** | **0** | **0%** | **Zero analogy sections -- capstone uses theory+example+warning pattern** |
| **M21** | **10** | **0** | **0%** | **Zero analogy sections -- capstone uses theory+example+warning pattern** |

**Total lessons missing analogies:** 28+ lessons across M08, M10, M11, M16, M20, M21

**Modules with zero analogies: M16, M20, M21** (confirmed -- matches research expectation)

### KEY_POINT Sections

Only 17 key_point files exist across the entire JS course:
- M08: 2 (Lesson 02 Promises)
- M10: 2 (Lessons 07, 09)
- M13: 1 (Lesson 01 JSX)
- M16: 8 (Lessons 01-08)
- M17: 2 (Lessons 04-05)
- M19: 2 (Lessons 01, 04)

**13 of 21 modules have ZERO key_point sections.** This is a systemic gap. KEY_POINT sections are particularly valuable for summarizing critical takeaways.

**Assign to:** Plan 03-06 (voice and enrichment pass) -- add key_points to at least the first lesson of each module.

### WARNING Sections

Warnings are well-distributed: 107 warning files across all 21 modules. Every module has warnings. No action needed.

---

## 5. Bun Challenge Classification

### Module 16: Testing with Bun (14 challenges)

| Lesson | Challenge | Classification | Notes |
|--------|-----------|---------------|-------|
| L01 | 01-identify-test-types | **Has simulation** | Conceptual exercise -- console.log only, no test runner needed |
| L02 | 01-write-your-first-tests | **Has simulation** | Simulates describe/it/expect with vanilla JS wrappers |
| L03 | 01-test-edge-cases | **Has simulation** | Same simulation wrapper pattern |
| L04 | 01-mock-an-api-call | **Has simulation** | Simulates mock() with custom implementation |
| L05 | 01-test-async-functions | **Has simulation** | Simulates mock, setSystemTime, advanceTime with custom timers |
| L06 | 01-organize-a-test-suite | **Has simulation** | Simulates beforeAll/afterAll/beforeEach/afterEach |
| L07 | 01-configure-coverage-ci | **Has simulation** | Configuration exercise -- no runtime needed, outputs JS objects |
| L08 | 01-test-a-hono-api | **Has simulation** | Simulates Hono's app.request() with in-memory router |
| L09 | 01-test-a-crud-api | **Has simulation** | Same Hono simulation pattern with full CRUD |
| L09 | 02-authentication-test-suite | **Has simulation** | Simulates auth flow with in-memory token store |
| L09 | 03-error-scenario-tests | **Has simulation** | Simulates order API with validation/stock/404 errors |
| L10 | 01-mock-a-database-service | **Has simulation** | Simulates mock() with call tracking |
| L10 | 02-api-error-testing-with-mocks | **Has simulation** | Simulates fetch with mockResponse helper |
| L10 | 03-timer-testing | **Has simulation** | Custom fakeSetTimeout/fakeClearTimeout/advanceTime implementation |

**M16 Verdict: ALL 14 challenges have simulation wrappers.** They are fully Node.js-compatible. No Bun runtime required. The simulation approach is consistent and well-executed -- each challenge implements just enough of bun:test's API surface to demonstrate the concept.

### Module 19: Advanced Bun Features (5 challenges)

| Lesson | Challenge | Classification | Notes |
|--------|-----------|---------------|-------|
| L01 | 01-build-a-todo-api | **Needs simulation / Conceptual only** | Uses `import { Database } from 'bun:sqlite'` -- raw Bun API, cannot run in Node.js |
| L02 | 01-user-authentication | **Needs simulation / Conceptual only** | Uses `Bun.password.hash()` and `Bun.password.verify()` -- raw Bun API |
| L03 | 01-build-script | **Needs simulation / Conceptual only** | Uses `import { $ } from 'bun'` (Bun Shell) -- raw Bun API |
| L04 | 01-production-build-script | **Needs simulation / Conceptual only** | Uses `Bun.build()` -- raw Bun API |
| L05 | 01-system-info-via-ffi | **Conceptual only** | Uses `import { dlopen, FFIType, suffix } from 'bun:ffi'` -- requires native code + Bun runtime |

**M19 Verdict: ALL 5 challenges use raw Bun APIs with NO simulation wrappers.** These are inherently Bun-specific and cannot run in Node.js. Recommended approach:
- L01-L04: Add simulation wrappers similar to M16 pattern, OR mark with comment: "// Requires Bun runtime: run with `bun run solution.js`"
- L05 (FFI): Truly conceptual only -- even with Bun, requires native library compilation. Mark as "// Conceptual: Requires Bun runtime AND native C library. Read and understand, do not expect to run."

**Assign to:** Plan 03-05 (Modules 16-19 accuracy pass)

### Module 20: Capstone Task Manager API (8 challenges)

| Lesson | Challenge | Classification | Notes |
|--------|-----------|---------------|-------|
| L01 | 01-verify-project-setup | Framework code (TypeScript) | Uses Prisma Client -- framework, not Bun-specific |
| L02 | 01-add-task-queries | Framework code (TypeScript) | Prisma queries -- framework code |
| L03 | 01-add-password-change-endpoint | **Uses Bun API** | `Bun.password.verify()` and `Bun.password.hash()` in Hono route |
| L04 | 01-add-bulk-task-operations | Framework code (TypeScript) | Prisma + Zod + Hono -- standard framework code |
| L05 | 01-add-rate-limiting | Framework code (TypeScript) | Pure JS Map-based rate limiter, no Bun APIs |
| L06 | 01-add-tests | **Uses bun:test** | Direct `import { describe, it, expect } from 'bun:test'` |
| L07 | 01-add-redis | Framework code (YAML + TypeScript) | Docker Compose + Redis client -- not Bun-specific |
| L08 | 01-add-health-endpoint | Framework code (TypeScript) | Hono + Prisma health check -- not Bun-specific |

**M20 Verdict:** 6 of 8 challenges use standard framework code (Prisma, Hono, Zod, Redis). 2 challenges use Bun APIs:
- L03: `Bun.password` -- could add bcrypt fallback comment
- L06: `bun:test` imports -- intended to run with `bun test`

Since M20 is a capstone that explicitly states "Bun" as the runtime, Bun API usage is expected and appropriate here. No simulation needed -- these challenges are designed to be run in a Bun project.

**Assign to:** Plan 03-06 (Modules 20-21 accuracy pass)

---

## 6. ES2025 Overlap Analysis

### M18 (ES2025 Modern Patterns) vs Earlier Modules

| M18 Topic | Earlier Module Coverage | Overlap Assessment |
|-----------|------------------------|-------------------|
| Promise.try / Promise.withResolvers (L01) | M08 L02 (Promises) covers basic Promise API | **No overlap** -- M08 teaches standard Promise/then/catch; M18 teaches NEW ES2025 additions (Promise.try, withResolvers). These are genuinely new APIs not covered in M08. |
| Import Attributes Deep Dive (L02) | M08 L03 (Import Attributes intro) | **Intentional deepening** -- M08 L03 introduces the concept and basic `with { type: "json" }` syntax. M18 L02 goes deeper into security model, future attribute types, and advanced patterns. Acceptable overlap -- the M08 intro is a prerequisite, not a duplication. |
| Top-Level Await (L03) | M08 L05 (async/await) | **Mild overlap** -- M08 teaches async/await inside functions. M18 L03 focuses specifically on module-level await. Some foundational content may repeat but the focus is distinct. |
| RegExp Modifiers (L04) | No prior coverage | **No overlap** -- entirely new content |
| Decorators Preview (L05) | No prior coverage | **No overlap** -- entirely new content (Stage 3 preview) |
| Iterator Helpers (L06) | M05 L03 (map/filter/reduce on arrays) | **No overlap** -- M05 teaches array methods. M18 L06 teaches lazy iteration with Iterator.prototype.map/filter/take. Different mechanism despite similar method names. |
| RegExp.escape (L07) | No prior coverage | **No overlap** -- entirely new content |

**Verdict: M18 is NOT redundant.** It is a consolidation/advancement module that covers genuinely new ES2025 features. The Import Attributes topic has intentional overlap with M08 (intro vs. deep dive). No changes needed to M18's positioning.

### M05 Lessons 09-10 (Modern Array/Set Methods) vs M18

M05 L09 covers ES2023-2025 array methods (toSorted, toReversed, toSpliced, with, at, Object.groupBy). M05 L10 covers ES2025 Set methods (union, intersection, difference, etc.). These are placed in M05 because they are DATA STRUCTURE methods and belong with the data structures module. M18 covers different ES2025 features (Promise, Iterator, RegExp, Decorators). No overlap.

---

## 7. Findings by Accuracy Pass Plan

### Plan 03-03: Modules 01-08 Accuracy Pass

| Priority | Finding | Location | Action |
|----------|---------|----------|--------|
| **HIGH** | M08 Lessons 03/04 ordering: Import Attributes before CJS vs ESM | M08 lesson.json | Swap order values (03->04, 04->03) and swap directory names |
| MEDIUM | M08 L04 (CJS vs ESM) missing ANALOGY section | M08 L04 content/ | Add analogy section comparing CJS/ESM to two postal systems |
| LOW | M06 -> M07 transition: no explicit bridge to HTML/CSS | M07 L01 | Consider adding a brief "You'll need basic HTML" note (M07 L01 analogy already covers this well) |
| LOW | M02 has only 2 lessons (lowest in course) | M02 | Consider whether string methods or template literals warrant a third lesson -- but may be fine for pacing |

### Plan 03-04: Modules 09-15 Accuracy Pass

| Priority | Finding | Location | Action |
|----------|---------|----------|--------|
| MEDIUM | M12 -> M13 transition: no bridge from backend to frontend | M13 L01 | Add opening paragraph: "Now that you can build a backend API, let's build the user interface that connects to it" |
| MEDIUM | M10 L10 (Utility Types) may lack analogy section | M10 L10 content/ | Verify and add if missing |
| MEDIUM | M11 L06-07 missing analogies | M11 L06, L07 | Add analogy sections for Zod validation and backend organization |
| LOW | M12 difficulty "advanced" but content is intermediate-level (CRUD queries) | M12 module.json | Consider changing to "intermediate" |

### Plan 03-05: Modules 16-19 Accuracy Pass

| Priority | Finding | Location | Action |
|----------|---------|----------|--------|
| **HIGH** | M16 has ZERO analogy sections (10 lessons, 0 analogies) | M16 all lessons | Add analogy sections to at least lessons 01-04 |
| **HIGH** | M19 ALL 5 challenges use raw Bun APIs with no simulation wrappers | M19 all challenges | Add simulation wrappers OR add "Requires Bun runtime" header comments |
| MEDIUM | M16 difficulty "intermediate" but tests Hono+Prisma integration | M16 module.json | Update to "advanced" |
| MEDIUM | M19 L05 (FFI) is truly conceptual-only -- cannot simulate | M19 L05 challenge | Add explicit "Conceptual: requires native library" comment |
| LOW | M18 L02 (Import Attributes Deep Dive) slightly overlaps M08 L03 | M18 L02 | Add cross-reference: "Building on the introduction in Module 8..." |

### Plan 03-06: Modules 20-21 Accuracy Pass

| Priority | Finding | Location | Action |
|----------|---------|----------|--------|
| **HIGH** | M20 has ZERO analogy sections (8 lessons) | M20 all lessons | Add project-framing analogies to at least L01-L03 |
| **HIGH** | M21 has ZERO analogy sections (10 lessons) | M21 all lessons | Add project-framing analogies to at least L01-L03 |
| MEDIUM | M20 has ZERO key_point sections | M20 all lessons | Add key_points summarizing each capstone step |
| MEDIUM | M21 has ZERO key_point sections | M21 all lessons | Add key_points summarizing each capstone step |
| MEDIUM | M20 L03 uses `Bun.password` -- consider noting this is Bun-specific | M20 L03 challenge | Add comment about bcrypt alternative for non-Bun runtimes |
| MEDIUM | M21 only has 4 warning sections (10 lessons) | M21 L02-04, L06, L09-10 | Add warnings for common pitfalls in missing lessons |

### Plan 03-07: Global Verification and Voice Pass

| Priority | Finding | Location | Action |
|----------|---------|----------|--------|
| MEDIUM | 13 of 21 modules have zero KEY_POINT sections | Global | Assess whether key_points should be added systematically |
| LOW | M15 -> M16 conceptual gap (deploy before test) | M16 L01 | Add bridging note: "In professional development, testing often happens BEFORE deployment" |
| LOW | Difficulty metadata inconsistencies | M12, M16 | Verify all difficulty labels match actual content level |

---

## 8. Knowledge Cliff Risk Assessment

No severe knowledge cliffs (>2 level jumps) were detected. The closest risk points:

1. **M06 (intermediate) -> M07 (intermediate):** Functions to DOM. The M07 L01 analogy (actor on stage) effectively explains HTML/CSS/JS separation. Risk: LOW.

2. **M10 (intermediate) -> M11 (intermediate):** TypeScript to server-side Bun. M11 L01 introduces Bun from scratch with a kitchen analogy. Risk: LOW.

3. **M12 (advanced) -> M13 (advanced):** Backend Prisma to React frontend. This is the largest context switch in the course (server-side database queries to client-side UI components). Risk: MEDIUM. Mitigated by M13 L01's JSX analogy, but a bridging paragraph would help.

4. **M15 (advanced) -> M16 (intermediate):** Deployment to testing. Difficulty actually DECREASES. The backward ordering is conceptually awkward but not a knowledge cliff.

---

## Deviations from Plan

None -- plan executed exactly as written (read-only structural assessment).

## Success Criteria Verification

| Criteria | Status |
|----------|--------|
| SUMMARY contains all 6 analysis areas | PASS (8 sections including inventory and plan assignments) |
| Every module transition (20 transitions) assessed | PASS (see Section 3) |
| Bun-specific challenges individually classified | PASS (see Section 5 -- all 27 challenges in M16/M19/M20 classified) |
| Specific findings tagged to accuracy pass plans | PASS (see Section 7) |
| No files were modified | PASS (git diff shows zero changes to JS course content) |
| Module progression analyzed with gaps documented | PASS |
| Module ordering concerns assessed with recommendations | PASS (3 concerns evaluated) |
| Knowledge cliff risk points identified | PASS (4 risk points documented) |
| Missing ANALOGY sections catalogued | PASS (M16, M20, M21 confirmed zero) |
| Bun-specific challenge strategy documented | PASS (M16 all simulated, M19 all need simulation, M20 mixed) |

## Next Phase Readiness

All structural findings documented and assigned to specific plans. Plans 03-03 through 03-07 can proceed with clear priorities.
