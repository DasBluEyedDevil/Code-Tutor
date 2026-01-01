---
type: "THEORY"
title: "Step 4: Create the Service"
---

Business logic layer:

@Service
public class TaskService {
    private final TaskRepository taskRepository;
    
    public TaskService(TaskRepository taskRepository) {
        this.taskRepository = taskRepository;
    }
    
    public List<Task> getAllTasksForUser(Long userId) {
        return taskRepository.findByUserId(userId);
    }
    
    public Task createTask(Task task, Long userId) {
        // Validation
        if (task.getTitle() == null || task.getTitle().isBlank()) {
            throw new IllegalArgumentException("Title cannot be empty");
        }
        
        // Set user (security: use authenticated user, not from request)
        User user = new User();
        user.setId(userId);
        task.setUser(user);
        
        return taskRepository.save(task);
    }
    
    public Task updateTask(Long taskId, Task updates, Long userId) {
        Task existing = taskRepository.findById(taskId)
            .orElseThrow(() -> new ResourceNotFoundException(
                "Task", taskId));
        
        // Security check: can only update your own tasks
        if (!existing.getUser().getId().equals(userId)) {
            throw new ForbiddenException(
                "Cannot update another user's task");
        }
        
        // Update fields
        if (updates.getTitle() != null) {
            existing.setTitle(updates.getTitle());
        }
        if (updates.getDescription() != null) {
            existing.setDescription(updates.getDescription());
        }
        if (updates.getCompleted() != null) {
            existing.setCompleted(updates.getCompleted());
        }
        
        return taskRepository.save(existing);
    }
    
    public void deleteTask(Long taskId, Long userId) {
        Task task = taskRepository.findById(taskId)
            .orElseThrow(() -> new ResourceNotFoundException(
                "Task", taskId));
        
        // Security check
        if (!task.getUser().getId().equals(userId)) {
            throw new ForbiddenException(
                "Cannot delete another user's task");
        }
        
        taskRepository.delete(task);
    }
}