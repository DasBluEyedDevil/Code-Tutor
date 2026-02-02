---
type: "EXAMPLE"
title: "Environment Variables"
---

Configure for different environments:

```bash
# .env.development
VITE_API_URL=http://localhost:3000
VITE_ENV=development

# .env.production
VITE_API_URL=https://api.example.com
VITE_ENV=production
VITE_SENTRY_DSN=your-dsn
```
