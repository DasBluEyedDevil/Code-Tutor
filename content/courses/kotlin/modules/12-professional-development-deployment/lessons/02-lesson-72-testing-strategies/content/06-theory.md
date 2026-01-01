---
type: "THEORY"
title: "Kotest - Beautiful Testing DSL"
---


### Why Kotest?

Kotest provides a more readable, Kotlin-idiomatic testing syntax.


**Comparison**:

**JUnit**:

**Kotest**:

### Kotest Matchers


---



```kotlin
import io.kotest.matchers.*
import io.kotest.matchers.collections.*
import io.kotest.matchers.string.*

class KotestMatchersTest : StringSpec({

    "string matchers" {
        val name = "Kotlin"

        name shouldStartWith "Kot"
        name shouldEndWith "lin"
        name shouldContain "otl"
        name shouldHaveLength 6
        name shouldMatch "K[a-z]+".toRegex()
    }

    "collection matchers" {
        val list = listOf(1, 2, 3, 4, 5)

        list shouldHaveSize 5
        list shouldContain 3
        list shouldContainAll listOf(1, 3, 5)
        list.shouldBeSorted()

        val emptyList = emptyList<Int>()
        emptyList.shouldBeEmpty()
    }

    "numeric matchers" {
        val price = 99.99

        price shouldBeGreaterThan 50.0
        price shouldBeLessThan 100.0
        price.shouldBeBetween(90.0, 100.0)
    }

    "exception matchers" {
        shouldThrow<IllegalArgumentException> {
            require(false) { "Error message" }
        }.message shouldBe "Error message"
    }
})
```
