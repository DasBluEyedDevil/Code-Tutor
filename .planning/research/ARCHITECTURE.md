# Architecture: Content Audit Process and Curriculum Structure

**Domain:** Systematic content audit of 6 programming language courses
**Researched:** 2026-02-02
**Overall confidence:** HIGH (based on direct codebase analysis + industry curriculum patterns)

---

## Part 1: Current State Assessment

### What Exists

Six courses with the following structure:

| Course | Modules | Lessons | Challenges | Challenge Coverage | Hours (claimed) |
|--------|---------|---------|------------|-------------------|-----------------|
| Java | 16 | 96 | 182 | 96/96 (100%) | 85 |
| Python | 22 | 171 | 166 | 163/171 (95%) | 56 |
| C# | 24 | 132 | 132 | 132/132 (100%) | 29 |
| JavaScript | 21 | 132 | 151 | 132/132 (100%) | 42 |
| Kotlin | 15 | 128 | 80 | 58/128 (45%) | 94 |
| Flutter | 18 | 153 | 217 | 138/153 (90%) | 150 |

### Structural Problems Detected

**1. Duplicate/Conflicting Lessons (CRITICAL)**

Python Module 14 (HTTP/Web APIs) has 26 lessons with duplicate numbering: lessons 02, 03, 04, 05, 06, 11, 12, and 13 each have TWO files (e.g., `02-data-validation-with-pydantic` AND `02-fastapi-fundamentals`). This module was clearly assembled across multiple AI sessions that each generated overlapping content. It also contains topics (Django, PostgreSQL, authentication) that belong in separate modules (and duplicate content that IS in later modules 19-21).

**2. Inconsistent Content Section Types (CRITICAL)**

The WPF app only has dedicated controls for four content types: THEORY, EXAMPLE, KEY_POINT, and LEGACY_COMPARISON. All other types fall through to a generic default renderer. But each course uses different section types:

| Content Type | Java | Python | C# | JS | Kotlin | Flutter | Has Dedicated Control? |
|-------------|------|--------|-----|-----|--------|---------|----------------------|
| THEORY | 389 | 236 | 129 | 125 | 1390 | 861 | YES |
| EXAMPLE | 49 | 317 | 143 | 180 | 193 | 431 | YES |
| KEY_POINT | 177 | 179 | 1 | 17 | 30 | 160 | YES |
| WARNING | 62 | 113 | 105 | 123 | 64 | 73 | NO (default) |
| ANALOGY | 1 | 81 | 130 | 100 | 48 | 37 | NO (default) |
| LEGACY_COMPARISON | 0 | 0 | 0 | 19 | 0 | 0 | YES |
| CODE | 0 | 0 | 0 | 109 | 0 | 0 | NO (default) |
| CONCEPT | 0 | 0 | 0 | 33 | 0 | 0 | NO (default) |
| ARCHITECTURE | 0 | 0 | 11 | 0 | 0 | 0 | NO (default) |
| REAL_WORLD | 0 | 0 | 8 | 0 | 0 | 0 | NO (default) |
| DEEP_DIVE | 0 | 0 | 5 | 0 | 0 | 0 | NO (default) |
| EXPERIMENT | 0 | 0 | 0 | 0 | 1 | 18 | NO (default) |

Key problems:
- C# has only 1 KEY_POINT section across 132 lessons
- Java has only 1 ANALOGY and 49 EXAMPLE sections across 96 lessons -- overwhelmingly theory
- JavaScript uses non-standard types (CODE, CONCEPT) that other courses do not
- Kotlin has 1390 THEORY sections but only 30 KEY_POINTs -- extremely theory-heavy
- WARNING and ANALOGY are common across courses but render as generic defaults

**3. Wildly Inconsistent Estimated Hours**

C# claims 29 hours for 24 modules (132 lessons) while Java claims 85 hours for 16 modules (96 lessons). Python claims 56 hours for 22 modules (171 lessons). These estimates appear arbitrary and were likely set by different AI sessions.

**4. Missing Capstone Projects**

Kotlin has no capstone module. Every other course has at least one.

**5. Challenge Coverage Gaps**

Kotlin has challenges in only 45% of lessons (58/128). This is the weakest coverage and makes many lessons read-only theory with no hands-on practice.

**6. Module Naming Inconsistency**

Flutter uses generic module names: "Module 0: Flutter Development", "Module 2: Flutter Development", etc. These are not descriptive. Other courses use topic-based names.

**7. Course Metadata Inconsistency**

- Course.json schemas differ: Java has totalModules/totalLessons/learningOutcomes/prerequisites; Python has only title/description/difficulty/estimatedHours/prerequisites (empty array)
- Module.json schemas differ: Java has an "order" field; Python does not
- Lesson.json difficulty values inconsistent: some use "beginner"/"intermediate"/"advanced", C# course.json says difficulty is "advanced" (wrong -- it starts from fundamentals)

**8. Module Ordering Anomalies**

C# modules 13-14 (Blazor) are numbered out of order with 15-24, suggesting they were inserted after initial creation. The filesystem sort shows them appearing after module 22.

---

## Part 2: Ideal Curriculum Progressions (Per Language)

Each progression follows this universal pattern validated by industry roadmaps (freeCodeCamp, roadmap.sh, Zero To Mastery, Udemy top courses):

```
Language Fundamentals --> Data Structures & Algorithms --> Developer Tools (Git) -->
  OOP/Advanced Language --> Testing --> Web Fundamentals/APIs -->
  Framework/Platform --> Database --> Auth/Security -->
  Frontend Integration --> Architecture Patterns --> DevOps/Deployment --> Capstone
```

### Python: Zero to Backend Developer

**Current:** 22 modules. Fundamentals are good (Modules 1-12). Module 14 is a disaster (26 lessons, duplicates, mixed FastAPI/Flask/Django). Modules 15-22 cover deployment/testing/Django/PostgreSQL/auth/capstone but arrived from a different session.

**Ideal progression (should be):**

| Phase | Modules | Topics |
|-------|---------|--------|
| 1. Fundamentals | 1-7 | Basics, variables, booleans, loops, lists/tuples, functions, dictionaries |
| 2. Intermediate Python | 8-10 | Exception handling, file I/O, modules/packages |
| 3. OOP & Advanced | 11-12 | Classes, inheritance, decorators, dataclasses, Pydantic |
| 4. Developer Tools | NEW | Git, virtual environments, project structure, linting |
| 5. Testing | 13 | pytest, test architecture, TDD |
| 6. Async Python | 14 | asyncio, structured concurrency, exception groups |
| 7. Web APIs (FastAPI) | 15 | HTTP basics, Pydantic, FastAPI routes, dependency injection |
| 8. Databases | 16 | SQLAlchemy, PostgreSQL, migrations (Alembic) |
| 9. Auth & Security | 17 | JWT, OAuth2, password hashing |
| 10. Django (alt framework) | 18 | Django fundamentals, ORM, views, DRF |
| 11. CLI Tools | 19 | Typer for professional CLI applications |
| 12. Deployment | 20 | Docker, CI/CD, sharing packages |
| 13. Capstone | 21 | Complete full-stack application (personal finance tracker) |

**Key audit actions:**
- Module 14 must be broken apart: extract Django to its own module, de-duplicate FastAPI content, move database/auth lessons to dedicated modules
- Add a Git/developer tools module (Python is missing this entirely)
- Reorder so testing comes before frameworks (test what you build)
- Verify Python 3.13+/3.14 practices throughout

### Java: Zero to Full-Stack Developer

**Current:** 16 modules. Well-structured overall. Git is Module 3 (appropriately early). Spring Boot Module 11 leads to security/React/DevOps/capstone. React frontend integration (Module 13) is an interesting choice.

**Ideal progression (verified against the course):**

| Phase | Current Module | Topics | Status |
|-------|---------------|--------|--------|
| 1. Fundamentals | 1-2 | Java basics, data types, control flow, methods | Good |
| 2. Developer Tools | 3 | Git workflow | Good placement |
| 3. OOP | 4 | Classes, inheritance, polymorphism, interfaces, records, sealed classes | Good |
| 4. Collections & FP | 5-6 | Collections framework, streams, lambdas, functional programming | Good |
| 5. Concurrency | 7 | Virtual threads, structured concurrency | Good |
| 6. Testing & Build | 8 | JUnit, Maven/Gradle | Good |
| 7. Databases | 9 | SQL, JDBC, JPA/Hibernate | Good |
| 8. Web Fundamentals | 10 | HTTP, REST principles | Only 3 lessons -- possibly thin |
| 9. Spring Boot | 11 | Spring Boot 3.4+, REST APIs, dependency injection | Good |
| 10. Security | 12 | JWT, sessions, Spring Security | Good |
| 11. Frontend | 13 | React integration | Verify completeness |
| 12. DevOps | 14 | Docker, CI/CD | Good |
| 13. Full-Stack | 15 | Integration patterns | Good |
| 14. Capstone | 16 | Task Manager application (9 lessons) | Good |

**Key audit actions:**
- Module 10 (Web Fundamentals) has only 3 lessons -- verify sufficient depth for HTTP/REST before Spring Boot
- Verify Java version consistency: course.json says Java 21+ but curriculum docs reference Java 23/25. Content references `IO.println` (JEP 512, Java 25 preview). Must decide: Java 21 LTS (stable) or Java 25 (cutting edge)? Recommend Java 21 LTS as primary, with Java 25 preview features noted as forward-looking
- Java has only 49 EXAMPLE sections and 1 ANALOGY across 96 lessons. Lessons are theory-heavy and need examples and analogies added
- Verify all 182 challenges compile and pass with the chosen Java version

### C#: Zero to Full-Stack .NET Developer

**Current:** 24 modules. Fundamentals (1-10) are solid. Then ASP.NET Core (11), databases (12), Blazor (13-14), testing (15), .NET Aspire (16), AOT (17), clean architecture (18), advanced API (19), auth (20-22), CI/CD (23), capstone (24).

**Ideal progression (with issues noted):**

| Phase | Current Module | Topics | Issue |
|-------|---------------|--------|-------|
| 1. Fundamentals | 1-6 | Getting started, variables, control flow, loops, collections, methods | Good |
| 2. OOP | 7-8 | OOP basics, advanced OOP | Good |
| 3. LINQ | 9 | LINQ and query expressions | Good |
| 4. Async | 10 | Async programming | Good |
| 5. Web APIs | 11 | ASP.NET Core | Good |
| 6. Data | 12 | File I/O, databases, caching | Good |
| 7. Blazor | 13-14 | Interactive UIs, deployment | Misordered in filesystem |
| 8. Testing | 15 | xUnit | Should come earlier (before APIs) |
| 9-17 | 16-24 | Advanced topics, auth, DevOps, capstone | Very advanced cluster |

**Key audit actions:**
- Testing (Module 15) should come before ASP.NET Core (Module 11) or at minimum before Blazor. Students should test their code before building complex UIs
- C# has only 1 KEY_POINT across all 132 lessons. This is a severe content gap -- every lesson needs at least one key takeaway
- 29 estimated hours for 24 modules is absurdly low. Should be 60-80 hours
- Module ordering in filesystem (13-14 appearing after 22) needs fixing
- Verify .NET 9/10 compatibility and C# 13 features throughout
- The advanced modules (16-22) create a very long tail. Consider whether .NET Aspire, Native AOT, and OpenAPI/Scalar are table stakes or advanced topics that could be consolidated

### JavaScript: Zero to Full-Stack with Bun/Hono/React

**Current:** 21 modules. Fundamentals (1-6) use descriptive names with analogies ("The Boxes", "The Loops", "The Recipes"). DOM manipulation (7) before async (8) is correct. TypeScript (10) before server-side (11) is good. Two capstones (API + full-stack).

**Ideal progression:**

| Phase | Current Module | Topics | Issue |
|-------|---------------|--------|-------|
| 1. Fundamentals | 1-6 | Basics, variables, decisions, loops, collections, functions | Module 2 has only 2 lessons -- thin |
| 2. DOM | 7 | Browser manipulation | Good |
| 3. Async | 8 | Promises, async/await | Good |
| 4. Error Handling | 9 | Debugging | Good |
| 5. TypeScript | 10 | TypeScript fundamentals (10 lessons) | Good depth |
| 6. Backend | 11 | Bun + Hono server | Good |
| 7. Databases | 12 | Prisma ORM | Good |
| 8. Frontend | 13 | React 19 | Good |
| 9. Full-Stack | 14 | Integration | Only 4 lessons -- possibly thin |
| 10. Deployment | 15 | Professional tools | Good |
| 11. Testing | 16 | Bun test runner | Should come earlier |
| 12-15 | 17-19 | JSDoc, ES2025, advanced Bun | Nice-to-have but long tail |
| 16-17 | 20-21 | Two capstones | Good |

**Key audit actions:**
- Module 2 (variables) has only 2 lessons. This is a critical fundamental topic that needs expansion
- Testing (Module 16, 10 lessons) should come earlier, ideally before or alongside backend development
- JavaScript uses non-standard content types (CODE: 109, CONCEPT: 33) that other courses do not. Standardize or add app controls
- Modules 17-19 (JSDoc, ES2025, Advanced Bun) may be niche -- evaluate whether they are table stakes or should be consolidated
- Verify Bun/Hono are still the recommended stack (both relatively new; confirm they have not been superseded)

### Kotlin: Zero to Multiplatform Developer

**Current:** 15 modules. Fundamentals (1-2), OOP (3), Advanced Kotlin (4), Coroutines (5), Ktor backend (6), Compose Multiplatform (7), SQLDelight (8), Architecture (9), DI with Koin (10), Testing (11), Deployment (12), Gradle (13), Arrow FP (14), K2 Tooling (15).

**Ideal progression:**

| Phase | Current Module | Topics | Issue |
|-------|---------------|--------|-------|
| 1. Fundamentals | 1-2 | Basics, control flow | Module 1 has 10 lessons (good depth) |
| 2. OOP | 3 | Classes, inheritance | Good |
| 3. Advanced | 4 | 13 lessons (generics, DSLs, etc.) | Very large module |
| 4. Coroutines | 5 | Coroutines, Flows | Good |
| 5. Backend | 6 | Ktor (15 lessons) | Very large module |
| 6. Mobile | 7 | Compose Multiplatform | Good |
| 7. Persistence | 8 | SQLDelight | Good |
| 8. Architecture | 9 | KMP patterns | Good |
| 9. DI | 10 | Koin | Good |
| 10. Testing | 11 | KMP testing | Should come earlier |
| 11. Deployment | 12 | 14 lessons | Very large |
| 12. Gradle | 13 | Build system mastery | Could be earlier |
| 13-14 | 14-15 | Arrow FP, K2 tooling | Advanced/niche |

**Key audit actions:**
- NO CAPSTONE MODULE. This is a critical gap. A capstone pulling together Ktor backend + Compose Multiplatform frontend + SQLDelight persistence is essential
- Only 45% of lessons have challenges. This is the worst coverage of any course. 70 lessons need challenges added
- 1390 THEORY sections but only 30 KEY_POINTs and 48 ANALOGYs. The course reads like a textbook, not an interactive tutorial
- Module naming uses internal IDs ("module-04a", "module-06a", "module-06b", "module-06c") -- clean these up to descriptive names
- Gradle mastery (Module 13) should arguably come earlier since it is needed to configure Kotlin projects from the start
- Testing (Module 11) should come before deployment (Module 12)
- Verify Kotlin 2.0+/K2 compiler features and KMP 1.9+ compatibility

### Flutter/Dart: Zero to Full-Stack App Developer

**Current:** 18 modules. Setup (0), Dart basics (1), Widgets (2-4), Riverpod/MVVM (5), Navigation (6), Dart Frog backend (7), Serverpod (8), Backend Testing (9), API/Auth (10), Real-time (11), Offline (12), Frontend Testing (13), Advanced UI (14), Deployment (15), Production Ops (16), Capstone (17).

**Ideal progression:**

| Phase | Current Module | Topics | Issue |
|-------|---------------|--------|-------|
| 1. Setup | 0 | Flutter environment | Good |
| 2. Dart Fundamentals | 1 | Language basics | Good |
| 3. Widgets | 2-3 | Stateless, Stateful widgets, layouts | Good |
| 4. State Management | 4-5 | setState, then Riverpod/MVVM | Good progression |
| 5. Navigation | 6 | GoRouter, deep linking | Good |
| 6. Backend (Dart Frog) | 7 | File-based routing, middleware | Good |
| 7. Backend (Serverpod) | 8 | Production backend (19 lessons!) | Very large |
| 8. Backend Testing | 9 | Test the backend | Good placement |
| 9. API/Auth | 10 | Integration, auth flows | Good |
| 10. Real-time | 11 | WebSockets, etc. | Good |
| 11. Offline | 12 | Persistence, offline-first | Good |
| 12. Frontend Testing | 13 | Widget tests, integration tests | Good |
| 13. Advanced UI | 14 | Animations, custom painters | Good |
| 14. Deployment | 15-16 | DevOps, production ops | Good |
| 15. Capstone | 17 | Social chat app | Good |

**Key audit actions:**
- Module names are generic ("Module 2: Flutter Development", "Module 3: Flutter Development"). Every module needs a descriptive topic name
- Module 8 (Serverpod) has 19 lessons -- this is unusually large. Consider splitting into "Serverpod Basics" and "Serverpod Advanced"
- Having both Dart Frog AND Serverpod creates confusion. Decide: one as primary, one as alternative? Or Dart Frog for learning, Serverpod for production?
- 150 estimated hours is the most of any course. Verify this is realistic
- Check Flutter 3.27+/Dart 3.6+ compatibility throughout
- Flutter prerequisite says "Basic programming knowledge recommended" but other courses say "No prior experience". Since Module 1 teaches Dart from scratch, the prerequisite should be removed or clarified

---

## Part 3: Content Audit Methodology

### The Three-Pass Audit

Every lesson should be reviewed in three passes, each catching different problems.

#### Pass 1: Structural Integrity (Automated/Semi-automated)

Check without reading content. Can be partially scripted.

| Check | What to Verify | Pass/Fail Criteria |
|-------|---------------|-------------------|
| Metadata completeness | lesson.json has id, title, description, moduleId, order, estimatedMinutes, difficulty | All fields present and non-empty |
| Content sections exist | At least 1 THEORY section | Fail if no content directory or empty |
| Section type validity | All sections use standard types | THEORY, EXAMPLE, KEY_POINT, ANALOGY, WARNING only (or add controls for others) |
| Frontmatter present | Every .md has valid YAML frontmatter with type and title | Fail if missing frontmatter |
| Challenge exists | At least 1 challenge per lesson | Fail if no challenge (exception: pure-theory introductory lessons) |
| Challenge files complete | challenge.json + starter.{lang} + solution.{lang} all present | Fail if any missing |
| Test cases exist | challenge.json has at least 1 testCase with expectedOutput | Fail if no test cases |
| No duplicate numbering | No two files in content/ share the same number prefix | Fail if duplicates found |
| Section balance | At least 1 THEORY + 1 EXAMPLE or KEY_POINT per lesson | Warn if all-theory lesson |

#### Pass 2: Content Quality (Human/AI Review)

Read content and evaluate against rubric.

**Per-Lesson Quality Rubric:**

| Dimension | Score 1 (Poor) | Score 3 (Adequate) | Score 5 (Excellent) |
|-----------|---------------|-------------------|-------------------|
| **Accuracy** | Contains errors, deprecated syntax, wrong API | Mostly correct, minor issues | All code correct, current APIs, best practices |
| **Completeness** | Major concepts missing, unexplained leaps | Core concepts covered, some gaps | Thorough coverage, no knowledge gaps |
| **Freshness** | Uses deprecated patterns, old versions | Mostly current, a few outdated references | Current stable versions, 2025-2026 practices |
| **Progression** | Assumes knowledge not yet taught, or re-explains basics | Generally builds on prior lessons | Perfect scaffolding, each concept builds on the last |
| **Pedagogical Quality** | Walls of text, no examples, no analogies | Has theory and examples | Theory + analogy + example + key point + warning flow |
| **Challenge Quality** | Trivial/impossible, no hints, bad test cases | Appropriate difficulty, some hints | Well-scaffolded, good hints, comprehensive tests |
| **Voice & Tone** | Academic/robotic or overly casual | Generally consistent | Friendly expert, consistent Socratic style |

**Minimum passing score:** Average of 3.0 across all dimensions. Any single dimension below 2.0 requires immediate fix.

#### Pass 3: Cross-Lesson Coherence (Module-Level Review)

Review the module as a whole unit after individual lesson review.

| Check | What to Verify |
|-------|---------------|
| Progressive difficulty | Each lesson is harder than the last within a module |
| No knowledge gaps | Lesson N+1 does not require concepts not taught in lessons 1..N |
| No redundancy | Concepts are taught once, reinforced with examples, not re-explained |
| Consistent terminology | Same term used for same concept (not "function" in one lesson, "method" in another, unless the distinction is being taught) |
| Module learning arc | Module begins with "why" motivation and ends with a mini-project or synthesis |
| Correct ordering | Lesson order fields match filesystem ordering |
| Appropriate length | Module has 5-10 lessons (not 1-2, not 26) |

### Content Section Flow Within a Lesson

Based on Bloom's Taxonomy progression (Remember -> Understand -> Apply -> Analyze -> Evaluate -> Create) and industry best practice from top coding education platforms, each lesson should follow this flow:

```
1. THEORY (The Problem)
   - What problem does this concept solve?
   - Why does the learner need this?
   - Connect to previous lesson: "In the last lesson you learned X. But what if you need to Y?"

2. ANALOGY (Make It Click)
   - Real-world analogy that maps to the concept
   - "Think of [concept] like [familiar thing]..."
   - Provides mental model before showing code

3. THEORY (The Solution)
   - Introduce the concept/syntax/API
   - Explain how it works
   - Show the mental model

4. EXAMPLE (See It Work)
   - Complete, working code example
   - Annotated line-by-line
   - Output shown

5. KEY_POINT (Remember This)
   - 2-3 bullet points of critical takeaways
   - The "if you remember nothing else" summary

6. WARNING (Watch Out)
   - Common mistakes with this concept
   - What breaks and why
   - How to fix it

7. [CHALLENGE] (Your Turn)
   - Hands-on practice applying the concept
   - Scaffolded from the example (similar but not identical)
   - Progressive hints available
```

Not every lesson needs every section type, but the minimum viable lesson is:

```
THEORY + EXAMPLE + KEY_POINT + CHALLENGE (minimum)
```

Richer lessons add ANALOGY and WARNING sections. The existing courses show this minimum is often not met -- Java has lessons with only THEORY + KEY_POINT (no examples), and Kotlin has lessons that are pure theory.

### Standardizing Content Section Types

**Decision needed:** The app currently handles THEORY, EXAMPLE, KEY_POINT, and LEGACY_COMPARISON with dedicated controls. All other types fall through to a generic renderer.

**Recommended standard set:**

| Type | Purpose | Render Priority |
|------|---------|----------------|
| THEORY | Conceptual explanation, the "what" and "why" | Existing control |
| EXAMPLE | Working code with explanation | Existing control |
| KEY_POINT | Essential takeaways to remember | Existing control |
| ANALOGY | Real-world metaphor to build mental model | Needs dedicated control (currently 397 across all courses) |
| WARNING | Common mistakes, gotchas, pitfalls | Needs dedicated control (currently 540 across all courses) |
| LEGACY_COMPARISON | Old way vs. new way comparison | Existing control (only 19 uses in JS) |

**Types to migrate/eliminate:**

| Current Type | Action | Migrate To |
|-------------|--------|-----------|
| CODE (109 in JS) | Merge into EXAMPLE | EXAMPLE |
| CONCEPT (33 in JS) | Merge into THEORY | THEORY |
| ARCHITECTURE (11 in C#) | Merge into THEORY | THEORY |
| REAL_WORLD (8 in C#) | Merge into ANALOGY | ANALOGY |
| DEEP_DIVE (5 in C#) | Merge into THEORY | THEORY |
| EXPERIMENT (19 in Kotlin/Flutter) | Merge into EXAMPLE | EXAMPLE |

This reduces the content type taxonomy to 6 types, 4 of which have dedicated controls. ANALOGY and WARNING need dedicated controls added to the app (or the default renderer needs to be improved to handle them well -- they are common enough to warrant visual distinction).

---

## Part 4: Audit Build Order

### Principle: Start with the Most Structured, End with the Most Broken

Audit courses in order of current structural quality. This lets you:
1. Establish the "gold standard" on the best course first
2. Apply that standard with increasing confidence to worse courses
3. Build audit muscle memory on easier cases before tackling hard ones

### Recommended Audit Order

**Phase 1: Java (best structure, moderate content issues)**
- Rationale: 16 well-organized modules, 100% challenge coverage, clear progression. Main issues are content-level (too theory-heavy, needs examples/analogies) not structural
- Estimated effort: MEDIUM (structure is fine, content needs enrichment)
- Sets the template for what "done" looks like

**Phase 2: JavaScript (good structure, minor gaps)**
- Rationale: 21 modules with good progression, 100% challenge coverage. Issues are Module 2 thin (2 lessons), non-standard content types, testing module placement
- Estimated effort: MEDIUM (structural tweaks + content type migration)

**Phase 3: C# (good structure, metadata issues)**
- Rationale: 24 modules, 100% challenge coverage. Issues are module ordering, near-zero KEY_POINTs, low estimated hours, advanced module cluster
- Estimated effort: MEDIUM-HIGH (structural reorder + massive content gap in KEY_POINTs)

**Phase 4: Flutter (decent structure, naming and scope issues)**
- Rationale: 18 modules, 90% challenge coverage. Issues are generic module names, very large modules (Serverpod 19 lessons), dual backend confusion
- Estimated effort: MEDIUM-HIGH (naming, splitting, 15 lessons need challenges)

**Phase 5: Kotlin (structural gaps)**
- Rationale: 15 modules but NO capstone, only 45% challenge coverage, internal-ID naming, theory-dominated content
- Estimated effort: HIGH (needs capstone created, 70 lessons need challenges, content enrichment)

**Phase 6: Python (structural damage)**
- Rationale: 22 modules but Module 14 has 26 lessons with duplicates. Needs major restructuring (break apart Module 14, add Git module, reorder modules)
- Estimated effort: HIGH (structural surgery + content review)

### Within Each Course: Module Audit Order

Audit modules in this order within each course:

1. **First module** (the "onboarding" -- highest impact, most students see it)
2. **Last module / capstone** (the "destination" -- what students are building toward)
3. **Remaining modules in order** (catch progression gaps as you go)

This "bookend first" approach ensures the beginning and end are solid before filling in the middle.

---

## Part 5: Cross-Course Consistency Standards

### Metadata Schema (Should Be Identical Across All Courses)

**course.json:**
```json
{
  "id": "string (language name)",
  "language": "string (language identifier for code execution)",
  "title": "string (Full course title)",
  "description": "string (2-3 sentence overview)",
  "difficulty": "beginner-to-advanced",
  "estimatedHours": "number (realistic total)",
  "totalModules": "number",
  "totalLessons": "number",
  "prerequisites": ["string array (can be empty for zero-experience courses)"],
  "learningOutcomes": ["string array (5-10 measurable outcomes)"]
}
```

**module.json:**
```json
{
  "id": "string (module-XX)",
  "title": "string (descriptive topic name, no 'Module X:' prefix)",
  "description": "string (what the student will learn)",
  "difficulty": "beginner | intermediate | advanced",
  "estimatedHours": "number",
  "order": "number (matches filesystem prefix)"
}
```

**lesson.json:**
```json
{
  "id": "string (unique across course)",
  "title": "string (descriptive, no lesson number prefix)",
  "description": "string (one sentence, what the student will learn/build)",
  "moduleId": "string (matches parent module id)",
  "order": "number (matches filesystem prefix)",
  "estimatedMinutes": "number (15-45 typical)",
  "difficulty": "beginner | intermediate | advanced"
}
```

### Voice and Tone Standards

All courses should share:
- **Second person:** "You will learn..." not "The student will learn..."
- **Active voice:** "Write a function that..." not "A function should be written..."
- **Practical framing:** Start lessons with the problem, not the theory
- **Socratic style:** Ask questions that lead to understanding: "What happens if we try...?"
- **No jargon without definition:** First use of any term must include a definition or analogy
- **Consistent terminology:** Use the same word for the same concept. Create a glossary per course if needed

### Freshness Standards (As of 2026)

| Language | Current Stable Version (Target) | Key Modern Features to Use |
|----------|-------------------------------|---------------------------|
| Python | 3.13+ (note 3.14 where applicable) | Match/case, exception groups, dataclasses with slots, type hints |
| Java | 21 LTS (primary), note 25 features | Records, sealed classes, pattern matching, virtual threads, text blocks |
| C# | 13 (.NET 9/10) | Primary constructors, collection expressions, raw string literals |
| JavaScript | ES2025 | Array.fromAsync, Promise.withResolvers, Set methods, decorators |
| Kotlin | 2.0+ (K2 compiler) | Context receivers, value classes, KMP stable |
| Dart | 3.6+ (Flutter 3.27+) | Class modifiers, patterns, records, macros (if stable) |

### Challenge Standards

Every challenge should have:
- `type`: FREE_CODING or MULTIPLE_CHOICE
- `description`: Clear problem statement
- `instructions`: Step-by-step what to do
- `testCases`: At least 2 (one basic, one edge case), each with expectedOutput
- `hints`: At least 2 levels (nudge, then near-solution)
- `commonMistakes`: At least 1 with mistake/consequence/correction
- `starter.{lang}`: Compilable scaffold with TODOs
- `solution.{lang}`: Working reference solution that passes all test cases

---

## Part 6: Curriculum Anti-Patterns to Fix

### Anti-Pattern 1: The Knowledge Cliff

**Symptom:** Lesson N teaches "Hello World", lesson N+1 teaches dependency injection.
**Found in:** Check each course's early-to-intermediate transition (Modules 3-5 typically).
**Fix:** Insert bridging lessons. If the difficulty jump between consecutive lessons exceeds one Bloom's level, add an intermediate lesson.

### Anti-Pattern 2: The Monolith Module

**Symptom:** One module has 15+ lessons covering multiple unrelated topics.
**Found in:** Python Module 14 (26 lessons!), Kotlin Module 4 (13 lessons), Kotlin Module 6 (15 lessons), Flutter Module 8 (19 lessons).
**Fix:** Split into focused modules of 5-10 lessons each. Each module should have ONE clear learning objective.

### Anti-Pattern 3: Theory Without Practice

**Symptom:** Multiple consecutive THEORY sections with no EXAMPLE or CHALLENGE.
**Found in:** Kotlin (1390 THEORY, 45% challenge coverage), Java (389 THEORY, 49 EXAMPLE).
**Fix:** Every theory concept needs a code example within 2 sections. Every lesson needs at least one challenge.

### Anti-Pattern 4: Orphaned Advanced Content

**Symptom:** Advanced modules that assume knowledge from nowhere (students dropped off before reaching them).
**Found in:** C# Modules 16-22 (7 advanced modules in a row), Kotlin Modules 13-15 (Gradle/Arrow/K2 feel disconnected).
**Fix:** Ensure each advanced module explicitly states prerequisites and connects back to earlier content. Consider making some advanced modules optional/bonus.

### Anti-Pattern 5: Inconsistent Code Style

**Symptom:** Lesson 3 uses `var` everywhere, Lesson 7 uses explicit types, Lesson 12 mixes both with no explanation.
**Found in:** Cannot detect without reading content, but high risk given multi-session AI authoring.
**Fix:** Establish a code style guide per language and audit every code example against it.

### Anti-Pattern 6: Stale Framework Versions

**Symptom:** "Install Spring Boot 2.7" when 3.4 is current. "Use React 17" when 19 is current.
**Found in:** High risk across all courses. The Java content references Java 25 preview features (IO.println, JEP 512) which may not be stable.
**Fix:** Pin to current stable versions. Note preview/beta features explicitly.

---

## Part 7: Roadmap Implications

### Recommended Phase Structure for the Audit Milestone

**Phase 1: Structural Audit and Standards (all courses)**
- Run automated structural checks (metadata, section types, challenge presence)
- Establish the standardized schema, content types, and voice guide
- Fix metadata across all courses to match standard schema
- Migrate non-standard content types (CODE -> EXAMPLE, CONCEPT -> THEORY, etc.)
- Fix Python Module 14 duplicate files (structural surgery)
- Add ANALOGY and WARNING controls to the app (or improve default renderer)

**Phase 2: Content Audit - Java and JavaScript (gold standard)**
- Full three-pass audit of Java (96 lessons)
- Full three-pass audit of JavaScript (132 lessons)
- These establish the quality template

**Phase 3: Content Audit - C# and Flutter**
- Full three-pass audit of C# (132 lessons), including KEY_POINT addition
- Full three-pass audit of Flutter (153 lessons), including module renaming

**Phase 4: Content Audit - Kotlin and Python (heaviest work)**
- Full three-pass audit of Kotlin (128 lessons) + create capstone + add 70 challenges
- Full three-pass audit of Python (171 lessons) + restructure modules

**Phase 5: Cross-Course Verification**
- Verify consistent voice/tone across all 6 courses
- Verify all challenges execute in the app
- Verify all content renders properly
- Final quality gate review

### Why This Order

1. **Structural fixes first** because broken structure makes content audit impossible (how do you review Python Module 14 when it has duplicate lessons?)
2. **Java first** because it has the best structure and will establish the template fastest
3. **JavaScript second** because it pairs well with Java (similar structural quality) and catches the non-standard content type issue
4. **C# and Flutter middle** because they need moderate structural work plus content review
5. **Kotlin and Python last** because they need the most work (missing capstone, duplicate modules, low challenge coverage) and benefit from the most mature audit process
6. **Cross-course verification last** because consistency checking requires all courses to be individually complete

---

## Sources

### Direct Codebase Analysis (HIGH confidence)
- All course/module/lesson/challenge counts from filesystem analysis
- Content section type distribution from filename analysis across all courses
- WPF app section rendering from `LessonPage.xaml.cs` (lines 105-112)
- Content parsing from `CourseService.cs` (lines 243-254)
- Existing review template from `scripts/review-templates/lesson-review-prompt.md`
- Course structure metadata from `course_structure/` directory (Perplexity-generated curriculum docs)

### Industry Curriculum References (MEDIUM confidence)
- [freeCodeCamp Full Stack Curriculum 2025](https://forum.freecodecamp.org/t/full-stack-curriculum-update-september-2025/760885)
- [roadmap.sh Backend Developer Roadmap](https://roadmap.sh/backend)
- [NareshIT Full Stack Java Course Syllabus 2025](https://nareshit.com/blogs/full-stack-java-course-syllabus-2025-updated-guide)
- [ASP.NET Core Developer Roadmap 2026](https://github.com/MoienTajik/AspNetCore-Developer-Roadmap)
- [codewithmukesh .NET Developer Roadmap 2026](https://codewithmukesh.com/blog/dotnet-developer-roadmap/)
- [JetBrains KMP Learning Journey](https://lp.jetbrains.com/kmp-level-up/)
- [KMP Developer Roadmap (GitHub)](https://github.com/skydoves/kmp-developer-roadmap)
- [Flutter Official Learning Path](https://flutter.dev/learn)
- [Hono + Bun Getting Started](https://hono.dev/docs/getting-started/bun)
- [BHVR Monorepo Template (Bun/Hono/Vite/React)](https://github.com/stevedylandev/bhvr)

### Pedagogical References (MEDIUM confidence)
- [Effective Curriculum Audits Guide](https://alisonyang.com/master-curriculum-audits-guide/)
- [Bloom's Taxonomy - UCF Teaching Resources](https://fctl.ucf.edu/teaching-resources/course-design/blooms-taxonomy/)
- [CSTA PD Alignment Rubrics](https://csteachers.org/pd-alignment-rubrics/)
- [ACM CCECC Assessment Rubric](http://ccecc.acm.org/guidance/computer-science/rubric/)
- [Students' Misconceptions in Introductory Programming (ACM TOCE)](https://dl.acm.org/doi/10.1145/3077618)

---

*Architecture analysis: 2026-02-02*
