---
type: "THEORY"
title: "Validation Error Handling with MethodArgumentNotValidException"
---

When @Valid validation fails, Spring throws MethodArgumentNotValidException. Our exception handler converts this to a user-friendly response with field-specific error messages.

Validation Flow:
1. Client sends POST /api/tasks with invalid data
2. @Valid triggers Bean Validation
3. Validation fails on @NotBlank("title")
4. Spring throws MethodArgumentNotValidException
5. GlobalExceptionHandler catches it
6. Returns 400 Bad Request with error details

Enhanced Validation Error Response:
```java
@ExceptionHandler(MethodArgumentNotValidException.class)
public ProblemDetail handleValidationErrors(MethodArgumentNotValidException ex) {
    Map<String, Object> fieldErrors = new HashMap<>();
    
    for (FieldError error : ex.getBindingResult().getFieldErrors()) {
        String field = error.getField();
        String message = error.getDefaultMessage();
        Object rejectedValue = error.getRejectedValue();
        
        Map<String, Object> errorDetail = new HashMap<>();
        errorDetail.put("message", message);
        errorDetail.put("rejectedValue", rejectedValue);
        
        fieldErrors.put(field, errorDetail);
    }

    ProblemDetail problem = ProblemDetail.forStatusAndDetail(
        HttpStatus.BAD_REQUEST, 
        String.format("Validation failed for %d field(s)", fieldErrors.size()));
    problem.setTitle("Validation Error");
    problem.setType(URI.create("https://api.taskmanager.com/errors/validation"));
    problem.setProperty("timestamp", Instant.now());
    problem.setProperty("errors", fieldErrors);
    
    return problem;
}
```

Rich Error Response:
```json
{
  "type": "https://api.taskmanager.com/errors/validation",
  "title": "Validation Error",
  "status": 400,
  "detail": "Validation failed for 2 field(s)",
  "timestamp": "2024-01-15T10:30:00Z",
  "errors": {
    "title": {
      "message": "Title is required",
      "rejectedValue": null
    },
    "priority": {
      "message": "Invalid priority. Must be LOW, MEDIUM, HIGH, or URGENT",
      "rejectedValue": "SUPER_HIGH"
    }
  }
}
```

This format helps frontend developers:
- Identify which fields failed validation
- Display appropriate error messages to users
- Show what value was rejected for debugging