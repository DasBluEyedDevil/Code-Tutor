---
type: "THEORY"
title: "Cost Optimization Tips"
---

Cloud databases can get expensive. Here's how to keep costs down:

**Free Tier Maximization:**

| Provider | Free Limit | Best For |
|----------|------------|----------|
| Supabase | 500MB, 2 projects | Side projects |
| Neon | 512MB, unlimited | Development |
| Railway | $5/month credit | Quick deploys |

**Reduce Storage Costs:**

1. **Compress JSONB:** Store only essential data
2. **Partition old data:** Archive transactions > 1 year
3. **Use appropriate types:** `SMALLINT` vs `INTEGER` vs `BIGINT`
4. **Clean up soft deletes:** Hard delete after 90 days

**Reduce Compute Costs:**

1. **Connection pooling:** Fewer connections = smaller instance
2. **Optimize queries:** Add indexes, avoid N+1
3. **Cache aggressively:** Redis for frequent reads
4. **Serverless (Neon):** Scale to zero when idle

**Neon-Specific Savings:**
```python
# Neon charges for compute time
# - Use min_size=1 to allow scaling down
# - Short connection lifetime for serverless
pool = await asyncpg.create_pool(
    neon_url,
    min_size=1,  # Can scale to zero
    max_size=5,  # Don't over-provision
    max_inactive_connection_lifetime=30,
)
```

**Monitoring Costs:**
- Set up billing alerts
- Review usage weekly
- Right-size instances monthly