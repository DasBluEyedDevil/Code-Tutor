class KeyValueStore {
    private val store = mutableMapOf<String, Any>()
    
    fun put(key: String, value: Any) {
        TODO("Store the value")
    }
    
    fun getString(key: String, default: String = ""): String {
        TODO("Retrieve as String")
    }
    
    fun getInt(key: String, default: Int = 0): Int {
        TODO("Retrieve as Int")
    }
}

fun main() {
    val prefs = KeyValueStore()
    prefs.put("name", "Alice")
    prefs.put("age", 25)
    println("Name: ${prefs.getString("name")}")
    println("Age: ${prefs.getInt("age")}")
    println("Missing: ${prefs.getString("city", "unknown")}")
}
