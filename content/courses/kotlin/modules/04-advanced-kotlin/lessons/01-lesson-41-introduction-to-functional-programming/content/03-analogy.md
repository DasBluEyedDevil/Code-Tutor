---
type: "ANALOGY"
title: "The Concept: What Is Functional Programming?"
---


### The Assembly Line Analogy

Imagine two approaches to making a pizza:

**Imperative Approach** (Traditional Programming):

**Functional Approach**:

The functional approach:
- Chains operations together
- Each step transforms data and passes it forward
- Reads more naturally
- Easier to understand at a glance

### Core Principles of Functional Programming

**1. Functions Are First-Class Citizens**

In FP, functions are values just like numbers or strings. You can:
- Store them in variables
- Pass them to other functions
- Return them from functions
- Create them on the fly


**2. Higher-Order Functions**

Functions that take other functions as parameters or return functions:


**3. Immutability**

Prefer values that don't change (immutable data):


**4. Pure Functions**

Functions with no side effects—same input always gives same output:


---



```kotlin
// ✅ Pure function
fun add(a: Int, b: Int): Int = a + b

// ❌ Impure function (depends on external state)
var discount = 0.1
fun applyDiscount(price: Double): Double = price * (1 - discount)
```
