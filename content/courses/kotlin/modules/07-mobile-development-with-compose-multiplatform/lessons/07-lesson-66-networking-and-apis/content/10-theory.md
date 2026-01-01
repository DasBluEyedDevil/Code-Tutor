---
type: "THEORY"
title: "Advanced: Pagination"
---



---



```kotlin
class PaginatedViewModel : ViewModel() {
    private val _posts = MutableStateFlow<List<Post>>(emptyList())
    val posts: StateFlow<List<Post>> = _posts.asStateFlow()

    private val _isLoading = MutableStateFlow(false)
    val isLoading: StateFlow<Boolean> = _isLoading.asStateFlow()

    private var currentPage = 1
    private val pageSize = 20

    fun loadMore() {
        if (_isLoading.value) return

        viewModelScope.launch {
            _isLoading.value = true

            try {
                val newPosts = apiService.getPosts(
                    page = currentPage,
                    limit = pageSize
                )

                _posts.value = _posts.value + newPosts
                currentPage++
            } catch (e: Exception) {
                // Handle error
            } finally {
                _isLoading.value = false
            }
        }
    }
}

@Composable
fun PaginatedList(viewModel: PaginatedViewModel = viewModel()) {
    val posts by viewModel.posts.collectAsState()
    val isLoading by viewModel.isLoading.collectAsState()
    val listState = rememberLazyListState()

    LaunchedEffect(listState) {
        snapshotFlow { listState.layoutInfo.visibleItemsInfo.lastOrNull()?.index }
            .collect { lastVisibleIndex ->
                if (lastVisibleIndex != null && lastVisibleIndex >= posts.size - 5) {
                    viewModel.loadMore()
                }
            }
    }

    LazyColumn(state = listState) {
        items(posts) { post ->
            PostCard(post)
        }

        if (isLoading) {
            item {
                Box(
                    modifier = Modifier.fillMaxWidth().padding(16.dp),
                    contentAlignment = Alignment.Center
                ) {
                    CircularProgressIndicator()
                }
            }
        }
    }
}
```
