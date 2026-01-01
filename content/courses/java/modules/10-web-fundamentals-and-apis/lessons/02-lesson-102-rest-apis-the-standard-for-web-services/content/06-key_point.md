---
type: "KEY_POINT"
title: "REST API Best Practices"
---

1. USE NOUNS, NOT VERBS IN URLS:
   ✓ GET /api/users
   ✗ GET /api/getUsers

2. USE PLURAL NOUNS:
   ✓ /api/users/123
   ✗ /api/user/123

3. USE HTTP METHODS FOR ACTIONS:
   Don't: POST /api/users/delete/123
   Do: DELETE /api/users/123

4. VERSION YOUR API:
   /api/v1/users
   /api/v2/users

5. RETURN APPROPRIATE STATUS CODES:
   Success → 200
   Created → 201
   Not Found → 404

6. USE QUERY PARAMETERS FOR FILTERING:
   /api/users?age=20&active=true