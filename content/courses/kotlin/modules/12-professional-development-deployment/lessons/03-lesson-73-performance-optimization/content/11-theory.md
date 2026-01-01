---
type: "THEORY"
title: "Solution 1"
---



**Improvements**:
1. ✅ Stable parameters (`UserUiState`, lambda references)
2. ✅ Keys in LazyColumn
3. ✅ UI state pre-computed in ViewModel
4. ✅ Image loading with Coil (handles caching)
5. ✅ `derivedStateOf` for filtering
6. ✅ No ViewModel passed to composables

---



```kotlin
@Stable
data class UserUiState(
    val id: String,
    val name: String,
    val avatarUrl: String,
    val isOnline: Boolean,
    val unreadCount: Int
)

@Composable
fun UserListScreen(viewModel: UserViewModel) {
    val users by viewModel.usersUiState.collectAsState()
    val searchQuery by viewModel.searchQuery.collectAsState()

    Column {
        SearchBar(
            query = searchQuery,
            onQueryChange = viewModel::updateSearchQuery
        )

        // derivedStateOf - only recalculate when users or query changes
        val filteredUsers by remember {
            derivedStateOf {
                if (searchQuery.isBlank()) {
                    users
                } else {
                    users.filter { it.name.contains(searchQuery, ignoreCase = true) }
                }
            }
        }

        UserList(
            users = filteredUsers,
            onUserClick = viewModel::selectUser
        )
    }
}

@Composable
fun UserList(
    users: List<UserUiState>,
    onUserClick: (String) -> Unit
) {
    LazyColumn {
        items(
            items = users,
            key = { it.id } // ✅ Stable keys
        ) { user ->
            UserCard(
                user = user,
                onClick = { onUserClick(user.id) }
            )
        }
    }
}

@Composable
fun UserCard(
    user: UserUiState,
    onClick: () -> Unit
) {
    Card(onClick = onClick) {
        Row {
            // ✅ Coil handles caching
            AsyncImage(
                model = user.avatarUrl,
                contentDescription = null,
                modifier = Modifier.size(48.dp)
            )

            Column {
                Text(user.name)
                Text("Unread: ${user.unreadCount}")

                if (user.isOnline) {
                    OnlineBadge()
                }
            }
        }
    }
}

class UserViewModel : ViewModel() {
    private val _searchQuery = MutableStateFlow("")
    val searchQuery = _searchQuery.asStateFlow()

    // Pre-compute UI state in ViewModel
    val usersUiState: StateFlow<List<UserUiState>> = combine(
        userRepository.users,
        onlineStatusRepository.onlineUsers,
        messageRepository.unreadCounts
    ) { users, onlineIds, unreadCounts ->
        users.map { user ->
            UserUiState(
                id = user.id,
                name = user.name,
                avatarUrl = user.avatarUrl,
                isOnline = user.id in onlineIds,
                unreadCount = unreadCounts[user.id] ?: 0
            )
        }
    }.stateIn(
        scope = viewModelScope,
        started = SharingStarted.WhileSubscribed(5000),
        initialValue = emptyList()
    )

    fun updateSearchQuery(query: String) {
        _searchQuery.value = query
    }

    fun selectUser(userId: String) {
        // Handle selection
    }
}
```
