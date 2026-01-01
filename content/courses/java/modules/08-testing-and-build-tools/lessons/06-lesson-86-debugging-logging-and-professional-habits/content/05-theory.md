---
type: "THEORY"
title: "Writing Good Documentation"
---

JAVADOC = Standard way to document Java code

/**
 * Calculates the total price including tax.
 * 
 * @param subtotal The price before tax
 * @param taxRate The tax rate as decimal (0.08 for 8%)
 * @return The total price including tax
 * @throws IllegalArgumentException if subtotal is negative
 */
public double calculateTotal(double subtotal, double taxRate) {
    if (subtotal < 0) {
        throw new IllegalArgumentException("Subtotal cannot be negative");
    }
    return subtotal * (1 + taxRate);
}

JAVADOC TAGS:
@param - Describes a parameter
@return - Describes return value
@throws - Describes exceptions thrown
@see - Links to related code
@since - When this was added
@deprecated - Mark as obsolete

WHEN TO WRITE DOCS:
✓ Public methods/classes (API)
✓ Complex algorithms
✓ Non-obvious behavior
✗ Self-explanatory code
✗ Private helper methods (usually)