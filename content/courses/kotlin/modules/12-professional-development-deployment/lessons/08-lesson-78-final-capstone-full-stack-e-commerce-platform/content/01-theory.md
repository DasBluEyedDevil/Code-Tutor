---
type: "THEORY"
title: "Capstone Project: TaskFlow KMP"
---

**Estimated Time**: 12-16 hours

Welcome to the final capstone project. You will build **TaskFlow** -- a full-stack task management application using Kotlin Multiplatform. This project ties together every major topic from the course into a single, cohesive, production-style application.

TaskFlow has three modules that mirror real KMP project structure:

| Module | Technology | Purpose |
|--------|-----------|---------|
| `server/` | Ktor 3.4 + Exposed 1.0 + H2 | REST API with JWT auth |
| `shared/` | commonMain + kotlinx-serialization | Domain models, repository interfaces, DTOs |
| `composeApp/` | Compose Multiplatform 1.10 | Material 3 UI for Android and Desktop |

The application supports user registration and login (JWT), full task CRUD with categories and priorities, an offline-first SQLDelight cache on the client, and a synchronized server-side database via Exposed.

### Why This Architecture?

Every layer maps to something you learned in the course:

- **Modules 01-04**: Kotlin fundamentals, OOP, FP, and coroutines power every layer
- **Module 05**: Coroutines and Flows drive the reactive data pipeline
- **Module 06**: Ktor server routes, Exposed database tables, Koin DI
- **Module 07**: Compose Multiplatform UI with Material 3
- **Module 08**: SQLDelight for the shared cache, expect/actual for platform drivers
- **Module 09**: KMP architecture with shared domain layer
- **Module 10**: Koin dependency injection across server and client
- **Module 11**: kotlin.test, runTest, and MockK for testing
- **Module 12**: CI/CD, deployment, and this capstone itself

### No External Services Required

TaskFlow uses **H2 embedded database** on the server side. There is no PostgreSQL to install, no Docker required for development, and no cloud accounts needed. Run the server with a single Gradle command and the database creates itself in memory (or as a file for persistence).

---

