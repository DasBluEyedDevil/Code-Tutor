---
type: "THEORY"
title: "Docker Compose for Full Stack"
---

BOTH PATHS (with differences)

Docker Compose orchestrates multiple containers, making it easy to run the complete application stack locally or in staging environments.

**Thymeleaf path:** You only need TWO services: postgres and api. Remove the frontend service entirely -- your Thymeleaf templates are served by the Spring Boot app.

**React path:** You need THREE services: postgres, api, and frontend. The React app is built and served by nginx.

The docker-compose.yml below shows the full React stack. Thymeleaf users: simply delete the frontend service and you are done.

```yaml
# docker-compose.yml
version: '3.8'

services:
  # PostgreSQL Database
  postgres:
    image: postgres:17-alpine
    container_name: taskmanager-db
    environment:
      POSTGRES_DB: taskmanager
      POSTGRES_USER: taskmanager
      POSTGRES_PASSWORD: ${DB_PASSWORD:-localdev123}
    volumes:
      - postgres_data:/var/lib/postgresql/data
    ports:
      - "5432:5432"
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U taskmanager"]
      interval: 10s
      timeout: 5s
      retries: 5

  # Spring Boot Backend
  api:
    build:
      context: .
      dockerfile: Dockerfile
    container_name: taskmanager-api
    depends_on:
      postgres:
        condition: service_healthy
    environment:
      SPRING_DATASOURCE_URL: jdbc:postgresql://postgres:5432/taskmanager
      SPRING_DATASOURCE_USERNAME: taskmanager
      SPRING_DATASOURCE_PASSWORD: ${DB_PASSWORD:-localdev123}
      SPRING_JPA_HIBERNATE_DDL_AUTO: update
      JWT_SECRET: ${JWT_SECRET:-dev-secret-key-change-in-production}
      JWT_EXPIRATION: 86400000
      CORS_ALLOWED_ORIGINS: http://localhost:5173,http://localhost:3000
    ports:
      - "8080:8080"
    healthcheck:
      test: ["CMD", "wget", "--quiet", "--tries=1", "--spider", "http://localhost:8080/actuator/health"]
      interval: 30s
      timeout: 10s
      retries: 3
      start_period: 40s

  # React Frontend
  frontend:
    build:
      context: ./frontend
      dockerfile: Dockerfile
    container_name: taskmanager-frontend
    depends_on:
      api:
        condition: service_healthy
    environment:
      VITE_API_URL: http://localhost:8080/api
    ports:
      - "3000:80"

volumes:
  postgres_data:
```

Frontend Dockerfile (frontend/Dockerfile):
```dockerfile
# Stage 1: Build React app
FROM node:20-alpine AS builder

WORKDIR /app

# Install dependencies
COPY package*.json ./
RUN npm ci

# Copy source and build
COPY . .
RUN npm run build

# Stage 2: Serve with nginx
FROM nginx:alpine

# Copy built files
COPY --from=builder /app/dist /usr/share/nginx/html

# Custom nginx config for SPA routing
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
```

Nginx config for SPA (frontend/nginx.conf):
```nginx
server {
    listen 80;
    root /usr/share/nginx/html;
    index index.html;

    # Handle client-side routing
    location / {
        try_files $uri $uri/ /index.html;
    }

    # Proxy API requests to backend
    location /api {
        proxy_pass http://api:8080;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
    }
}
```

Run the full stack:
```bash
# Start all services
docker-compose up -d

# View logs
docker-compose logs -f

# Stop all services
docker-compose down

# Stop and remove volumes (fresh start)
docker-compose down -v
```