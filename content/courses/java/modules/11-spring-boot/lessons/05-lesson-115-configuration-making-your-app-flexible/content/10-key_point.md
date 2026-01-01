---
type: "KEY_POINT"
title: "Why @ConfigurationProperties is Better Than @Value"
---

@VALUE (Old Way):
@Service
public class EmailService {
    @Value("${app.email.host}")
    private String host;
    
    @Value("${app.email.port}")
    private int port;
    
    @Value("${app.email.username}")
    private String username;
    
    @Value("${app.email.password}")
    private String password;
    
    // 4 separate annotations, scattered!
}

@CONFIGURATIONPROPERTIES (Modern Way):
@ConfigurationProperties(prefix = "app.email")
public record EmailProperties(
    String host,
    int port,
    String username,
    String password
) { }

BENEFITS:
✓ TYPE-SAFE: Compile-time checking
✓ GROUPED: All related properties together
✓ IMMUTABLE: Records are immutable by default
✓ VALIDATION: Can use @Validated annotations
✓ AUTOCOMPLETE: IDE helps with property names
✓ REUSABLE: Inject same config into multiple services