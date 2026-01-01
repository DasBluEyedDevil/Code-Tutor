---
type: "EXAMPLE"
title: "Testing Validated"
---


Test patterns for Validated-returning functions:



```kotlin
import arrow.core.*
import kotlin.test.*

class RegistrationValidatorTest {
    
    @Test
    fun `valid input returns Valid`() {
        val result = validateRegistration(
            username = "johndoe",
            email = "john@example.com",
            password = "password123",
            age = 25
        )
        
        assertTrue(result is Validated.Valid)
        assertEquals("johndoe", result.value.username)
    }
    
    @Test
    fun `collects all validation errors`() {
        val result = validateRegistration(
            username = "ab",           // Too short
            email = "invalid",         // No @
            password = "123",          // Too short
            age = 16                   // Too young
        )
        
        assertTrue(result is Validated.Invalid)
        val errors = result.value
        
        assertEquals(4, errors.size)
        assertTrue(errors.any { "username" in it.lowercase() })
        assertTrue(errors.any { "email" in it.lowercase() })
        assertTrue(errors.any { "password" in it.lowercase() })
        assertTrue(errors.any { "18" in it || "age" in it.lowercase() })
    }
    
    @Test
    fun `single error returns NonEmptyList with one element`() {
        val result = validateRegistration(
            username = "validuser",
            email = "valid@email.com",
            password = "validpassword",
            age = 16  // Only this fails
        )
        
        assertTrue(result is Validated.Invalid)
        assertEquals(1, result.value.size)
    }
}
```
