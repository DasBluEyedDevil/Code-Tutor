---
type: "WARNING"
title: "Spring Boot Migration and Version Considerations"
---

SPRING BOOT 3.4 MIGRATION NOTES:

1. @MockBean DEPRECATION (Spring Boot 3.4)
   - @MockBean and @SpyBean are deprecated
   - Replace with: @MockitoBean and @MockitoSpyBean
   - Note: Not a direct 1-to-1 replacement
   - @MockitoBean only works on fields, not types

2. VIRTUAL THREADS CAUTION
   - Virtual threads require Java 21+
   - ThreadLocal usage needs review
   - Some libraries may not be compatible
   - Test thoroughly before enabling in production

3. STRUCTURED LOGGING SETUP
   - Choose format: ecs, logstash, or gelf
   - Configure log aggregation system accordingly
   - Use spring.application.group for multi-app setups

FUTURE: SPRING BOOT 4.0 (November 2025)
- All javax.* imports must become jakarta.*
- Check migration guide at spring.io

4. DEVELOPMENT VS PRODUCTION
   - This course teaches concepts, not production setup
   - Production apps need: proper security, monitoring, logging
   - Consider Spring Cloud for microservices