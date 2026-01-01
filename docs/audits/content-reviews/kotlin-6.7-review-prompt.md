# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Kotlin Multiplatform Complete Course (kotlin)
- **Module:** Mobile Development with Compose Multiplatform
- **Lesson:** Lesson 6.7: Local Data Storage (ID: 6.7)
- **Difficulty:** advanced
- **Estimated Time:** 75 minutes

## Current Lesson Content

{
    "id":  "6.7",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "**Estimated Time**: 75 minutes\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "\nApps need to store data locally for offline access, caching, and user preferences. For Compose Multiplatform, we use cross-platform solutions that work on both Android and iOS.\n\n**Cross-Platform Options:**\n- **SQLDelight**: Cross-platform database (recommended for multiplatform)\n- **Room**: Android-only (great for Android-specific projects)\n- **Multiplatform Settings**: Cross-platform key-value storage\n\nIn this lesson, you\u0027ll master:\n- ✅ Room/SQLDelight database setup and configuration\n- ✅ Entity definitions with relationships\n- ✅ DAOs (Data Access Objects) for queries\n- ✅ Repository pattern with databases\n- ✅ Flows for reactive data updates\n- ✅ DataStore/Settings for preferences\n- ✅ **iOS storage APIs (UserDefaults equivalent)**\n- ✅ Combining local and remote data\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Setup Dependencies",
                                "content":  "\nAdd in `build.gradle.kts`:\n\n\nIn `gradle/libs.versions.toml`:\n\n\n---\n\n",
                                "code":  "[versions]\nroom = \"2.6.1\"\nksp = \"2.0.21-1.0.27\"\ndatastore = \"1.1.1\"\n\n[libraries]\nandroidx-room-runtime = { group = \"androidx.room\", name = \"room-runtime\", version.ref = \"room\" }\nandroidx-room-ktx = { group = \"androidx.room\", name = \"room-ktx\", version.ref = \"room\" }\nandroidx-room-compiler = { group = \"androidx.room\", name = \"room-compiler\", version.ref = \"room\" }\nandroidx-datastore-preferences = { group = \"androidx.datastore\", name = \"datastore-preferences\", version.ref = \"datastore\" }\n\n[plugins]\nksp = { id = \"com.google.devtools.ksp\", version.ref = \"ksp\" }",
                                "language":  "toml"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Room Database",
                                "content":  "\n### Entity (Table)\n\n\n### Type Converters\n\n\n### DAO (Data Access Object)\n\n\n### Database\n\n\n---\n\n",
                                "code":  "import android.content.Context\nimport androidx.room.Database\nimport androidx.room.Room\nimport androidx.room.RoomDatabase\nimport androidx.room.TypeConverters\n\n@Database(\n    entities = [Task::class],\n    version = 1,\n    exportSchema = false\n)\n@TypeConverters(Converters::class)\nabstract class AppDatabase : RoomDatabase() {\n    abstract fun taskDao(): TaskDao\n\n    companion object {\n        @Volatile\n        private var INSTANCE: AppDatabase? = null\n\n        fun getDatabase(context: Context): AppDatabase {\n            return INSTANCE ?: synchronized(this) {\n                val instance = Room.databaseBuilder(\n                    context.applicationContext,\n                    AppDatabase::class.java,\n                    \"app_database\"\n                )\n                    .fallbackToDestructiveMigration()  // For development only!\n                    .build()\n\n                INSTANCE = instance\n                instance\n            }\n        }\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Repository with Room",
                                "content":  "\n\n---\n\n",
                                "code":  "class TaskRepository(private val taskDao: TaskDao) {\n    val allTasks: Flow\u003cList\u003cTask\u003e\u003e = taskDao.getAllTasks()\n\n    fun getTask(taskId: Int): Flow\u003cTask?\u003e = taskDao.getTask(taskId)\n\n    fun getActiveTasks(): Flow\u003cList\u003cTask\u003e\u003e = taskDao.getTasksByStatus(false)\n\n    fun getCompletedTasks(): Flow\u003cList\u003cTask\u003e\u003e = taskDao.getTasksByStatus(true)\n\n    suspend fun insertTask(task: Task): Long {\n        return taskDao.insertTask(task)\n    }\n\n    suspend fun updateTask(task: Task) {\n        taskDao.updateTask(task)\n    }\n\n    suspend fun deleteTask(task: Task) {\n        taskDao.deleteTask(task)\n    }\n\n    suspend fun toggleTaskStatus(taskId: Int, isCompleted: Boolean) {\n        taskDao.updateTaskStatus(taskId, isCompleted)\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "ViewModel with Room",
                                "content":  "\n\n---\n\n",
                                "code":  "import androidx.lifecycle.ViewModel\nimport androidx.lifecycle.viewModelScope\nimport kotlinx.coroutines.flow.SharingStarted\nimport kotlinx.coroutines.flow.StateFlow\nimport kotlinx.coroutines.flow.stateIn\nimport kotlinx.coroutines.launch\n\nclass TasksViewModel(\n    private val repository: TaskRepository\n) : ViewModel() {\n\n    val allTasks: StateFlow\u003cList\u003cTask\u003e\u003e = repository.allTasks\n        .stateIn(\n            scope = viewModelScope,\n            started = SharingStarted.WhileSubscribed(5000),\n            initialValue = emptyList()\n        )\n\n    fun addTask(title: String, description: String, priority: Priority) {\n        viewModelScope.launch {\n            val task = Task(\n                title = title,\n                description = description,\n                priority = priority\n            )\n            repository.insertTask(task)\n        }\n    }\n\n    fun toggleTask(task: Task) {\n        viewModelScope.launch {\n            repository.updateTask(task.copy(isCompleted = !task.isCompleted))\n        }\n    }\n\n    fun deleteTask(task: Task) {\n        viewModelScope.launch {\n            repository.deleteTask(task)\n        }\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "UI with Room Data",
                                "content":  "\n\n---\n\n",
                                "code":  "@Composable\nfun TasksScreen(viewModel: TasksViewModel) {\n    val tasks by viewModel.allTasks.collectAsState()\n    var showDialog by remember { mutableStateOf(false) }\n\n    Scaffold(\n        floatingActionButton = {\n            FloatingActionButton(onClick = { showDialog = true }) {\n                Icon(Icons.Default.Add, contentDescription = \"Add task\")\n            }\n        }\n    ) { innerPadding -\u003e\n        if (tasks.isEmpty()) {\n            Box(\n                modifier = Modifier\n                    .fillMaxSize()\n                    .padding(innerPadding),\n                contentAlignment = Alignment.Center\n            ) {\n                Text(\"No tasks yet. Add one!\")\n            }\n        } else {\n            LazyColumn(\n                modifier = Modifier.padding(innerPadding),\n                contentPadding = PaddingValues(16.dp),\n                verticalArrangement = Arrangement.spacedBy(8.dp)\n            ) {\n                items(tasks, key = { it.id }) { task -\u003e\n                    TaskItem(\n                        task = task,\n                        onToggle = { viewModel.toggleTask(task) },\n                        onDelete = { viewModel.deleteTask(task) }\n                    )\n                }\n            }\n        }\n    }\n\n    if (showDialog) {\n        AddTaskDialog(\n            onDismiss = { showDialog = false },\n            onAdd = { title, description, priority -\u003e\n                viewModel.addTask(title, description, priority)\n                showDialog = false\n            }\n        )\n    }\n}\n\n@Composable\nfun TaskItem(\n    task: Task,\n    onToggle: () -\u003e Unit,\n    onDelete: () -\u003e Unit\n) {\n    Card(\n        modifier = Modifier.fillMaxWidth()\n    ) {\n        Row(\n            modifier = Modifier.padding(16.dp),\n            verticalAlignment = Alignment.CenterVertically\n        ) {\n            Checkbox(\n                checked = task.isCompleted,\n                onCheckedChange = { onToggle() }\n            )\n\n            Spacer(modifier = Modifier.width(8.dp))\n\n            Column(modifier = Modifier.weight(1f)) {\n                Text(\n                    task.title,\n                    style = MaterialTheme.typography.titleMedium,\n                    textDecoration = if (task.isCompleted) {\n                        TextDecoration.LineThrough\n                    } else null\n                )\n                Text(\n                    task.description,\n                    style = MaterialTheme.typography.bodySmall,\n                    color = MaterialTheme.colorScheme.onSurfaceVariant\n                )\n            }\n\n            IconButton(onClick = onDelete) {\n                Icon(Icons.Default.Delete, contentDescription = \"Delete\")\n            }\n        }\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Relationships",
                                "content":  "\n### One-to-Many\n\n\n---\n\n",
                                "code":  "@Entity(tableName = \"categories\")\ndata class Category(\n    @PrimaryKey(autoGenerate = true)\n    val id: Int = 0,\n    val name: String,\n    val color: String\n)\n\n@Entity(\n    tableName = \"tasks_with_category\",\n    foreignKeys = [\n        ForeignKey(\n            entity = Category::class,\n            parentColumns = [\"id\"],\n            childColumns = [\"categoryId\"],\n            onDelete = ForeignKey.CASCADE\n        )\n    ]\n)\ndata class TaskWithCategory(\n    @PrimaryKey(autoGenerate = true)\n    val id: Int = 0,\n    val title: String,\n    val categoryId: Int\n)\n\n// Query with relationship\ndata class CategoryWithTasks(\n    @Embedded val category: Category,\n    @Relation(\n        parentColumn = \"id\",\n        entityColumn = \"categoryId\"\n    )\n    val tasks: List\u003cTaskWithCategory\u003e\n)\n\n@Dao\ninterface CategoryDao {\n    @Transaction\n    @Query(\"SELECT * FROM categories\")\n    fun getCategoriesWithTasks(): Flow\u003cList\u003cCategoryWithTasks\u003e\u003e\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "DataStore for Preferences",
                                "content":  "\n\n---\n\n",
                                "code":  "import android.content.Context\nimport androidx.datastore.core.DataStore\nimport androidx.datastore.preferences.core.*\nimport androidx.datastore.preferences.preferencesDataStore\nimport kotlinx.coroutines.flow.Flow\nimport kotlinx.coroutines.flow.map\n\nprivate val Context.dataStore: DataStore\u003cPreferences\u003e by preferencesDataStore(name = \"settings\")\n\nclass PreferencesRepository(private val context: Context) {\n\n    private object PreferencesKeys {\n        val THEME_KEY = stringPreferencesKey(\"theme\")\n        val NOTIFICATIONS_ENABLED = booleanPreferencesKey(\"notifications_enabled\")\n        val USERNAME = stringPreferencesKey(\"username\")\n    }\n\n    val theme: Flow\u003cString\u003e = context.dataStore.data\n        .map { preferences -\u003e\n            preferences[PreferencesKeys.THEME_KEY] ?: \"system\"\n        }\n\n    val notificationsEnabled: Flow\u003cBoolean\u003e = context.dataStore.data\n        .map { preferences -\u003e\n            preferences[PreferencesKeys.NOTIFICATIONS_ENABLED] ?: true\n        }\n\n    suspend fun setTheme(theme: String) {\n        context.dataStore.edit { preferences -\u003e\n            preferences[PreferencesKeys.THEME_KEY] = theme\n        }\n    }\n\n    suspend fun setNotificationsEnabled(enabled: Boolean) {\n        context.dataStore.edit { preferences -\u003e\n            preferences[PreferencesKeys.NOTIFICATIONS_ENABLED] = enabled\n        }\n    }\n\n    suspend fun setUsername(username: String) {\n        context.dataStore.edit { preferences -\u003e\n            preferences[PreferencesKeys.USERNAME] = username\n        }\n    }\n\n    suspend fun clearPreferences() {\n        context.dataStore.edit { it.clear() }\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Migration",
                                "content":  "\n\n---\n\n",
                                "code":  "val MIGRATION_1_2 = object : Migration(1, 2) {\n    override fun migrate(database: SupportSQLiteDatabase) {\n        database.execSQL(\"ALTER TABLE tasks ADD COLUMN categoryId INTEGER\")\n    }\n}\n\nval database = Room.databaseBuilder(\n    context,\n    AppDatabase::class.java,\n    \"app_database\"\n)\n    .addMigrations(MIGRATION_1_2)\n    .build()",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise 1: Notes App with Room",
                                "content":  "\nCreate a notes app:\n- Add, edit, delete notes\n- Search notes\n- Persist to Room database\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Solution 1",
                                "content":  "\n\n---\n\n",
                                "code":  "@Entity(tableName = \"notes\")\ndata class Note(\n    @PrimaryKey(autoGenerate = true)\n    val id: Int = 0,\n    val title: String,\n    val content: String,\n    val timestamp: Long = System.currentTimeMillis()\n)\n\n@Dao\ninterface NoteDao {\n    @Query(\"SELECT * FROM notes ORDER BY timestamp DESC\")\n    fun getAllNotes(): Flow\u003cList\u003cNote\u003e\u003e\n\n    @Query(\"SELECT * FROM notes WHERE title LIKE \u0027%\u0027 || :query || \u0027%\u0027 OR content LIKE \u0027%\u0027 || :query || \u0027%\u0027\")\n    fun searchNotes(query: String): Flow\u003cList\u003cNote\u003e\u003e\n\n    @Insert\n    suspend fun insertNote(note: Note)\n\n    @Update\n    suspend fun updateNote(note: Note)\n\n    @Delete\n    suspend fun deleteNote(note: Note)\n}\n\nclass NotesViewModel(private val noteDao: NoteDao) : ViewModel() {\n    val allNotes: StateFlow\u003cList\u003cNote\u003e\u003e = noteDao.getAllNotes()\n        .stateIn(viewModelScope, SharingStarted.WhileSubscribed(5000), emptyList())\n\n    fun addNote(title: String, content: String) {\n        viewModelScope.launch {\n            noteDao.insertNote(Note(title = title, content = content))\n        }\n    }\n\n    fun updateNote(note: Note) {\n        viewModelScope.launch {\n            noteDao.updateNote(note)\n        }\n    }\n\n    fun deleteNote(note: Note) {\n        viewModelScope.launch {\n            noteDao.deleteNote(note)\n        }\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise 2: Favorites with DataStore",
                                "content":  "\nImplement favorites functionality:\n- Save favorite item IDs\n- Load favorites on app start\n- Toggle favorite status\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Solution 2",
                                "content":  "\n\n---\n\n",
                                "code":  "import androidx.datastore.preferences.core.stringSetPreferencesKey\n\nclass FavoritesRepository(private val context: Context) {\n    private val FAVORITES_KEY = stringSetPreferencesKey(\"favorites\")\n\n    val favorites: Flow\u003cSet\u003cString\u003e\u003e = context.dataStore.data\n        .map { preferences -\u003e\n            preferences[FAVORITES_KEY] ?: emptySet()\n        }\n\n    suspend fun addFavorite(itemId: String) {\n        context.dataStore.edit { preferences -\u003e\n            val currentFavorites = preferences[FAVORITES_KEY]?.toMutableSet() ?: mutableSetOf()\n            currentFavorites.add(itemId)\n            preferences[FAVORITES_KEY] = currentFavorites\n        }\n    }\n\n    suspend fun removeFavorite(itemId: String) {\n        context.dataStore.edit { preferences -\u003e\n            val currentFavorites = preferences[FAVORITES_KEY]?.toMutableSet() ?: mutableSetOf()\n            currentFavorites.remove(itemId)\n            preferences[FAVORITES_KEY] = currentFavorites\n        }\n    }\n\n    suspend fun toggleFavorite(itemId: String) {\n        context.dataStore.edit { preferences -\u003e\n            val currentFavorites = preferences[FAVORITES_KEY]?.toMutableSet() ?: mutableSetOf()\n            if (currentFavorites.contains(itemId)) {\n                currentFavorites.remove(itemId)\n            } else {\n                currentFavorites.add(itemId)\n            }\n            preferences[FAVORITES_KEY] = currentFavorites\n        }\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise 3: Offline-First App",
                                "content":  "\nCombine Room + Retrofit:\n- Fetch data from API\n- Cache in Room\n- Show cached data while loading\n- Update cache when new data arrives\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Solution 3",
                                "content":  "\n\n---\n\n",
                                "code":  "class OfflineFirstRepository(\n    private val apiService: ApiService,\n    private val userDao: UserDao\n) {\n    fun getUsers(): Flow\u003cResult\u003cList\u003cUser\u003e\u003e\u003e = flow {\n        // Emit cached data first\n        emit(Result.Loading)\n\n        val cachedUsers = userDao.getAllUsers().first()\n        if (cachedUsers.isNotEmpty()) {\n            emit(Result.Success(cachedUsers))\n        }\n\n        // Fetch from network\n        try {\n            val remoteUsers = apiService.getUsers()\n\n            // Update cache\n            userDao.deleteAll()\n            userDao.insertAll(remoteUsers)\n\n            // Emit fresh data\n            emit(Result.Success(remoteUsers))\n        } catch (e: Exception) {\n            // If network fails and we have cache, keep showing cached data\n            if (cachedUsers.isNotEmpty()) {\n                emit(Result.Success(cachedUsers))\n            } else {\n                emit(Result.Error(e.message ?: \"Unknown error\"))\n            }\n        }\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Why This Matters",
                                "content":  "\n**Statistics**:\n- **80%** of app usage happens offline or on slow networks\n- Apps with local storage have **5x** better retention\n- Users expect instant data (not \"Loading...\")\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Checkpoint Quiz",
                                "content":  "\n### Question 1\nWhat is Room?\n\nA) Image loading library\nB) SQLite database wrapper with type safety\nC) Network library\nD) UI component\n\n### Question 2\nWhat does `Flow\u003cList\u003cTask\u003e\u003e` provide?\n\nA) One-time data fetch\nB) Reactive updates when database changes\nC) Faster queries\nD) Automatic caching\n\n### Question 3\nWhen should you use DataStore instead of Room?\n\nA) For large datasets\nB) For simple key-value preferences\nC) For complex queries\nD) For images\n\n### Question 4\nWhat does `@PrimaryKey(autoGenerate = true)` do?\n\nA) Makes field required\nB) Generates unique ID automatically\nC) Enables caching\nD) Creates index\n\n### Question 5\nWhat is an offline-first strategy?\n\nA) Never use network\nB) Show cached data immediately, update from network\nC) Only load data once\nD) Disable network features\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Quiz Answers",
                                "content":  "\n**Question 1: B** - Room is a type-safe SQLite wrapper\n**Question 2: B** - Flow provides reactive, automatic updates\n**Question 3: B** - DataStore for preferences, Room for structured data\n**Question 4: B** - Auto-generates incrementing IDs\n**Question 5: B** - Show cache first, then update from network\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "iOS Storage APIs",
                                "content":  "\n### Cross-Platform Storage with Multiplatform Settings\n\nFor Compose Multiplatform apps, use the `multiplatform-settings` library instead of DataStore:\n\n```kotlin\n// In commonMain - works on Android AND iOS!\nexpect class SettingsFactory {\n    fun create(): Settings\n}\n\nclass UserPreferences(private val settings: Settings) {\n    var isDarkMode: Boolean\n        get() = settings.getBoolean(\"dark_mode\", false)\n        set(value) = settings.putBoolean(\"dark_mode\", value)\n    \n    var username: String?\n        get() = settings.getStringOrNull(\"username\")\n        set(value) = if (value != null) {\n            settings.putString(\"username\", value)\n        } else {\n            settings.remove(\"username\")\n        }\n}\n```\n\n### Platform Implementations\n\n**Android** uses SharedPreferences:\n```kotlin\n// androidMain\nactual class SettingsFactory(private val context: Context) {\n    actual fun create(): Settings {\n        return SharedPreferencesSettings(\n            context.getSharedPreferences(\"app_prefs\", Context.MODE_PRIVATE)\n        )\n    }\n}\n```\n\n**iOS** uses UserDefaults:\n```kotlin\n// iosMain\nactual class SettingsFactory {\n    actual fun create(): Settings {\n        return NSUserDefaultsSettings(NSUserDefaults.standardUserDefaults)\n    }\n}\n```\n\n### Cross-Platform Database with SQLDelight\n\nFor structured data across platforms, use SQLDelight:\n\n| Feature | Room (Android) | SQLDelight (Multiplatform) |\n|---------|----------------|---------------------------|\n| Platform | Android only | Android, iOS, Desktop, Web |\n| Query Language | SQL annotations | .sq files |\n| Code Generation | Kotlin | Kotlin |\n| Flow Support | Yes | Yes |\n\n### Running on iOS\n\n1. Build and run on iOS Simulator\n2. Save some data using your storage APIs\n3. Force-close the app\n4. Reopen - verify data persisted!\n5. UserDefaults and SQLite work transparently on iOS\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "What You\u0027ve Learned",
                                "content":  "\n✅ Room/SQLDelight database setup and entities\n✅ DAOs for type-safe queries\n✅ Repository pattern with databases\n✅ Flows for reactive data\n✅ Entity relationships (one-to-many)\n✅ DataStore/Multiplatform Settings for preferences\n✅ Database migrations\n✅ Offline-first architecture\n✅ **iOS storage (UserDefaults via multiplatform-settings)**\n✅ **Cross-platform database with SQLDelight**\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Next Steps",
                                "content":  "\nIn **Lesson 6.8: MVVM Architecture**, you\u0027ll learn:\n- MVVM pattern in depth (works on both platforms!)\n- ViewModel lifecycle\n- LiveData vs StateFlow\n- Dependency injection with Koin (cross-platform)\n- Clean architecture layers\n- Testing ViewModels\n\nGet ready to structure professional apps for Android and iOS!\n\n"
                            }
                        ],
    "challenges":  [

                   ],
    "difficulty":  "advanced",
    "title":  "Lesson 6.7: Local Data Storage",
    "estimatedMinutes":  75
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
- Search for "kotlin Lesson 6.7: Local Data Storage 2024 2025" to find latest practices
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
  "lessonId": "6.7",
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

