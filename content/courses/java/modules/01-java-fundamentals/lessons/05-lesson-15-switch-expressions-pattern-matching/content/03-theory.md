---
type: "THEORY"
title: "Advanced Features: Yield and Pattern Matching"
---

### The `yield` Keyword
Sometimes you need to do more than just return a value in a switch case. You can use a code block `{}` and the `yield` keyword.

```java
int result = switch (command) {
    case "ADD" -> 1 + 1;
    case "LOG" -> {
        System.out.println("Logging...");
        yield 0; // Returns 0 from this block
    }
    default -> -1;
};
```

### Pattern Matching (Java 21+)
You can switch on TYPES, not just values!

```java
Object obj = "Hello";

String result = switch (obj) {
    case Integer i -> "It is an integer: " + i;
    case String s -> "It is a string: " + s;
    case null -> "It is null";
    default -> "Unknown type";
};
```

### Guards (`when`)
You can add extra conditions to patterns.

```java
case Integer i when i > 0 -> "Positive number";
case Integer i when i < 0 -> "Negative number";
```
