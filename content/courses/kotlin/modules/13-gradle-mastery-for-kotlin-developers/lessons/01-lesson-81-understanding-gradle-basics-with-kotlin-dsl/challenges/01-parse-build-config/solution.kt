data class BuildConfig(
    val group: String,
    val version: String,
    val dependencies: List<String>
)

class BuildConfigParser {
    private val assignmentPattern = Regex("""(\w+)\s*=\s*"([^"]+)"""")
    private val dependencyPattern = Regex("""implementation\("([^"]+)"\)""")

    fun parse(config: String): BuildConfig {
        var group = ""
        var version = ""
        val dependencies = mutableListOf<String>()

        config.lines().forEach { line ->
            val trimmed = line.trim()

            // Check for group/version assignments
            assignmentPattern.find(trimmed)?.let { match ->
                when (match.groupValues[1]) {
                    "group" -> group = match.groupValues[2]
                    "version" -> version = match.groupValues[2]
                }
            }

            // Check for dependency declarations
            dependencyPattern.find(trimmed)?.let { match ->
                dependencies.add(match.groupValues[1])
            }
        }

        return BuildConfig(group, version, dependencies)
    }
}

fun main() {
    val config = """
        group = "com.example"
        version = "1.0.0"

        dependencies {
            implementation("org.jetbrains.kotlin:kotlin-stdlib:2.3.0")
            implementation("io.ktor:ktor-server-core:3.4.0")
            testImplementation("org.jetbrains.kotlin:kotlin-test:2.3.0")
        }
    """.trimIndent()

    val parser = BuildConfigParser()
    val result = parser.parse(config)

    println("group=${result.group}, version=${result.version}")
    println("dependencies=${result.dependencies}")
}
