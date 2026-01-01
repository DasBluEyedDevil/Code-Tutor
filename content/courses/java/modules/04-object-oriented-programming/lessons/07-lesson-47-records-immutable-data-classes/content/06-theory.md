---
type: "THEORY"
title: "When to Use Records"
---

USE RECORDS FOR:
- Data Transfer Objects (DTOs)
- API response/request objects
- Value objects (Money, Coordinate, Range)
- Configuration objects
- Tuple-like groupings of data
- Immutable data holders

RECORD EXAMPLES:

// API Response
record ApiResponse<T>(T data, String message, int status) {}

// Geographic coordinates
record Coordinate(double latitude, double longitude) {
    public double distanceTo(Coordinate other) {
        // Calculate distance...
    }
}

// Money with currency
record Money(BigDecimal amount, String currency) {
    public Money add(Money other) {
        if (!currency.equals(other.currency)) {
            throw new IllegalArgumentException("Currency mismatch");
        }
        return new Money(amount.add(other.amount), currency);
    }
}

DON'T USE RECORDS FOR:
- Entities that need to change (use regular classes)
- Classes that need inheritance hierarchies
- Classes with complex initialization logic
- JPA/Hibernate entities (they need no-arg constructors)