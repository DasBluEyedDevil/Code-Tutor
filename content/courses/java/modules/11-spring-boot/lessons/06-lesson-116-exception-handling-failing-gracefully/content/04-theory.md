---
type: "THEORY"
title: "@RestControllerAdvice - Global Exception Handler"
---

Centralize ALL exception handling in one place:

@RestControllerAdvice
public class GlobalExceptionHandler {
    
    // Handle ResourceNotFoundException
    @ExceptionHandler(ResourceNotFoundException.class)
    public ResponseEntity<ErrorResponse> handleNotFound(ResourceNotFoundException ex) {
        ErrorResponse error = new ErrorResponse(
            HttpStatus.NOT_FOUND.value(),
            ex.getMessage(),
            System.currentTimeMillis()
        );
        return ResponseEntity.status(HttpStatus.NOT_FOUND).body(error);
    }
    
    // Handle InvalidRequestException
    @ExceptionHandler(InvalidRequestException.class)
    public ResponseEntity<ErrorResponse> handleBadRequest(InvalidRequestException ex) {
        ErrorResponse error = new ErrorResponse(
            HttpStatus.BAD_REQUEST.value(),
            ex.getMessage(),
            System.currentTimeMillis()
        );
        return ResponseEntity.badRequest().body(error);
    }
    
    // Handle all other exceptions (fallback)
    @ExceptionHandler(Exception.class)
    public ResponseEntity<ErrorResponse> handleGeneric(Exception ex) {
        // Log full error for developers
        // Log full error for developers (use a real logger in production)
        IO.println("Unexpected error: " + ex.getMessage());
        
        // Return safe message to client
        ErrorResponse error = new ErrorResponse(
            HttpStatus.INTERNAL_SERVER_ERROR.value(),
            "An unexpected error occurred. Please try again later.",
            System.currentTimeMillis()
        );
        return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(error);
    }
}

@RestControllerAdvice:
- Applies to ALL @RestController classes
- Catches exceptions from any controller
- Returns consistent error format