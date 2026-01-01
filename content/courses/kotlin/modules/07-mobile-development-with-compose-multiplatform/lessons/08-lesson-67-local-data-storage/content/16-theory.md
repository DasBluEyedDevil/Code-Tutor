---
type: "THEORY"
title: "Solution 3"
---



---



```kotlin
class OfflineFirstRepository(
    private val apiService: ApiService,
    private val userDao: UserDao
) {
    fun getUsers(): Flow<Result<List<User>>> = flow {
        // Emit cached data first
        emit(Result.Loading)

        val cachedUsers = userDao.getAllUsers().first()
        if (cachedUsers.isNotEmpty()) {
            emit(Result.Success(cachedUsers))
        }

        // Fetch from network
        try {
            val remoteUsers = apiService.getUsers()

            // Update cache
            userDao.deleteAll()
            userDao.insertAll(remoteUsers)

            // Emit fresh data
            emit(Result.Success(remoteUsers))
        } catch (e: Exception) {
            // If network fails and we have cache, keep showing cached data
            if (cachedUsers.isNotEmpty()) {
                emit(Result.Success(cachedUsers))
            } else {
                emit(Result.Error(e.message ?: "Unknown error"))
            }
        }
    }
}
```
