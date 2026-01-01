---
type: "THEORY"
title: "Making HTTP Requests: The Four Main Methods"
---


HTTP defines several request methods, each with a specific purpose. Understanding when to use each is fundamental to working with REST APIs.

**GET - Retrieve Data**

GET requests fetch data without modifying anything on the server. They are:
- Idempotent (calling multiple times produces the same result)
- Cacheable (browsers and proxies can cache responses)
- Should not have a request body (use query parameters instead)

Examples:
- Fetch a user's profile: `GET /users/123`
- Search for products: `GET /products?category=electronics&sort=price`
- Get weather: `GET /weather?city=London`

**POST - Create New Resources**

POST requests create new resources on the server. They are:
- Not idempotent (calling twice creates two resources)
- Not cacheable
- Include a request body with the data to create

Examples:
- Create a new user: `POST /users` with user data in body
- Submit an order: `POST /orders` with order details
- Upload a file: `POST /files` with multipart form data

**PUT - Replace Entire Resource**

PUT requests completely replace a resource. They are:
- Idempotent (replacing with the same data multiple times has same result)
- Require the complete resource in the body
- Create the resource if it does not exist (in some APIs)

Examples:
- Update entire user profile: `PUT /users/123` with complete user object
- Replace configuration: `PUT /settings` with all settings

**PATCH - Partial Update**

PATCH requests update specific fields of a resource. They are:
- May or may not be idempotent (depends on implementation)
- Include only the fields to update in the body
- More efficient than PUT for small changes

Examples:
- Change just the email: `PATCH /users/123` with `{"email": "new@email.com"}`
- Toggle a flag: `PATCH /posts/456` with `{"published": true}`

**DELETE - Remove Resources**

DELETE requests remove resources. They are:
- Idempotent (deleting an already-deleted resource is a no-op)
- Usually do not have a request body
- Return 204 No Content or 200 OK on success

Examples:
- Delete a user: `DELETE /users/123`
- Remove from cart: `DELETE /cart/items/789`

