---
type: "THEORY"
title: "Practical Use Cases"
---


### Use Case 1: Simple Validation Framework


### Use Case 2: Simple Serialization


### Use Case 3: Dependency Injection Container


---



```kotlin
import kotlin.reflect.full.*

@Target(AnnotationTarget.PROPERTY)
@Retention(AnnotationRetention.RUNTIME)
annotation class Inject

class Database {
    fun query(sql: String) = "Result for: $sql"
}

class UserRepository {
    @Inject
    lateinit var database: Database

    fun findUser(id: Int): String {
        return database.query("SELECT * FROM users WHERE id = $id")
    }
}

class Container {
    private val instances = mutableMapOf<kotlin.reflect.KClass<*>, Any>()

    fun <T : Any> register(kClass: kotlin.reflect.KClass<T>, instance: T) {
        instances[kClass] = instance
    }

    fun <T : Any> get(kClass: kotlin.reflect.KClass<T>): T {
        @Suppress("UNCHECKED_CAST")
        return instances[kClass] as T
    }

    fun <T : Any> inject(obj: T) {
        val kClass = obj::class

        kClass.memberProperties.forEach { prop ->
            if (prop.annotations.any { it is Inject }) {
                if (prop is kotlin.reflect.KMutableProperty<*>) {
                    val dependency = instances[prop.returnType.classifier as kotlin.reflect.KClass<*>]
                    if (dependency != null) {
                        prop.setter.call(obj, dependency)
                    }
                }
            }
        }
    }
}

fun main() {
    val container = Container()
    container.register(Database::class, Database())

    val repository = UserRepository()
    container.inject(repository)

    println(repository.findUser(1))
    // Result for: SELECT * FROM users WHERE id = 1
}
```
