---
type: "THEORY"
title: "Challenge Extensions"
---


Ready to level up? Try these advanced challenges:

### Challenge 1: Add BMI Calculator

Add height and weight questions, then calculate and display BMI:


<details>
<summary>Click to see implementation hint</summary>


</details>

---

### Challenge 2: Add Zodiac Sign Calculator

Calculate Western zodiac sign based on birth month and day:

<details>
<summary>Click to see solution</summary>


</details>

---

### Challenge 3: Add Life Events Timeline

Calculate and display significant life milestones:

<details>
<summary>Click to see solution</summary>


</details>

---

### Challenge 4: Save Profile to File (Advanced)

Save the generated profile to a text file:

<details>
<summary>Click to see solution</summary>


Note: File I/O requires additional imports and is an advanced topic!

</details>

---

### Challenge 5: Multiple Profiles

Allow creating profiles for multiple people:

<details>
<summary>Click to see implementation hint</summary>


</details>

---



```kotlin
fun main() {
    var continueCreating = true

    while (continueCreating) {
        // Run profile creation code

        println()
        print("Create another profile? (yes/no): ")
        val response = readln().lowercase()
        continueCreating = response == "yes" || response == "y"
    }

    println("Thank you for using Profile Generator!")
}
```
