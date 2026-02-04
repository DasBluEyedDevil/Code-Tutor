fun assertEquals(expected: Any, actual: Any, name: String) {
    // Print "PASS: name" or "FAIL: name - expected X but got Y"
    TODO("Implement assertEquals")
}

fun assertTrue(condition: Boolean, name: String) {
    // Print "PASS: name" or "FAIL: name"
    TODO("Implement assertTrue")
}

fun main() {
    assertEquals(4, 2 + 2, "2 + 2 equals 4")
    assertTrue(listOf(1, 2, 3).isNotEmpty(), "list is not empty")
    assertEquals(5, 2 + 2, "wrong sum")
}
