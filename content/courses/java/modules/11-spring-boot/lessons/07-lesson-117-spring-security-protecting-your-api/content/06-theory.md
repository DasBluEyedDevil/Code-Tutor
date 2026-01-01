---
type: "THEORY"
title: "In-Memory Users - Simple Authentication"
---

For development/testing, store users in memory:

@Bean
public UserDetailsService userDetailsService() {
    UserDetails user = User.builder()
        .username("alice")
        .password(passwordEncoder().encode("password123"))
        .roles("USER")
        .build();
    
    UserDetails admin = User.builder()
        .username("admin")
        .password(passwordEncoder().encode("admin123"))
        .roles("ADMIN")
        .build();
    
    return new InMemoryUserDetailsManager(user, admin);
}

@Bean
public PasswordEncoder passwordEncoder() {
    return new BCryptPasswordEncoder();  // Secure password hashing
}

Now you have:
- Username: alice, Password: password123, Role: USER
- Username: admin, Password: admin123, Role: ADMIN

NEVER store plain passwords!
BCryptPasswordEncoder hashes them securely.