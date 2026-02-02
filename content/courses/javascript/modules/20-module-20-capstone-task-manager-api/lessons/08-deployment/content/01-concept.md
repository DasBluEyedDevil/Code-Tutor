---
type: "THEORY"
title: "Production Deployment Checklist"
---

Before deploying to production, ensure your application meets these requirements:

**Security:**
- Environment variables for all secrets (never commit .env)
- JWT_SECRET should be cryptographically strong (32+ characters)
- HTTPS enabled (redirects HTTP to HTTPS)
- CORS properly configured for your frontend domain
- Rate limiting to prevent abuse
- Input validation with Zod

**Environment Variables:**
- DATABASE_URL - Production database connection string
- JWT_SECRET - Secure signing key
- NODE_ENV - Set to 'production'
- PORT - Set by hosting platform
- LOG_LEVEL - Usually 'error' or 'warn' in production

**Monitoring:**
- Health endpoint for uptime monitoring
- Error logging and alerting
- Database connection monitoring
- Request/response logging
- Performance metrics

**Database:**
- Backups enabled
- Migrations tested before deployment
- Connection pooling configured
- Indexes in place for critical queries

**Best Practices:**
- Use a staging environment first
- Run tests before deploying
- Have a rollback plan
- Monitor logs after deployment
- Set up automated backups