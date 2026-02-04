data class LintWarning(
    val line: Int,
    val message: String
)

class DockerfileLinter {
    fun lint(dockerfile: String): List<LintWarning> {
        val warnings = mutableListOf<LintWarning>()
        val lines = dockerfile.lines()
        val exposedPorts = mutableSetOf<String>()
        var hasFrom = false
        var hasUser = false
        var hasWorkdir = false
        var firstInstruction = true

        lines.forEachIndexed { index, rawLine ->
            val line = rawLine.trim()
            val lineNum = index + 1

            // Skip empty lines and comments
            if (line.isEmpty() || line.startsWith("#")) return@forEachIndexed

            val instruction = line.split("\\s+".toRegex()).first().uppercase()

            // Check: first instruction must be FROM
            if (firstInstruction) {
                if (instruction != "FROM") {
                    warnings.add(LintWarning(lineNum, "WARNING: No FROM instruction found"))
                }
                firstInstruction = false
            }

            // Check: COPY/ADD before FROM
            if (!hasFrom && instruction in listOf("COPY", "ADD")) {
                warnings.add(LintWarning(lineNum, "WARNING: $instruction before FROM instruction"))
            }

            when (instruction) {
                "FROM" -> hasFrom = true
                "USER" -> hasUser = true
                "WORKDIR" -> hasWorkdir = true
                "EXPOSE" -> {
                    val port = line.substringAfter("EXPOSE").trim().split("\\s+".toRegex()).firstOrNull() ?: ""
                    if (port in exposedPorts) {
                        warnings.add(LintWarning(lineNum, "WARNING: Duplicate EXPOSE port $port"))
                    }
                    exposedPorts.add(port)
                }
            }
        }

        if (!hasUser && hasFrom) {
            warnings.add(LintWarning(0, "WARNING: Container runs as root (no USER instruction)"))
        }
        if (!hasWorkdir && hasFrom) {
            warnings.add(LintWarning(0, "WARNING: No WORKDIR set (using default directory)"))
        }

        return warnings
    }
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
    warnings2.forEach { println(it.message) }
}
