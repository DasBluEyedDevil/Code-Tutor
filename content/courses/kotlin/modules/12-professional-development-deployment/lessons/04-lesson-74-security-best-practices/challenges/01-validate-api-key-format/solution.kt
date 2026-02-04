sealed class ValidationResult {
    data object Valid : ValidationResult()
    data class Invalid(val reason: String) : ValidationResult()

    override fun toString(): String = when (this) {
        is Valid -> "Valid"
        is Invalid -> "Invalid: $reason"
    }
}

class ApiKeyValidator(
    val requiredPrefix: String = "sk-",
    val minLength: Int = 20,
    val allowedPattern: Regex = Regex("[a-zA-Z0-9-]+")
) {
    fun validate(key: String): ValidationResult {
        if (key.isBlank()) {
            return ValidationResult.Invalid("must not be empty")
        }
        if (!key.startsWith(requiredPrefix)) {
            return ValidationResult.Invalid("must start with '$requiredPrefix'")
        }
        if (key.length < minLength) {
            return ValidationResult.Invalid("minimum length is $minLength")
        }
        if (!allowedPattern.matches(key)) {
            return ValidationResult.Invalid("contains illegal characters")
        }
        return ValidationResult.Valid
    }
}

fun main() {
    val validator = ApiKeyValidator()

    println(validator.validate("sk-abc123def456ghi789"))
    println(validator.validate("pk-abc123def456ghi789"))
    println(validator.validate("sk-short"))
    println(validator.validate("sk-abc!@#def456ghi789"))
    println(validator.validate(""))
}
