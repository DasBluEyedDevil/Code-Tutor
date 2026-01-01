---
type: "THEORY"
title: "Checkpoint Quiz"
---


### Question 1: Class Delegation

What does the `by` keyword do in class delegation?

**A)** Creates a subclass
**B)** Forwards interface implementation to another object
**C)** Copies all methods from another class
**D)** Creates a singleton

**Answer**: **B** - The `by` keyword automatically forwards interface implementation to the specified delegate object.

---

### Question 2: Lazy Initialization

When is a lazy property initialized?

**A)** When the class is created
**B)** At compile time
**C)** On first access
**D)** Never

**Answer**: **C** - Lazy properties are initialized on first access, not when the class is created.

---

### Question 3: Observable

What does `Delegates.observable` do?

**A)** Validates property values
**B)** Notifies when property changes
**C)** Makes property thread-safe
**D)** Caches property values

**Answer**: **B** - `Delegates.observable` calls a lambda whenever the property value changes, allowing you to observe changes.

---

### Question 4: Vetoable

How does `Delegates.vetoable` work?

**A)** It logs all changes
**B)** It returns true/false to accept/reject changes
**C)** It automatically validates types
**D)** It prevents all changes

**Answer**: **B** - `Delegates.vetoable` calls a lambda that returns true to accept or false to reject the property change.

---

### Question 5: Custom Delegates

What must a custom property delegate implement?

**A)** `get()` and `set()`
**B)** `getValue()` and `setValue()` operators
**C)** `read()` and `write()`
**D)** `load()` and `store()`

**Answer**: **B** - Custom delegates must implement `getValue()` operator (and `setValue()` for mutable properties).

---

