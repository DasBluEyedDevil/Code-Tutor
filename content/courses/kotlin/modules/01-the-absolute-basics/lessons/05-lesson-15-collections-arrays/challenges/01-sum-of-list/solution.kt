fun main() {
    val input = readln()
    val numbers = input.split(",").map { it.trim().toInt() }
    val sum = numbers.sum()
    println(sum)
}