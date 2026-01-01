---
type: "THEORY"
title: "Implementing RBAC in Database"
---

Store roles and permissions in database:

// User entity
@Entity
public class User {
    @Id @GeneratedValue
    private Long id;
    
    private String username;
    private String password;
    
    @ManyToMany(fetch = FetchType.EAGER)
    @JoinTable(name = "user_roles",
        joinColumns = @JoinColumn(name = "user_id"),
        inverseJoinColumns = @JoinColumn(name = "role_id"))
    private Set<Role> roles = new HashSet<>();
}

// Role entity
@Entity
public class Role {
    @Id @GeneratedValue
    private Long id;
    
    private String name;  // ADMIN, MANAGER, USER
    
    @ManyToMany(fetch = FetchType.EAGER)
    @JoinTable(name = "role_permissions",
        joinColumns = @JoinColumn(name = "role_id"),
        inverseJoinColumns = @JoinColumn(name = "permission_id"))
    private Set<Permission> permissions = new HashSet<>();
}

// Permission entity
@Entity
public class Permission {
    @Id @GeneratedValue
    private Long id;
    
    private String name;  // user:read, user:write, report:view
}

// Database tables:
// users: id, username, password
// roles: id, name
// permissions: id, name
// user_roles: user_id, role_id
// role_permissions: role_id, permission_id

This allows runtime role/permission management without code changes.