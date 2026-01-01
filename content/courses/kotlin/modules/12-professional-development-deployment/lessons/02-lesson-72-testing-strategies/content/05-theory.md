---
type: "THEORY"
title: "JUnit 5 Fundamentals"
---


### Basic Test Structure


**Simple Test**:

### Test Lifecycle


### Parameterized Tests


---



```kotlin
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.*

class ValidationTest {

    @ParameterizedTest
    @ValueSource(strings = ["test@example.com", "user@domain.co", "name+tag@email.com"])
    fun `valid emails should pass validation`(email: String) {
        assertTrue(Validator.isValidEmail(email))
    }

    @ParameterizedTest
    @CsvSource(
        "0, INFANT",
        "5, CHILD",
        "13, TEEN",
        "20, ADULT",
        "70, SENIOR"
    )
    fun `age categories should be correct`(age: Int, expectedCategory: String) {
        assertEquals(expectedCategory, getAgeCategory(age))
    }

    @ParameterizedTest
    @MethodSource("passwordProvider")
    fun `weak passwords should fail validation`(password: String) {
        assertFalse(Validator.isStrongPassword(password))
    }

    companion object {
        @JvmStatic
        fun passwordProvider() = listOf(
            "123",
            "password",
            "abc123",
            "NoNumber"
        )
    }
}
```
