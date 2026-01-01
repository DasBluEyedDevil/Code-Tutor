---
type: "THEORY"
title: "@ConfigurationProperties - Modern Best Practice (Spring Boot 4)"
---

For complex configs, use @ConfigurationProperties with Records:

application.yml:
app:
  email:
    host: smtp.gmail.com
    port: 587
    username: user@example.com
    password: secret123
  features:
    signup-enabled: true
    max-upload-size: 10485760

Java Record (Java 17+):

@ConfigurationProperties(prefix = "app.email")
public record EmailProperties(
    String host,
    int port,
    String username,
    String password
) { }

Enable in main class:

@SpringBootApplication
@ConfigurationPropertiesScan  // Scan for @ConfigurationProperties
public class MyApplication {
    public static void main(String[] args) {
        SpringApplication.run(MyApplication.class, args);
    }
}

Use in services:

@Service
public class EmailService {
    private final EmailProperties emailProps;
    
    public EmailService(EmailProperties emailProps) {
        this.emailProps = emailProps;
    }
    
    public void sendEmail(String to, String message) {
        String host = emailProps.host();  // smtp.gmail.com
        int port = emailProps.port();      // 587
        // Send email...
    }
}