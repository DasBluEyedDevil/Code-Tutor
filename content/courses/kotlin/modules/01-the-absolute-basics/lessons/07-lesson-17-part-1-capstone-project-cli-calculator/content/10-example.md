---
type: "EXAMPLE"
title: "Code Quality Review"
---


Let's review what makes this code high-quality:

### 1. Single Responsibility Principle

Each function does ONE thing:

### 2. Descriptive Names

Names clearly indicate purpose:

### 3. Error Handling

Graceful error handling throughout:

### 4. Null Safety

Proper use of nullable types:

### 5. Code Organization

Clear sections and structure:
- Data models first
- Core functions
- UI functions
- Main program

### 6. User Experience

Professional, helpful interface:
- Clear menu
- Helpful error messages
- Confirmation messages
- Nice formatting

---



```kotlin
val num1 = getNumber("Enter first number: ")
if (num1 == null) {
    println("Invalid number!")
    return true
}
// num1 is smart-cast to Double here
```
