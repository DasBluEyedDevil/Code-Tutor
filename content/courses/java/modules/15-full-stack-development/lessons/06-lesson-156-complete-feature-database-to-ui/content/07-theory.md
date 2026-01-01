---
type: "THEORY"
title: "Step 5: Create the Controller"
---

REST API endpoints:

@RestController
@RequestMapping("/api/tasks")
@CrossOrigin(origins = "http://localhost:3000")
public class TaskController {
    private final TaskService taskService;
    
    public TaskController(TaskService taskService) {
        this.taskService = taskService;
    }
    
    // GET /api/tasks - Get all tasks for current user
    @GetMapping
    public ResponseEntity<List<Task>> getAllTasks(
            @AuthenticationPrincipal UserDetails userDetails) {
        Long userId = getCurrentUserId(userDetails);
        List<Task> tasks = taskService.getAllTasksForUser(userId);
        return ResponseEntity.ok(tasks);
    }
    
    // POST /api/tasks - Create new task
    @PostMapping
    public ResponseEntity<Task> createTask(
            @Valid @RequestBody TaskRequest request,
            @AuthenticationPrincipal UserDetails userDetails) {
        Long userId = getCurrentUserId(userDetails);
        Task task = new Task(request.getTitle(), 
                            request.getDescription(), null);
        Task created = taskService.createTask(task, userId);
        return ResponseEntity
            .status(HttpStatus.CREATED)
            .body(created);
    }
    
    // PUT /api/tasks/{id} - Update task
    @PutMapping("/{id}")
    public ResponseEntity<Task> updateTask(
            @PathVariable Long id,
            @RequestBody TaskRequest request,
            @AuthenticationPrincipal UserDetails userDetails) {
        Long userId = getCurrentUserId(userDetails);
        Task updates = new Task();
        updates.setTitle(request.getTitle());
        updates.setDescription(request.getDescription());
        updates.setCompleted(request.getCompleted());
        Task updated = taskService.updateTask(id, updates, userId);
        return ResponseEntity.ok(updated);
    }
    
    // DELETE /api/tasks/{id} - Delete task
    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteTask(
            @PathVariable Long id,
            @AuthenticationPrincipal UserDetails userDetails) {
        Long userId = getCurrentUserId(userDetails);
        taskService.deleteTask(id, userId);
        return ResponseEntity.noContent().build();
    }
    
    private Long getCurrentUserId(UserDetails userDetails) {
        // Extract user ID from authenticated user
        // Implementation depends on your security setup
        return 1L; // Placeholder
    }
}