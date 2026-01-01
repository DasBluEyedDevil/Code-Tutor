---
type: "WARNING"
title: "Admin Security Considerations"
---

**The admin is powerful - protect it:**

1. **Use strong passwords** for superusers
2. **Change the admin URL** from `/admin/`:
   ```python
   # urls.py
   urlpatterns = [
       path('secret-dashboard/', admin.site.urls),
   ]
   ```

3. **Restrict admin access by IP** in production:
   ```python
   # middleware or nginx configuration
   ADMIN_ALLOWED_IPS = ['10.0.0.1', '192.168.1.100']
   ```

4. **Use 2FA** with django-otp or similar

5. **Audit admin actions** - who changed what:
   ```python
   # Admin logs are built-in
   from django.contrib.admin.models import LogEntry
   ```

6. **Don't expose admin to public internet** if possible