---
type: "EXAMPLE"
title: "Your First Dockerfile"
---

A Dockerfile defines how to build your container image. Here's a production-ready Dockerfile for Spring Boot:

```dockerfile
# Build stage - compiles the application
FROM eclipse-temurin:21-jdk-alpine AS build

# Set working directory inside container
WORKDIR /app

# Copy Maven wrapper and pom.xml first (better caching)
COPY .mvn/ .mvn/
COPY mvnw pom.xml ./

# Download dependencies (cached if pom.xml unchanged)
RUN ./mvnw dependency:go-offline

# Copy source code
COPY src/ src/

# Build the application
RUN ./mvnw clean package -DskipTests

# Runtime stage - smaller image for production
FROM eclipse-temurin:21-jre-alpine

# Create non-root user for security
RUN addgroup -S spring && adduser -S spring -G spring
USER spring:spring

# Set working directory
WORKDIR /app

# Copy JAR from build stage
COPY --from=build /app/target/*.jar app.jar

# Document the port (doesn't actually expose it)
EXPOSE 8080

# Health check for container orchestration
HEALTHCHECK --interval=30s --timeout=3s \
  CMD wget -q --spider http://localhost:8080/actuator/health || exit 1

# Run the application
ENTRYPOINT ["java", "-jar", "app.jar"]
```
