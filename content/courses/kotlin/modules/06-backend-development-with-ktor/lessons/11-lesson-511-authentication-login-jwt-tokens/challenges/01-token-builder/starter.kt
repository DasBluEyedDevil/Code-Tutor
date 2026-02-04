fun buildToken(userId: Int, role: String): String {
    // Build a fake JWT-like token: "header.payload.signature"
    TODO("Implement token builder")
}

fun main() {
    val token = buildToken(42, "admin")
    val parts = token.split(".")
    println("Parts: ${parts.size}")
    println("Payload: ${parts[1]}")
}
