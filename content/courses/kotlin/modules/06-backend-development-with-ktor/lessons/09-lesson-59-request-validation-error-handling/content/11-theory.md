---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: C) Service layer**

The service layer is the perfect place for business rule validation:
- Route layer handles HTTP parsing
- Service layer knows business logic ("email must be unique" requires checking database)
- Repository layer is just data access

Client-side validation is for UX but can be bypassed, so never trust it alone.

---

**Question 2: B) 400 Bad Request**

HTTP status code guidelines:
- **400 Bad Request**: Invalid input format (malformed JSON, invalid email format)
- **422 Unprocessable Entity**: Valid format but violates business rules
- **409 Conflict**: Duplicate resource
- **500 Internal Server Error**: Unexpected server error

For format validation like email pattern matching, use 400.

---

**Question 3: C) Users can fix all issues at once, improving UX**

Compare these experiences:

**Fail-fast approach**:
1. Submit form → "Name is required"
2. Add name, submit → "Email is invalid"
3. Fix email, submit → "Password too short"
4. 😤 Three round trips!

**Accumulated errors**:
1. Submit form → Shows all three errors at once
2. Fix all issues, submit → Success!
3. 😊 One round trip!

---

**Question 4: B) Log the detailed error server-side, return a generic message to client**

Security and UX best practice:


Never expose stack traces or internal details—they can reveal vulnerabilities.

---

**Question 5: B) It enables type-safe, exhaustive error handling**

Using a sealed class hierarchy gives you compile-time safety:


This prevents bugs from unhandled exception types.

---



```kotlin
sealed class ApiException : Exception()
class ValidationException : ApiException()
class NotFoundException : ApiException()

// The compiler ensures you handle all cases
when (exception) {
    is ValidationException -> // handle
    is NotFoundException -> // handle
    // Compiler error if you forget a case!
}
```
