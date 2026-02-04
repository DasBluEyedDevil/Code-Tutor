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

// TODO: Implement builders with lambda receivers
// 1. CodeStyleBuilder with maxLineLength, indentSize, trailingComma properties
// 2. DependencyRulesBuilder with ban(group), require(group) functions
// 3. TeamConventionsBuilder with codeStyle { }, dependencies { }, minTestCoverage
// 4. Top-level function: fun teamConventions(block: ...) -> TeamConventions

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
