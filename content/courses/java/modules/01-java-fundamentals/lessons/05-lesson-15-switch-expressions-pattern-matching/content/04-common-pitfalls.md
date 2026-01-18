---
type: "COMMON_PITFALLS"
title: "Common Pitfalls"
---

### 1. Mixing Arrows and Colons
You cannot mix `->` and `:` in the same switch statement.
❌ Wrong:
```java
switch (x) {
    case 1 -> "One";
    case 2: "Two"; // Error!
}
```

### 2. Missing Default
When using a switch *expression* (returning a value), you MUST cover all possible cases. If you don't cover everything, you need a `default`.

❌ Wrong:
```java
String type = switch (day) { // Error: doesn't cover all strings!
    case "MONDAY" -> "Start";
};
```

✅ Right:
```java
String type = switch (day) {
    case "MONDAY" -> "Start";
    default -> "Other";
};
```

### 3. Semicolon at End
A switch expression is a statement, so it needs a semicolon at the end of the closing brace `};`.

```java
var result = switch(x) {
    ...
}; // Don't forget this!
```
