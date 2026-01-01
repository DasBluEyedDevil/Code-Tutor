---
type: "THEORY"
title: "Why Kotlin?"
---


### The Kotlin Story

Kotlin was created by JetBrains (makers of IntelliJ IDEA) in 2011 and officially released in 2016. In 2017, Google announced Kotlin as an official language for Android development. In 2019, Google declared Kotlin the **preferred language** for Android.

### Kotlin's Superpowers

**1. Modern & Concise**

Compare Java vs Kotlin for the same task:



**Same functionality, 90% less code!**

**2. Null Safety Built-In**

One of the most common programming errors is the "null pointer exception" (trying to use something that doesn't exist). Kotlin prevents this at compile-time:


**3. Multiplatform**

Write code once, run it everywhere:
- **Android**: Mobile apps
- **JVM**: Backend servers, desktop apps
- **JavaScript**: Web frontend
- **Native**: iOS apps, embedded systems

### Industry Adoption

Companies using Kotlin:
- **Google**: Android OS and apps
- **Netflix**: Mobile apps
- **Uber**: Internal tools
- **Pinterest**: Mobile apps
- **Trello**: Android app
- **Coursera**: Android app
- **Evernote**: Android app

**Job Market**: Over 50,000 Kotlin developer jobs posted in 2024 (Indeed, LinkedIn).

---



```kotlin
var name: String = "Alice"
name = null  // ❌ Compiler error: "Null can not be a value of a non-null type String"

var nullableName: String? = "Bob"
nullableName = null  // ✅ OK, we explicitly said this can be null
```
