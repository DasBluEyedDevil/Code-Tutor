---
type: "KEY_POINT"
title: "Roles vs Authorities in Spring Security"
---

Spring Security has two concepts:

ROLES:
- Prefixed with ROLE_ internally
- Represent job functions
- Coarse-grained

.hasRole("ADMIN")  // Checks for ROLE_ADMIN authority
.hasAnyRole("ADMIN", "MANAGER")

AUTHORITIES:
- Fine-grained permissions
- No prefix convention
- Flexible naming

.hasAuthority("user:read")
.hasAuthority("user:write")
.hasAuthority("ROLE_ADMIN")  // Same as hasRole("ADMIN")

Best Practice:

// User entity
public class User {
    private Set<Role> roles;  // ADMIN, MANAGER, USER
}

// Role entity
public class Role {
    private String name;
    private Set<Permission> permissions;  // user:read, user:write
}

// Grant both roles AND permissions as authorities
public Collection<? extends GrantedAuthority> getAuthorities() {
    Set<SimpleGrantedAuthority> authorities = new HashSet<>();
    
    // Add roles
    roles.forEach(role -> 
        authorities.add(new SimpleGrantedAuthority("ROLE_" + role.getName())));
    
    // Add permissions
    roles.forEach(role ->
        role.getPermissions().forEach(permission ->
            authorities.add(new SimpleGrantedAuthority(permission.getName()))));
    
    return authorities;
}