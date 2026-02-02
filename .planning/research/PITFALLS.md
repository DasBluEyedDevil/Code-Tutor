# Domain Pitfalls

**Domain:** Code education platform with AI-generated multi-language curriculum
**Researched:** 2026-02-02
**Confidence:** HIGH (grounded in codebase evidence + industry research)

---

## Critical Pitfalls

Mistakes that cause rewrites, student harm, or fundamental credibility loss.

---

### CRITICAL-1: Phantom API Syndrome -- AI-Generated Code References Non-Existent APIs

**What goes wrong:** AI tools confidently generate code using APIs, methods, or classes that do not exist. Students copy-paste, get errors, and lose trust in the entire course. Research shows 20% of AI-generated code references non-existent packages, and 42% of AI suggestions are incorrect in complex scenarios.

**Evidence in Code Tutor:** The Java course teaches `IO.println()` as a JEP 512 feature throughout 75 files (104 occurrences). While JEP 512 was finalized in JDK 25 (September 2025), the backup file (`course.json.bak`) also references a bare `println()` without `IO.` prefix, which was the older preview syntax. The content was clearly generated across multiple sessions -- some using the pre-finalization syntax (`println()`) and some using the finalized syntax (`IO.println()`). Both `IO.println` (104 occurrences) and `System.out.println` (325 occurrences in 98 files) coexist in the same course, which is pedagogically confusing but not technically wrong -- however, students will be confused about which to use and when.

**Why it happens:** LLMs hallucinate APIs with high confidence. When content was generated at different times, different AI sessions had different training data about preview vs. finalized features. The AI writes code that "looks right" but may reference preview syntax, deprecated methods, or entirely fabricated APIs.

**Consequences:**
- Student code does not compile; student blames themselves instead of the content
- Students learn non-existent patterns they later have to unlearn
- Trust in the platform collapses after 2-3 broken examples
- On forums and reviews, "the code doesn't even work" is a death sentence

**Warning signs:**
- Code examples that use unfamiliar method names or classes
- Imports referencing packages that do not exist on package managers
- Version claims that do not match any released version
- Inconsistent API usage across lessons in the same course

**Prevention:**
1. Every code example must be compiled/run before publication
2. Create a per-language "API verification checklist" of every non-standard method used
3. For cutting-edge features (JEP 512, ES2025, Python 3.13+), verify against official release notes with URLs
4. Automated CI that compiles every solution file against the target language version

**Detection:** Search all solution files for method calls, cross-reference against official documentation. Grep for inconsistent patterns (e.g., `IO.println` vs `println` vs `System.out.println` in the same course).

**Phase mapping:** Content audit phase -- must be addressed first, before any other content improvements.

**Code Tutor specifics:**
- Java: `IO.println` vs `System.out.println` inconsistency (both present across the course)
- Java: Lesson title says "Java 23" but content says "Java 25" (e.g., `06-lesson-16-modern-java-syntax-writing-less-code-java-23` folder name vs content referencing Java 25)
- JavaScript: Bun/Hono stack is current but version-sensitive; verify all Bun APIs still exist
- Flutter: Dart Frog content needs version verification (framework transitioned to community-led in July 2025)
- Kotlin: K2 compiler content must be verified against Kotlin 2.0+ stable releases
- Python: Claims "Python 3.14 features noted where applicable" -- verify which features are actually stable

---

### CRITICAL-2: The Knowledge Cliff -- Sudden Jumps from Basics to Frameworks

**What goes wrong:** Courses teach variables, loops, and functions at a gentle pace, then suddenly jump to Spring Boot, Django, or Riverpod with no bridging material. Students hit a wall and abandon the course. This is the number one reason learners quit "zero to full stack" courses.

**Evidence in Code Tutor:**
- Java: Goes from Module 5 (Collections/Functional) to Module 6 (Streams), then Module 7 (Concurrency/Virtual Threads), then Module 9 (Databases/SQL), then Module 11 (Spring Boot). The jump from "what is a for loop" to "Virtual Threads" to "Spring Boot dependency injection" is enormous.
- Flutter: Module 5 (generic "flutter development") jumps to Module 6 (MVVM Architecture with Riverpod) -- a massive conceptual leap for someone who just learned about widgets.
- C#: Claims `difficulty: "advanced"` in the course metadata but is supposed to start from zero. Module 7 is "OOP Basics" but Module 11 is "ASP.NET Core Web APIs" -- the gap between knowing classes and building REST APIs is huge.
- Kotlin: Goes from Module 4 (Advanced Kotlin) to Module 5 (Coroutines/Flows) -- coroutines are notoriously difficult for beginners.

**Why it happens:** Each module was generated in a separate AI session. The AI that generated Module 11 (Spring Boot) did not have context about what Module 2 (loops) actually taught. Each session assumes different levels of prerequisite knowledge.

**Consequences:**
- 60-80% learner dropout at the "framework transition" point
- Students feel stupid rather than recognizing the content gap
- Students who push through have fragile understanding and cannot debug real problems
- The course promise ("zero to full stack") becomes a lie

**Warning signs:**
- Modules that introduce 5+ new concepts simultaneously
- No "bridge" lessons between fundamentals and frameworks (e.g., "What is a web server?" before Spring Boot)
- Estimated time per module varies wildly (10 minutes vs 45 minutes for similar complexity)
- Later modules assume knowledge not explicitly covered in earlier modules

**Prevention:**
1. Create explicit prerequisite chains: every module lists exactly which prior lessons it requires
2. Add "bridge" modules between fundamentals and frameworks (e.g., "HTTP and the Web" before any backend framework)
3. Ensure estimated times are calibrated against actual complexity, not just content volume
4. Test with a "fresh eyes" review: would someone who ONLY read the prior lessons understand this one?

**Detection:** Map every concept introduced in each lesson. Flag any lesson that uses a concept not introduced in a prior lesson. Look for estimate time jumps (10min -> 45min in consecutive lessons).

**Phase mapping:** Content audit phase (diagnosis), then content restructuring phase (fix). This requires rewriting module ordering, not just fixing individual lessons.

**Code Tutor specifics:**
- Java Module 3 is "Git Development Workflow" placed between OOP and Collections -- it breaks the programming flow
- C# has 24 modules (the most of any course) but claims only 29 estimated hours -- averaging 72 minutes per module for topics like "Clean Architecture" and "CI/CD with GitHub Actions" is absurdly low
- Kotlin's "Gradle Mastery" module appears at Module 13, far after students would need it for project setup
- JavaScript has 21 modules but only 42 estimated hours -- some advanced modules would need far more

---

### CRITICAL-3: Cross-Course Identity Crisis -- Six Courses, Six Different Personalities

**What goes wrong:** When each course is generated by different AI sessions (or different AI models), students experience a jarring shift in voice, structure, depth, and approach when switching between courses or even within a single course. The platform feels like a collection of unrelated tutorials rather than a cohesive learning product.

**Evidence in Code Tutor:**

**ID schema inconsistency** (confirmed in codebase):
| Course | Lesson ID Format | Example |
|--------|-----------------|---------|
| Java | `epoch-X-lesson-Y` | `epoch-0-lesson-1` |
| JavaScript | `X.Y` (dotted numeric) | `1.1` |
| Kotlin | `X.Y` (dotted numeric) | `1.1` |
| C# | `lesson-XX-YY` (hyphenated) | `lesson-01-01` |
| Python | `module-XX-lesson-YY` | `module-01-lesson-01` |
| Flutter | Mixed patterns | Various |

**Metadata schema inconsistency:**
- Java lessons have `description` fields; JavaScript and Kotlin lessons do NOT
- Python `course.json` has no `totalModules`/`totalLessons`/`learningOutcomes`/`prerequisites`; Java has all of these
- C# course says `difficulty: "advanced"` while all others say `"beginner"` -- but C# also starts from "What is Programming?"
- Estimated hours vary enormously: Flutter (150h), Kotlin (94h), Java (85h), Python (56h), JavaScript (42h), C# (29h) -- the C# figure is implausible for 24 modules covering topics through CI/CD

**Title format inconsistency:**
- Java: `"Lesson 1.1: What is a Computer Program?"` (numbered prefix)
- JavaScript: `"What Is Programming? (The Recipe Analogy)"` (no number, subtitle)
- Kotlin: `"Lesson 1.1: Introduction to Kotlin & Development Setup"` (numbered prefix)
- C#: `"What is Programming?"` (bare title)
- Python: `"What is Programming, Really?"` (bare title with flair)

**Why it happens:** Each course was a fresh AI conversation. No shared style guide, no template, no coordination. The AI chose its own conventions each time.

**Consequences:**
- Platform feels unprofessional and cobbled together
- Navigation/search features break when metadata schemas differ
- Students cannot predict content structure, increasing cognitive load
- Maintenance becomes a nightmare (every fix is different per course)

**Warning signs:**
- JSON schemas differ between courses
- Tone shifts between courses or within a single course
- Some lessons have descriptions, others do not
- Module naming conventions differ (e.g., "Module 1: X" vs "The Absolute Basics" vs "01-getting-started")

**Prevention:**
1. Define a SINGLE content schema that all courses must follow (mandatory fields, ID format, title format)
2. Create a style guide document before auditing (voice, tone, complexity targets, analogy density)
3. Use schema validation (JSON Schema) in CI to reject non-conforming content
4. Run a "normalization pass" before the content quality pass

**Detection:** Write a script that loads every `lesson.json` and `course.json` across all 6 courses and reports schema differences, missing fields, and format inconsistencies.

**Phase mapping:** Must be the FIRST thing done in the content audit. Normalize the container (schema) before fixing the contents. Otherwise, auditors making fixes will each introduce their own format, making the problem worse.

**Code Tutor specifics:** All 6 courses are affected. The normalization is mechanical (rename fields, add missing fields, standardize formats) and should be automated where possible.

---

### CRITICAL-4: Broken Challenges That Cannot Execute

**What goes wrong:** Coding challenges reference APIs, use syntax, or require dependencies that the execution environment does not support. The student writes correct code but the validator says it is wrong, or the code does not compile at all.

**Evidence in Code Tutor:**
- Java challenges use `IO.println()` which requires JDK 25+ with compact source file support. If the execution environment uses an older JDK, every challenge fails.
- Java challenges use `void main()` without `public static void main(String[] args)` -- this requires JDK 25+ compact source file mode. Standard `javac` compilation without the right flags will reject this.
- The app executes code via "local runtime detection" -- the student's installed JDK version must match the code's syntax requirements.
- Challenge validation uses `expectedOutput` string matching (confirmed in challenge.json files). If the solution code produces output with different whitespace, line endings, or formatting than expected, valid solutions are rejected.

**Why it happens:** AI generates code targeting the latest language version. The execution environment may not support that version. String-based output matching is brittle and fails on formatting differences.

**Consequences:**
- Students cannot complete challenges despite correct logic
- Students believe they are wrong when the platform is wrong
- The core value proposition (interactive learning) is destroyed
- Students cannot distinguish between their mistakes and platform bugs

**Warning signs:**
- Challenges that use cutting-edge syntax (compact source files, pattern matching, etc.)
- String-exact `expectedOutput` without tolerance for whitespace
- No documentation of required runtime versions per course
- Solution files that have not been run against the actual execution engine

**Prevention:**
1. Document required runtime versions for each course and verify the execution engine supports them
2. Run EVERY solution file through the ACTUAL app execution pipeline as a CI check
3. Use output matching that trims whitespace and normalizes line endings
4. For Java specifically: decide whether to target JDK 25 compact syntax or traditional syntax, and be consistent
5. Add a "runtime version check" feature to the app that warns students when their installed runtime is too old

**Detection:** Existing `ChallengeValidationTests.cs` checks structure but not execution. Need to add execution-based validation.

**Phase mapping:** Challenge validation phase -- must run after content normalization (CRITICAL-3) and API verification (CRITICAL-1). Requires the execution engine to be working correctly first.

**Code Tutor specifics:**
- Java: The entire early course depends on JDK 25 compact source files. If students have JDK 21 LTS (the previous LTS), nothing works. This is a critical decision point: target JDK 25 (new LTS) or support JDK 21 with fallback?
- JavaScript: Bun-specific APIs (`Bun.file()`, `Bun.serve()`) require Bun runtime. If execution uses Node.js, these fail.
- C#: Roslyn executor is in-process and version-locked to the app's .NET version. Content must match.
- Kotlin: K2 compiler features require specific Kotlin compiler version.
- Flutter/Dart: Dart Frog challenges require the `dart_frog` package to be available.

---

## Moderate Pitfalls

Mistakes that cause delays, technical debt, or degraded learning experience.

---

### MODERATE-1: The Audit Itself Introduces Inconsistency

**What goes wrong:** Multiple reviewers audit different courses (or different parts of the same course) without shared standards. Reviewer A rewrites lessons in a casual conversational tone; Reviewer B uses formal academic language. Reviewer A adds type annotations everywhere; Reviewer B prefers inference. The audit creates more inconsistency than existed before.

**Why it happens:** Content audits are inherently distributed work. Without a shared rubric, reviewers make different judgment calls. If AI tools are used for the audit itself, each AI session makes different stylistic choices.

**Consequences:**
- Audit undoes itself: fixed content is inconsistent in new ways
- Rework required to re-audit the audit
- Timeline doubles or triples
- Quality bar becomes a moving target

**Warning signs:**
- No written style guide before audit begins
- Different reviewers assigned to different courses without calibration
- "Fix it as you see fit" instructions without examples
- No review of the first few audited lessons before continuing

**Prevention:**
1. Write a content style guide BEFORE the audit begins. Include: voice/tone, code style per language, analogy density, explanation depth, formatting rules.
2. Audit one lesson from each course as a "calibration pass" -- review the calibration output, refine the guide, then continue.
3. Use a standardized rubric (checklist) for every lesson, not subjective judgment.
4. If using AI for the audit, provide the style guide as context in every session.
5. Run a consistency check AFTER the audit (automated where possible).

**Detection:** Compare tone, style, and formatting of newly audited lessons across courses. Look for systematic differences.

**Phase mapping:** Pre-audit setup phase. The style guide and rubric must exist before any content changes begin.

---

### MODERATE-2: Fixing Content Breaks Lesson Dependencies

**What goes wrong:** An auditor fixes Lesson 5 to use better patterns. But Lesson 8 references the old code from Lesson 5. Now Lesson 8 makes no sense because it builds on something that no longer exists.

**Why it happens:** AI-generated lessons sometimes reference earlier code by repeating it or building upon it. If you change the foundation, everything above it shifts. Without a dependency graph, auditors cannot predict the blast radius of changes.

**Consequences:**
- Students encounter "undefined variable" or "unknown concept" errors in later lessons
- Fixes cascade: fixing one lesson requires fixing 3 more that reference it
- Audit timelines balloon unpredictably

**Warning signs:**
- Lessons that say "remember the X we built in Lesson Y"
- Variable names or class names that appear in multiple lessons
- Progressive projects (like "mini-project" or "capstone") that build across lessons

**Prevention:**
1. Build a forward-reference map before editing: for each lesson, what later lessons reference it?
2. When modifying a lesson, search all subsequent lessons for references to changed content
3. Edit in dependency order (prerequisites first, dependents second)
4. For progressive projects, treat all project lessons as a single atomic unit

**Detection:** Grep for cross-references (lesson titles, variable names, class names) across lessons. Build a dependency DAG.

**Phase mapping:** Content audit phase. Build the dependency map as a pre-step before making edits.

**Code Tutor specifics:**
- Java Module 2 teaches `IO.println` which is used in every subsequent module. Changing this single decision cascades through 75+ files.
- Python capstone (Module 22) likely references patterns from many earlier modules.
- Flutter capstone (Module 18) spans the entire course.

---

### MODERATE-3: Version Claim Staleness -- Content Claims "Latest" But Is Not

**What goes wrong:** Course metadata and content claim to cover specific versions ("Java 21+", "Python 3.13+", "Spring Boot 3.4+", ".NET 8/9", "ES2025", "Kotlin 2.0") but the code examples reflect older or preview versions. Since content was generated at different times, different lessons within the same course may target different versions.

**Evidence in Code Tutor:**
- Java course.json says "Java 21+" but early lessons teach JEP 512 (JDK 25 feature). The backup file references "Java 23" in several places while current content says "Java 25". The folder name literally says `java-23` while content says `java-25`.
- Python course says "3.13+ features like Exception Groups and TaskGroups" -- but Exception Groups (PEP 654) shipped in Python 3.11 and TaskGroups in 3.11. These are not "3.13+" features.
- C# course references ".NET 8/9" features but the lesson on params collections says "Requires .NET 9 / C# 13!" -- the course difficulty claims "advanced" but starts from "What is Programming?"
- JavaScript course says "Updated for ES2025 with Bun/Hono stack" -- ES2025 features need verification against the actual spec finalization.

**Why it happens:** AI generates plausible-sounding version claims. When the AI was trained, some features were in preview; now they may be released, deprecated, or changed. Different generation sessions targeted different versions.

**Consequences:**
- Students install the wrong version and code does not work
- Students learn "wrong" version numbering (confusing 3.11 features with 3.13)
- Credibility loss when knowledgeable students notice errors
- Search engines surface the course for wrong version queries

**Prevention:**
1. Create a "versions manifest" per course: exact target language version, framework versions, and dependency versions
2. Verify every version claim against official release notes (not just AI knowledge)
3. Pin versions explicitly: "This course targets Java 25 LTS" not "Java 21+"
4. Add version verification to CI: check that all version claims match a single source-of-truth file

**Detection:** Grep for version numbers across all content files. Compare against a single "target versions" document.

**Phase mapping:** Content audit phase. Version normalization should happen immediately after schema normalization (CRITICAL-3).

---

### MODERATE-4: Local LLM Tutor Gives Wrong Answers About Course Content

**What goes wrong:** The Phi-4 LLM tutor, when asked about concepts in the course, generates answers that contradict what the lesson teaches. Or worse, when helping students debug challenge code, the LLM suggests code that does not work with the execution environment. Research shows that without RAG augmentation, even GPT-4 answers only about one-third of university exam questions correctly in STEM domains. Phi-4 (14B parameters) will perform significantly worse.

**Why it happens:**
- Phi-4 is a general-purpose SLM with no knowledge of Code Tutor's specific curriculum
- Its training data may teach different patterns than the course (e.g., Phi-4 might suggest `System.out.println` while the course uses `IO.println`)
- Small models (14B) have limited parametric knowledge and are more prone to hallucination than frontier models
- The LLM cannot see the student's execution environment constraints

**Consequences:**
- Student gets contradictory advice: lesson says X, tutor says Y
- Student code works according to the tutor but fails the challenge validator
- Students lose trust in both the course AND the tutor
- Over-reliance on the tutor prevents independent debugging skill development

**Warning signs:**
- Tutor suggests `System.out.println` when the course teaches `IO.println`
- Tutor suggests packages or imports not used in the course
- Tutor gives correct-but-irrelevant explanations (answering a different language's patterns)
- Tutor cannot explain course-specific concepts (e.g., why compact source files exist)

**Prevention:**
1. Implement RAG: feed the current lesson's content to Phi-4 as context before responding
2. Constrain the tutor's language scope: if the student is in the Java course, inject "You are helping with Java. The course uses JDK 25 compact source files with IO.println()."
3. Add disclaimer: "The AI tutor may make mistakes. Always verify against lesson content."
4. Test the tutor against every challenge: ask it to solve each challenge and verify its answers
5. Consider progressive hints (already in challenge.json) as the PRIMARY help, with LLM as secondary

**Detection:** Create a test suite: for each challenge, ask the tutor to help with common mistakes. Verify the tutor's advice produces code that passes the challenge validator.

**Phase mapping:** LLM tutor enhancement phase -- must happen AFTER content is stabilized (or the RAG context would be feeding the LLM outdated content).

**Code Tutor specifics:**
- Phi-4 excels at math/STEM reasoning but its training data is 92% English and may not cover cutting-edge language features
- Without RAG, Phi-4 will almost certainly suggest `System.out.println` for Java beginners, contradicting JEP 512-based content
- The existing system prompt and context management for Phi-4 should be reviewed and enhanced

---

### MODERATE-5: Redundant Introductory Content Across Zero-Knowledge Courses

**What goes wrong:** A student who starts with Python, then moves to Java, has to re-learn "What is Programming?" and "What are Variables?" in a completely different style. The concepts are the same but the explanations are not. This is either redundant (boring for returning learners) or contradictory (different analogies for the same concept create confusion).

**Evidence in Code Tutor:** At least 4 of 6 courses start with some form of "What is Programming" lesson:
- Python: "What is Programming, Really?"
- Java: "What is a Computer Program?"
- JavaScript: "What Is Programming? (The Recipe Analogy)"
- C#: "What is Programming?"
- Kotlin: "Introduction to Kotlin & Development Setup" (combined)

Each was generated independently, likely with different analogies, depth, and approach.

**Why it happens:** Each AI session was told "this course starts from zero." The AI dutifully generates beginner content each time, with no awareness that 5 other courses already cover the same ground.

**Consequences:**
- Returning learners skip content and may miss language-specific setup
- Contradictory analogies (recipe vs blueprint vs instructions) create confusion
- 20-30% of total content volume is duplicated across courses
- Maintaining accuracy of the "same" concept in 6 places is error-prone

**Prevention:**
1. Decide: are these standalone courses (keep intros) or a platform curriculum (share intros)?
2. If standalone: ensure all intros are high quality and consistent in analogies
3. If platform: create a shared "Programming Fundamentals" track and have each language course start at "Setting up X"
4. At minimum: ensure analogies do not contradict each other across courses

**Detection:** Read the first 2-3 lessons of each course side by side. Note conceptual overlaps and contradictions.

**Phase mapping:** Content audit phase (assessment), then a design decision about course structure.

---

### MODERATE-6: Estimated Times Are Fiction

**What goes wrong:** The `estimatedMinutes` and `estimatedHours` values in course metadata are AI-generated guesses, not calibrated measurements. Students use these to plan their learning and are consistently misled.

**Evidence in Code Tutor:**
- C# claims 29 hours for 24 modules covering basics through CI/CD with GitHub Actions, Clean Architecture, and .NET Aspire. This is approximately 72 minutes per module for topics that take professionals weeks to learn.
- Kotlin claims 94 hours for 15 modules (6.3 hours per module average) -- more reasonable but still uncalibrated.
- Flutter claims 150 hours for 18 modules (8.3 hours per module) -- the widest course.
- Within courses, lesson estimates vary inconsistently: Python basics at 10 min, Kotlin setup at 45 min, JavaScript intro at 25 min -- all for roughly equivalent "intro" lessons.

**Why it happens:** AI cannot estimate learning time. It estimates reading time for the content it generated, not the time to understand, practice, and complete challenges.

**Consequences:**
- Students feel behind schedule (damaging motivation)
- Students rush through content to "stay on time"
- Course completion statistics are meaningless
- Planning and scheduling features based on these estimates are unreliable

**Prevention:**
1. Remove or de-emphasize time estimates until they can be calibrated with real user data
2. If keeping estimates, use a consistent formula: (content_word_count / reading_speed) + (challenge_count * avg_challenge_time) + buffer
3. Differentiate reading time from practice time
4. Mark all estimates as "approximate" in the UI

**Detection:** Calculate content volume (words + challenges) per lesson and compare against estimated minutes. Flag outliers.

**Phase mapping:** Content normalization phase. Apply a consistent formula after content is finalized.

---

## Minor Pitfalls

Mistakes that cause annoyance or minor confusion but are fixable without restructuring.

---

### MINOR-1: Inconsistent Code Style Within a Single Language Course

**What goes wrong:** Different lessons within the same course use different code styles: different naming conventions, different formatting, different import patterns. This confuses students about what "correct" code looks like.

**Evidence in Code Tutor:**
- Java alternates between `IO.println` and `System.out.println` even in adjacent modules
- Some Java lessons include class declarations; others use compact source file format
- Common mistakes in challenge.json sometimes do not match the lesson content (e.g., Hello World challenge lists "Using = instead of == for comparison" as a common mistake, which is irrelevant to Hello World)

**Prevention:**
1. Define a code style guide per language before auditing
2. Use linters/formatters on all code examples (Prettier, google-java-format, black, etc.)
3. Ensure common mistakes are relevant to the specific challenge, not copy-pasted from a template

**Phase mapping:** Content audit phase -- fix alongside other content corrections.

---

### MINOR-2: Backup Files and Content Artifacts in Repository

**What goes wrong:** Six `.json.bak` files exist in the content directories, one per course. These are artifacts from previous migration/repair operations. They clutter the repository, confuse version control, and may be accidentally loaded by the application.

**Evidence in Code Tutor:** Confirmed: `course.json.bak` exists for all 6 courses. Some are very large (the Java backup likely contains the entire pre-migration course structure).

**Prevention:**
1. Delete all `.bak` files and add `*.bak` to `.gitignore`
2. Use git history for version tracking, not backup files
3. Add a pre-commit hook to reject `.bak` files

**Phase mapping:** Repository cleanup -- can be done immediately, no content impact.

---

### MINOR-3: Folder Names Do Not Match Content

**What goes wrong:** Directory names reference one version or topic but the content inside has been updated to reference a different version.

**Evidence in Code Tutor:**
- Java: Folder `06-lesson-16-modern-java-syntax-writing-less-code-java-23` but content references Java 25 (JEP 512 finalized in JDK 25, September 2025)
- Flutter: Module folders use generic names like `01-module-0-flutter-development` through `05-module-4-flutter-development` -- meaningless to developers navigating the file system

**Prevention:**
1. Rename directories to match current content during the normalization pass
2. Use semantic directory names that describe the content, not just module numbers
3. If renaming breaks references, update all cross-references atomically

**Phase mapping:** Content normalization phase -- do alongside schema normalization.

---

### MINOR-4: Generic Common Mistakes in Challenges

**What goes wrong:** Challenge files include `commonMistakes` arrays that are clearly template-generated rather than specific to the challenge. The same three mistakes appear across many challenges regardless of relevance.

**Evidence in Code Tutor:**
- The "Your First Hello World" Java challenge lists "Using = instead of == for comparison" as a common mistake. There is no comparison in a Hello World program.
- This pattern suggests the AI was given a template of common Java mistakes and included them regardless of context.

**Prevention:**
1. Review each challenge's `commonMistakes` for relevance to that specific challenge
2. Remove irrelevant mistakes; add context-specific ones
3. Ensure mistakes match the skills being tested in that challenge

**Phase mapping:** Content audit phase -- low priority but easy to fix during other edits.

---

## Phase-Specific Warnings

| Phase Topic | Likely Pitfall | Mitigation | Severity |
|-------------|---------------|------------|----------|
| Schema normalization | Changing IDs breaks app navigation state or user progress tracking | Check app code for ID-dependent logic before changing any IDs | HIGH |
| Version pinning | Targeting latest versions (JDK 25, Python 3.14) limits accessibility | Target LTS/stable versions; document exact requirements | HIGH |
| Java content audit | Choosing IO.println vs System.out.println affects 75+ files | Make the decision ONCE, document it, apply globally | HIGH |
| Challenge validation | Running solutions requires specific runtime versions installed on CI | Set up CI with exact runtime versions matching course targets | MEDIUM |
| LLM tutor RAG setup | RAG on unstable content means reindexing after every content change | Implement RAG AFTER content is finalized, not during audit | HIGH |
| Cross-course style | Auditing courses in parallel with different "reviewers" (human or AI) | Write style guide first; calibrate on one lesson per course | MEDIUM |
| Estimated times | Fixing estimates before content is final means re-fixing them later | Apply time estimates as the LAST step of content audit | LOW |
| Flutter backend content | Dart Frog transitioned to community-led; may have API changes | Verify all Dart Frog code against current `dart_frog` package version | MEDIUM |
| JavaScript stack | Bun/Hono are fast-moving; APIs change frequently | Pin exact Bun version in course requirements; verify all APIs | MEDIUM |
| Capstone projects | Not all courses have capstones (check each course) | Verify capstone existence; create where missing AFTER content audit | LOW |

---

## The Recommended Audit Order (Based on Pitfalls)

Based on the pitfalls above, the content audit should proceed in this specific order to avoid cascading problems:

1. **Schema normalization** (CRITICAL-3) -- Standardize the container before fixing contents
2. **Version pinning** (MODERATE-3) -- Decide target versions before auditing code examples
3. **API verification** (CRITICAL-1) -- Verify all code examples compile against target versions
4. **Knowledge cliff assessment** (CRITICAL-2) -- Map concept progression, identify gaps
5. **Content quality pass** (MODERATE-1) -- Fix explanations, tone, accuracy WITH style guide
6. **Challenge validation** (CRITICAL-4) -- Run every solution through the execution engine
7. **Cross-course consistency** (CRITICAL-3 follow-up) -- Verify unified voice across courses
8. **Time estimate calibration** (MODERATE-6) -- Apply formula AFTER content is final
9. **LLM tutor enhancement** (MODERATE-4) -- Implement RAG with finalized content

Doing these out of order creates cascading rework. For example, fixing content before normalizing schemas means re-fixing format issues. Implementing RAG before finalizing content means reindexing everything.

---

## Sources

**AI-Generated Code Quality:**
- [State of AI Code Quality 2025 - Qodo](https://www.qodo.ai/reports/state-of-ai-code-quality/)
- [Survey of Bugs in AI-Generated Code - arXiv](https://arxiv.org/html/2512.05239v1)
- [Hallucinations in Code - Simon Willison](https://simonwillison.net/2025/Mar/2/hallucinations-in-code/)
- [AI vs Human Code Generation Report - CodeRabbit](https://www.coderabbit.ai/blog/state-of-ai-vs-human-code-generation-report)
- [AI Code Quality Crisis 2025 - ByteIota](https://byteiota.com/ai-code-quality-crisis-2025-bugs-up-41-trust-down-67/)

**LLM Tutoring Failure Modes:**
- [Problems With LLMs for Learner Modelling - arXiv](https://arxiv.org/abs/2512.23036)
- [Small Models, Big Support: Local LLM Framework - arXiv](https://arxiv.org/html/2506.05925v1)
- [LLMs in Education: Systematic Review - ScienceDirect](https://www.sciencedirect.com/science/article/pii/S2666920X25001699)
- [Phi-4 Model Card - Hugging Face](https://huggingface.co/microsoft/phi-4)

**Java JEP 512 Verification:**
- [JEP 512: Compact Source Files - OpenJDK](https://openjdk.org/jeps/512)
- [Instance Main Methods Finalized in JDK 25 - InfoQ](https://www.infoq.com/news/2025/05/jdk25-instance-main-methods/)
- [Java 25 Released - InfoQ](https://www.infoq.com/news/2025/09/java25-released/)

**Dart Frog Status:**
- [Dart Frog Has Found a New Pond - Very Good Ventures](https://www.verygood.ventures/blog/dart-frog-has-found-a-new-pond)

**Content Audit Best Practices:**
- [Content Inventory and Auditing 101 - NN/g](https://www.nngroup.com/articles/content-audits/)
- [Content Governance in Large Organizations](https://www.enterprisecms.org/guides/content-governance-in-large-organizations)
- [Content Audit Pitfalls - LinkedIn Advice](https://www.linkedin.com/advice/1/what-common-content-audit-pitfalls-how-avoid)

**Curriculum Design:**
- [Coding and Computational Thinking Across Curriculum - SAGE](https://journals.sagepub.com/doi/10.3102/00346543241241327)
- [Understanding the Beginner's Coding Struggle - Medium](https://tomtalksit.medium.com/understanding-the-beginners-coding-struggle-why-learning-to-code-feels-overwhelming-in-2025-5675e5eea18f)

---

*Pitfalls research: 2026-02-02*
