---
type: "THEORY"
title: "JPA Entity: Category"
---

The Category entity allows users to organize their tasks into logical groups.

```java
package com.taskmanager.model;

import jakarta.persistence.*;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

@Entity
@Table(name = "categories", 
       uniqueConstraints = @UniqueConstraint(columnNames = {"name", "owner_id"}))
public class Category {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    
    @Column(nullable = false, length = 50)
    private String name;
    
    @Column(length = 255)
    private String description;
    
    @Column(length = 7)
    private String color = "#6B7280"; // Default gray
    
    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "owner_id", nullable = false)
    private User owner;
    
    @OneToMany(mappedBy = "category")
    private List<Task> tasks = new ArrayList<>();
    
    @Column(name = "created_at", updatable = false)
    private LocalDateTime createdAt;
    
    public Category() {}
    
    public Category(String name, String description, String color, User owner) {
        this.name = name;
        this.description = description;
        this.color = color;
        this.owner = owner;
    }
    
    @PrePersist
    protected void onCreate() {
        this.createdAt = LocalDateTime.now();
    }
    
    // Getters and setters
    public Long getId() { return id; }
    public void setId(Long id) { this.id = id; }
    
    public String getName() { return name; }
    public void setName(String name) { this.name = name; }
    
    public String getDescription() { return description; }
    public void setDescription(String description) { this.description = description; }
    
    public String getColor() { return color; }
    public void setColor(String color) { this.color = color; }
    
    public User getOwner() { return owner; }
    public void setOwner(User owner) { this.owner = owner; }
    
    public List<Task> getTasks() { return tasks; }
    public void setTasks(List<Task> tasks) { this.tasks = tasks; }
    
    public LocalDateTime getCreatedAt() { return createdAt; }
}
```

Key Design Decisions:
- Composite unique constraint: Each user can only have one category with a given name
- FetchType.LAZY: Do not load owner automatically (prevents N+1 queries)
- No cascade on tasks: Deleting category does not delete tasks, just sets category to null
- Color field: Hex color for UI display, makes the app more visually appealing