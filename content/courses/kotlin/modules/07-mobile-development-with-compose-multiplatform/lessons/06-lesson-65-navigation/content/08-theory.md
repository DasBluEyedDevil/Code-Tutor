---
type: "THEORY"
title: "Nested Navigation"
---



---



```kotlin
// Main navigation graph
NavHost(navController = mainNavController, startDestination = "main") {
    // Auth flow
    navigation(startDestination = "login", route = "auth") {
        composable("login") { LoginScreen() }
        composable("register") { RegisterScreen() }
    }

    // Main app flow
    navigation(startDestination = "home", route = "main") {
        composable("home") { HomeScreen() }
        composable("profile") { ProfileScreen() }

        // Nested settings flow
        navigation(startDestination = "settings_main", route = "settings") {
            composable("settings_main") { SettingsScreen() }
            composable("settings_account") { AccountSettingsScreen() }
            composable("settings_privacy") { PrivacySettingsScreen() }
        }
    }
}
```
