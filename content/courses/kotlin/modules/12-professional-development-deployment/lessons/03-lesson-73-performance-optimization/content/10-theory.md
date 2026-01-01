---
type: "THEORY"
title: "Exercise 1: Optimize a Slow Screen"
---


You have a slow user list screen. Profile and optimize it.

### Initial Code (Slow)


### Performance Issues

1. ⚠️ Entire list recomposes when search query changes
2. ⚠️ No keys in LazyColumn
3. ⚠️ `isUserOnline()` and `getUnreadCount()` called on every recomposition
4. ⚠️ Images loaded from network on every recomposition
5. ⚠️ ViewModel passed to composable (unstable parameter)

---



```kotlin
@Composable
fun UserListScreen(viewModel: UserViewModel) {
    val users = viewModel.users.collectAsState()
    val searchQuery = viewModel.searchQuery.collectAsState()

    Column {
        SearchBar(
            query = searchQuery.value,
            onQueryChange = { viewModel.updateSearchQuery(it) }
        )

        LazyColumn {
            items(users.value) { user ->
                UserCard(
                    user = user,
                    isOnline = viewModel.isUserOnline(user.id),
                    messageCount = viewModel.getUnreadCount(user.id),
                    onClick = { viewModel.selectUser(user) }
                )
            }
        }
    }
}

@Composable
fun UserCard(
    user: User,
    isOnline: Boolean,
    messageCount: Int,
    onClick: () -> Unit
) {
    // Heavy image loading
    val avatar = loadImageFromNetwork(user.avatarUrl)

    Card(onClick = onClick) {
        Row {
            Image(bitmap = avatar, contentDescription = null)
            Column {
                Text(user.name)
                Text("Unread: $messageCount")
                if (isOnline) {
                    OnlineBadge()
                }
            }
        }
    }
}
```
