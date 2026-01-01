---
type: "THEORY"
title: "Navigation on iOS"
---


### Critical Platform Differences

Navigation behavior differs significantly between Android and iOS. Understanding these differences is crucial for building great cross-platform apps.

| Feature | Android | iOS |
|---------|---------|-----|
| **Back Navigation** | Hardware/gesture back button | Swipe from left edge |
| **Back Button** | System provides it | You must add a back button in the UI |
| **Navigation Bar** | Optional TopAppBar | Expected on every screen |
| **Swipe Gesture** | Edge swipe optional | Users expect swipe-back |
| **Tab Bar Position** | Bottom (Material) | Bottom (iOS standard) |
| **Drawer** | Common pattern | Less common on iOS |

### iOS Swipe-Back Gesture

On iOS, users expect to swipe from the left edge to go back. Compose Multiplatform supports this automatically when you use navigation properly:

```kotlin
// The navigation back stack handles iOS swipe-back
@Composable
fun DetailsScreen(
    onNavigateBack: () -> Unit
) {
    Scaffold(
        topBar = {
            TopAppBar(
                title = { Text("Details") },
                // Always show back button on iOS!
                navigationIcon = {
                    IconButton(onClick = onNavigateBack) {
                        Icon(
                            Icons.AutoMirrored.Filled.ArrowBack,
                            contentDescription = "Back"
                        )
                    }
                }
            )
        }
    ) { /* content */ }
}
```

### Best Practices for Cross-Platform Navigation

1. **Always include a visible back button** - iOS has no hardware back
2. **Use TopAppBar consistently** - iOS users expect it
3. **Avoid navigation drawers on iOS** - Use tab bars instead
4. **Test swipe-back gesture** - Run on iOS Simulator regularly
5. **Handle safe areas** - Status bar and home indicator differ

### Running Navigation on iOS Simulator

1. Build and run on iOS Simulator
2. Navigate to a detail screen
3. Try swiping from the left edge to go back
4. Verify the back button works
5. Test bottom navigation tabs

### Platform-Specific Navigation Patterns

```kotlin
// In commonMain - adapts to each platform
@Composable
fun AppNavigation() {
    val navController = rememberNavController()
    
    Scaffold(
        // Bottom bar works on both platforms
        bottomBar = {
            NavigationBar {
                // Tab items...
            }
        }
    ) { padding ->
        NavHost(
            navController = navController,
            startDestination = "home",
            modifier = Modifier.padding(padding)
        ) {
            composable("home") {
                HomeScreen(
                    onItemClick = { id ->
                        navController.navigate("details/$id")
                    }
                )
            }
            composable("details/{id}") { backStackEntry ->
                DetailsScreen(
                    id = backStackEntry.arguments?.getString("id") ?: "",
                    // popBackStack works on both Android and iOS!
                    onNavigateBack = { navController.popBackStack() }
                )
            }
        }
    }
}
```

---

