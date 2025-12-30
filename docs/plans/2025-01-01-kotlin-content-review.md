# Kotlin Course Content Quality Review Plan

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Systematically review all 87 lessons in the Kotlin course for accuracy, completeness, freshness, and pedagogical gaps.

**Architecture:** Process each lesson through an AI agent with web search capability. The agent verifies content against Kotlin 2.x, modern tooling (K2 compiler, KSP), and current best practices for backend (Ktor) and mobile (Compose).

**Tech Stack:** PowerShell scripts, AI agents with web search, JSON course files, Markdown review reports

---

## Course Overview

- **Course ID:** kotlin
- **Total Modules:** 10
- **Total Lessons:** 87
- **Topics Covered:** Kotlin Basics → Control Flow → OOP → Advanced Kotlin → Backend (Ktor) → Mobile (Jetpack Compose) → Gradle → Arrow (Functional) → Modern Tooling

## Review Criteria

For each lesson, the AI reviewer must:

1. **Accuracy** - Verify code works with Kotlin 2.x and K2 compiler
2. **Completeness** - Ensure all concepts needed for understanding are present
3. **Freshness** - Check against latest Kotlin features and ecosystem updates
4. **Pedagogical Gaps** - Identify missing explanations or prerequisite knowledge

---

## Module 1: Kotlin Fundamentals (9 lessons)

### Tasks 1.1-1.9: Review lessons 1.1 through 1.9

**Topics:** Variables, Types, Null Safety, Functions, String Templates

**AI Review Focus:**
- Kotlin 2.0 changes
- K2 compiler benefits
- `val` vs `var` guidance
- Null safety best practices
- When to use `lateinit` vs nullable

---

## Module 2: Control Flow (5 lessons)

### Tasks 2.1-2.5: Review lessons 2.1 through 2.5

**Topics:** If/Else, When, Loops, Ranges

**AI Review Focus:**
- `when` as expression vs statement
- Exhaustive when for sealed classes
- Range operators
- Destructuring in for loops

---

## Module 3: Object-Oriented Kotlin (7 lessons)

### Tasks 3.1-3.7: Review lessons 3.1 through 3.7

**Topics:** Classes, Data Classes, Sealed Classes, Objects, Interfaces

**AI Review Focus:**
- Data class `copy()` patterns
- Sealed class hierarchies
- Companion objects
- Object declarations vs expressions
- Interface default implementations

---

## Module 4: Advanced Kotlin (13 lessons)

### Tasks 4.1-4.13: Review lessons 4.1 through 4.13

**Topics:** Generics, Coroutines, Extension Functions, DSLs, Annotations

**AI Review Focus:**
- Variance (`in`, `out`) clarity
- Coroutine structured concurrency
- Flow API updates
- Context receivers (experimental)
- DSL markers

---

## Module 5: Backend with Ktor (15 lessons)

### Tasks 5.1-5.15: Review lessons 5.1 through 5.15

**Topics:** Ktor Framework, Routing, Serialization, Authentication, Database

**AI Review Focus:**
- Ktor 2.x/3.x syntax
- Exposed ORM patterns
- kotlinx.serialization
- JWT authentication
- Koin dependency injection

---

## Module 6: Jetpack Compose (10 lessons)

### Tasks 6.1-6.10: Review lessons 6.1 through 6.10

**Topics:** Composable Functions, State, Layouts, Navigation, Theming

**AI Review Focus:**
- Compose latest APIs
- State hoisting patterns
- Navigation Compose
- Material 3 components
- Side effects (`LaunchedEffect`, etc.)

---

## Module 7: Production & DevOps (8 lessons)

### Tasks 7.1-7.8: Review lessons 7.1 through 7.8

**Topics:** Testing, CI/CD, Docker, Monitoring, Security

**AI Review Focus:**
- Kotest vs JUnit 5
- MockK patterns
- Testcontainers Kotlin
- Ktor testing utilities
- GitHub Actions for Kotlin

---

## Module 8: Gradle & Build (6 lessons)

### Tasks 8.1-8.6: Review lessons 8.1 through 8.6

**Topics:** Gradle Kotlin DSL, Dependencies, Plugins, Multi-module

**AI Review Focus:**
- Gradle 8.x features
- Version catalogs
- Convention plugins
- Kotlin Multiplatform setup
- Build cache optimization

---

## Module 9: Functional with Arrow (6 lessons)

### Tasks 9.1-9.6: Review lessons 9.1 through 9.6

**Topics:** Arrow Library, Either, Option, Validated, Raise

**AI Review Focus:**
- Arrow 2.x syntax
- `either { }` builder
- Raise context
- Railway-oriented programming
- Integration with coroutines

---

## Module 10: Modern Tooling (8 lessons)

### Tasks 10.1-10.8: Review lessons 10.1 through 10.8

**Topics:** K2 Compiler, KSP, Context Receivers, Multiplatform

**AI Review Focus:**
- K2 migration guide
- KSP vs kapt performance
- Context receivers status
- Kotlin Multiplatform Mobile
- Compose Multiplatform

---

## Execution Steps

### Step 1: Generate all review prompts

```powershell
powershell -File scripts/batch-review-lessons.ps1 -Course kotlin
```

### Step 2: Process each prompt with AI agent

For each `kotlin-lesson-*-review-prompt.md`:
1. Load prompt
2. AI performs web searches for Kotlin 2.x, Ktor, Compose documentation
3. Save result as `kotlin-lesson-*-review-result.json`

### Step 3: Aggregate results

```powershell
powershell -File scripts/aggregate-reviews.ps1
```

### Step 4: Fix high-priority issues

Review `docs/audits/content-review-summary.md` and apply fixes.

---

## Success Criteria

- [ ] All 87 lessons reviewed
- [ ] All code examples verified against Kotlin 2.x
- [ ] K2 compiler compatibility confirmed
- [ ] All content sections > 50 characters
- [ ] All deprecated patterns flagged and updated
- [ ] All pedagogical gaps documented
