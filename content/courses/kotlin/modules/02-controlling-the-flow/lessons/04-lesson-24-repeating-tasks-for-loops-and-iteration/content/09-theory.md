---
type: "THEORY"
title: "Hands-On Exercises"
---


### Exercise 1: Sum of Numbers

**Challenge:** Calculate the sum of all numbers from 1 to 100 using a for loop.

<details>
<summary>Click to see solution</summary>


**Output:**

**Key concepts:**
- Using a range with for loop
- Accumulating values in a variable
- The `+=` compound operator

**Bonus - Math fact:** The formula is n(n+1)/2 = 100(101)/2 = 5050
</details>

---

### Exercise 2: FizzBuzz

**Challenge:** The classic FizzBuzz problem:
- Print numbers 1 to 30
- For multiples of 3, print "Fizz" instead
- For multiples of 5, print "Buzz" instead
- For multiples of both 3 and 5, print "FizzBuzz"

<details>
<summary>Click to see solution</summary>


**Output:**

**Key concepts:**
- Combining for loops with when expressions
- Using modulo operator for divisibility
- Order matters (check 15 before 3 or 5)
</details>

---

### Exercise 3: Reverse a String

**Challenge:** Write a program that reverses a string using a for loop.

**Example:** "KOTLIN" → "NILTOK"

<details>
<summary>Click to see solution</summary>


**Output:**

**Alternative solution using indices:**

**Key concepts:**
- String indexing
- Reverse iteration with downTo
- String concatenation
</details>

---

### Exercise 4: Find Maximum Value

**Challenge:** Given a list of numbers, find the maximum value using a for loop.

<details>
<summary>Click to see solution</summary>


**Output:**

**Alternative using indices:**

**Key concepts:**
- Tracking maximum value
- Comparing values in a loop
- Initializing with first element
</details>

---



```kotlin
fun main() {
    val numbers = listOf(45, 23, 67, 12, 89, 34, 56)
    var max = numbers[0]
    var maxIndex = 0

    for (i in numbers.indices) {
        if (numbers[i] > max) {
            max = numbers[i]
            maxIndex = i
        }
    }

    println("Maximum value: $max at index $maxIndex")
}
```
