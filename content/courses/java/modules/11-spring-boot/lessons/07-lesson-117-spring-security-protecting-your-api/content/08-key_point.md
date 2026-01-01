---
type: "KEY_POINT"
title: "Method-Level Security with @PreAuthorize"
---

Secure individual methods:

Enable in SecurityConfig:

@Configuration
@EnableWebSecurity
@EnableMethodSecurity  // Enable method-level security
public class SecurityConfig {
    // ...
}

Use in controllers/services:

@RestController
@RequestMapping("/api/users")
public class UserController {
    
    @GetMapping
    @PreAuthorize("hasRole('USER')")  // Requires USER role
    public List<User> getAllUsers() {
        return userService.findAll();
    }
    
    @DeleteMapping("/{id}")
    @PreAuthorize("hasRole('ADMIN')")  // Requires ADMIN role
    public void deleteUser(@PathVariable Long id) {
        userService.delete(id);
    }
    
    @GetMapping("/me")
    @PreAuthorize("isAuthenticated()")  // Any logged-in user
    public User getCurrentUser() {
        return userService.getCurrentUser();
    }
}

EXPRESSIONS:
- hasRole('ROLE'): Has specific role
- hasAnyRole('ROLE1', 'ROLE2'): Has any of these roles
- isAuthenticated(): Logged in
- permitAll(): Anyone can access