---
type: "KEY_POINT"
title: "Environment Variables Best Practices"
---

**Never commit secrets to version control!**

**.env.example (commit this):**
```env
# Database
DATABASE_URL=postgresql://user:password@localhost/finance_tracker

# Security - generate with: python -c "import secrets; print(secrets.token_urlsafe(32))"
SECRET_KEY=your-secret-key-here

# Optional
DEBUG=false
ACCESS_TOKEN_EXPIRE_MINUTES=30
```

**.env (never commit - add to .gitignore):**
```env
DATABASE_URL=postgresql://postgres:mypassword@localhost/finance_tracker
SECRET_KEY=abc123supersecretkey456xyz
DEBUG=true
```

**.gitignore:**
```
.env
*.pyc
__pycache__/
.pytest_cache/
.mypy_cache/
```