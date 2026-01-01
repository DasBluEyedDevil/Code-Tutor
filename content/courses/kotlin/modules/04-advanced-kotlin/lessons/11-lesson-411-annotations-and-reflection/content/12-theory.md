---
type: "THEORY"
title: "Checkpoint Quiz"
---


### Question 1: Annotation Retention

What does `@Retention(AnnotationRetention.RUNTIME)` mean?

**A)** Annotation is discarded after compilation
**B)** Annotation is available at runtime via reflection
**C)** Annotation only works at compile time
**D)** Annotation is stored in source code only

**Answer**: **B** - `RUNTIME` retention makes annotations available at runtime for reflection.

---

### Question 2: KClass

How do you get a KClass reference from an instance?

**A)** `instance.class`
**B)** `instance::class`
**C)** `instance.getClass()`
**D)** `classOf(instance)`

**Answer**: **B** - Use `instance::class` to get KClass from an instance.

---

### Question 3: @JvmStatic

What does `@JvmStatic` do?

**A)** Makes a property immutable
**B)** Generates a static method for Java interop
**C)** Prevents inheritance
**D)** Makes a class final

**Answer**: **B** - `@JvmStatic` generates a static method in the companion object for Java interoperability.

---

### Question 4: Reflection Performance

What's a disadvantage of reflection?

**A)** It's type-safe
**B)** It's slower than direct access
**C)** It can't access private members
**D)** It only works with data classes

**Answer**: **B** - Reflection is slower than direct access because it involves runtime type checking and dynamic invocation.

---

### Question 5: Annotation Targets

Which target allows annotating a property's backing field?

**A)** `@field:`
**B)** `@property:`
**C)** `@get:`
**D)** `@param:`

**Answer**: **A** - Use `@field:` to annotate the backing field of a property.

---

