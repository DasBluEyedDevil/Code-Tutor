# C# Course Content Quality Review Plan

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Systematically review all 104 lessons in the C# course for accuracy, completeness, freshness, and pedagogical gaps.

**Architecture:** Process each lesson through an AI agent with web search capability. The agent verifies content against current C# 12/13 and .NET 8/9 documentation, identifies outdated patterns, and flags content gaps.

**Tech Stack:** PowerShell scripts, AI agents with web search, JSON course files, Markdown review reports

---

## Course Overview

- **Course ID:** csharp
- **Total Modules:** 18
- **Total Lessons:** 104
- **Topics Covered:** C# Basics → OOP → LINQ → Async/Await → Web APIs → Databases → Blazor → Testing → Cloud/Deployment

## Review Criteria

For each lesson, the AI reviewer must:

1. **Accuracy** - Verify code works with C# 12/13 and .NET 8/9
2. **Completeness** - Ensure all concepts needed for understanding are present
3. **Freshness** - Check against latest C# features and best practices (2024-2025)
4. **Pedagogical Gaps** - Identify missing explanations or prerequisite knowledge

---

## Module 1: Getting Started (5 lessons)

### Task 1.1: Review lesson-01-01

**Lesson:** Getting Started / Your First C# Program

**AI Review Instructions:**
1. Search: "C# hello world 2024 .NET 8"
2. Verify: top-level statements syntax is current
3. Check: `Console.WriteLine` vs newer output methods
4. Gaps: Does it explain what .NET is?

**Files:**
- Read: `content/courses/csharp/course.json` → modules[0].lessons[0]
- Output: `docs/audits/content-reviews/csharp-lesson-01-01-review.json`

### Task 1.2: Review lesson-01-02

**Lesson:** Variables and Data Types

**AI Review Instructions:**
1. Search: "C# primitive types 2024"
2. Verify: `var` vs explicit typing guidance is current
3. Check: mentions of `nint`/`nuint` if appropriate
4. Gaps: Does it explain stack vs heap for value/reference types?

### Task 1.3: Review lesson-01-03

**Lesson:** Operators and Expressions

**AI Review Instructions:**
1. Search: "C# operators 2024"
2. Verify: pattern matching operators coverage
3. Check: null-coalescing operators (`??`, `??=`)
4. Gaps: Does it cover operator precedence?

### Task 1.4: Review lesson-01-04

**Lesson:** Control Flow - If/Else

**AI Review Instructions:**
1. Search: "C# if statement best practices"
2. Verify: switch expression syntax (C# 8+)
3. Check: pattern matching in if statements
4. Gaps: When to use switch vs if-else chains?

### Task 1.5: Review lesson-01-05

**Lesson:** Loops

**AI Review Instructions:**
1. Search: "C# loops performance 2024"
2. Verify: foreach with Span<T> coverage
3. Check: parallel loops mentioned?
4. Gaps: When to use each loop type?

---

## Module 2: Working with Data (6 lessons)

### Task 2.1-2.6: Review lessons lesson-02-01 through lesson-02-06

**Topics:** Strings, Arrays, Collections, Lists, Dictionaries, LINQ Basics

**AI Review Focus:**
- String interpolation with raw string literals (C# 11)
- Collection expressions (C# 12)
- `List<T>` vs `ImmutableList<T>` guidance
- LINQ performance considerations
- Span<T> and Memory<T> for performance

---

## Module 3: Object-Oriented Programming (5 lessons)

### Task 3.1-3.5: Review lessons lesson-03-01 through lesson-03-05

**Topics:** Classes, Objects, Encapsulation, Inheritance, Polymorphism

**AI Review Focus:**
- Primary constructors (C# 12)
- Record types vs classes
- `init` accessors
- `required` members (C# 11)
- Sealed classes and when to use them

---

## Module 4: Advanced OOP (5 lessons)

### Task 4.1-4.5: Review lessons lesson-04-01 through lesson-04-05

**Topics:** Interfaces, Abstract Classes, Static Members, Generics, Extension Methods

**AI Review Focus:**
- Default interface implementations
- Static abstract members in interfaces (C# 11)
- Generic math interfaces (C# 11)
- Generic type constraints

---

## Module 5: Exception Handling (5 lessons)

### Task 5.1-5.5: Review lessons lesson-05-01 through lesson-05-05

**Topics:** Try/Catch, Finally, Custom Exceptions, Exception Filters

**AI Review Focus:**
- Exception filtering best practices
- Nullable reference types impact on null checks
- Result pattern vs exceptions
- When to throw vs when to return error

---

## Module 6: LINQ Deep Dive (7 lessons)

### Task 6.1-6.7: Review lessons lesson-06-01 through lesson-06-07

**Topics:** Query Syntax, Method Syntax, Deferred Execution, Aggregations, Joins, Grouping, Projections

**AI Review Focus:**
- LINQ performance with large datasets
- EF Core LINQ translation
- Async LINQ (`ToListAsync`, etc.)
- New LINQ methods in .NET 8/9

---

## Module 7: Async/Await (7 lessons)

### Task 7.1-7.7: Review lessons lesson-07-01 through lesson-07-07

**Topics:** Tasks, async/await, Parallel Programming, Cancellation, Progress Reporting

**AI Review Focus:**
- `ValueTask` vs `Task`
- `IAsyncEnumerable<T>`
- Async disposal pattern
- ConfigureAwait guidance (library vs app)
- Parallel.ForEachAsync

---

## Module 8: File I/O and Serialization (5 lessons)

### Task 8.1-8.5: Review lessons lesson-08-01 through lesson-08-05

**Topics:** File Reading/Writing, Streams, JSON Serialization, XML

**AI Review Focus:**
- `System.Text.Json` vs Newtonsoft.Json
- Source generators for JSON
- Async file operations
- `Utf8JsonReader`/`Utf8JsonWriter` for performance

---

## Module 9: Web Development Basics (6 lessons)

### Task 9.1-9.6: Review lessons lesson-09-01 through lesson-09-06

**Topics:** HTTP, REST, ASP.NET Core Basics, Controllers, Routing

**AI Review Focus:**
- Minimal APIs vs Controllers
- .NET 8/9 new features
- OpenAPI/Swagger integration
- Native AOT compatibility

---

## Module 10: Building REST APIs (5 lessons)

### Task 10.1-10.5: Review lessons lesson-10-01 through lesson-10-05

**Topics:** API Design, Validation, Error Handling, Versioning, Documentation

**AI Review Focus:**
- Problem Details (RFC 7807)
- FluentValidation vs DataAnnotations
- API versioning strategies
- Rate limiting middleware

---

## Module 11: Data Access with EF Core (6 lessons)

### Task 11.1-11.6: Review lessons lesson-11-01 through lesson-11-06

**Topics:** Entity Framework Core, DbContext, Migrations, Queries, Relationships

**AI Review Focus:**
- EF Core 8/9 new features
- Compiled queries
- Bulk operations
- Split queries for includes
- Interceptors

---

## Module 12: Authentication & Authorization (5 lessons)

### Task 12.1-12.5: Review lessons lesson-12-01 through lesson-12-05

**Topics:** Identity, JWT, OAuth, Role-Based Access, Claims

**AI Review Focus:**
- ASP.NET Core Identity updates
- Minimal API auth patterns
- Keycloak/Auth0 integration
- .NET 8 Identity API endpoints

---

## Module 13: Testing (5 lessons)

### Task 13.1-13.5: Review lessons lesson-13-01 through lesson-13-05

**Topics:** Unit Testing, xUnit, Mocking, Integration Testing, TDD

**AI Review Focus:**
- xUnit vs NUnit vs MSTest current state
- WebApplicationFactory patterns
- Testcontainers for integration tests
- Snapshot testing

---

## Modules 14-18: Advanced Topics (remaining lessons)

### Task 14-18: Review remaining lessons

**Topics:** Blazor, SignalR, gRPC, Cloud Deployment, Performance

**AI Review Focus:**
- Blazor United (.NET 8)
- Aspire for cloud-native
- Native AOT
- Docker/Kubernetes patterns

---

## Execution Steps

### Step 1: Generate all review prompts

```powershell
powershell -File scripts/batch-review-lessons.ps1 -Course csharp
```

### Step 2: Process each prompt with AI agent

For each `csharp-lesson-*-review-prompt.md`:
1. Load prompt
2. AI performs web searches for C# 12/13/.NET 8/9 documentation
3. Save result as `csharp-lesson-*-review-result.json`

### Step 3: Aggregate results

```powershell
powershell -File scripts/aggregate-reviews.ps1
```

### Step 4: Fix high-priority issues

Review `docs/audits/content-review-summary.md` and apply fixes.

---

## Success Criteria

- [ ] All 104 lessons reviewed
- [ ] All code examples verified against C# 12/13
- [ ] All content sections > 50 characters
- [ ] All deprecated patterns flagged and updated
- [ ] All pedagogical gaps documented
