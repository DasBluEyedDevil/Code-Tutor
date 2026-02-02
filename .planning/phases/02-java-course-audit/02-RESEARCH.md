# Phase 2: Java Course Audit - Research

**Researched:** 2026-02-02
**Domain:** Java 25 LTS course content, Spring Boot full-stack development, course pedagogy
**Confidence:** HIGH (core Java 25 features verified via OpenJDK JEPs and multiple authoritative sources)

## Summary

The Java course audit requires updating 96 lessons (678 content files, 182 challenges) across 16 modules to target Java 25 LTS (released September 2025). The course currently has mixed references -- some files already use `IO.println` and compact source file syntax, while 226 occurrences of `System.out.println` remain across 97 files. The version manifest still says Java 21.

Three Java 25 JEPs are directly relevant to the course content: JEP 512 (Compact Source Files and Instance Main Methods), JEP 511 (Module Import Declarations), and JEP 513 (Flexible Constructor Bodies). All three are finalized (no `--enable-preview` flag needed).

A critical structural issue exists: the CONTEXT.md decision calls for "Spring Boot backend + simple frontend (Thymeleaf or similar)" for the capstone, but the current course has 3 full modules dedicated to React (modules 13, 15, 16) and the capstone is entirely built on React + Spring Boot. This must be resolved before planning begins.

**Primary recommendation:** Execute the audit in four sub-phases as the roadmap specifies (structural review, accuracy pass, challenge validation, voice/polish), treating the React-vs-Thymeleaf capstone decision and the Spring Boot version (3.5.x vs 4.0.x) as blocking decisions to resolve first.

## Standard Stack

The established technologies for this course domain:

### Core Runtime
| Technology | Version | Purpose | Why Standard |
|------------|---------|---------|--------------|
| Java (OpenJDK) | 25 LTS | Runtime and language | Latest LTS, released Sep 2025, 8-year support from Oracle |
| `java.lang.IO` | JDK 25 | Console I/O | JEP 512 finalized; replaces `System.out.println` pattern |
| Compact Source Files | JEP 512 | Beginner entry point | No class boilerplate for simple programs |
| Module Import Declarations | JEP 511 | Simplified imports | `import module java.base;` replaces individual imports |
| Flexible Constructor Bodies | JEP 513 | OOP teaching | Statements before `super()` now legal |

### Framework Stack
| Technology | Version | Purpose | Notes |
|------------|---------|---------|-------|
| Spring Boot | 3.5.x or 4.0.x | Web framework | 3.5.5+ confirmed Java 25 ready; 4.0.x has first-class Java 25 support |
| Spring Framework | 6.2.x or 7.0.x | Core framework | Matches Spring Boot version choice |
| JUnit | 5.x | Testing | Unchanged |
| JPA/Hibernate | 6.x | ORM | Ships with Spring Boot 3.x/4.x |
| Gradle | 8.x | Build tool | Already in manifest |

### Spring Boot Version Decision (OPEN -- must resolve)
| Option | Pros | Cons |
|--------|------|------|
| Spring Boot 3.5.x | Smaller migration, content mostly correct already | OSS support ends June 2026, references Spring Boot 3.4 which EOL'd Dec 2025 |
| Spring Boot 4.0.x | Latest, virtual threads default, Jakarta EE 11, 2027+ support | Significant content rewrite (Spring Framework 7, Jackson 3.x, JUnit 6 changes) |

**Recommendation:** Use **Spring Boot 4.0.x** -- it aligns with the Java 25 LTS target, has long-term support, and the course already references some Spring Boot 4.0 features (virtual threads enabled by default). The content will need updating anyway.

### Capstone Frontend Decision (OPEN -- must resolve)
| Option | Pros | Cons |
|--------|------|------|
| Keep React (current) | No structural rewrite, modules 13/15/16 remain | Requires teaching React in a Java course (3 modules), CONTEXT.md says "Thymeleaf or similar" |
| Switch to Thymeleaf | Pure Java stack, simpler, matches CONTEXT.md decision | Requires rewriting modules 13, 15, 16 entirely -- massive scope increase |
| Hybrid approach | Keep React modules as "bonus", add Thymeleaf capstone | Inconsistent, two frontend approaches |

**Recommendation:** This is a **user decision** that was partially made in CONTEXT.md ("Thymeleaf or similar") but conflicts with the existing 3-module React curriculum. The planner must treat this as a blocking question. The current course has modules 13 (React Frontend Integration, 6 lessons), 15 (Full Stack Development, 7 lessons), and 16 (Capstone, 9 lessons) all built on React.

## Architecture Patterns

### Java 25 Source File Progression (Course Teaching Order)
```
Modules 01-02: Compact source files
  void main() {
      IO.println("Hello!");
  }

Module 04 (OOP transition): Full class declaration introduced
  public class Student {
      public static void main(String[] args) { ... }
  }

Modules 05+: Full class syntax as default
  (compact syntax only for quick examples)
```

### Content File Structure (Per Lesson)
```
lesson-XX/
  lesson.json           # Metadata
  content/
    01-theory.md        # THEORY sections
    02-key_point.md     # KEY_POINT sections
    03-example.md       # EXAMPLE sections
    04-warning.md       # WARNING sections
    05-analogy.md       # ANALOGY sections (MISSING in 95/96 lessons!)
    06-legacy.md        # LEGACY_COMPARISON (MISSING entirely)
  challenges/
    01-challenge-name/
      challenge.json    # Test cases, hints, metadata
      solution.java     # Reference solution
      starter.java      # Starting code (optional)
```

### Current Content Type Distribution (678 files)
| Type | Count | Percentage | Status |
|------|-------|------------|--------|
| THEORY | 102 | 50.7% | Present in all lessons |
| KEY_POINT | 67 | 33.3% | Present in most lessons |
| WARNING | 18 | 9.0% | Present in ~60% of lessons |
| EXAMPLE | 12 | 6.0% | Sparse |
| ANALOGY | 1 | 0.5% | **Only 1 lesson has ANALOGY** |
| LEGACY_COMPARISON | 0 | 0.0% | **Zero instances** |

### Code Execution Architecture (WPF App)
The WPF app executes Java challenges via `StartJavaSessionAsync()`:
1. Regex extracts class name from `public class (\w+)`
2. Falls back to `className = "Main"` if no match
3. Saves code to `{className}.java` in temp directory
4. Compiles with `javac "{javaFile}"`
5. Runs with `java -cp "{tempDir}" {className}`

**Critical issue for compact source files:** The execution service expects either:
- A class declaration to extract the name from, OR
- A fallback to "Main" which creates `Main.java`

For JDK 25 compact source files, `javac Main.java` will compile `void main() { ... }` into a `Main.class` (named after the filename), and `java -cp dir Main` will run it. This should work, BUT the implicit class name must match the filename. The current approach of defaulting to "Main" and saving as "Main.java" is compatible with compact source files.

**HOWEVER:** The `java` source-file mode (`java HelloWorld.java` -- runs directly without separate compile step) would be simpler for compact source files. This is a potential app enhancement but out of scope for this content audit.

### Anti-Patterns to Avoid
- **Mixed IO patterns in same lesson:** Never use both `System.out.println` and `IO.println` in the same lesson (except the one legacy-mention lesson)
- **Assuming --enable-preview:** JEP 512/511/513 are finalized in Java 25 -- no preview flags needed
- **Teaching compact syntax late:** Compact source files should be the FIRST thing students see (Module 01, Lesson 01)

## Don't Hand-Roll

Problems that have existing solutions -- do not create custom approaches:

| Problem | Don't Build | Use Instead | Why |
|---------|-------------|-------------|-----|
| Console I/O | Custom Scanner wrappers | `IO.println()`, `IO.readln()` | JEP 512 provides these; `java.lang.IO` replaces need for Scanner for basic input |
| Boilerplate reduction | Comments explaining why boilerplate exists | Compact source files (JEP 512) | Students should learn the clean way first |
| Module imports | Long import lists | `import module java.base;` (JEP 511) | Available in all source files, not just compact |
| Constructor validation | Helper methods before super() | Flexible constructor bodies (JEP 513) | Can now write statements before `super()` legally |
| Spring Boot project setup | Manual configuration | Spring Initializr (start.spring.io) | Generate project with correct dependencies |
| Deployment configuration | Manual Docker + cloud setup | Railway one-click deploy from GitHub | Simplest path for beginners |

**Key insight:** Java 25 eliminates several "ceremony" problems that older Java courses work around with comments and explanations. The audit should strip these workarounds out and teach the clean Java 25 way, with a single LEGACY_COMPARISON section noting the old approach.

## Common Pitfalls

### Pitfall 1: Inconsistent IO Pattern Across Files
**What goes wrong:** Some files use `IO.println`, some use `System.out.println`, some use both
**Why it happens:** Partial migration; course was likely written for Java 21 and partially updated
**How to avoid:** Global search-and-replace with manual review of each file; the one-time legacy mention should use LEGACY_COMPARISON content type
**Current state:** 226 occurrences of `System.out.println` in 97 files; 104 occurrences of `IO.println` in 75 files. Many files have BOTH.
**Warning signs:** `grep -c "System.out.println" + grep -c "IO.println"` on same file

### Pitfall 2: Java Version References Scattered Throughout
**What goes wrong:** Content says "Java 21" or "Java 23" when it should say "Java 25"
**Why it happens:** Course written incrementally across Java versions
**How to avoid:** Search for all version references: "Java 21", "Java 23", "Java 24", "JDK 21", "JDK 23", "JDK 24"
**Current state:** Found 40+ references to older Java versions across content and challenge files
**Warning signs:** Challenge starters with comments like "// Using Java 23 implicit main syntax"

### Pitfall 3: Compact Source File Compilation in WPF App
**What goes wrong:** The code execution service extracts class names via regex for `public class (\w+)` -- compact source files have no class declaration
**Why it happens:** Execution service was designed for traditional Java files
**How to avoid:** Verify that the fallback to "Main" filename works with JDK 25 compact source files (it should -- `javac Main.java` will create `Main.class` for compact files). Test explicitly.
**Warning signs:** Any challenge using compact source file syntax should be tested against the execution service

### Pitfall 4: Missing ANALOGY Sections
**What goes wrong:** Only 1 out of 96 lessons has an ANALOGY content section
**Why it happens:** Original course generation didn't include analogies consistently
**How to avoid:** CONTEXT.md mandates "every lesson gets an ANALOGY section" -- this is a massive content addition (95 new files)
**Warning signs:** Any lesson directory without an `*analogy*` file in its content folder

### Pitfall 5: Spring Boot Version Mismatch
**What goes wrong:** Content references Spring Boot 3.4 (EOL December 2025) while some content already references Spring Boot 4.0
**Why it happens:** Content was written across different time periods
**How to avoid:** Pick ONE Spring Boot version and update all references consistently
**Current state:** Module descriptions reference "Spring Boot 3.4+", some lesson content references Spring Boot 4.0 features (virtual threads default, @MockitoBean)

### Pitfall 6: Capstone Architecture Mismatch with Decision
**What goes wrong:** CONTEXT.md says "Thymeleaf or similar" but capstone uses React + Spring Boot
**Why it happens:** The decision was made in the context discussion, but the existing course was built with React
**How to avoid:** Resolve this decision before planning begins -- it affects modules 13, 15, and 16 (22 lessons total)
**Warning signs:** Any plan that doesn't address this conflict

### Pitfall 7: `--enable-preview` Flag in Old Content
**What goes wrong:** Old content or instructions may reference `--enable-preview` for features now finalized
**Why it happens:** JEP 512 was in preview from JDK 21-24
**How to avoid:** Search for `enable-preview` across all content and remove
**Warning signs:** Any build configuration or command that includes `--enable-preview`

## Code Examples

Verified patterns from official sources:

### Compact Source File (JEP 512 -- Finalized in Java 25)
```java
// Source: https://openjdk.org/jeps/512
// No class declaration needed. File can be named anything.java
void main() {
    IO.println("Hello, World!");
}
```
- No `--enable-preview` flag needed
- Compiler creates implicit class named after the file
- `java.lang.IO` auto-available (in `java.lang` package)
- All `java.base` exports auto-imported in compact source files

### IO Class Methods (java.lang.IO)
```java
// Source: https://openjdk.org/jeps/512
// Available in ALL Java files (not just compact) -- it's in java.lang
IO.print("Enter name: ");       // print without newline
IO.println("Hello, World!");    // print with newline
IO.println();                   // blank line
String name = IO.readln("Name: "); // read with prompt
String line = IO.readln();      // read without prompt
```

### Module Import Declarations (JEP 511 -- Finalized in Java 25)
```java
// Source: https://openjdk.org/jeps/511
import module java.base;  // imports ALL packages exported by java.base

// Now List, Map, Stream, Collectors, etc. are all available
var names = List.of("Alice", "Bob", "Charlie");
var upper = names.stream()
    .map(String::toUpperCase)
    .collect(Collectors.toList());
```

### Flexible Constructor Bodies (JEP 513 -- Finalized in Java 25)
```java
// Source: https://openjdk.org/jeps/513
public class PositiveInteger {
    private final int value;

    public PositiveInteger(int value) {
        // Statements BEFORE super() -- now legal in Java 25!
        if (value <= 0) {
            throw new IllegalArgumentException("Must be positive: " + value);
        }
        super();  // can now appear after validation
        this.value = value;
    }
}
```

### Spring Boot 4.0 Hello World (with Java 25)
```java
// Source: https://docs.spring.io/spring-boot/tutorial/first-application/index.html
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

@SpringBootApplication
@RestController
public class TaskManagerApplication {

    public static void main(String[] args) {
        SpringApplication.run(TaskManagerApplication.class, args);
    }

    @GetMapping("/hello")
    String hello() {
        return "Hello, World!";
    }
}
```

### Railway Deployment (Spring Boot)
```dockerfile
# Source: https://docs.railway.com/guides/spring-boot
FROM eclipse-temurin:25-jdk-alpine AS build
WORKDIR /app
COPY . .
RUN ./gradlew bootJar

FROM eclipse-temurin:25-jre-alpine
WORKDIR /app
COPY --from=build /app/build/libs/*.jar app.jar
EXPOSE 8080
ENTRYPOINT ["java", "-jar", "app.jar"]
```
- Railway auto-detects Java/Gradle/Maven projects
- Set `PORT=8080` environment variable
- Generate public URL via Networking settings

## State of the Art

| Old Approach | Current Approach (Java 25) | When Changed | Impact on Course |
|--------------|----------------------------|--------------|------------------|
| `System.out.println()` | `IO.println()` | JDK 25 (JEP 512) | All 97 files with System.out.println must migrate |
| `public class Main { public static void main(String[] args) }` | `void main()` | JDK 25 (JEP 512) | Early modules use compact syntax, OOP modules keep full |
| `--enable-preview` for compact files | No flag needed | JDK 25 (finalized) | Remove any --enable-preview references |
| Individual imports | `import module java.base;` | JDK 25 (JEP 511) | Simplify import sections in later modules |
| Validation after super() | Validation before super() | JDK 25 (JEP 513) | Update constructor lessons in OOP module |
| Spring Boot 3.4 | Spring Boot 4.0.x (or 3.5.x) | Nov 2025 | Version references throughout modules 11-16 |
| `@MockBean` | `@MockitoBean` | Spring Boot 3.4/4.0 | Testing module content |
| `spring.threads.virtual.enabled=true` | Virtual threads default in SB 4.0 | Nov 2025 | Concurrency and Spring Boot modules |

**Deprecated/outdated items found in current content:**
- `System.out.println` -- replaced by `IO.println` (226 occurrences)
- "Java 21" version references in course.json and lesson content (40+ references)
- "Java 23 implicit main syntax" comments in challenge starters
- Spring Boot 3.4 references (EOL December 2025)
- `@MockBean` annotation (deprecated, replaced by `@MockitoBean`)
- Docker images using `eclipse-temurin:21-*` (should be `25-*`)

## Quantitative Audit Summary

### Scope Numbers
| Metric | Count |
|--------|-------|
| Total modules | 16 |
| Total lessons | 96 |
| Total content files (.md) | 678 |
| Total challenges | 182 |
| Files with `System.out.println` | 97 |
| Files with `IO.println` | 75 |
| Files with Java 21/23/24 version refs | 40+ |
| Lessons missing ANALOGY section | 95 of 96 |
| Lessons missing WARNING section | ~35 of 96 |
| LEGACY_COMPARISON sections | 0 |
| Spring Boot version references to update | 25+ |

### Module-by-Module Assessment

| Module | Lessons | Challenges | Key Issues |
|--------|---------|------------|------------|
| 01 Java Fundamentals | 6 | 11 | Mixed IO patterns; lesson 6 references "Java 23" in title |
| 02 Data Types/Loops/Methods | 6 | 13 | Challenge starters reference "Java 23 implicit main" |
| 03 Git/Dev Workflow | 4 | 15 | Low Java-specific issues; mostly language-agnostic |
| 04 OOP | 8 | 20 | System.out.println in class examples; need flexible constructor bodies |
| 05 Collections/FP | 7 | 17 | "Java 8" in lesson titles; System.out.println throughout |
| 06 Streams/FP | 5 | 10 | Heavy System.out.println usage |
| 07 Concurrency | 5 | 10 | "Java 21" in virtual threads lesson title; System.out.println |
| 08 Testing/Build | 6 | 8 | Need @MockitoBean update |
| 09 Databases/SQL | 7 | 7 | System.out.println in JDBC examples |
| 10 Web/APIs | 3 | 3 | Spring Boot 3.4 references |
| 11 Spring Boot | 7 | 7 | Spring Boot 3.4 throughout; need version update |
| 12 Security/JWT | 5 | 10 | Spring Security config may need SB 4.0 update |
| 13 React Frontend | 6 | 12 | **Entire module may need rewrite if Thymeleaf chosen** |
| 14 DevOps/Deployment | 5 | 10 | Docker images reference JDK 21; deployment configs outdated |
| 15 Full-Stack Dev | 7 | 9 | **React-based; affected by frontend decision** |
| 16 Capstone | 9 | 20 | **React + Spring Boot; affected by frontend decision** |

## Open Questions

Things that could not be fully resolved and require user decisions:

1. **Capstone Frontend: Thymeleaf vs React**
   - What we know: CONTEXT.md says "Thymeleaf or similar". Current course has 22 lessons across 3 modules built on React.
   - What's unclear: Does the user want to rewrite modules 13/15/16 for Thymeleaf, or revise the CONTEXT.md decision to keep React?
   - Recommendation: Ask the user. This is the single largest scope question -- switching to Thymeleaf would add ~22 lessons of rewriting.
   - **Impact if Thymeleaf:** Modules 13, 15, 16 completely rewritten (22 lessons, 41 challenges)
   - **Impact if React kept:** Only version/API updates needed for React content

2. **Spring Boot Version: 3.5.x vs 4.0.x**
   - What we know: 3.5.x supports Java 25. 4.0.x has first-class Java 25 support with Spring Framework 7.
   - What's unclear: Does the user want the safer incremental path (3.5.x) or the future-proof path (4.0.x)?
   - Recommendation: Spring Boot 4.0.x -- the course already references some 4.0 features, and 3.5.x OSS support ends June 2026. Content will need updating regardless.
   - **Impact if 3.5.x:** Smaller changes to Spring Boot modules
   - **Impact if 4.0.x:** Jakarta EE 11 changes, Jackson 3.x, some API differences

3. **ANALOGY Section Scope**
   - What we know: CONTEXT.md mandates "every lesson gets an ANALOGY section". Currently 95/96 lessons are missing analogies.
   - What's unclear: Should all 95 analogies be written during this audit phase, or is this "thin but correct content" that gets flagged only?
   - Recommendation: The CONTEXT.md decision says "every lesson gets an ANALOGY section" -- this means writing 95 new ANALOGY sections is in scope.

4. **Code Execution Service Compatibility**
   - What we know: The WPF app's `StartJavaSessionAsync` defaults to "Main" class name for compact source files. This should work with JDK 25 (compact file `Main.java` compiles to `Main.class`).
   - What's unclear: Whether JDK 25 is installed on the development machine, and whether the execution service has been tested with compact source files.
   - Recommendation: Flag for verification during challenge validation sub-phase. App changes are out of scope for this content audit.

## Sources

### Primary (HIGH confidence)
- [JEP 512: Compact Source Files and Instance Main Methods](https://openjdk.org/jeps/512) -- finalized feature specification
- [JEP 511: Module Import Declarations](https://openjdk.org/jeps/511) -- finalized feature specification
- [JEP 513: Flexible Constructor Bodies](https://openjdk.org/jeps/513) -- finalized feature specification
- [JDK 25 Official Page](https://openjdk.org/projects/jdk/25/) -- release info, 18 JEPs listed
- [Oracle Java 25 Release Notes](https://www.oracle.com/java/technologies/javase/25-relnotes.html)
- [Java 25 Language Spec: Compact Source Files](https://docs.oracle.com/en/java/javase/25/language/compact-source-files-and-instance-main-methods.html)
- [Railway Spring Boot Deployment Guide](https://docs.railway.com/guides/spring-boot) -- official deployment docs
- [Spring Boot 4.0.0 Release](https://spring.io/blog/2025/11/20/spring-boot-4-0-0-available-now/) -- official release announcement

### Secondary (MEDIUM confidence)
- [Baeldung: Java 25 Features](https://www.baeldung.com/java-25-features) -- well-known Java resource, cross-verified with OpenJDK
- [JetBrains: Java 25 LTS and IntelliJ IDEA](https://blog.jetbrains.com/idea/2025/09/java-25-lts-and-intellij-idea/) -- IDE vendor documentation
- [InfoQ: Java 25 Released](https://www.infoq.com/news/2025/09/java25-released/) -- tech news, cross-verified
- [Spring Boot GitHub Issue #47245: Document Java 25 Support](https://github.com/spring-projects/spring-boot/issues/47245)
- [Spring Boot System Requirements](https://docs.spring.io/spring-boot/system-requirements.html)

### Tertiary (LOW confidence)
- Various Medium articles on Java 25 features (used for cross-reference only)
- Community discussions on Railway deployment patterns

## Metadata

**Confidence breakdown:**
- Java 25 features (JEP 512/511/513): **HIGH** -- verified via official OpenJDK JEP specs
- IO.println API: **HIGH** -- verified via JEP 512 and official Java 25 language docs
- Spring Boot version compatibility: **HIGH** -- verified via spring.io blog and GitHub issues
- Compact source file compilation behavior: **HIGH** -- verified via official Oracle docs
- Railway deployment: **MEDIUM** -- verified via official Railway docs, but pricing may change
- WPF execution service compatibility: **MEDIUM** -- code analysis shows it should work, but untested
- ANALOGY section scope: **HIGH** -- CONTEXT.md is explicit

**Research date:** 2026-02-02
**Valid until:** 2026-04-02 (60 days -- Java 25 and Spring Boot 4.0 are both LTS/stable)
