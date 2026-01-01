---
type: "EXAMPLE"
title: "Code Quality Review"
---


Let's analyze what makes this project high-quality:

### 1. Single Responsibility Principle

Each function has one clear job:


### 2. Descriptive Naming

Names clearly indicate purpose:


### 3. Consistent Formatting


### 4. Reusability

Functions can be used in different contexts:


### 5. Parameter Flexibility

Functions accept parameters for customization:


---



```kotlin
// Same function, different uses
val ageIn10Years = calculateFutureAge(age, 10)
val ageIn20Years = calculateFutureAge(age, 20)
val ageIn50Years = calculateFutureAge(age, 50)
```
