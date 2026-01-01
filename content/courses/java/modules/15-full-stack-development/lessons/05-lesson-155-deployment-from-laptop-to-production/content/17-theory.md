---
type: "THEORY"
title: "Production-Ready Features"
---

Add Spring Boot Actuator for monitoring:

pom.xml:
<dependency>
    <groupId>org.springframework.boot</groupId>
    <artifactId>spring-boot-starter-actuator</artifactId>
</dependency>

application-prod.yml:
management:
  endpoints:
    web:
      exposure:
        include: health,info,metrics
  endpoint:
    health:
      show-details: when-authorized

Endpoints:
GET /actuator/health - Is app running?
{
  "status": "UP",
  "components": {
    "db": {"status": "UP"},
    "diskSpace": {"status": "UP"}
  }
}

GET /actuator/info - App information
GET /actuator/metrics - Performance metrics

Custom health check:
@Component
public class DatabaseHealthIndicator implements HealthIndicator {
    private final UserRepository userRepository;
    
    public DatabaseHealthIndicator(UserRepository userRepository) {
        this.userRepository = userRepository;
    }
    
    @Override
    public Health health() {
        try {
            long count = userRepository.count();
            return Health.up()
                .withDetail("database", "reachable")
                .withDetail("userCount", count)
                .build();
        } catch (Exception e) {
            return Health.down()
                .withDetail("error", e.getMessage())
                .build();
        }
    }
}