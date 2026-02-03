---
type: "EXAMPLE"
title: "Complete KMP Flow Example"
---

A complete cross-platform flow setup:

```kotlin
// ========== commonMain ==========

// data/User.kt
data class User(val id: String, val name: String, val email: String)

// data/UserRepository.kt
class UserRepository {
    private val _currentUser = MutableStateFlow<User?>(null)
    val currentUser: StateFlow<User?> = _currentUser.asStateFlow()
    
    suspend fun login(email: String, password: String): Result<User> {
        return runCatching {
            delay(1000) // Simulate network
            val user = User("1", "John Doe", email)
            _currentUser.value = user
            user
        }
    }
    
    fun logout() {
        _currentUser.value = null
    }
}

// presentation/AuthViewModel.kt
sealed class AuthState {
    data object NotAuthenticated : AuthState()
    data object Loading : AuthState()
    data class Authenticated(val user: User) : AuthState()
    data class Error(val message: String) : AuthState()
}

class AuthViewModel(private val repository: UserRepository) {
    private val scope = CoroutineScope(Dispatchers.Main + SupervisorJob())
    
    private val _authState = MutableStateFlow<AuthState>(AuthState.NotAuthenticated)
    val authState: StateFlow<AuthState> = _authState.asStateFlow()
    
    init {
        // Observe repository user changes
        scope.launch {
            repository.currentUser.collect { user ->
                _authState.value = user?.let { 
                    AuthState.Authenticated(it) 
                } ?: AuthState.NotAuthenticated
            }
        }
    }
    
    fun login(email: String, password: String) {
        scope.launch {
            _authState.value = AuthState.Loading
            repository.login(email, password)
                .onSuccess { /* State updated by repository observer */ }
                .onFailure { e -> 
                    _authState.value = AuthState.Error(e.message ?: "Login failed")
                }
        }
    }
    
    fun logout() = repository.logout()
    
    fun onCleared() = scope.cancel()
}

// ========== Usage Examples ==========

// Android (Compose)
@Composable
fun AuthScreen(viewModel: AuthViewModel) {
    val state by viewModel.authState.collectAsStateWithLifecycle()
    
    when (val s = state) {
        AuthState.NotAuthenticated -> LoginForm(onLogin = viewModel::login)
        AuthState.Loading -> CircularProgressIndicator()
        is AuthState.Authenticated -> ProfileScreen(s.user)
        is AuthState.Error -> ErrorWithRetry(s.message, onRetry = { })
    }
}

// iOS (with SKIE)
// struct AuthView: View {
//     @StateFlow var state: AuthState
//     let viewModel: AuthViewModel
//     
//     var body: some View {
//         switch state {
//         case is AuthState.NotAuthenticated: LoginForm()
//         case is AuthState.Loading: ProgressView()
//         case let auth as AuthState.Authenticated: ProfileView(user: auth.user)
//         case let error as AuthState.Error: ErrorView(message: error.message)
//         }
//     }
// }
```
