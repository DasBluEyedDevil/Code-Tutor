---
type: "KEY_POINT"
title: "The Security Filter Chain"
---

Spring Security works through a chain of filters:

HTTP Request
    |
    v
[SecurityFilterChain]
    |
    +-> SecurityContextPersistenceFilter (load security context)
    +-> LogoutFilter (handle /logout)
    +-> UsernamePasswordAuthenticationFilter (form login)
    +-> BasicAuthenticationFilter (HTTP Basic)
    +-> RequestCacheAwareFilter (saved request)
    +-> SecurityContextHolderFilter
    +-> SessionManagementFilter
    +-> ExceptionTranslationFilter (handle auth exceptions)
    +-> FilterSecurityInterceptor (authorization check)
    |
    v
Your Controller

Each filter has a specific responsibility:
- Early filters: Extract credentials
- Middle filters: Authenticate user
- Late filters: Authorize request

Filters can:
- Pass request to next filter
- Short-circuit (deny request)
- Modify request/response

You configure WHAT to protect and HOW. Spring Security handles the rest.