---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: C) Serves as the entry point where the program starts**

The `main()` function is special—every Kotlin program begins execution here. When you run your program, the computer looks for `fun main()` and starts executing the code inside its curly braces.


---

**Question 2: B) Prints a line of text to the console**

`println()` stands for "print line." It displays text on the screen and moves to the next line.

```kotlin
println("Hello")
println("Kotlin")
```

Output:
```text
Hello
Kotlin
```

---

**Question 3: A) Inserting variable values into text using `$variableName`**

String interpolation lets you embed variables directly in strings:

```kotlin
val score = 100
println("Your score is $score")
```

The `$` tells Kotlin to insert the variable's value.

---

**Question 4: D) `/* */` (both C and D are correct)**

Kotlin supports two comment styles:

```kotlin
// Single-line comment

/* 
   Multi-line
   comment
*/
```

Comments are ignored by the compiler—they're for human readers only.

---

**Question 5: C) Reads user input and converts it to an integer**

```kotlin
val age = readln().toInt()
```

This does two things:
1. `readln()` reads text from user: `"25"`
2. `.toInt()` converts text to number: `25`

Without `.toInt()`, you'd have text, not a number you can do math with.

---
