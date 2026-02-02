---
type: "THEORY"
title: "Building a Complete Blog API"
---

**Congratulations! You're ready to build a real-world API project.**

This mini-project combines everything you've learned about FastAPI and Pydantic into a complete blog system with user authentication.

**What we're building:**

A RESTful Blog API with:
- **User registration and login** (JWT authentication)
- **Blog post CRUD** (Create, Read, Update, Delete)
- **Comments system**
- **Protected routes** (only authors can edit their posts)
- **Input validation** with Pydantic models

**Project structure:**
```
blog_api/
    main.py             # FastAPI application
    models.py           # Pydantic models
    auth.py             # OAuth2 + JWT logic
    routers/
        users.py        # User endpoints
        posts.py        # Post endpoints
        comments.py     # Comment endpoints
    tests/
        test_auth.py    # Auth tests
        test_posts.py   # Post tests
```

**API Endpoints:**

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| POST | /api/auth/register | Register new user | No |
| POST | /api/auth/login | Login, get token | No |
| GET | /api/posts | List all posts | No |
| GET | /api/posts/{id} | Get single post | No |
| POST | /api/posts | Create post | Yes |
| PUT | /api/posts/{id} | Update post | Yes |
| DELETE | /api/posts/{id} | Delete post | Yes |
| POST | /api/posts/{id}/comments | Add comment | Yes |

**Technologies used:**
- FastAPI (web framework)
- python-jose + OAuth2 (authentication)
- Pydantic (data validation)
- In-memory storage (can upgrade to SQLite/PostgreSQL)
- pytest + httpx (testing)