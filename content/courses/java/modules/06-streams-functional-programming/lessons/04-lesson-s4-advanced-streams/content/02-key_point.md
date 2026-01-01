---
type: "KEY_POINT"
title: "reduce: Aggregating Values"
---

The reduce() operation combines all elements into a single result using an accumulator function.

Three forms of reduce:

1. With identity and accumulator:
   int sum = numbers.stream().reduce(0, (a, b) -> a + b);
   // Equivalent to: Integer.sum as method reference
   int sum = numbers.stream().reduce(0, Integer::sum);

2. Without identity (returns Optional):
   Optional<Integer> max = numbers.stream()
       .reduce((a, b) -> a > b ? a : b);

3. With identity, accumulator, and combiner (for parallel):
   int result = numbers.parallelStream()
       .reduce(0, Integer::sum, Integer::sum);

Examples:
  // Product of all numbers
  int product = List.of(1, 2, 3, 4).stream()
      .reduce(1, (a, b) -> a * b);  // 24
  
  // Concatenate strings
  String concat = List.of("a", "b", "c").stream()
      .reduce("", (a, b) -> a + b);  // "abc"
  // But prefer Collectors.joining() for strings!