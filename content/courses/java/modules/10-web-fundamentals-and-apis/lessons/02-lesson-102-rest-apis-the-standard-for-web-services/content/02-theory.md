---
type: "THEORY"
title: "HTTP Methods - The Verbs"
---

GET - Retrieve data (read-only, safe):
GET /api/products
Response: [{ "id": 1, "name": "Laptop", "price": 999 }]

POST - Create new resource:
POST /api/products
Body: { "name": "Mouse", "price": 25 }
Response: { "id": 2, "name": "Mouse", "price": 25 }

PUT - Update entire resource:
PUT /api/products/2
Body: { "name": "Wireless Mouse", "price": 30 }

PATCH - Update partial resource:
PATCH /api/products/2
Body: { "price": 28 }

DELETE - Remove resource:
DELETE /api/products/2

IDEMPOTENT:
GET, PUT, DELETE are idempotent (same result if called multiple times)
POST is NOT idempotent (creates new resource each time)