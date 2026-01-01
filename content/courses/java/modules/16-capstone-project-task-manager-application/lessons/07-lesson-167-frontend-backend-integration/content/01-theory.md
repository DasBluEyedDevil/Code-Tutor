---
type: "THEORY"
title: "Connecting React to Spring Boot API"
---

Now that we have both a fully functional Spring Boot backend and a React frontend, it is time to connect them. This integration is where the full-stack application truly comes together, allowing users to interact with your backend through a beautiful, responsive interface.

The connection between frontend and backend happens through HTTP requests. React makes requests to our Spring Boot REST API endpoints, receives JSON responses, and updates the UI accordingly. We already set up the Axios API service in lesson 16.6 - now we will ensure everything works seamlessly together.

CORS Configuration Reminder:
Your Spring Boot backend must allow requests from the React development server. In SecurityConfig, we configured CORS to allow http://localhost:5173 (Vite's default port). In production, you will update this to your actual frontend domain.

```java
// In SecurityConfig.java - CORS configuration
@Bean
public CorsConfigurationSource corsConfigurationSource() {
    CorsConfiguration configuration = new CorsConfiguration();
    configuration.setAllowedOrigins(Arrays.asList(
        "http://localhost:5173",
        "https://yourdomain.com"
    ));
    configuration.setAllowedMethods(Arrays.asList("GET", "POST", "PUT", "DELETE", "OPTIONS"));
    configuration.setAllowedHeaders(Arrays.asList("*"));
    configuration.setAllowCredentials(true);
    return new UrlBasedCorsConfigurationSource() {{
        registerCorsConfiguration("/**", configuration);
    }};
}
```

Environment Configuration:
Create a .env file in your frontend directory to configure the API URL:

```bash
# frontend/.env
VITE_API_URL=http://localhost:8080/api
```

For production, create .env.production:
```bash
# frontend/.env.production
VITE_API_URL=https://api.yourdomain.com/api
```

Vite automatically loads the correct file based on the build mode. When you run npm run build, it uses .env.production.