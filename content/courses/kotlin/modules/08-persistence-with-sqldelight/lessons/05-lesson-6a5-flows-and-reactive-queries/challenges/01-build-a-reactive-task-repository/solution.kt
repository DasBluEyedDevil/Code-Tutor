import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.combine
import kotlinx.coroutines.flow.map
import kotlinx.coroutines.Dispatchers
import app.cash.sqldelight.coroutines.asFlow
import app.cash.sqldelight.coroutines.mapToList
import app.cash.sqldelight.coroutines.mapToOne

class TaskRepository(private val database: AppDatabase) {
    private val queries = database.taskQueries
    
    fun observeAllTasks(): Flow<List<Task>> {
        return queries.getAllTasks()
            .asFlow()
            .mapToList(Dispatchers.IO)
    }
    
    fun observeIncompleteTasks(): Flow<List<Task>> {
        return queries.getIncompleteTasks()
            .asFlow()
            .mapToList(Dispatchers.IO)
    }
    
    fun observeTaskCount(): Flow<Long> {
        return queries.countAllTasks()
            .asFlow()
            .mapToOne(Dispatchers.IO)
    }
    
    fun observeCompletionRate(): Flow<Float> {
        val totalFlow = queries.countAllTasks().asFlow().mapToOne(Dispatchers.IO)
        val completedFlow = queries.countCompletedTasks().asFlow().mapToOne(Dispatchers.IO)
        
        return totalFlow.combine(completedFlow) { total, completed ->
            if (total == 0L) 0f else completed.toFloat() / total.toFloat()
        }
    }
}