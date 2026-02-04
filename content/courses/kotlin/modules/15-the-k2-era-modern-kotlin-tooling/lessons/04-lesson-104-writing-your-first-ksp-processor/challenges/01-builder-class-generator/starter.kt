data class PropertyInfo(
    val name: String,
    val type: String,
    val nullable: Boolean = false
)

// TODO: Implement BuilderGenerator
class BuilderGenerator {
    // Implement generate(className: String, properties: List<PropertyInfo>): String
    //
    // Output format:
    // class {ClassName}Builder {
    //     private var prop1: Type? = null
    //     private var prop2: Type? = null
    //
    //     fun prop1(value: Type) = apply { this.prop1 = value }
    //     fun prop2(value: Type) = apply { this.prop2 = value }
    //
    //     fun build() = ClassName(
    //         prop1 = requireNotNull(prop1) { "prop1 is required" },
    //         prop2 = prop2  // nullable, no requireNotNull
    //     )
    // }
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
