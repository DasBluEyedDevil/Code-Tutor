class StateHolder<T>(initial: T) {
    var value: T = initial
        private set
    var changeCount: Int = 0
        private set
    
    fun setValue(newValue: T) {
        value = newValue
        changeCount++
    }
    
    override fun toString() = "State: $value (changes: $changeCount)"
}

fun main() {
    val counter = StateHolder(0)
    println(counter)
    counter.setValue(5)
    println(counter)
    counter.setValue(10)
    println(counter)
}
