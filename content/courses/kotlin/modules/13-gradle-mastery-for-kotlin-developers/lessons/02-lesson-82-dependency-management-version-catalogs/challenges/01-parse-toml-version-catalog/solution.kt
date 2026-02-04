data class VersionCatalog(
    val versions: Map<String, String>,
    val libraries: Map<String, String>,
    val bundles: Map<String, List<String>>
)

class VersionCatalogParser {
    private val sectionPattern = Regex("""\[(\w+)]""")
    private val versionPattern = Regex("""(\S+)\s*=\s*"([^"]+)"""")
    private val libraryPattern = Regex("""(\S+)\s*=\s*\{[^}]*module\s*=\s*"([^"]+)"[^}]*version\.ref\s*=\s*"([^"]+)"[^}]*}""")
    private val bundlePattern = Regex("""(\S+)\s*=\s*\[([^\]]+)]""")

    fun parse(toml: String): VersionCatalog {
        val versions = mutableMapOf<String, String>()
        val libraries = mutableMapOf<String, String>()
        val bundles = mutableMapOf<String, List<String>>()
        var currentSection = ""

        toml.lines().forEach { line ->
            val trimmed = line.trim()
            if (trimmed.isEmpty() || trimmed.startsWith("#")) return@forEach

            // Check for section header
            sectionPattern.matchEntire(trimmed)?.let {
                currentSection = it.groupValues[1]
                return@forEach
            }

            when (currentSection) {
                "versions" -> {
                    versionPattern.find(trimmed)?.let { match ->
                        versions[match.groupValues[1]] = match.groupValues[2]
                    }
                }
                "libraries" -> {
                    libraryPattern.find(trimmed)?.let { match ->
                        val key = match.groupValues[1]
                        val module = match.groupValues[2]
                        val versionRef = match.groupValues[3]
                        val resolvedVersion = versions[versionRef] ?: versionRef
                        libraries[key] = "$module:$resolvedVersion"
                    }
                }
                "bundles" -> {
                    bundlePattern.find(trimmed)?.let { match ->
                        val key = match.groupValues[1]
                        val libs = match.groupValues[2]
                            .split(",")
                            .map { it.trim().removeSurrounding("\"") }
                        bundles[key] = libs
                    }
                }
            }
        }

        return VersionCatalog(versions, libraries, bundles)
    }
}

fun main() {
    val toml = """
        [versions]
        kotlin = "2.3.0"
        ktor = "3.4.0"
        koin = "4.1.1"

        [libraries]
        ktor-server-core = { module = "io.ktor:ktor-server-core", version.ref = "ktor" }
        ktor-server-netty = { module = "io.ktor:ktor-server-netty", version.ref = "ktor" }
        koin-core = { module = "io.insert-koin:koin-core", version.ref = "koin" }

        [bundles]
        ktor-server = ["ktor-server-core", "ktor-server-netty"]

        [plugins]
        kotlin-jvm = { id = "org.jetbrains.kotlin.jvm", version.ref = "kotlin" }
    """.trimIndent()

    val parser = VersionCatalogParser()
    val catalog = parser.parse(toml)

    catalog.libraries.forEach { (key, coord) ->
        println("$key = $coord")
    }
    catalog.bundles.forEach { (key, libs) ->
        println("$key = $libs")
    }
}
