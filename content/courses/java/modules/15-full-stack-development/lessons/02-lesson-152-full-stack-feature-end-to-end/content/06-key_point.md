---
type: "KEY_POINT"
title: "The Full Flow"
---

USER ACTION:
1. User fills form, clicks "Add User"

FRONTEND:
2. JavaScript captures form data
3. fetch() sends POST to http://localhost:8080/api/users
4. Sends JSON: {"name":"Alice","email":"alice@email.com"}

BACKEND:
5. @PostMapping receives request
6. @RequestBody converts JSON to User object
7. UserService.createUser() called
8. UserRepository.save() inserts to database

DATABASE:
9. INSERT INTO users...
10. Returns generated ID

BACKEND RESPONSE:
11. Return User object as JSON
12. Status: 200 OK

FRONTEND:
13. Receives response
14. Calls loadUsers() to refresh list
15. GET /api/users
16. Displays updated user list

COMPLETE CYCLE!