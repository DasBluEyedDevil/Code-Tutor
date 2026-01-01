---
type: "WARNING"
title: "Common Docker Mistakes"
---

MISTAKE 1: Running as root

# BAD - container runs as root
ENTRYPOINT ["java", "-jar", "app.jar"]

# GOOD - create and use non-root user
RUN addgroup -S spring && adduser -S spring -G spring
USER spring:spring

MISTAKE 2: No .dockerignore

# Without .dockerignore, COPY . . includes:
# - target/ (huge, unnecessary)
# - .git/ (huge, security risk)
# - node_modules/ (if frontend exists)
# - IDE files

MISTAKE 3: Using 'latest' tag in production

# BAD - 'latest' can change unexpectedly
docker pull myapp:latest

# GOOD - use specific version
docker pull myapp:1.2.3
docker pull myapp:$(git rev-parse --short HEAD)

MISTAKE 4: Not handling signals

Spring Boot needs graceful shutdown:

ENTRYPOINT ["java", "-jar", "app.jar"]

Add to application.properties:
server.shutdown=graceful
spring.lifecycle.timeout-per-shutdown-phase=30s

MISTAKE 5: Hardcoding configuration

# BAD
ENV DB_URL=jdbc:postgresql://localhost:5432/mydb

# GOOD - configure at runtime
docker run -e DB_URL=jdbc:postgresql://prod-db:5432/mydb myapp