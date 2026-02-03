---
type: "KEY_POINT"
title: "Authorization Matchers"
---

Spring Security 7 uses requestMatchers() with various matching strategies:

BY PATH:
.requestMatchers("/admin/**")           // /admin/anything
.requestMatchers("/api/users/{id}")     // Path variable
.requestMatchers("/public", "/home")   // Multiple paths

BY HTTP METHOD:
.requestMatchers(HttpMethod.GET, "/api/**")    // GET only
.requestMatchers(HttpMethod.POST, "/api/**")   // POST only
.requestMatchers(HttpMethod.DELETE, "/admin/**") // DELETE only

AUTHORIZATION RULES:
.permitAll()              // No authentication required
.authenticated()          // Must be logged in
.hasRole("ADMIN")         // Must have ADMIN role
.hasAnyRole("ADMIN", "MANAGER")  // Any of these roles
.hasAuthority("WRITE_PERMISSION") // Specific authority
.denyAll()                // Never allow

ORDER MATTERS!
Rules are evaluated top to bottom. First match wins.

// WRONG - /admin/public is protected!
.requestMatchers("/admin/**").hasRole("ADMIN")
.requestMatchers("/admin/public").permitAll()

// RIGHT - specific rule first
.requestMatchers("/admin/public").permitAll()
.requestMatchers("/admin/**").hasRole("ADMIN")