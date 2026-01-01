// Convert this DSL module to annotations:

val appModule = module {
    single { HttpClient() }
    single<UserApi> { UserApiImpl(get()) }
    single<UserRepository> { UserRepositoryImpl(get()) }
    factory { GetUserUseCase(get()) }
    factory { UpdateUserUseCase(get()) }
    viewModel { UserProfileViewModel(get(), get()) }
}

// Classes to annotate:
class HttpClient()

interface UserApi {
    suspend fun getUser(id: String): User
}

class UserApiImpl(private val client: HttpClient) : UserApi {
    override suspend fun getUser(id: String): User = TODO()
}

interface UserRepository {
    suspend fun getUser(id: String): User
}

class UserRepositoryImpl(
    private val api: UserApi
) : UserRepository {
    override suspend fun getUser(id: String) = api.getUser(id)
}

class GetUserUseCase(private val repository: UserRepository) {
    suspend operator fun invoke(id: String) = repository.getUser(id)
}

class UpdateUserUseCase(private val repository: UserRepository) {
    suspend operator fun invoke(user: User) { TODO() }
}

class UserProfileViewModel(
    private val getUser: GetUserUseCase,
    private val updateUser: UpdateUserUseCase
)

// TODO: Add appropriate annotations to each class