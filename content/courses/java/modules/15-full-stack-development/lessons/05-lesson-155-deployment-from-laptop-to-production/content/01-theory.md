---
type: "THEORY"
title: "The Journey from Development to Production"
---

Your application works on your laptop. Great!
But how do users actually use it?

THE DEPLOYMENT PIPELINE:

1. DEVELOPMENT (Your laptop):
   - Run: ./mvnw spring-boot:run
   - Database: localhost:5432
   - Hot reload enabled
   - Debug mode on

2. BUILD (Create executable):
   - Package as JAR file
   - All dependencies included
   - Single file to deploy

3. TEST ENVIRONMENT (Pre-production):
   - Test database
   - Similar to production
   - Catch bugs before users see them

4. PRODUCTION (The real thing):
   - Real database with real data
   - Real users
   - Must be reliable, secure, fast
   - Logging and monitoring essential

GOAL: Make deployment smooth, repeatable, and safe!