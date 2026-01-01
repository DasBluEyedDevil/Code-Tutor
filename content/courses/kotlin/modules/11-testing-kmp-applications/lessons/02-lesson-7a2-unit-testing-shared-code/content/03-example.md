---
type: "EXAMPLE"
title: "Testing Domain Logic"
---

Domain logic is the easiest to test - pure functions with no dependencies:

```kotlin
// Production code: commonMain/domain/usecase/ValidateNoteUseCase.kt
class ValidateNoteUseCase {
    fun execute(title: String, content: String): ValidationResult {
        val errors = mutableListOf<String>()
        
        if (title.isBlank()) {
            errors.add("Title cannot be empty")
        }
        if (title.length > 100) {
            errors.add("Title must be 100 characters or less")
        }
        if (content.length > 10000) {
            errors.add("Content must be 10,000 characters or less")
        }
        
        return if (errors.isEmpty()) {
            ValidationResult.Valid
        } else {
            ValidationResult.Invalid(errors)
        }
    }
}

sealed class ValidationResult {
    object Valid : ValidationResult()
    data class Invalid(val errors: List<String>) : ValidationResult()
}

// Test code: commonTest/domain/usecase/ValidateNoteUseCaseTest.kt
class ValidateNoteUseCaseTest {
    private val useCase = ValidateNoteUseCase()
    
    @Test
    fun `valid note returns Valid`() {
        val result = useCase.execute(
            title = "My Note",
            content = "Some content"
        )
        
        assertTrue(result is ValidationResult.Valid)
    }
    
    @Test
    fun `empty title returns Invalid with error message`() {
        val result = useCase.execute(
            title = "",
            content = "Some content"
        )
        
        assertTrue(result is ValidationResult.Invalid)
        val errors = (result as ValidationResult.Invalid).errors
        assertTrue(errors.contains("Title cannot be empty"))
    }
    
    @Test
    fun `title over 100 chars returns Invalid`() {
        val longTitle = "a".repeat(101)
        
        val result = useCase.execute(
            title = longTitle,
            content = "Content"
        )
        
        assertTrue(result is ValidationResult.Invalid)
    }
    
    @Test
    fun `multiple validation errors are returned`() {
        val longTitle = "a".repeat(101)
        val longContent = "b".repeat(10001)
        
        val result = useCase.execute(title = longTitle, content = longContent)
        
        assertTrue(result is ValidationResult.Invalid)
        assertEquals(2, (result as ValidationResult.Invalid).errors.size)
    }
}
```
