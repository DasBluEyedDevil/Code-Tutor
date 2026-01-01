---
type: "THEORY"
title: "The Fake Pattern (Recommended)"
---

Fakes are test implementations that behave like the real thing but are simpler and controllable.

### Principles of Good Fakes

1. **Implement the same interface** as production code
2. **Store data in memory** instead of real storage
3. **Provide control methods** to set up test scenarios
4. **Are reusable** across many tests

```kotlin
// Interface (shared by real and fake)
interface UserRepository {
    suspend fun getUser(id: String): User?
    suspend fun saveUser(user: User)
    suspend fun deleteUser(id: String)
    fun observeUser(id: String): Flow<User?>
}

// Fake implementation
class FakeUserRepository : UserRepository {
    private val users = mutableMapOf<String, User>()
    private val userFlows = mutableMapOf<String, MutableStateFlow<User?>>()
    
    // Test control properties
    var simulateNetworkError = false
    var simulateDelay: Long = 0
    
    override suspend fun getUser(id: String): User? {
        if (simulateNetworkError) throw IOException("Network error")
        if (simulateDelay > 0) delay(simulateDelay)
        return users[id]
    }
    
    override suspend fun saveUser(user: User) {
        if (simulateNetworkError) throw IOException("Network error")
        users[user.id] = user
        userFlows[user.id]?.value = user
    }
    
    override suspend fun deleteUser(id: String) {
        users.remove(id)
        userFlows[id]?.value = null
    }
    
    override fun observeUser(id: String): Flow<User?> {
        return userFlows.getOrPut(id) {
            MutableStateFlow(users[id])
        }
    }
    
    // Test helper methods
    fun addUser(user: User) {
        users[user.id] = user
        userFlows[user.id]?.value = user
    }
    
    fun clear() {
        users.clear()
        userFlows.clear()
    }
}
```