---
type: "WARNING"
title: "Deployment Pitfalls & Security"
---

Common mistakes that cause production outages and security breaches:

**Secrets Exposure:**
- Never commit .env to git
- Never log environment variables
- Never include secrets in error messages sent to clients
- Never use weak JWT_SECRET (min 32 characters)
- Rotate secrets regularly
- Use different secrets for each environment
- Store secrets in platform-managed secret managers

**Database Migrations:**
- Don't run migrations with the same connection pool as your app
- Don't skip testing migrations first
- Don't migrate without a backup
- Run migrations before deploying new code
- Always have a rollback plan
- Test migrations on staging environment first
- Make migrations backwards compatible when possible

**Connection Management:**
- Don't create new database connections in request handlers
- Don't use infinite timeouts
- Don't ignore connection pool warnings
- Use connection pooling (PgBouncer for PostgreSQL)
- Set reasonable query timeouts (5-30 seconds)
- Monitor connection pool stats

**Deployment Process:**
- Don't deploy without running tests
- Don't deploy during peak hours without a team
- Don't skip the staging environment
- Don't delete old database backups immediately
- Always test in staging first
- Deploy during off-peak hours
- Have a communication plan
- Keep backups for at least 30 days

**Error Handling:**
- Don't expose internal errors to clients
- Don't log passwords or tokens
- Don't send stack traces to frontend
- Return generic error messages
- Log full details server-side
- Use error codes for debugging