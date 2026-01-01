---
type: "THEORY"
title: "Repository Interfaces"
---

Spring Data JPA generates repository implementations automatically. We only need to define interfaces.

```java
// com/taskmanager/repository/UserRepository.java
package com.taskmanager.repository;

import com.taskmanager.model.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import java.util.Optional;

@Repository
public interface UserRepository extends JpaRepository<User, Long> {
    Optional<User> findByEmail(String email);
    boolean existsByEmail(String email);
}

// com/taskmanager/repository/CategoryRepository.java
package com.taskmanager.repository;

import com.taskmanager.model.Category;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import java.util.List;
import java.util.Optional;

@Repository
public interface CategoryRepository extends JpaRepository<Category, Long> {
    List<Category> findByOwnerId(Long ownerId);
    Optional<Category> findByIdAndOwnerId(Long id, Long ownerId);
    boolean existsByNameAndOwnerId(String name, Long ownerId);
}

// com/taskmanager/repository/TaskRepository.java
package com.taskmanager.repository;

import com.taskmanager.model.Task;
import com.taskmanager.model.enums.TaskStatus;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;
import java.time.LocalDate;
import java.util.List;
import java.util.Optional;

@Repository
public interface TaskRepository extends JpaRepository<Task, Long> {
    
    Page<Task> findByOwnerId(Long ownerId, Pageable pageable);
    
    Optional<Task> findByIdAndOwnerId(Long id, Long ownerId);
    
    List<Task> findByOwnerIdAndStatus(Long ownerId, TaskStatus status);
    
    List<Task> findByOwnerIdAndCategoryId(Long ownerId, Long categoryId);
    
    @Query("SELECT t FROM Task t WHERE t.owner.id = :ownerId " +
           "AND t.dueDate <= :date AND t.status != 'COMPLETED'")
    List<Task> findOverdueTasks(@Param("ownerId") Long ownerId, 
                                 @Param("date") LocalDate date);
    
    @Query("SELECT COUNT(t) FROM Task t WHERE t.owner.id = :ownerId " +
           "AND t.status = :status")
    long countByOwnerIdAndStatus(@Param("ownerId") Long ownerId, 
                                  @Param("status") TaskStatus status);
}
```

Spring Data JPA Magic:
- findByEmail: Generates SELECT * FROM users WHERE email = ?
- existsByEmail: Generates SELECT COUNT(*) > 0 FROM users WHERE email = ?
- findByOwnerId with Pageable: Adds LIMIT, OFFSET, ORDER BY automatically
- @Query: For complex queries that cannot be derived from method names
- @Param: Named parameters in JPQL queries

Return Types:
- Optional<T>: For single results that might not exist
- List<T>: For multiple results (empty list if none)
- Page<T>: For paginated results with total count