---
type: "WARNING"
title: "Web Development Considerations"
---

IMPORTANT CONCEPTS FOR REAL-WORLD WEB DEVELOPMENT:

1. HTTPS IS MANDATORY
   - Never use plain HTTP in production
   - All modern browsers flag HTTP as insecure
   - SSL/TLS encrypts data in transit

2. CORS (Cross-Origin Resource Sharing)
   - Browsers block requests to different domains by default
   - Backend must explicitly allow frontend origins
   - Spring Boot: Use @CrossOrigin or configure WebMvcConfigurer

3. THIS COURSE IS SIMPLIFIED
   - Real web apps need authentication, rate limiting
   - Production requires proper error handling and logging
   - Security headers (CSP, HSTS) are essential

4. API DESIGN MATTERS
   - RESTful conventions make APIs predictable
   - Versioning prevents breaking changes
   - Documentation (OpenAPI/Swagger) is essential