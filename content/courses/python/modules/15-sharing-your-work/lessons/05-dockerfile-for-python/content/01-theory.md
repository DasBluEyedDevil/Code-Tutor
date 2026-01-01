---
type: "THEORY"
title: "Why Docker for Python Development"
---

**Docker = Consistent environments everywhere**

Docker solves the "works on my machine" problem by packaging your application with all its dependencies into a container.

**Benefits for Python projects:**

1. **Consistent environments**
   - Same Python version everywhere
   - Same dependencies, same versions
   - No more "it works on my laptop"

2. **Easy deployment**
   - Build once, run anywhere
   - Cloud platforms support Docker natively
   - No installation steps on servers

3. **Isolation**
   - Each project in its own container
   - No conflicting dependencies
   - Clean separation of concerns

4. **Reproducibility**
   - Dockerfile is version-controlled
   - Anyone can rebuild the exact same image
   - Great for CI/CD pipelines

**Key Docker concepts:**

| Concept | Description |
|---------|-------------|
| **Image** | Blueprint/recipe for a container |
| **Container** | Running instance of an image |
| **Dockerfile** | Instructions to build an image |
| **Registry** | Storage for images (Docker Hub, ghcr.io) |
| **Layer** | Cached build step in an image |

**For our Personal Finance Tracker:**
- FastAPI app + dependencies in one container
- Same behavior in dev, staging, and production
- Easy to share with teammates