---
type: "THEORY"
title: "TaskController: Full CRUD Implementation"
---

The controller handles HTTP requests, delegates to services, and returns proper responses.

```java
// com/taskmanager/controller/TaskController.java
package com.taskmanager.controller;

import com.taskmanager.dto.request.TaskRequest;
import com.taskmanager.dto.response.TaskResponse;
import com.taskmanager.model.User;
import com.taskmanager.service.TaskService;
import jakarta.validation.Valid;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import org.springframework.data.web.PageableDefault;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.annotation.AuthenticationPrincipal;
import org.springframework.web.bind.annotation.*;

import java.net.URI;

@RestController
@RequestMapping("/api/tasks")
public class TaskController {
    
    private final TaskService taskService;
    
    public TaskController(TaskService taskService) {
        this.taskService = taskService;
    }
    
    @GetMapping
    public ResponseEntity<Page<TaskResponse>> getAllTasks(
            @AuthenticationPrincipal User user,
            @PageableDefault(size = 20, sort = "createdAt", 
                           direction = Sort.Direction.DESC) Pageable pageable) {
        Page<TaskResponse> tasks = taskService.getTasksForUser(user, pageable);
        return ResponseEntity.ok(tasks);
    }
    
    @GetMapping("/{id}")
    public ResponseEntity<TaskResponse> getTask(
            @PathVariable Long id,
            @AuthenticationPrincipal User user) {
        TaskResponse task = taskService.getTask(id, user);
        return ResponseEntity.ok(task);
    }
    
    @PostMapping
    public ResponseEntity<TaskResponse> createTask(
            @Valid @RequestBody TaskRequest request,
            @AuthenticationPrincipal User user) {
        TaskResponse created = taskService.createTask(request, user);
        URI location = URI.create("/api/tasks/" + created.getId());
        return ResponseEntity.created(location).body(created);
    }
    
    @PutMapping("/{id}")
    public ResponseEntity<TaskResponse> updateTask(
            @PathVariable Long id,
            @Valid @RequestBody TaskRequest request,
            @AuthenticationPrincipal User user) {
        TaskResponse updated = taskService.updateTask(id, request, user);
        return ResponseEntity.ok(updated);
    }
    
    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteTask(
            @PathVariable Long id,
            @AuthenticationPrincipal User user) {
        taskService.deleteTask(id, user);
        return ResponseEntity.noContent().build();
    }
}
```

Controller Annotations Explained:
- @RestController: Combines @Controller and @ResponseBody (returns JSON)
- @RequestMapping("/api/tasks"): Base URL for all endpoints
- @GetMapping, @PostMapping, etc.: Map HTTP methods to handlers
- @PathVariable: Extract values from URL path
- @RequestBody: Deserialize JSON body to object
- @Valid: Trigger validation annotations on the request object
- @AuthenticationPrincipal: Inject the currently authenticated user
- @PageableDefault: Set default pagination (size, sort field, direction)