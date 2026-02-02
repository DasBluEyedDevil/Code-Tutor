---
type: "EXAMPLE"
title: "Essential Settings Configuration"
---

Key settings every Django project needs:

**Expected Output:**
```
Settings configured for: finance_tracker
DEBUG: True (development mode)
Installed apps: 6 built-in + 3 custom
Database: SQLite (finance_tracker.db)
```

```python
# settings.py - Key configuration for finance tracker

import os
from pathlib import Path

# Build paths inside the project
BASE_DIR = Path(__file__).resolve().parent.parent

# SECURITY: Keep secret key hidden in production!
SECRET_KEY = os.environ.get('DJANGO_SECRET_KEY', 'dev-key-change-in-production')

# SECURITY: Never run DEBUG=True in production
DEBUG = os.environ.get('DJANGO_DEBUG', 'True') == 'True'

ALLOWED_HOSTS = ['localhost', '127.0.0.1'] if DEBUG else ['yoursite.com']

# Application definition
INSTALLED_APPS = [
    # Django built-in apps
    'django.contrib.admin',      # Admin interface
    'django.contrib.auth',       # Authentication
    'django.contrib.contenttypes',
    'django.contrib.sessions',   # Session management
    'django.contrib.messages',   # Flash messages
    'django.contrib.staticfiles', # Static file serving
    
    # Our finance tracker apps
    'accounts',      # User profiles, authentication
    'transactions',  # Income/expense tracking
    'budgets',       # Budget management
]

# Database configuration
DATABASES = {
    'default': {
        'ENGINE': 'django.db.backends.sqlite3',
        'NAME': BASE_DIR / 'finance_tracker.db',
    }
}

# For production, use PostgreSQL:
# DATABASES = {
#     'default': {
#         'ENGINE': 'django.db.backends.postgresql',
#         'NAME': os.environ.get('DB_NAME'),
#         'USER': os.environ.get('DB_USER'),
#         'PASSWORD': os.environ.get('DB_PASSWORD'),
#         'HOST': os.environ.get('DB_HOST', 'localhost'),
#         'PORT': os.environ.get('DB_PORT', '5432'),
#     }
# }

# Internationalization
LANGUAGE_CODE = 'en-us'
TIME_ZONE = 'UTC'
USE_I18N = True
USE_TZ = True  # Always use timezone-aware datetimes

# Static files (CSS, JavaScript, Images)
STATIC_URL = '/static/'

# Demonstrate the configuration
print(f"Settings configured for: finance_tracker")
print(f"DEBUG: {DEBUG} ({'development' if DEBUG else 'production'} mode)")
print(f"Installed apps: 6 built-in + 3 custom")
print(f"Database: SQLite (finance_tracker.db)")
```
