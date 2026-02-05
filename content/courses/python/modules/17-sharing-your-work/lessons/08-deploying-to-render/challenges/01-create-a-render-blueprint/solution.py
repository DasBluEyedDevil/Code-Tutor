# Render Blueprint Solution
# This file outputs the render.yaml content that students should create

RENDER_YAML = """services:
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
"""

print("=== Render Blueprint (render.yaml) ===")
print()
print("Save this content to 'render.yaml' in your repo root:")
print()
print(RENDER_YAML)
print()
print("=== Key Features ===")
print("1. Docker-based deployment")
print("2. Auto-deploy on push to main")
print("3. Managed PostgreSQL database")
print("4. Environment variables from database")
print("5. Auto-generated SECRET_KEY")
print()
print("=== Deployment Steps ===")
print("1. Push render.yaml to your repo")
print("2. Go to render.com/new")
print("3. Select 'Blueprint' and connect your repo")
print("4. Render will create all services automatically")
