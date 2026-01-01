---
type: "THEORY"
title: "Creating an Entity"
---

@Entity tells JPA this maps to a database table:

@Entity
@Table(name = "users")  // Optional: custom table name
public class User {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    
    @Column(name = "full_name", nullable = false, length = 100)
    private String name;
    
    @Column(nullable = false)
    private int age;
    
    @Column(unique = true)
    private String email;
    
    // Getters and setters
}

@Entity - Marks as database entity
@Id - Primary key
@GeneratedValue - Auto-increment
@Column - Column properties