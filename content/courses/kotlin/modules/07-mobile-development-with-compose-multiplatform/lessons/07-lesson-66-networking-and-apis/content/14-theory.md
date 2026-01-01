---
type: "THEORY"
title: "Solution 2"
---



---



```kotlin
import kotlinx.coroutines.FlowPreview
import kotlinx.coroutines.flow.*
import kotlinx.coroutines.delay

data class SearchUiState(
    val allUsers: List<User> = emptyList(),
    val filteredUsers: List<User> = emptyList(),
    val searchQuery: String = "",
    val isLoading: Boolean = false,
    val errorMessage: String? = null
)

class SearchViewModel(
    private val repository: UserRepository = UserRepository(RetrofitClient.apiService)
) : ViewModel() {

    private val _uiState = MutableStateFlow(SearchUiState())
    val uiState: StateFlow<SearchUiState> = _uiState.asStateFlow()

    private val searchQuery = MutableStateFlow("")

    init {
        loadUsers()

        // Debounced search
        viewModelScope.launch {
            searchQuery
                .debounce(300)  // Wait 300ms after user stops typing
                .collect { query ->
                    filterUsers(query)
                }
        }
    }

    private fun loadUsers() {
        viewModelScope.launch {
            _uiState.value = _uiState.value.copy(isLoading = true)

            when (val result = repository.getUsers()) {
                is Result.Success -> {
                    _uiState.value = _uiState.value.copy(
                        allUsers = result.data,
                        filteredUsers = result.data,
                        isLoading = false
                    )
                }
                is Result.Error -> {
                    _uiState.value = _uiState.value.copy(
                        isLoading = false,
                        errorMessage = result.message
                    )
                }
                else -> {}
            }
        }
    }

    fun onSearchQueryChange(query: String) {
        _uiState.value = _uiState.value.copy(searchQuery = query)
        searchQuery.value = query
    }

    private fun filterUsers(query: String) {
        val filtered = if (query.isEmpty()) {
            _uiState.value.allUsers
        } else {
            _uiState.value.allUsers.filter {
                it.name.contains(query, ignoreCase = true) ||
                it.email.contains(query, ignoreCase = true)
            }
        }

        _uiState.value = _uiState.value.copy(filteredUsers = filtered)
    }
}

@Composable
fun SearchUsersScreen(viewModel: SearchViewModel = viewModel()) {
    val uiState by viewModel.uiState.collectAsState()

    Column(modifier = Modifier.fillMaxSize()) {
        // Search field
        OutlinedTextField(
            value = uiState.searchQuery,
            onValueChange = { viewModel.onSearchQueryChange(it) },
            label = { Text("Search users") },
            leadingIcon = {
                Icon(Icons.Default.Search, contentDescription = null)
            },
            modifier = Modifier
                .fillMaxWidth()
                .padding(16.dp)
        )

        // Results
        if (uiState.isLoading) {
            Box(modifier = Modifier.fillMaxSize(), contentAlignment = Alignment.Center) {
                CircularProgressIndicator()
            }
        } else {
            LazyColumn(
                contentPadding = PaddingValues(horizontal = 16.dp),
                verticalArrangement = Arrangement.spacedBy(8.dp)
            ) {
                items(uiState.filteredUsers) { user ->
                    UserCard(user)
                }

                if (uiState.filteredUsers.isEmpty() && uiState.searchQuery.isNotEmpty()) {
                    item {
                        Text(
                            "No users found",
                            modifier = Modifier.padding(16.dp),
                            style = MaterialTheme.typography.bodyLarge
                        )
                    }
                }
            }
        }
    }
}
```
