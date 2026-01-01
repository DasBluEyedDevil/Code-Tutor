---
type: "THEORY"
title: "⚠️ Security: Never Expose Stack Traces!"
---

❌ BAD - Exposing internal details:

@ExceptionHandler(Exception.class)
public ResponseEntity<String> handleError(Exception ex) {
    return ResponseEntity.status(500)
        .body("Error: " + ex.getMessage() + "\n" + 
              Arrays.toString(ex.getStackTrace()));
}

This exposes:
- Internal class names
- File paths
- Database structure
- Library versions
→ Helps attackers!

✓ GOOD - Safe error handling:

@ExceptionHandler(Exception.class)
public ResponseEntity<ErrorResponse> handleError(Exception ex) {
    // Log full details for developers (server-side only)
    logger.error("Unexpected error", ex);
    
    // Return safe, generic message to client
    ErrorResponse error = new ErrorResponse(
        500,
        "An unexpected error occurred",
        System.currentTimeMillis()
    );
    return ResponseEntity.status(500).body(error);
}

BEST PRACTICES:
✓ Log full errors server-side (for debugging)
✓ Return generic messages to clients
✓ Use error codes/IDs to correlate logs
✓ Never expose paths, SQL, or stack traces