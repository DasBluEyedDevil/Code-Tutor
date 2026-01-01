class Counter {
    var count: Int = 0
    
    fun increment() {
        count++
    }
    
    fun decrement() {
        count--
    }
}

fun main() {
    val counter = Counter()
    println("Initial: ${counter.count}")
    counter.increment()
    counter.increment()
    println("After increments: ${counter.count}")
    counter.decrement()
    println("After decrement: ${counter.count}")
}