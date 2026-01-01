---
type: "THEORY"
title: "What is State?"
---


**State** is any value that can change over time and affects what's displayed in the UI.

### Examples of State


---



```kotlin
// UI state
var isLoading: Boolean = false
var errorMessage: String? = null
var searchQuery: String = ""

// Data state
var userProfile: User? = null
var todoList: List<Todo> = emptyList()
var selectedTab: Int = 0

// Form state
var email: String = ""
var password: String = ""
var agreeToTerms: Boolean = false
```
