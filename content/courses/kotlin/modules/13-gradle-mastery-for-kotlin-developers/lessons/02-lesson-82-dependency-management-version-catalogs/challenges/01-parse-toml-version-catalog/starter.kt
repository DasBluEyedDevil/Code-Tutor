data class VersionCatalog(
    val versions: Map<String, String>,
    val libraries: Map<String, String>,  // key -> "group:artifact:resolvedVersion"
    val bundles: Map<String, List<String>> // key -> list of library keys
)

// TODO: Implement VersionCatalogParser
class VersionCatalogParser {
    // Implement parse(toml: String): VersionCatalog
    //
    // 1. Track current section: [versions], [libraries], [bundles], [plugins]
    // 2. In [versions]: extract key = "value" pairs
    // 3. In [libraries]: parse { module = "group:artifact", version.ref = "key" }
    //    and resolve version.ref from the versions map
    // 4. In [bundles]: parse key = ["lib1", "lib2"] into lists
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
