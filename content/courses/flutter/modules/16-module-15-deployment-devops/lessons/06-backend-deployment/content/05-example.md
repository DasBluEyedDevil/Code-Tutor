---
type: "EXAMPLE"
title: "Docker Production Setup"
---


Optimized multi-stage Docker builds for production Dart backends:



```dockerfile
## Optimized Production Dockerfile

# Stage 1: Dependencies
FROM dart:stable AS deps

WORKDIR /app

# Copy only dependency files first (better caching)
COPY pubspec.yaml pubspec.lock ./
RUN dart pub get --no-precompile

# Stage 2: Build
FROM deps AS build

COPY . .

# Compile to native executable
RUN dart compile exe bin/main.dart -o bin/server \
    --define=SERVERPOD_ENV=production

# Stage 3: Production Image
FROM alpine:3.19 AS production

# Install required runtime libraries
RUN apk add --no-cache \
    libc6-compat \
    ca-certificates \
    tzdata

# Create non-root user for security
RUN addgroup -S appgroup && adduser -S appuser -G appgroup

WORKDIR /app

# Copy only necessary files
COPY --from=build /app/bin/server /app/
COPY --from=build /app/config/ /app/config/
COPY --from=build /app/web/ /app/web/
COPY --from=build /app/migrations/ /app/migrations/

# Set ownership
RUN chown -R appuser:appgroup /app
USER appuser

# Environment
ENV SERVERPOD_ENV=production
ENV TZ=UTC

# Expose port
EXPOSE 8080

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
    CMD wget --no-verbose --tries=1 --spider http://localhost:8080/health || exit 1

# Run server
CMD ["./server", "--mode", "production"]

---

## docker-compose.production.yml

version: '3.8'

services:
  server:
    build:
      context: .
      dockerfile: Dockerfile
      target: production
    ports:
      - "8080:8080"
    environment:
      - SERVERPOD_ENV=production
      - DATABASE_HOST=postgres
      - DATABASE_PORT=5432
      - DATABASE_NAME=myapp
      - DATABASE_USER=postgres
      - DATABASE_PASSWORD=${DB_PASSWORD}
    depends_on:
      postgres:
        condition: service_healthy
    restart: unless-stopped
    deploy:
      resources:
        limits:
          cpus: '1.0'
          memory: 512M
        reservations:
          cpus: '0.5'
          memory: 256M

  postgres:
    image: postgres:16-alpine
    environment:
      - POSTGRES_DB=myapp
      - POSTGRES_USER=postgres
      - POSTGRES_PASSWORD=${DB_PASSWORD}
    volumes:
      - postgres_data:/var/lib/postgresql/data
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U postgres"]
      interval: 10s
      timeout: 5s
      retries: 5
    restart: unless-stopped

  redis:
    image: redis:7-alpine
    volumes:
      - redis_data:/data
    healthcheck:
      test: ["CMD", "redis-cli", "ping"]
      interval: 10s
      timeout: 5s
      retries: 5
    restart: unless-stopped

volumes:
  postgres_data:
  redis_data:

---

## Build and Deploy Commands

# Build optimized image
docker build -t my-serverpod-app:latest --target production .

# Check image size (should be ~30-50MB)
docker images my-serverpod-app:latest

# Run locally
docker-compose -f docker-compose.production.yml up -d

# Push to registry
docker tag my-serverpod-app:latest registry.example.com/my-serverpod-app:v1.0.0
docker push registry.example.com/my-serverpod-app:v1.0.0
```
