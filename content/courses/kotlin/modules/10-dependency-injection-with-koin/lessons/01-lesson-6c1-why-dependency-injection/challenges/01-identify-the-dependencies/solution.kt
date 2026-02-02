// Dependencies that should be injected:
// 1. UserDao (from database)
// 2. UserApi (network endpoint)
// 3. Cache<String, User>
// 4. Logger

// Refactored with dependency injection:

interface UserCache {
    fun get(key: String): User?
    fun put(key: String, user: User)
}

class UserProfileManager(
    private val userDao: UserDao,           // Injected
    private val userApi: UserApi,           // Injected
    private val cache: UserCache,           // Injected
    private val logger: Logger              // Injected
) {
    suspend fun getUser(userId: String): User {
        // Check cache first
        cache.get(userId)?.let { return it }
        
        // Try database
        val localUser = userDao.findById(userId)
        if (localUser != null) {
            cache.put(userId, localUser)
            return localUser
        }
        
        // Fetch from network
        logger.info("Fetching user $userId from network")
        val remoteUser = userApi.getUser(userId)
        userDao.insert(remoteUser)
        cache.put(userId, remoteUser)
        return remoteUser
    }
}

// Benefits:
// - Each dependency is an interface (can be mocked)
// - No hidden global state
// - Constructor clearly shows requirements
// - Easy to test with fakes:

class UserProfileManagerTest {
    @Test
    fun `getUser returns cached user`() = runTest {
        val fakeCache = FakeUserCache()
        fakeCache.put("123", User("123", "John"))
        
        val manager = UserProfileManager(
            userDao = FakeUserDao(),
            userApi = FakeUserApi(),
            cache = fakeCache,
            logger = NoOpLogger()
        )
        
        val user = manager.getUser("123")
        assertEquals("John", user.name)
    }
}