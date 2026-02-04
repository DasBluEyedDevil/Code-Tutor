class Stack<T> {
    private val items = mutableListOf<T>()
    
    fun push(item: T) {
        TODO("Add item to top of stack")
    }
    
    fun pop(): T? {
        TODO("Remove and return top item, or null if empty")
    }
    
    fun peek(): T? {
        TODO("Return top item without removing, or null if empty")
    }
    
    fun isEmpty(): Boolean = items.isEmpty()
}

fun main() {
    val stack = Stack<Int>()
    stack.push(10)
    stack.push(20)
    stack.push(30)
    println("Top: ${stack.peek()}")
    println("Popped: ${stack.pop()}")
    println("Top: ${stack.peek()}")
}
