---
type: "THEORY"
title: "Solution"
---


### Complete Multi-Tenant System


### Testing


---



```bash
# Request for Tenant A
curl -X GET http://localhost:8080/api/users \
  -H "X-Tenant-ID: tenant-a"

# Returns only Tenant A's users

# Request for Tenant B
curl -X GET http://localhost:8080/api/users \
  -H "X-Tenant-ID: tenant-b"

# Returns only Tenant B's users
```
