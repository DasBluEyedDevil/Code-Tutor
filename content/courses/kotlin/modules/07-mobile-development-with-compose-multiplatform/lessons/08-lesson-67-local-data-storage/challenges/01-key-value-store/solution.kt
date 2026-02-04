class KeyValueStore {
    private val store = mutableMapOf<String, Any>()
    
    fun put(key: String, value: Any) {
        store[key] = value
    }
    
    fun getString(key: String, default: String = ""): String {
        return store[key] as? String ?: default
    }
    
    fun getInt(key: String, default: Int = 0): Int {
        return store[key] as? Int ?: default
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
