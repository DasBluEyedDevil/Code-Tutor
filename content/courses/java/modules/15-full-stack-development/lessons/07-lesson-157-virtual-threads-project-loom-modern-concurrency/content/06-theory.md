---
type: "THEORY"
title: "Spring Boot with Virtual Threads"
---

Virtual Threads in Spring Boot:

Spring Boot 4.0+: Virtual threads require configuration.
Set spring.threads.virtual.enabled=true in application.properties.

Spring Boot 3.2-3.x: Enable manually:
# application.properties
spring.threads.virtual.enabled=true

BEFORE (Platform Threads):
- Tomcat default: 200 threads
- 200 concurrent requests max
- Request 201 waits in queue

AFTER (Virtual Threads):
- Each request gets its own virtual thread
- Handle 10,000+ concurrent requests
- No thread pool exhaustion!

Your existing code works unchanged:

@RestController
public class UserController {
    @GetMapping("/api/users/{id}")
    public User getUser(@PathVariable Long id) {
        // This blocks waiting for DB - that's FINE now!
        return userRepository.findById(id).orElseThrow();
    }
}

With virtual threads enabled:
- JVM automatically "parks" virtual thread during DB wait
- Platform thread freed to handle other requests
- Virtual thread "unparked" when DB returns
- You don't change ANY code!