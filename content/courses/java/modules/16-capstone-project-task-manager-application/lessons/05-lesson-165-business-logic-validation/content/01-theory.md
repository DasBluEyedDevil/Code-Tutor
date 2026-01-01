---
type: "THEORY"
title: "Bean Validation Annotations"
---

Jakarta Bean Validation (formerly javax.validation) provides declarative validation through annotations. These annotations enforce data constraints before your business logic processes the data, catching invalid input early.

Core Validation Annotations:

@NotNull - Value cannot be null
@NotBlank - String cannot be null, empty, or whitespace only (most common for strings)
@NotEmpty - Collection/String cannot be null or empty (but allows whitespace strings)
@Size(min, max) - String/Collection length must be within bounds
@Min(value) / @Max(value) - Numeric value bounds
@Email - Must be valid email format
@Pattern(regexp) - Must match regular expression
@Past / @Future - Date must be in past or future
@PastOrPresent / @FutureOrPresent - Include today
@Positive / @PositiveOrZero - Number must be > 0 or >= 0
@Negative / @NegativeOrZero - Number must be < 0 or <= 0

Example Request DTO:
```java
package com.taskmanager.dto.request;

import jakarta.validation.constraints.*;
import java.time.LocalDate;

public class TaskRequest {

    @NotBlank(message = "Title is required")
    @Size(min = 1, max = 255, message = "Title must be 1-255 characters")
    private String title;

    @Size(max = 5000, message = "Description cannot exceed 5000 characters")
    private String description;

    @FutureOrPresent(message = "Due date cannot be in the past")
    private LocalDate dueDate;

    private String status;   // Validated by custom validator
    private String priority; // Validated by custom validator
    
    private Long categoryId;

    // Getters and setters...
}
```

Activating Validation:
Add @Valid before @RequestBody in controller methods:
```java
@PostMapping
public ResponseEntity<TaskResponse> createTask(
        @Valid @RequestBody TaskRequest request,  // @Valid triggers validation
        @AuthenticationPrincipal User user) {
    // This code only runs if validation passes
}
```

When validation fails, Spring throws MethodArgumentNotValidException, which we handle in our GlobalExceptionHandler.