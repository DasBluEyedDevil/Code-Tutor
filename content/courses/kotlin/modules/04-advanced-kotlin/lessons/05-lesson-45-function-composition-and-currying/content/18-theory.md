---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: B) Combining functions to create new functions where output of one becomes input of another**


Composition builds complex operations from simple parts.

---

**Question 2: A) Converting a multi-parameter function into a sequence of single-parameter functions**


Currying enables partial application and function specialization.

---

**Question 3: B) Allows calling functions without dot notation and parentheses (binary operation style)**


Makes code read more naturally.

---

**Question 4: B) Defining custom behavior for operators like +, -, *, / on custom types**


Enables intuitive syntax for custom types.

---

**Question 5: B) An API designed to read like natural language for a specific domain**


DSLs make code expressive and domain-specific.

---



```kotlin
// DSL for HTML
html {
    head {
        title { +"My Page" }
    }
    body {
        h1 { +"Welcome" }
    }
}

// Reads like HTML structure!
```
