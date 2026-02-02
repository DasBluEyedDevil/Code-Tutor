---
type: "WARNING"
title: "SSL and Domains"
---


**Custom Domain Configuration**

Most PaaS providers handle SSL automatically, but you need to configure DNS correctly:

**Railway Custom Domains:**
1. Go to Settings > Domains in Railway dashboard
2. Add your custom domain (api.myapp.com)
3. Create CNAME record pointing to provided Railway URL
4. Wait for SSL certificate provisioning (automatic)

**Fly.io Custom Domains:**
1. Run: `fly certs create api.myapp.com`
2. Create CNAME or A record as instructed
3. Verify: `fly certs show api.myapp.com`

**SSL/TLS Best Practices:**

- Always force HTTPS redirects
- Use TLS 1.2 or higher only
- Enable HSTS headers
- Use secure cookies (Secure, HttpOnly flags)
- Implement certificate monitoring

**Health Check Endpoints:**

Every production deployment needs health checks:

| Check Type | Purpose | Path |
|------------|---------|------|
| Liveness | Is the server running? | /health |
| Readiness | Can it serve requests? | /ready |
| Database | Is DB connected? | /health/db |

**Common SSL Issues:**

1. **Mixed content**: Ensure all URLs use HTTPS
2. **Certificate expiry**: Use auto-renewal (Let's Encrypt)
3. **SSL termination**: May happen at load balancer level
4. **WebSocket over HTTPS**: Use wss:// not ws://

