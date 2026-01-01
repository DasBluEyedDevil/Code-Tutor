---
type: "THEORY"
title: "Custom Query Methods"
---

Spring Data JPA generates queries from method names!

public interface UserRepository extends JpaRepository<User, Long> {
    
    // Find by name
    List<User> findByName(String name);
    // SELECT * FROM users WHERE name = ?
    
    // Find by age greater than
    List<User> findByAgeGreaterThan(int age);
    // SELECT * FROM users WHERE age > ?
    
    // Find by email containing
    List<User> findByEmailContaining(String keyword);
    // SELECT * FROM users WHERE email LIKE %?%
    
    // Find by name and age
    List<User> findByNameAndAge(String name, int age);
    // SELECT * FROM users WHERE name = ? AND age = ?
    
    // Check if exists
    boolean existsByEmail(String email);
    
    // Count by age
    long countByAge(int age);
}

METHOD NAME KEYWORDS:
- findBy, getBy, queryBy
- And, Or
- GreaterThan, LessThan, Between
- Like, Containing, StartingWith, EndingWith
- OrderBy...Asc, OrderBy...Desc