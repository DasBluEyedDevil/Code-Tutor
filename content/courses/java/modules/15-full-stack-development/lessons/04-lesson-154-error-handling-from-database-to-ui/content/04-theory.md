---
type: "THEORY"
title: "Custom Business Exceptions"
---

Create meaningful exception classes:

// Base exception
public class BusinessException extends RuntimeException {
    private final String errorCode;
    
    public BusinessException(String message, String errorCode) {
        super(message);
        this.errorCode = errorCode;
    }
    
    public String getErrorCode() {
        return errorCode;
    }
}

// Specific exceptions
public class ResourceNotFoundException extends BusinessException {
    public ResourceNotFoundException(String resource, Long id) {
        super(resource + " with ID " + id + " not found", "RESOURCE_NOT_FOUND");
    }
}

public class DuplicateResourceException extends BusinessException {
    public DuplicateResourceException(String message) {
        super(message, "DUPLICATE_RESOURCE");
    }
}

public class InsufficientFundsException extends BusinessException {
    public InsufficientFundsException(double available, double required) {
        super("Insufficient funds. Available: " + available + 
              ", Required: " + required, "INSUFFICIENT_FUNDS");
    }
}

Usage in service:

@Service
public class UserService {
    
    public User findById(Long id) {
        return userRepository.findById(id)
            .orElseThrow(() -> new ResourceNotFoundException("User", id));
    }
    
    public User create(User user) {
        if (userRepository.existsByEmail(user.getEmail())) {
            throw new DuplicateResourceException(
                "Email " + user.getEmail() + " is already registered"
            );
        }
        return userRepository.save(user);
    }
}