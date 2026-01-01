---
type: "THEORY"
title: "The collect() Terminal Operation"
---

After processing elements through a stream pipeline, you often need to gather results into a collection. The collect() method is the primary way to do this.

Basic usage with Collectors:

// Collect to List
List<String> list = stream.collect(Collectors.toList());

// Java 16+: Simpler syntax
List<String> list = stream.toList();  // Returns unmodifiable list

// Collect to Set (removes duplicates)
Set<String> set = stream.collect(Collectors.toSet());

// Collect to specific collection type
TreeSet<String> treeSet = stream.collect(
    Collectors.toCollection(TreeSet::new)
);

The Collectors utility class provides many powerful collectors for different use cases.