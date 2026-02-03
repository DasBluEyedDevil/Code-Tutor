# Phase 02 Plan 07: Module 16 Capstone Dual-Path Restructure Summary

**One-liner:** Capstone restructured with dual Thymeleaf/React frontend paths, self-contained Thymeleaf tutorial in Lesson 06, all content targeting Spring Boot 4.0.x/Java 25 with Railway deployment

---

## Metadata

| Field | Value |
|-------|-------|
| Phase | 02-java-course-audit |
| Plan | 07 |
| Duration | ~15 min |
| Completed | 2026-02-02 |
| Subsystem | content/courses/java/modules/16-capstone-project-task-manager-application |
| Tags | capstone, thymeleaf, react, spring-boot-4, dual-path, railway, docker |

## Dependency Graph

- **Requires:** 02-01 (version targets), 02-02 (IO.println patterns), 02-03 (OOP patterns), 02-04 (testing patterns), 02-05 (Spring Security patterns), 02-06 (Docker/deployment patterns)
- **Provides:** Complete dual-path capstone module with self-contained Thymeleaf tutorial
- **Affects:** 02-08 (final verification pass)

## Tasks Completed

| Task | Name | Commit | Files Changed |
|------|------|--------|---------------|
| 1 | Update capstone backend lessons to Spring Boot 4.0.x, add dual-path introduction | 223afbac | 16 files |
| 2 | Create dual-path capstone with Thymeleaf tutorial and React path | ebbef404 | 26 files |

## Key Changes

### Task 1: Backend Lessons + Dual-Path Introduction

**Lesson 01 (Requirements/Architecture):**
- Added "Choose Your Frontend Path" introduction explaining Thymeleaf (simpler) vs React (advanced)
- Architecture overview rewritten showing both presentation tiers sharing one Spring Boot backend
- Project structure updated to show both `src/main/resources/templates/` (Thymeleaf) and separate `frontend/` (React)

**Lesson 02 (Backend Setup):**
- Spring Boot 3.2.x updated to 4.0.x, Java 21 to 25
- Artifact name normalized to `taskmanager`
- PostgreSQL updated to 17-alpine

**Lesson 04 (Authentication):**
- Security configuration rewritten to remove version-tagged framing ("Spring Security 6")
- Reframed as "Spring Security Configuration Overview" for Spring Boot 4.0.x

**Lesson 08 (Testing):**
- @MockBean replaced with @MockitoBean throughout (3 annotations + import)
- PostgreSQL Testcontainers updated to postgres:17

**Lesson 09 (Deployment):**
- Dockerfile: gradle:8.12-jdk25, eclipse-temurin:25-jre-alpine
- Docker Compose: postgres:17-alpine
- CI/CD: JDK 25, postgres:17
- Challenge files updated with matching versions

### Task 2: Dual-Path Frontend Restructure (Lessons 06-09)

**Lesson 06 -- "Frontend Development: Choose Your Path":**
- 01-theory: Two approaches overview with comparison
- 02-theory: Thymeleaf fundamentals (th:text, th:each, th:if/th:unless, th:href, th:src)
- 03-theory: Thymeleaf forms (th:action, th:object, th:field, th:errors, th:fragment, th:classappend)
- 04-theory: Building Task Manager pages with Thymeleaf (full task list + Spring MVC controller)
- 05-theory: React path setup (Vite + React 19, Axios service layer with interceptors)
- 06-theory: React components (TaskList, TaskForm, AuthContext)
- 07-theory: React routing (BrowserRouter, ProtectedRoute)
- 08-key_point: Path comparison summary

**Lesson 07 -- "Connecting Frontend to Backend":**
- 01-theory: Thymeleaf MVC form handling, CSRF, validation, session-based SecurityFilterChain
- 02-theory: Thymeleaf login/registration pages with AuthViewController
- 03-theory: React CORS config, JWT flow, LoginPage.jsx
- 04-theory: React CRUD operations, optimistic updates, error handling
- 05-theory: Shared dashboard page (both paths)
- 06-theory: Shared loading states and UX patterns

**Lesson 08 -- "Testing the Application":**
- Added Thymeleaf view controller testing with MockMvc (view().name, model().attributeExists, form param tests)
- React testing section marked as "REACT PATH" with skip note for Thymeleaf users
- E2E testing section marked as "BOTH PATHS"

**Lesson 09 -- "Deployment to Production":**
- Dockerfile section notes Thymeleaf single-JAR advantage
- Docker Compose section explains 2-service (Thymeleaf) vs 3-service (React) configuration
- CI/CD pipeline notes which jobs to remove for Thymeleaf path
- Railway walkthrough notes deployment service count difference

**Module metadata:**
- module.json description updated to mention both paths
- All lesson.json descriptions updated for dual-path awareness

## Decisions Made

| Decision | Rationale |
|----------|-----------|
| Both paths in same lesson files (not separate directories) | Preserves linear lesson structure; students read only their path's sections |
| Thymeleaf tutorial is self-contained within Lesson 06 | Students can choose Thymeleaf without prior exposure; all syntax taught in-context while building |
| "BOTH PATHS" / "THYMELEAF PATH" / "REACT PATH" headers | Clear section markers so students know which content applies to their choice |
| Thymeleaf single-JAR advantage highlighted in deployment | Key differentiator that makes Thymeleaf path simpler for beginners |

## Deviations from Plan

None -- plan executed exactly as written.

## Tech Stack

- **Added patterns:** Thymeleaf (th:text, th:each, th:if, th:action, th:object, th:field, th:fragment, th:classappend), MockMvc view testing
- **Updated:** Spring Boot 4.0.x, Java 25, PostgreSQL 17, eclipse-temurin:25, Gradle 8.12, @MockitoBean

## Verification Results

| Check | Result |
|-------|--------|
| System.out.println in Lessons 06-09 | 0 occurrences |
| th:each in module | 5 occurrences |
| th:text in module | 20 occurrences |
| th:action in module | 10 occurrences |
| th:object in module | 4 occurrences |
| th:if in module | 12 occurrences |
| th:fragment in module | 3 occurrences |
| Thymeleaf mentions | 61 occurrences across 25 files |
| React mentions | 54 occurrences across 27 files |
| eclipse-temurin:25 | 2 occurrences |
| @MockBean (should be 0) | 0 occurrences |
| JSON validation | All challenge.json, lesson.json, module.json valid |

## Next Phase Readiness

Plan 02-08 (final verification pass) can proceed. All Module 16 content is now complete with dual-path structure. The capstone module satisfies JAVA-04 (deployable project) for both Thymeleaf and React paths.
