---
type: "KEY_POINT"
title: "Collectors and Terminal Operations"
---

COLLECTING TO DIFFERENT TYPES:

// To List
List<String> list = stream.collect(Collectors.toList());

// To Set (removes duplicates)
Set<String> set = stream.collect(Collectors.toSet());

// To Map
Map<String, Integer> map = names.stream()
    .collect(Collectors.toMap(
        name -> name,        // Key mapper
        String::length       // Value mapper
    ));

// Joining strings
String joined = names.stream()
    .collect(Collectors.joining(", ")); // "Alice, Bob, Charlie"

// Grouping
Map<Integer, List<String>> byLength = names.stream()
    .collect(Collectors.groupingBy(String::length));

OTHER TERMINAL OPERATIONS:

// Count
long count = stream.count();

// Sum, average, max, min (for numbers)
int sum = numbers.stream().mapToInt(Integer::intValue).sum();
OptionalDouble avg = numbers.stream().mapToInt(Integer::intValue).average();

// Reduce to single value
Optional<String> concat = names.stream()
    .reduce((a, b) -> a + b);

// Any/All/None match
boolean anyLong = names.stream().anyMatch(s -> s.length() > 10);
boolean allShort = names.stream().allMatch(s -> s.length() < 20);