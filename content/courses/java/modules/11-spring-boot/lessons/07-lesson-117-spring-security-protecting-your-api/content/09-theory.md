---
type: "THEORY"
title: "Getting the Current User"
---

Access authenticated user in controllers:

METHOD 1: Principal parameter

@GetMapping("/profile")
public String getProfile(Principal principal) {
    String username = principal.getName();
    return "Logged in as: " + username;
}

METHOD 2: Authentication parameter

@GetMapping("/profile")
public String getProfile(Authentication authentication) {
    String username = authentication.getName();
    Collection<? extends GrantedAuthority> authorities = 
        authentication.getAuthorities();
    return "User: " + username + ", Roles: " + authorities;
}

METHOD 3: SecurityContextHolder (anywhere in code)

Authentication auth = SecurityContextHolder.getContext().getAuthentication();
String username = auth.getName();
boolean isAdmin = auth.getAuthorities().stream()
    .anyMatch(a -> a.getAuthority().equals("ROLE_ADMIN"));

Spring injects the current authenticated user automatically!