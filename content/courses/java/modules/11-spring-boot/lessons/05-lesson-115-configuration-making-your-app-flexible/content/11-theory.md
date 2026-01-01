---
type: "THEORY"
title: "Validation with @ConfigurationProperties"
---

Add validation to configuration:

import jakarta.validation.constraints.*;

@ConfigurationProperties(prefix = "app.email")
@Validated  // Enable validation
public record EmailProperties(
    @NotBlank(message = "Email host is required")
    String host,
    
    @Min(value = 1, message = "Port must be positive")
    @Max(value = 65535, message = "Port must be valid")
    int port,
    
    @Email(message = "Invalid email format")
    String username,
    
    @Size(min = 8, message = "Password must be at least 8 characters")
    String password
) { }

If configuration is invalid, Spring Boot FAILS TO START:
- Shows clear error message
- Prevents running with bad config
- Catches errors early!