data class CodeStyleConfig(
    val maxLineLength: Int = 120,
    val indentSize: Int = 4,
    val trailingComma: Boolean = true
)

data class DependencyRules(
    val bannedGroups: List<String> = emptyList(),
    val requiredGroup: String? = null
)

data class TeamConventions(
    val codeStyle: CodeStyleConfig = CodeStyleConfig(),
    val dependencies: DependencyRules = DependencyRules(),
    val minTestCoverage: Int = 80
)

@DslMarker
annotation class ConventionsDsl

@ConventionsDsl
class CodeStyleBuilder {
    var maxLineLength: Int = 120
    var indentSize: Int = 4
    var trailingComma: Boolean = true

    fun build() = CodeStyleConfig(maxLineLength, indentSize, trailingComma)
}

@ConventionsDsl
class DependencyRulesBuilder {
    private val banned = mutableListOf<String>()
    private var required: String? = null

    fun ban(group: String) { banned.add(group) }
    fun require(group: String) { required = group }

    fun build() = DependencyRules(banned.toList(), required)
}

@ConventionsDsl
class TeamConventionsBuilder {
    private var codeStyleConfig = CodeStyleConfig()
    private var dependencyRules = DependencyRules()
    var minTestCoverage: Int = 80

    fun codeStyle(block: CodeStyleBuilder.() -> Unit) {
        codeStyleConfig = CodeStyleBuilder().apply(block).build()
    }

    fun dependencies(block: DependencyRulesBuilder.() -> Unit) {
        dependencyRules = DependencyRulesBuilder().apply(block).build()
    }

    fun build() = TeamConventions(codeStyleConfig, dependencyRules, minTestCoverage)
}

fun teamConventions(block: TeamConventionsBuilder.() -> Unit): TeamConventions {
    return TeamConventionsBuilder().apply(block).build()
}

fun main() {
    val conventions = teamConventions {
        codeStyle {
            maxLineLength = 120
            indentSize = 4
        }
        dependencies {
            ban("com.google.guava")
            require("org.jetbrains.kotlinx")
        }
        minTestCoverage = 90
    }

    println("maxLineLength=${conventions.codeStyle.maxLineLength}, indentSize=${conventions.codeStyle.indentSize}")
    println("bannedGroups=${conventions.dependencies.bannedGroups}, requiredGroup=${conventions.dependencies.requiredGroup}")
    println("minTestCoverage=${conventions.minTestCoverage}")
}
