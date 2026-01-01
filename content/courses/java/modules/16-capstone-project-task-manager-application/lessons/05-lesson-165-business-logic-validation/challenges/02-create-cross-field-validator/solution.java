// com/taskmanager/validation/RequireDescriptionForUrgent.java
@Documented
@Constraint(validatedBy = RequireDescriptionForUrgentValidator.class)
@Target({ElementType.TYPE})
@Retention(RetentionPolicy.RUNTIME)
public @interface RequireDescriptionForUrgent {
    String message() default "Description is required for URGENT priority tasks";
    Class<?>[] groups() default {};
    Class<? extends Payload>[] payload() default {};
}

// com/taskmanager/validation/RequireDescriptionForUrgentValidator.java
public class RequireDescriptionForUrgentValidator 
        implements ConstraintValidator<RequireDescriptionForUrgent, TaskRequest> {

    @Override
    public boolean isValid(TaskRequest request, ConstraintValidatorContext context) {
        if (request == null) {
            return true;
        }

        String priority = request.getPriority();
        String description = request.getDescription();

        // Only validate if priority is URGENT
        if ("URGENT".equalsIgnoreCase(priority)) {
            return description != null && !description.trim().isEmpty();
        }

        return true; // Non-URGENT tasks don't need description
    }
}