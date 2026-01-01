---
type: "THEORY"
title: "RFC 7807 Problem Details - Industry Standard Errors (Spring Boot 3.4+)"
---

Spring Boot 3.4+ supports RFC 7807 Problem Details out of the box!

ENABLE IN application.properties:
spring.mvc.problemdetails.enabled=true

Now Spring automatically returns standardized error responses:

{
  "type": "about:blank",
  "title": "Not Found",
  "status": 404,
  "detail": "User not found with id: 999",
  "instance": "/api/users/999"
}

RFC 7807 FIELDS:
- type: URI identifying the problem type
- title: Short human-readable summary
- status: HTTP status code
- detail: Detailed explanation
- instance: URI identifying the specific occurrence

CUSTOM ProblemDetail IN EXCEPTION HANDLER:

@ExceptionHandler(UserNotFoundException.class)
public ProblemDetail handleNotFound(UserNotFoundException ex) {
    ProblemDetail problem = ProblemDetail.forStatusAndDetail(
        HttpStatus.NOT_FOUND, ex.getMessage());
    problem.setTitle("User Not Found");
    problem.setType(URI.create("https://api.example.com/errors/user-not-found"));
    problem.setProperty("userId", ex.getUserId());  // Custom fields!
    return problem;
}

BENEFITS:
- Industry standard format
- Clients know what to expect
- Extensible with custom properties
- Content-Type: application/problem+json