---
type: "THEORY"
title: "Micro vs Batteries-Included: Two Philosophies"
---

**You've mastered FastAPI. Now let's understand why Django exists.**

In Modules 14-16, you built APIs with FastAPI—explicit, flexible, and async-first. Django takes a completely different approach: it's a **batteries-included** framework that makes decisions for you.

**The Two Framework Philosophies:**

| Aspect | FastAPI (Micro) | Django (Batteries-Included) |
|--------|-----------------|----------------------------|
| Database | You choose: SQLAlchemy, Tortoise ORM, raw SQL | Built-in ORM, always |
| Admin Interface | Build your own | Auto-generated from models |
| Authentication | Install and configure (M16) | Built-in users, groups, permissions |
| Validation | Pydantic models | Django forms + serializers |
| Templating | Your choice (or none for APIs) | Django templates built-in |
| Async Support | First-class, native | Partial (Django 4.0+) |

**Why Learn Both?**

1. **Different projects need different tools**: APIs → FastAPI, Content sites → Django
2. **Job market demands both**: Many companies use Django for established apps
3. **Understanding tradeoffs**: Knowing both helps you choose wisely
4. **Cross-pollination**: Django REST Framework uses FastAPI-like patterns

**What You Already Know from FastAPI:**

- HTTP methods (GET, POST, PUT, DELETE)
- Request/response patterns
- Authentication flows (JWT, OAuth2)
- Database integration (SQLAlchemy)
- API documentation

**What's Different in Django:**

- Convention over configuration
- Project structure is prescribed
- ORM is tightly integrated
- Templates for HTML (not just JSON APIs)
- Admin interface for free

**The Mental Shift:**

FastAPI says: "Here are the building blocks—assemble them however you want."
Django says: "Here's the Django way—follow it and you'll move fast."

Neither is better. They solve different problems.
