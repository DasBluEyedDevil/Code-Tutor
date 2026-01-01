---
type: "EXAMPLE"
title: "Django Project Structure"
---

**Standard Django Project Layout:**

```
myproject/
|-- manage.py           # CLI tool for Django commands
|-- myproject/
|   |-- __init__.py
|   |-- settings.py     # Configuration (database, apps, etc.)
|   |-- urls.py         # URL routing (main router)
|   |-- wsgi.py         # WSGI entry point (production)
|   |-- asgi.py         # ASGI entry point (async)
|-- myapp/
|   |-- __init__.py
|   |-- admin.py        # Admin configuration
|   |-- models.py       # Database models (ORM)
|   |-- views.py        # Request handlers
|   |-- urls.py         # App-specific URLs
|   |-- tests.py        # Tests
|   |-- forms.py        # Form definitions
|   |-- templates/      # HTML templates
|   |-- static/         # CSS, JS, images
|   |-- migrations/     # Database migrations
```

**Key Concepts:**

- **Project vs App:** A project contains multiple apps. Apps are reusable components.
- **settings.py:** Central configuration - database, installed apps, middleware
- **urls.py:** Maps URLs to views (like FastAPI's path decorators)
- **models.py:** Define your database schema with Python classes
- **views.py:** Handle requests and return responses
- **admin.py:** Register models for the admin interface

**Django's MVT Pattern:**
- **Model:** Data layer (models.py)
- **View:** Logic layer (views.py)
- **Template:** Presentation layer (templates/)