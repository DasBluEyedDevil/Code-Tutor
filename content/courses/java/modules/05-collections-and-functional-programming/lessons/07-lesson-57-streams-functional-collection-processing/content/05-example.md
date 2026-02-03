---
type: "EXAMPLE"
title: "Stream Pipeline Examples"
---

Stream pipelines chain filter, map, and collect operations for declarative collection processing. Operations are lazy until a terminal operation like collect() triggers processing.

```java
import module java.base;

void main() {
    var names = List.of("Alice", "Bob", "Charlie", "Dan", "Eve");

    // Filter and collect
    var longNames = names.stream()
        .filter(n -> n.length() > 3)
        .toList();
    IO.println(longNames); // [Alice, Charlie]

    // Map and collect
    var lengths = names.stream()
        .map(String::length)
        .toList();
    IO.println(lengths); // [5, 3, 7, 3, 3]

    // Filter, map, collect
    var upperLong = names.stream()
        .filter(n -> n.length() > 3)
        .map(String::toUpperCase)
        .toList();
    IO.println(upperLong); // [ALICE, CHARLIE]

    // Find first matching
    var firstLong = names.stream()
        .filter(n -> n.length() > 3)
        .findFirst();
    firstLong.ifPresent(IO::println); // Alice

    // Count
    long count = names.stream()
        .filter(n -> n.length() == 3)
        .count();
    IO.println("3-letter names: " + count); // 3

    // Sum with mapToInt
    var numbers = List.of(1, 2, 3, 4, 5);
    int sum = numbers.stream()
        .mapToInt(Integer::intValue)
        .sum();
    IO.println("Sum: " + sum); // 15

    // Join strings
    var joined = names.stream()
        .collect(Collectors.joining(", "));
    IO.println(joined); // Alice, Bob, Charlie, Dan, Eve

    // Group by length
    var byLength = names.stream()
        .collect(Collectors.groupingBy(String::length));
    IO.println(byLength); // {3=[Bob, Dan, Eve], 5=[Alice], 7=[Charlie]}
}
```
