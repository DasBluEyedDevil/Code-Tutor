---
type: "KEY_POINT"
title: "Spring Boot 4.0: Virtual Threads by Default"
---

Spring Boot 4.0 enables virtual threads by default -- no configuration needed!

BEFORE (platform threads, older Spring Boot):
- tomcat.threads.max=200 (default)
- 200 concurrent requests max before queueing
- Each blocked request holds a precious thread

AFTER (virtual threads, Spring Boot 4.0):
- Thousands of concurrent requests
- Blocking I/O doesn't waste resources
- Same simple @RestController code
- Zero configuration required

@RestController
public class UserController {
    @GetMapping("/users/{id}")
    public User getUser(@PathVariable Long id) {
        // Blocking database call is now FINE!
        return userRepository.findById(id).orElseThrow();
    }
}

JDBC, JPA, RestTemplate - all blocking calls become efficient with virtual threads.

HISTORICAL NOTE: In Spring Boot 3.2-3.x, you had to manually enable virtual threads with spring.threads.virtual.enabled=true. Spring Boot 4.0 made this the default.