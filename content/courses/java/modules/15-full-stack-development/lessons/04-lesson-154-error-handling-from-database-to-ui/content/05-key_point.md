---
type: "KEY_POINT"
title: "Security: Never Expose Stack Traces!"
---

❌ BAD - Exposes internal details:
{
  "error": "java.sql.SQLException: Duplicate entry 'alice@email.com'",
  "stackTrace": [
    "at com.example.UserRepository.save(UserRepository.java:42)",
    "at com.example.UserService.create(UserService.java:28)",
    ...
  ]
}

SECURITY RISKS:
- Reveals database structure
- Shows internal file paths
- Exposes technology stack
- Helps attackers understand your system

✓ GOOD - User-friendly message:
{
  "status": 409,
  "title": "Duplicate Resource",
  "detail": "This email address is already registered",
  "errorCode": "DUPLICATE_RESOURCE",
  "timestamp": "2025-01-15T10:30:00Z"
}

Meanwhile, on the SERVER:
LOG: Full stack trace for developers
LOG: Request details, user ID, timestamp
LOG: SQL query that failed

Implementation:

@RestControllerAdvice
public class GlobalExceptionHandler {
    
    private static final Logger logger = 
        LoggerFactory.getLogger(GlobalExceptionHandler.class);
    
    @ExceptionHandler(Exception.class)
    public ProblemDetail handleGenericException(
            Exception ex, HttpServletRequest request) {
        
        // LOG everything for developers
        logger.error("Unexpected error at {}: {}", 
                    request.getRequestURI(), ex.getMessage(), ex);
        
        // RETURN safe message to user
        ProblemDetail problem = ProblemDetail.forStatusAndDetail(
            HttpStatus.INTERNAL_SERVER_ERROR,
            "An unexpected error occurred. Please try again later."
        );
        problem.setTitle("Internal Server Error");
        
        // Include error ID for support team
        String errorId = UUID.randomUUID().toString();
        problem.setProperty("errorId", errorId);
        logger.error("Error ID: {}", errorId);
        
        return problem;
    }
}