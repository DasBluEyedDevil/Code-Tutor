---
type: "THEORY"
title: "Exercises"
---


### Exercise 1: Test Runner (Medium)

Create a simple test runner using annotations.

**Requirements**:
- `@Test` for test methods
- `@BeforeEach` for setup
- `@AfterEach` for cleanup
- Run all tests and report results

**Solution**:


### Exercise 2: Query Builder (Hard)

Create a query builder using annotations and reflection.

**Requirements**:
- `@Table` for table name
- `@Column` for column mapping
- Generate SELECT, INSERT queries

**Solution**:


### Exercise 3: Object Mapper (Hard)

Create an object mapper that converts between objects and maps.

**Requirements**:
- Convert object to Map<String, Any?>
- Convert Map<String, Any?> to object
- Support custom field names
- Handle nested objects

**Solution**:


---



```kotlin
import kotlin.reflect.full.*
import kotlin.reflect.KClass

@Target(AnnotationTarget.PROPERTY)
@Retention(AnnotationRetention.RUNTIME)
annotation class Field(val name: String = "")

data class Address(
    @Field("street_name")
    val street: String,

    val city: String
)

data class Person(
    @Field("full_name")
    val name: String,

    val age: Int,

    val address: Address
)

object ObjectMapper {
    fun toMap(obj: Any): Map<String, Any?> {
        val kClass = obj::class
        val map = mutableMapOf<String, Any?>()

        kClass.memberProperties.forEach { prop ->
            val fieldName = prop.annotations.filterIsInstance<Field>().firstOrNull()?.name?.takeIf { it.isNotEmpty() }
                ?: prop.name

            val value = prop.get(obj)

            map[fieldName] = when {
                value == null -> null
                isPrimitive(value) -> value
                else -> toMap(value)  // Nested object
            }
        }

        return map
    }

    fun <T : Any> fromMap(map: Map<String, Any?>, kClass: KClass<T>): T {
        val constructor = kClass.constructors.first()
        val args = constructor.parameters.associateWith { param ->
            val prop = kClass.memberProperties.find { it.name == param.name }

            val fieldName = prop?.annotations?.filterIsInstance<Field>()?.firstOrNull()?.name?.takeIf { it.isNotEmpty() }
                ?: param.name

            val value = map[fieldName]

            when {
                value == null -> null
                param.type.classifier == String::class -> value.toString()
                param.type.classifier == Int::class -> (value as? Number)?.toInt()
                else -> {
                    // Nested object
                    @Suppress("UNCHECKED_CAST")
                    fromMap(value as Map<String, Any?>, param.type.classifier as KClass<Any>)
                }
            }
        }

        return constructor.callBy(args)
    }

    private fun isPrimitive(value: Any): Boolean {
        return value is String || value is Number || value is Boolean
    }
}

fun main() {
    val person = Person(
        name = "Alice",
        age = 30,
        address = Address("123 Main St", "Springfield")
    )

    val map = ObjectMapper.toMap(person)
    println("To Map:")
    println(map)
    // {full_name=Alice, age=30, address={street_name=123 Main St, city=Springfield}}

    val restored = ObjectMapper.fromMap(map, Person::class)
    println("\nFrom Map:")
    println(restored)
    // Person(name=Alice, age=30, address=Address(street=123 Main St, city=Springfield))
}
```
