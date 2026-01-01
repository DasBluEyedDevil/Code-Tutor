---
type: "KEY_POINT"
title: "Static Files Configuration"
---

**Static files are CSS, JavaScript, and images that don't change per request.**

**Development Setup:**
```python
# settings.py
STATIC_URL = '/static/'  # URL prefix

# Where to find static files during development
STATICFILES_DIRS = [
    BASE_DIR / 'static',  # Project-level static files
]

# Where collectstatic puts files for production
STATIC_ROOT = BASE_DIR / 'staticfiles'
```

**File Organization:**
```
project/
    static/                 # Project-level
        css/style.css
        js/app.js
        images/logo.png
    transactions/
        static/             # App-level
            transactions/   # Namespaced!
                js/transactions.js
```

**Using Static Files in Templates:**
```django
{% load static %}

<link rel="stylesheet" href="{% static 'css/style.css' %}">
<script src="{% static 'js/app.js' %}"></script>
<img src="{% static 'images/logo.png' %}" alt="Logo">
```

**Production Deployment:**
```bash
# Collect all static files to STATIC_ROOT
python manage.py collectstatic
# Then serve via nginx, CDN, or whitenoise
```