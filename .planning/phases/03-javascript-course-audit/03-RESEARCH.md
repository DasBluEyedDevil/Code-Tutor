# Phase 3: JavaScript Course Audit - Research

**Researched:** 2026-02-02
**Domain:** JavaScript/TypeScript course content, ES2024+/ES2025, Node.js 22 LTS, Bun 1.3.x, Hono 4.x, React 19, Prisma 6.x/7.x
**Confidence:** HIGH (course structure verified via filesystem analysis; version targets cross-referenced with official sources)

## Summary

The JavaScript course contains 21 modules with 132 lessons, 707 content files, and 151 challenges. It is the largest course in the project and covers a complete path from absolute basics through TypeScript, server-side development with Bun/Hono, databases with Prisma, React frontend, and two capstone projects (API-only and full-stack).

The most critical finding is that **162 content files still have non-standard filenames** (109 `code.md`, 33 `concept.md`, 19 `legacy_comparison.md`, 1 `pitfalls.md`) even though Phase 1 correctly migrated the frontmatter `type:` values. The filenames need renaming to match the standard (`code.md` -> `example.md`, `concept.md` -> `theory.md`, `pitfalls.md` -> `warning.md`). The `legacy_comparison.md` files are a special case -- `LEGACY_COMPARISON` is a valid standard type with its own WPF control, so these filenames are actually correct.

A second critical finding: **challenges in Modules 19 and 20 use Bun-specific APIs** (`bun:sqlite`, `bun:ffi`, `Bun.password`, `bun:test`) that will not execute in the WPF app's Node.js-based code execution service. These challenges either need Node.js-compatible alternatives or the course must acknowledge they are Bun-only exercises.

The course's version landscape is mostly sound but has specific freshness issues: Prisma 7 has been released (manifest says 6.x), Bun is at v1.3.6 (manifest says generic 1.x which is fine), and a content file incorrectly claims "TypeScript 7.0 (December 2025)" when TS 7.0 is still unreleased (targeting mid-2026).

**Primary recommendation:** Structure the audit as 6-7 plans following the Java audit pattern: (1) content type filename migration + version targets, (2) structural review, (3-5) accuracy passes grouped by module complexity bands, (6) challenge validation, (7) global verification pass.

## Standard Stack

The established technologies for this course domain:

### Core Runtime
| Technology | Version | Purpose | Status |
|------------|---------|---------|--------|
| Node.js | 22 LTS (22.22.0) | Primary runtime, challenge execution | Maintenance LTS through Apr 2027 |
| ES2025 | Finalized Jun 2025 | Language specification target | Shipped in all major engines |
| ES2024 | Finalized 2024 | Previous spec (Object.groupBy, Promise.withResolvers) | Fully shipped |

### Framework Stack (from version manifest)
| Technology | Manifest Version | Current Latest | Gap | Notes |
|------------|-----------------|----------------|-----|-------|
| Bun | 1.x | 1.3.6 | OK (semver compatible) | Major features: zero-config frontend, built-in cookies, parameterized routes |
| Hono | 4.x | 4.11.7 | OK (semver compatible) | Security fixes in 4.11.7; `alg` now required in JWT middleware |
| React | 19.x | 19.1.4 | OK | React Compiler, Server Components, Activity component (19.2) |
| Prisma | 6.x | **7.0 released** | **VERSION GAP** | Prisma 7 ships ESM-first, removes Rust engines; v6.x still functional |
| TypeScript | 5.x | 5.7.x (6.0 bridge pending) | OK | TS 7.0 (Go rewrite) targeting mid-2026, NOT December 2025 as course claims |

### Version Decisions Needed
| Item | Current State | Recommendation | Impact |
|------|--------------|----------------|--------|
| Prisma 6.x vs 7.x | Manifest says 6.x, Prisma 7 released | Stay on 6.x for now | Prisma 7 drops MongoDB, requires ESM; content already uses 6.x patterns. Update manifest to note 7.x exists. |
| Bun 1.3 features | Course content written for ~1.0-1.1 era | Verify Bun-specific content against 1.3 API docs | New APIs (Bun.Archive, Bun.JSONC, built-in cookies) are additions, not breaking changes |
| TypeScript 7.0 claim | One file claims "TS 7.0 (December 2025)" | **Fix**: TS 7.0 is unreleased (targeting mid-2026). Remove or correct this claim. | Module 17 lesson 04 |

## Architecture Patterns

### Course Module Progression
```
Modules 01-04: Language fundamentals (basics, variables, decisions, loops)         -- 14 lessons
Module 05:     Data structures (arrays, objects, destructuring, modern methods)    -- 10 lessons
Module 06:     Functions (declarations, arrow functions, scope)                    -- 4 lessons
Module 07:     DOM (HTML/CSS/JS layers, querySelector, events)                    -- 5 lessons
Module 08:     Async (promises, async/await, modules, fetch)                      -- 6 lessons
Module 09:     Error handling (try/catch, custom errors, async errors)             -- 5 lessons
Module 10:     TypeScript (types, interfaces, generics, utility types)             -- 10 lessons
Module 11:     Backend (Bun, Hono server, routing, middleware, Zod validation)     -- 7 lessons
Module 12:     Database (Prisma ORM, schema, migrations, relations, optimization) -- 7 lessons
Module 13:     React (JSX, components, hooks, context, Tailwind)                  -- 9 lessons
Module 14:     Full-stack integration (architecture, CORS, full example)           -- 4 lessons
Module 15:     Deployment (Git, deploy platforms, env vars, CI/CD, Docker)         -- 6 lessons
Module 16:     Testing (bun:test, mocking, async, integration, coverage)           -- 10 lessons
Module 17:     JSDoc typing (type safety without TypeScript)                       -- 5 lessons
Module 18:     ES2025 modern patterns (Promise.try, iterators, decorators, RegExp) -- 7 lessons
Module 19:     Advanced Bun (SQLite, password hashing, shell, bundling, FFI)       -- 5 lessons
Module 20:     Capstone API (Bun/Hono/Prisma task manager)                         -- 8 lessons
Module 21:     Capstone Full-Stack (React + API monorepo)                          -- 10 lessons
```

### Content File Structure (Per Lesson)
```
lesson-XX/
  lesson.json           # Metadata (id, title, description, order)
  content/
    01-analogy.md       # ANALOGY section (present in ~100/132 lessons)
    02-example.md       # EXAMPLE section (standard filename)
    03-theory.md        # THEORY section
    04-warning.md       # WARNING section (present in ~128/132 lessons)
    05-key_point.md     # KEY_POINT section (sparse -- only 17 files total)
    NN-code.md          # NON-STANDARD filename (type: EXAMPLE in frontmatter)
    NN-concept.md       # NON-STANDARD filename (type: THEORY in frontmatter)
    NN-legacy_comparison.md  # Standard type, correct filename
  challenges/
    01-challenge-name/
      challenge.json    # Test cases, hints, commonMistakes, difficulty
      solution.js       # Reference solution
      starter.js        # Starting code template
```

### Content Type Distribution (707 files)
| Type (Frontmatter) | Filename Pattern | Count | Standard? |
|---------------------|-----------------|-------|-----------|
| EXAMPLE | `*-example.md` | 180 | YES |
| THEORY | `*-theory.md` | 125 | YES |
| WARNING | `*-warning.md` | 123 | YES |
| EXAMPLE | `*-code.md` | 109 | **FILENAME MISMATCH** |
| ANALOGY | `*-analogy.md` | 100 | YES |
| THEORY | `*-concept.md` | 33 | **FILENAME MISMATCH** |
| LEGACY_COMPARISON | `*-legacy_comparison.md` | 19 | YES (valid type) |
| KEY_POINT | `*-key_point.md` | 17 | YES |
| WARNING | `*-pitfalls.md` | 1 | **FILENAME MISMATCH** |

### Code Execution Architecture
The WPF app executes JavaScript challenges via:
1. `StartLocalSessionAsync("node", code, ".js", "--interactive")`
2. Saves code to temp `.js` file
3. Runs `node --interactive "{tempFile}"`
4. 30-second timeout

**Critical implication:** All challenges MUST be Node.js compatible. Bun-specific APIs (`bun:sqlite`, `bun:ffi`, `Bun.password`, `Bun.build`) will fail at runtime.

### Structural Observations
- **Module 07 (DOM)** teaches browser-only concepts but challenges run in Node.js -- these challenges likely simulate DOM operations
- **Module 08 lesson 03** (Import Attributes) appears before lesson 04 (CJS vs ESM) -- import attributes logically come after understanding module systems
- **Module 17 (JSDoc)** comes after Module 10 (TypeScript) -- teaching "types without TS" after teaching TS feels backward
- **Module 18 (ES2025 patterns)** covers features already introduced in earlier modules (Promise.try in M08, Set methods in M05) -- may overlap
- **Module 16 (Testing)** appears between deployment (M15) and JSDoc (M17) -- unusual placement

### Anti-Patterns to Avoid
- **Mixing Node.js and Bun execution contexts in challenges:** Never write a challenge solution that imports from `bun:*` without a Node.js fallback
- **Teaching deprecated patterns as current:** Decorators are still Stage 3, not shipped in any browser natively
- **Inconsistent module system usage:** Some files use `import`, some use `require` -- ensure consistency per module
- **Version pinning in content:** Package.json examples use `"typescript": "^5.0.0"` which is fine as a semver range

## Don't Hand-Roll

Problems that have existing solutions -- do not create custom approaches:

| Problem | Don't Build | Use Instead | Why |
|---------|-------------|-------------|-----|
| Bun API simulation | Custom fake Bun objects in every challenge | Standard Node.js alternatives OR clearly mark as "Bun-only, not executable in app" | Module 16 already uses simulation pattern; Module 19 does not |
| Content type migration | Manual file-by-file renames | Scripted batch rename (same as Phase 1 approach) | 143 files to rename |
| Version verification | Reading each file manually | grep/search for version strings across all 707 files | Automated sweep catches all references |
| Challenge validation | Manual review of each solution | Run `node solution.js` in batch for all 151 challenges | Automated execution reveals runtime errors |

**Key insight:** The JS course is 38% larger than the Java course (132 vs 96 lessons, 707 vs 678 content files) but has fewer non-standard issues to fix because Phase 1 already migrated the frontmatter types. The main work is filename normalization, Bun API verification, and challenge validation.

## Common Pitfalls

### Pitfall 1: Non-Standard Filenames With Correct Frontmatter
**What goes wrong:** Files are named `code.md` but have `type: "EXAMPLE"` in frontmatter. The WPF app uses the frontmatter type for rendering, so they display correctly. But filename inconsistency makes the codebase harder to maintain.
**Why it happens:** Phase 1 migrated frontmatter types but intentionally did NOT rename files (only type values were changed).
**How to avoid:** Batch-rename 143 files: `code.md` -> `example.md`, `concept.md` -> `theory.md`, `pitfalls.md` -> `warning.md`.
**Scope:** 109 code.md + 33 concept.md + 1 pitfalls.md = 143 files to rename.
**Distribution:** Concentrated in Modules 08, 10, 11, 16, 20, 21. Modules 01-07, 09, 12-15, 17-19 are clean.

### Pitfall 2: Bun-Specific Challenges That Cannot Run in Node.js
**What goes wrong:** The app runs JS challenges with `node`, but Modules 19 and 20 have challenges using `bun:sqlite`, `bun:ffi`, `Bun.password`, `Bun.build`, and `import from 'bun:test'`.
**Why it happens:** Course was designed for Bun runtime but app executes via Node.js.
**How to avoid:** For each Bun-specific challenge, either (a) provide a Node.js-compatible solution that teaches the same concept, or (b) add simulation wrappers like Module 16 already does, or (c) mark these as conceptual/non-executable exercises.
**Scope:** ~12 challenge files across Modules 19, 20 use real Bun APIs. Module 16 already handles this via "Simulating bun:test" wrappers.

### Pitfall 3: TypeScript 7.0 Misinformation
**What goes wrong:** Module 17, Lesson 04, content file `05-warning.md` states "TypeScript 7.0 (December 2025) changes JSDoc handling" with specific claims about removed tags.
**Why it happens:** Content was likely generated with speculative future information.
**How to avoid:** Verify: TypeScript 7.0 is NOT released (targeting mid-2026 with Go-based rewrite). The current stable TypeScript is 5.7.x, with 6.0 as a "bridge" release pending. This content must be corrected or removed.
**File:** `content/courses/javascript/modules/17-type-safe-javascript-jsdoc/lessons/04-type-checking-with-bun-ts-check/content/05-warning.md`

### Pitfall 4: Prisma Version Gap
**What goes wrong:** Version manifest pins Prisma 6.x but Prisma 7 has been released with ESM-first shipping, removal of Rust engines, and new `prisma-client` generator.
**Why it happens:** Prisma 7 released after manifest was created.
**How to avoid:** Decide whether to update to Prisma 7 or stay on 6.x. Prisma 6.x is still functional and receives patches. Prisma 7 drops MongoDB support and requires ESM (which aligns with this course's direction). Recommendation: stay on 6.x for this audit cycle, add a note about 7.x availability.

### Pitfall 5: Missing ANALOGY Sections in Later Modules
**What goes wrong:** While Modules 01-15 and 17-19 have ANALOGY sections in most lessons, Modules 16, 20, and 21 are completely missing ANALOGY content (28 lessons without analogies).
**Why it happens:** Testing and capstone modules were written in a more code-focused style without analogies.
**How to avoid:** Unlike the Java audit where CONTEXT.md mandated analogies for every lesson, the JS audit has no CONTEXT.md. This is a quality improvement to consider but not a hard requirement unless the planner decides to add it.
**Distribution:**
  - Module 16 (Testing): 10/10 lessons missing
  - Module 20 (Capstone API): 8/8 lessons missing
  - Module 21 (Capstone Full-Stack): 10/10 lessons missing
  - Module 08: 1 lesson missing
  - Module 10: 1 lesson missing
  - Module 11: 2 lessons missing

### Pitfall 6: Decorator Status Misrepresentation
**What goes wrong:** Module 18 Lesson 05 is titled "Decorators Preview Stage 3" -- decorators are indeed Stage 3 but have NOT shipped in any browser as of Feb 2026. They work only via Babel or TypeScript transpilation.
**Why it happens:** Stage 3 proposals are often described as "shipping soon" but decorators have been Stage 3 since ~2022.
**How to avoid:** Verify the lesson content accurately describes decorator status: Stage 3, available via TypeScript/Babel, not natively in browsers or Node.js.

### Pitfall 7: Stale Bun Docker Image References
**What goes wrong:** Challenge files reference `oven/bun:1.1` and `oven/bun:1.1-slim` Docker images.
**Why it happens:** Content written for Bun 1.1 era.
**How to avoid:** Update Docker image references to `oven/bun:1` or `oven/bun:1.3` (current latest).
**Files:** Module 15 deployment challenge.

### Pitfall 8: Leftover Script File
**What goes wrong:** A `refactor_course.py` file exists at `content/courses/javascript/refactor_course.py` -- this appears to be a development artifact.
**How to avoid:** Delete it during the audit.

## Code Examples

### ES2025 Iterator Helpers (works in Node.js 22)
```javascript
// Source: V8 docs -- https://v8.dev/features/iterator-helpers
// Available in Node.js 22+ (V8 12.x)
function* fibonacci() {
  let a = 0, b = 1;
  while (true) { yield a; [a, b] = [b, a + b]; }
}

const result = fibonacci()
  .filter(n => n % 2 === 0)
  .take(5)
  .toArray();
// [0, 2, 8, 34, 144]
```

### ES2025 Set Methods (works in Node.js 22)
```javascript
// Source: TC39 -- https://tc39.es/ecma262/2025/
const frontend = new Set(['React', 'Vue', 'Angular']);
const fullstack = new Set(['React', 'Node', 'Bun']);

frontend.intersection(fullstack);        // Set {'React'}
frontend.union(fullstack);               // Set {'React', 'Vue', 'Angular', 'Node', 'Bun'}
frontend.difference(fullstack);           // Set {'Vue', 'Angular'}
frontend.symmetricDifference(fullstack);  // Set {'Vue', 'Angular', 'Node', 'Bun'}
```

### Hono 4.x Basic Server (current pattern)
```typescript
// Source: https://hono.dev/docs/
import { Hono } from 'hono';

const app = new Hono();

app.get('/', (c) => c.text('Hello Hono!'));
app.get('/api/users/:id', (c) => {
  const id = c.req.param('id');
  return c.json({ id, name: 'User' });
});

export default app;
```

### Bun-Specific Challenge Simulation Pattern (Module 16 approach)
```javascript
// Module 16 uses this pattern to make bun:test challenges Node.js compatible
// Simulating bun:test functions for this exercise
function describe(name, fn) { console.log(`Suite: ${name}`); fn(); }
function it(name, fn) { try { fn(); console.log(`  PASS: ${name}`); } catch(e) { console.log(`  FAIL: ${name}: ${e.message}`); } }
function expect(val) {
  return {
    toBe: (expected) => { if (val !== expected) throw new Error(`Expected ${expected}, got ${val}`); },
    toEqual: (expected) => { if (JSON.stringify(val) !== JSON.stringify(expected)) throw new Error(`Deep equality failed`); }
  };
}
```

## Quantitative Audit Summary

### Scope Numbers
| Metric | Count | vs. Java Course |
|--------|-------|-----------------|
| Total modules | 21 | 16 (+31%) |
| Total lessons | 132 | 96 (+38%) |
| Total content files (.md) | 707 | 678 (+4%) |
| Total challenges | 151 | 182 (-17%) |
| Files with non-standard filenames | 143 | 0 (Java had none) |
| LEGACY_COMPARISON sections | 19 | 0 |
| Lessons missing ANALOGY | 32 | 95 (Java was much worse) |
| Bun-specific challenges (won't run in Node) | ~12 | N/A |

### Module-by-Module Assessment

| Module | Lessons | Challenges | Non-std Files | Key Issues |
|--------|---------|------------|---------------|------------|
| 01 Absolute Basics | 3 | 3 | 0 | Clean |
| 02 Variables | 2 | 2 | 0 | Only 2 lessons feels thin |
| 03 Decisions | 5 | 5 | 0 | Clean |
| 04 Loops | 4 | 4 | 0 | Clean |
| 05 Data Structures | 10 | 13 | 0 | ES2025 Set/Array methods need version verification |
| 06 Functions | 4 | 4 | 0 | Clean |
| 07 DOM | 5 | 5 | 0 | Challenges run in Node.js -- verify DOM simulation works |
| 08 Async/Modules | 6 | 8 | 9 | Import Attributes before CJS/ESM lesson (order concern); 6 code.md, 1 concept.md, 1 legacy_comparison.md, 1 pitfalls.md |
| 09 Error Handling | 5 | 5 | 0 | Clean |
| 10 TypeScript | 10 | 16 | 8 | 7 code.md, 1 concept.md; NoInfer reference (TS 5.4+) is correct |
| 11 Bun/Hono | 7 | 11 | 16 | 9 code.md, 2 concept.md, 5 legacy_comparison.md; Hono API freshness check needed |
| 12 Prisma | 7 | 7 | 0 | Prisma 6.x vs 7.x decision; verify API patterns |
| 13 React | 9 | 9 | 0 | React 19 features need verification |
| 14 Full-Stack | 4 | 4 | 3 | 3 legacy_comparison.md files |
| 15 Deployment | 6 | 6 | 3 | 3 legacy_comparison.md; stale `oven/bun:1.1` Docker image |
| 16 Testing | 10 | 14 | 17 | 8 code.md, 2 concept.md, 7 legacy_comparison.md; NO analogies in any lesson |
| 17 JSDoc | 5 | 5 | 0 | TypeScript 7.0 misinformation in lesson 04 |
| 18 ES2025 | 7 | 7 | 0 | Verify ES2025 feature accuracy; decorators status |
| 19 Adv. Bun | 5 | 5 | 0 | **ALL challenges use Bun-only APIs -- won't run in Node.js** |
| 20 Capstone API | 8 | 8 | 52 | 44 code.md, 8 concept.md; NO analogies; 2 challenges use Bun APIs |
| 21 Capstone React | 10 | 10 | 54 | 35 code.md, 19 concept.md; NO analogies |

### Non-Standard Filename Concentration
| Module Range | Non-std Files | Notes |
|-------------|--------------|-------|
| Modules 01-07 | 0 | All clean |
| Module 08 | 9 | Mixed types |
| Module 09 | 0 | Clean |
| Module 10 | 8 | code.md + concept.md |
| Module 11 | 16 | Heavy -- code + concept + legacy_comparison |
| Modules 12-15 | 6 | Mostly legacy_comparison (correct filename) |
| Module 16 | 17 | Heavy -- code + concept + legacy_comparison |
| Modules 17-19 | 0 | Clean |
| Module 20 | 52 | **Heaviest** -- capstone is nearly all code.md/concept.md |
| Module 21 | 54 | **Heaviest** -- same pattern as Module 20 |

## State of the Art

| Old/Current Approach | Latest State | When Changed | Impact on Course |
|---------------------|-------------|-------------|------------------|
| Prisma 6.x | Prisma 7.0 released | Jan 2026 | Course uses 6.x patterns; 7.x is ESM-first with no Rust engines. Low urgency. |
| Bun 1.0-1.1 APIs | Bun 1.3.6 | Jan 2026 | New APIs (Archive, JSONC, cookies); existing APIs stable. Verify Module 19 against 1.3 docs. |
| Hono 4.x | Hono 4.11.7 | Jan 2026 | Security fixes; `alg` required in JWT middleware. Check Module 11 JWT usage. |
| React 19.0 patterns | React 19.1.4 / 19.2 | Dec 2025 | Activity component, Owner Stack. Course likely fine with 19.0 patterns. |
| TS 5.x | TS 5.7.x (6.0/7.0 pending) | Ongoing | Course correctly targets 5.x. Fix the TS 7.0 misinformation. |
| Decorators "preview" | Still Stage 3, no browser support | No change | Course title says "preview" which is accurate. Verify lesson body. |
| ES2025 finalized | June 2025 | Jun 2025 | Iterator Helpers, Set Methods, Promise.try, RegExp.escape all finalized and shipped in Node.js 22. |

## Comparison to Java Audit

| Dimension | Java Audit (Phase 2) | JS Audit (Phase 3) | Implications |
|-----------|---------------------|--------------------|----|
| Modules | 16 | 21 | More modules but many are small (2-5 lessons) |
| Lessons | 96 | 132 | 38% more lessons to review |
| Content files | 678 | 707 | Similar total content |
| Challenges | 182 | 151 | Fewer challenges (simpler validation) |
| Plans used | 8 | **Suggest 6-7** | JS has fewer blocking decisions (no Thymeleaf-vs-React question) |
| Version migration needed | Massive (Java 21->25, System.out->IO, Spring Boot 3.4->4.0) | Moderate (filenames, Bun version check, TS 7.0 fix) | Less rewriting needed |
| Content type issues | 0 files with wrong filenames | 143 files need filename rename | New task type not in Java audit |
| Missing analogies | 95/96 lessons | 32/132 lessons | Much better starting point |
| Capstone issues | React-vs-Thymeleaf decision | Bun-only challenges in Node.js runtime | Different problem, similar complexity |
| Non-standard types | None (Java had 0 non-standard) | 19 LEGACY_COMPARISON (valid), 143 filename mismatches | Phase 1 migration was incomplete for filenames |

### Suggested Plan Structure (6-7 plans)

Based on the Java audit pattern adapted for JS-specific needs:

| Plan | Focus | Scope | Rationale |
|------|-------|-------|-----------|
| 03-01 | Content type filename migration + version targets | All 21 modules (scripted) | Automated rename of 143 files + version manifest update. Must complete first. |
| 03-02 | Structural review + progression analysis | All 21 modules (read-only) | Identify gaps, ordering issues, knowledge cliffs. No content changes. |
| 03-03 | Accuracy pass: Fundamentals (M01-09) | 44 lessons, ~229 content files | Core JavaScript -- ES2024/2025 features, DOM simulation, async patterns |
| 03-04 | Accuracy pass: TypeScript + Backend (M10-12) | 24 lessons, ~131 content files | TypeScript types, Hono API freshness, Prisma patterns |
| 03-05 | Accuracy pass: React + Integration + Deployment + Testing (M13-16) | 29 lessons, ~142 content files | React 19 verification, Docker images, bun:test simulation |
| 03-06 | Accuracy pass: Advanced + Capstones (M17-21) | 35 lessons, ~205 content files | ES2025 features, Bun-specific APIs, capstone completeness, deployment instructions |
| 03-07 | Challenge validation + global verification | All 151 challenges + global sweep | Execute solutions in Node.js, fix Bun-only challenges, voice consistency |

**Why 7 plans instead of 4:** The roadmap's 4-plan structure bundles too much work per plan. The Java audit used 8 plans for 96 lessons. With 132 lessons and the additional filename migration task, 7 plans provides manageable scope per plan while keeping the audit focused.

**Why not 8+ plans:** The JS course has fewer blocking decisions than Java (no capstone restructure needed -- both capstones exist and are complete). The accuracy passes can group modules by theme rather than requiring per-module plans.

## Open Questions

1. **Prisma 6.x vs 7.x Decision**
   - What we know: Prisma 7 is released, drops Rust engines, ships ESM-first. Course content uses Prisma 6.x patterns (e.g., `@prisma/client` generator).
   - What's unclear: Whether to update the course to Prisma 7 patterns or stay on 6.x.
   - Recommendation: Stay on 6.x for this audit. Prisma 6.x is still supported, and 7.x has significant breaking changes. Add a note in the version manifest about 7.x availability.

2. **Bun-Only Challenge Strategy**
   - What we know: Module 19 (5 challenges) and Module 20 (2+ challenges) use Bun-only APIs. The app executes JS via Node.js.
   - What's unclear: Should these be rewritten for Node.js compatibility, wrapped in simulation, or marked as conceptual-only?
   - Recommendation: Module 16's "Simulating bun:test" pattern works well. Apply the same approach to Module 19/20 where possible. For truly Bun-specific features (FFI, shell scripting), mark as conceptual exercises with comments explaining they require Bun runtime.

3. **Module Ordering Concerns**
   - What we know: Module 08 lesson order (Import Attributes before CJS/ESM), Module 17 after Module 10 (JSDoc after TypeScript), Module 16 between Deployment and JSDoc.
   - What's unclear: Whether these ordering choices are intentional pedagogical decisions or oversights.
   - Recommendation: Flag in structural review (Plan 03-02) for assessment. Reordering modules is high-risk (breaks numbering) vs. reordering lessons within a module (lower risk).

4. **Missing Analogies in Modules 16, 20, 21**
   - What we know: 28 lessons have no ANALOGY section. Unlike Java audit, there is no CONTEXT.md mandating analogies.
   - What's unclear: Whether the planner should add analogies as a quality improvement or leave them absent.
   - Recommendation: Flag as quality improvement, not a blocker. Capstone and testing modules are more code-focused by nature and may not benefit as much from analogies.

5. **`refactor_course.py` Artifact**
   - What we know: A Python script exists at `content/courses/javascript/refactor_course.py`.
   - What's unclear: Whether it was used for initial course generation and should be deleted.
   - Recommendation: Delete during the first plan (03-01) as a cleanup item.

6. **Hono JWT Middleware Security Update**
   - What we know: Hono 4.11.0 made the `alg` option required for `jwt()` and `jwk()` middleware.
   - What's unclear: Whether Module 11 or Module 20 (capstone authentication) uses JWT middleware without the `alg` parameter.
   - Recommendation: Check during accuracy pass (Plan 03-04 or 03-06) -- search for `jwt(` in Hono middleware content.

## Sources

### Primary (HIGH confidence)
- Filesystem analysis of `C:/Users/dasbl/Downloads/Code-Tutor/content/courses/javascript/` -- direct file counting and content inspection
- Phase 1 Summary (`01-03-SUMMARY.md`) -- confirmed 186 type migrations, including 109 CODE->EXAMPLE and 33 CONCEPT->THEORY in JavaScript
- Version manifest (`content/version-manifest.json`) -- pinned versions for all frameworks
- WPF Code Execution Service (`native-app-wpf/Services/CodeExecutionService.cs`) -- confirmed Node.js execution for JavaScript challenges
- [ECMAScript 2025 Specification](https://tc39.es/ecma262/2025/) -- ES2025 finalized features
- [Node.js 22 LTS Release](https://nodejs.org/en/blog/release/v22.22.0) -- Latest Node.js 22 version (22.22.0)
- [V8 Iterator Helpers](https://v8.dev/features/iterator-helpers) -- Confirmed available in Node.js 22

### Secondary (MEDIUM confidence)
- [Bun Blog - v1.3](https://bun.com/blog/bun-v1.3) -- Bun 1.3 features and breaking changes
- [Hono GitHub Releases](https://github.com/honojs/hono/releases) -- Hono 4.11.7, JWT middleware changes
- [Prisma v6/v7 Upgrade Guide](https://www.prisma.io/docs/orm/more/upgrade-guides/upgrading-versions/upgrading-to-prisma-7) -- Prisma 7 breaking changes
- [React 19.2 Blog Post](https://react.dev/blog/2025/10/01/react-19-2) -- React 19.2 features
- [TypeScript 7 Progress - December 2025](https://devblogs.microsoft.com/typescript/progress-on-typescript-7-december-2025/) -- TS 7.0 is NOT released, targeting mid-2026
- [Decorators - Can I Use](https://caniuse.com/decorators) -- Not shipped in any browser

### Tertiary (LOW confidence)
- [InfoQ: Bun v3.1 (acquired by Anthropic)](https://www.infoq.com/news/2026/01/bun-v3-1-release/) -- Bun acquisition news
- Various Medium articles on ES2025 features (cross-verified with TC39 spec)

## Metadata

**Confidence breakdown:**
- Course structure and file counts: **HIGH** -- directly measured via filesystem
- Content type distribution: **HIGH** -- directly measured via filesystem + grep
- ES2025 feature accuracy: **HIGH** -- verified via TC39 spec and Node.js compatibility
- Bun/Hono API versions: **HIGH** -- verified via official releases
- Prisma version gap: **HIGH** -- Prisma 7 release confirmed via official docs
- TypeScript 7.0 status: **HIGH** -- verified via official Microsoft blog
- Challenge execution compatibility: **HIGH** -- verified by reading CodeExecutionService.cs source
- Module ordering concerns: **MEDIUM** -- based on pedagogical judgment, not verified with user
- Suggested plan structure: **MEDIUM** -- extrapolated from Java audit pattern, subject to planner adjustment

**Research date:** 2026-02-02
**Valid until:** 2026-03-04 (30 days -- Bun and Hono release frequently; re-verify if audit extends)
