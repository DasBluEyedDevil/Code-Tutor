fun assertEquals(expected: Any, actual: Any, name: String) {
    if (expected == actual) {
        println("PASS: $name")
    } else {
        println("FAIL: $name - expected $expected but got $actual")
    }
}

fun assertTrue(condition: Boolean, name: String) {
    if (condition) {
        println("PASS: $name")
    } else {
        println("FAIL: $name")
    }
}

fun main() {
    assertEquals(4, 2 + 2, "2 + 2 equals 4")
    assertTrue(listOf(1, 2, 3).isNotEmpty(), "list is not empty")
    assertEquals(5, 2 + 2, "wrong sum")
}
