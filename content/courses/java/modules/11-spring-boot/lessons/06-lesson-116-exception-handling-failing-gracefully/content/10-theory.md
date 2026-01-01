---
type: "THEORY"
title: "Complete Exception Handling with ProblemDetail (Spring Boot 3.4+)"
---

```java
MODERN APPROACH: Use RFC 7807 ProblemDetail

1. Enable in application.properties:
spring.mvc.problemdetails.enabled=true

2. Custom Exceptions with ProblemDetail support:

public class UserNotFoundException extends RuntimeException {
    private final Long userId;
    
    public UserNotFoundException(Long id) {
        super("User not found with id: " + id);
        this.userId = id;
    }
    
    public Long getUserId() { return userId; }
}

3. Global Exception Handler with ProblemDetail:

@RestControllerAdvice
public class GlobalExceptionHandler {
    
    @ExceptionHandler(UserNotFoundException.class)
    public ProblemDetail handleUserNotFound(UserNotFoundException ex) {
        ProblemDetail problem = ProblemDetail.forStatusAndDetail(
            HttpStatus.NOT_FOUND, ex.getMessage());
        problem.setTitle("User Not Found");
        problem.setType(URI.create("https://api.example.com/errors/not-found"));
        problem.setProperty("userId", ex.getUserId());  // Custom field!
        return problem;
    }
    
    @ExceptionHandler(MethodArgumentNotValidException.class)
    public ProblemDetail handleValidation(MethodArgumentNotValidException ex) {
        ProblemDetail problem = ProblemDetail.forStatusAndDetail(
            HttpStatus.BAD_REQUEST, "Validation failed");
        problem.setTitle("Invalid Request Data");
        
        List<String> errors = ex.getBindingResult().getFieldErrors()
            .stream()
            .map(err -> err.getField() + ": " + err.getDefaultMessage())
            .toList();
        problem.setProperty("validationErrors", errors);
        return problem;
    }
}

4. Response format (automatic!):

{
  "type": "https://api.example.com/errors/not-found",
  "title": "User Not Found",
  "status": 404,
  "detail": "User not found with id: 999",
  "instance": "/api/users/999",
  "userId": 999
}

BENEFITS:
- Industry-standard RFC 7807 format
- Content-Type: application/problem+json
- Extensible with custom properties
- Works automatically when enabled
```