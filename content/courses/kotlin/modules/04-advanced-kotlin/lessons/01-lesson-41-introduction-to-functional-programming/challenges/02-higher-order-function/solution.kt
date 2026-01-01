fun applyOperation(a: Int, b: Int, operation: (Int, Int) -> Int): Int {
    return operation(a, b)
}

fun main() {
    val result1 = applyOperation(10, 5) { x, y -> x + y }
    val result2 = applyOperation(10, 5) { x, y -> x * y }
    println("Addition: $result1")
    println("Multiplication: $result2")
}