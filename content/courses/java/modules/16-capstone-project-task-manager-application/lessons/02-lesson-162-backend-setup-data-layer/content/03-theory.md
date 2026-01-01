---
type: "THEORY"
title: "JPA Entity: User"
---

Now let us create our first entity. The User entity represents registered accounts and is the foundation for authentication and task ownership.

```java
package com.taskmanager.model;

import jakarta.persistence.*;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

@Entity
@Table(name = "users")
public class User {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    
    @Column(unique = true, nullable = false, length = 255)
    private String email;
    
    @Column(nullable = false)
    private String password;
    
    @Column(length = 100)
    private String name;
    
    @Enumerated(EnumType.STRING)
    @Column(nullable = false)
    private Role role = Role.USER;
    
    @Column(name = "created_at", updatable = false)
    private LocalDateTime createdAt;
    
    @OneToMany(mappedBy = "owner", cascade = CascadeType.ALL, orphanRemoval = true)
    private List<Task> tasks = new ArrayList<>();
    
    @OneToMany(mappedBy = "owner", cascade = CascadeType.ALL, orphanRemoval = true)
    private List<Category> categories = new ArrayList<>();
    
    // Default constructor required by JPA
    public User() {}
    
    public User(String email, String password, String name) {
        this.email = email;
        this.password = password;
        this.name = name;
        this.role = Role.USER;
    }
    
    @PrePersist
    protected void onCreate() {
        this.createdAt = LocalDateTime.now();
    }
    
    // Getters and setters
    public Long getId() { return id; }
    public void setId(Long id) { this.id = id; }
    
    public String getEmail() { return email; }
    public void setEmail(String email) { this.email = email; }
    
    public String getPassword() { return password; }
    public void setPassword(String password) { this.password = password; }
    
    public String getName() { return name; }
    public void setName(String name) { this.name = name; }
    
    public Role getRole() { return role; }
    public void setRole(Role role) { this.role = role; }
    
    public LocalDateTime getCreatedAt() { return createdAt; }
    
    public List<Task> getTasks() { return tasks; }
    public void setTasks(List<Task> tasks) { this.tasks = tasks; }
    
    public List<Category> getCategories() { return categories; }
    public void setCategories(List<Category> categories) { this.categories = categories; }
}
```

Annotation Deep Dive:
- @Entity: Marks this class as a JPA entity (maps to database table)
- @Table(name = "users"): Explicit table name (avoids reserved word issues)
- @Id + @GeneratedValue: Auto-generated primary key using database sequence
- @Column: Defines column constraints (unique, nullable, length)
- @Enumerated(EnumType.STRING): Stores enum as string "USER" not ordinal 0
- @OneToMany: Defines the "one" side of one-to-many relationship
- cascade = CascadeType.ALL: All operations cascade to children
- orphanRemoval = true: Delete children when removed from collection
- @PrePersist: Lifecycle hook that runs before INSERT