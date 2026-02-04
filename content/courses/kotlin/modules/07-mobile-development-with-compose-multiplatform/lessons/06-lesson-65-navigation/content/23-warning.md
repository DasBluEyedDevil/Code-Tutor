---
type: "WARNING"
title: "Back Stack Management Issues"
---

**Uncontrolled back stacks lead to confusing navigation** where users can't find their way out of the app or encounter unexpected screen orders.

**Don't create circular navigation**:
```kotlin
// BAD - A -> B -> C -> A creates a loop
navigator.push(ScreenA())  // From C
```

Users pressing back repeatedly cycle through A-B-C forever instead of exiting the app.

**Pop to root when switching main sections**:
```kotlin
// BAD - switching tabs stacks screens infinitely
bottomNavItem.onClick { navigator.push(HomeScreen()) }

// GOOD - clear back stack when switching tabs
bottomNavItem.onClick {
    navigator.popUntilRoot()
    navigator.push(HomeScreen())
}
```

**Don't put login screens in the back stack** after successful login:
```kotlin
// BAD - user can press back to return to login
loginSuccess {
    navigator.push(HomeScreen())
}

// GOOD - replace login with home, can't go back
loginSuccess {
    navigator.replaceAll(HomeScreen())
}
```

**Be careful with state restoration**—if navigation state isn't saved properly, process death (Android) or app backgrounding loses navigation history, frustrating users.

Test navigation thoroughly including back button, deep links, and process death scenarios.
