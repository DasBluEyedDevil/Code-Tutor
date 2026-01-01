// Review this ViewModel and identify architecture problems:

class UserProfileViewModel : ViewModel() {
    val context = App.instance.applicationContext
    val database = Room.databaseBuilder(context, AppDb::class.java, "app.db").build()
    val api = Retrofit.Builder().baseUrl("https://api.example.com").build()
    
    var userName by mutableStateOf("")
    var userEmail by mutableStateOf("")
    var posts = mutableStateListOf<Post>()
    var isLoading by mutableStateOf(false)
    var errorMessage by mutableStateOf<String?>(null)
    
    fun loadUserProfile(userId: String) {
        isLoading = true
        GlobalScope.launch {
            try {
                val response = api.create(UserApi::class.java).getUser(userId)
                if (response.isSuccessful) {
                    val user = response.body()!!
                    userName = user.name
                    userEmail = user.email
                    database.userDao().insert(user)
                    
                    val postsResponse = api.create(PostsApi::class.java).getPosts(userId)
                    posts.clear()
                    posts.addAll(postsResponse.body()!!)
                }
            } catch (e: Exception) {
                errorMessage = e.message
            }
            isLoading = false
        }
    }
}

// List at least 5 architecture problems: