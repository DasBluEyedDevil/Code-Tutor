---
type: "THEORY"
title: "Choosing the Right Base Image"
---

**Base images determine your container's foundation**

Python offers several official base images:

**1. `python:3.13` (full)**
- Based on Debian
- ~900MB image size
- Includes build tools, compilers
- Good for development, debugging

**2. `python:3.13-slim` (recommended)**
- Based on Debian minimal
- ~150MB image size
- Just Python and essentials
- Best balance of size and compatibility

**3. `python:3.13-alpine`**
- Based on Alpine Linux
- ~50MB image size
- Uses musl instead of glibc
- Some packages may not work or compile slower

**Our recommendation: `python:3.13-slim`**

```dockerfile
# Good default choice
FROM python:3.13-slim

# Avoid unless you know what you're doing
# FROM python:3.13-alpine
```

**Why not Alpine for Python?**
- Many wheels are built for glibc, not musl
- Packages with C extensions compile from source (slow)
- Debugging is harder (different tooling)
- Size savings often lost to compiled dependencies

**Slim is the sweet spot:**
- Small enough for production
- Compatible with all pip packages
- Familiar Debian-based tools