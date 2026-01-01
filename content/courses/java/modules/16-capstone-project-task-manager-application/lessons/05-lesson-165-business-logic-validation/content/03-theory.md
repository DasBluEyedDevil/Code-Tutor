---
type: "THEORY"
title: "Task Due Date and Priority Business Rules"
---

Business logic goes beyond simple field validation. Our TaskService must enforce rules about how tasks behave based on their state and relationships.

```java
// com/taskmanager/service/TaskService.java - Business Logic Methods
package com.taskmanager.service;

import com.taskmanager.dto.request.TaskRequest;
import com.taskmanager.dto.response.TaskResponse;
import com.taskmanager.exception.BusinessRuleException;
import com.taskmanager.exception.ResourceNotFoundException;
import com.taskmanager.model.Category;
import com.taskmanager.model.Task;
import com.taskmanager.model.User;
import com.taskmanager.model.enums.Priority;
import com.taskmanager.model.enums.TaskStatus;
import com.taskmanager.repository.CategoryRepository;
import com.taskmanager.repository.TaskRepository;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.List;

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

    /**
     * Business Rule: Cannot set due date in the past for new tasks
     */
    public TaskResponse createTask(TaskRequest request, User user) {
        if (request.getDueDate() != null && request.getDueDate().isBefore(LocalDate.now())) {
            throw new BusinessRuleException("Due date cannot be in the past");
        }

        Task task = new Task();
        task.setTitle(request.getTitle().trim());
        task.setDescription(request.getDescription());
        task.setStatus(parseStatus(request.getStatus(), TaskStatus.PENDING));
        task.setPriority(parsePriority(request.getPriority(), Priority.MEDIUM));
        task.setDueDate(request.getDueDate());
        task.setOwner(user);

        if (request.getCategoryId() != null) {
            assignCategory(task, request.getCategoryId(), user);
        }

        return TaskResponse.fromEntity(taskRepository.save(task));
    }

    /**
     * Business Rule: Completed tasks cannot be modified
     */
    public TaskResponse updateTask(Long taskId, TaskRequest request, User user) {
        Task task = findTaskAndVerifyOwnership(taskId, user);

        if (task.getStatus() == TaskStatus.COMPLETED) {
            throw new BusinessRuleException(
                "Cannot modify a completed task. Reopen it first.");
        }

        task.setTitle(request.getTitle().trim());
        task.setDescription(request.getDescription());
        
        if (request.getStatus() != null) {
            task.setStatus(parseStatus(request.getStatus(), task.getStatus()));
        }
        if (request.getPriority() != null) {
            task.setPriority(parsePriority(request.getPriority(), task.getPriority()));
        }
        
        task.setDueDate(request.getDueDate());

        if (request.getCategoryId() != null) {
            assignCategory(task, request.getCategoryId(), user);
        } else {
            task.setCategory(null);
        }

        return TaskResponse.fromEntity(taskRepository.save(task));
    }

    /**
     * Business Rule: Mark task complete with completion timestamp
     */
    public TaskResponse markComplete(Long taskId, User user) {
        Task task = findTaskAndVerifyOwnership(taskId, user);

        if (task.getStatus() == TaskStatus.COMPLETED) {
            throw new BusinessRuleException("Task is already completed");
        }

        task.setStatus(TaskStatus.COMPLETED);
        // Note: Add completedAt field to Task entity if needed
        
        return TaskResponse.fromEntity(taskRepository.save(task));
    }

    /**
     * Business Rule: Reopen a completed task (sets to IN_PROGRESS)
     */
    public TaskResponse reopenTask(Long taskId, User user) {
        Task task = findTaskAndVerifyOwnership(taskId, user);

        if (task.getStatus() != TaskStatus.COMPLETED) {
            throw new BusinessRuleException("Only completed tasks can be reopened");
        }

        task.setStatus(TaskStatus.IN_PROGRESS);
        return TaskResponse.fromEntity(taskRepository.save(task));
    }

    /**
     * Auto-escalate overdue tasks to URGENT priority
     */
    public List<TaskResponse> escalateOverdueTasks(User user) {
        List<Task> overdueTasks = taskRepository.findOverdueTasks(
            user.getId(), LocalDate.now());

        overdueTasks.stream()
            .filter(t -> t.getPriority() != Priority.URGENT)
            .forEach(t -> t.setPriority(Priority.URGENT));

        return taskRepository.saveAll(overdueTasks).stream()
            .map(TaskResponse::fromEntity)
            .toList();
    }

    // Helper methods
    private void assignCategory(Task task, Long categoryId, User user) {
        Category category = categoryRepository
            .findByIdAndOwnerId(categoryId, user.getId())
            .orElseThrow(() -> new ResourceNotFoundException(
                "Category not found: " + categoryId));
        task.setCategory(category);
    }

    private TaskStatus parseStatus(String value, TaskStatus defaultValue) {
        if (value == null) return defaultValue;
        try {
            return TaskStatus.valueOf(value.toUpperCase());
        } catch (IllegalArgumentException e) {
            return defaultValue;
        }
    }

    private Priority parsePriority(String value, Priority defaultValue) {
        if (value == null) return defaultValue;
        try {
            return Priority.valueOf(value.toUpperCase());
        } catch (IllegalArgumentException e) {
            return defaultValue;
        }
    }

    private Task findTaskAndVerifyOwnership(Long taskId, User user) {
        return taskRepository.findByIdAndOwnerId(taskId, user.getId())
            .orElseThrow(() -> new ResourceNotFoundException(
                "Task not found: " + taskId));
    }
}
```

Business rules implemented:
1. New tasks cannot have past due dates
2. Completed tasks are immutable until reopened
3. Marking complete is a distinct action from updating
4. Overdue tasks can be auto-escalated to URGENT