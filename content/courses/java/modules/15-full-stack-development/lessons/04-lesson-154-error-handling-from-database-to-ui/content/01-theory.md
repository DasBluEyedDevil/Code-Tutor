---
type: "THEORY"
title: "The Full-Stack Error Journey"
---

Errors can occur at ANY layer:

DATABASE:
- Connection timeout
- Constraint violation (duplicate email)
- Transaction rollback

BACKEND (Spring Boot):
- Validation error (@NotBlank, @Email)
- Business logic error (insufficient funds)
- Resource not found (user ID doesn't exist)
- Authentication/authorization failure

NETWORK:
- 500 Internal Server Error
- 404 Not Found
- Timeout

FRONTEND (JavaScript):
- Network failure (no internet)
- JSON parsing error
- Display error to user

GOAL: Catch errors everywhere, provide clear feedback!