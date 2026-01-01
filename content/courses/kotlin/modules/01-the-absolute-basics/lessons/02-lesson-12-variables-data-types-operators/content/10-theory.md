---
type: "THEORY"
title: "String Operations"
---


### String Concatenation

You can combine strings in two ways:

**Using the `+` operator**:
```kotlin
val firstName = "John"
val lastName = "Doe"
val fullName = firstName + " " + lastName  // "John Doe"
```

**Using string templates** (preferred):
```kotlin
val firstName = "John"
val lastName = "Doe"
val fullName = "$firstName $lastName"  // "John Doe"

// For expressions, use curly braces:
val age = 25
val message = "In 10 years, you'll be ${age + 10} years old"
```

String templates are cleaner and more readable than concatenation with `+`.

### String Properties and Methods

Strings come with many useful properties and methods:

```kotlin
val text = "Hello, Kotlin!"

// Properties
text.length           // 14 (number of characters)

// Common methods
text.uppercase()      // "HELLO, KOTLIN!"
text.lowercase()      // "hello, kotlin!"
text.first()          // 'H' (first character)
text.last()           // '!' (last character)

// Checking content
text.isEmpty()        // false
text.isNotEmpty()     // true
text.startsWith("He") // true
text.endsWith("!")    // true
text.contains("Kot")  // true

// Extracting parts
text.substring(0, 5)  // "Hello"
text.drop(7)          // "Kotlin!"
text.take(5)          // "Hello"

// Finding and replacing
text.indexOf("K")     // 7 (position of 'K')
text.replace("Kotlin", "World")  // "Hello, World!"

// Splitting
val parts = "a,b,c".split(",")  // ["a", "b", "c"]

// Trimming whitespace
"  hello  ".trim()    // "hello"
```

### Accessing Individual Characters

Strings are sequences of characters, accessible by index (starting at 0):

```kotlin
val greeting = "Hello"
val firstLetter = greeting[0]   // 'H'
val thirdLetter = greeting[2]   // 'l'

// Loop through characters
for (char in greeting) {
    println(char)
}
```

### Multi-line Strings

For text that spans multiple lines, use triple quotes:

```kotlin
val poem = """
    Roses are red,
    Violets are blue,
    Kotlin is awesome,
    And so are you!
""".trimIndent()

println(poem)
```

The `trimIndent()` function removes the common leading whitespace, so your output looks clean.

**Raw strings** preserve all formatting, making them useful for:
- JSON templates
- SQL queries
- ASCII art
- Multi-paragraph text

---
