---
phase: 02-java-course-audit
plan: 01
subsystem: course-metadata
tags: [java, versioning, metadata, structural-review]
dependency-graph:
  requires: [01-05]
  provides: [version-foundation, structural-assessment]
  affects: [02-02, 02-03, 02-04, 02-05, 02-06, 02-07, 02-08]
tech-stack:
  added: [Jackson 3.x]
  patterns: [version-manifest-driven-audit]
key-files:
  created: []
  modified:
    - content/version-manifest.json
    - content/courses/java/course.json
    - content/courses/java/modules/07-concurrency-virtual-threads/module.json
    - content/courses/java/modules/11-spring-boot/module.json
    - content/courses/java/modules/01-java-fundamentals/lessons/05-lesson-15-switch-expressions-pattern-matching/content/07-key_point.md
    - content/courses/java/modules/15-full-stack-development/lessons/07-lesson-157-virtual-threads-project-loom-modern-concurrency/content/07-theory.md
  renamed:
    - from: 06-lesson-16-modern-java-syntax-writing-less-code-java-23
      to: 06-lesson-16-modern-java-syntax-writing-less-code
    - from: 06-lesson-56-lambda-expressions-functions-as-data-java-8
      to: 06-lesson-56-lambda-expressions-functions-as-data
    - from: 07-lesson-57-streams-functional-collection-processing-java-8
      to: 07-lesson-57-streams-functional-collection-processing
    - from: 05-lesson-c5-virtual-threads-java-21
      to: 05-lesson-c5-virtual-threads
decisions:
  - id: D-0201-01
    summary: "Java 25 LTS as course target (not Java 24 or Java 21)"
    rationale: "Java 25 is the next LTS after 21; course should target current LTS"
  - id: D-0201-02
    summary: "Spring Boot 4.0.x as framework target"
    rationale: "Spring Boot 4.0 ships with Spring Framework 7, Jakarta EE 11, and first-class Java 25 support"
  - id: D-0201-03
    summary: "Remove all version numbers from lesson titles and directory names"
    rationale: "Version numbers in paths create rename churn; content body can reference versions where pedagogically relevant"
  - id: D-0201-04
    summary: "Keep primitive type patterns content but mark as preview without --enable-preview flag"
    rationale: "Students should know about preview features but not be told to use preview compiler flags"
metrics:
  duration: "5 min"
  completed: "2026-02-02"
---

# Phase 02 Plan 01: Version Targets and Structural Review Summary

**One-liner:** Java 25 LTS + Spring Boot 4.0.x version targets set, 4 lesson directories renamed to remove embedded version numbers, all --enable-preview references removed, 16-module structural review completed.

## What Was Done

### Task 1: Version Targets and Course Metadata

- **version-manifest.json**: Java runtime updated from 21 to 25 (LTS). Spring Boot updated from 3.4.x to 4.0.x with notes about Spring Framework 7, Jakarta EE 11, and virtual threads default. JPA/Hibernate notes updated. Jackson 3.x added as new framework entry.
- **course.json**: minimumRuntimeVersion changed to "Java 25". Description updated to reference Spring Boot 4.0+ and Thymeleaf. Learning outcomes updated to Java 25+, Spring Boot 4.0+, Thymeleaf + React. New outcome added: "Deploy a full-stack application (Spring Boot + Thymeleaf/React) to the cloud".

### Task 2: Version References, enable-preview, and Structural Review

**Directory renames (4):**
| Old Name | New Name | Module |
|----------|----------|--------|
| `06-lesson-16-modern-java-syntax-writing-less-code-java-23` | `06-lesson-16-modern-java-syntax-writing-less-code` | 01 |
| `06-lesson-56-lambda-expressions-functions-as-data-java-8` | `06-lesson-56-lambda-expressions-functions-as-data` | 05 |
| `07-lesson-57-streams-functional-collection-processing-java-8` | `07-lesson-57-streams-functional-collection-processing` | 05 |
| `05-lesson-c5-virtual-threads-java-21` | `05-lesson-c5-virtual-threads` | 07 |

**lesson.json title updates (4):** Removed "(Java 25+)", "(Java 8+)", "(Java 21)" from lesson titles.

**module.json updates (2):**
- Module 07: Removed "Java 21's" from description
- Module 11: Changed "Spring Boot 3.4+" to "Spring Boot 4.0+"

**--enable-preview removal (2 files):**
- `modules/01/.../07-key_point.md`: Removed flag reference, kept preview feature note
- `modules/15/.../07-theory.md`: Replaced "Java 21-22...stable in Java 23" with version-neutral language, removed --enable-preview reference

## Structural Review Findings

### Module Ordering Assessment

The 16-module curriculum follows this progression:

| # | Module | Difficulty | Lessons | Assessment |
|---|--------|-----------|---------|------------|
| 01 | Java Fundamentals | beginner | 6 | Good foundation |
| 02 | Data Types, Loops, Methods | beginner | 6 | Natural progression |
| 03 | Git & Development Workflow | beginner | 4 | Well-placed break from Java syntax |
| 04 | Object-Oriented Programming | beginner | 8 | Correct after fundamentals |
| 05 | Collections & Functional Programming | beginner | 7 | Appropriate after OOP |
| 06 | Streams & Functional Programming | intermediate | 5 | **ISSUE: Heavy overlap with Module 05** |
| 07 | Concurrency & Virtual Threads | advanced | 5 | Large difficulty jump from Module 06 |
| 08 | Testing & Build Tools | beginner | 6 | **ISSUE: Difficulty regression** |
| 09 | Databases & SQL | beginner | 7 | Correct prerequisite for Spring |
| 10 | Web Fundamentals & APIs | beginner | 3 | Good intro before Spring |
| 11 | Spring Boot | beginner | 7 | Correct placement |
| 12 | Security: Sessions & JWT | advanced | 5 | Depends on Spring Boot |
| 13 | React Frontend Integration | intermediate | 6 | **ISSUE: Assumes React knowledge from scratch in a Java course** |
| 14 | DevOps & Deployment | intermediate | 5 | Correct after Spring Boot |
| 15 | Full-Stack Development | beginner | 7 | **ISSUE: Difficulty marked beginner but content is advanced; overlaps M13+M14** |
| 16 | Capstone Project | advanced | 9 | Appropriate capstone |

### Prerequisite Chain Violations Found

1. **Module 05 vs Module 06 overlap**: Module 05 "Collections and Functional Programming" already teaches lambdas (lesson 5.6) and streams (lesson 5.7). Module 06 "Streams & Functional Programming" teaches lambdas (lesson S.1) and streams (lesson S.2) again. This is a clear content duplication. Recommendation: Module 06 should be an ADVANCED streams module (parallel streams, custom collectors, advanced reduce patterns) rather than re-teaching basics.

2. **Module 08 difficulty regression**: Testing & Build Tools is marked "beginner" but appears after the advanced concurrency module. The difficulty metadata should be "intermediate" since students are expected to write tests for the code patterns they just learned. The placement itself is defensible (testing early is valuable) but the beginner tag is misleading.

3. **Module 15 difficulty mismatch**: Full-Stack Development is marked "beginner" but lesson content includes virtual threads/Project Loom (lesson 15.7), REST API design standards, and complete database-to-UI feature development. Should be "intermediate" or "advanced".

4. **Module 15 lesson 7 is misplaced**: "Virtual Threads & Project Loom - Modern Concurrency" belongs in Module 07 (Concurrency), not Module 15 (Full-Stack Development). This is a structural duplication with Module 07.

### Missing Bridge Lessons

1. **No error handling/exceptions lesson**: The curriculum jumps from basic OOP to collections without covering Java exception handling (try/catch/finally, custom exceptions, checked vs unchecked). This is taught implicitly in later modules but never has its own lesson. Students will encounter exceptions in Module 05+ without preparation.

2. **No String manipulation lesson**: Strings are used throughout but never formally taught (StringBuilder, String methods, regex basics, text blocks).

3. **No file I/O lesson**: Reading/writing files is a fundamental skill missing from the curriculum. This would naturally fit between Module 02 and Module 04.

### Module 13 (React) Assessment

Module 13 teaches React from scratch (frontend fundamentals, React introduction, state/events, fetching data, React Router, connecting to Spring Boot). This is a 6-lesson mini-course within a Java course. Two concerns:

- **Scope creep**: Teaching React fundamentals in a Java course dilutes the Java focus
- **Resolution per plan**: Plan 02-07 adds Thymeleaf as the primary server-rendered frontend, making React optional. This is the correct approach -- Thymeleaf is Java-native and reduces the cognitive load.

### Thymeleaf Prerequisite Resolution

No separate Thymeleaf module needed. Plan 02-07 teaches Thymeleaf fundamentals in-context during the capstone project lessons. This avoids adding yet another module and keeps the focus on practical application within Spring Boot.

### Specific Lessons Flagged for Content Issues

| Lesson | Issue | Affects Plan |
|--------|-------|-------------|
| Module 05 Lessons 5.6-5.7 | Duplicate content with Module 06 Lessons S.1-S.2 | 02-04 |
| Module 07 Lesson C.5 | Title said "Java 21" -- FIXED in this plan | -- |
| Module 08 | Difficulty metadata says "beginner" -- should be "intermediate" | 02-02 |
| Module 11 | Description said "Spring Boot 3.4+" -- FIXED in this plan | -- |
| Module 15 | Difficulty metadata says "beginner" -- should be "advanced" | 02-02 |
| Module 15 Lesson 15.7 | Virtual threads content duplicates Module 07 | 02-06 |
| Module 01 Lesson 1.5 key_point | Primitive patterns still labeled "preview" -- correct for JDK 25 | 02-03 |

## Deviations from Plan

### Auto-fixed Issues

**1. [Rule 2 - Missing Critical] Updated Module 11 Spring Boot version reference**
- **Found during:** Task 2 (scanning module.json files for version references)
- **Issue:** Module 11 description still said "Spring Boot 3.4+" after Task 1 only updated version-manifest.json and course.json
- **Fix:** Updated module 11 description to "Spring Boot 4.0+"
- **Files modified:** content/courses/java/modules/11-spring-boot/module.json
- **Commit:** ae1a3a3a

**2. [Rule 1 - Bug] Fixed "Stable in Java 23" content reference**
- **Found during:** Task 2 (fixing enable-preview in module 15 file)
- **Issue:** Module 15 lesson 15.7 content title said "Structured Concurrency (Stable in Java 23)" and code comment said "STRUCTURED CONCURRENCY (Stable in Java 23)" -- outdated version reference
- **Fix:** Removed version numbers from title and code comment, made version-neutral
- **Files modified:** content/courses/java/modules/15-full-stack-development/lessons/07-lesson-157-virtual-threads-project-loom-modern-concurrency/content/07-theory.md
- **Commit:** ae1a3a3a

## Commits

| Task | Commit | Message |
|------|--------|---------|
| 1 | 333a4dc9 | feat(02-01): update Java version targets to Java 25 LTS and Spring Boot 4.0.x |
| 2 | ae1a3a3a | feat(02-01): fix outdated version refs in titles/dirs, remove enable-preview, structural review |

## Next Phase Readiness

All subsequent plans (02-02 through 02-08) can now reference files at their correct paths. Key inputs for downstream plans:

- **02-02 (IO.println)**: Module 08 and Module 15 difficulty metadata should be corrected
- **02-03 (Content accuracy)**: Primitive type patterns in Module 01 are correctly marked as preview for JDK 25
- **02-04 (Streams)**: Module 05/06 overlap documented; use this to guide consolidation
- **02-06 (Concurrency)**: Module 15 lesson 15.7 duplication with Module 07 documented
- **02-07 (Capstone)**: Thymeleaf in-context teaching approach confirmed
