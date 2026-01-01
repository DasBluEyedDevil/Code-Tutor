---
type: "THEORY"
title: "Key Annotations Explained"
---

@RestController:
- Combines @Controller + @ResponseBody
- Returns data (JSON) instead of views (HTML)
- Every method returns data, not page names

@RequestMapping("/api/users"):
- Base URL for all methods in this controller
- Methods add to this path

@GetMapping, @PostMapping, etc.:
- Shorthand for @RequestMapping(method = GET/POST)
- Maps HTTP method to Java method

@PathVariable:
- Extracts value from URL path
- /api/users/{id} → @PathVariable Long id
- /api/users/123 → id = 123

@RequestBody:
- Reads JSON from request body
- Automatically converts JSON to Java object
- POST/PUT requests with data

@RequestParam:
- Extracts query parameters
- /api/users?age=20 → @RequestParam int age