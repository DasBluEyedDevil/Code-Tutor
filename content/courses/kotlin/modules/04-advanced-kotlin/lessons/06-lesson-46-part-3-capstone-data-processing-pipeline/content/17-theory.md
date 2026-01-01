---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: B) Sequences use lazy evaluation, processing elements only as needed**


Sequences excel with large data and partial results.

---

**Question 2: B) They create fluent, chainable APIs that read naturally**


Reads left-to-right, naturally chains operations.

---

**Question 3: B) It filters out null values automatically while mapping**


More concise and expresses intent clearly.

---

**Question 4: B) Calling the function without dot notation: `records inCategory "Electronics"`**


Reads more naturally, like English.

---

**Question 5: B) Separation of concerns: easier to test, reuse, and maintain**


Modular design is a core programming principle.

---



```kotlin
// Separated: easy to test each part
val parsed = CsvParser.parseCSV(data)
val validated = Validators.validate(parsed)
val analyzed = Analytics.analyze(validated)

// Each component can be:
// - Tested independently
// - Reused in different contexts
// - Modified without affecting others
// - Understood in isolation
```
