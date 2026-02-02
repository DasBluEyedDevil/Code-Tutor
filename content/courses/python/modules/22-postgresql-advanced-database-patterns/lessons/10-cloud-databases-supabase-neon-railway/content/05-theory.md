---
type: "THEORY"
title: "SSL/TLS Configuration"
---

Cloud databases require encrypted connections. Here's how to configure SSL properly:

**asyncpg SSL Modes:**

| Mode | Description |
|------|-------------|
| `'disable'` | No SSL (never for cloud!) |
| `'prefer'` | Try SSL, fall back to plain |
| `'require'` | Must use SSL, skip verification |
| `'verify-ca'` | Verify server certificate |
| `'verify-full'` | Verify cert + hostname |

**Basic SSL (Most Cloud Providers):**
```python
pool = await asyncpg.create_pool(
    database_url,
    ssl='require',  # Encrypted, but doesn't verify cert
)
```

**Full Verification (Enterprise):**
```python
import ssl

ssl_context = ssl.create_default_context(
    cafile='/path/to/ca-certificate.crt'
)
ssl_context.check_hostname = True
ssl_context.verify_mode = ssl.CERT_REQUIRED

pool = await asyncpg.create_pool(
    database_url,
    ssl=ssl_context,
)
```

**Environment Variables:**
```bash
# Set in your deployment environment
export DATABASE_URL="postgresql://user:pass@host/db?sslmode=require"

# Or with certificate
export PGSSLCERT=/path/to/client.crt
export PGSSLKEY=/path/to/client.key
export PGSSLROOTCERT=/path/to/ca.crt
```