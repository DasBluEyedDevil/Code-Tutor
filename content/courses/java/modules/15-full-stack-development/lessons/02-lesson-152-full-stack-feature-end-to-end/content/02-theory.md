---
type: "THEORY"
title: "Step 1: Backend Entity and Repository"
---

@Entity
public class User {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    
    private String name;
    private String email;
    
    @CreationTimestamp
    private LocalDateTime createdAt;
    
    // Getters and setters
}

public interface UserRepository extends JpaRepository<User, Long> {
    // Spring generates all methods
}