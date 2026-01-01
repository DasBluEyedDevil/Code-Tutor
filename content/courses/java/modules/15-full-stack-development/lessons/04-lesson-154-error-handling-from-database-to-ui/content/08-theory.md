---
type: "THEORY"
title: "💻 Complete Full-Stack Error Flow"
---

```java
SCENARIO: User tries to register with existing email

1. FRONTEND sends request:
POST /api/users
{
  "name": "Bob",
  "email": "alice@email.com"  // Already exists!
}

2. BACKEND - Service layer:
@Service
public class UserService {
    public User create(User user) {
        if (userRepository.existsByEmail(user.getEmail())) {
            throw new DuplicateResourceException(
                "Email already registered"
            );
        }
        return userRepository.save(user);
    }
}

3. BACKEND - Exception handler catches it:
@RestControllerAdvice
public class GlobalExceptionHandler {
    @ExceptionHandler(DuplicateResourceException.class)
    public ProblemDetail handleDuplicate(DuplicateResourceException ex) {
        ProblemDetail problem = ProblemDetail.forStatusAndDetail(
            HttpStatus.CONFLICT,  // 409
            ex.getMessage()
        );
        problem.setProperty("errorCode", ex.getErrorCode());
        return problem;
    }
}

4. RESPONSE sent to frontend:
HTTP/1.1 409 Conflict
Content-Type: application/problem+json
{
  "status": 409,
  "detail": "Email already registered",
  "errorCode": "DUPLICATE_RESOURCE"
}

5. FRONTEND handles error:
if (response.status === 409) {
    displayError('This email is already registered. Please login instead.');
}

6. USER sees:
[Error Message Box]
This email is already registered. Please login instead.
[×]

COMPLETE JOURNEY - Error caught and handled at every layer!
```