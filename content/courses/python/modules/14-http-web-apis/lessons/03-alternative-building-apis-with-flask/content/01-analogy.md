---
type: "ANALOGY"
title: "Flask: Understanding Web Framework Fundamentals"
---

**Why Learn Flask?**

While FastAPI is the modern choice for new APIs, Flask remains valuable for:

1. **Understanding Fundamentals** - Flask's explicit approach teaches you how web frameworks work under the hood
2. **Legacy Codebases** - Many production systems still use Flask
3. **Simple Prototypes** - Quick scripts and internal tools
4. **Extensive Ecosystem** - Thousands of Flask extensions available

**Flask = Lightweight web framework for Python**

**Think of it like a restaurant:**
- **Routes** = Menu items (what you can order)
- **Methods** = How to order (dine-in, takeout, delivery)
- **Responses** = What you get back

**REST API Principles (same as FastAPI):**

1. **Resources** - Things you work with (users, posts, products)
2. **URLs** - Addresses for resources (`/api/users`, `/api/posts`)
3. **HTTP Methods** - Actions on resources
   - GET `/api/users` - List all users
   - GET `/api/users/1` - Get user #1
   - POST `/api/users` - Create new user
   - PUT `/api/users/1` - Update user #1
   - DELETE `/api/users/1` - Delete user #1

4. **JSON** - Data format for requests/responses

**Flask vs FastAPI:**
- Flask requires manual validation; FastAPI validates automatically
- Flask needs extensions for docs; FastAPI generates docs from code
- Flask is synchronous by default; FastAPI is async-first
- Both are excellent choices - know when to use each!