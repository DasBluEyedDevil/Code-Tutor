data class FormState(val name: String = "", val email: String = "")

fun main() {
    var formState = FormState()
    println("Initial: $formState")
    
    formState = formState.copy(name = "Alice")
    println("After name update: $formState")
    
    formState = formState.copy(email = "alice@example.com")
    println("After email update: $formState")
}