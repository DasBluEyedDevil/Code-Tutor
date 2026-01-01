---
type: "THEORY"
title: "API Versioning"
---

APIs evolve. Don't break existing clients!

VERSIONING STRATEGIES:

1. URL Path (Most common):
/api/v1/users
/api/v2/users

@RestController
@RequestMapping("/api/v1/users")
public class UserControllerV1 { }

@RestController
@RequestMapping("/api/v2/users")
public class UserControllerV2 { }

2. Header-based:
GET /api/users
Header: Accept: application/vnd.myapi.v1+json

When to version:
✓ Breaking changes (removed fields)
✓ Changed response structure
✓ Different authentication method

DON'T version for:
✓ Bug fixes
✓ Adding optional fields
✓ Performance improvements