---
type: "KEY_POINT"
title: "Architecture Overview: Spring Boot + PostgreSQL (Two Frontend Paths)"
---

Our application follows the modern three-tier architecture pattern. The backend and database tiers are shared between both frontend paths.

Presentation Tier (Frontend -- Choose One):

Thymeleaf Path (Server-Side Rendering):
- Technology: Spring MVC + Thymeleaf templates
- Responsibility: Server-rendered HTML pages with dynamic data
- Communicates with: Backend directly through Spring MVC controllers
- Features: Template-based UI, form binding, fragments for layout reuse, zero JavaScript required

React Path (Client-Side Rendering):
- Technology: React 19.x with JavaScript
- Responsibility: Single-page application with dynamic UI
- Communicates with: Backend via REST API (HTTP/JSON)
- Features: Component-based UI, state management, form validation, responsive design

Application Tier (Backend -- Shared):
- Technology: Spring Boot 4.0.x with Java 25
- Responsibility: Business logic, security, data validation, API endpoints
- Components:
  - Controllers: Handle HTTP requests, route to services
  - Services: Implement business logic, coordinate operations
  - Repositories: Database access layer (Spring Data JPA)
  - DTOs: Data transfer objects for API requests/responses
  - Security: JWT authentication (React path) or session-based auth (Thymeleaf path), authorization rules

Data Tier (Database -- Shared):
- Technology: PostgreSQL 17
- Responsibility: Persistent data storage, data integrity, queries
- Managed by: Flyway for schema migrations
- Accessed by: Spring Data JPA repositories

Communication Flow (Thymeleaf Path):
1. User submits a form or clicks a link in the browser
2. Spring MVC controller receives the request
3. Spring Security validates the session
4. Controller calls service layer for business logic
5. Service queries/updates PostgreSQL via repositories
6. Controller adds data to the Model
7. Thymeleaf renders the template with the model data
8. Server returns complete HTML page to browser

Communication Flow (React Path):
1. User interacts with React UI
2. React sends HTTP request to Spring Boot API
3. Spring Security validates JWT token
4. Controller receives request, validates input
5. Service executes business logic
6. Repository queries/updates PostgreSQL
7. Response flows back through layers to React
8. React updates UI with new data

This separation of concerns makes our application maintainable, testable, and scalable. The backend is identical for both paths -- only the presentation tier differs.
