---
type: "THEORY"
title: "Solution"
---


### Complete Resource Ownership System




### Test Scenarios

**Test 1: Alice creates a post**:

**Test 2: Bob tries to edit Alice's post** (should fail):

Response (403 Forbidden):

**Test 3: Alice edits her own post** (should succeed):

Response (200 OK): Post updated successfully!

**Test 4: Admin edits anyone's post** (should succeed):

Response (200 OK): Post updated successfully!

---



```bash
# Login as admin
TOKEN_ADMIN=$(curl -s -X POST http://localhost:8080/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email": "admin@example.com", "password": "AdminPass789!"}' \
  | jq -r '.data.token')

# Admin can edit Alice's post
curl -X PUT http://localhost:8080/api/posts/1 \
  -H "Authorization: Bearer $TOKEN_ADMIN" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Admin Edit",
    "content": "Admins can edit any post"
  }'
```
