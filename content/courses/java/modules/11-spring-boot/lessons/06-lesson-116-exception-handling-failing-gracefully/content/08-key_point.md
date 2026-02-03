---
type: "KEY_POINT"
title: "Common Validation Annotations"
---

Jakarta Validation (Spring Boot 4.0 uses jakarta.validation exclusively):

NULL/NOT NULL:
@NotNull - Value cannot be null
@Null - Value must be null

STRINGS:
@NotBlank - Not null, not empty, not whitespace
@NotEmpty - Not null and not empty (but can be whitespace)
@Size(min, max) - Length constraints
@Pattern(regexp) - Must match regex

NUMBERS:
@Min(value) - Minimum value
@Max(value) - Maximum value
@Positive - Must be > 0
@PositiveOrZero - Must be >= 0
@Negative - Must be < 0
@NegativeOrZero - Must be <= 0

FORMAT:
@Email - Valid email format
@Past - Date in the past
@Future - Date in the future

Example:

public class Product {
    @NotBlank
    @Size(min = 3, max = 100)
    private String name;
    
    @Positive
    private double price;
    
    @Min(0)
    @Max(10000)
    private int stockQuantity;
}