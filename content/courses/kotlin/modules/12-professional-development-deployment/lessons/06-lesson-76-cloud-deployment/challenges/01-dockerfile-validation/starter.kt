data class LintWarning(
    val line: Int,
    val message: String
)

// TODO: Implement DockerfileLinter
class DockerfileLinter {
    // Implement lint(dockerfile: String): List<LintWarning>
    //
    // Rules to check:
    // 1. First non-comment instruction must be FROM
    // 2. COPY/ADD before FROM is invalid
    // 3. No USER instruction -> warn about running as root
    // 4. Duplicate EXPOSE ports -> warn
    // 5. No WORKDIR set -> warn about default directory
}

fun main() {
    val validDockerfile = """
        FROM eclipse-temurin:21-jre-alpine
        WORKDIR /app
        COPY build/libs/app.jar app.jar
        EXPOSE 8080
        USER appuser
        CMD ["java", "-jar", "app.jar"]
    """.trimIndent()

    val badDockerfile = """
        COPY build/libs/app.jar app.jar
        RUN apt-get update
        EXPOSE 8080
        EXPOSE 8080
        CMD ["java", "-jar", "app.jar"]
    """.trimIndent()

    val linter = DockerfileLinter()

    val warnings1 = linter.lint(validDockerfile)
    println("Warnings: ${warnings1.size}")

    val warnings2 = linter.lint(badDockerfile)
    warnings2.forEach { println("${it.message}") }
}
