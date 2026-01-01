---
type: "EXAMPLE"
title: "Real Logging Example"
---

A proper logging setup using SLF4J with different log levels for authentication flow.

```java
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class UserService {
    private static final Logger logger = 
        LoggerFactory.getLogger(UserService.class);

    public User authenticateUser(String username, String password) {
        logger.debug("Attempting authentication for user: {}", username);
        
        User user = userRepository.findByUsername(username);
        
        if (user == null) {
            logger.warn("Authentication failed: user not found - {}", username);
            return null;
        }
        
        if (!passwordMatches(user, password)) {
            logger.warn("Authentication failed: incorrect password for {}", username);
            return null;
        }
        
        logger.info("User {} authenticated successfully", username);
        return user;
    }
}

// OUTPUT (with timestamps automatically added):
// 2025-01-15 10:30:15 DEBUG UserService - Attempting authentication for user: alice
// 2025-01-15 10:30:15 INFO UserService - User alice authenticated successfully
```
