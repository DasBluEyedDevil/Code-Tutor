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


**In Kotlin**: This doesn't compile! The compiler catches it.

---



```java
// Java example - this crashes at runtime!
String name = null;
int length = name.length();  // NullPointerException!
```
