---
type: "THEORY"
title: "Custom Validators"
---

For complex validation logic that cannot be expressed with built-in annotations, create custom validators. Let us create a @ValidPriority annotation that ensures the priority value is one of our allowed enum values.

```java
// com/taskmanager/validation/ValidPriority.java
package com.taskmanager.validation;

import jakarta.validation.Constraint;
import jakarta.validation.Payload;
import java.lang.annotation.*;

@Documented
@Constraint(validatedBy = PriorityValidator.class)
@Target({ElementType.FIELD, ElementType.PARAMETER})
@Retention(RetentionPolicy.RUNTIME)
public @interface ValidPriority {
    String message() default "Invalid priority. Must be LOW, MEDIUM, HIGH, or URGENT";
    Class<?>[] groups() default {};
    Class<? extends Payload>[] payload() default {};
}

// com/taskmanager/validation/PriorityValidator.java
package com.taskmanager.validation;

import com.taskmanager.model.enums.Priority;
import jakarta.validation.ConstraintValidator;
import jakarta.validation.ConstraintValidatorContext;

public class PriorityValidator implements ConstraintValidator<ValidPriority, String> {

    @Override
    public void initialize(ValidPriority constraintAnnotation) {
        // No initialization needed
    }

    @Override
    public boolean isValid(String value, ConstraintValidatorContext context) {
        // Null is valid (use @NotNull if required)
        if (value == null) {
            return true;
        }

        try {
            Priority.valueOf(value.toUpperCase());
            return true;
        } catch (IllegalArgumentException e) {
            return false;
        }
    }
}

// com/taskmanager/validation/ValidStatus.java
package com.taskmanager.validation;

import jakarta.validation.Constraint;
import jakarta.validation.Payload;
import java.lang.annotation.*;

@Documented
@Constraint(validatedBy = StatusValidator.class)
@Target({ElementType.FIELD, ElementType.PARAMETER})
@Retention(RetentionPolicy.RUNTIME)
public @interface ValidStatus {
    String message() default "Invalid status. Must be PENDING, IN_PROGRESS, COMPLETED, or CANCELLED";
    Class<?>[] groups() default {};
    Class<? extends Payload>[] payload() default {};
}

// com/taskmanager/validation/StatusValidator.java
package com.taskmanager.validation;

import com.taskmanager.model.enums.TaskStatus;
import jakarta.validation.ConstraintValidator;
import jakarta.validation.ConstraintValidatorContext;

public class StatusValidator implements ConstraintValidator<ValidStatus, String> {

    @Override
    public boolean isValid(String value, ConstraintValidatorContext context) {
        if (value == null) {
            return true;
        }

        try {
            TaskStatus.valueOf(value.toUpperCase());
            return true;
        } catch (IllegalArgumentException e) {
            return false;
        }
    }
}
```

Using Custom Validators:
```java
public class TaskRequest {
    
    @NotBlank
    private String title;
    
    @ValidStatus
    private String status;
    
    @ValidPriority
    private String priority;
}
```

This approach provides user-friendly error messages when clients send invalid enum values like "SUPER_HIGH" instead of generic JSON parsing errors.