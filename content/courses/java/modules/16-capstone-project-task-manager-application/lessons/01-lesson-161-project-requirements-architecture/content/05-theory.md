---
type: "THEORY"
title: "Project Structure and Module Organization"
---

A well-organized project structure makes code easier to navigate, maintain, and scale. Here is how we will organize our Task Manager:

Backend Structure (Spring Boot):
taskmanager-api/
  src/main/java/com/taskmanager/
    TaskManagerApplication.java       # Main entry point
    config/                            # Configuration classes
      SecurityConfig.java              # Spring Security setup
      WebConfig.java                   # CORS, converters
    controller/                        # REST endpoints
      AuthController.java              # Login, register, logout
      TaskController.java              # Task CRUD operations
      CategoryController.java          # Category CRUD operations
    service/                           # Business logic
      AuthService.java                 # Authentication logic
      TaskService.java                 # Task operations
      CategoryService.java             # Category operations
    repository/                        # Database access
      UserRepository.java
      TaskRepository.java
      CategoryRepository.java
    model/                             # JPA entities
      User.java
      Task.java
      Category.java
      enums/                           # Enumerations
        Role.java
        TaskStatus.java
        Priority.java
    dto/                               # Data transfer objects
      request/                         # Incoming data
        TaskRequest.java
        LoginRequest.java
      response/                        # Outgoing data
        TaskResponse.java
        UserResponse.java
    exception/                         # Custom exceptions
      ResourceNotFoundException.java
      UnauthorizedException.java
    security/                          # JWT and auth
      JwtTokenProvider.java
      JwtAuthenticationFilter.java
  src/main/resources/
    application.yml                    # Configuration
    db/migration/                      # Flyway migrations
      V1__create_users.sql
      V2__create_categories.sql
      V3__create_tasks.sql

Frontend Structure (React):
taskmanager-ui/
  src/
    components/                        # Reusable UI components
      TaskCard.tsx
      CategoryBadge.tsx
      Header.tsx
    pages/                             # Page components
      LoginPage.tsx
      DashboardPage.tsx
      TasksPage.tsx
    hooks/                             # Custom React hooks
      useAuth.ts
      useTasks.ts
    services/                          # API communication
      api.ts
      authService.ts
      taskService.ts
    types/                             # TypeScript interfaces
      Task.ts
      User.ts
    context/                           # React context
      AuthContext.tsx

This structure follows industry best practices: separation by feature type, clear naming conventions, and logical grouping of related files.