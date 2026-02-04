fun linear(t: Double): Double = TODO("Return linear interpolation")
fun easeIn(t: Double): Double = TODO("Return quadratic ease-in")
fun easeOut(t: Double): Double = TODO("Return quadratic ease-out")

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
