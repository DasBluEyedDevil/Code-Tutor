---
type: "THEORY"
title: "Application Configuration"
---

Create your application.yml file in src/main/resources. This YAML configuration is more readable than properties files and supports hierarchical structure.

```yaml
spring:
  application:
    name: taskmanager
  
  datasource:
    url: jdbc:postgresql://localhost:5432/taskmanager
    username: ${DB_USERNAME:taskuser}
    password: ${DB_PASSWORD:localdev123}
    driver-class-name: org.postgresql.Driver
  
  jpa:
    hibernate:
      ddl-auto: validate  # Use Flyway for migrations
    show-sql: true
    properties:
      hibernate:
        format_sql: true
        dialect: org.hibernate.dialect.PostgreSQLDialect
  
  flyway:
    enabled: true
    locations: classpath:db/migration
    baseline-on-migrate: true

server:
  port: 8080

logging:
  level:
    com.taskmanager: DEBUG
    org.springframework.security: DEBUG
```

Key Configuration Explained:

- datasource.url: JDBC connection string. Uses environment variables with defaults for local development.
- jpa.hibernate.ddl-auto: Set to 'validate' because Flyway manages schema changes. Never use 'create' or 'update' in production!
- flyway.enabled: Enables automatic migration on startup. Flyway runs SQL scripts in order.
- Environment Variables: ${DB_PASSWORD:localdev123} means "use DB_PASSWORD if set, otherwise use localdev123". This allows different values per environment.