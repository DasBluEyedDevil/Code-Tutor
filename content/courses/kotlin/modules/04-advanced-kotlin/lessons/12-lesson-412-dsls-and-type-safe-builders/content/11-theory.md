---
type: "THEORY"
title: "Checkpoint Quiz"
---


### Question 1: Lambda with Receiver

What's the difference between `(T) -> Unit` and `T.() -> Unit`?

**A)** They're identical
**B)** First takes T as parameter, second has T as receiver (this)
**C)** Second is faster
**D)** First is type-safe, second isn't

**Answer**: **B** - `(T) -> Unit` takes T as a parameter, while `T.() -> Unit` has T as the receiver, accessible as `this`.

---

### Question 2: DSL Marker

What does `@DslMarker` do?

**A)** Makes DSLs faster
**B)** Prevents implicit receiver mixing in nested scopes
**C)** Enables reflection on DSLs
**D)** Makes DSLs type-safe

**Answer**: **B** - `@DslMarker` prevents accidentally calling outer scope functions from inner scopes in nested DSLs.

---

### Question 3: Type-Safe Builders

What makes a builder "type-safe"?

**A)** It's written in Kotlin
**B)** Compiler checks types at compile time
**C)** It uses strings
**D)** It throws exceptions

**Answer**: **B** - Type-safe builders leverage Kotlin's type system so the compiler catches errors at compile time.

---

### Question 4: When to Use DSLs

When should you create a DSL?

**A)** For every class
**B)** When you have complex, hierarchical configurations
**C)** Only for HTML
**D)** Never, they're too complex

**Answer**: **B** - DSLs are best for complex, hierarchical configurations where a fluent API improves readability.

---

### Question 5: initTag Pattern

In HTML DSL, what does `initTag` typically do?

**A)** Deletes a tag
**B)** Creates, configures, and adds a child tag
**C)** Validates HTML
**D)** Converts to string

**Answer**: **B** - `initTag` creates a tag, runs its configuration lambda, adds it to children, and returns it.

---

