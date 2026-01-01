---
type: "THEORY"
title: "Entity Classes - Mapping Objects to Tables"
---

An ENTITY is a Java class that maps to a database table:

import jakarta.persistence.*;

@Entity
@Table(name = "students")
public class Student {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    
    @Column(name = "full_name", nullable = false)
    private String name;
    
    private int age;  // Maps to 'age' column automatically
    
    @Column(unique = true)
    private String email;
    
    // Constructors, getters, setters...
}

ANNOTATIONS EXPLAINED:
@Entity - This class maps to a table
@Table - Specify table name (optional if same as class)
@Id - Primary key
@GeneratedValue - Auto-increment
@Column - Column customization (name, constraints)