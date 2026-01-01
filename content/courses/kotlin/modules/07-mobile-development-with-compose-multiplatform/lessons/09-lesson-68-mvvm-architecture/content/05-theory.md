---
type: "THEORY"
title: "LiveData vs StateFlow"
---


### LiveData (Legacy)


### StateFlow (Modern, Recommended)


### Comparison

| Feature              | LiveData        | StateFlow         |
|----------------------|-----------------|-------------------|
| **Lifecycle aware**  | Yes             | No (use collectAsStateWithLifecycle) |
| **Initial value**    | Optional        | Required          |
| **Kotlin/Multiplatform** | No          | Yes               |
| **Operators**        | Limited         | Full Flow API     |
| **Recommendation**   | Legacy          | **Use this**      |

---



```kotlin
class UserViewModel : ViewModel() {
    private val _users = MutableStateFlow<List<User>>(emptyList())
    val users: StateFlow<List<User>> = _users.asStateFlow()

    fun loadUsers() {
        viewModelScope.launch {
            _users.value = repository.getUsers()
        }
    }
}

// In Composable
@Composable
fun UsersScreen(viewModel: UserViewModel) {
    val users by viewModel.users.collectAsState()

    LazyColumn {
        items(users) { user ->
            Text(user.name)
        }
    }
}
```
