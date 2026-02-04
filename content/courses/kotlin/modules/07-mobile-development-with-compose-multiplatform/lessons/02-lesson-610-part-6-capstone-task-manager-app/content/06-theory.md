---
type: "THEORY"
title: "Complete Implementation"
---


### 1. Data Models


### 2. Database Layer


### 3. Repository


### 4. Dependency Injection


### 5. Home Screen


### 6. Add/Edit Screen


### 7. Statistics Screen


---



```kotlin
// ui/screens/statistics/StatisticsScreen.kt
@Composable
fun StatisticsScreen(
    onNavigateBack: () -> Unit,
    viewModel: StatisticsViewModel  // Provided via Koin or manual DI
) {
    val stats by viewModel.stats.collectAsState()

    Scaffold(
        topBar = {
            TopAppBar(
                title = { Text("Statistics") },
                navigationIcon = {
                    IconButton(onClick = onNavigateBack) {
                        Icon(Icons.Default.ArrowBack, contentDescription = "Back")
                    }
                }
            )
        }
    ) { innerPadding ->
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(innerPadding)
                .padding(16.dp),
            verticalArrangement = Arrangement.spacedBy(16.dp)
        ) {
            // Summary cards
            Row(horizontalArrangement = Arrangement.spacedBy(8.dp)) {
                StatCard(
                    title = "Total",
                    value = stats.totalTasks.toString(),
                    modifier = Modifier.weight(1f)
                )
                StatCard(
                    title = "Completed",
                    value = stats.completedTasks.toString(),
                    modifier = Modifier.weight(1f)
                )
                StatCard(
                    title = "Active",
                    value = stats.activeTasks.toString(),
                    modifier = Modifier.weight(1f)
                )
            }

            // Completion percentage
            LinearProgressIndicator(
                progress = stats.completionPercentage,
                modifier = Modifier.fillMaxWidth()
            )
            Text("${(stats.completionPercentage * 100).toInt()}% Completed")

            // By category
            Text("By Category", style = MaterialTheme.typography.titleMedium)
            stats.byCategory.forEach { (category, count) ->
                Row(
                    modifier = Modifier.fillMaxWidth(),
                    horizontalArrangement = Arrangement.SpaceBetween
                ) {
                    Text("${category.icon} ${category.displayName}")
                    Text("$count")
                }
            }
        }
    }
}

@Composable
fun StatCard(title: String, value: String, modifier: Modifier = Modifier) {
    Card(modifier = modifier) {
        Column(
            modifier = Modifier.padding(16.dp),
            horizontalAlignment = Alignment.CenterHorizontally
        ) {
            Text(value, style = MaterialTheme.typography.headlineMedium)
            Text(title, style = MaterialTheme.typography.bodySmall)
        }
    }
}
```
