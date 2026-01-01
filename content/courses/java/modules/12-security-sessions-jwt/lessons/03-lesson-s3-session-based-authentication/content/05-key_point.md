---
type: "KEY_POINT"
title: "Session Management Configuration"
---

Control session behavior:

@Bean
public SecurityFilterChain filterChain(HttpSecurity http) throws Exception {
    return http
        .sessionManagement(session -> session
            // What to do with session ID on authentication
            .sessionFixation().changeSessionId()  // Safest (default)
            
            // Concurrent session control
            .maximumSessions(1)                   // Only 1 session per user
            .maxSessionsPreventsLogin(true)       // Block new login
            // OR .maxSessionsPreventsLogin(false) // Kick out old session
            
            // Session creation policy
            .sessionCreationPolicy(SessionCreationPolicy.IF_REQUIRED)
            // IF_REQUIRED: Create when needed (default)
            // ALWAYS: Always create
            // NEVER: Never create (but use if exists)
            // STATELESS: Never create or use (for JWT)
            
            // Invalid session handling
            .invalidSessionUrl("/login?expired")
        )
        .build();
}

Concurrent session control prevents:
- Account sharing
- Session persistence after password change
- Forgotten logins on public computers