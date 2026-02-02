---
type: "KEY_POINT"
title: "Configuration Best Practices"
---

**1. Never Commit Secrets**
```gitignore
# .gitignore
.env
.env.*
*.pem
*.key
secrets/
```

**2. Use Different Secrets Per Environment**
```
Dev:  JWT_SECRET=dev-only-secret-not-for-prod
Prod: JWT_SECRET=<generated-256-bit-random-key>
```

**3. Validate Configuration at Startup**
```python
# Fail fast if config is invalid
settings = get_settings()  # Raises on missing/invalid config
```

**4. Use SecretStr for Sensitive Values**
```python
api_key: SecretStr  # Won't appear in logs
```

**5. Document Required Variables**
```python
# .env.example (commit this!)
DATABASE_URL=postgresql://user:pass@host/db
JWT_SECRET=your-secret-here-min-32-chars
```

**6. Audit Secret Access**
- Log when secrets are accessed (not the values!)
- Track who/what accessed production secrets
- Alert on unusual access patterns

**7. Minimum Necessary Access**
- App only gets secrets it needs
- Different services get different secrets
- Rotate on employee departure