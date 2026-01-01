---
type: "KEY_POINT"
title: "Spring Boot 3.2+ Integration"
---

Spring Boot 3.2+ makes virtual threads easy:

application.properties:
spring.threads.virtual.enabled=true

That's it! All request handling uses virtual threads.

BEFORE (platform threads):
- tomcat.threads.max=200 (default)
- 200 concurrent requests max before queueing
- Each blocked request holds a precious thread

AFTER (virtual threads):
- Thousands of concurrent requests
- Blocking I/O doesn't waste resources
- Same simple @RestController code

@RestController
public class UserController {
    @GetMapping("/users/{id}")
    public User getUser(@PathVariable Long id) {
        // Blocking database call is now FINE!
        return userRepository.findById(id).orElseThrow();
    }
}

JDBC, JPA, RestTemplate - all blocking calls become efficient with virtual threads.