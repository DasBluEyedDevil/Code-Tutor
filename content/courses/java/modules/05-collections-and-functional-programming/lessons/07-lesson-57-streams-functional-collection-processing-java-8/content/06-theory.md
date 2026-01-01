---
type: "THEORY"
title: "Best Practices for Streams"
---

DO:
- Use streams for transforming collections
- Chain operations for readability
- Use method references when possible
- Use parallel streams for CPU-intensive operations on large data

DON'T:
- Use streams for simple loops (overkill)
- Modify external state in stream operations
- Reuse streams (they can only be consumed once)
- Use parallel streams for I/O operations

PARALLEL STREAMS:
List<String> result = names.parallelStream()
    .filter(n -> n.length() > 3)
    .map(String::toUpperCase)
    .collect(Collectors.toList());

Note: Parallel streams split work across CPU cores.
Only use for CPU-bound operations on large datasets!

WHEN TO USE STREAMS:
- Filtering, mapping, reducing collections
- When you want declarative, readable code
- When operations can be parallelized

WHEN NOT TO USE:
- Simple iteration with side effects
- When performance is critical and data is small
- When you need to modify the original collection