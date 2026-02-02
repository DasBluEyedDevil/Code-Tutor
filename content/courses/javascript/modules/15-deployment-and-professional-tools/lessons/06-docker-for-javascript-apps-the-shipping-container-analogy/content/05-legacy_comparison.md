---
type: "LEGACY_COMPARISON"
title: "Node.js Dockerfile Equivalent"
---

Here's how the same Dockerfile would look with Node.js instead of Bun. The Bun version is faster and produces smaller images.

```dockerfile
# Dockerfile for Node.js (traditional approach)
# Compare with Bun version above

# ============================================
# Stage 1: Install dependencies
# ============================================
FROM node:20-slim AS base
WORKDIR /app

# Copy package files
COPY package.json package-lock.json ./

# Install dependencies (slower than bun install)
RUN npm ci --only=production

# ============================================
# Stage 2: Build
# ============================================
FROM base AS build
WORKDIR /app

COPY . .
RUN npm run build

# ============================================
# Stage 3: Production
# ============================================
FROM node:20-slim AS production
WORKDIR /app

# Copy from build stages
COPY --from=base /app/node_modules ./node_modules
COPY --from=build /app/dist ./dist
COPY package.json ./

ENV NODE_ENV=production
ENV PORT=3000

EXPOSE 3000

HEALTHCHECK --interval=30s --timeout=3s \
  CMD curl -f http://localhost:3000/health || exit 1

CMD ["node", "dist/index.js"]

# ============================================
# Comparison:
# Node.js image: ~200MB
# Bun image: ~100MB
#
# npm ci: ~30 seconds
# bun install: ~5 seconds
# ============================================
```
