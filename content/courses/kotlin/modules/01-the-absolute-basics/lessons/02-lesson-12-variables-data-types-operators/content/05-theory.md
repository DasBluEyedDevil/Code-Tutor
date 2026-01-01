---
type: "THEORY"
title: "Data Types"
---


Every variable has a **type** that determines what kind of data it can hold.

### Basic Data Types

| Type | Description | Example Values | Memory Size |
|------|-------------|----------------|-------------|
| `Int` | Whole numbers | -2,147,483,648 to 2,147,483,647 | 32 bits |
| `Long` | Large whole numbers | -9 quintillion to 9 quintillion | 64 bits |
| `Short` | Small whole numbers | -32,768 to 32,767 | 16 bits |
| `Byte` | Tiny whole numbers | -128 to 127 | 8 bits |
| `Double` | Decimal numbers | 3.14, -0.001, 1.5e10 | 64 bits |
| `Float` | Smaller decimals | 3.14f, 2.5f | 32 bits |
| `Boolean` | True or false | true, false | 1 bit |
| `Char` | Single character | 'A', 'z', '5', '@' | 16 bits |
| `String` | Text (sequence of characters) | "Hello", "Kotlin" | Variable |

### Examples of Each Type


**Note**: Underscores in numbers improve readability:

---



```kotlin
val million = 1_000_000  // Same as 1000000
val billion = 1_000_000_000L
```
