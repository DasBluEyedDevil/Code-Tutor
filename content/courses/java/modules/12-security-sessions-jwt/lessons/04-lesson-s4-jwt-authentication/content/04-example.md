---
type: "EXAMPLE"
title: "JWT Security Configuration"
---

Complete Spring Security 7 JWT setup:

```java
@Configuration
@EnableWebSecurity
public class SecurityConfig {

    private final JwtAuthenticationFilter jwtAuthFilter;
    private final AuthenticationProvider authenticationProvider;

    @Bean
    public SecurityFilterChain securityFilterChain(HttpSecurity http) throws Exception {
        return http
            // Disable CSRF - not needed for stateless JWT
            .csrf(csrf -> csrf.disable())
            
            // Stateless session - no server-side session
            .sessionManagement(session ->
                session.sessionCreationPolicy(SessionCreationPolicy.STATELESS))
            
            // Authorization rules
            .authorizeHttpRequests(auth -> auth
                // Public endpoints
                .requestMatchers("/api/auth/**").permitAll()
                .requestMatchers("/api/public/**").permitAll()
                // Admin endpoints
                .requestMatchers("/api/admin/**").hasRole("ADMIN")
                // All other API endpoints require authentication
                .anyRequest().authenticated()
            )
            
            // Set authentication provider
            .authenticationProvider(authenticationProvider)
            
            // Add JWT filter BEFORE username/password filter
            .addFilterBefore(jwtAuthFilter, 
                UsernamePasswordAuthenticationFilter.class)
            
            .build();
    }
}

// JWT Authentication Filter
@Component
@RequiredArgsConstructor
public class JwtAuthenticationFilter extends OncePerRequestFilter {

    private final JwtService jwtService;
    private final UserDetailsService userDetailsService;

    @Override
    protected void doFilterInternal(
            HttpServletRequest request,
            HttpServletResponse response,
            FilterChain filterChain) throws ServletException, IOException {
        
        // Extract Authorization header
        final String authHeader = request.getHeader("Authorization");
        
        // Check for Bearer token
        if (authHeader == null || !authHeader.startsWith("Bearer ")) {
            filterChain.doFilter(request, response);
            return;
        }
        
        // Extract token (remove "Bearer " prefix)
        final String jwt = authHeader.substring(7);
        final String username = jwtService.extractUsername(jwt);
        
        // If username extracted and not already authenticated
        if (username != null && 
            SecurityContextHolder.getContext().getAuthentication() == null) {
            
            UserDetails userDetails = userDetailsService
                .loadUserByUsername(username);
            
            // Validate token
            if (jwtService.isTokenValid(jwt, userDetails)) {
                // Create authentication token
                UsernamePasswordAuthenticationToken authToken = 
                    new UsernamePasswordAuthenticationToken(
                        userDetails,
                        null,
                        userDetails.getAuthorities()
                    );
                
                authToken.setDetails(
                    new WebAuthenticationDetailsSource()
                        .buildDetails(request)
                );
                
                // Set authentication in context
                SecurityContextHolder.getContext()
                    .setAuthentication(authToken);
            }
        }
        
        filterChain.doFilter(request, response);
    }
}
```
