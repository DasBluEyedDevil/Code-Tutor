fun buildToken(userId: Int, role: String): String {
    val header = "alg=HS256"
    val payload = "userId=$userId,role=$role"
    val signature = "signed"
    return "$header.$payload.$signature"
}

fun main() {
    val token = buildToken(42, "admin")
    val parts = token.split(".")
    println("Parts: ${parts.size}")
    println("Payload: ${parts[1]}")
}
