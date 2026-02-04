fun checkAccess(path: String, token: String?, requiredRole: String): String {
    if (token.isNullOrBlank()) return "Denied"
    val role = token.substringAfter("role=", "")
    return if (role == requiredRole) "Granted" else "Denied"
}

fun main() {
    println("/admin: ${checkAccess("/admin", "role=admin", "admin")}")
    println("/admin: ${checkAccess("/admin", "role=user", "admin")}")
    println("/admin: ${checkAccess("/admin", null, "admin")}")
}
