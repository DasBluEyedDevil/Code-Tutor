---
type: "EXAMPLE"
title: "Stream Pipeline Examples"
---

Stream pipelines chain filter, map, and collect operations for declarative collection processing. Operations are lazy until a terminal operation like collect() triggers processing.

```java
import java.util.*;
import java.util.stream.*;

public class StreamDemo {
    public static void main(String[] args) {
        List<String> names = List.of("Alice", "Bob", "Charlie", "Dan", "Eve");
        
        // Filter and collect
        List<String> longNames = names.stream()
            .filter(n -> n.length() > 3)
            .collect(Collectors.toList());
        System.out.println(longNames); // [Alice, Charlie]
        
        // Map and collect
        List<Integer> lengths = names.stream()
            .map(String::length)
            .collect(Collectors.toList());
        System.out.println(lengths); // [5, 3, 7, 3, 3]
        
        // Filter, map, collect
        List<String> upperLong = names.stream()
            .filter(n -> n.length() > 3)
            .map(String::toUpperCase)
            .collect(Collectors.toList());
        System.out.println(upperLong); // [ALICE, CHARLIE]
        
        // Find first matching
        Optional<String> firstLong = names.stream()
            .filter(n -> n.length() > 3)
            .findFirst();
        firstLong.ifPresent(System.out::println); // Alice
        
        // Count
        long count = names.stream()
            .filter(n -> n.length() == 3)
            .count();
        System.out.println("3-letter names: " + count); // 3
        
        // Sum with mapToInt
        List<Integer> numbers = List.of(1, 2, 3, 4, 5);
        int sum = numbers.stream()
            .mapToInt(Integer::intValue)
            .sum();
        System.out.println("Sum: " + sum); // 15
        
        // Join strings
        String joined = names.stream()
            .collect(Collectors.joining(", "));
        System.out.println(joined); // Alice, Bob, Charlie, Dan, Eve
        
        // Group by length
        Map<Integer, List<String>> byLength = names.stream()
            .collect(Collectors.groupingBy(String::length));
        System.out.println(byLength); // {3=[Bob, Dan, Eve], 5=[Alice], 7=[Charlie]}
    }
}
```
