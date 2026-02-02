---
type: "EXAMPLE"
title: "Creating the render.yaml Blueprint"
---

**render.yaml = Your deployment blueprint**

This file tells Render exactly how to deploy your application. Place it in your repository root.

```yaml
# render.yaml - Render Blueprint for Personal Finance Tracker
# Place this file in your repository root

services:
  # Web service (FastAPI application)
  - type: web
    name: finance-api
    runtime: docker
    dockerfilePath: ./Dockerfile
    
    # Health check endpoint
    healthCheckPath: /health
    
    # Environment variables
    envVars:
      # Database connection (auto-populated from database)
      - key: DATABASE_URL
        fromDatabase:
          name: finance-db
          property: connectionString
      
      # Auto-generate a secure secret key
      - key: SECRET_KEY
        generateValue: true
      
      # Environment setting
      - key: ENVIRONMENT
        value: production
      
      # Disable debug mode
      - key: DEBUG
        value: "false"
    
    # Auto-deploy when main branch updates
    autoDeploy: true
    
    # Branch to deploy from
    branch: main

# PostgreSQL database
databases:
  - name: finance-db
    plan: free  # Free tier for 90 days
    databaseName: finance_tracker
    user: finance_user
```
