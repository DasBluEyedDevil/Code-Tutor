---
type: "THEORY"
title: "Navigation with Arguments"
---


### Passing Simple Arguments


### Optional Arguments


### Type-Safe Navigation (Recommended)


---



```kotlin
// Define routes
sealed class Screen(val route: String) {
    object Home : Screen("home")
    object Profile : Screen("profile")
    data class Details(val userId: Int) : Screen("details/$userId") {
        companion object {
            const val route = "details/{userId}"
        }
    }
}

// Navigation graph
NavHost(navController = navController, startDestination = Screen.Home.route) {
    composable(Screen.Home.route) {
        HomeScreen(onNavigateToDetails = { userId ->
            navController.navigate(Screen.Details(userId).route)
        })
    }

    composable(
        route = Screen.Details.route,
        arguments = listOf(navArgument("userId") { type = NavType.IntType })
    ) { backStackEntry ->
        val userId = backStackEntry.arguments?.getInt("userId") ?: 0
        DetailsScreen(userId = userId)
    }
}
```
