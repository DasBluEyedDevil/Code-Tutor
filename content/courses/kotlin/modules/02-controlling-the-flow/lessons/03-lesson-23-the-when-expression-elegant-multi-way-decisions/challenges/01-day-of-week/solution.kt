fun main() {
    val day = 6
    when (day) {
        in 1..5 -> println("Weekday")
        6, 7 -> println("Weekend")
        else -> println("Invalid")
    }
}