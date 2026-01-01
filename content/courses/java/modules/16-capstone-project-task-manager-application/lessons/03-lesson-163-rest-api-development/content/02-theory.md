---
type: "THEORY"
title: "Data Transfer Objects (DTOs)"
---

DTOs separate our API contract from our internal entities. This provides several benefits: security (we control what is exposed), flexibility (API can differ from database), and stability (internal changes do not break API).

```java
// com/taskmanager/dto/request/TaskRequest.java
package com.taskmanager.dto.request;

import com.taskmanager.model.enums.Priority;
import com.taskmanager.model.enums.TaskStatus;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Size;
import java.time.LocalDate;

public class TaskRequest {
    
    @NotBlank(message = "Title is required")
    @Size(max = 255, message = "Title must be less than 255 characters")
    private String title;
    
    @Size(max = 5000, message = "Description must be less than 5000 characters")
    private String description;
    
    private TaskStatus status;
    
    private Priority priority;
    
    private LocalDate dueDate;
    
    private Long categoryId;
    
    // Getters and setters
    public String getTitle() { return title; }
    public void setTitle(String title) { this.title = title; }
    
    public String getDescription() { return description; }
    public void setDescription(String description) { this.description = description; }
    
    public TaskStatus getStatus() { return status; }
    public void setStatus(TaskStatus status) { this.status = status; }
    
    public Priority getPriority() { return priority; }
    public void setPriority(Priority priority) { this.priority = priority; }
    
    public LocalDate getDueDate() { return dueDate; }
    public void setDueDate(LocalDate dueDate) { this.dueDate = dueDate; }
    
    public Long getCategoryId() { return categoryId; }
    public void setCategoryId(Long categoryId) { this.categoryId = categoryId; }
}
```

Validation annotations ensure data integrity before it reaches the service layer. @NotBlank checks for null and empty strings, @Size limits string length.