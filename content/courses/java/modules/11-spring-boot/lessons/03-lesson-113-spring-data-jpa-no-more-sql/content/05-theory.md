---
type: "THEORY"
title: "Custom JPQL Queries"
---

For complex queries, write JPQL (like SQL but for Java objects):

public interface UserRepository extends JpaRepository<User, Long> {
    
    @Query("SELECT u FROM User u WHERE u.age > :minAge AND u.age < :maxAge")
    List<User> findUsersByAgeRange(@Param("minAge") int min, @Param("maxAge") int max);
    
    @Query("SELECT u FROM User u WHERE u.name LIKE %:keyword%")
    List<User> searchByName(@Param("keyword") String keyword);
    
    @Query(value = "SELECT * FROM users WHERE age > ?1", nativeQuery = true)
    List<User> findUsingNativeSQL(int age);
}

@Query - Custom JPQL query
@Param - Named parameter
nativeQuery = true - Use actual SQL instead of JPQL