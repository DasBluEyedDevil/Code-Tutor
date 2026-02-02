---
type: "THEORY"
title: "Database Configuration in Serverpod"
---

Serverpod uses PostgreSQL as its database. The connection settings are stored in configuration files within the `config/` directory of your server project.

**Configuration Files:**

```
my_app_server/
  config/
    development.yaml    # Local development settings
    staging.yaml        # Staging environment
    production.yaml     # Production deployment
    passwords.yaml      # Sensitive credentials (gitignored)
```

**How Configuration Works:**

1. When you start the server, it loads the appropriate config based on the `--mode` flag
2. The default mode is `development` when running locally
3. Each config file specifies database host, port, name, and credentials
4. Passwords are stored separately in `passwords.yaml` to keep them out of version control

**Environment Separation:**

Development uses Docker containers on localhost. Staging and production connect to remote database servers. This separation ensures you never accidentally modify production data during development.

