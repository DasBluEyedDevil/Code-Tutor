---
type: "KEY_POINT"
title: "App Registration Pattern"
---

**Every app must be registered in INSTALLED_APPS:**

```python
INSTALLED_APPS = [
    # ... built-in apps
    'transactions',  # Short form
    'transactions.apps.TransactionsConfig',  # Full form (recommended)
]
```

**Why the full form?**
- Allows app configuration in `apps.py`
- Enables signals, ready() hooks
- Better for app-specific settings

**The apps.py file:**
```python
# transactions/apps.py
from django.apps import AppConfig

class TransactionsConfig(AppConfig):
    default_auto_field = 'django.db.models.BigAutoField'
    name = 'transactions'
    verbose_name = 'Transaction Management'
    
    def ready(self):
        # Import signals here
        import transactions.signals
```