---
type: "KEY_POINT"
title: "Stream Pipeline Structure"
---

A stream pipeline has three parts:

1. SOURCE - Where data comes from
   list.stream()           // From collection
   Arrays.stream(array)    // From array
   Stream.of(a, b, c)      // From values
   Stream.iterate(0, n -> n + 1)  // Generated

2. INTERMEDIATE OPERATIONS - Transform the stream (lazy!)
   .filter(predicate)      // Keep matching elements
   .map(function)          // Transform each element
   .sorted()               // Sort elements
   .distinct()             // Remove duplicates
   .limit(n)               // Take first n
   .skip(n)                // Skip first n

3. TERMINAL OPERATION - Produce result (triggers processing)
   .collect(Collectors.toList())  // To list
   .forEach(consumer)      // Do something with each
   .count()                // Count elements
   .findFirst()            // Get first (Optional)
   .reduce(identity, accumulator)  // Combine to one
   .anyMatch(predicate)    // Any match?
   .allMatch(predicate)    // All match?

KEY: Streams are LAZY - nothing happens until terminal operation!