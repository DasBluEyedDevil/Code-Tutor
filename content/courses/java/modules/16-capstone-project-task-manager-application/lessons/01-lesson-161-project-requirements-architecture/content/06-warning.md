---
type: "WARNING"
title: "Common Architecture Mistakes to Avoid"
---

As you build this project, watch out for these common pitfalls:

Mistake 1: Exposing Entities Directly in API
Problem: Returning JPA entities from controllers exposes internal details (like password hashes) and creates tight coupling.
Solution: Always use DTOs for API requests and responses.

Mistake 2: Business Logic in Controllers
Problem: Putting validation and business rules in controllers makes them hard to test and reuse.
Solution: Controllers should only handle HTTP concerns. Move logic to services.

Mistake 3: N+1 Query Problem
Problem: Loading relationships lazily in a loop causes hundreds of database queries.
Solution: Use JOIN FETCH in JPQL queries or @EntityGraph for eager loading when needed.

Mistake 4: Storing Plain Text Passwords
Problem: If database is breached, all passwords are exposed.
Solution: Always hash passwords with BCrypt before storing.

Mistake 5: No Input Validation
Problem: Malicious or malformed data can corrupt your database or crash your application.
Solution: Use Bean Validation (@NotBlank, @Email, @Size) on all request DTOs.

Mistake 6: Hardcoding Configuration
Problem: Database URLs, secrets in code makes deployment difficult and insecure.
Solution: Use application.yml with environment variable substitution.

Mistake 7: No Error Handling
Problem: Unhandled exceptions expose stack traces to users (security risk) and provide poor UX.
Solution: Use @ControllerAdvice for global exception handling with proper error responses.

We will address all of these concerns as we build the application. Keep this list handy as a checklist.