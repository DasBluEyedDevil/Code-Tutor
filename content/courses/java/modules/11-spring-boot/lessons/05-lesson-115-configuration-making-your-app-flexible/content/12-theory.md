---
type: "THEORY"
title: "Environment-Specific Configuration"
---

Different configs for dev, test, production:

FILE STRUCTURE:
src/main/resources/
  ├── application.yml              (default)
  ├── application-dev.yml          (development)
  ├── application-test.yml         (testing)
  └── application-prod.yml         (production)

application.yml (default):
spring:
  profiles:
    active: dev  # Default profile

application-dev.yml:
spring:
  datasource:
    url: jdbc:mysql://localhost:3306/dev_db
server:
  port: 8080

application-prod.yml:
spring:
  datasource:
    url: jdbc:mysql://prod-server:3306/prod_db
server:
  port: 80

ACTIVATE PROFILE:
1. In application.yml: spring.profiles.active=prod
2. Command line: java -jar app.jar --spring.profiles.active=prod
3. Environment variable: SPRING_PROFILES_ACTIVE=prod

Spring merges default + active profile configs!