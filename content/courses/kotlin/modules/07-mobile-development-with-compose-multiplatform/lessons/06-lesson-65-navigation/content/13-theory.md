---
type: "THEORY"
title: "Solution 2"
---



---



```kotlin
sealed class SettingsScreen(val route: String, val title: String) {
    object Main : SettingsScreen("settings_main", "Settings")
    object General : SettingsScreen("settings_general", "General")
    object Account : SettingsScreen("settings_account", "Account")
    object Privacy : SettingsScreen("settings_privacy", "Privacy")
}

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun SettingsApp() {
    val navController = rememberNavController()

    Scaffold(
        topBar = {
            val navBackStackEntry by navController.currentBackStackEntryAsState()
            val currentRoute = navBackStackEntry?.destination?.route
            val title = when (currentRoute) {
                SettingsScreen.General.route -> SettingsScreen.General.title
                SettingsScreen.Account.route -> SettingsScreen.Account.title
                SettingsScreen.Privacy.route -> SettingsScreen.Privacy.title
                else -> SettingsScreen.Main.title
            }

            TopAppBar(
                title = { Text(title) },
                navigationIcon = {
                    if (currentRoute != SettingsScreen.Main.route) {
                        IconButton(onClick = { navController.popBackStack() }) {
                            Icon(Icons.Default.ArrowBack, contentDescription = "Back")
                        }
                    }
                }
            )
        }
    ) { innerPadding ->
        NavHost(
            navController = navController,
            startDestination = SettingsScreen.Main.route,
            modifier = Modifier.padding(innerPadding)
        ) {
            composable(SettingsScreen.Main.route) {
                SettingsMainScreen(onNavigate = { route ->
                    navController.navigate(route)
                })
            }

            composable(SettingsScreen.General.route) {
                GeneralSettingsScreen()
            }

            composable(SettingsScreen.Account.route) {
                AccountSettingsScreen()
            }

            composable(SettingsScreen.Privacy.route) {
                PrivacySettingsScreen()
            }
        }
    }
}

@Composable
fun SettingsMainScreen(onNavigate: (String) -> Unit) {
    LazyColumn {
        item {
            SettingsItem(
                title = "General",
                subtitle = "App preferences",
                icon = Icons.Default.Settings,
                onClick = { onNavigate(SettingsScreen.General.route) }
            )
        }
        item {
            SettingsItem(
                title = "Account",
                subtitle = "Manage your account",
                icon = Icons.Default.Person,
                onClick = { onNavigate(SettingsScreen.Account.route) }
            )
        }
        item {
            SettingsItem(
                title = "Privacy",
                subtitle = "Privacy and security",
                icon = Icons.Default.Lock,
                onClick = { onNavigate(SettingsScreen.Privacy.route) }
            )
        }
    }
}

@Composable
fun SettingsItem(
    title: String,
    subtitle: String,
    icon: ImageVector,
    onClick: () -> Unit
) {
    Row(
        modifier = Modifier
            .fillMaxWidth()
            .clickable(onClick = onClick)
            .padding(16.dp),
        verticalAlignment = androidx.compose.ui.Alignment.CenterVertically
    ) {
        Icon(icon, contentDescription = null, tint = MaterialTheme.colorScheme.primary)
        Spacer(modifier = Modifier.width(16.dp))
        Column(modifier = Modifier.weight(1f)) {
            Text(title, style = MaterialTheme.typography.bodyLarge)
            Text(subtitle, style = MaterialTheme.typography.bodySmall, color = MaterialTheme.colorScheme.onSurfaceVariant)
        }
        Icon(Icons.Default.ChevronRight, contentDescription = null)
    }
}

@Composable
fun GeneralSettingsScreen() {
    var darkMode by remember { mutableStateOf(false) }
    var notifications by remember { mutableStateOf(true) }

    Column(modifier = Modifier.padding(16.dp)) {
        SwitchSetting("Dark Mode", darkMode) { darkMode = it }
        SwitchSetting("Notifications", notifications) { notifications = it }
    }
}

@Composable
fun AccountSettingsScreen() {
    Column(modifier = Modifier.padding(16.dp)) {
        Text("Email: user@example.com")
        Spacer(modifier = Modifier.height(16.dp))
        Button(onClick = { }) {
            Text("Change Password")
        }
    }
}

@Composable
fun PrivacySettingsScreen() {
    var analytics by remember { mutableStateOf(true) }

    Column(modifier = Modifier.padding(16.dp)) {
        SwitchSetting("Share Analytics", analytics) { analytics = it }
    }
}

@Composable
fun SwitchSetting(label: String, checked: Boolean, onCheckedChange: (Boolean) -> Unit) {
    Row(
        modifier = Modifier.fillMaxWidth().padding(vertical = 8.dp),
        horizontalArrangement = Arrangement.SpaceBetween,
        verticalAlignment = androidx.compose.ui.Alignment.CenterVertically
    ) {
        Text(label)
        Switch(checked = checked, onCheckedChange = onCheckedChange)
    }
}
```
