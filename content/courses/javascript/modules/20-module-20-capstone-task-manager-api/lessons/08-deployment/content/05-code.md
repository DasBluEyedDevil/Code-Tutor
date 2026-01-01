---
type: "CODE"
title: "Production Environment Variables"
---

Configure all necessary environment variables for production:

Never hardcode secrets in your code. Use environment variables for all sensitive data:

```bash
# Production Environment Variables
# Store these in your hosting platform's secrets manager, NOT in code

# Database
DATABASE_URL="postgresql://user:password@db.railway.app:5432/taskmanager"

# Security
JWT_SECRET="super-secure-random-32-character-key-change-me"
JWT_EXPIRY="7d"

# Environment
NODE_ENV="production"
PORT=3000

# Logging
LOG_LEVEL="warn"

# CORS (for your frontend domain)
FRONTEND_URL="https://yourdomain.com"

# API Configuration
API_URL="https://api.yourdomain.com"

# For monitoring and alerting
SENTRY_DSN="https://your-sentry-project-id"
DATA_DOG_KEY="your-datadog-api-key"

# How to set them:
# Railway: railway variables set KEY=value
# Fly.io: flyctl secrets set KEY=value
# Heroku: heroku config:set KEY=value
```
