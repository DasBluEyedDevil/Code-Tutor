data class DeprecationFinding(
    val pattern: String,
    val message: String,
    val replacement: String
)

class DeprecationDetector {
    private val rules = listOf(
        Triple(
            "kapt",
            "'kapt' found -- replace with KSP (kotlin(\"com.google.devtools.ksp\"))",
            "id(\"com.google.devtools.ksp\")"
        ),
        Triple(
            "-Xjvm-default=all",
            "'-Xjvm-default=all' -- no longer needed in Kotlin 2.0+ (all interface methods have default impls)",
            "Remove the flag entirely"
        ),
        Triple(
            "kotlin-android-extensions",
            "'kotlin-android-extensions' is removed -- use View Binding or Compose",
            "buildFeatures { viewBinding = true }"
        ),
        Triple(
            "experimental-coroutines",
            "'experimental-coroutines' flag -- coroutines are stable since 1.3",
            "Remove the flag; no opt-in needed"
        )
    )

    fun detect(sourceCode: String): List<DeprecationFinding> {
        return rules.mapNotNull { (pattern, message, replacement) ->
            if (sourceCode.contains(pattern)) {
                DeprecationFinding(pattern, message, replacement)
            } else null
        }
    }
}

fun main() {
    val detector = DeprecationDetector()

    val buildFile = """
        plugins {
            kotlin("jvm")
            kotlin("kapt")
        }

        tasks.withType<KotlinCompile> {
            compilerOptions {
                freeCompilerArgs.add("-Xjvm-default=all")
            }
        }
    """.trimIndent()

    val findings = detector.detect(buildFile)
    findings.forEach { println("DEPRECATED: ${it.message}") }

    println()

    val cleanCode = """
        plugins {
            kotlin("jvm")
            id("com.google.devtools.ksp")
        }
    """.trimIndent()

    val cleanFindings = detector.detect(cleanCode)
    if (cleanFindings.isEmpty()) println("No deprecated patterns found")
}
