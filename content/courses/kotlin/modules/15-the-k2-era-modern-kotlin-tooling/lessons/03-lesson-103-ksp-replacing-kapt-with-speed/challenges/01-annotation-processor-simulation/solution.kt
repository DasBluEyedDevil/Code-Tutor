data class GeneratedFunction(
    val className: String,
    val properties: List<String>,
    val code: String
)

class AnnotationProcessor {
    private val classPattern = Regex("""(?:data\s+)?class\s+(\w+)\s*\(""")
    private val propertyPattern = Regex("""val\s+(\w+)\s*:""")

    fun process(sourceCode: String): List<GeneratedFunction> {
        val results = mutableListOf<GeneratedFunction>()
        val lines = sourceCode.lines()
        var i = 0

        while (i < lines.size) {
            val trimmed = lines[i].trim()

            if (trimmed == "@AutoToString") {
                // Find the class declaration (may be on next line)
                val classLines = mutableListOf<String>()
                var j = i + 1
                var braceDepth = 0
                var foundClass = false

                while (j < lines.size) {
                    val line = lines[j].trim()
                    if (!foundClass && classPattern.containsMatchIn(line)) {
                        foundClass = true
                    }
                    if (foundClass) {
                        classLines.add(line)
                        braceDepth += line.count { it == '(' } - line.count { it == ')' }
                        if (braceDepth <= 0) break
                    }
                    j++
                }

                if (foundClass) {
                    val fullDeclaration = classLines.joinToString(" ")
                    val className = classPattern.find(fullDeclaration)?.groupValues?.get(1) ?: ""
                    val properties = propertyPattern.findAll(fullDeclaration).map { it.groupValues[1] }.toList()

                    val propsString = properties.joinToString(", ") { "$it=\$$it" }
                    val code = """fun $className.generatedToString() = "$className($propsString)""""

                    results.add(GeneratedFunction(className, properties, code))
                }
                i = j + 1
            } else {
                i++
            }
        }

        return results
    }
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
