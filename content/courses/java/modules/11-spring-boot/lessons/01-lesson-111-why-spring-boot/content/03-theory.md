---
type: "THEORY"
title: "Your First Spring Boot Endpoint"
---

A REST API endpoint in Spring Boot:

@RestController
public class HelloController {
    
    @GetMapping("/hello")
    public String sayHello() {
        return "Hello, World!";
    }
}

That's it! Spring Boot handles:
- Starting a web server
- Routing GET /hello to this method
- Converting return value to HTTP response

Visit http://localhost:8080/hello → See "Hello, World!"