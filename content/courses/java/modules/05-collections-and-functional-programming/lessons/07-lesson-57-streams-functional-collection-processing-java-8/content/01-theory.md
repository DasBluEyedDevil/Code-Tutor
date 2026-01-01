---
type: "THEORY"
title: "The Problem: Verbose Collection Processing"
---

Traditional way to filter and transform a list:

// Get names longer than 3 characters, convert to uppercase
List<String> names = List.of("Alice", "Bob", "Charlie", "Dan");
List<String> result = new ArrayList<>();

for (String name : names) {
    if (name.length() > 3) {           // Filter
        result.add(name.toUpperCase());  // Transform
    }
}
// result: [ALICE, CHARLIE]

Problems:
1. Very verbose (5+ lines for simple operation)
2. Mixes what we want with how to do it
3. Hard to parallelize
4. Lots of mutable state

With STREAMS:

List<String> result = names.stream()
    .filter(name -> name.length() > 3)
    .map(String::toUpperCase)
    .collect(Collectors.toList());

Cleaner, more readable, and easily parallelizable!