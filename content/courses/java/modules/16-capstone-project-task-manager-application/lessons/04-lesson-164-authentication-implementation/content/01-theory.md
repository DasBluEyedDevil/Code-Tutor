---
type: "THEORY"
title: "Spring Security 6 Configuration Overview"
---

Spring Security 6 brings significant changes from previous versions, adopting a more functional, lambda-based configuration style. Understanding these changes is crucial for building secure applications.

The Security Filter Chain:
Spring Security works by intercepting HTTP requests through a chain of filters. Each filter performs a specific security function: authentication, authorization, CSRF protection, session management, and more. In Spring Security 6, we configure this chain using the SecurityFilterChain bean.

Key Concepts:

1. SecurityFilterChain: The primary configuration point. It defines which requests require authentication, which are public, and how authentication should work.

2. AuthenticationManager: Coordinates authentication by delegating to AuthenticationProvider implementations. For username/password auth, it uses UserDetailsService to load user data.

3. SecurityContext: Holds the Authentication object for the current request. After successful authentication, Spring stores the authenticated user here.

4. OncePerRequestFilter: Base class for custom filters that should execute once per request. We extend this for our JWT authentication filter.

Changes from Spring Security 5:
- authorizeRequests() replaced with authorizeHttpRequests()
- antMatchers() replaced with requestMatchers()
- Lambda DSL is now preferred over chained method calls
- WebSecurityConfigurerAdapter is deprecated; use SecurityFilterChain bean instead

Our authentication flow:
1. User sends credentials to /api/auth/login
2. AuthController validates credentials via AuthService
3. On success, JwtService generates a JWT token
4. Client includes token in subsequent requests via Authorization header
5. JwtAuthenticationFilter validates token and sets SecurityContext
6. Controllers access authenticated user via @AuthenticationPrincipal