---
type: "THEORY"
title: "Exception Handling"
---

Handle errors gracefully:

@RestControllerAdvice
public class GlobalExceptionHandler {
    
    @ExceptionHandler(UserNotFoundException.class)
    public ResponseEntity<ErrorResponse> handleUserNotFound(UserNotFoundException ex) {
        ErrorResponse error = new ErrorResponse(
            "User not found",
            ex.getMessage()
        );
        return ResponseEntity.status(HttpStatus.NOT_FOUND).body(error);
    }
    
    @ExceptionHandler(IllegalArgumentException.class)
    public ResponseEntity<ErrorResponse> handleBadRequest(IllegalArgumentException ex) {
        ErrorResponse error = new ErrorResponse(
            "Invalid request",
            ex.getMessage()
        );
        return ResponseEntity.badRequest().body(error);
    }
}

When controller throws UserNotFoundException:
→ Handler catches it
→ Returns 404 with error message