---
type: "EXAMPLE"
title: "Test the Endpoints"
---

Use curl or HTTPie to test your authentication endpoints:

```bash
# Start the server
bun run dev

# Register a new user
curl -X POST http://localhost:3000/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "test@example.com",
    "password": "Password123",
    "name": "Test User"
  }'

# Response:
# {
#   "message": "Registration successful",
#   "user": { "id": "...", "email": "test@example.com", ... },
#   "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
# }

# Login
curl -X POST http://localhost:3000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "test@example.com",
    "password": "Password123"
  }'

# Get current user (use token from login response)
curl http://localhost:3000/api/auth/me \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```
