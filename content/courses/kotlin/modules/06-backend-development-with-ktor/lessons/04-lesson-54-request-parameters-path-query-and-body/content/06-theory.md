---
type: "THEORY"
title: "📦 Request Body: Complex Data"
---


### When to Use Request Body

Use request body for:
- ✅ Creating resources (POST)
- ✅ Updating resources (PUT/PATCH)
- ✅ Complex search queries
- ✅ Data that doesn't fit in URLs
- ✅ Sensitive data (passwords, etc.)

### Receiving JSON Body


**Test it:**

### Receiving Plain Text


### Receiving Form Data


**Test it:**

---



```bash
curl -X POST http://localhost:8080/login \
  -d "username=alice&password=secret123"
```
