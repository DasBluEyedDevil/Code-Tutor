---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine a file cabinet with different actions:
• READ a file (GET) - Just look, don't change
• CREATE new file (POST) - Add new document
• UPDATE existing file (PUT) - Replace entire file
• DELETE file (DELETE) - Remove it

These are HTTP METHODS (or VERBS)! Each has a purpose:

GET = Read data (no changes)
POST = Create new resource
PUT = Update existing resource (full update)
DELETE = Remove resource
PATCH = Partial update (advanced)

REST API pattern:
• GET /api/products - List all
• GET /api/products/5 - Get one
• POST /api/products - Create new
• PUT /api/products/5 - Update
• DELETE /api/products/5 - Delete

ASP.NET Core mapping:
• app.MapGet() - GET requests
• app.MapPost() - POST requests
• app.MapPut() - PUT requests
• app.MapDelete() - DELETE requests

Think: Different HTTP methods = Different actions on same resource!