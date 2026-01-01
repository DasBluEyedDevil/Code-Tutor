---
type: "THEORY"
title: "⚠️ Common REST API Mistakes to Avoid"
---

❌ MISTAKE 1: Verbs in URLs
POST /api/createUser
FIX: POST /api/users

❌ MISTAKE 2: Wrong HTTP methods
GET /api/users/delete/123
FIX: DELETE /api/users/123

❌ MISTAKE 3: Returning wrong status codes
return ResponseEntity.ok(null);  // 200 when nothing found
FIX: return ResponseEntity.notFound().build();  // 404

❌ MISTAKE 4: Exposing database structure
/api/users?query=SELECT * FROM users
FIX: /api/users?role=admin&status=active

❌ MISTAKE 5: No rate limiting
Anyone can call your API 1000000 times/second
FIX: Implement rate limiting (429 status code)

❌ MISTAKE 6: Returning passwords or sensitive data
{"id": 1, "name": "Alice", "password": "secret123"}
FIX: Use @JsonIgnore or DTOs to exclude sensitive fields