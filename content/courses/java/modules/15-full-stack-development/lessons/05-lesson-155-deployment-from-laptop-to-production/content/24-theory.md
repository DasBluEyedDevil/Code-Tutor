---
type: "THEORY"
title: "Logging in Production"
---

Configure proper logging:

application-prod.yml:
logging:
  level:
    root: INFO
    com.myapp: INFO
    org.springframework.web: WARN
  file:
    name: /var/log/myapp/application.log
  pattern:
    console: "%d{yyyy-MM-dd HH:mm:ss} - %msg%n"
    file: "%d{yyyy-MM-dd HH:mm:ss} [%thread] %-5level %logger{36} - %msg%n"

Use structured logging:

@RestController
public class UserController {
    private static final Logger logger = 
        LoggerFactory.getLogger(UserController.class);
    
    @PostMapping("/api/users")
    public User create(@RequestBody User user) {
        logger.info("Creating user: email={}", user.getEmail());
        try {
            User saved = userService.create(user);
            logger.info("User created: id={}, email={}", 
                       saved.getId(), saved.getEmail());
            return saved;
        } catch (DuplicateResourceException e) {
            logger.warn("Duplicate user creation attempt: email={}", 
                       user.getEmail());
            throw e;
        } catch (Exception e) {
            logger.error("Failed to create user: email={}", 
                        user.getEmail(), e);
            throw e;
        }
    }
}

NEVER log sensitive data:
❌ logger.info("User: {}", user.getPassword());
❌ logger.info("Credit card: {}", creditCard);
✓ logger.info("User authenticated: id={}", user.getId());