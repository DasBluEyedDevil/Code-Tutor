---
type: "WARNING"
title: "CORS: The Security Barrier"
---

When your React app (port 5173) tries to call your Spring Boot API (port 8080), the browser will BLOCK the request:

Access to fetch at 'http://localhost:8080/api/users' 
from origin 'http://localhost:5173' has been blocked by CORS policy.

CORS (Cross-Origin Resource Sharing) is a security feature. Browsers prevent JavaScript from making requests to different origins (domain + port) by default.

WITHOUT CORS CONFIG:
- React (localhost:5173) cannot call API (localhost:8080)
- Browser blocks the request before it reaches your server
- Your perfectly good API is inaccessible

SOLUTION:
Configure Spring Boot to ALLOW requests from your React app:

@Configuration
public class CorsConfig implements WebMvcConfigurer {
    @Override
    public void addCorsMappings(CorsRegistry registry) {
        registry.addMapping("/api/**")
            .allowedOrigins("http://localhost:5173")
            .allowedMethods("GET", "POST", "PUT", "DELETE")
            .allowedHeaders("*")
            .allowCredentials(true);
    }
}

We'll cover this in detail in Lesson R.4.