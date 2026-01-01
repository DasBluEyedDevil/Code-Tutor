data class LoginForm(val email: String, val password: String)

fun validateLogin(form: LoginForm): Map<String, String> {
    val errors = mutableMapOf<String, String>()
    
    if (!form.email.contains("@")) {
        errors["email"] = "Invalid email format"
    }
    
    if (form.password.length < 8) {
        errors["password"] = "Password must be at least 8 characters"
    }
    
    return errors
}

fun main() {
    val form1 = LoginForm("invalidemail", "short")
    println("Errors: ${validateLogin(form1)}")
    
    val form2 = LoginForm("user@example.com", "password123")
    println("Valid: ${validateLogin(form2).isEmpty()}")
}