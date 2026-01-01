---
type: "THEORY"
title: "Testing Protected Routes"
---


### Setup: Create Users


### Test 1: Access Protected Route Without Token


Response (401 Unauthorized):

### Test 2: Login and Get Token


Response:

**Copy the token** - you'll need it for subsequent requests.

### Test 3: Access Protected Route With Token


Response (200 OK):

✅ **Authentication works!**

### Test 4: Regular User Tries to Access Admin Route


Response (403 Forbidden):

✅ **Authorization works!**

### Test 5: Admin Accesses Admin Route

First, create an admin user or promote existing user:


Login as admin:

Now access admin route with admin token:

Response (200 OK):

✅ **Admin access works!**

---



```json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "email": "alice@example.com",
      "fullName": "Alice Johnson",
      "role": "ADMIN",
      "createdAt": "2025-01-15T10:30:45"
    }
  ],
  "message": "Retrieved 1 users"
}
```
