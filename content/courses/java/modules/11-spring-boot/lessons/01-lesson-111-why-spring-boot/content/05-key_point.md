---
type: "KEY_POINT"
title: "Spring Boot 3.4 and Beyond - What's New"
---

Spring Boot 3.4 Key Features (Released November 2024):

1. VIRTUAL THREADS (Project Loom)
   - Enable with: spring.threads.virtual.enabled=true
   - Requires Java 21+, recommended Java 24+
   - HttpClient, OtlpMeterRegistry, Undertow use virtual threads
   - Massive scalability for I/O-heavy applications

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
   - Replaces deprecated @MockBean annotation
   - Part of Spring Framework 6.2 core
   - Use @MockitoSpyBean instead of @SpyBean
   - Standardized mocking in integration tests

5. DOCKER COMPOSE IMPROVEMENTS
   - spring.docker.compose.start.arguments
   - spring.docker.compose.stop.arguments
   - Better container orchestration

SPRING BOOT 4.0 (November 2025 Preview):
- Jakarta EE 11 + Servlet 6.1 baseline
- Java 17+ required, 25 recommended
- HTTP Service Clients (interface-based)
- Built-in API versioning support
- GraalVM v25+ for native images

WHY THIS MATTERS:
- Better observability (structured logging)
- Massive scalability (virtual threads)
- Standardized errors (Problem Details)
- Modern testing (@MockitoBean)