fun linear(t: Double): Double = t
fun easeIn(t: Double): Double = t * t
fun easeOut(t: Double): Double = 1.0 - (1.0 - t) * (1.0 - t)

fun main() {
    val points = listOf(0.0, 0.25, 0.50, 0.75, 1.0)
    for (t in points) {
        println("Linear(${"%.2f".format(t)}) = ${"%.2f".format(linear(t))}")
    }
    for (t in points) {
        println("EaseIn(${"%.2f".format(t)}) = ${"%.2f".format(easeIn(t))}")
    }
    for (t in points) {
        println("EaseOut(${"%.2f".format(t)}) = ${"%.2f".format(easeOut(t))}")
    }
}
