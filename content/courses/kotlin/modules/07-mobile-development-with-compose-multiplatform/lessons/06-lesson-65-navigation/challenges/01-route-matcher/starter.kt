data class RouteMatch(val screen: String, val params: Map<String, String>)

class Router {
    private val routes = mutableListOf<Pair<String, String>>()
    
    fun register(pattern: String, screen: String) {
        routes.add(pattern to screen)
    }
    
    fun match(path: String): RouteMatch {
        // Match path against registered patterns
        // Extract path parameters from {param} segments
        TODO("Implement route matching")
    }
}

fun main() {
    val router = Router()
    router.register("/home", "Home")
    router.register("/users/{id}", "UserDetail")
    
    listOf("/home", "/users/42", "/unknown").forEach { path ->
        val result = router.match(path)
        println("Screen: ${result.screen}, Params: ${result.params}")
    }
}
