---
type: "THEORY"
title: "The Problem: Ugly Error Messages"
---

Without proper exception handling:

@GetMapping("/users/{id}")
public User getUser(@PathVariable Long id) {
    User user = userRepository.findById(id).orElse(null);
    return user;  // What if user is null?
}

When user doesn't exist:
{
  "timestamp": "2025-01-15T10:30:00",
  "status": 500,
  "error": "Internal Server Error",
  "trace": "java.lang.NullPointerException\n\tat..."
}

PROBLEMS:
❌ Exposes stack traces (security risk!)
❌ Wrong status code (500 instead of 404)
❌ Unhelpful message for clients
❌ No consistent error format

Professional APIs need:
✓ Clear, consistent error messages
✓ Correct HTTP status codes
✓ No sensitive data exposed
✓ Structured JSON responses