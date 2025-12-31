# Java Full-Stack Course Execution Plan

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Transform the existing 10-module Java course into a comprehensive 15-module full-stack curriculum with dual Java syntax, complete content, and no placeholders.

**Architecture:** Phase-based execution updating existing modules first, then creating new modules, then restructuring. Each lesson modification requires web research for current best practices, followed by content updates with complete code examples.

**Tech Stack:** Java 21 LTS / Java 23+ syntax, Spring Boot 3.4.x, React 19, JUnit 5, Docker, GitHub Actions, Railway

---

## Current State Analysis

**Existing Modules (10 total, 63 lessons):**
| # | Module ID | Title | Lessons |
|---|-----------|-------|---------|
| 1 | module-01 | Java Fundamentals | 6 (epoch-0) |
| 2 | module-02 | Data Types, Loops, Methods | 6 (epoch-1) |
| 3 | module-03 | OOP | 7 (epoch-2) |
| 4 | module-04 | Collections & Functional | 7 (epoch-3) |
| 5 | module-05 | Testing & Build Tools | 6 (epoch-4) |
| 6 | module-06 | Databases & SQL | 5 (epoch-5) |
| 7 | module-07 | Web Fundamentals & APIs | 3 (epoch-6) |
| 8 | module-08 | Spring Boot | 7 (epoch-7) |
| 9 | module-09 | Full-Stack Development | 7 (epoch-8) |
| 10 | module-10 | Capstone | 9 (epoch-9) |

**Target Structure (15 modules, ~84 lessons):**
See design document: `docs/plans/2025-12-31-java-fullstack-course-redesign.md`

---

## Phase 1: Update Existing Modules with Dual Syntax

### Task 1.1: Create Dual Syntax Helper Template

**Files:**
- Create: `scripts/dual-syntax-template.md`

**Step 1: Create the template file**

```markdown
## Dual Syntax Pattern

When presenting code, use this structure:

### Modern Java (23+)
```java
void main() {
    println("Hello, World!");
}
```

<details>
<summary>📜 Traditional Syntax (Java 8-21)</summary>

```java
public class HelloWorld {
    public static void main(String[] args) {
        System.out.println("Hello, World!");
    }
}
```

**Why the difference?** Modern Java uses implicit classes (JEP 477) to reduce boilerplate. The traditional syntax is what you'll see in most existing codebases and is required for Java 21 LTS and earlier.

</details>
```

**Step 2: Commit**

```bash
git add scripts/dual-syntax-template.md
git commit -m "docs: add dual syntax template for lesson content"
```

---

### Task 1.2: Update Module 1 Lesson 2 (First Java Program)

**Files:**
- Modify: `content/courses/java/course.json` (epoch-0-lesson-2)

**Step 1: Research current Java syntax**

Run web search: "Java 21 vs Java 23 implicit classes main method differences 2025"

**Step 2: Update lesson content**

Locate `epoch-0-lesson-2` in course.json and update the "Your First Java Program" THEORY section to include both syntaxes with clear explanation of when to use each.

**Content to add after modern syntax section:**

```json
{
  "type": "KEY_POINT",
  "title": "Which Syntax Should You Use?",
  "content": "MODERN SYNTAX (Java 23+):\nvoid main() {\n    println(\"Hello, World!\");\n}\n\nUse this when: Learning, prototyping, or using Java 23+.\n\nTRADITIONAL SYNTAX (Java 8-21 LTS):\npublic class HelloWorld {\n    public static void main(String[] args) {\n        System.out.println(\"Hello, World!\");\n    }\n}\n\nUse this when: Working in enterprise codebases, using Java 21 LTS, or in job interviews.\n\nIMPORTANT: Most companies use Java 17 or 21 LTS. You MUST recognize both syntaxes. This course teaches modern syntax first (it's simpler!) but always shows the traditional equivalent."
}
```

**Step 3: Update challenges to accept both syntaxes**

Update test cases to accept either syntax format.

**Step 4: Commit**

```bash
git add content/courses/java/course.json
git commit -m "feat(java): add dual syntax to Lesson 0.2 - First Java Program"
```

---

### Task 1.3: Update Module 1 Remaining Lessons (1.3-1.6)

**Files:**
- Modify: `content/courses/java/course.json` (epoch-0-lesson-3 through epoch-0-lesson-6)

**Step 1: For each lesson, add dual syntax examples**

For variables (1.3):
- Show `var name = "Alice";` (modern)
- Show `String name = "Alice";` (traditional)

For if/else (1.4):
- Pattern matching with instanceof (Java 16+)
- Traditional instanceof with cast

For switch (1.5):
- Switch expressions (Java 14+)
- Traditional switch statements

For modern syntax overview (1.6):
- This lesson already covers modern features - add LTS equivalents

**Step 2: Commit after each lesson**

```bash
git commit -m "feat(java): add dual syntax to Lesson 0.X - [Topic]"
```

---

### Task 1.4: Update Module 2 (Data Types, Loops, Methods)

**Files:**
- Modify: `content/courses/java/course.json` (epoch-1-lesson-1 through epoch-1-lesson-6)

**Step 1: Research current best practices**

Run web search: "Java text blocks String.formatted best practices 2025"

**Step 2: Update each lesson**

| Lesson | Key Updates |
|--------|-------------|
| 1.1 Data Types | Add text blocks for multi-line strings |
| 1.2 Operators | No major changes needed |
| 1.3 While Loops | No major changes needed |
| 1.4 For Loops | Enhanced for-loop patterns |
| 1.5 Methods | Method references preview |
| 1.6 public/static/void | Explain why modern syntax hides these |

**Step 3: Commit**

```bash
git commit -m "feat(java): update Module 2 with modern Java patterns"
```

---

### Task 1.5: Update Module 3 (OOP) - Add Records & Sealed Classes

**Files:**
- Modify: `content/courses/java/course.json` (epoch-2-lesson-7)

**Step 1: Research records and sealed classes**

Run web search: "Java 21 records sealed classes best practices examples 2025"

**Step 2: Verify epoch-2-lesson-7 (Records) content is complete**

Check that the lesson covers:
- Record syntax and components
- Compact constructors
- When to use records vs classes
- Limitations of records

**Step 3: Add new lesson for Sealed Classes if missing**

If sealed classes are not covered, add content section to existing lesson or create new lesson.

**Step 4: Commit**

```bash
git commit -m "feat(java): enhance OOP module with records and sealed classes"
```

---

### Task 1.6: Update Module 4 (Collections) - Add Generics Depth & Sequenced Collections

**Files:**
- Modify: `content/courses/java/course.json` (module-04 lessons)

**Step 1: Research Java 21 Sequenced Collections**

Run web search: "Java 21 Sequenced Collections JEP 431 examples tutorial"

**Step 2: Update existing lessons**

Add generics depth to ArrayList/HashMap lessons:
- Type parameters explanation
- Diamond operator
- Bounded types (brief mention)

**Step 3: Add Sequenced Collections content**

Add to appropriate lesson:
```
Java 21 introduced Sequenced Collections (JEP 431):

SequencedCollection<E> - ordered collection with first/last access
- getFirst(), getLast()
- addFirst(e), addLast(e)
- removeFirst(), removeLast()
- reversed()

List, Deque, SortedSet now implement SequencedCollection.
```

**Step 4: Commit**

```bash
git commit -m "feat(java): add Sequenced Collections and generics depth to Module 4"
```

---

### Task 1.7: Update Module 5 (Testing) - JUnit 5 Best Practices

**Files:**
- Modify: `content/courses/java/course.json` (module-05 lessons)

**Step 1: Research JUnit 5 current practices**

Run web search: "JUnit 5 best practices 2025 Spring Boot @MockitoBean"

**Step 2: Update testing lessons**

Ensure coverage of:
- `@ExtendWith(MockitoExtension.class)` for unit tests
- `@MockitoBean` (Spring Boot 3.4+ replacement for `@MockBean`)
- Test slices: `@WebMvcTest`, `@DataJpaTest`
- Testcontainers mention

**Step 3: Commit**

```bash
git commit -m "feat(java): update testing module with JUnit 5.10+ patterns"
```

---

### Task 1.8: Update Module 6 (Databases) - Expand to JPA

**Files:**
- Modify: `content/courses/java/course.json` (module-06 lessons)

**Step 1: Check current content**

Verify JDBC lesson exists (epoch-5-lesson-5).

**Step 2: Ensure progression is clear**

SQL basics → JDBC (understand the pain) → JPA (the solution)

**Step 3: Add Flyway migrations content if missing**

```
Database Migrations with Flyway:

Why? Schema changes need version control like code.

1. Add dependency: org.flywaydb:flyway-core
2. Create: src/main/resources/db/migration/V1__create_users.sql
3. Flyway auto-runs on startup

Naming: V{version}__{description}.sql
```

**Step 4: Commit**

```bash
git commit -m "feat(java): enhance database module with JPA and migrations focus"
```

---

### Task 1.9: Update Modules 7-8 (Web & Spring Boot) - Spring Boot 3.4.x

**Files:**
- Modify: `content/courses/java/course.json` (modules 07 and 08)

**Step 1: Research Spring Boot 3.4 changes**

Run web search: "Spring Boot 3.4 new features virtual threads Problem Details"

**Step 2: Update content for Spring Boot 3.4**

Key updates:
- Virtual threads enabled: `spring.threads.virtual.enabled=true`
- Problem Details (RFC 7807) for error responses
- `@MockitoBean` replacing `@MockBean`
- Structured logging with `spring.application.group`

**Step 3: Commit**

```bash
git commit -m "feat(java): update Spring Boot modules to 3.4.x patterns"
```

---

## Phase 2: Create New Modules

### Task 2.1: Create Module - Git & Development Workflow

**Files:**
- Modify: `content/courses/java/course.json` (insert new module after module-02)

**Step 1: Research Git best practices**

Run web search: "Git workflow best practices 2025 beginners"

**Step 2: Create 4 new lessons**

**Lesson 3.1: Why Version Control?**
```json
{
  "id": "git-lesson-1",
  "title": "Lesson 3.1: Why Version Control?",
  "moduleId": "module-03-git",
  "order": 1,
  "estimatedMinutes": 20,
  "difficulty": "beginner",
  "contentSections": [
    {
      "type": "THEORY",
      "title": "The Problem: Code Without History",
      "content": "Imagine writing an essay and making changes for a week. Then your teacher says 'I liked the version from Tuesday better.' Without saving each version, you're stuck.\n\nNow imagine 5 people editing the same essay simultaneously. Chaos!\n\nThis is software development without version control:\n• No way to undo mistakes\n• No history of what changed and why\n• Impossible to collaborate\n• 'It worked yesterday' with no proof\n\nVersion control solves ALL of these problems."
    },
    {
      "type": "KEY_POINT",
      "title": "Git: The Time Machine for Code",
      "content": "Git is the most popular version control system. Think of it as:\n\n1. A TIME MACHINE - Go back to any previous version\n2. A COLLABORATION TOOL - Multiple people work on the same code\n3. A SAFETY NET - Experiment without fear of breaking things\n4. A HISTORY BOOK - See who changed what, when, and why\n\nEvery professional developer uses Git. Every company requires it. Learning Git is not optional."
    },
    {
      "type": "THEORY",
      "title": "Mental Model: Snapshots, Not Changes",
      "content": "Git doesn't store 'changes' - it stores SNAPSHOTS of your entire project at a point in time.\n\nImagine taking a photo of your desk every hour:\n• 9am: Clean desk, laptop closed\n• 10am: Coffee cup appeared, laptop open\n• 11am: Papers everywhere, coffee half empty\n\nEach photo is complete - you can 'restore' any moment.\n\nGit calls each snapshot a COMMIT. Your project's history is a series of commits, each with a message explaining what changed and why."
    }
  ],
  "challenges": []
}
```

**Lesson 3.2: Git Basics**
- init, add, commit, status, log, diff
- Complete hands-on exercises

**Lesson 3.3: Branching & Merging**
- Create branches, switch, merge
- Resolve conflicts

**Lesson 3.4: GitHub & Collaboration**
- Remote repos, push/pull
- Pull requests, code review

**Step 3: Commit**

```bash
git commit -m "feat(java): add Git & Development Workflow module (Module 3)"
```

---

### Task 2.2: Create Module - Streams & Functional Programming (Expanded)

**Files:**
- Modify: `content/courses/java/course.json` (expand existing content or create new module)

**Step 1: Research Stream API patterns**

Run web search: "Java Stream API best practices 2025 Optional patterns"

**Step 2: Create/expand to 5 lessons**

| Lesson | Content |
|--------|---------|
| 6.1 | Lambdas & Functional Interfaces (Predicate, Function, Consumer) |
| 6.2 | Stream Basics (stream, filter, map, forEach) |
| 6.3 | Collecting Results (collect, toList, Collectors) |
| 6.4 | Advanced Streams (flatMap, reduce, groupingBy) |
| 6.5 | Optional & Null Safety |

**Step 3: Include complete, runnable examples**

Each lesson must have:
- Full code examples (not snippets)
- Real-world use cases
- 2-3 challenges with solutions

**Step 4: Commit**

```bash
git commit -m "feat(java): add Streams & Functional Programming module"
```

---

### Task 2.3: Create Module - Concurrency & Virtual Threads

**Files:**
- Modify: `content/courses/java/course.json` (new module)

**Step 1: Research Java 21 concurrency**

Run web search: "Java 21 virtual threads tutorial CompletableFuture best practices 2025"

**Step 2: Create 5 lessons**

| Lesson | Content |
|--------|---------|
| 7.1 | Why Concurrency? (real-world need, mental model) |
| 7.2 | Threads & Runnables (creating threads, lifecycle) |
| 7.3 | Executors & Thread Pools (ExecutorService, shutdown) |
| 7.4 | CompletableFuture (async operations, chaining) |
| 7.5 | Virtual Threads - Java 21 (Thread.ofVirtual, Spring integration) |

**Step 3: Virtual Threads lesson content**

```json
{
  "type": "THEORY",
  "title": "Virtual Threads: The Game Changer",
  "content": "Before Java 21, each thread was expensive:\n• Platform threads map 1:1 to OS threads\n• Creating 10,000 threads? Your app crashes.\n• Solution: Thread pools, but complex to manage\n\nJava 21 introduces VIRTUAL THREADS (Project Loom):\n• Lightweight - create millions without crashing\n• JVM manages them, not the OS\n• Same Thread API - your code barely changes\n\n// Create a virtual thread\nThread.ofVirtual().start(() -> {\n    System.out.println(\"Hello from virtual thread!\");\n});\n\n// Or use an executor\nvar executor = Executors.newVirtualThreadPerTaskExecutor();\nexecutor.submit(() -> handleRequest());\n\nSpring Boot 3.2+:\nspring.threads.virtual.enabled=true\n\nThat's it! All request handling uses virtual threads automatically."
}
```

**Step 4: Commit**

```bash
git commit -m "feat(java): add Concurrency & Virtual Threads module"
```

---

### Task 2.4: Create Module - Security (Sessions & JWT)

**Files:**
- Modify: `content/courses/java/course.json` (new module after Spring Boot)

**Step 1: Research Spring Security 6.x**

Run web search: "Spring Security 6 JWT authentication tutorial 2025"

**Step 2: Create 5 lessons**

| Lesson | Content |
|--------|---------|
| 12.1 | Web Security Fundamentals (auth vs authz, common attacks) |
| 12.2 | Spring Security Basics (filter chain, protecting endpoints) |
| 12.3 | Session-Based Authentication (form login, sessions) |
| 12.4 | JWT Authentication (token structure, stateless auth) |
| 12.5 | Role-Based Access Control (@PreAuthorize, method security) |

**Step 3: Include complete security configuration**

```java
@Configuration
@EnableWebSecurity
public class SecurityConfig {

    @Bean
    public SecurityFilterChain securityFilterChain(HttpSecurity http) throws Exception {
        return http
            .csrf(csrf -> csrf.disable()) // For APIs
            .sessionManagement(session ->
                session.sessionCreationPolicy(SessionCreationPolicy.STATELESS))
            .authorizeHttpRequests(auth -> auth
                .requestMatchers("/api/auth/**").permitAll()
                .requestMatchers("/api/admin/**").hasRole("ADMIN")
                .anyRequest().authenticated())
            .addFilterBefore(jwtAuthFilter, UsernamePasswordAuthenticationFilter.class)
            .build();
    }
}
```

**Step 4: Commit**

```bash
git commit -m "feat(java): add Security module with Sessions & JWT"
```

---

### Task 2.5: Create Module - React Frontend Integration

**Files:**
- Modify: `content/courses/java/course.json` (new module)

**Step 1: Research React 19 + Spring Boot**

Run web search: "React 19 fetch API Spring Boot CORS integration 2025"

**Step 2: Create 6 lessons**

| Lesson | Content |
|--------|---------|
| 13.1 | Frontend Fundamentals (HTML/CSS/JS review, DOM) |
| 13.2 | React Introduction (components, JSX, props, Vite setup) |
| 13.3 | State & Events (useState, forms, controlled inputs) |
| 13.4 | Fetching Data (fetch, useEffect, loading states, CORS) |
| 13.5 | React Router (routes, navigation, URL params) |
| 13.6 | Connecting to Spring Boot (auth flow, JWT in headers) |

**Step 3: Include CORS configuration for Spring Boot**

```java
@Configuration
public class CorsConfig implements WebMvcConfigurer {

    @Override
    public void addCorsMappings(CorsRegistry registry) {
        registry.addMapping("/api/**")
            .allowedOrigins("http://localhost:5173") // Vite default
            .allowedMethods("GET", "POST", "PUT", "DELETE")
            .allowedHeaders("*")
            .allowCredentials(true);
    }
}
```

**Step 4: Commit**

```bash
git commit -m "feat(java): add React Frontend Integration module"
```

---

### Task 2.6: Create Module - DevOps & Deployment

**Files:**
- Modify: `content/courses/java/course.json` (new module)

**Step 1: Research Docker + Railway deployment**

Run web search: "Spring Boot Docker deployment Railway 2025 GitHub Actions"

**Step 2: Create 5 lessons**

| Lesson | Content |
|--------|---------|
| 14.1 | Why DevOps? (dev vs ops, CI/CD concept) |
| 14.2 | Docker Fundamentals (images, containers, Dockerfile) |
| 14.3 | Docker Compose (multi-container, Postgres) |
| 14.4 | GitHub Actions CI/CD (workflows, testing on push) |
| 14.5 | Cloud Deployment (Railway, env config, health checks) |

**Step 3: Include complete Dockerfile**

```dockerfile
# Multi-stage build for Spring Boot
FROM eclipse-temurin:21-jdk-alpine AS build
WORKDIR /app
COPY . .
RUN ./mvnw clean package -DskipTests

FROM eclipse-temurin:21-jre-alpine
WORKDIR /app
COPY --from=build /app/target/*.jar app.jar
EXPOSE 8080
ENTRYPOINT ["java", "-jar", "app.jar"]
```

**Step 4: Include GitHub Actions workflow**

```yaml
name: CI/CD Pipeline

on:
  push:
    branches: [main]
  pull_request:
    branches: [main]

jobs:
  test:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-java@v4
        with:
          java-version: '21'
          distribution: 'temurin'
      - name: Run tests
        run: ./mvnw test

  deploy:
    needs: test
    if: github.ref == 'refs/heads/main'
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - name: Deploy to Railway
        uses: berviantoleo/railway-deploy@v1
        with:
          railway_token: ${{ secrets.RAILWAY_TOKEN }}
```

**Step 5: Commit**

```bash
git commit -m "feat(java): add DevOps & Deployment module"
```

---

## Phase 3: Restructure Module Order & Capstone

### Task 3.1: Reorder Modules in course.json

**Files:**
- Modify: `content/courses/java/course.json`

**Step 1: Update module IDs and order**

New order:
1. module-01: Java Fundamentals
2. module-02: Data Types, Loops, Methods
3. module-03: Git & Development Workflow (NEW)
4. module-04: Object-Oriented Programming
5. module-05: Collections & Generics
6. module-06: Streams & Functional Programming (NEW/EXPANDED)
7. module-07: Concurrency & Virtual Threads (NEW)
8. module-08: Exception Handling & Testing (reorganized)
9. module-09: Build Tools & Project Structure (split from module-05)
10. module-10: Databases & JPA
11. module-11: Spring Boot & REST APIs
12. module-12: Security - Sessions & JWT (NEW)
13. module-13: React Frontend Integration (NEW)
14. module-14: DevOps & Deployment (NEW)
15. module-15: Capstone - Task Manager

**Step 2: Update all moduleId references in lessons**

**Step 3: Commit**

```bash
git commit -m "refactor(java): reorder modules to match new curriculum structure"
```

---

### Task 3.2: Restructure Capstone as Task Manager

**Files:**
- Modify: `content/courses/java/course.json` (module-15 lessons)

**Step 1: Update capstone lessons**

| Lesson | New Content |
|--------|-------------|
| 15.1 | Task Manager requirements, wireframes, data model |
| 15.2 | Backend setup: Spring Boot, entities, Flyway migrations |
| 15.3 | REST API: CRUD for tasks, users, categories |
| 15.4 | Authentication: JWT, registration, login |
| 15.5 | Business logic: due dates, priorities, validation |
| 15.6 | React frontend: task list, forms, filtering |
| 15.7 | Integration: full auth flow, API consumption |
| 15.8 | Testing: unit tests, integration tests |
| 15.9 | Deployment: Docker, CI/CD, Railway |

**Step 2: Define Task Manager data model**

```java
// User entity
@Entity
public class User {
    @Id @GeneratedValue
    private Long id;
    private String email;
    private String password;
    private String name;

    @OneToMany(mappedBy = "user")
    private List<Task> tasks;
}

// Task entity
@Entity
public class Task {
    @Id @GeneratedValue
    private Long id;
    private String title;
    private String description;
    private LocalDate dueDate;
    private Priority priority; // enum: LOW, MEDIUM, HIGH
    private boolean completed;

    @ManyToOne
    private User user;

    @ManyToOne
    private Category category;
}

// Category entity
@Entity
public class Category {
    @Id @GeneratedValue
    private Long id;
    private String name;
    private String color;
}
```

**Step 3: Commit**

```bash
git commit -m "feat(java): restructure capstone as Task Manager project"
```

---

## Phase 4: Quality Assurance

### Task 4.1: Verify All Code Examples

**Step 1: Create test project**

```bash
cd /tmp
mkdir java-course-test
cd java-course-test
# Test all code examples compile and run
```

**Step 2: Test each module's code**

For each module, verify:
- [ ] All code compiles with Java 21
- [ ] All code compiles with Java 23 (if using preview features)
- [ ] All challenges have working solutions
- [ ] All test cases pass

---

### Task 4.2: Update Course Metadata

**Files:**
- Modify: `content/courses/java/course.json` (top-level metadata)

**Step 1: Update estimatedHours**

Calculate based on 15 modules:
- ~5-6 hours per module average
- Total: ~80-90 hours

**Step 2: Update description**

```json
{
  "description": "Master Java from absolute beginner to job-ready full-stack developer. Covers Java 21/23 with dual syntax, Spring Boot 3.4, React, databases, security, testing, and deployment. Build a complete Task Manager application.",
  "estimatedHours": 85
}
```

**Step 3: Commit**

```bash
git commit -m "docs(java): update course metadata for expanded curriculum"
```

---

### Task 4.3: Cross-Module Consistency Check

**Step 1: Verify lesson numbering**

All lessons should follow pattern: `Lesson X.Y: Title`
- X = module number
- Y = lesson within module

**Step 2: Verify challenge IDs are unique**

No duplicate challenge IDs across entire course.

**Step 3: Verify all challenges have:**
- Description
- Instructions
- Starter code
- Solution
- Test cases
- Hints

**Step 4: Final commit**

```bash
git commit -m "chore(java): quality assurance pass complete"
```

---

## Success Criteria Checklist

- [ ] All 15 modules present with complete lessons
- [ ] All code examples show dual syntax (modern + traditional)
- [ ] All code verified against Java 21 LTS
- [ ] Spring Boot 3.4.x patterns throughout
- [ ] React 19 with Vite in frontend module
- [ ] Complete Task Manager capstone with deployment
- [ ] All challenges have working solutions
- [ ] No stubs, placeholders, or TODOs
- [ ] Estimated hours updated to ~85
- [ ] Git commits at each task completion

---

## Research Sources

Perform web searches before each task:

| Topic | Search Query |
|-------|--------------|
| Java syntax | "Java 21 vs 23 implicit classes main method 2025" |
| Text blocks | "Java text blocks String.formatted examples" |
| Records | "Java 21 records sealed classes tutorial" |
| Collections | "Java 21 Sequenced Collections JEP 431" |
| Streams | "Java Stream API best practices 2025" |
| Virtual Threads | "Java 21 virtual threads Spring Boot 3.4" |
| JUnit 5 | "JUnit 5.10 Spring Boot 3.4 @MockitoBean" |
| Spring Security | "Spring Security 6 JWT tutorial 2025" |
| React | "React 19 fetch API CORS Spring Boot" |
| Docker | "Spring Boot Docker multi-stage build 2025" |
| Railway | "Railway Spring Boot deployment guide" |
| GitHub Actions | "GitHub Actions Java Maven workflow" |
