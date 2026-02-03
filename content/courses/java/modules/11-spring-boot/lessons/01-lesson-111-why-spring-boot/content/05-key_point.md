---
type: "KEY_POINT"
title: "Spring Boot 4.0 - What's New"
---

Spring Boot 4.0 Key Features:

1. VIRTUAL THREADS (Project Loom) - ON BY DEFAULT
   - No configuration needed -- virtual threads are the default in Spring Boot 4.0
   - HttpClient, web servers, and schedulers all use virtual threads
   - Massive scalability for I/O-heavy applications
   - No code changes needed -- same blocking code, better performance

2. STRUCTURED LOGGING (Observability)
   - Built-in support for ECS, Logstash, GELF formats
   - Configure: logging.structured.format.console=ecs
   - Use spring.application.group for application grouping
   - Machine-readable logs for log management systems

3. PROBLEM DETAILS (RFC 7807)
   - Standardized error responses for APIs
   - Enable: spring.mvc.problemdetails.enabled=true
   - Returns: type, title, status, detail, instance fields
   - Industry-standard error format

4. @MockitoBean (Testing)
   - Replaces the removed @MockBean annotation
   - Part of Spring Framework 7 core
   - Use @MockitoSpyBean instead of @SpyBean
   - Standardized mocking in integration tests

5. SPRING FRAMEWORK 7 + JAKARTA EE 11
   - Jakarta EE 11 + Servlet 6.1 baseline
   - Java 17+ required, Java 25 recommended
   - HTTP Service Clients (interface-based)
   - Built-in API versioning support
   - GraalVM v25+ for native images

WHY THIS MATTERS:
- Better observability (structured logging)
- Massive scalability (virtual threads by default)
- Standardized errors (Problem Details)
- Modern testing (@MockitoBean)
- Latest Jakarta EE and Spring Framework