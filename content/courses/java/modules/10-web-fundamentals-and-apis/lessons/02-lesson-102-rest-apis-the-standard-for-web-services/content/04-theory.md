---
type: "THEORY"
title: "HTTP Status Codes - Response Indicators"
---

2xx SUCCESS:
200 OK - Request succeeded
201 Created - Resource created (POST)
204 No Content - Success but no body (DELETE)

3xx REDIRECTION:
301 Moved Permanently
304 Not Modified (cached)

4xx CLIENT ERRORS:
400 Bad Request - Invalid data
401 Unauthorized - Need authentication
403 Forbidden - Authenticated but not allowed
404 Not Found - Resource doesn't exist
409 Conflict - Resource state conflict

5xx SERVER ERRORS:
500 Internal Server Error - Server crashed
502 Bad Gateway - Upstream server failed
503 Service Unavailable - Server overloaded

EXAMPLE:
GET /api/users/999
→ 404 Not Found (user doesn't exist)

POST /api/users (with invalid data)
→ 400 Bad Request