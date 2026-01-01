---
type: "EXAMPLE"
title: "Dockerfile for Bun Apps"
---

Complete Dockerfile using multi-stage builds for production-ready images.

```dockerfile
# Dockerfile for Bun/Hono API
# Multi-stage build for smallest possible image

# ============================================
# Stage 1: Install dependencies
# ============================================
FROM oven/bun:1.1 AS base
WORKDIR /app

# Copy package files first (better caching)
COPY package.json bun.lockb ./

# Install dependencies
RUN bun install --frozen-lockfile --production

# ============================================
# Stage 2: Build (if needed)
# ============================================
FROM base AS build
WORKDIR /app

# Copy source code
COPY . .

# If you have a build step (TypeScript, etc.)
# RUN bun run build

# ============================================
# Stage 3: Production image
# ============================================
FROM oven/bun:1.1-slim AS production
WORKDIR /app

# Install curl for health checks (slim images don't include it)
RUN apt-get update && apt-get install -y --no-install-recommends curl && rm -rf /var/lib/apt/lists/*

# Copy only what we need from build stage
COPY --from=base /app/node_modules ./node_modules
COPY --from=build /app/src ./src
COPY --from=build /app/package.json ./

# Set production environment
ENV NODE_ENV=production
ENV PORT=3000

# Expose port
EXPOSE 3000

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
  CMD curl -f http://localhost:3000/health || exit 1

# Run the app
CMD ["bun", "run", "src/index.ts"]

# ============================================
# Image size comparison:
# - Node.js full: ~1GB
# - Node.js slim: ~200MB
# - Bun slim: ~150MB
# - Bun distroless: ~100MB
# ============================================
```
