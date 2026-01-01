---
type: "THEORY"
title: "Understanding Your First Program"
---


Let's break down the classic "Hello World" program:

```kotlin
fun main() {
    println("Hello, World!")
}
```

### Line-by-Line Breakdown

**`fun main()`**:
- `fun` = keyword that declares a **function** (a reusable block of code)
- `main` = the name of this function (special name: every program starts here)
- `()` = parentheses hold parameters (inputs to the function—none in this case)

The `main` function is the **entry point** of every Kotlin program. Think of it as the front door—when you run your program, the computer enters through `main()`.

**`{` and `}`**:
- Curly braces create a **code block**
- Everything inside the braces is part of the `main` function

**`println("Hello, World!")`**:
- `println` = a built-in function that **print**s a **line** of text
- `"Hello, World!"` = a **string** (text) to print
- `;` is optional in Kotlin (unlike Java)

**How It Works**:

---



```kotlin
1. Computer starts program
   ↓
2. Finds main() function
   ↓
3. Executes code inside { }
   ↓
4. Calls println() function
   ↓
5. Displays "Hello, World!" on screen
   ↓
6. Program ends
```
