---
type: "EXAMPLE"
title: "Using --dart-define-from-file"
---


For many variables, use a JSON file:

**Create config files:**



```json
// config/dev.json
{
  "ENVIRONMENT": "dev",
  "API_URL": "http://localhost:3000",
  "ENABLE_LOGGING": true,
  "ANALYTICS_ENABLED": false,
  "SENTRY_DSN": ""
}

// config/staging.json
{
  "ENVIRONMENT": "staging",
  "API_URL": "https://staging-api.myapp.com",
  "ENABLE_LOGGING": true,
  "ANALYTICS_ENABLED": true,
  "SENTRY_DSN": "https://staging@sentry.io/123"
}

// config/prod.json
{
  "ENVIRONMENT": "prod",
  "API_URL": "https://api.myapp.com",
  "ENABLE_LOGGING": false,
  "ANALYTICS_ENABLED": true,
  "SENTRY_DSN": "https://prod@sentry.io/456"
}
```
