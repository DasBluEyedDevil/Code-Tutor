---
type: "KEY_POINT"
title: "Environment Variables for Secrets"
---

NEVER commit secrets to Git!

❌ BAD - Hardcoded in application.yml:
spring.datasource.password=mysecretpassword
jwt.secret=my-super-secret-key-12345

✓ GOOD - Environment variables:
spring.datasource.password=${DATABASE_PASSWORD}
jwt.secret=${JWT_SECRET}

Set on server:
export DATABASE_PASSWORD='actual-secret-password'
export JWT_SECRET='actual-jwt-secret-key'
java -jar myapp.jar

Or pass directly:
java -jar myapp.jar \
  --spring.datasource.password=secret123 \
  --jwt.secret=my-jwt-key

Spring Boot automatically converts:
DATABASE_URL → spring.datasource.url
DATABASE_USERNAME → spring.datasource.username
DATABASE_PASSWORD → spring.datasource.password

Why environment variables?
✓ Different values per environment (dev, test, prod)
✓ Secrets not in Git (security)
✓ Easy to change without rebuilding
✓ Cloud platforms (AWS, Azure, Heroku) support them