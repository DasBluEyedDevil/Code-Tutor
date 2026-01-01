---
type: "THEORY"
title: "Deployment Options"
---

Where can you run your Spring Boot application?

TRADITIONAL HOSTING (VPS):
- DigitalOcean Droplets, AWS EC2, Linode
- You manage the server, OS, Java, etc.
- Full control, full responsibility
- Good for: Learning Linux, complex setups

PLATFORM AS A SERVICE (PaaS):
- Railway, Heroku, Render, Fly.io
- Upload code, platform handles the rest
- Less control, less work
- Good for: Getting started, MVPs, side projects

CONTAINER PLATFORMS:
- AWS ECS/EKS, Google Cloud Run, Azure Container Apps
- Run Docker containers at scale
- More control than PaaS, less than VPS
- Good for: Production workloads

SERVERLESS:
- AWS Lambda, Google Cloud Functions
- Pay only when code runs
- Limited to short-lived functions
- Good for: Event-driven, low-traffic apps

FOR LEARNING:
We'll use Railway because:
- Free tier for hobby projects
- Git-based deployments
- Automatic HTTPS
- Easy PostgreSQL addon
- No credit card required to start