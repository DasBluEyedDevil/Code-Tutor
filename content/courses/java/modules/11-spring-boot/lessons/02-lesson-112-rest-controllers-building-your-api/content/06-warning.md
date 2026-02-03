---
type: "WARNING"
title: "REST Controller Best Practices"
---

AVOID COMMON CONTROLLER MISTAKES:

1. CONTROLLER SHOULD BE THIN
   - No business logic in controllers
   - Delegate to service layer
   - Controllers only handle HTTP concerns

2. USE DTOs (Data Transfer Objects)
   - Never expose entities directly
   - DTOs hide internal structure
   - Separate input/output DTOs if needed

3. PROPER HTTP STATUS CODES
   - 201 for resource creation (not 200)
   - 204 for successful deletion
   - 400 for validation errors
   - 404 for not found

4. VALIDATION IS REQUIRED
   - Always use @Valid on @RequestBody
   - Bean Validation annotations on DTOs
   - Custom validators for complex rules

5. EXCEPTION HANDLING (Spring Boot 4.0+)
   - Use @RestControllerAdvice globally
   - Never expose stack traces
   - Enable RFC 7807 Problem Details:
     spring.mvc.problemdetails.enabled=true
   - Return ProblemDetail objects for errors
   - Standard format: type, title, status, detail, instance