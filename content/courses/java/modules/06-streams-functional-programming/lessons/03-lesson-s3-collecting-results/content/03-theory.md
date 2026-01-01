---
type: "THEORY"
title: "Creating Maps with toMap()"
---

Converting a stream to a Map is common but has pitfalls:

Basic toMap:
  Map<Integer, String> idToName = people.stream()
      .collect(Collectors.toMap(
          Person::getId,      // key mapper
          Person::getName     // value mapper
      ));

Handling duplicate keys (throws exception by default!):
  Map<String, Integer> wordCounts = words.stream()
      .collect(Collectors.toMap(
          word -> word,           // key: the word itself
          word -> 1,              // value: count of 1
          (existing, newVal) -> existing + newVal  // merge: add counts
      ));

Specifying map type:
  LinkedHashMap<Integer, String> ordered = people.stream()
      .collect(Collectors.toMap(
          Person::getId,
          Person::getName,
          (a, b) -> a,            // keep first on collision
          LinkedHashMap::new      // use LinkedHashMap
      ));

WARNING: toMap() with duplicate keys throws IllegalStateException unless you provide a merge function!