---
type: "THEORY"
title: "Complete Configuration Example (Spring Boot 3.4+)"
---

```java
application.yml:

spring:
  application:
    name: book-store-api
    group: retail-services  # NEW in 3.4: Group related apps
  threads:
    virtual:
      enabled: true  # NEW in 3.4: Enable virtual threads (Java 21+)
  datasource:
    url: jdbc:mysql://localhost:3306/bookstore
    username: ${DB_USER:root}
    password: ${DB_PASSWORD:password}
  jpa:
    hibernate:
      ddl-auto: update
    show-sql: true
  mvc:
    problemdetails:
      enabled: true  # NEW in 3.4: RFC 7807 error responses

server:
  port: 8080

logging:
  structured:
    format:
      console: ecs  # NEW in 3.4: Structured logging (ecs, logstash, gelf)

app:
  features:
    signup-enabled: true
    max-users: 1000
  email:
    host: smtp.gmail.com
    port: 587
    username: ${EMAIL_USER}
    password: ${EMAIL_PASSWORD}

Configuration classes:

@ConfigurationProperties(prefix = "app.features")
public record FeaturesConfig(
    boolean signupEnabled,
    int maxUsers
) { }

@ConfigurationProperties(prefix = "app.email")
@Validated
public record EmailConfig(
    @NotBlank String host,
    @Min(1) @Max(65535) int port,
    @NotBlank String username,
    @NotBlank String password
) { }

Main application:

@SpringBootApplication
@ConfigurationPropertiesScan
public class BookStoreApplication {
    public static void main(String[] args) {
        SpringApplication.run(BookStoreApplication.class, args);
    }
}

Using in service:

@Service
public class UserService {
    private final FeaturesConfig features;
    
    public UserService(FeaturesConfig features) {
        this.features = features;
    }
    
    public boolean canSignup() {
        return features.signupEnabled();
    }
}
```