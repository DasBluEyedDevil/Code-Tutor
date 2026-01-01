---
type: "WARNING"
title: "Security Warnings"
---

**Critical Security Settings:**

1. **SECRET_KEY** - Never commit to version control!
   - Use environment variables in production
   - Generate with: `python -c "from django.core.management.utils import get_random_secret_key; print(get_random_secret_key())"`

2. **DEBUG = False** in production
   - DEBUG=True exposes sensitive error information
   - Shows full stack traces to users
   - Lists all your settings

3. **ALLOWED_HOSTS** must be set in production
   - Prevents HTTP Host header attacks
   - List only your actual domains

4. **Database credentials** - Use environment variables
   - Never hardcode passwords
   - Use `.env` files (not in git) or secrets manager