---
type: "THEORY"
title: "Checkpoint Quiz"
---


### Question 1
What's the difference between authentication and authorization?

A) They're the same thing
B) Authentication verifies identity, authorization verifies permissions
C) Authentication is for users, authorization is for admins
D) Authorization happens before authentication

### Question 2
Where should you extract the authenticated user's information in a protected route?

A) From the database
B) From the request body
C) From `call.principal<UserPrincipal>()`
D) From a query parameter

### Question 3
What HTTP status code should you return when a user tries to access an admin-only endpoint without admin role?

A) 401 Unauthorized
B) 403 Forbidden
C) 404 Not Found
D) 500 Internal Server Error

### Question 4
In the resource ownership pattern, who can modify a resource?

A) Only the owner
B) Only admins
C) The owner OR admins
D) Anyone with a valid token

### Question 5
What happens if you try to access a protected route without a token?

A) The route executes normally
B) Ktor returns 403 Forbidden
C) The challenge function is called, typically returning 401 Unauthorized
D) The server crashes

---

