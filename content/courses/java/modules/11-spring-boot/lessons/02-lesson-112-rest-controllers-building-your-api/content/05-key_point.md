---
type: "KEY_POINT"
title: "Validation with Spring Boot"
---

Use Bean Validation annotations:

public class User {
    @NotNull(message = "ID is required")
    private Long id;
    
    @NotBlank(message = "Name cannot be empty")
    @Size(min = 2, max = 100, message = "Name must be 2-100 characters")
    private String name;
    
    @Min(value = 18, message = "Must be at least 18")
    private int age;
    
    @Email(message = "Invalid email format")
    private String email;
}

In controller:
@PostMapping
public ResponseEntity<User> createUser(@Valid @RequestBody User user) {
    // If validation fails, Spring returns 400 automatically
    return ResponseEntity.ok(userService.save(user));
}

@Valid triggers validation!
Invalid request → 400 Bad Request with error details