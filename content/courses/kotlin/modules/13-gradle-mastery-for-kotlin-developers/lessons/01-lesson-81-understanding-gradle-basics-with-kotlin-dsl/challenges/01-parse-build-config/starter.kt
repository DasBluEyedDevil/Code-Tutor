data class BuildConfig(
    val group: String,
    val version: String,
    val dependencies: List<String>
)

// TODO: Implement BuildConfigParser
class BuildConfigParser {
    // Implement parse(config: String): BuildConfig
    // 1. Find line with group = "..." and extract the value
    // 2. Find line with version = "..." and extract the value
    // 3. Find lines with implementation("...") and extract coordinates
    // Return BuildConfig with extracted values
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
