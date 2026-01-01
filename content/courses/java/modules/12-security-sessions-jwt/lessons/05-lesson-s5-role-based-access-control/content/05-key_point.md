---
type: "KEY_POINT"
title: "Controller-Level Security"
---

Apply security at the controller level:

@RestController
@RequestMapping("/api/admin")
@PreAuthorize("hasRole('ADMIN')")
public class AdminController {
    
    // All methods require ADMIN role
    
    @GetMapping("/users")
    public List<User> getUsers() { }  // ADMIN required
    
    @DeleteMapping("/users/{id}")
    public void deleteUser(@PathVariable Long id) { }  // ADMIN required
    
    // Override for specific method
    @GetMapping("/stats")
    @PreAuthorize("hasAnyRole('ADMIN', 'MANAGER')")
    public Stats getStats() { }  // ADMIN or MANAGER
}

// Combine URL and method security
@Configuration
@EnableWebSecurity
@EnableMethodSecurity
public class SecurityConfig {
    
    @Bean
    public SecurityFilterChain filterChain(HttpSecurity http) throws Exception {
        return http
            .authorizeHttpRequests(auth -> auth
                // Coarse-grained URL security
                .requestMatchers("/api/admin/**").hasRole("ADMIN")
                .requestMatchers("/api/**").authenticated()
            )
            // Method security provides fine-grained control
            .build();
    }
}

BEST PRACTICE:
- URL security for broad access patterns
- Method security for business-logic authorization
- Use both for defense in depth