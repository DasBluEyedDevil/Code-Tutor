# render.yaml for Flask Application

services:
  - type: ____
    name: myapp-api
    runtime: ____
    dockerfilePath: ./Dockerfile
    healthCheckPath: ____
    
    envVars:
      - key: DATABASE_URL
        fromDatabase:
          name: ____
          property: connectionString
      
      - key: SECRET_KEY
        ____: true
    
    autoDeploy: ____
    branch: ____

databases:
  - name: ____
    plan: free