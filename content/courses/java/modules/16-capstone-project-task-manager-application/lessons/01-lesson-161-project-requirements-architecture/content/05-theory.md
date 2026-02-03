---
type: "THEORY"
title: "Project Structure and Module Organization"
---

A well-organized project structure makes code easier to navigate, maintain, and scale. Here is how we will organize our Task Manager:

Backend Structure (Spring Boot -- Shared by Both Paths):
taskmanager/
  src/main/java/com/taskmanager/
    TaskManagerApplication.java       # Main entry point
    config/                            # Configuration classes
      SecurityConfig.java              # Spring Security setup
      WebConfig.java                   # CORS, converters
    controller/                        # REST endpoints (and MVC controllers for Thymeleaf path)
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
    templates/                         # Thymeleaf templates (Thymeleaf path only)
      layout.html
      tasks/list.html
      tasks/form.html

Frontend Structure -- Thymeleaf Path:
The Thymeleaf path keeps everything inside the Spring Boot project. Templates live in src/main/resources/templates/ and static assets in src/main/resources/static/. There is no separate frontend project to build or deploy.

Frontend Structure -- React Path:
taskmanager-ui/
  src/
    components/                        # Reusable UI components
      TaskCard.jsx
      CategoryBadge.jsx
      Header.jsx
    pages/                             # Page components
      LoginPage.jsx
      DashboardPage.jsx
      TasksPage.jsx
    hooks/                             # Custom React hooks
      useAuth.js
      useTasks.js
    services/                          # API communication
      api.js
      authService.js
      taskService.js
    context/                           # React context
      AuthContext.jsx

This structure follows industry best practices: separation by feature type, clear naming conventions, and logical grouping of related files.
