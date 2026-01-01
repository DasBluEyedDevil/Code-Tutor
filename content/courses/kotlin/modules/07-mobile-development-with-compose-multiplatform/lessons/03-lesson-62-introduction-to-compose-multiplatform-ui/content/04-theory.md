---
type: "THEORY"
title: "Composable Functions"
---


### The @Composable Annotation

A **composable function** is a regular Kotlin function annotated with `@Composable`:


**Rules**:
1. Must be annotated with `@Composable`
2. Can only be called from other `@Composable` functions
3. Can emit UI elements
4. Can call other `@Composable` functions

### Basic Composable


### Composable Naming Convention

**Convention**: Use **PascalCase** (same as classes):


**Why?**
- Composables represent UI components (like classes)
- Distinguishes them from regular functions
- Follows official Compose style guide

---



```kotlin
@Composable
fun UserProfile() { }  // ✅ Good

@Composable
fun userProfile() { }  // ❌ Bad (should be PascalCase)
```
