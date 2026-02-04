data class GeneratedFunction(
    val className: String,
    val properties: List<String>,
    val code: String
)

// TODO: Implement AnnotationProcessor
class AnnotationProcessor {
    // Implement process(sourceCode: String): List<GeneratedFunction>
    //
    // 1. Find lines with @AutoToString annotation
    // 2. Find the next class declaration after the annotation
    // 3. Extract class name and constructor parameters
    // 4. Generate: fun ClassName.generatedToString() = "ClassName(prop1=$prop1, prop2=$prop2)"
    // 5. Return list of generated functions
}

fun main() {
    val sourceCode = """
        @AutoToString
        data class User(
            val name: String,
            val age: Int
        )

        class NotAnnotated(val x: Int)

        @AutoToString
        data class Product(
            val id: Long,
            val title: String,
            val price: Double
        )
    """.trimIndent()

    val processor = AnnotationProcessor()
    val generated = processor.process(sourceCode)

    generated.forEach { println(it.code) }
    println("Generated ${generated.size} toString functions")
}
