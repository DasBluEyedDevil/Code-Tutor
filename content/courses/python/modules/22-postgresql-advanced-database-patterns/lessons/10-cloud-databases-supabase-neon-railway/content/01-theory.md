---
type: "THEORY"
title: "Cloud PostgreSQL Options"
---

Running your own PostgreSQL server is great for learning, but production apps need reliability, backups, and scalability. Here are the top cloud options:

**Supabase** - The Firebase Alternative
- Free tier: 500MB storage, 2 projects
- Built-in auth, real-time subscriptions, edge functions
- Automatic REST API (PostgREST)
- Great dashboard and SQL editor
- Best for: Full-stack apps needing auth + database

**Neon** - Serverless PostgreSQL
- Free tier: 512MB storage, unlimited projects
- Serverless (scales to zero)
- Database branching (like git for databases!)
- Instant provisioning
- Best for: Development, CI/CD, cost-sensitive projects

**Railway** - Deploy Everything
- Free tier: $5/month credit
- One-click PostgreSQL
- Automatic deployments from GitHub
- Great for full stack (app + database together)
- Best for: Quick deployments, hobby projects

**Comparison:**

| Feature | Supabase | Neon | Railway |
|---------|----------|------|--------|
| Free Storage | 500MB | 512MB | Limited |
| Branching | No | Yes | No |
| Real-time | Yes | No | No |
| Auth Built-in | Yes | No | No |
| Serverless | Partial | Yes | No |