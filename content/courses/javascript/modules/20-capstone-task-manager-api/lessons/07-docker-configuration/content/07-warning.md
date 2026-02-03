---
type: "WARNING"
title: "Docker Pitfalls - Common Mistakes"
---

**1. Large Image Sizes**
```dockerfile
# WRONG - Final image includes build tools
FROM oven/bun:1
COPY . .
RUN bun install
CMD ["bun", "run", "src/index.ts"]
# Image size: ~500MB

# RIGHT - Multi-stage build drops build tools
FROM oven/bun:1 as builder
COPY . .
RUN bun install

FROM oven/bun:1
COPY --from=builder /app ./
CMD ["bun", "run", "src/index.ts"]
# Image size: ~100MB
```

**2. Secrets in Docker Images**
```dockerfile
# WRONG - Secret hardcoded in image
FROM oven/bun:1
ENV JWT_SECRET=my-actual-secret
COPY . .

# RIGHT - Use environment variables at runtime
FROM oven/bun:1
COPY . .
# Don't set secrets in image
# Instead: docker run -e JWT_SECRET=$JWT_SECRET ...
```

**3. Volume Mounting Issues**
```yaml
# WRONG - node_modules synced from host (breaks platform differences)
volumes:
  - ./src:/app/src
  - ./node_modules:/app/node_modules

# RIGHT - Use named volume for node_modules
volumes:
  - ./src:/app/src
  - /app/node_modules  # Anonymous volume, not synced
```

**4. Running as Root**
```dockerfile
# WRONG - Container runs as root (security risk)
FROM oven/bun:1
COPY . .
CMD ["bun", "run", "src/index.ts"]

# RIGHT - Create non-root user
FROM oven/bun:1
RUN useradd -m -u 1000 appuser
USER appuser
COPY . .
CMD ["bun", "run", "src/index.ts"]
```

**5. Missing Health Checks**
```yaml
# WRONG - Container might be "running" but not ready
services:
  api:
    build: .

# RIGHT - Kubernetes and load balancers use health checks
services:
  api:
    build: .
    healthcheck:
      test: ["CMD", "curl", "-f", "http://localhost:3000/health/ready"]
      interval: 30s
      retries: 3
```

**6. Not Using .dockerignore**
```
# Create .dockerignore to reduce build context
node_modules/
.git/
.env
*.db
.DS_Store
dist/
.turbo/
```