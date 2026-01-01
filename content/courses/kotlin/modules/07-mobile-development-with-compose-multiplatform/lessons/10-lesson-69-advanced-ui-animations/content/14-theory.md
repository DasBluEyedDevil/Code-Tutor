---
type: "THEORY"
title: "Solution 3"
---



---



```kotlin
@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun PullToRefreshExample() {
    var isRefreshing by remember { mutableStateOf(false) }
    var items by remember { mutableStateOf(List(20) { "Item $it" }) }
    val pullRefreshState = rememberPullToRefreshState()

    LaunchedEffect(isRefreshing) {
        if (isRefreshing) {
            delay(2000)  // Simulate network call
            items = List(20) { "Item ${it + items.size}" }
            isRefreshing = false
        }
    }

    Box(modifier = Modifier.fillMaxSize()) {
        LazyColumn(
            modifier = Modifier
                .fillMaxSize()
                .pullToRefresh(
                    state = pullRefreshState,
                    isRefreshing = isRefreshing,
                    onRefresh = { isRefreshing = true }
                )
        ) {
            items(items) { item ->
                Text(
                    item,
                    modifier = Modifier
                        .fillMaxWidth()
                        .padding(16.dp)
                )
            }
        }

        if (pullRefreshState.isRefreshing) {
            LinearProgressIndicator(modifier = Modifier.fillMaxWidth())
        }
    }
}
```
