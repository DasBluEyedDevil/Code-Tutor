---
type: "THEORY"
title: "What Makes a Good REST API?"
---

REST = Representational State Transfer

A well-designed API should be:

✓ INTUITIVE - Easy to understand and use
✓ CONSISTENT - Follows predictable patterns
✓ DISCOVERABLE - Clear what each endpoint does
✓ SCALABLE - Can handle growth
✓ WELL-DOCUMENTED - Clear examples and responses

BAD API:
POST /createNewUser
GET /getUserById?id=123
POST /deleteUser

GOOD API:
POST /api/users
GET /api/users/123
DELETE /api/users/123

The difference? HTTP methods + resource-based URLs!