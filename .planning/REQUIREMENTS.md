# Requirements: Code Tutor

**Defined:** 2026-02-02
**Core Value:** Every course teaches a coherent, progressive path from absolute beginner to independently building and deploying a real application.

## v1 Requirements

### Content Normalization

- [ ] **NORM-01**: All 6 courses use consistent lesson.json schema (id format, required fields, metadata structure)
- [ ] **NORM-02**: All content sections use standardized type names recognized by the app (THEORY, EXAMPLE, KEY_POINT, ANALOGY, WARNING, CODE)
- [ ] **NORM-03**: Module and lesson numbering is sequential with no gaps or duplicates across all courses
- [ ] **NORM-04**: Python Module 14 restructured — duplicate lessons resolved, coherent progression restored
- [ ] **NORM-05**: Version targets pinned per language (Java 21 LTS, Python 3.12+, C# 12/.NET 8, Node 22 LTS, Kotlin 2.0+, Flutter 3.x/Dart 3.x)

### Content Quality — Python

- [ ] **PYTH-01**: Every lesson reviewed for accuracy against Python 3.12+ / current stable
- [ ] **PYTH-02**: Progressive curriculum from basics through web framework to deployable project
- [ ] **PYTH-03**: All coding challenges execute and validate correctly
- [ ] **PYTH-04**: Capstone project exists and is deployable
- [ ] **PYTH-05**: Consistent voice, tone, and difficulty progression across all modules

### Content Quality — Java

- [ ] **JAVA-01**: Every lesson reviewed for accuracy — resolve IO.println vs System.out.println inconsistency
- [ ] **JAVA-02**: Progressive curriculum from basics through Spring Boot to deployable project
- [ ] **JAVA-03**: All coding challenges execute and validate correctly
- [ ] **JAVA-04**: Capstone project exists and is deployable
- [ ] **JAVA-05**: Consistent voice, tone, and difficulty progression across all modules

### Content Quality — C#

- [ ] **CSRP-01**: Every lesson reviewed for accuracy against C# 12/.NET 8
- [ ] **CSRP-02**: Progressive curriculum from basics through ASP.NET to deployable project
- [ ] **CSRP-03**: All coding challenges execute and validate correctly
- [ ] **CSRP-04**: Capstone project exists and is deployable
- [ ] **CSRP-05**: KEY_POINTs added throughout (currently only 1 across 132 lessons)

### Content Quality — JavaScript

- [ ] **JSCR-01**: Every lesson reviewed for accuracy against ES2024+/Node 22 LTS
- [ ] **JSCR-02**: Progressive curriculum from basics through React/Node to deployable project
- [ ] **JSCR-03**: All coding challenges execute and validate correctly
- [ ] **JSCR-04**: Capstone project exists and is deployable
- [ ] **JSCR-05**: Non-standard content types (CODE, CONCEPT) migrated to standard types

### Content Quality — Kotlin

- [ ] **KTLN-01**: Every lesson reviewed for accuracy against Kotlin 2.0+
- [ ] **KTLN-02**: Progressive curriculum from basics through Android/Ktor to deployable project
- [ ] **KTLN-03**: All coding challenges execute and validate correctly (70+ challenges need creation)
- [ ] **KTLN-04**: Capstone project created (currently missing)
- [ ] **KTLN-05**: KEY_POINTs added throughout (currently only 30 across 128 lessons)

### Content Quality — Flutter/Dart

- [ ] **FLTR-01**: Every lesson reviewed for accuracy against Flutter 3.x/Dart 3.x stable
- [ ] **FLTR-02**: Progressive curriculum from basics through full app with backend to deployable project
- [ ] **FLTR-03**: All coding challenges execute and validate correctly
- [ ] **FLTR-04**: Capstone project exists and is deployable
- [ ] **FLTR-05**: Dart Frog content verified against current APIs (community transition in 2025)

### AI Tutor

- [ ] **TUTR-01**: Tutor uses Socratic method — guides discovery with questions, not direct answers
- [ ] **TUTR-02**: Tutor receives full lesson content as context (not just lesson title)
- [ ] **TUTR-03**: Tutor provides progressive hints (3 levels: nudge, guidance, solution)
- [ ] **TUTR-04**: Tutor helps debug code errors with contextual suggestions tied to lesson concepts
- [ ] **TUTR-05**: ONNX Runtime GenAI upgraded from 0.5.2 to current stable

### UI Enhancement

- [ ] **UIEX-01**: Code examples are interactive — students can edit and run them inline
- [ ] **UIEX-02**: Progress tracking works (streaks, lesson completion, challenge success rates)
- [ ] **UIEX-03**: MaterialDesign theming applied for consistent visual polish
- [ ] **UIEX-04**: Dedicated content section renderers for ANALOGY, WARNING, and other types
- [ ] **UIEX-05**: Improved content rendering (better markdown display, code block formatting)

### Infrastructure

- [ ] **INFR-01**: .bak files removed from version control
- [ ] **INFR-02**: Compiled binaries (bin/obj) removed from git history for csharp capstone
- [ ] **INFR-03**: Minimum runtime versions updated (Python 3.12+, Java 21+, Node 22+, Kotlin 2.0+, Dart 3.0+)
- [ ] **INFR-04**: .gitignore updated to prevent future .bak and binary checkins

## v2 Requirements

### Engagement

- **ENGR-01**: Spaced repetition system for concept review
- **ENGR-02**: Code visualization / step-through debugger
- **ENGR-03**: Concept maps showing relationships between topics
- **ENGR-04**: Achievement/badge system

### Platform

- **PLAT-01**: Cross-platform support (macOS, Linux)

## Out of Scope

| Feature | Reason |
|---------|--------|
| Rust course | Deprecated, never built beyond stub |
| Mobile companion app | Desktop-first, deferred indefinitely |
| Cloud code execution | Local runtimes + Piston sufficient |
| User accounts / auth | Offline desktop app, no login needed |
| Leaderboards / social | Anti-feature for beginner audience — creates anxiety |
| Video tutorials | High production cost, text-based learning is core model |
| AI code generation | Anti-feature — students must write code to learn |
| Community forums | Scope creep, single-developer project |
| Certificates | No accreditation value without institutional backing |

## Traceability

| Requirement | Phase | Status |
|-------------|-------|--------|
| NORM-01 | TBD | Pending |
| NORM-02 | TBD | Pending |
| NORM-03 | TBD | Pending |
| NORM-04 | TBD | Pending |
| NORM-05 | TBD | Pending |
| PYTH-01 | TBD | Pending |
| PYTH-02 | TBD | Pending |
| PYTH-03 | TBD | Pending |
| PYTH-04 | TBD | Pending |
| PYTH-05 | TBD | Pending |
| JAVA-01 | TBD | Pending |
| JAVA-02 | TBD | Pending |
| JAVA-03 | TBD | Pending |
| JAVA-04 | TBD | Pending |
| JAVA-05 | TBD | Pending |
| CSRP-01 | TBD | Pending |
| CSRP-02 | TBD | Pending |
| CSRP-03 | TBD | Pending |
| CSRP-04 | TBD | Pending |
| CSRP-05 | TBD | Pending |
| JSCR-01 | TBD | Pending |
| JSCR-02 | TBD | Pending |
| JSCR-03 | TBD | Pending |
| JSCR-04 | TBD | Pending |
| JSCR-05 | TBD | Pending |
| KTLN-01 | TBD | Pending |
| KTLN-02 | TBD | Pending |
| KTLN-03 | TBD | Pending |
| KTLN-04 | TBD | Pending |
| KTLN-05 | TBD | Pending |
| FLTR-01 | TBD | Pending |
| FLTR-02 | TBD | Pending |
| FLTR-03 | TBD | Pending |
| FLTR-04 | TBD | Pending |
| FLTR-05 | TBD | Pending |
| TUTR-01 | TBD | Pending |
| TUTR-02 | TBD | Pending |
| TUTR-03 | TBD | Pending |
| TUTR-04 | TBD | Pending |
| TUTR-05 | TBD | Pending |
| UIEX-01 | TBD | Pending |
| UIEX-02 | TBD | Pending |
| UIEX-03 | TBD | Pending |
| UIEX-04 | TBD | Pending |
| UIEX-05 | TBD | Pending |
| INFR-01 | TBD | Pending |
| INFR-02 | TBD | Pending |
| INFR-03 | TBD | Pending |
| INFR-04 | TBD | Pending |

**Coverage:**
- v1 requirements: 44 total
- Mapped to phases: 0 (pending roadmap)
- Unmapped: 44

---
*Requirements defined: 2026-02-02*
*Last updated: 2026-02-02 after initial definition*
