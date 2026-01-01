---
type: "KEY_POINT"
title: "Validation Best Practices"
---

Follow these practices for robust validation:

1. Validate at Multiple Layers:
   - DTO level: Input format, required fields, size limits
   - Service level: Business rules, cross-field validation, entity state
   - Database level: Constraints as last line of defense

2. Provide Clear Error Messages:
   - Bad: "Invalid value"
   - Good: "Due date must be today or in the future"
   - Include the field name and expected format

3. Validate Early, Fail Fast:
   - Check inputs before expensive operations
   - Do not let invalid data reach the database

4. Use Groups for Conditional Validation:
```java
public interface OnCreate {}
public interface OnUpdate {}

public class TaskRequest {
    @Null(groups = OnCreate.class, message = "ID must be null for create")
    @NotNull(groups = OnUpdate.class, message = "ID is required for update")
    private Long id;
}

// In controller:
@PostMapping
public ResponseEntity<?> create(
    @Validated(OnCreate.class) @RequestBody TaskRequest request) {...}

@PutMapping("/{id}")
public ResponseEntity<?> update(
    @Validated(OnUpdate.class) @RequestBody TaskRequest request) {...}
```

5. Sanitize Input:
```java
task.setTitle(request.getTitle().trim());  // Remove leading/trailing whitespace
user.setEmail(request.getEmail().toLowerCase().trim());  // Normalize email
```

6. Do Not Trust Client Data:
   - Always validate on server side
   - Client-side validation is for UX only
   - Assume all input is potentially malicious