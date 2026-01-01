---
type: "THEORY"
title: "HTTP Requests and Responses"
---

When you visit a website:

1. CLIENT SENDS REQUEST:
GET /api/users/123 HTTP/1.1
Host: example.com

This means: "Give me information about user 123"

2. SERVER PROCESSES REQUEST:
- Looks up user 123 in database
- Prepares the data

3. SERVER SENDS RESPONSE:
HTTP/1.1 200 OK
Content-Type: application/json
{"name": "Alice", "age": 20}

Common HTTP methods:
- GET: Retrieve data ("show me")
- POST: Create data ("save this")
- PUT: Update data ("change this")
- DELETE: Remove data ("delete this")