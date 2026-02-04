data class PropertyInfo(
    val name: String,
    val type: String,
    val nullable: Boolean = false
)

class BuilderGenerator {
    fun generate(className: String, properties: List<PropertyInfo>): String {
        val sb = StringBuilder()
        sb.appendLine("class ${className}Builder {")

        // Private fields
        properties.forEach { prop ->
            sb.appendLine("    private var ${prop.name}: ${prop.type}? = null")
        }
        sb.appendLine()

        // Setter methods
        properties.forEach { prop ->
            sb.appendLine("    fun ${prop.name}(value: ${prop.type}) = apply { this.${prop.name} = value }")
        }
        sb.appendLine()

        // Build method
        sb.appendLine("    fun build() = $className(")
        properties.forEachIndexed { index, prop ->
            val comma = if (index < properties.size - 1) "," else ""
            if (prop.nullable) {
                sb.appendLine("        ${prop.name} = ${prop.name}$comma")
            } else {
                sb.appendLine("        ${prop.name} = requireNotNull(${prop.name}) { \"${prop.name} is required\" }$comma")
            }
        }
        sb.appendLine("    )")
        sb.appendLine("}")

        return sb.toString()
    }
}

fun main() {
    val generator = BuilderGenerator()

    val properties = listOf(
        PropertyInfo("name", "String"),
        PropertyInfo("email", "String"),
        PropertyInfo("age", "Int"),
        PropertyInfo("nickname", "String", nullable = true)
    )

    val code = generator.generate("User", properties)
    println(code)
}
