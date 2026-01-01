---
type: "THEORY"
title: "Tips for Success"
---


### Design Principles Applied

**1. Single Responsibility Principle**
- Each class has one clear purpose
- `Book` manages book data, `Library` manages operations

**2. Open/Closed Principle**
- `Book` is open for extension (PhysicalBook, DigitalBook)
- Closed for modification (base behavior is stable)

**3. Liskov Substitution Principle**
- `PhysicalBook` and `DigitalBook` can be used anywhere `Book` is expected

**4. Interface Segregation**
- Small, focused interfaces (Borrowable, Reservable)

**5. Dependency Inversion**
- Code depends on abstractions (Book, not specific types)

---

