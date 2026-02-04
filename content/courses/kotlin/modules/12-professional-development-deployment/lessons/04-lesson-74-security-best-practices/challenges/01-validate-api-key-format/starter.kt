sealed class ValidationResult {
    data object Valid : ValidationResult()
    data class Invalid(val reason: String) : ValidationResult()
}

// TODO: Implement ApiKeyValidator
class ApiKeyValidator(
    val requiredPrefix: String = "sk-",
    val minLength: Int = 20,
    val allowedPattern: Regex = Regex("[a-zA-Z0-9-]+")
) {
    // Implement validate(key: String): ValidationResult
    // Checks in order:
    // 1. Not blank -> "must not be empty"
    // 2. Starts with requiredPrefix -> "must start with '${requiredPrefix}'"
    // 3. At least minLength characters -> "minimum length is $minLength"
    // 4. All characters match allowedPattern -> "contains illegal characters"
    // 5. All pass -> Valid
}

fun main() {
    val validator = ApiKeyValidator()

    println(validator.validate("sk-abc123def456ghi789"))  // Valid
    println(validator.validate("pk-abc123def456ghi789"))  // Invalid: must start with 'sk-'
    println(validator.validate("sk-short"))               // Invalid: minimum length is 20
    println(validator.validate("sk-abc!@#def456ghi789"))  // Invalid: contains illegal characters
    println(validator.validate(""))                        // Invalid: must not be empty
}
