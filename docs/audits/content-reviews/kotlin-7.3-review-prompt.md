# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Kotlin Multiplatform Complete Course (kotlin)
- **Module:** Professional Development & Deployment
- **Lesson:** Lesson 7.3: Performance Optimization (ID: 7.3)
- **Difficulty:** advanced
- **Estimated Time:** 85 minutes

## Current Lesson Content

{
    "id":  "7.3",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "**Estimated Time**: 85 minutes\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "\n\"Premature optimization is the root of all evil\" - Donald Knuth\n\nBut **measured, strategic optimization** is the difference between a slow app that users delete and a fast app they love.\n\nIn this lesson, you\u0027ll master performance optimization for Kotlin applications:\n- ✅ Profiling tools to identify bottlenecks\n- ✅ Memory management and leak detection\n- ✅ Coroutine performance optimization\n- ✅ Jetpack Compose recomposition optimization\n- ✅ Database query optimization\n- ✅ Network performance best practices\n\nBy the end, you\u0027ll know how to build blazing-fast applications that delight users.\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "The Golden Rule of Optimization",
                                "content":  "\n### Measure First, Optimize Second\n\n**Wrong Approach**:\n\n**Right Approach**:\n\n**Why This Matters**:\n- 90% of execution time is spent in 10% of code\n- Optimizing the wrong code = wasted time\n- Profilers show you the **actual** bottlenecks\n\n---\n\n",
                                "code":  "// 1. Measure with profiler\n// 2. Find actual bottleneck (it\u0027s not where you think!)\n// 3. Optimize the bottleneck\n// 4. Measure again to verify improvement",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Profiling Tools",
                                "content":  "\n### Android Studio Profiler\n\n**CPU Profiler**:\n\nShows:\n- Which functions take the most time\n- Call stack and flame graphs\n- Thread activity\n\n**Example Output**:\n\n**Memory Profiler**:\n\nShows:\n- Memory allocation over time\n- Heap dumps\n- Memory leaks\n\n**Network Profiler**:\n\nShows:\n- Request/response times\n- Payload sizes\n- Connection duration\n\n### Ktor Server Profiling\n\n**Add Timing Plugin**:\n\n**Output**:\n\n---\n\n",
                                "code":  "GET /api/users - 45ms\nGET /api/products - 850ms ⚠️ SLOW!\nPOST /api/orders - 120ms",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Memory Management",
                                "content":  "\n### Detecting Memory Leaks\n\n**Common Leak: Activity Reference in ViewModel**:\n\n❌ **Bad**:\n\n✅ **Good**:\n\n**Common Leak: Coroutine Not Cancelled**:\n\n❌ **Bad**:\n\n✅ **Good**:\n\n**Better: Use lifecycleScope**:\n\n### Memory Leak Detection with LeakCanary\n\n\nLeakCanary automatically detects leaks and shows:\n- What object leaked\n- Reference path keeping it alive\n- Suggested fix\n\n---\n\n",
                                "code":  "// build.gradle.kts\ndependencies {\n    debugImplementation(\"com.squareup.leakcanary:leakcanary-android:2.13\")\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Coroutine Performance",
                                "content":  "\n### Dispatcher Selection\n\n**Wrong Dispatcher = Poor Performance**:\n\n❌ **Bad**:\n\n✅ **Good**:\n\n**Dispatcher Guide**:\n\n### Avoiding Excessive Coroutine Creation\n\n❌ **Bad** (Creates 1000 coroutines):\n\n✅ **Good** (Single coroutine):\n\n✅ **Better** (Parallel processing with limit):\n\n### Flow Performance\n\n**Cold vs Hot Flows**:\n\n❌ **Bad** (Network call on every collect):\n\n✅ **Good** (SharedFlow - single source):\n\n**Debouncing Search**:\n\n❌ **Bad** (API call on every keystroke):\n\n✅ **Good** (Debounce 300ms):\n\n---\n\n",
                                "code":  "searchField.textAsFlow()\n    .debounce(300)\n    .distinctUntilChanged()\n    .collectLatest { query -\u003e\n        viewModel.search(query)\n    }",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Jetpack Compose Optimization",
                                "content":  "\n### Recomposition Basics\n\n**What is Recomposition?**\n\nWhen state changes, Compose re-runs composables to update UI.\n\n**Problem**: Unnecessary recompositions = poor performance\n\n**Example**:\n\n### Optimization 1: Stable Parameters\n\n❌ **Bad** (Recomposes unnecessarily):\n\n✅ **Good** (Only necessary recompositions):\n\n### Optimization 2: derivedStateOf\n\n❌ **Bad** (Recalculates on every recomposition):\n\n✅ **Good** (Only recalculates when products change):\n\n### Optimization 3: LazyColumn Keys\n\n❌ **Bad** (Entire list recomposes):\n\n✅ **Good** (Only changed items recompose):\n\n### Optimization 4: Immutable Collections\n\n\n✅ **Good** (Compose knows it\u0027s immutable):\n\n### Measuring Recompositions\n\n\n---\n\n",
                                "code":  "@Composable\nfun LogCompositions(tag: String) {\n    val ref = remember { Ref(0) }\n    SideEffect {\n        ref.value++\n        Log.d(\"Recomposition\", \"$tag recomposed ${ref.value} times\")\n    }\n}\n\nclass Ref(var value: Int)\n\n@Composable\nfun MyScreen() {\n    LogCompositions(\"MyScreen\")\n\n    // Your content\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Database Optimization",
                                "content":  "\n### Query Optimization\n\n❌ **Bad** (N+1 queries):\n\n✅ **Good** (Single query with JOIN):\n\n### Indexing\n\n❌ **Bad** (Full table scan):\n\n✅ **Good** (Indexed):\n\n### Pagination\n\n❌ **Bad** (Load all 10,000 products):\n\n✅ **Good** (Paging):\n\n### Batch Operations\n\n❌ **Bad** (Individual inserts):\n\n✅ **Good** (Batch insert):\n\n---\n\n",
                                "code":  "@Insert\nsuspend fun insertAll(products: List\u003cProduct\u003e)\n\n// Single transaction - much faster\ndatabase.productDao().insertAll(products)",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Network Optimization",
                                "content":  "\n### Response Caching\n\n**HTTP Caching with OkHttp**:\n\n**Cache Headers**:\n\n### Compression\n\n\n### Request Coalescing\n\n❌ **Bad** (Multiple identical requests):\n\n✅ **Good** (Share single request):\n\n### Prefetching\n\n\n---\n\n",
                                "code":  "class ProductRepository {\n    private val cache = mutableMapOf\u003cString, Product\u003e()\n\n    suspend fun prefetchProducts(ids: List\u003cString\u003e) {\n        val uncachedIds = ids.filter { it !in cache }\n        if (uncachedIds.isEmpty()) return\n\n        val products = api.getProductsBatch(uncachedIds)\n        products.forEach { cache[it.id] = it }\n    }\n\n    suspend fun getProduct(id: String): Product {\n        return cache[id] ?: api.getProduct(id).also {\n            cache[id] = it\n        }\n    }\n}\n\n// Usage\nrepository.prefetchProducts(listOf(\"1\", \"2\", \"3\"))\n// Later...\nval product = repository.getProduct(\"1\") // Instant! (cached)",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise 1: Optimize a Slow Screen",
                                "content":  "\nYou have a slow user list screen. Profile and optimize it.\n\n### Initial Code (Slow)\n\n\n### Performance Issues\n\n1. ⚠️ Entire list recomposes when search query changes\n2. ⚠️ No keys in LazyColumn\n3. ⚠️ `isUserOnline()` and `getUnreadCount()` called on every recomposition\n4. ⚠️ Images loaded from network on every recomposition\n5. ⚠️ ViewModel passed to composable (unstable parameter)\n\n---\n\n",
                                "code":  "@Composable\nfun UserListScreen(viewModel: UserViewModel) {\n    val users = viewModel.users.collectAsState()\n    val searchQuery = viewModel.searchQuery.collectAsState()\n\n    Column {\n        SearchBar(\n            query = searchQuery.value,\n            onQueryChange = { viewModel.updateSearchQuery(it) }\n        )\n\n        LazyColumn {\n            items(users.value) { user -\u003e\n                UserCard(\n                    user = user,\n                    isOnline = viewModel.isUserOnline(user.id),\n                    messageCount = viewModel.getUnreadCount(user.id),\n                    onClick = { viewModel.selectUser(user) }\n                )\n            }\n        }\n    }\n}\n\n@Composable\nfun UserCard(\n    user: User,\n    isOnline: Boolean,\n    messageCount: Int,\n    onClick: () -\u003e Unit\n) {\n    // Heavy image loading\n    val avatar = loadImageFromNetwork(user.avatarUrl)\n\n    Card(onClick = onClick) {\n        Row {\n            Image(bitmap = avatar, contentDescription = null)\n            Column {\n                Text(user.name)\n                Text(\"Unread: $messageCount\")\n                if (isOnline) {\n                    OnlineBadge()\n                }\n            }\n        }\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Solution 1",
                                "content":  "\n\n**Improvements**:\n1. ✅ Stable parameters (`UserUiState`, lambda references)\n2. ✅ Keys in LazyColumn\n3. ✅ UI state pre-computed in ViewModel\n4. ✅ Image loading with Coil (handles caching)\n5. ✅ `derivedStateOf` for filtering\n6. ✅ No ViewModel passed to composables\n\n---\n\n",
                                "code":  "@Stable\ndata class UserUiState(\n    val id: String,\n    val name: String,\n    val avatarUrl: String,\n    val isOnline: Boolean,\n    val unreadCount: Int\n)\n\n@Composable\nfun UserListScreen(viewModel: UserViewModel) {\n    val users by viewModel.usersUiState.collectAsState()\n    val searchQuery by viewModel.searchQuery.collectAsState()\n\n    Column {\n        SearchBar(\n            query = searchQuery,\n            onQueryChange = viewModel::updateSearchQuery\n        )\n\n        // derivedStateOf - only recalculate when users or query changes\n        val filteredUsers by remember {\n            derivedStateOf {\n                if (searchQuery.isBlank()) {\n                    users\n                } else {\n                    users.filter { it.name.contains(searchQuery, ignoreCase = true) }\n                }\n            }\n        }\n\n        UserList(\n            users = filteredUsers,\n            onUserClick = viewModel::selectUser\n        )\n    }\n}\n\n@Composable\nfun UserList(\n    users: List\u003cUserUiState\u003e,\n    onUserClick: (String) -\u003e Unit\n) {\n    LazyColumn {\n        items(\n            items = users,\n            key = { it.id } // ✅ Stable keys\n        ) { user -\u003e\n            UserCard(\n                user = user,\n                onClick = { onUserClick(user.id) }\n            )\n        }\n    }\n}\n\n@Composable\nfun UserCard(\n    user: UserUiState,\n    onClick: () -\u003e Unit\n) {\n    Card(onClick = onClick) {\n        Row {\n            // ✅ Coil handles caching\n            AsyncImage(\n                model = user.avatarUrl,\n                contentDescription = null,\n                modifier = Modifier.size(48.dp)\n            )\n\n            Column {\n                Text(user.name)\n                Text(\"Unread: ${user.unreadCount}\")\n\n                if (user.isOnline) {\n                    OnlineBadge()\n                }\n            }\n        }\n    }\n}\n\nclass UserViewModel : ViewModel() {\n    private val _searchQuery = MutableStateFlow(\"\")\n    val searchQuery = _searchQuery.asStateFlow()\n\n    // Pre-compute UI state in ViewModel\n    val usersUiState: StateFlow\u003cList\u003cUserUiState\u003e\u003e = combine(\n        userRepository.users,\n        onlineStatusRepository.onlineUsers,\n        messageRepository.unreadCounts\n    ) { users, onlineIds, unreadCounts -\u003e\n        users.map { user -\u003e\n            UserUiState(\n                id = user.id,\n                name = user.name,\n                avatarUrl = user.avatarUrl,\n                isOnline = user.id in onlineIds,\n                unreadCount = unreadCounts[user.id] ?: 0\n            )\n        }\n    }.stateIn(\n        scope = viewModelScope,\n        started = SharingStarted.WhileSubscribed(5000),\n        initialValue = emptyList()\n    )\n\n    fun updateSearchQuery(query: String) {\n        _searchQuery.value = query\n    }\n\n    fun selectUser(userId: String) {\n        // Handle selection\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise 2: Optimize Database Queries",
                                "content":  "\nOptimize this slow order fetching code.\n\n### Initial Code (Slow)\n\n\n---\n\n",
                                "code":  "@Dao\ninterface OrderDao {\n    @Query(\"SELECT * FROM orders\")\n    fun getAllOrders(): List\u003cOrder\u003e\n\n    @Query(\"SELECT * FROM orders WHERE user_id = :userId\")\n    fun getOrdersByUser(userId: String): List\u003cOrder\u003e\n}\n\n// Usage\nfun displayUserOrders(userId: String) {\n    val orders = orderDao.getOrdersByUser(userId)\n\n    orders.forEach { order -\u003e\n        val user = userDao.getById(order.userId) // N+1 query!\n        val items = orderItemDao.getByOrderId(order.id) // N+1 query!\n\n        println(\"Order ${order.id} by ${user.name}: ${items.size} items\")\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Solution 2",
                                "content":  "\n\n---\n\n",
                                "code":  "// 1. Add indexes\n@Entity(\n    tableName = \"orders\",\n    indices = [\n        Index(value = [\"user_id\"]),\n        Index(value = [\"created_at\"])\n    ]\n)\ndata class Order(\n    @PrimaryKey val id: String,\n    val userId: String,\n    val totalAmount: Double,\n    val status: String,\n    val createdAt: Long\n)\n\n// 2. Create joined data class\ndata class OrderWithDetails(\n    @Embedded val order: Order,\n\n    @Relation(\n        parentColumn = \"user_id\",\n        entityColumn = \"id\"\n    )\n    val user: User,\n\n    @Relation(\n        parentColumn = \"id\",\n        entityColumn = \"order_id\"\n    )\n    val items: List\u003cOrderItem\u003e\n)\n\n// 3. Single query with JOIN\n@Dao\ninterface OrderDao {\n    @Transaction\n    @Query(\"SELECT * FROM orders WHERE user_id = :userId ORDER BY created_at DESC\")\n    fun getOrdersWithDetails(userId: String): List\u003cOrderWithDetails\u003e\n\n    // For pagination\n    @Transaction\n    @Query(\"SELECT * FROM orders WHERE user_id = :userId ORDER BY created_at DESC\")\n    fun getOrdersWithDetailsPaged(userId: String): PagingSource\u003cInt, OrderWithDetails\u003e\n}\n\n// Usage\nfun displayUserOrders(userId: String) {\n    val ordersWithDetails = orderDao.getOrdersWithDetails(userId) // Single query!\n\n    ordersWithDetails.forEach { orderDetail -\u003e\n        println(\"Order ${orderDetail.order.id} by ${orderDetail.user.name}: ${orderDetail.items.size} items\")\n    }\n}\n\n// For large datasets, use paging\nfun getOrdersPaged(userId: String): Flow\u003cPagingData\u003cOrderWithDetails\u003e\u003e {\n    return Pager(\n        config = PagingConfig(pageSize = 20, enablePlaceholders = false),\n        pagingSourceFactory = { orderDao.getOrdersWithDetailsPaged(userId) }\n    ).flow\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise 3: Optimize Network Calls",
                                "content":  "\nCreate an optimized image loading repository with caching and prefetching.\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Solution 3",
                                "content":  "\n\n---\n\n",
                                "code":  "class ImageRepository(\n    private val api: ImageApi,\n    private val diskCache: DiskLruCache,\n    private val memoryCache: LruCache\u003cString, Bitmap\u003e\n) {\n    private val scope = CoroutineScope(Dispatchers.IO + SupervisorJob())\n\n    // In-flight requests to avoid duplicates\n    private val loadingImages = mutableMapOf\u003cString, Deferred\u003cBitmap\u003e\u003e()\n\n    suspend fun loadImage(url: String): Bitmap? {\n        // 1. Check memory cache (fastest)\n        memoryCache.get(url)?.let { return it }\n\n        // 2. Check disk cache\n        diskCache.get(url)?.let { bytes -\u003e\n            val bitmap = BitmapFactory.decodeByteArray(bytes, 0, bytes.size)\n            memoryCache.put(url, bitmap)\n            return bitmap\n        }\n\n        // 3. Coalesce network requests\n        return loadingImages[url]?.await() ?: run {\n            val deferred = scope.async {\n                downloadAndCache(url)\n            }\n            loadingImages[url] = deferred\n\n            try {\n                deferred.await().also {\n                    loadingImages.remove(url)\n                }\n            } catch (e: Exception) {\n                loadingImages.remove(url)\n                null\n            }\n        }\n    }\n\n    private suspend fun downloadAndCache(url: String): Bitmap {\n        val bytes = api.downloadImage(url)\n        val bitmap = BitmapFactory.decodeByteArray(bytes, 0, bytes.size)\n\n        // Cache in memory\n        memoryCache.put(url, bitmap)\n\n        // Cache on disk\n        diskCache.put(url, bytes)\n\n        return bitmap\n    }\n\n    fun prefetch(urls: List\u003cString\u003e) {\n        scope.launch {\n            urls.forEach { url -\u003e\n                if (url !in memoryCache \u0026\u0026 url !in diskCache) {\n                    try {\n                        loadImage(url)\n                    } catch (e: Exception) {\n                        // Ignore prefetch errors\n                    }\n                }\n            }\n        }\n    }\n\n    fun clearCache() {\n        memoryCache.evictAll()\n        diskCache.delete()\n    }\n}\n\n// Usage\nclass ProductListViewModel(private val imageRepo: ImageRepository) : ViewModel() {\n    fun loadProducts(products: List\u003cProduct\u003e) {\n        // Prefetch images for visible products\n        val imageUrls = products.take(10).map { it.imageUrl }\n        imageRepo.prefetch(imageUrls)\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Why This Matters",
                                "content":  "\n### Real-World Impact\n\n**Performance Statistics**:\n- 53% of users abandon apps that take \u003e 3 seconds to load\n- 1-second delay = 7% reduction in conversions\n- Google ranks faster sites higher in search\n\n**Business Impact**:\n- **Amazon**: 100ms faster = 1% more revenue\n- **Pinterest**: 40% reduction in wait time = 15% more signups\n- **Shopify**: Faster stores convert 1.2x better\n\n**Career Impact**:\n- Performance optimization is a senior-level skill\n- Companies pay 20-30% more for engineers who can optimize\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Checkpoint Quiz",
                                "content":  "\n### Question 1\nWhat\u0027s the first step in performance optimization?\n\nA) Rewrite everything in C\nB) Profile to find bottlenecks\nC) Optimize all loops\nD) Remove all logging\n\n### Question 2\nWhich Dispatcher should you use for heavy calculations?\n\nA) Dispatchers.Main\nB) Dispatchers.IO\nC) Dispatchers.Default\nD) Dispatchers.Unconfined\n\n### Question 3\nHow do you prevent unnecessary Compose recompositions?\n\nA) Use var instead of mutableStateOf\nB) Use stable parameters and keys in LazyColumn\nC) Disable recomposition in settings\nD) Recomposition can\u0027t be prevented\n\n### Question 4\nWhat\u0027s the N+1 query problem?\n\nA) A query that returns N+1 rows\nB) Making N additional queries in a loop\nC) A query with N+1 joins\nD) A query error code\n\n### Question 5\nWhat does `derivedStateOf` do?\n\nA) Creates a new state\nB) Only recalculates when dependencies change\nC) Derives state from database\nD) Deletes old state\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Quiz Answers",
                                "content":  "\n**Question 1: B) Profile to find bottlenecks**\n\nAlways measure first:\n1. Profile with Android Studio Profiler\n2. Find the actual bottleneck\n3. Optimize that specific code\n4. Measure again to verify\n\n90% of time is in 10% of code - find that 10%!\n\n---\n\n**Question 2: C) Dispatchers.Default**\n\n\n---\n\n**Question 3: B) Use stable parameters and keys in LazyColumn**\n\n\n---\n\n**Question 4: B) Making N additional queries in a loop**\n\n\n---\n\n**Question 5: B) Only recalculates when dependencies change**\n\n\n---\n\n",
                                "code":  "val filteredItems by remember {\n    derivedStateOf {\n        items.filter { it.price \u003e 100 }\n    }\n}\n// Only recalculates when \u0027items\u0027 changes",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "What You\u0027ve Learned",
                                "content":  "\n✅ The golden rule: measure first, optimize second\n✅ Using Android Studio Profiler to find bottlenecks\n✅ Memory leak detection and prevention\n✅ Coroutine performance optimization (dispatchers, flow)\n✅ Jetpack Compose recomposition optimization\n✅ Database optimization (indexing, joins, paging)\n✅ Network optimization (caching, compression, prefetching)\n✅ Practical optimization exercises\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Next Steps",
                                "content":  "\nIn **Lesson 7.4: Security Best Practices**, you\u0027ll learn:\n- Secure coding practices\n- Input validation and sanitization\n- Encryption and hashing\n- API security (OAuth 2.0, JWT best practices)\n- Android security (KeyStore, ProGuard)\n- Common vulnerabilities (OWASP Top 10)\n\nFast apps are great, but secure apps are essential!\n\n---\n\n"
                            }
                        ],
    "challenges":  [

                   ],
    "difficulty":  "advanced",
    "title":  "Lesson 7.3: Performance Optimization",
    "estimatedMinutes":  85
}

## Review Instructions

Perform the following analysis:

### 1. Accuracy Check
- Verify all code examples are syntactically correct
- Confirm technical explanations match current kotlin documentation
- Search the web for the latest kotlin version and verify examples work with it
- Flag any deprecated APIs, syntax, or patterns

### 2. Completeness Check
- Does the lesson cover all concepts needed for a beginner to understand this topic?
- Are there missing explanations between concepts that would confuse a learner?
- Does the lesson have:
  - [ ] A clear analogy or real-world example (ANALOGY section)
  - [ ] Theoretical explanation (THEORY section)
  - [ ] Working code example (EXAMPLE section)
  - [ ] Common mistakes to avoid (WARNING section)
  - [ ] At least one practice challenge

### 3. Freshness Check
- Search for "kotlin Lesson 7.3: Performance Optimization 2024 2025" to find latest practices
- Compare lesson content against current best practices
- Identify any outdated patterns or recommendations

### 4. Pedagogical Gap Analysis
- What prerequisite knowledge is assumed but not explained?
- What follow-up questions would a learner likely have?
- What practical applications or use cases are missing?
- Are the challenges appropriately scaffolded for the difficulty level?

## Output Format

Provide your review as structured JSON:

```json
{
  "lessonId": "7.3",
  "reviewDate": "YYYY-MM-DD",
  "overallScore": 1-10,
  "accuracy": {
    "score": 1-10,
    "issues": ["issue 1", "issue 2"],
    "recommendations": ["fix 1", "fix 2"]
  },
  "completeness": {
    "score": 1-10,
    "missingSections": ["section type needed"],
    "gaps": ["gap 1", "gap 2"],
    "recommendations": ["add X", "expand Y"]
  },
  "freshness": {
    "score": 1-10,
    "outdatedItems": ["item 1"],
    "currentVersion": "language version checked",
    "recommendations": ["update X to Y"]
  },
  "pedagogicalGaps": {
    "score": 1-10,
    "missingPrerequisites": ["concept 1"],
    "unansweredQuestions": ["question learner would have"],
    "missingUseCases": ["practical application"],
    "recommendations": ["add section on X"]
  },
  "contentLengthIssues": {
    "shortSections": [
      {"sectionTitle": "title", "currentLength": 42, "recommendation": "expand to explain X"}
    ]
  },
  "suggestedNewContent": [
    {
      "sectionType": "THEORY|EXAMPLE|WARNING|etc",
      "title": "suggested title",
      "contentOutline": "what this section should cover"
    }
  ],
  "priority": "HIGH|MEDIUM|LOW"
}
```

