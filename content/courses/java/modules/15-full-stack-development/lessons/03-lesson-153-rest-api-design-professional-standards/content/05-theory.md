---
type: "THEORY"
title: "HTTP Status Codes - The Response Language"
---

2xx - SUCCESS:
200 OK - Request succeeded (GET, PUT, PATCH)
201 Created - Resource created (POST)
204 No Content - Success but no data to return (DELETE)

4xx - CLIENT ERRORS (User's fault):
400 Bad Request - Invalid data sent
401 Unauthorized - Not authenticated (no login)
403 Forbidden - Authenticated but not authorized
404 Not Found - Resource doesn't exist
409 Conflict - Duplicate or conflicting data
422 Unprocessable Entity - Validation failed
429 Too Many Requests - Rate limit exceeded

5xx - SERVER ERRORS (Our fault):
500 Internal Server Error - Something broke
503 Service Unavailable - Server down/overloaded

Example in Spring Boot:

@PostMapping("/api/users")
public ResponseEntity<User> create(@Valid @RequestBody User user) {
    if (userService.existsByEmail(user.getEmail())) {
        return ResponseEntity.status(HttpStatus.CONFLICT)
            .body(null);  // 409 Conflict
    }
    User saved = userService.save(user);
    return ResponseEntity
        .status(HttpStatus.CREATED)  // 201 Created
        .body(saved);
}