# Phase 3 Plan 6: Accuracy Pass Modules 17-21 Summary

**One-liner:** Fix TS 7.0 misinformation, verify ES2025 features, add Bun challenge wrappers, fix JWT alg and Docker tags in capstones, add missing analogies/key_points to M20-M21.

## Changes Made

### Task 1: Accuracy Pass Modules 17-19 (JSDoc, ES2025, Advanced Bun)
**Commit:** `51ba9c2f`

**M17 -- Type-Safe JavaScript with JSDoc (5 lessons):**
- CRITICAL FIX: L04 05-warning.md falsely claimed "TypeScript 7.0 (December 2025) changes JSDoc handling" with specific false claims about removed tags (@enum, @constructor). TypeScript 7.0 is NOT released; the Go-based rewrite targets mid-2026+. Replaced with accurate future-proofing guidance.
- Verified: All JSDoc annotations (@type, @param, @returns, @typedef, @template, @satisfies) are accurate
- Verified: tsc --checkJs and jsconfig.json configuration accurate
- Verified: Migration path to TypeScript in L05 is correct

**M18 -- ES2025 Modern Patterns (7 lessons):**
- Promise.try(): Accurately described as finalized ES2025. Examples correct.
- Promise.withResolvers(): Accurately described (technically ES2024, grouped with modern patterns -- acceptable).
- Iterator Helpers: Complete API surface verified (map, filter, take, drop, flatMap, toArray, forEach, some, every, find, reduce). Correct lazy evaluation description.
- RegExp.escape(): Accurately described as finalized ES2025.
- RegExp Modifiers (L04): Fixed incorrect example comment claiming domain capture "lowercased" -- regex modifiers don't transform captured text, they control matching behavior.
- Decorators (L05): Correctly described as Stage 3, NOT native in browsers/Node.js. Warning properly states "not yet standard." TypeScript `experimentalDecorators` note accurate.
- Import Attributes (L02): Accurately described with correct syntax (`with { type: 'json' }`).
- Top-Level Await (L03): Correctly shown as standard feature in ESM.

**M19 -- Advanced Bun Features (5 lessons):**
- Verified content accuracy against Bun 1.3.x API: bun:sqlite Database API, Bun.password hash/verify, Bun.$ shell, Bun.build(), bun:ffi dlopen
- L01 (bun:sqlite): Added simulation wrapper with in-memory Database class mimicking prepare/query/run API
- L02 (Bun.password): Added simulation wrapper using Node.js crypto for hash/verify interface
- L03 (Bun.$): Marked as Bun-only with Node.js alternative guidance (child_process)
- L04 (Bun.build): Added simulation wrapper with mock config object reporting
- L05 (bun:ffi): Marked as conceptual/Bun-only with note about node-ffi-napi alternative

### Task 2: Accuracy Pass Modules 20-21 (Both Capstones)
**Commit:** `dfb85e87`

**M20 -- Capstone Task Manager API (8 lessons):**
- JWT FIX: Added `alg: 'HS256'` to both `sign()` and `verify()` calls in L03 03-example.md (required since Hono 4.11.0)
- Docker FIX: Updated `oven/bun:1.1` to `oven/bun:1` in L07 02-example.md (2 occurrences) and L07 07-warning.md (4 occurrences)
- Fly.io FIX: Updated outdated fly.toml in L08 04-example.md (old `[[services]]` format replaced with modern `[http_service]` format, removed invalid buildpack reference, added flyctl CLI commands)
- Architecture verified: Bun + Hono + Prisma + Zod stack consistent throughout
- Zod validation matches M11 patterns (zValidator middleware with proper schemas)
- Deployment instructions complete: Railway and Fly.io with step-by-step CLI commands
- M20 L03/L06 challenges correctly use Bun APIs (expected for Bun capstone, no simulation needed)
- Added 3 analogies (L01 restaurant kitchen, L02 filing cabinet, L03 concert wristband)
- Added 2 key_points (L01 project setup checklist, L08 capstone deployment summary)

**M21 -- Capstone React Full-Stack (10 lessons):**
- Monorepo: Bun workspace config accurate (packages/api, packages/web, packages/shared)
- Shared types pattern correct (TypeScript interfaces in shared package)
- React components: All functional with hooks, proper TypeScript typing
- Auth UI: Context API with useAuth hook, matches M20 API auth endpoints
- Protected routes: react-router-dom v6+ pattern (Routes/Route/Navigate, not v5 Switch/Redirect)
- Testing: React Testing Library (not Enzyme) -- correct for modern React
- Production build: Vite config with proper chunk splitting
- Deploy: Vercel for frontend, separate backend deployment
- Added 3 analogies (L01 apartment building, L02 department contract, L03 typed telephone)
- Added 2 key_points (L01 monorepo structure, L10 full-stack capstone summary)

## Deviations from Plan

### Auto-fixed Issues

**1. [Rule 1 - Bug] Fixed incorrect regex modifier example comment**
- **Found during:** Task 1, M18 L04 review
- **Issue:** Example claimed regex modifier `(?i:https?)` would cause the domain capture group to be "lowercased." Regex modifiers control matching behavior, not captured text.
- **Fix:** Corrected comment and added second test case showing case-sensitive domain matching
- **Files modified:** M18 L04 03-theory.md
- **Commit:** 51ba9c2f

**2. [Rule 1 - Bug] Fixed outdated Fly.io fly.toml configuration**
- **Found during:** Task 2, M20 L08 review
- **Issue:** fly.toml used deprecated `[[services]]` format, invalid `builder = "patak/buildpacks"`, and missing flyctl CLI commands
- **Fix:** Updated to modern `[http_service]` format with Dockerfile build strategy and added deployment CLI commands
- **Files modified:** M20 L08 04-example.md
- **Commit:** dfb85e87

**3. [Rule 2 - Missing Critical] Added analogies to capstone modules**
- **Found during:** Structural review findings in plan
- **Issue:** M20 had ZERO analogy sections (8 lessons), M21 had ZERO analogy sections (10 lessons)
- **Fix:** Added project-framing analogies to L01-L03 of both modules (6 new files)
- **Files created:** M20 L01/L02/L03 analogy.md, M21 L01/L02/L03 analogy.md
- **Commit:** dfb85e87

**4. [Rule 2 - Missing Critical] Added key_point sections to capstone modules**
- **Found during:** Structural review findings in plan
- **Issue:** M20 had ZERO key_point sections, M21 had ZERO key_point sections
- **Fix:** Added key_points to bookend lessons (setup and deployment) in both modules (4 new files)
- **Files created:** M20 L01/L08 key_point.md, M21 L01/L10 key_point.md
- **Commit:** dfb85e87

## Verification Results

| Must-Have | Status |
|-----------|--------|
| TypeScript 7.0 misinformation corrected | DONE -- false claims replaced with accurate future-proofing guidance |
| ES2025 features verified as finalized | DONE -- Promise.try, Iterator Helpers, RegExp.escape confirmed ES2025 |
| Decorators described as Stage 3 / transpiler-only | DONE -- warning correctly states "not yet standard" |
| M19 Bun challenges have simulation wrappers or Bun-only markers | DONE -- L01/L02/L04 simulations, L03/L05 Bun-only markers |
| M20 JWT with alg parameter | DONE -- alg: 'HS256' added to sign() and verify() |
| M20 Docker uses oven/bun:1 | DONE -- 6 occurrences of oven/bun:1.1 updated |
| M21 deployment instructions complete | DONE -- Vite build + Vercel frontend + backend deployment steps present |

## Files Modified

**M17 (1 file modified):**
- lessons/04-type-checking-with-bun-ts-check/content/05-warning.md

**M18 (1 file modified):**
- lessons/04-es2025-regexp-modifiers/content/03-theory.md

**M19 (10 files modified):**
- lessons/01-built-in-sqlite-with-bunsqlite/challenges/01-build-a-todo-api/starter.js
- lessons/01-built-in-sqlite-with-bunsqlite/challenges/01-build-a-todo-api/solution.js
- lessons/02-secure-password-hashing-with-bunpassword/challenges/01-user-authentication/starter.js
- lessons/02-secure-password-hashing-with-bunpassword/challenges/01-user-authentication/solution.js
- lessons/03-shell-scripting-with-bun-shell/challenges/01-build-script/starter.js
- lessons/03-shell-scripting-with-bun-shell/challenges/01-build-script/solution.js
- lessons/04-bundling-for-production-with-bunbuild/challenges/01-production-build-script/starter.js
- lessons/04-bundling-for-production-with-bunbuild/challenges/01-production-build-script/solution.js
- lessons/05-calling-native-code-with-ffi/challenges/01-system-info-via-ffi/starter.js
- lessons/05-calling-native-code-with-ffi/challenges/01-system-info-via-ffi/solution.js

**M20 (8 files modified/created):**
- lessons/01-project-setup-planning/content/09-analogy.md (NEW)
- lessons/01-project-setup-planning/content/10-key_point.md (NEW)
- lessons/02-database-schema-design/content/08-analogy.md (NEW)
- lessons/03-authentication-implementation/content/03-example.md (JWT alg fix)
- lessons/03-authentication-implementation/content/09-analogy.md (NEW)
- lessons/07-docker-configuration/content/02-example.md (Docker tag fix)
- lessons/07-docker-configuration/content/07-warning.md (Docker tag fix)
- lessons/08-deployment/content/04-example.md (Fly.io config fix)
- lessons/08-deployment/content/09-key_point.md (NEW)

**M21 (5 files created):**
- lessons/01-monorepo-setup-with-bun-workspaces/content/11-analogy.md (NEW)
- lessons/01-monorepo-setup-with-bun-workspaces/content/12-key_point.md (NEW)
- lessons/02-shared-types-between-frontend-and-backend/content/09-analogy.md (NEW)
- lessons/03-api-client-with-type-safety/content/07-analogy.md (NEW)
- lessons/10-production-build-deploy/content/05-key_point.md (NEW)

## Decisions Made

- M19 L03/L05: Bun shell and FFI challenges marked Bun-only rather than adding simulations (no meaningful cross-runtime equivalent for shell template literals and native FFI)
- M19 L01/L02/L04: Full simulation wrappers added (Database, Bun.password, Bun.build APIs can be meaningfully simulated)
- M20/M21 analogies: Added to first 3 lessons of each module (project setup, core architecture, key feature) rather than all lessons
- M20/M21 key_points: Added to bookend lessons (first and last) to frame the capstone journey

## Metrics

- **Duration:** ~17 min
- **Completed:** 2026-02-03
- **Files touched:** 26 (12 modified, 14 created)
- **Modules verified:** 5 (M17-M21, 35 lessons total)
