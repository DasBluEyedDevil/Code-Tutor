---
type: "KEY_POINT"
title: "The CI/CD Pipeline"
---

A CI/CD pipeline automates the journey from code to production:

[Developer pushes code]
        |
        v
[1. SOURCE CONTROL]
   Git repository receives commit
        |
        v
[2. BUILD]
   Compile code, resolve dependencies
   ./mvnw clean package
        |
        v
[3. TEST]
   Run unit tests, integration tests
   ./mvnw test
        |
        v
[4. ANALYZE] (optional)
   Code quality, security scanning
        |
        v
[5. PACKAGE]
   Create deployable artifact (JAR, Docker image)
        |
        v
[6. DEPLOY TO STAGING]
   Test in production-like environment
        |
        v
[7. DEPLOY TO PRODUCTION]
   Release to users
        |
        v
[8. MONITOR]
   Track performance, errors, usage

IF ANY STEP FAILS:
- Pipeline stops
- Team is notified immediately
- Bad code never reaches production