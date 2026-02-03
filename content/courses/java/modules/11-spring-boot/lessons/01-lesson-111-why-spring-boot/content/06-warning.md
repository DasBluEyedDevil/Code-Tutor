---
type: "WARNING"
title: "Spring Boot 4.0 Migration and Version Considerations"
---

SPRING BOOT 4.0 NOTES:

1. @MockBean REMOVED (Spring Boot 4.0)
   - @MockBean and @SpyBean are removed (were deprecated in 3.4)
   - Use: @MockitoBean and @MockitoSpyBean
   - Note: Not a direct 1-to-1 replacement
   - @MockitoBean only works on fields, not types

2. VIRTUAL THREADS ARE THE DEFAULT
   - Virtual threads are enabled by default in Spring Boot 4.0
   - No need to set spring.threads.virtual.enabled=true
   - ThreadLocal usage needs review
   - Most modern libraries are compatible

3. JAKARTA EE 11
   - All javax.* imports are jakarta.* (this was done in Spring Boot 3.0)
   - Spring Boot 4.0 uses Jakarta EE 11 + Servlet 6.1
   - Check migration guide at spring.io

4. STRUCTURED LOGGING SETUP
   - Choose format: ecs, logstash, or gelf
   - Configure log aggregation system accordingly
   - Use spring.application.group for multi-app setups

5. DEVELOPMENT VS PRODUCTION
   - This course teaches concepts, not production setup
   - Production apps need: proper security, monitoring, logging
   - Consider Spring Cloud for microservices