---
type: "THEORY"
title: "Deep Linking"
---


### Setup in Manifest


### Deep Link in NavGraph


---



```kotlin
composable(
    route = "profile/{userId}",
    arguments = listOf(navArgument("userId") { type = NavType.IntType }),
    deepLinks = listOf(navDeepLink { uriPattern = "myapp://profile/{userId}" })
) { backStackEntry ->
    val userId = backStackEntry.arguments?.getInt("userId") ?: 0
    ProfileScreen(userId = userId)
}

// Users can open: myapp://profile/123
// App navigates directly to ProfileScreen with userId=123
```
