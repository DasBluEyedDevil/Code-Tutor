---
type: "WARNING"
title: "Authentication Security"
---

**Critical Security Practices:**

1. **Never Store Plaintext Passwords:**
   ```python
   # BAD: Never do this!
   user.password = request.POST['password']
   
   # GOOD: Django's password hashing
   user.set_password(request.POST['password'])
   ```

2. **Use HTTPS in Production:**
   - Session cookies sent over HTTP can be intercepted
   - Enable `SESSION_COOKIE_SECURE = True`
   - Use `SECURE_SSL_REDIRECT = True`

3. **Protect Against Brute Force:**
   ```python
   # Use django-axes or similar
   AXES_FAILURE_LIMIT = 5  # Lock after 5 failures
   AXES_COOLOFF_TIME = timedelta(hours=1)
   ```

4. **Session Security Settings:**
   ```python
   SESSION_COOKIE_HTTPONLY = True  # No JS access
   SESSION_COOKIE_SECURE = True    # HTTPS only
   SESSION_COOKIE_AGE = 3600       # 1 hour expiry
   ```

5. **Password Validation:**
   ```python
   AUTH_PASSWORD_VALIDATORS = [
       {'NAME': 'django.contrib.auth.password_validation.MinimumLengthValidator'},
       {'NAME': 'django.contrib.auth.password_validation.CommonPasswordValidator'},
       {'NAME': 'django.contrib.auth.password_validation.NumericPasswordValidator'},
   ]
   ```

**Common Mistakes:**
- Forgetting `@login_required` on sensitive views
- Not using `get_user_model()` for custom user models
- Exposing user IDs in URLs (use UUIDs instead)
- Not logging failed authentication attempts