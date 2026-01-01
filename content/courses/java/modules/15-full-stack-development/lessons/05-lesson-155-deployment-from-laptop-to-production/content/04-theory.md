---
type: "THEORY"
title: "Environment-Specific Configuration"
---

DON'T hardcode production values!

BAD:
spring.datasource.url=jdbc:postgresql://prod-server:5432/db
spring.datasource.password=supersecret123

GOOD: Use Spring Profiles

application.yml (default/shared config):
spring:
  application:
    name: myapp
  jpa:
    hibernate:
      ddl-auto: validate  # Safe default

application-dev.yml (development):
spring:
  datasource:
    url: jdbc:postgresql://localhost:5432/myapp_dev
    username: dev_user
    password: dev_password
  jpa:
    hibernate:
      ddl-auto: create-drop  # Recreate DB each time
    show-sql: true  # Show all SQL queries

application-prod.yml (production):
spring:
  datasource:
    url: ${DATABASE_URL}  # From environment variable!
    username: ${DATABASE_USERNAME}
    password: ${DATABASE_PASSWORD}
  jpa:
    hibernate:
      ddl-auto: validate  # NEVER recreate production DB!
    show-sql: false  # Don't log SQL (performance)

Activate profile when running:
java -jar myapp.jar --spring.profiles.active=prod

Or set environment variable:
export SPRING_PROFILES_ACTIVE=prod
java -jar myapp.jar