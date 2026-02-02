---
type: "EXAMPLE"
title: "Building Stream Pipelines"
---

Let's see how to chain operations effectively:

```java
import java.util.*;
import java.util.stream.*;

void main() {
    List<String> names = List.of(
        "Alice", "Bob", "Charlie", "David", "Eve", "Alice"
    );
    
    // Filter, transform, collect
    List<String> result = names.stream()
        .filter(name -> name.length() > 3)      // Alice, Charlie, David, Alice
        .map(String::toUpperCase)                // ALICE, CHARLIE, DAVID, ALICE
        .distinct()                              // ALICE, CHARLIE, DAVID
        .sorted()                                // ALICE, CHARLIE, DAVID
        .collect(Collectors.toList());
    
    IO.println(result);  // [ALICE, CHARLIE, DAVID]
    
    // Count elements matching a condition
    long count = names.stream()
        .filter(name -> name.startsWith("A"))
        .count();
    IO.println("Names starting with A: " + count);  // 2
    
    // Find first match
    Optional<String> first = names.stream()
        .filter(name -> name.contains("v"))
        .findFirst();
    first.ifPresent(name -> IO.println("Found: " + name));  // David
    
    // Numeric operations with IntStream
    int sum = IntStream.rangeClosed(1, 10).sum();
    IO.println("Sum 1-10: " + sum);  // 55
}
```
