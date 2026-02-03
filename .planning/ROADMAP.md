# Roadmap: Code Tutor

## Overview

Code Tutor's primary blocker is content quality, not application architecture. The 812 lessons across 6 courses were assembled by multiple AI tools with no shared standards, resulting in phantom APIs, knowledge cliffs, broken challenges, and inconsistent schemas. This roadmap normalizes the content foundation first, then audits each course individually (ordered by structural readiness), then enhances the AI tutor and UI after content is stable. Content gates everything -- no amount of tutoring or polish fixes lessons that teach non-existent APIs.

## Phases

**Phase Numbering:**
- Integer phases (1, 2, 3): Planned milestone work
- Decimal phases (2.1, 2.2): Urgent insertions (marked with INSERTED)

Decimal phases appear between their surrounding integers in numeric order.

- [x] **Phase 1: Foundation and Content Normalization** - Clean infrastructure and standardize all content schemas before editing begins
- [x] **Phase 2: Java Course Audit** - Establish the gold-standard audited course (best existing structure)
- [ ] **Phase 3: JavaScript Course Audit** - Second audit, migrate non-standard content types, verify Bun/Hono APIs
- [ ] **Phase 4: C# Course Audit** - Add missing KEY_POINTs, reorder modules, calibrate estimated hours
- [ ] **Phase 5: Flutter/Dart Course Audit** - Rename generic modules, verify Dart Frog APIs, split mega-modules
- [ ] **Phase 6: Kotlin Course Audit** - Create missing capstone, add 70+ challenges, balance content types
- [ ] **Phase 7: Python Course Audit** - Restructure Module 14, resolve duplicates, add Git module
- [ ] **Phase 8: AI Tutor Enhancement** - Socratic method prompting, lesson-aware context, ONNX Runtime upgrade
- [ ] **Phase 9: UI and Engagement** - Interactive code examples, progress tracking, visual polish

## Phase Details

### Phase 1: Foundation and Content Normalization
**Goal**: All 6 courses share a single, validated content schema with standardized section types, sequential numbering, pinned version targets, and a clean git history -- so that every subsequent audit phase operates on consistent data
**Depends on**: Nothing (first phase)
**Requirements**: INFR-01, INFR-02, INFR-03, INFR-04, NORM-01, NORM-02, NORM-03, NORM-04, NORM-05
**Success Criteria** (what must be TRUE):
  1. Every lesson.json across all 6 courses validates against a single shared schema (consistent id format, required fields, metadata structure)
  2. Every content section file uses one of the standardized type names (THEORY, EXAMPLE, KEY_POINT, ANALOGY, WARNING, LEGACY_COMPARISON) and the app renders all of them
  3. Module and lesson numbering is sequential with no gaps or duplicates in any course
  4. Python Module 14 has been restructured into coherent focused modules with no duplicate lessons
  5. Version targets are pinned per language (Java 21, Python 3.12+, C# 12/.NET 8, Node 22, Kotlin 2.0+, Flutter 3.x/Dart 3.x) and documented in a manifest file
**Plans**: 6 plans

Plans:
- [x] 01-01-PLAN.md -- Git cleanup (remove .bak files, verify INFR-02, update .gitignore)
- [x] 01-02-PLAN.md -- Schema standardization (define shared JSON schemas, standardize all IDs, validate all courses)
- [x] 01-03-PLAN.md -- Content type migration (migrate 185 non-standard section types, fix numbering gaps, clean directory names)
- [x] 01-04-PLAN.md -- Python Module 14 restructuring (split into focused modules, resolve 8 duplicate pairs, renumber downstream)
- [x] 01-05-PLAN.md -- Version manifest and runtime requirements (pin targets, create manifest, update course.json)
- [x] 01-06-PLAN.md -- Gap closure: ANALOGY and WARNING WPF renderers (close verification gap for NORM-02)

### Phase 2: Java Course Audit
**Goal**: The Java course teaches a complete, accurate path from absolute beginner to deploying a Spring Boot application, with every lesson verified against Java 25 LTS and every challenge executing correctly
**Depends on**: Phase 1
**Requirements**: JAVA-01, JAVA-02, JAVA-03, JAVA-04, JAVA-05
**Success Criteria** (what must be TRUE):
  1. Every Java lesson uses consistent, correct API references (IO.println used globally, System.out.println mentioned only in one LEGACY_COMPARISON section)
  2. All 96 lessons progress from fundamentals through OOP through Spring Boot with no knowledge gaps or assumed context not yet taught
  3. Every coding challenge compiles and runs against Java 25 LTS, and test validation passes for both starter and solution code
  4. A deployable capstone project exists with both Thymeleaf (simpler) and React (advanced) frontend paths
  5. Voice, tone, and difficulty progression are consistent from Module 1 through the final module
**Plans**: 8 plans

Plans:
- [x] 02-01-PLAN.md -- Version targets and structural review (Java 25, Spring Boot 4.0.x, fix directory names, module progression analysis)
- [x] 02-02-PLAN.md -- Modules 01-03 accuracy pass (fundamentals, data types, Git -- IO.println migration, compact syntax, LEGACY_COMPARISON)
- [x] 02-03-PLAN.md -- Modules 04-05 accuracy pass (OOP, collections/FP -- syntax transition, flexible constructors, remove Java 8 framing)
- [x] 02-04-PLAN.md -- Modules 06-09 accuracy pass (streams, concurrency, testing, databases -- virtual threads, @MockitoBean, JDBC)
- [x] 02-05-PLAN.md -- Modules 10-12 accuracy pass (web/APIs, Spring Boot, security -- Spring Boot 4.0.x migration, Spring Security 7)
- [x] 02-06-PLAN.md -- Modules 13-15 accuracy pass (React, DevOps, full-stack -- Docker eclipse-temurin:25, deployment configs)
- [x] 02-07-PLAN.md -- Module 16 capstone restructure (dual path: Thymeleaf + React, Railway deployment)
- [x] 02-08-PLAN.md -- Global verification and voice pass (sweep for survivors, progression review, human checkpoint)

### Phase 3: JavaScript Course Audit
**Goal**: The JavaScript course teaches a complete path from fundamentals through React and Node.js to a deployable full-stack application, with all non-standard content types migrated and Bun/Hono APIs verified
**Depends on**: Phase 1
**Requirements**: JSCR-01, JSCR-02, JSCR-03, JSCR-04, JSCR-05
**Success Criteria** (what must be TRUE):
  1. Every JavaScript lesson is accurate against ES2024+/Node 22 LTS, with all Bun-specific APIs verified against current stable Bun release
  2. All 132 lessons progress from basics through React and Node.js/Bun to a deployable project with no knowledge cliffs
  3. Every coding challenge executes correctly (Node.js and Bun paths both verified) and test validation passes
  4. A deployable capstone project exists with clear deployment instructions
  5. All non-standard content types (CODE, CONCEPT) have been migrated to standard types (EXAMPLE, THEORY) and render correctly
**Plans**: 7 plans

Plans:
- [ ] 03-01-PLAN.md -- Content type filename migration + version targets (rename 143 files, update manifest, fix course.json, delete artifact)
- [ ] 03-02-PLAN.md -- Structural review and progression analysis (module ordering, prerequisite chains, Bun challenge strategy)
- [ ] 03-03-PLAN.md -- Accuracy pass: Fundamentals M01-09 (ES2024/2025 features, DOM simulation, async patterns)
- [ ] 03-04-PLAN.md -- Accuracy pass: TypeScript + Backend M10-12 (TS 5.x, Hono JWT alg fix, Prisma 6.x)
- [ ] 03-05-PLAN.md -- Accuracy pass: React + Deployment + Testing M13-16 (React 19, Docker oven/bun:1, bun:test simulation)
- [ ] 03-06-PLAN.md -- Accuracy pass: Advanced + Capstones M17-21 (fix TS 7.0 misinfo, ES2025, Bun challenges, capstone deploy)
- [ ] 03-07-PLAN.md -- Global verification and voice pass (sweep for survivors, progression review, human checkpoint)

### Phase 4: C# Course Audit
**Goal**: The C# course teaches a complete path from basics through ASP.NET to a deployable application, with KEY_POINTs added throughout and estimated hours calibrated to reality
**Depends on**: Phase 1
**Requirements**: CSRP-01, CSRP-02, CSRP-03, CSRP-04, CSRP-05
**Success Criteria** (what must be TRUE):
  1. Every C# lesson is accurate against C# 12/.NET 8, using current stable patterns and no deprecated APIs
  2. All 132 lessons progress smoothly from fundamentals through OOP through ASP.NET Core with no knowledge gaps
  3. Every coding challenge executes via Roslyn and test validation passes for both starter and solution code
  4. A deployable capstone project exists with clear build and deployment instructions
  5. Every lesson has at least one KEY_POINT section (currently only 1 across 132 lessons), providing clear takeaways
**Plans**: TBD

Plans:
- [ ] 04-01: C# structural review (module reordering, progression analysis, prerequisite verification)
- [ ] 04-02: C# content accuracy pass (C# 12/.NET 8 verification, deprecated API removal)
- [ ] 04-03: C# KEY_POINT enrichment (add KEY_POINTs to all 132 lessons, add missing examples/analogies)
- [ ] 04-04: C# challenge validation (execute all solutions via Roslyn, verify test cases, confirm capstone)

### Phase 5: Flutter/Dart Course Audit
**Goal**: The Flutter course teaches a complete path from Dart basics through full app development with backend integration to a deployable mobile/web application, with Dart Frog APIs verified against current community-maintained status
**Depends on**: Phase 1
**Requirements**: FLTR-01, FLTR-02, FLTR-03, FLTR-04, FLTR-05
**Success Criteria** (what must be TRUE):
  1. Every Flutter lesson is accurate against Flutter 3.x/Dart 3.x stable, with no deprecated widget usage or outdated patterns
  2. All 153 lessons progress from Dart fundamentals through widgets through state management through backend to deployment with no knowledge cliffs
  3. Every coding challenge executes correctly and test validation passes
  4. A deployable capstone project exists (cross-platform Flutter app with backend)
  5. Dart Frog content is verified against current community-maintained APIs, with clear guidance on its status and relationship to Serverpod
**Plans**: TBD

Plans:
- [ ] 05-01: Flutter structural review (rename generic modules, split Serverpod mega-module, verify progression)
- [ ] 05-02: Flutter content accuracy pass (Flutter 3.x/Dart 3.x verification, widget freshness)
- [ ] 05-03: Dart Frog verification (API existence check, community status documentation, Serverpod relationship)
- [ ] 05-04: Flutter challenge validation (execute solutions, add challenges to under-covered lessons, verify capstone)

### Phase 6: Kotlin Course Audit
**Goal**: The Kotlin course teaches a complete path from basics through Android and Ktor to a deployable application, with a capstone project created from scratch and challenge coverage raised from 45% to 100%
**Depends on**: Phase 1
**Requirements**: KTLN-01, KTLN-02, KTLN-03, KTLN-04, KTLN-05
**Success Criteria** (what must be TRUE):
  1. Every Kotlin lesson is accurate against Kotlin 2.0+ with K2 compiler, using current idioms and no deprecated patterns
  2. All 128 lessons progress from basics through OOP through Android/Ktor to deployment with no assumed knowledge gaps
  3. Every lesson has at least one coding challenge (70+ new challenges created), and all challenges execute and validate correctly
  4. A capstone project exists (Ktor + Compose Multiplatform or equivalent) that students can build and deploy
  5. KEY_POINTs added throughout (currently only 30 across 128 lessons) and content balanced with examples and analogies
**Plans**: TBD

Plans:
- [ ] 06-01: Kotlin structural review (progression, module ordering, identify challenge gaps)
- [ ] 06-02: Kotlin content accuracy pass (Kotlin 2.0+ verification, K2 compiler patterns)
- [ ] 06-03: Kotlin challenge creation (create 70+ missing challenges with starter code, solutions, and test cases)
- [ ] 06-04: Kotlin capstone creation (design and build deployable capstone project)
- [ ] 06-05: Kotlin enrichment pass (KEY_POINTs, examples, analogies, voice consistency)

### Phase 7: Python Course Audit
**Goal**: The Python course teaches a complete path from absolute basics through web frameworks to a deployable application, with Module 14 restructured into focused modules and a Git/developer tools module added
**Depends on**: Phase 1
**Requirements**: PYTH-01, PYTH-02, PYTH-03, PYTH-04, PYTH-05
**Success Criteria** (what must be TRUE):
  1. Every Python lesson is accurate against Python 3.12+ stable, with no incorrect version claims or deprecated patterns
  2. All 171 lessons (post-restructure) progress from basics through data structures through web frameworks to deployment with explicit bridge modules at framework transitions
  3. Every coding challenge executes against Python 3.12+ and test validation passes
  4. A deployable capstone project exists (FastAPI or Django application with database)
  5. The former Module 14 (26 lessons covering FastAPI, Django, PostgreSQL, Auth) is split into 3-4 focused modules with clear progression
**Plans**: TBD

Plans:
- [ ] 07-01: Python structural review (post-normalization module structure, progression analysis)
- [ ] 07-02: Python content accuracy pass (Python 3.12+ verification, library freshness)
- [ ] 07-03: Python Module 14 deep edit (content quality within restructured modules, bridge lessons)
- [ ] 07-04: Python challenge validation (execute all solutions, fix broken challenges, verify capstone)
- [ ] 07-05: Python voice and polish pass (consistent tone, difficulty progression, add Git/tools module if missing)

### Phase 8: AI Tutor Enhancement
**Goal**: The local Phi-4 AI tutor uses Socratic method to guide students through concepts and debugging, with full lesson context awareness and progressive hint delivery
**Depends on**: Phases 2-7 (content must be stable before RAG indexing)
**Requirements**: TUTR-01, TUTR-02, TUTR-03, TUTR-04, TUTR-05
**Success Criteria** (what must be TRUE):
  1. The tutor asks guiding questions to lead students toward understanding rather than giving direct answers (Socratic method)
  2. The tutor receives and uses full lesson content as context (theory, examples, key points) -- not just the lesson title
  3. When a student is stuck on a challenge, the tutor provides progressive hints (nudge, then guidance, then solution) rather than immediately revealing the answer
  4. When a student's code fails, the tutor provides debugging suggestions tied to the specific lesson concepts being practiced
  5. ONNX Runtime GenAI is upgraded from 0.5.2 to current stable, with Phi-4 model running correctly on the new runtime
**Plans**: TBD

Plans:
- [ ] 08-01: ONNX Runtime GenAI upgrade (0.5.2 to current stable, verify Phi-4 inference works)
- [ ] 08-02: Socratic system prompt and lesson context injection (rewrite prompts, pass full lesson content)
- [ ] 08-03: Progressive hints and debug assistance (3-level hint system, failure-triggered help)
- [ ] 08-04: Tutor validation (test tutor against sample challenges across all 6 courses)

### Phase 9: UI and Engagement
**Goal**: The application provides an interactive, visually polished learning experience with working progress tracking, inline code execution for examples, and dedicated renderers for all content types
**Depends on**: Phase 1 (content types), Phase 8 (tutor integration)
**Requirements**: UIEX-01, UIEX-02, UIEX-03, UIEX-04, UIEX-05
**Success Criteria** (what must be TRUE):
  1. Code examples within lessons have a "Try It" button that lets students edit and run them inline without navigating to the challenge editor
  2. Progress tracking works end-to-end: lesson completion persists across sessions, challenge success rates are tracked, and streaks display correctly
  3. MaterialDesign theming is applied consistently across all views (course browser, lesson viewer, code editor, chat)
  4. ANALOGY, WARNING, and other content section types render with dedicated visual treatments (not generic default styling)
  5. Markdown content renders cleanly with proper code block formatting, syntax highlighting, and readable typography
**Plans**: TBD

Plans:
- [ ] 09-01: Content section renderers (dedicated UI for ANALOGY, WARNING, KEY_POINT, LEGACY_COMPARISON)
- [ ] 09-02: Interactive code examples (add "Try It" execution to EXAMPLE sections)
- [ ] 09-03: Progress tracking implementation (streaks, completion persistence, challenge success rates)
- [ ] 09-04: MaterialDesign theming and markdown rendering polish

## Progress

**Execution Order:**
Phases execute in numeric order: 1 -> 2 -> 3 -> 4 -> 5 -> 6 -> 7 -> 8 -> 9

Note: Phases 2-7 (course audits) depend only on Phase 1 and can execute in any order. The recommended order (Java -> JS -> C# -> Flutter -> Kotlin -> Python) goes from least to most structural work, building audit muscle memory. Phase 8 depends on all content phases (2-7) being complete. Phase 9 depends on Phase 1 (for content types) and Phase 8 (for tutor integration).

| Phase | Plans Complete | Status | Completed |
|-------|---------------|--------|-----------|
| 1. Foundation and Content Normalization | 6/6 | Complete | 2026-02-02 |
| 2. Java Course Audit | 8/8 | Complete | 2026-02-03 |
| 3. JavaScript Course Audit | 0/7 | Not started | - |
| 4. C# Course Audit | 0/4 | Not started | - |
| 5. Flutter/Dart Course Audit | 0/4 | Not started | - |
| 6. Kotlin Course Audit | 0/5 | Not started | - |
| 7. Python Course Audit | 0/5 | Not started | - |
| 8. AI Tutor Enhancement | 0/4 | Not started | - |
| 9. UI and Engagement | 0/4 | Not started | - |
