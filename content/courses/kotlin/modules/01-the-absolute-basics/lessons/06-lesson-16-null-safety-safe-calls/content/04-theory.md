---
type: "THEORY"
title: "Understanding Null"
---


### What is null?

`null` represents **absence of a value**—nothing, empty, doesn't exist.

**Real-World Examples**:
- Phone number field when user hasn't provided one
- Middle name when person doesn't have one
- Search result when nothing matches
- User session when not logged in

### The Problem with Null (in other languages)
In many languages (like Java), any variable can be null at any time. If you try to call a function on a null variable, your entire program crashes. This is known as a **NullPointerException**.

**In Kotlin**: This doesn't compile! The compiler forces you to acknowledge that a variable might be null before you can use it.

---



```java
// Java example - this crashes at runtime!
String name = null;
int length = name.length();  // NullPointerException!
```
