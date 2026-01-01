---
type: "THEORY"
title: "Validation with @Valid and @Validated"
---

Validate request bodies automatically:

Entity with validation:

import jakarta.validation.constraints.*;

public class User {
    @NotNull(message = "ID is required")
    private Long id;
    
    @NotBlank(message = "Name cannot be empty")
    @Size(min = 2, max = 100, message = "Name must be 2-100 characters")
    private String name;
    
    @Email(message = "Invalid email format")
    private String email;
    
    @Min(value = 18, message = "Must be at least 18 years old")
    private int age;
    
    // Constructor, getters, setters
}

Controller with validation:

@PostMapping("/users")
public ResponseEntity<User> createUser(@Valid @RequestBody User user) {
    // If validation fails, Spring throws MethodArgumentNotValidException
    User saved = userService.save(user);
    return ResponseEntity.status(HttpStatus.CREATED).body(saved);
}

@Valid triggers validation automatically!