---
type: "THEORY"
title: "Django Project vs App"
---

Django has a two-level organization:

**Project** - The entire web application
- Contains settings, URL configuration, deployment config
- Created with `django-admin startproject`
- One project per web application

**App** - A module that does one thing
- Contains models, views, templates for a feature
- Created with `python manage.py startapp`
- Multiple apps per project (reusable components)

**Example Structure for Finance Tracker:**
```
finance_tracker/           # Project root
    manage.py              # CLI utility
    finance_tracker/       # Project package
        __init__.py
        settings.py        # Configuration
        urls.py            # URL routing
        wsgi.py            # Production server
        asgi.py            # Async server
    accounts/              # App: User authentication
        models.py
        views.py
        urls.py
    transactions/          # App: Transaction management
        models.py
        views.py
        urls.py
    budgets/               # App: Budget tracking
        models.py
        views.py
        urls.py
```

**Key Commands:**
```bash
# Create new project
django-admin startproject finance_tracker

# Create new app
cd finance_tracker
python manage.py startapp transactions
```