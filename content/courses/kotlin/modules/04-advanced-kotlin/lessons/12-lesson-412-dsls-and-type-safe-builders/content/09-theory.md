---
type: "THEORY"
title: "Advanced DSL Pattern: Builder with Validation"
---



---



```kotlin
class ValidationException(message: String) : Exception(message)

@DslMarker
annotation class FormMarker

@FormMarker
class Form {
    private val fields = mutableListOf<Field>()
    var submitUrl: String = ""

    fun textField(action: TextField.() -> Unit) {
        fields.add(TextField().apply(action))
    }

    fun emailField(action: EmailField.() -> Unit) {
        fields.add(EmailField().apply(action))
    }

    fun numberField(action: NumberField.() -> Unit) {
        fields.add(NumberField().apply(action))
    }

    fun validate() {
        if (submitUrl.isBlank()) {
            throw ValidationException("Submit URL is required")
        }

        fields.forEach { it.validate() }
    }

    fun render(): String {
        return """
            Form (submit to: $submitUrl)
            Fields:
            ${fields.joinToString("\n") { "  - ${it.render()}" }}
        """.trimIndent()
    }
}

@FormMarker
abstract class Field {
    var name: String = ""
    var label: String = ""
    var required: Boolean = false

    abstract fun validate()
    abstract fun render(): String

    protected fun baseValidation() {
        if (name.isBlank()) {
            throw ValidationException("Field name is required")
        }
    }
}

@FormMarker
class TextField : Field() {
    var minLength: Int = 0
    var maxLength: Int = Int.MAX_VALUE

    override fun validate() {
        baseValidation()
        if (minLength < 0) {
            throw ValidationException("$name: minLength cannot be negative")
        }
        if (maxLength < minLength) {
            throw ValidationException("$name: maxLength must be >= minLength")
        }
    }

    override fun render() = "TextField('$name', label='$label', required=$required, length=$minLength..$maxLength)"
}

@FormMarker
class EmailField : Field() {
    override fun validate() {
        baseValidation()
    }

    override fun render() = "EmailField('$name', label='$label', required=$required)"
}

@FormMarker
class NumberField : Field() {
    var min: Int = Int.MIN_VALUE
    var max: Int = Int.MAX_VALUE

    override fun validate() {
        baseValidation()
        if (max < min) {
            throw ValidationException("$name: max must be >= min")
        }
    }

    override fun render() = "NumberField('$name', label='$label', required=$required, range=$min..$max)"
}

fun form(action: Form.() -> Unit): Form {
    val form = Form()
    form.action()
    form.validate()
    return form
}

fun main() {
    val contactForm = form {
        submitUrl = "/contact"

        textField {
            name = "fullName"
            label = "Full Name"
            required = true
            minLength = 3
            maxLength = 100
        }

        emailField {
            name = "email"
            label = "Email Address"
            required = true
        }

        numberField {
            name = "age"
            label = "Age"
            min = 18
            max = 120
        }
    }

    println(contactForm.render())
}
```
