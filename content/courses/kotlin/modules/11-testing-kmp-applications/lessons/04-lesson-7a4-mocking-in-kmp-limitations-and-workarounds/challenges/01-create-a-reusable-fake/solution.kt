class FakeAuthRepository : AuthRepository {
    // Test control properties
    var simulateLoginError: Exception? = null
    var loginDelay: Long = 0
    var registeredUsers = mutableMapOf<String, Pair<String, User>>()  // email -> (password, user)
    
    // Internal state
    private var currentUser: User? = null
    private val _authState = MutableStateFlow<AuthState>(AuthState.LoggedOut)
    
    override suspend fun login(email: String, password: String): Result<User> {
        _authState.value = AuthState.Loading
        
        if (loginDelay > 0) delay(loginDelay)
        
        simulateLoginError?.let { error ->
            _authState.value = AuthState.LoggedOut
            return Result.failure(error)
        }
        
        val credentials = registeredUsers[email]
        if (credentials == null || credentials.first != password) {
            _authState.value = AuthState.LoggedOut
            return Result.failure(IllegalArgumentException("Invalid credentials"))
        }
        
        currentUser = credentials.second
        _authState.value = AuthState.LoggedIn(credentials.second)
        return Result.success(credentials.second)
    }
    
    override suspend fun logout() {
        currentUser = null
        _authState.value = AuthState.LoggedOut
    }
    
    override suspend fun getCurrentUser(): User? = currentUser
    
    override fun observeAuthState(): Flow<AuthState> = _authState.asStateFlow()
    
    // Test helper methods
    fun registerUser(email: String, password: String, user: User) {
        registeredUsers[email] = password to user
    }
    
    fun setLoggedInUser(user: User) {
        currentUser = user
        _authState.value = AuthState.LoggedIn(user)
    }
    
    fun clear() {
        currentUser = null
        registeredUsers.clear()
        simulateLoginError = null
        loginDelay = 0
        _authState.value = AuthState.LoggedOut
    }
}

// Example usage in tests
class LoginViewModelTest {
    private lateinit var fakeAuth: FakeAuthRepository
    
    @BeforeTest
    fun setup() {
        fakeAuth = FakeAuthRepository()
        fakeAuth.registerUser(
            "test@example.com",
            "password123",
            User("1", "test@example.com", "Test User")
        )
    }
    
    @Test
    fun `successful login updates auth state`() = runTest {
        val viewModel = LoginViewModel(fakeAuth)
        
        fakeAuth.observeAuthState().test {
            assertEquals(AuthState.LoggedOut, awaitItem())
            
            viewModel.login("test@example.com", "password123")
            
            assertEquals(AuthState.Loading, awaitItem())
            
            val loggedIn = awaitItem()
            assertTrue(loggedIn is AuthState.LoggedIn)
            assertEquals("Test User", (loggedIn as AuthState.LoggedIn).user.name)
            
            cancelAndIgnoreRemainingEvents()
        }
    }
}