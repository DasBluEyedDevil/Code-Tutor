---
type: "THEORY"
title: "⚠️ Common Error Handling Mistakes"
---

❌ MISTAKE 1: Catching and ignoring exceptions
try {
    userService.create(user);
} catch (Exception e) {
    // Nothing here - ERROR VANISHES!
}

❌ MISTAKE 2: Generic error messages
displayError("Error occurred");  // What error? How to fix?

✓ FIX: Be specific
displayError("Email already exists. Please use a different email.");

❌ MISTAKE 3: Not validating on both sides
// Only frontend validation - can be bypassed!
// Only backend validation - bad UX (slow feedback)

✓ FIX: Validate on BOTH frontend and backend

❌ MISTAKE 4: Returning 200 OK with error
return ResponseEntity.ok(Map.of("error", "User not found"));

✓ FIX: Use proper status codes
return ResponseEntity.status(HttpStatus.NOT_FOUND)
    .body(problemDetail);

❌ MISTAKE 5: Not logging errors
// Error happens, no log, no way to debug

✓ FIX: Always log exceptions
logger.error("Failed to create user: {}", ex.getMessage(), ex);