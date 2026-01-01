---
type: "THEORY"
title: "SecurityFilterChain - Configuring Security (Spring Security 6)"
---

Create SecurityConfig class to customize security:

@Configuration
@EnableWebSecurity
public class SecurityConfig {
    
    @Bean
    public SecurityFilterChain securityFilterChain(HttpSecurity http) throws Exception {
        http
            .authorizeHttpRequests(auth -> auth
                .requestMatchers("/public/**").permitAll()      // Anyone
                .requestMatchers("/api/users/**").hasRole("USER")  // USER role
                .requestMatchers("/api/admin/**").hasRole("ADMIN") // ADMIN role
                .anyRequest().authenticated()  // All others need login
            )
            .formLogin(Customizer.withDefaults())  // Enable login form
            .httpBasic(Customizer.withDefaults()); // Enable basic auth
        
        return http.build();
    }
}

BREAKDOWN:
- authorizeHttpRequests: Define access rules
- requestMatchers: URL patterns to match
- permitAll(): No authentication needed
- hasRole("USER"): Requires USER role
- authenticated(): Any logged-in user
- formLogin: Enable HTML login form
- httpBasic: Enable HTTP Basic Auth (for APIs)