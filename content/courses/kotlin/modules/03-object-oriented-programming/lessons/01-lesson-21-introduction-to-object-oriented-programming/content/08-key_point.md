---
type: "KEY_POINT"
title: "The `this` Keyword"
---


**`this`** refers to the current instance of the class. Use it to:
1. Distinguish between properties and parameters with the same name
2. Reference the current object


---



```kotlin
class Person(name: String, age: Int) {
    var name: String = name
    var age: Int = age

    fun updateName(name: String) {
        this.name = name  // this.name is the property, name is the parameter
    }

    fun haveBirthday() {
        this.age++  // Optional: this.age++ is the same as age++
    }

    fun compareAge(otherPerson: Person): String {
        return when {
            this.age > otherPerson.age -> "$name is older than ${otherPerson.name}"
            this.age < otherPerson.age -> "$name is younger than ${otherPerson.name}"
            else -> "$name and ${otherPerson.name} are the same age"
        }
    }
}

fun main() {
    val alice = Person("Alice", 25)
    val bob = Person("Bob", 30)

    alice.updateName("Alicia")
    println(alice.name)  // Alicia

    println(alice.compareAge(bob))  // Alicia is younger than Bob
}
```
