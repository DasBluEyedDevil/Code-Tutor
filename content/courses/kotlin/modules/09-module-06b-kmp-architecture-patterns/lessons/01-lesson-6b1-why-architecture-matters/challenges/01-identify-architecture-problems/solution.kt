// Architecture Problems Identified:

// 1. DIRECT ANDROID DEPENDENCIES IN VIEWMODEL
// - Context, Room, Retrofit are created directly
// - Cannot share this ViewModel with iOS
// - Violates Dependency Inversion principle

// 2. GLOBALSCOPE USAGE
// - Memory leak risk - coroutine outlives ViewModel
// - Should use viewModelScope

// 3. NO REPOSITORY LAYER
// - ViewModel directly calls API and database
// - Business logic mixed with data access
// - No abstraction for testing

// 4. MUTABLE STATE SCATTERED
// - Multiple mutable state properties
// - No single source of truth
// - Hard to track state changes

// 5. FORCE UNWRAPPING (!!)
// - response.body()!! can crash
// - No null safety consideration

// 6. MULTIPLE RESPONSIBILITIES
// - Loading user AND posts in same function
// - Should be separate operations

// 7. ERROR HANDLING LEAKS IMPLEMENTATION
// - Raw exception.message shown to user
// - Should map to user-friendly errors

// 8. HARDCODED DEPENDENCIES
// - Database name, API URL in ViewModel
// - Should be injected or configured

// CORRECTED VERSION:
class UserProfileViewModel(
    private val userRepository: UserRepository,
    private val postRepository: PostRepository
) : ViewModel() {
    
    private val _state = MutableStateFlow(ProfileState())
    val state: StateFlow<ProfileState> = _state.asStateFlow()
    
    fun loadProfile(userId: String) {
        viewModelScope.launch {
            _state.update { it.copy(isLoading = true, error = null) }
            userRepository.getUser(userId)
                .onSuccess { user ->
                    _state.update { it.copy(user = user, isLoading = false) }
                }
                .onFailure { error ->
                    _state.update { it.copy(error = error.toUserMessage(), isLoading = false) }
                }
        }
    }
}

data class ProfileState(
    val user: User? = null,
    val isLoading: Boolean = false,
    val error: String? = null
)