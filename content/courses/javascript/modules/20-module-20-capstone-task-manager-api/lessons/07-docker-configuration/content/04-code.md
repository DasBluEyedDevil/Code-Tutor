---
type: "CODE"
title: "Environment Configuration - .env.docker and .env.production"
---

Create environment configuration files for different environments:

```bash
# .env.docker (for docker-compose)
# Used when running with docker-compose up
DATABASE_URL=postgresql://taskmanager:dev-password@postgres:5432/taskmanager_dev
JWT_SECRET=dev-secret-change-in-production-please
PORT=3000
NODE_ENV=development
LOG_LEVEL=debug

# .env.production (for production deployment)
# Never commit this - use secrets management (AWS Secrets Manager, Vault, etc.)
DATABASE_URL=postgresql://taskmanager:${SECURE_DB_PASSWORD}@prod-postgres:5432/taskmanager
JWT_SECRET=${SECURE_JWT_SECRET}
PORT=3000
NODE_ENV=production
LOG_LEVEL=warn
SENTRY_DSN=${SENTRY_DSN}

# .env.example (COMMIT THIS - never include real values)
# Copy to .env and fill in your values
DATABASE_URL=postgresql://user:password@localhost:5432/dbname
JWT_SECRET=your-secret-key-here
PORT=3000
NODE_ENV=development
LOG_LEVEL=debug
```
