---
type: "THEORY"
title: "Checkpoint Quiz"
---


Test your understanding of validation and error handling:

### Question 1
Where should business rule validation (like "email must be unique") primarily occur?

A) Client-side JavaScript
B) Route layer
C) Service layer
D) Repository layer

### Question 2
What HTTP status code should you return for a validation error like "email format is invalid"?

A) 200 OK
B) 400 Bad Request
C) 422 Unprocessable Entity
D) 500 Internal Server Error

### Question 3
What's the main benefit of accumulating validation errors instead of failing on the first error?

A) It makes the code run faster
B) It reduces server load
C) Users can fix all issues at once, improving UX
D) It's required by REST standards

### Question 4
What should you do when an unexpected exception occurs in production?

A) Return the full stack trace to the client for debugging
B) Log the detailed error server-side, return a generic message to client
C) Ignore it and return 200 OK
D) Crash the server to alert administrators

### Question 5
Why use a sealed class hierarchy for exceptions (ApiException subclasses)?

A) It makes the code look more professional
B) It enables type-safe, exhaustive error handling
C) It's required by Ktor
D) It improves performance

---

