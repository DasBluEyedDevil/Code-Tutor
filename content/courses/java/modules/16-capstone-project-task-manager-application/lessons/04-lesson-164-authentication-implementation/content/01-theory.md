---
type: "THEORY"
title: "Spring Security Configuration Overview"
---

Spring Security uses a functional, lambda-based configuration style. Understanding how it works is crucial for building secure applications.

The Security Filter Chain:
Spring Security works by intercepting HTTP requests through a chain of filters. Each filter performs a specific security function: authentication, authorization, CSRF protection, session management, and more. We configure this chain using the SecurityFilterChain bean.

Key Concepts:

1. SecurityFilterChain: The primary configuration point. It defines which requests require authentication, which are public, and how authentication should work.

2. AuthenticationManager: Coordinates authentication by delegating to AuthenticationProvider implementations. For username/password auth, it uses UserDetailsService to load user data.

3. SecurityContext: Holds the Authentication object for the current request. After successful authentication, Spring stores the authenticated user here.

4. OncePerRequestFilter: Base class for custom filters that should execute once per request. We extend this for our JWT authentication filter.

Configuration patterns used in Spring Boot 4.0.x:
- authorizeHttpRequests() with requestMatchers() for URL-based security rules
- Lambda DSL for fluent, readable configuration
- SecurityFilterChain bean (not the legacy WebSecurityConfigurerAdapter)

Our authentication flow:
1. User sends credentials to /api/auth/login
2. AuthController validates credentials via AuthService
3. On success, JwtService generates a JWT token
4. Client includes token in subsequent requests via Authorization header
5. JwtAuthenticationFilter validates token and sets SecurityContext
6. Controllers access authenticated user via @AuthenticationPrincipal
