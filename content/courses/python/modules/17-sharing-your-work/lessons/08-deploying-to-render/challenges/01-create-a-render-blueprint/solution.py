# render.yaml for Flask Application

services:
  - type: web
    name: myapp-api
    runtime: docker
    dockerfilePath: ./Dockerfile
    healthCheckPath: /api/health
    
    envVars:
      - key: DATABASE_URL
        fromDatabase:
          name: myapp-db
          property: connectionString
      
      - key: SECRET_KEY
        generateValue: true
    
    autoDeploy: true
    branch: main

databases:
  - name: myapp-db
    plan: free