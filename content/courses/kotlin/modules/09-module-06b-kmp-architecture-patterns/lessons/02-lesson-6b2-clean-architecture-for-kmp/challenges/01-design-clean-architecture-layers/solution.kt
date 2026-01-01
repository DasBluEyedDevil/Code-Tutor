// ========== DOMAIN LAYER ==========

// domain/model/Task.kt
data class Task(
    val id: String,
    val title: String,
    val description: String,
    val dueDate: Long?,
    val isCompleted: Boolean,
    val createdAt: Long
) {
    fun isOverdue(): Boolean {
        val now = Clock.System.now().toEpochMilliseconds()
        return dueDate != null && dueDate < now && !isCompleted
    }
}

enum class TaskFilter { ALL, PENDING, COMPLETED }

// domain/repository/TaskRepository.kt
interface TaskRepository {
    fun observeTasks(filter: TaskFilter): Flow<List<Task>>
    suspend fun getTaskById(id: String): Task?
    suspend fun saveTask(task: Task)
    suspend fun toggleComplete(taskId: String)
    suspend fun deleteTask(taskId: String)
    suspend fun syncWithRemote(): Result<Unit>
}

// domain/usecase/SyncTasksUseCase.kt
// Use case needed - combines sync logic with error handling
class SyncTasksUseCase(
    private val taskRepository: TaskRepository,
    private val connectivityChecker: ConnectivityChecker
) {
    suspend operator fun invoke(): SyncResult {
        if (!connectivityChecker.isOnline()) {
            return SyncResult.Offline
        }
        return taskRepository.syncWithRemote().fold(
            onSuccess = { SyncResult.Success },
            onFailure = { SyncResult.Error(it.message) }
        )
    }
}

// ========== DATA LAYER ==========

// data/remote/TaskDto.kt
@Serializable
data class TaskDto(
    val id: String,
    val title: String,
    val description: String,
    @SerialName("due_date") val dueDate: Long?,
    @SerialName("is_completed") val isCompleted: Boolean,
    @SerialName("created_at") val createdAt: Long
)

// data/remote/TaskApi.kt
interface TaskApi {
    suspend fun getTasks(): List<TaskDto>
    suspend fun createTask(task: TaskDto): TaskDto
    suspend fun updateTask(task: TaskDto): TaskDto
    suspend fun deleteTask(id: String)
}

// data/local/TaskDao.kt
class TaskDao(private val db: AppDatabase) {
    fun observeAll(): Flow<List<TaskEntity>> =
        db.taskQueries.getAllTasks().asFlow().mapToList(Dispatchers.Default)
    
    fun observeByFilter(completed: Boolean): Flow<List<TaskEntity>> =
        db.taskQueries.getTasksByStatus(if (completed) 1 else 0)
            .asFlow().mapToList(Dispatchers.Default)
    
    suspend fun upsert(task: TaskEntity) = db.taskQueries.upsertTask(...)
    suspend fun delete(id: String) = db.taskQueries.deleteTask(id)
}

// data/repository/TaskRepositoryImpl.kt
class TaskRepositoryImpl(
    private val taskDao: TaskDao,
    private val taskApi: TaskApi
) : TaskRepository {
    
    override fun observeTasks(filter: TaskFilter): Flow<List<Task>> {
        return when (filter) {
            TaskFilter.ALL -> taskDao.observeAll()
            TaskFilter.PENDING -> taskDao.observeByFilter(false)
            TaskFilter.COMPLETED -> taskDao.observeByFilter(true)
        }.map { entities -> entities.map { it.toDomain() } }
    }
    
    override suspend fun syncWithRemote(): Result<Unit> = runCatching {
        val remoteTasks = taskApi.getTasks()
        remoteTasks.forEach { dto ->
            taskDao.upsert(dto.toEntity())
        }
    }
}