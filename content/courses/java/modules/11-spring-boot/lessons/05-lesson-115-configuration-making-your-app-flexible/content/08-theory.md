---
type: "THEORY"
title: "@Value - Simple Property Injection"
---

@Value injects single values from configuration:

application.yml:
app:
  name: My App
  max-users: 100

Java code:

@Service
public class AppService {
    
    @Value("${app.name}")
    private String appName;
    
    @Value("${app.max-users}")
    private int maxUsers;
    
    @Value("${app.feature.enabled:false}")  // Default value
    private boolean featureEnabled;
    
    public void printConfig() {
        System.out.println("App: " + appName);
        System.out.println("Max users: " + maxUsers);
    }
}

SYNTAX:
@Value("${property.name}") - Read from config
@Value("${property.name:default}") - With default value

USE @Value FOR:
✓ Simple, individual properties
✓ One or two values

DON'T USE @Value FOR:
✗ Complex, grouped configurations
✗ Many related properties