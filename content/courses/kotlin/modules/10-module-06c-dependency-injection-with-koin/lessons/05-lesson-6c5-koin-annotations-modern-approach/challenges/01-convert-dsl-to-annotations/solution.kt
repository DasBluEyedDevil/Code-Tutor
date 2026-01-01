import org.koin.core.annotation.*

// Module declaration
@Module
@ComponentScan("com.example.app.user")
class UserModule

// Network
@Single
class HttpClient()

// API layer
interface UserApi {
    suspend fun getUser(id: String): User
}

@Single
@Bind(UserApi::class)
class UserApiImpl(
    private val client: HttpClient
) : UserApi {
    override suspend fun getUser(id: String): User = TODO()
}

// Repository layer
interface UserRepository {
    suspend fun getUser(id: String): User
}

@Single
@Bind(UserRepository::class)
class UserRepositoryImpl(
    private val api: UserApi
) : UserRepository {
    override suspend fun getUser(id: String) = api.getUser(id)
}

// Use cases (factory - new instance each time)
@Factory
class GetUserUseCase(
    private val repository: UserRepository
) {
    suspend operator fun invoke(id: String) = repository.getUser(id)
}

@Factory
class UpdateUserUseCase(
    private val repository: UserRepository
) {
    suspend operator fun invoke(user: User) { TODO() }
}

// ViewModel
@KoinViewModel
class UserProfileViewModel(
    private val getUser: GetUserUseCase,
    private val updateUser: UpdateUserUseCase
)

// Usage in startKoin:
// modules(UserModuleModule().module)