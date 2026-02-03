---
type: "THEORY"
title: "💻 Complete Production Deployment"
---

```java
STEP 1: Build the application
./mvnw clean package

STEP 2: Create Dockerfile
FROM eclipse-temurin:25-jre
WORKDIR /app
COPY target/myapp.jar app.jar
EXPOSE 8080
HEALTHCHECK --interval=30s --timeout=3s \
  CMD curl -f http://localhost:8080/actuator/health || exit 1
ENTRYPOINT ["java", "-jar", "app.jar"]

STEP 3: Create docker-compose.yml
version: '3.8'
services:
  db:
    image: postgres:15
    environment:
      POSTGRES_DB: myapp
      POSTGRES_USER: appuser
      POSTGRES_PASSWORD: ${DB_PASSWORD}
    volumes:
      - postgres-data:/var/lib/postgresql/data
    
  app:
    build: .
    ports:
      - "8080:8080"
    environment:
      SPRING_PROFILES_ACTIVE: prod
      DATABASE_URL: jdbc:postgresql://db:5432/myapp
      DATABASE_USERNAME: appuser
      DATABASE_PASSWORD: ${DB_PASSWORD}
    depends_on:
      - db
    restart: unless-stopped

volumes:
  postgres-data:

STEP 4: Create .env file (DON'T commit this!)
DB_PASSWORD=your-secure-password-here

STEP 5: Deploy
docker-compose up -d

STEP 6: Check health
curl http://localhost:8080/actuator/health

STEP 7: View logs
docker-compose logs -f app

STEP 8: Update application
./mvnw clean package
docker-compose build app
docker-compose up -d app
```