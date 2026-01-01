---
type: "THEORY"
title: "Service Layer Pattern"
---

The service layer contains business logic and coordinates between controllers and repositories. Controllers should be thin - they handle HTTP concerns only.

```java
// com/taskmanager/service/TaskService.java
package com.taskmanager.service;

import com.taskmanager.dto.request.TaskRequest;
import com.taskmanager.dto.response.TaskResponse;
import com.taskmanager.exception.ResourceNotFoundException;
import com.taskmanager.exception.UnauthorizedException;
import com.taskmanager.model.Category;
import com.taskmanager.model.Task;
import com.taskmanager.model.User;
import com.taskmanager.model.enums.Priority;
import com.taskmanager.model.enums.TaskStatus;
import com.taskmanager.repository.CategoryRepository;
import com.taskmanager.repository.TaskRepository;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
@Transactional
public class TaskService {
    
    private final TaskRepository taskRepository;
    private final CategoryRepository categoryRepository;
    
    public TaskService(TaskRepository taskRepository, 
                       CategoryRepository categoryRepository) {
        this.taskRepository = taskRepository;
        this.categoryRepository = categoryRepository;
    }
    
    @Transactional(readOnly = true)
    public Page<TaskResponse> getTasksForUser(User user, Pageable pageable) {
        return taskRepository.findByOwnerId(user.getId(), pageable)
                .map(TaskResponse::fromEntity);
    }
    
    @Transactional(readOnly = true)
    public TaskResponse getTask(Long taskId, User user) {
        Task task = findTaskAndVerifyOwnership(taskId, user);
        return TaskResponse.fromEntity(task);
    }
    
    public TaskResponse createTask(TaskRequest request, User user) {
        Task task = new Task();
        task.setTitle(request.getTitle());
        task.setDescription(request.getDescription());
        task.setStatus(request.getStatus() != null ? request.getStatus() : TaskStatus.PENDING);
        task.setPriority(request.getPriority() != null ? request.getPriority() : Priority.MEDIUM);
        task.setDueDate(request.getDueDate());
        task.setOwner(user);
        
        if (request.getCategoryId() != null) {
            Category category = categoryRepository
                .findByIdAndOwnerId(request.getCategoryId(), user.getId())
                .orElseThrow(() -> new ResourceNotFoundException(
                    "Category not found with id: " + request.getCategoryId()));
            task.setCategory(category);
        }
        
        Task saved = taskRepository.save(task);
        return TaskResponse.fromEntity(saved);
    }
    
    public TaskResponse updateTask(Long taskId, TaskRequest request, User user) {
        Task task = findTaskAndVerifyOwnership(taskId, user);
        
        task.setTitle(request.getTitle());
        task.setDescription(request.getDescription());
        if (request.getStatus() != null) {
            task.setStatus(request.getStatus());
        }
        if (request.getPriority() != null) {
            task.setPriority(request.getPriority());
        }
        task.setDueDate(request.getDueDate());
        
        if (request.getCategoryId() != null) {
            Category category = categoryRepository
                .findByIdAndOwnerId(request.getCategoryId(), user.getId())
                .orElseThrow(() -> new ResourceNotFoundException(
                    "Category not found with id: " + request.getCategoryId()));
            task.setCategory(category);
        } else {
            task.setCategory(null);
        }
        
        Task saved = taskRepository.save(task);
        return TaskResponse.fromEntity(saved);
    }
    
    public void deleteTask(Long taskId, User user) {
        Task task = findTaskAndVerifyOwnership(taskId, user);
        taskRepository.delete(task);
    }
    
    private Task findTaskAndVerifyOwnership(Long taskId, User user) {
        return taskRepository.findByIdAndOwnerId(taskId, user.getId())
                .orElseThrow(() -> new ResourceNotFoundException(
                    "Task not found with id: " + taskId));
    }
}
```

Key Service Layer Principles:
- @Service marks this as a Spring-managed service bean
- @Transactional ensures database operations are atomic
- @Transactional(readOnly = true) optimizes read-only operations
- Constructor injection for dependencies (not @Autowired on fields)
- Always verify ownership before modifying resources
- Convert entities to DTOs before returning