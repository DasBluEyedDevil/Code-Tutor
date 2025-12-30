# Java Course Content Quality Review Plan

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Systematically review all 63 lessons in the Java course for accuracy, completeness, freshness, and pedagogical gaps.

**Architecture:** Process each lesson through an AI agent with web search capability. The agent verifies content against Java 21/23, modern tooling, and current best practices for Spring Boot and web development.

**Tech Stack:** PowerShell scripts, AI agents with web search, JSON course files, Markdown review reports

---

## Course Overview

- **Course ID:** java
- **Total Modules:** 10
- **Total Lessons:** 63
- **Topics Covered:** Java Fundamentals → OOP → Collections → Exceptions → Testing → Database (JDBC) → Spring Boot → REST APIs → Full-Stack → Capstone

## Review Criteria

For each lesson, the AI reviewer must:

1. **Accuracy** - Verify code works with Java 21/23 LTS
2. **Completeness** - Ensure all concepts needed for understanding are present
3. **Freshness** - Check against latest Java features and Spring Boot 3.x patterns
4. **Pedagogical Gaps** - Identify missing explanations or prerequisite knowledge

---

## Module 1: Java Fundamentals (6 lessons)

### Tasks 1.1-1.6: Review epoch-1-lesson-1 through epoch-1-lesson-6

**Topics:** JDK Setup, First Program, Variables, Data Types, Operators

**AI Review Focus:**
- Java 21/23 installation
- `var` type inference usage
- Text blocks for strings
- Modern number formatting

---

## Module 2: Control Flow (6 lessons)

### Tasks 2.1-2.6: Review epoch-2-lesson-1 through epoch-2-lesson-6

**Topics:** If/Else, Switch, Loops, Break/Continue

**AI Review Focus:**
- Switch expressions (Java 14+)
- Pattern matching in switch (Java 21)
- Enhanced for loop patterns
- When to use each loop type

---

## Module 3: Object-Oriented Programming (8 lessons)

### Tasks 3.1-3.8: Review epoch-3-lesson-1 through epoch-3-lesson-8

**Topics:** Classes, Objects, Constructors, Inheritance, Polymorphism, Interfaces

**AI Review Focus:**
- Record types (Java 16+)
- Sealed classes (Java 17+)
- Pattern matching for instanceof
- Interface private methods

---

## Module 4: Collections Framework (6 lessons)

### Tasks 4.1-4.6: Review epoch-4-lesson-1 through epoch-4-lesson-6

**Topics:** Lists, Sets, Maps, Iterators, Streams

**AI Review Focus:**
- Stream API patterns
- `toList()` collector (Java 16+)
- Sequenced collections (Java 21)
- Record patterns in streams

---

## Module 5: Exception Handling (4 lessons)

### Tasks 5.1-5.4: Review epoch-5-lesson-1 through epoch-5-lesson-4

**Topics:** Try/Catch, Throw, Custom Exceptions, Try-with-resources

**AI Review Focus:**
- Multi-catch syntax
- Try-with-resources patterns
- Exception chaining
- NullPointerException clarity (Java 14+)

---

## Module 6: Testing with JUnit (5 lessons)

### Tasks 6.1-6.5: Review epoch-6-lesson-1 through epoch-6-lesson-5

**Topics:** JUnit 5, Assertions, Test Lifecycle, Mocking

**AI Review Focus:**
- JUnit 5 annotations
- Mockito 5.x patterns
- AssertJ vs Hamcrest
- Parameterized tests

---

## Module 7: Database Access (7 lessons)

### Tasks 7.1-7.7: Review epoch-7-lesson-1 through epoch-7-lesson-7

**Topics:** JDBC, Connection Pooling, Queries, Transactions

**AI Review Focus:**
- HikariCP as standard
- Prepared statements (SQL injection prevention)
- Transaction patterns
- Connection lifecycle

---

## Module 8: Spring Boot Fundamentals (9 lessons)

### Tasks 8.1-8.9: Review epoch-8-lesson-1 through epoch-8-lesson-9

**Topics:** Spring Boot Setup, Dependency Injection, Configuration, Beans

**AI Review Focus:**
- Spring Boot 3.x features
- `@Configuration` patterns
- Property binding
- Profiles and environments

---

## Module 9: REST APIs with Spring (9 lessons)

### Tasks 9.1-9.9: Review epoch-9-lesson-1 through epoch-9-lesson-9

**Topics:** Controllers, RequestMapping, Validation, Error Handling, Security

**AI Review Focus:**
- `@RestController` patterns
- Problem Details (RFC 7807)
- Bean Validation 3.0
- Spring Security 6.x
- OpenAPI/Swagger integration

---

## Module 10: Capstone Project (3 lessons)

### Tasks 10.1-10.3: Review epoch-10-lesson-1 through epoch-10-lesson-3

**Topics:** Full-Stack Application, Deployment, Best Practices

**AI Review Focus:**
- Docker deployment
- Spring Boot Native (GraalVM)
- Cloud deployment patterns
- Production configuration

---

## Execution Steps

### Step 1: Generate all review prompts

```powershell
powershell -File scripts/batch-review-lessons.ps1 -Course java
```

### Step 2: Process each prompt with AI agent

For each `java-lesson-*-review-prompt.md`:
1. Load prompt
2. AI performs web searches for Java 21/23, Spring Boot 3.x documentation
3. Save result as `java-lesson-*-review-result.json`

### Step 3: Aggregate results

```powershell
powershell -File scripts/aggregate-reviews.ps1
```

### Step 4: Fix high-priority issues

Review `docs/audits/content-review-summary.md` and apply fixes.

---

## Success Criteria

- [ ] All 63 lessons reviewed
- [ ] All code examples verified against Java 21/23
- [ ] Spring Boot 3.x patterns confirmed
- [ ] All content sections > 50 characters
- [ ] All deprecated patterns flagged and updated
- [ ] All pedagogical gaps documented
