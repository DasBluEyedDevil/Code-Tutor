---
type: "EXAMPLE"
title: "Development Configuration"
---

Here is the typical configuration for local development:



```yaml
# config/development.yaml

# API server configuration
apiServer:
  port: 8080
  publicHost: localhost
  publicPort: 8080
  publicScheme: http

# Insights server for monitoring
insightsServer:
  port: 8081
  publicHost: localhost
  publicPort: 8081
  publicScheme: http

# PostgreSQL database configuration
database:
  host: localhost
  port: 5432
  name: my_app         # Database name (matches your project)
  user: postgres

# Redis configuration for caching and sessions
redis:
  enabled: true
  host: localhost
  port: 6379

# The password is stored separately in passwords.yaml:
# database:
#   password: 'postgres_password'
# redis:
#   password: ''
```
