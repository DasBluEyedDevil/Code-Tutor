---
type: "EXAMPLE"
title: "Collecting in Practice"
---

Real-world examples of collecting stream results:

```java
import java.util.*;
import java.util.stream.*;

record Person(int id, String name, int age, String department) {}

void main() {
    List<Person> people = List.of(
        new Person(1, "Alice", 30, "Engineering"),
        new Person(2, "Bob", 25, "Marketing"),
        new Person(3, "Charlie", 35, "Engineering"),
        new Person(4, "Diana", 28, "Marketing")
    );
    
    // Collect names to a list
    List<String> names = people.stream()
        .map(Person::name)
        .toList();
    IO.println(names);  // [Alice, Bob, Charlie, Diana]
    
    // Create ID -> Person map
    Map<Integer, Person> byId = people.stream()
        .collect(Collectors.toMap(Person::id, p -> p));
    IO.println(byId.get(2).name());  // Bob
    
    // Join names with comma
    String joined = people.stream()
        .map(Person::name)
        .collect(Collectors.joining(", "));
    IO.println(joined);  // Alice, Bob, Charlie, Diana
    
    // Get age statistics
    IntSummaryStatistics stats = people.stream()
        .collect(Collectors.summarizingInt(Person::age));
    IO.println("Avg age: " + stats.getAverage());  // 29.5
    IO.println("Max age: " + stats.getMax());      // 35
    
    // Collect to TreeSet (sorted)
    TreeSet<String> sortedNames = people.stream()
        .map(Person::name)
        .collect(Collectors.toCollection(TreeSet::new));
    IO.println(sortedNames);  // [Alice, Bob, Charlie, Diana]
}
```
