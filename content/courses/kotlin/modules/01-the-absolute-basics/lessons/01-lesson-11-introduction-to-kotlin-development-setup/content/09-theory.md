---
type: "THEORY"
title: "Your First Interactive Program"
---


Let's make something more interesting—a program that talks back!


**Run this program** and interact with it:


### New Concepts Introduced

**`readln()`**:
- Reads a line of text from user input
- Waits for user to type something and press Enter

**`val name = readln()`**:
- `val` = declares a **val**ue (a named container for data)
- `name` = the name of this container
- `=` = assigns the result of `readln()` to `name`

**`"Hello, $name!"`**:
- `$name` = **string interpolation** (inserting a variable's value into text)
- Dollar sign tells Kotlin: "Replace this with the value of `name`"

**`toInt()`**:
- Converts text to an integer (whole number)
- `"25".toInt()` becomes `25` (number)

---



```kotlin
=== Kotlin Greeter ===
What's your name?
Alice
Hello, Alice!
Welcome to Kotlin programming!

How old are you?
25
You have 75 years until you're 100 years old!
```
