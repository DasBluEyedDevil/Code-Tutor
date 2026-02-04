fun checkAccess(path: String, token: String?, requiredRole: String): String {
    // Check if token contains the required role
    // Return "Granted" or "Denied"
    TODO("Implement route guard")
}

fun main() {
    println("/admin: ${checkAccess("/admin", "role=admin", "admin")}")
    println("/admin: ${checkAccess("/admin", "role=user", "admin")}")
    println("/admin: ${checkAccess("/admin", null, "admin")}")
}
