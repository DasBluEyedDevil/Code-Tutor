class Button(val label: String, val onClick: () -> Unit) {
    fun click() {
        // Call the onClick handler
    }
}

fun main() {
    val button = Button("Click Me") {
        println("Button was clicked!")
    }
    
    println("Button label: ${button.label}")
    button.click()
}