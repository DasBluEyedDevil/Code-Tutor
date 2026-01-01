---
type: "THEORY"
title: "Step 3: Create the Repository"
---

Interface for database access:

@Repository
public interface TaskRepository extends JpaRepository<Task, Long> {
    
    // Find all tasks for a specific user
    List<Task> findByUserId(Long userId);
    
    // Find only completed tasks
    List<Task> findByUserIdAndCompleted(Long userId, Boolean completed);
    
    // Find tasks with title containing text (case-insensitive)
    List<Task> findByUserIdAndTitleContainingIgnoreCase(
        Long userId, String searchTerm);
    
    // Count incomplete tasks
    Long countByUserIdAndCompletedFalse(Long userId);
    
    // Check if task belongs to user (for authorization)
    boolean existsByIdAndUserId(Long taskId, Long userId);
}

Spring Data JPA generates SQL automatically!

findByUserId(1L) becomes:
SELECT * FROM tasks WHERE user_id = 1;

findByUserIdAndCompleted(1L, true) becomes:
SELECT * FROM tasks WHERE user_id = 1 AND completed = true;