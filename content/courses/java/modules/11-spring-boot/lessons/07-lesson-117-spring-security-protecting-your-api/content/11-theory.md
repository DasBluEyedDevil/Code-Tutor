---
type: "THEORY"
title: "💻 Complete Security Configuration"
---

```java
SecurityConfig.java:

@Configuration
@EnableWebSecurity
@EnableMethodSecurity
public class SecurityConfig {
    
    @Bean
    public SecurityFilterChain securityFilterChain(HttpSecurity http) 
            throws Exception {
        http
            .csrf(csrf -> csrf.disable())  // Disable for REST APIs
            .authorizeHttpRequests(auth -> auth
                // Public endpoints
                .requestMatchers("/api/auth/**").permitAll()
                .requestMatchers("/api/public/**").permitAll()
                
                // User endpoints
                .requestMatchers("/api/users/**").hasAnyRole("USER", "ADMIN")
                
                // Admin endpoints
                .requestMatchers("/api/admin/**").hasRole("ADMIN")
                
                // All other requests need authentication
                .anyRequest().authenticated()
            )
            .httpBasic(Customizer.withDefaults())  // Enable HTTP Basic
            .sessionManagement(session -> session
                .sessionCreationPolicy(SessionCreationPolicy.STATELESS)
            );  // Stateless for REST APIs
        
        return http.build();
    }
    
    @Bean
    public PasswordEncoder passwordEncoder() {
        return new BCryptPasswordEncoder();
    }
}

This configuration:
✓ Allows public access to /api/auth/** and /api/public/**
✓ Requires USER or ADMIN role for /api/users/**
✓ Requires ADMIN role for /api/admin/**
✓ Uses BCrypt for password hashing
✓ Stateless sessions (REST API pattern)
```