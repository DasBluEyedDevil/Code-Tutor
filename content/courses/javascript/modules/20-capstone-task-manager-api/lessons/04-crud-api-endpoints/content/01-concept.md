---
type: "THEORY"
title: "RESTful CRUD Operations"
---

Now we'll build the core of our API: Task and Category CRUD operations. Every operation requires authentication and enforces owner-only access.

**CRUD = Create, Read, Update, Delete**

| Operation | HTTP Method | Endpoint | Description |
|-----------|-------------|----------|-------------|
| Create | POST | /tasks | Create new task |
| Read (list) | GET | /tasks | List all user's tasks |
| Read (one) | GET | /tasks/:id | Get single task |
| Update | PUT | /tasks/:id | Update task |
| Delete | DELETE | /tasks/:id | Delete task |

**Key Principles:**
1. All routes are protected (require JWT)
2. Users can only access their own data
3. Input is validated with Zod
4. Consistent error responses
5. Proper HTTP status codes