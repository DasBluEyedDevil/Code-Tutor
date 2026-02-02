---
type: "THEORY"
title: "Why Docker Compose for Development"
---

**Docker Compose = Multi-container orchestration made simple**

While a single Dockerfile runs one container, real applications often need multiple services:

**Our Personal Finance Tracker needs:**
- FastAPI application (port 8000)
- PostgreSQL database (port 5432)
- Maybe Redis for caching
- Maybe a background worker

**Docker Compose solves:**

1. **Multi-container management**
   - Define all services in one file
   - Start/stop everything together
   - Containers can talk to each other

2. **Development workflows**
   - Mount source code for hot reload
   - Override production settings
   - Easy database access

3. **Environment parity**
   - Same PostgreSQL version as production
   - Same environment variables
   - No "but it works with SQLite locally"

4. **One command to rule them all**
   ```bash
   docker compose up
   ```
   - Starts all services
   - Creates networks
   - Sets up volumes
   - Shows combined logs

**Compose vs Kubernetes:**
| Docker Compose | Kubernetes |
|----------------|------------|
| Local development | Production at scale |
| Single machine | Multiple machines |
| Simple YAML | Complex manifests |
| `docker compose up` | `kubectl apply` |

Use Compose for development, Kubernetes for production (if needed).