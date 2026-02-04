---
type: "THEORY"
title: "Solution Explanation"
---


### Why This Design Works

**1. Layered Validation**:
- **Format validation** in `ProductValidator` (structure, types, ranges)
- **Business rules** in `ProductService` (uniqueness, state transitions)
- **Database constraints** as last line of defense

**2. Accumulated Errors**:
Instead of failing on the first error, the validator collects all validation failures and returns them together. This provides better UX—users can fix multiple issues at once.

**3. Clear Error Taxonomy**:
- `ValidationException` (400): Bad input format
- `NotFoundException` (404): Resource doesn't exist
- `ConflictException` (409): Duplicate resource
- `BusinessRuleException` (422): Valid format but violates business logic

**4. Separation of Concerns**:
- **Validator**: Focuses purely on data format and constraints
- **Service**: Enforces business rules and orchestrates operations
- **Routes**: Handle HTTP concerns only
- **StatusPages**: Centralized error response formatting

**5. Type Safety with Result<T>**:
Using Kotlin's `Result<T>` type provides compile-time guarantees that errors are handled, preventing unhandled exceptions from reaching users.

---

