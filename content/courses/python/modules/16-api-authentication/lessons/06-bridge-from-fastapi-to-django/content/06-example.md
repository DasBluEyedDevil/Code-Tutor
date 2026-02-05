---
type: "EXAMPLE"
title: "Setting Up Your Django Environment"
---

**Before Module 21, let's prepare your Django environment.**

**Step 1: Create a New Project Directory**

```bash
# Create and enter directory
mkdir django-projects
cd django-projects

# Create virtual environment
python -m venv venv

# Activate it
# Windows:
venv\Scripts\activate
# macOS/Linux:
source venv/bin/activate
```

**Step 2: Install Django**

```bash
pip install django djangorestframework
```

**Step 3: Create Your First Django Project**

```bash
# Create project (note the dot at the end)
django-admin startproject mysite .

# Create your first app
python manage.py startapp blog
```

**Step 4: Explore the Project Structure**

```
django-projects/
├── manage.py              # CLI tool (like uvicorn but more)
├── mysite/                # Project settings
│   ├── __init__.py
│   ├── settings.py        # Configuration (like environment variables)
│   ├── urls.py            # Root URL routing
│   └── wsgi.py            # WSGI entry point
└── blog/                  # Your first app
    ├── __init__.py
    ├── admin.py           # Admin interface registration
    ├── apps.py            # App configuration
    ├── models.py          # Database models (like SQLAlchemy)
    ├── tests.py           # Tests (like pytest)
    └── views.py           # Request handlers (like FastAPI routes)
```

**Step 5: Run the Development Server**

```bash
python manage.py runserver
```

Visit `http://localhost:8000` to see the Django welcome page!

**Step 6: Explore the Admin**

```bash
# Create database tables
python manage.py migrate

# Create admin user
python manage.py createsuperuser
```

Visit `http://localhost:8000/admin/` and log in—you get a full admin interface with zero code!

**What Module 21 Will Teach:**

- Django ORM (models, querysets, relationships)
- URL routing and views
- Templates (for HTML pages)
- Django REST Framework (for APIs like FastAPI)
- Authentication and permissions
- Deployment best practices

**Ready? You're about to see why Django powers Instagram, Pinterest, and Mozilla.**
