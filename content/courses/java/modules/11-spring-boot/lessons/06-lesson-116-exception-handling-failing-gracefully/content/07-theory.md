---
type: "THEORY"
title: "Handling Validation Errors"
---

Catch validation exceptions in @RestControllerAdvice:

@ExceptionHandler(MethodArgumentNotValidException.class)
public ResponseEntity<ValidationErrorResponse> handleValidation(
        MethodArgumentNotValidException ex) {
    
    // Collect all validation errors
    List<String> errors = ex.getBindingResult()
        .getFieldErrors()
        .stream()
        .map(error -> error.getField() + ": " + error.getDefaultMessage())
        .toList();
    
    ValidationErrorResponse response = new ValidationErrorResponse(
        HttpStatus.BAD_REQUEST.value(),
        "Validation failed",
        errors,
        System.currentTimeMillis()
    );
    
    return ResponseEntity.badRequest().body(response);
}

ValidationErrorResponse class:

public class ValidationErrorResponse {
    private int status;
    private String message;
    private List<String> errors;
    private long timestamp;
    
    // Constructor, getters, setters
}

Response when validation fails:

{
  "status": 400,
  "message": "Validation failed",
  "errors": [
    "name: Name cannot be empty",
    "email: Invalid email format",
    "age: Must be at least 18 years old"
  ],
  "timestamp": 1705315800000
}