---
type: "ANALOGY"
title: "The Concept: Shared vs. Personal"
---

**Think of a car dealership:**

**Instance Attributes** (Personal)
- Each car has its own: color, mileage, VIN number
- Different for every car
- Defined in `__init__` with `self.attribute`

**Class Attributes** (Shared)
- All cars share: manufacturer name, warranty period
- Same for every car
- Defined directly in class, outside methods

**Example:**
```
Instance (Personal):           Class (Shared):
- My car is red               - All are Toyota
- My car has 15k miles        - All have 3-year warranty
- My VIN: ABC123              - All made in 2024
```

**Types of methods:**

1. **Instance methods** (most common)
   - Work with specific object (self)
   - Can access instance AND class attributes

2. **Class methods** (@classmethod)
   - Work with the class itself (cls)
   - Often used for alternative constructors

3. **Static methods** (@staticmethod)
   - Don't access instance or class
   - Utility functions related to the class