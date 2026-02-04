// TODO: Implement pipe -- applies function to value (left-to-right)
// infix fun <A, B> A.pipe(f: (A) -> B): B = ???

// TODO: Implement compose -- combines two functions (right-to-left)
// fun <A, B, C> compose(f: (B) -> C, g: (A) -> B): (A) -> C = ???

fun main() {
    // Test pipe: left-to-right
    val result1 = "hello world".pipe { it.uppercase() }
    println(result1) // HELLO WORLD

    // Test compose: right-to-left
    val doubleIt: (Int) -> Int = { it * 2 }
    val addOne: (Int) -> Int = { it + 1 }
    val doubleThenAddOne = compose(addOne, doubleIt)
    println(doubleThenAddOne(2)) // 6  (2 * 2 = 4, then 4 + 1 = 5... wait: 2*2=4, 4+1=5)
    // Actually: compose(f, g)(x) = f(g(x)) = addOne(doubleIt(2)) = addOne(4) = 5
    // Let's fix: addOne then double
    val addOneThenDouble = compose(doubleIt, addOne)
    println(addOneThenDouble(2)) // 6  (2 + 1 = 3, then 3 * 2 = 6)

    // Pipeline: chain transformations on a list
    val words = "kotlin,arrow,fp,functional"
    val result3 = words
        .pipe { it.split(",") }
        .pipe { it.filter { word -> word.length > 2 } }
        .pipe { it.map { word -> word.uppercase() } }
    println(result3) // [KOTLIN, ARROW, FUNCTIONAL]
}
