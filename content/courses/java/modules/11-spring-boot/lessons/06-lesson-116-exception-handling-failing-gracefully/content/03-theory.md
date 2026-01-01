---
type: "THEORY"
title: "Custom Exceptions - Creating Meaningful Errors"
---

Create custom exceptions for business logic:

public class ResourceNotFoundException extends RuntimeException {
    public ResourceNotFoundException(String message) {
        super(message);
    }
}

public class InvalidRequestException extends RuntimeException {
    public InvalidRequestException(String message) {
        super(message);
    }
}

Use in controller:

@GetMapping("/users/{id}")
public User getUser(@PathVariable Long id) {
    return userRepository.findById(id)
        .orElseThrow(() -> new ResourceNotFoundException(
            "User not found with id: " + id
        ));
}

Now when user doesn't exist, throw ResourceNotFoundException
instead of returning null!