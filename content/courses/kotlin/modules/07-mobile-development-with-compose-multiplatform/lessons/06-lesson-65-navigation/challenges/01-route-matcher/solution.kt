data class RouteMatch(val screen: String, val params: Map<String, String>)

class Router {
    private val routes = mutableListOf<Pair<String, String>>()
    
    fun register(pattern: String, screen: String) {
        routes.add(pattern to screen)
    }
    
    fun match(path: String): RouteMatch {
        for ((pattern, screen) in routes) {
            val patternParts = pattern.split("/")
            val pathParts = path.split("/")
            if (patternParts.size != pathParts.size) continue
            val params = mutableMapOf<String, String>()
            var matched = true
            for (i in patternParts.indices) {
                when {
                    patternParts[i].startsWith("{") -> {
                        val key = patternParts[i].removeSurrounding("{", "}")
                        params[key] = pathParts[i]
                    }
                    patternParts[i] != pathParts[i] -> { matched = false; break }
                }
            }
            if (matched) return RouteMatch(screen, params)
        }
        return RouteMatch("NotFound", emptyMap())
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
