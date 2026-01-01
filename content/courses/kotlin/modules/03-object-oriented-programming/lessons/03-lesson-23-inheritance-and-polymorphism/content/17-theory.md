---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: B) `open`**

Kotlin classes are final by default. Use `open` to allow inheritance.


---

**Question 2: B) The ability to treat objects of different types through a common interface**

Polymorphism lets you write code that works with a superclass but automatically uses the correct subclass implementation.


---

**Question 3: D) Both B and C**

Abstract classes provide partial implementation (some methods implemented, some abstract) and force subclasses to implement abstract methods.


---

**Question 4: C) Calls the superclass's implementation**

Use `super` to access the parent class's methods or properties.


---

**Question 5: B) Automatic type casting after a type check with `is`**

After checking a type with `is`, Kotlin automatically casts the variable.


---



```kotlin
if (animal is Dog) {
    animal.fetch()  // No explicit cast needed!
}
```
