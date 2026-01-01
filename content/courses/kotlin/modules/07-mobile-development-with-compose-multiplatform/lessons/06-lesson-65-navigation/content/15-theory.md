---
type: "THEORY"
title: "Solution 3"
---



---



```kotlin
@Composable
fun TabbedApp() {
    var selectedTab by remember { mutableStateOf(0) }
    val tabs = listOf("Feed", "Discover", "Profile")

    Column(modifier = Modifier.fillMaxSize()) {
        TabRow(selectedTabIndex = selectedTab) {
            tabs.forEachIndexed { index, title ->
                Tab(
                    selected = selectedTab == index,
                    onClick = { selectedTab = index },
                    text = { Text(title) }
                )
            }
        }

        when (selectedTab) {
            0 -> FeedScreen()
            1 -> DiscoverScreen()
            2 -> ProfileScreen()
        }
    }
}

@Composable
fun FeedScreen() {
    Box(modifier = Modifier.fillMaxSize(), contentAlignment = androidx.compose.ui.Alignment.Center) {
        Text("Feed Content", style = MaterialTheme.typography.headlineMedium)
    }
}

@Composable
fun DiscoverScreen() {
    Box(modifier = Modifier.fillMaxSize(), contentAlignment = androidx.compose.ui.Alignment.Center) {
        Text("Discover Content", style = MaterialTheme.typography.headlineMedium)
    }
}

@Composable
fun ProfileScreen() {
    Box(modifier = Modifier.fillMaxSize(), contentAlignment = androidx.compose.ui.Alignment.Center) {
        Text("Profile Content", style = MaterialTheme.typography.headlineMedium)
    }
}
```
