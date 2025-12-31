# Java Full-Stack Course Redesign - Implementation Plan

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Created:** 2025-12-31
**Goal:** Transform the existing Java course into a complete "newbie to job-ready full-stack developer" path with no shortcuts, stubs, or placeholders.

---

## Design Decisions

| Decision | Choice | Rationale |
|----------|--------|-----------|
| Java Version | Java 23+ primary, LTS equivalents shown alongside | Modern syntax is simpler for beginners; dual presentation ensures recognition of traditional code |
| Frontend | Moderate React basics with Spring Boot integration | Delivers on "full-stack" promise without derailing into a JavaScript course |
| Concurrency | Working knowledge (5 lessons) | Backend developers need executors, CompletableFuture, virtual threads for Spring Boot async |
| Capstone | Task Manager app | Universally understood domain, covers auth, CRUD, relationships without domain complexity |
| Security | Both session-based and JWT authentication | Students encounter both patterns in the wild |
| DevOps | Full module (Docker, CI/CD, cloud deploy) | Bridges "works on my machine" to production - critical for junior employability |
| Git | Dedicated module early, reinforced throughout | Git deserves focused attention before becoming ambient practice |

---

## Target Technology Stack (2025)

| Layer | Technology | Version |
|-------|------------|---------|
| Language | Java | 21 LTS / 23+ syntax |
| Framework | Spring Boot | 3.4.x |
| Data | Spring Data JPA + PostgreSQL | JPA 3.x, Hibernate 6.x |
| Security | Spring Security | 6.x |
| Testing | JUnit 5 + Mockito + Testcontainers | JUnit 5.10+, Mockito 5.x |
| Frontend | React + Vite | React 19, Vite 5.x |
| Build | Maven (primary), Gradle (sidebar) | Maven 3.9+, Gradle 8.x |
| DevOps | Docker + GitHub Actions + Railway | Latest stable |

---

## Revised Module Structure

**Total: 15 modules, ~80-90 hours**

| # | Module | Lessons | Status | Hours |
|---|--------|---------|--------|-------|
| 1 | Java Fundamentals | 6 | UPDATE | 4 |
| 2 | Data Types, Loops, Methods | 6 | UPDATE | 5 |
| 3 | Git & Development Workflow | 4 | **NEW** | 3 |
| 4 | Object-Oriented Programming | 7 | UPDATE | 6 |
| 5 | Collections & Generics | 7 | EXPAND | 6 |
| 6 | Streams & Functional Programming | 5 | **NEW** | 5 |
| 7 | Concurrency & Virtual Threads | 5 | **NEW** | 5 |
| 8 | Exception Handling & Testing | 6 | MERGE | 5 |
| 9 | Build Tools & Project Structure | 4 | **NEW** | 3 |
| 10 | Databases & JPA | 6 | EXPAND | 6 |
| 11 | Spring Boot & REST APIs | 9 | UPDATE | 8 |
| 12 | Security: Sessions & JWT | 5 | **NEW** | 5 |
| 13 | React Frontend Integration | 6 | **NEW** | 6 |
| 14 | DevOps & Deployment | 5 | **NEW** | 5 |
| 15 | Capstone: Task Manager | 9 | RESTRUCTURE | 12 |

---

## Module Details

### Module 1: Java Fundamentals (6 lessons) - UPDATE

**Changes:** Add LTS syntax alongside modern syntax in every lesson.

| Lesson | Topic | Key Update |
|--------|-------|------------|
| 1.1 | What is a Computer Program? | No change |
| 1.2 | Your First Java Program | Show **both** `void main()` AND `public static void main(String[] args)` |
| 1.3 | Understanding Variables | Add explicit type examples alongside `var` |
| 1.4 | Data Types Deep Dive | Cover primitive vs wrapper, mention when each matters |
| 1.5 | Operators & Expressions | No major changes |
| 1.6 | String Handling | Add text blocks (Java 15+), `String.formatted()` |

**Dual Syntax Pattern:** Every code example shows modern first, then a collapsible "Traditional Syntax" block.

---

### Module 2: Data Types, Loops, Methods (6 lessons) - UPDATE

| Lesson | Topic | Key Update |
|--------|-------|------------|
| 2.1 | Control Flow: if/else | Pattern matching for `instanceof` (Java 16+) |
| 2.2 | Switch Expressions | Show both switch expressions AND switch statements |
| 2.3 | Loops | Enhanced for-loop patterns |
| 2.4 | Methods & Parameters | Mention preview: unnamed variables `_` |
| 2.5 | Method Overloading | No major changes |
| 2.6 | Scope & Lifetime | No major changes |

---

### Module 3: Git & Development Workflow (4 lessons) - NEW

| Lesson | Topic | Content |
|--------|-------|---------|
| 3.1 | Why Version Control? | Problem without Git, mental model of snapshots |
| 3.2 | Git Basics | init, add, commit, status, log, diff |
| 3.3 | Branching & Merging | Branches, checkout, merge, resolving conflicts |
| 3.4 | GitHub & Collaboration | Remote repos, push/pull, PRs, code review basics |

**Reinforcement:** From this point forward, every module includes "Commit your work" checkpoints.

---

### Module 4: Object-Oriented Programming (7 lessons) - UPDATE

| Lesson | Topic | Key Update |
|--------|-------|------------|
| 4.1 | Classes & Objects | No major changes |
| 4.2 | Constructors & Initialization | Add compact constructors |
| 4.3 | Encapsulation | No major changes |
| 4.4 | Inheritance | Mention sealed classes conceptually |
| 4.5 | Polymorphism | Pattern matching with `instanceof` |
| 4.6 | Interfaces & Abstract Classes | Private interface methods (Java 9+) |
| 4.7 | **Records & Sealed Classes** | **NEW** - immutable data carriers, restricted hierarchies |

---

### Module 5: Collections & Generics (7 lessons) - EXPAND

| Lesson | Topic | Content |
|--------|-------|---------|
| 5.1 | Why Collections? | Arrays vs Collections, Collection hierarchy diagram |
| 5.2 | Lists (ArrayList, LinkedList) | When to use each, performance trade-offs |
| 5.3 | Sets (HashSet, TreeSet) | Uniqueness, ordering, `equals()`/`hashCode()` contract |
| 5.4 | Maps (HashMap, TreeMap) | Key-value patterns, iteration, `computeIfAbsent()` |
| 5.5 | Generics Fundamentals | Type parameters, bounded types, why generics exist |
| 5.6 | Wildcards & Type Erasure | `<? extends T>`, `<? super T>`, limitations |
| 5.7 | Sequenced Collections | **Java 21** - `getFirst()`, `getLast()`, `reversed()` |

---

### Module 6: Streams & Functional Programming (5 lessons) - NEW

| Lesson | Topic | Content |
|--------|-------|---------|
| 6.1 | Lambdas & Functional Interfaces | Syntax, `Predicate`, `Function`, `Consumer`, method references |
| 6.2 | Stream Basics | `stream()`, `filter()`, `map()`, `forEach()` |
| 6.3 | Collecting Results | `collect()`, `toList()` (Java 16+), `Collectors` utilities |
| 6.4 | Advanced Stream Operations | `flatMap()`, `reduce()`, `groupingBy()`, `partitioningBy()` |
| 6.5 | Optional & Null Safety | `Optional` patterns, avoiding `NullPointerException` |

---

### Module 7: Concurrency & Virtual Threads (5 lessons) - NEW

| Lesson | Topic | Content |
|--------|-------|---------|
| 7.1 | Why Concurrency? | Real-world need, threads vs processes, mental model |
| 7.2 | Threads & Runnables | Creating threads, `Runnable`, thread lifecycle |
| 7.3 | Executors & Thread Pools | `ExecutorService`, `Executors.newFixedThreadPool()`, shutdown |
| 7.4 | CompletableFuture | Async operations, `thenApply()`, `thenCompose()`, error handling |
| 7.5 | Virtual Threads (Java 21) | `Thread.ofVirtual()`, when to use, Spring Boot integration |

---

### Module 8: Exception Handling & Testing (6 lessons) - MERGE

| Lesson | Topic | Content |
|--------|-------|---------|
| 8.1 | Exceptions Fundamentals | Checked vs unchecked, try/catch/finally |
| 8.2 | Throwing & Custom Exceptions | `throw`, creating exception classes, exception chaining |
| 8.3 | Try-with-Resources | `AutoCloseable`, resource management patterns |
| 8.4 | JUnit 5 Fundamentals | `@Test`, assertions, test lifecycle, naming conventions |
| 8.5 | Mocking with Mockito | `@Mock`, `@InjectMocks`, `when().thenReturn()`, verification |
| 8.6 | Test-Driven Development Intro | Red-green-refactor, writing tests first |

---

### Module 9: Build Tools & Project Structure (4 lessons) - NEW

| Lesson | Topic | Content |
|--------|-------|---------|
| 9.1 | Why Build Tools? | Manual compilation pain, dependency management problem |
| 9.2 | Maven Fundamentals | `pom.xml`, dependencies, lifecycle (compile, test, package) |
| 9.3 | Gradle Basics | `build.gradle`, Groovy vs Kotlin DSL, tasks |
| 9.4 | Project Structure & Packaging | `src/main/java`, `src/test/java`, building JARs, running apps |

**Note:** Maven as primary, Gradle equivalents in sidebars.

---

### Module 10: Databases & JPA (6 lessons) - EXPAND

| Lesson | Topic | Content |
|--------|-------|---------|
| 10.1 | Relational Database Concepts | Tables, rows, columns, primary/foreign keys, relationships |
| 10.2 | SQL Fundamentals | SELECT, INSERT, UPDATE, DELETE, WHERE, JOIN |
| 10.3 | JDBC Basics | Connections, PreparedStatement, ResultSet, SQL injection prevention |
| 10.4 | Spring Data JPA Introduction | Entities, `@Entity`, `@Id`, repositories |
| 10.5 | Relationships & Queries | `@OneToMany`, `@ManyToOne`, JPQL, derived query methods |
| 10.6 | Transactions & Migrations | `@Transactional`, Flyway migrations, schema versioning |

---

### Module 11: Spring Boot & REST APIs (9 lessons) - UPDATE

| Lesson | Topic | Content |
|--------|-------|---------|
| 11.1 | What is Spring Boot? | Auto-configuration, Spring Initializr, first app |
| 11.2 | Dependency Injection | `@Component`, `@Service`, `@Repository`, `@Autowired` |
| 11.3 | Configuration & Profiles | `application.properties`, `@Value`, `@ConfigurationProperties`, profiles |
| 11.4 | REST Controllers | `@RestController`, `@GetMapping`, `@PostMapping`, path variables |
| 11.5 | Request/Response Handling | DTOs, `@RequestBody`, `@ResponseBody`, JSON serialization |
| 11.6 | Validation | Bean Validation 3.0, `@Valid`, `@NotNull`, `@Size`, error messages |
| 11.7 | Exception Handling | `@ControllerAdvice`, `@ExceptionHandler`, Problem Details (RFC 7807) |
| 11.8 | Testing Spring Boot | `@WebMvcTest`, `@DataJpaTest`, `MockMvc`, Testcontainers |
| 11.9 | Actuator & Observability | Health checks, metrics, structured logging |

---

### Module 12: Security - Sessions & JWT (5 lessons) - NEW

| Lesson | Topic | Content |
|--------|-------|---------|
| 12.1 | Web Security Fundamentals | Authentication vs authorization, common attacks (XSS, CSRF, SQL injection) |
| 12.2 | Spring Security Basics | Security filter chain, `@EnableWebSecurity`, protecting endpoints |
| 12.3 | Session-Based Authentication | Form login, session management, remember-me, logout |
| 12.4 | JWT Authentication | Token structure, stateless auth, `Authorization` header, refresh tokens |
| 12.5 | Role-Based Access Control | `@PreAuthorize`, method-level security, roles vs permissions |

---

### Module 13: React Frontend Integration (6 lessons) - NEW

| Lesson | Topic | Content |
|--------|-------|---------|
| 13.1 | Frontend Fundamentals | HTML/CSS/JS review, how browsers work, DOM basics |
| 13.2 | React Introduction | Components, JSX, props, Vite project setup |
| 13.3 | State & Events | `useState`, handling clicks/forms, controlled inputs |
| 13.4 | Fetching Data from APIs | `fetch()`, `useEffect()`, loading/error states, CORS configuration |
| 13.5 | React Router Basics | Routes, navigation, URL parameters |
| 13.6 | Connecting to Spring Boot | Full integration: login flow, protected routes, JWT in headers |

**Tooling:** Vite + React 19, no TypeScript (mention as "next step").

---

### Module 14: DevOps & Deployment (5 lessons) - NEW

| Lesson | Topic | Content |
|--------|-------|---------|
| 14.1 | Why DevOps? | Dev vs Ops, CI/CD concept, deployment pain points |
| 14.2 | Docker Fundamentals | Images, containers, `Dockerfile`, building Spring Boot images |
| 14.3 | Docker Compose | Multi-container setup (app + Postgres), environment variables |
| 14.4 | GitHub Actions CI/CD | Workflow files, running tests on push, building artifacts |
| 14.5 | Cloud Deployment | Deploy to Railway, environment config, health checks, monitoring basics |

---

### Module 15: Capstone - Task Manager (9 lessons) - RESTRUCTURE

| Lesson | Topic | Deliverable |
|--------|-------|-------------|
| 15.1 | Project Planning | Requirements, wireframes, data model design |
| 15.2 | Backend Setup | Spring Boot project, entities, repositories, Flyway migrations |
| 15.3 | REST API Implementation | CRUD endpoints for tasks, users, categories |
| 15.4 | Authentication Integration | JWT auth, user registration/login, protected endpoints |
| 15.5 | Business Logic & Validation | Due dates, priorities, task assignment, validation rules |
| 15.6 | React Frontend | Task list, create/edit forms, filtering, user dashboard |
| 15.7 | Frontend-Backend Integration | Full auth flow, API consumption, error handling |
| 15.8 | Testing & Quality | Unit tests, integration tests, test coverage goals |
| 15.9 | Deployment & Launch | Dockerize, CI/CD pipeline, deploy to Railway, production config |

---

## Content Quality Standards

Every lesson must meet these criteria:

| Requirement | Implementation |
|-------------|----------------|
| **Complete understanding** | No "we'll cover this later" - each concept fully explained when introduced |
| **No stubs/placeholders** | All code examples are complete, runnable, tested |
| **Dual syntax** | Modern Java 23+ shown first, LTS equivalent in collapsible block |
| **Real-world context** | Every concept tied to practical use case |
| **Challenges** | Each lesson has 2-3 hands-on coding challenges with full solutions |

---

## Web Research Required Per Module

Before writing/updating each module, research these sources:

| Module | Research Topics |
|--------|-----------------|
| 1-2 | Java 21/23 syntax differences, JEP documents for preview features |
| 3 | Git 2.x best practices, GitHub 2025 workflows |
| 4 | Records (JEP 395), Sealed Classes (JEP 409), pattern matching updates |
| 5 | Sequenced Collections (JEP 431), Collections best practices |
| 6 | Stream API updates, Optional patterns |
| 7 | Virtual Threads (JEP 444), Project Loom current state |
| 8 | JUnit 5.10+, Mockito 5.x, AssertJ patterns |
| 9 | Maven 3.9+, Gradle 8.x current features |
| 10 | Spring Data JPA 3.x, Flyway 10.x, Hibernate 6.x |
| 11 | Spring Boot 3.4.x release notes, Problem Details RFC 7807 |
| 12 | Spring Security 6.x, JWT best practices 2025, OWASP guidelines |
| 13 | React 19 features, Vite 5.x, fetch API patterns |
| 14 | Docker best practices, GitHub Actions syntax, Railway deployment docs |
| 15 | Full-stack integration patterns, production checklist |

---

## Execution Phases

### Phase 1: Update Existing Modules (Modules 1-2, 4-5, 8, 10-11)
- Add dual syntax presentation
- Verify code against Java 21 LTS
- Fill content gaps identified in Perplexity audit

### Phase 2: Create New Modules (Modules 3, 6, 7, 9, 12, 13, 14)
- Full lesson content from scratch
- Complete challenges with solutions
- Web research for each topic

### Phase 3: Restructure Capstone (Module 15)
- Redesign as Task Manager project
- Integrate all learned concepts
- End-to-end deployment

### Phase 4: Quality Assurance
- All code examples tested
- Cross-module consistency check
- Estimated hours recalculated

---

## Research Sources

- [Java 21 Features - Pretius](https://pretius.com/blog/java-21-features)
- [Java 21 Best Practices - JavaGuides](https://medium.com/javaguides/top-10-java-21-best-practices-with-complete-examples-97ebec502e00)
- [Spring Boot 3.4 Release Notes](https://github.com/spring-projects/spring-boot/wiki/Spring-Boot-3.4-Release-Notes)
- [Spring Boot 3.x Features - Dan Vega](https://www.danvega.dev/blog/spring-boot-3-features)
- [React 19 Features - WEQ](https://weqtechnologies.com/react-19-features-updates-2025-whats-new-why-it-matters/)
- [React + Spring Boot Integration - Bootify](https://bootify.io/frontend/react-spring-boot-integration.html)
- [JUnit 5 Spring Boot Testing - Medium](https://medium.com/the-modern-backend/bulletproof-your-code-unit-testing-spring-boot-with-junit-5-mockito-2025-interview-guide-c0bd57c84419)
- [Railway vs Render vs Fly.io - Northflank](https://northflank.com/blog/railway-vs-render)

---

## Success Criteria

- [ ] All 15 modules complete with full lesson content
- [ ] All code examples verified against Java 21 LTS
- [ ] Dual syntax (modern + traditional) in every code block
- [ ] Spring Boot 3.4.x patterns used throughout
- [ ] React 19 with Vite for frontend module
- [ ] Complete Task Manager capstone deployed to Railway
- [ ] All challenges have working solutions
- [ ] Estimated hours recalculated to ~80-90 total
- [ ] No stubs, placeholders, or TODOs in any lesson
