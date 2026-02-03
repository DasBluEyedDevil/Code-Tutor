---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Docker fundamentals for JavaScript developers:

1. **Basic Dockerfile Commands**:
   ```dockerfile
   FROM oven/bun:1        # Base image (major version tag)
   WORKDIR /app           # Set working directory
   COPY . .               # Copy files
   RUN bun install        # Run command during build
   EXPOSE 3000            # Document which port
   CMD ["bun", "start"]   # Command to run container
   ```

2. **Multi-Stage Builds** (smaller images):
   ```dockerfile
   # Stage 1: Build
   FROM oven/bun:1 AS builder
   COPY . .
   RUN bun install && bun run build

   # Stage 2: Production (only copy what's needed)
   FROM oven/bun:1-slim
   COPY --from=builder /app/dist ./dist
   CMD ["bun", "dist/index.js"]
   ```

3. **.dockerignore** (like .gitignore for Docker):
   ```
   node_modules
   .git
   .env
   .env.local
   dist
   *.log
   .DS_Store
   Dockerfile
   docker-compose.yml
   README.md
   ```

4. **Building Images**:
   ```bash
   # Build image
   docker build -t my-app .

   # Build with tag (for versioning)
   docker build -t my-app:1.0.0 .
   docker build -t my-app:latest .

   # Build for specific platform
   docker build --platform linux/amd64 -t my-app .
   ```

5. **Running Containers**:
   ```bash
   # Run container
   docker run -p 3000:3000 my-app

   # Run with environment variables
   docker run -p 3000:3000 -e DATABASE_URL=... my-app

   # Run with env file
   docker run -p 3000:3000 --env-file .env my-app

   # Run in background (detached)
   docker run -d -p 3000:3000 my-app

   # View running containers
   docker ps

   # Stop container
   docker stop <container-id>
   ```

6. **docker-compose for Local Development**:
   ```yaml
   # docker-compose.yml
   version: '3.8'

   services:
     api:
       build: .
       ports:
         - "3000:3000"
       environment:
         - DATABASE_URL=postgres://postgres:password@db:5432/myapp
       depends_on:
         - db

     db:
       image: postgres:16
       environment:
         POSTGRES_USER: postgres
         POSTGRES_PASSWORD: password
         POSTGRES_DB: myapp
       ports:
         - "5432:5432"
       volumes:
         - postgres_data:/var/lib/postgresql/data

   volumes:
     postgres_data:
   ```

7. **Common Commands**:
   ```bash
   # Start services
   docker compose up

   # Start in background
   docker compose up -d

   # Rebuild and start
   docker compose up --build

   # Stop services
   docker compose down

   # Stop and remove volumes (reset database!)
   docker compose down -v

   # View logs
   docker compose logs -f api
   ```

8. **Pushing to Registry**:
   ```bash
   # Login to Docker Hub
   docker login

   # Tag for registry
   docker tag my-app username/my-app:latest

   # Push to registry
   docker push username/my-app:latest

   # Or use GitHub Container Registry
   docker tag my-app ghcr.io/username/my-app:latest
   docker push ghcr.io/username/my-app:latest
   ```