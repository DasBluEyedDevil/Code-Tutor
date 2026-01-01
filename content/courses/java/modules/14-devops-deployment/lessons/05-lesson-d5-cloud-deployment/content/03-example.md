---
type: "EXAMPLE"
title: "Deploying to Railway"
---

Step-by-step deployment of your Spring Boot application:

```bash
# Step 1: Prepare your application

# Ensure you have a Dockerfile (Railway will use it)
# Or Railway auto-detects pom.xml and builds with Maven

# Add health endpoint for Railway
# In pom.xml, add Spring Boot Actuator:
<dependency>
    <groupId>org.springframework.boot</groupId>
    <artifactId>spring-boot-starter-actuator</artifactId>
</dependency>

# In application.properties:
management.endpoints.web.exposure.include=health
management.endpoint.health.show-details=always

# Step 2: Configure for production

# application-production.properties
spring.datasource.url=${DATABASE_URL}
server.port=${PORT:8080}
spring.jpa.hibernate.ddl-auto=update

# Note: Railway sets DATABASE_URL and PORT automatically!

# Step 3: Deploy via GitHub

# 1. Push to GitHub
git push origin main

# 2. Go to railway.app
# 3. Click 'New Project'
# 4. Select 'Deploy from GitHub repo'
# 5. Choose your repository
# 6. Railway auto-detects and deploys!

# Step 4: Add PostgreSQL

# In Railway dashboard:
# 1. Click '+ New' in your project
# 2. Select 'Database' > 'PostgreSQL'
# 3. Railway auto-connects and sets DATABASE_URL

# Step 5: Set environment variables

# In Railway dashboard > your service > Variables:
SPRING_PROFILES_ACTIVE=production
JWT_SECRET=your-production-secret
# Railway auto-provides: DATABASE_URL, PORT
```
