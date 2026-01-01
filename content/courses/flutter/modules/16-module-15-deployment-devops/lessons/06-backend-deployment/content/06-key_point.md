---
type: "KEY_POINT"
title: "Database Configuration"
---


**Production PostgreSQL Setup**

Your database configuration significantly impacts backend performance and reliability.

**Connection Pooling:**

Serverpod uses connection pooling to efficiently manage database connections. Configure pools based on your deployment:

| Deployment Size | Pool Size | Max Connections |
|-----------------|-----------|----------------|
| Small (1 instance) | 10-20 | 25 |
| Medium (2-4 instances) | 5-10 per instance | 50 |
| Large (5+ instances) | 3-5 per instance | 100+ |

**Connection Pool Formula:**
```
pool_size = (max_connections - reserved) / num_instances
```

**Migration Best Practices:**

1. **Version control all migrations**: Store in `migrations/` directory
2. **Never modify applied migrations**: Create new ones instead
3. **Test migrations on staging first**: Verify data integrity
4. **Use transactions**: Ensure atomic changes
5. **Plan for rollbacks**: Keep rollback scripts ready

**Database Security Checklist:**

- Use strong, unique passwords
- Enable SSL connections (require_ssl: true)
- Restrict network access (VPC, firewall rules)
- Use connection string secrets, not plaintext
- Regular automated backups
- Point-in-time recovery enabled

