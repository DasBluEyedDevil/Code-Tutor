class Counter {
    var count: Int = 0
    
    // Add increment method
    
    // Add decrement method
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