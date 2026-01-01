---
type: "KEY_POINT"
title: "Architecture Overview: React + Spring Boot + PostgreSQL"
---

Our application follows the modern three-tier architecture pattern:

Presentation Tier (Frontend):
- Technology: React 18+ with TypeScript
- Responsibility: User interface and user experience
- Communicates with: Backend via REST API (HTTP/JSON)
- Features: Component-based UI, state management, form validation, responsive design

Application Tier (Backend):
- Technology: Spring Boot 3.2+ with Java 21+
- Responsibility: Business logic, security, data validation, API endpoints
- Components:
  - Controllers: Handle HTTP requests, route to services
  - Services: Implement business logic, coordinate operations
  - Repositories: Database access layer (Spring Data JPA)
  - DTOs: Data transfer objects for API requests/responses
  - Security: JWT authentication, authorization rules

Data Tier (Database):
- Technology: PostgreSQL 16
- Responsibility: Persistent data storage, data integrity, queries
- Managed by: Flyway for schema migrations
- Accessed by: Spring Data JPA repositories

Communication Flow:
1. User interacts with React UI
2. React sends HTTP request to Spring Boot API
3. Spring Security validates JWT token
4. Controller receives request, validates input
5. Service executes business logic
6. Repository queries/updates PostgreSQL
7. Response flows back through layers to React
8. React updates UI with new data

This separation of concerns makes our application maintainable, testable, and scalable. Each tier can be developed, tested, and deployed independently.