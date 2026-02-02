---
type: "EXAMPLE"
title: "Railway Deployment"
---


Railway offers the simplest deployment experience for Dart backends with automatic builds and managed PostgreSQL:



```yaml
## Step 1: Prepare Your Serverpod Project

# Ensure your project structure is correct
my_app/
├── my_app_server/       # This is what we deploy
│   ├── Dockerfile
│   ├── lib/
│   └── config/
├── my_app_client/
└── my_app_flutter/

## Step 2: Create Dockerfile for Railway

# In my_app_server/Dockerfile
FROM dart:stable AS build

WORKDIR /app
COPY pubspec.* ./
RUN dart pub get

COPY . .
RUN dart compile exe bin/main.dart -o bin/server

FROM alpine:latest
RUN apk add --no-cache libc6-compat

WORKDIR /app
COPY --from=build /app/bin/server /app/
COPY --from=build /app/config/ /app/config/
COPY --from=build /app/web/ /app/web/

EXPOSE 8080
ENV SERVERPOD_ENV=production

CMD ["./server", "--mode", "production"]

## Step 3: Deploy to Railway

# 1. Create Railway account at railway.app
# 2. Install Railway CLI
npm install -g @railway/cli

# 3. Login and initialize
railway login
cd my_app_server
railway init

# 4. Add PostgreSQL database
railway add --database postgresql

# 5. Configure environment variables
railway variables set SERVERPOD_ENV=production
railway variables set DATABASE_URL=${{Postgres.DATABASE_URL}}

# 6. Deploy
railway up

## Step 4: Configure Serverpod for Railway

# config/production.yaml
apiServer:
  port: 8080
  publicHost: your-app.up.railway.app
  publicPort: 443
  publicScheme: https

insightsServer:
  port: 8081
  publicHost: your-app.up.railway.app
  publicPort: 443
  publicScheme: https

webServer:
  port: 8082
  publicHost: your-app.up.railway.app
  publicPort: 443
  publicScheme: https

database:
  host: $DATABASE_HOST
  port: $DATABASE_PORT
  name: $DATABASE_NAME
  user: $DATABASE_USER
  password: $DATABASE_PASSWORD

## Railway Provides These Automatically:
# - SSL/TLS termination
# - Custom domains (add in dashboard)
# - Automatic deployments from GitHub
# - Built-in monitoring and logs
```
