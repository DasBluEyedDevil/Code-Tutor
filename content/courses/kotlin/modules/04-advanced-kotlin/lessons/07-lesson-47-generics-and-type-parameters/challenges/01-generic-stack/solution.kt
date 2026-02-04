class Stack<T> {
    private val items = mutableListOf<T>()
    
    fun push(item: T) {
        items.add(item)
    }
    
    fun pop(): T? {
        if (items.isEmpty()) return null
        return items.removeAt(items.size - 1)
    }
    
    fun peek(): T? {
        return items.lastOrNull()
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
