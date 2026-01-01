// Method using Spring Data JPA query derivation:
List<Task> findByOwnerIdAndPriorityInAndStatusNot(
    Long ownerId, 
    List<Priority> priorities, 
    TaskStatus status
);

// Called like:
// taskRepository.findByOwnerIdAndPriorityInAndStatusNot(
//     userId, 
//     List.of(Priority.HIGH, Priority.URGENT), 
//     TaskStatus.COMPLETED
// );

// Alternative using @Query:
@Query("SELECT t FROM Task t WHERE t.owner.id = :ownerId " +
       "AND t.priority IN ('HIGH', 'URGENT') " +
       "AND t.status != 'COMPLETED'")
List<Task> findHighPriorityIncompleteTasks(@Param("ownerId") Long ownerId);