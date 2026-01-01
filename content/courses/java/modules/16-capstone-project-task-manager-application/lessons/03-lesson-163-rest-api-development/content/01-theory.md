---
type: "THEORY"
title: "REST API Design Principles"
---

A well-designed REST API follows consistent conventions that make it intuitive for frontend developers and other API consumers. Let us establish our design principles.

URL Structure:
Resources are nouns (not verbs), plural form:
- /api/tasks (not /api/getTask or /api/task)
- /api/categories (not /api/category)
- /api/users (not /api/user)

HTTP Methods define actions:
- GET /api/tasks - List all tasks (for current user)
- GET /api/tasks/123 - Get specific task
- POST /api/tasks - Create new task
- PUT /api/tasks/123 - Update entire task
- PATCH /api/tasks/123 - Partial update
- DELETE /api/tasks/123 - Delete task

HTTP Status Codes communicate results:
- 200 OK: Successful GET, PUT, PATCH
- 201 Created: Successful POST (include Location header)
- 204 No Content: Successful DELETE
- 400 Bad Request: Invalid input data
- 401 Unauthorized: Not authenticated
- 403 Forbidden: Authenticated but not authorized
- 404 Not Found: Resource does not exist
- 409 Conflict: Duplicate resource (e.g., email exists)
- 500 Internal Server Error: Server-side failure

Request/Response Format:
- Always JSON (Content-Type: application/json)
- Use camelCase for field names (not snake_case)
- Include metadata for paginated responses
- Never expose internal entity IDs in URLs for security-sensitive operations