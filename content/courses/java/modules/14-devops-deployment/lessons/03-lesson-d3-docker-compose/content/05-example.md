---
type: "EXAMPLE"
title: "Spring Profile for Docker"
---

Create a Docker-specific profile to handle the different database URL:

```properties
# src/main/resources/application.properties
# Default configuration (local development)
spring.datasource.url=jdbc:postgresql://localhost:5432/myapp
spring.datasource.username=postgres
spring.datasource.password=postgres

# src/main/resources/application-docker.properties
# Docker configuration (overrides default)
# Database URL points to Docker service name 'db'
spring.datasource.url=jdbc:postgresql://db:5432/myapp

# Note: Username and password inherited from application.properties
# unless overridden by environment variables
```
