infix fun <A, B> A.pipe(f: (A) -> B): B = f(this)

fun <A, B, C> compose(f: (B) -> C, g: (A) -> B): (A) -> C = { a -> f(g(a)) }

fun main() {
    // Test pipe: left-to-right
    val result1 = "hello world".pipe { it.uppercase() }
    println(result1) // HELLO WORLD

    // Test compose: right-to-left
    val doubleIt: (Int) -> Int = { it * 2 }
    val addOne: (Int) -> Int = { it + 1 }

    val addOneThenDouble = compose(doubleIt, addOne)
    println(addOneThenDouble(2)) // 6  (2 + 1 = 3, then 3 * 2 = 6)

    // Pipeline: chain transformations
    val words = "kotlin,arrow,fp,functional"
    val result3 = words
        .pipe { it.split(",") }
        .pipe { it.filter { word -> word.length > 2 } }
        .pipe { it.map { word -> word.uppercase() } }
    println(result3) // [KOTLIN, ARROW, FUNCTIONAL]
}
