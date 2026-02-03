---
type: "EXAMPLE"
title: "Dockerfile for Bun API - Multi-Stage Build"
---

Create a Dockerfile that optimizes for size and security:

```dockerfile
# Dockerfile
# Multi-stage build: first stage builds, second stage runs

# Build stage
FROM oven/bun:1 as builder

WORKDIR /app

# Copy package files
COPY package.json bun.lockb* ./

# Install dependencies
RUN bun install --frozen-lockfile

# Copy source code
COPY tsconfig.json ./
COPY src ./src
COPY prisma ./prisma

# Generate Prisma client
RUN bunx prisma generate

# Production stage
FROM oven/bun:1

WORKDIR /app

# Copy only production dependencies and built assets from builder
COPY --from=builder /app/node_modules ./node_modules
COPY --from=builder /app/src ./src
COPY --from=builder /app/prisma ./prisma
COPY --from=builder /app/package.json ./
COPY --from=builder /app/.bun ./.bun

# Don't run as root
RUN useradd -m -u 1000 appuser
USER appuser

# Expose port
EXPOSE 3000

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
  CMD bun run --eval "console.log(await fetch('http://localhost:3000/').then(r => r.ok ? 'OK' : 'FAIL'))"

# Run the application
CMD ["bun", "run", "src/index.ts"]
```
