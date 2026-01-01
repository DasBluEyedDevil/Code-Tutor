---
type: "THEORY"
title: "Navigation Drawer"
---



---



```kotlin
import androidx.compose.material3.DrawerValue
import androidx.compose.material3.ModalDrawerSheet
import androidx.compose.material3.ModalNavigationDrawer
import androidx.compose.material3.rememberDrawerState
import kotlinx.coroutines.launch

sealed class DrawerItem(val route: String, val icon: ImageVector, val label: String) {
    object Home : DrawerItem("home", Icons.Default.Home, "Home")
    object Settings : DrawerItem("settings", Icons.Default.Settings, "Settings")
    object About : DrawerItem("about", Icons.Default.Info, "About")
}

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun AppWithDrawer() {
    val navController = rememberNavController()
    val drawerState = rememberDrawerState(initialValue = DrawerValue.Closed)
    val scope = rememberCoroutineScope()

    val drawerItems = listOf(
        DrawerItem.Home,
        DrawerItem.Settings,
        DrawerItem.About
    )

    ModalNavigationDrawer(
        drawerState = drawerState,
        drawerContent = {
            ModalDrawerSheet {
                Text(
                    "My App",
                    modifier = Modifier.padding(16.dp),
                    style = MaterialTheme.typography.headlineMedium
                )

                HorizontalDivider()

                drawerItems.forEach { item ->
                    NavigationDrawerItem(
                        icon = { Icon(item.icon, contentDescription = null) },
                        label = { Text(item.label) },
                        selected = false,
                        onClick = {
                            navController.navigate(item.route)
                            scope.launch { drawerState.close() }
                        },
                        modifier = Modifier.padding(horizontal = 12.dp)
                    )
                }
            }
        }
    ) {
        Scaffold(
            topBar = {
                TopAppBar(
                    title = { Text("My App") },
                    navigationIcon = {
                        IconButton(onClick = {
                            scope.launch { drawerState.open() }
                        }) {
                            Icon(Icons.Default.Menu, contentDescription = "Menu")
                        }
                    }
                )
            }
        ) { innerPadding ->
            NavHost(
                navController = navController,
                startDestination = DrawerItem.Home.route,
                modifier = Modifier.padding(innerPadding)
            ) {
                composable(DrawerItem.Home.route) { HomeScreen() }
                composable(DrawerItem.Settings.route) { SettingsScreen() }
                composable(DrawerItem.About.route) { AboutScreen() }
            }
        }
    }
}
```
