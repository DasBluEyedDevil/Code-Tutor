---
type: "WARNING"
title: "Common Pitfalls"
---

Common Docker mistakes:

1. **Copying node_modules** (huge and wrong!):
   ```dockerfile
   # WRONG! Copies local node_modules
   COPY . .
   # This includes node_modules which:
   # - May have wrong binaries for Linux
   # - Is HUGE
   # - Defeats purpose of Docker

   # CORRECT! Use .dockerignore
   # .dockerignore:
   node_modules

   # Then install in Docker
   COPY package.json bun.lockb ./
   RUN bun install
   COPY . .
   ```

2. **Missing .dockerignore**:
   ```
   # Always create .dockerignore!
   node_modules
   .git
   .env
   .env.*
   dist
   *.log
   ```

3. **Running as root** (security risk):
   ```dockerfile
   # Add non-root user
   FROM oven/bun:1-slim

   # Create app user
   RUN addgroup --system app && adduser --system --group app
   USER app

   WORKDIR /app
   COPY --chown=app:app . .
   ```

4. **Not using multi-stage builds**:
   ```dockerfile
   # WRONG! Image is 1GB+
   FROM node:20
   COPY . .
   RUN npm install
   CMD ["node", "index.js"]

   # CORRECT! Image is ~150MB
   FROM oven/bun:1-slim
   COPY --from=build /app/dist ./dist
   CMD ["bun", "dist/index.js"]
   ```

5. **Hardcoded secrets**:
   ```dockerfile
   # NEVER DO THIS!
   ENV DATABASE_URL=postgres://user:password@db/app

   # CORRECT: Pass at runtime
   # docker run -e DATABASE_URL=... my-app
   ```

6. **Wrong platform** (M1/M2 Mac issue):
   ```bash
   # Building on M1 Mac for Linux servers
   # WRONG! Uses arm64
   docker build -t my-app .

   # CORRECT! Specify platform
   docker build --platform linux/amd64 -t my-app .
   ```

7. **No health check**:
   ```dockerfile
   # Always add health check!
   HEALTHCHECK --interval=30s --timeout=3s \
     CMD curl -f http://localhost:3000/health || exit 1
   ```

8. **Forgetting to expose port**:
   ```dockerfile
   # Document which port (doesn't publish, just documents)
   EXPOSE 3000

   # Then when running:
   docker run -p 3000:3000 my-app
   ```