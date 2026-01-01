---
type: "KEY_POINT"
title: "Common Collectors"
---

The Collectors class provides pre-built collectors for common operations:

toList(), toSet(), toCollection()
  Gather elements into a collection

toMap(keyMapper, valueMapper)
  Create a map from stream elements
  Map<Integer, String> map = people.stream()
      .collect(Collectors.toMap(Person::getId, Person::getName));

joining(), joining(delimiter)
  Concatenate strings
  String csv = names.stream().collect(Collectors.joining(", "));
  // "Alice, Bob, Charlie"

counting()
  Count elements (like count() but as a collector)

summarizingInt/Long/Double(mapper)
  Get statistics (count, sum, min, max, average)

averageInt/Long/Double(mapper)
  Calculate average

maxBy(comparator), minBy(comparator)
  Find max/min element