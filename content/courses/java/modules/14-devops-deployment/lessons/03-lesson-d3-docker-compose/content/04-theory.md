---
type: "THEORY"
title: "Understanding the Configuration"
---

KEY SECTIONS EXPLAINED:

services:
  Each service is a container. 'app' and 'db' are service names.
  These names become DNS hostnames on the network.
  Your app connects to 'db:5432' not 'localhost:5432'.

build:
  context: .            # Directory with Dockerfile
  dockerfile: Dockerfile # Which Dockerfile to use

image:
  image: postgres:16-alpine   # Use pre-built image
  Don't use 'latest' - pin versions!

ports:
  - "8080:8080"    # host:container
  - "5432:5432"    # Expose DB for local development

depends_on:
  db:
    condition: service_healthy   # Wait for health check
  Without condition, only waits for container to START
  (not for service to be READY)

environment:
  Pass environment variables to container
  Spring Boot reads these automatically
  SPRING_DATASOURCE_URL overrides application.properties

volumes:
  postgres_data:/var/lib/postgresql/data
  Named volume - data persists when container stops
  Without this, database is wiped on restart!

healthcheck:
  Container orchestrators use this to know when
  service is actually ready, not just running

restart: unless-stopped
  Auto-restart on crash (but not if manually stopped)